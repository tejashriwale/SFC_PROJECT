import Select from "react-select";
import React, { useState, useEffect, useRef } from "react";
import { useNavigate } from "react-router-dom";
import { ArrowLeft } from "feather-icons-react/build/IconComponents";
import withReactContent from "sweetalert2-react-content";
import Swal from "sweetalert2";
import axios from 'axios';
import { baseUrl, ACSPLGUID } from "../../core/json/custom";
import { getUserData } from "../../Context/UserData";
import { Content } from "antd/es/layout/layout";

function OnProccedQuatation({ praid, caid, CSTATE, onRefresh }) {
    console.log("run")
    console.log(praid, caid, CSTATE)
    const navigate = useNavigate();
    const { userdetail } = getUserData();
    const [products, setProducts] = useState([]);
    const [states, setstates] = useState([]);
    const [paymentDues, setpaymentDues] = useState([]);
    const [detailData, setdetailData] = useState([]);
    const [Productdataa, setproductData] = useState([]);
    const [formData, setFormData] = useState({
        QAMAID: "",
        QNO: "",
        QDATE: "",
        requisition_no: "",
        QDUEDATE: "",
        QVAIDID: "",
        QVAID: "",
        QVCONTACT: "",
        QVEMAIL: "",
        QVADDRESS: "",
        BankName: "",
        AccountNumber: "",
        IFSCCode: "",
        BranchName: "",
        QTRANSPORTTERM: "",
        QPAYMENTTERM: "",
        QPDUEDATE: "",
        QNODAYS: 0,
    });

    useEffect(() => {
        const fetchstates = async () => {
            try {
                const headers = {
                    "Content-Type": "application/json",
                    Accept: "*/*",
                };
                const response = await axios.post(
                    `${baseUrl.Url}/backend/api/GET_StateCodeDistinct`,
                    { headers }
                );
                if (response.status !== 200)
                    throw new Error("Failed to fetch vendor data");
                console.log("requisition setails", response.data)
                const data = response.data;
                const statesdata = data
                    .map(({ sstatename, sstatecode }) => ({
                        label: sstatename,
                        value: sstatecode,
                    }));
                setstates(statesdata);
            } catch (error) {
                console.error("Error fetching vendor data:", error);
            }
        };



        const fetchImplications = async () => {
            try {

                const response = await axios.get(
                    baseUrl.Url + "/backend/api/Implications/QTYPE"
                );

                if (response.status != 200)
                    throw new Error("Failed to fetch social media data");

                const data = await response.data;

                const rackTypes = data
                    .filter((item) => item.iGroup === "QTYPE")
                    .map(({ iTitle, iValue }) => ({
                        label: iTitle,
                        value: iValue,
                    }));

                console.log("rack implication", rackTypes)


                setpaymentDues(rackTypes);
            } catch (error) {
                console.error("Error fetching implications:", error);
            }
        };

        const fetchProductData = async () => {
            try {
                const payload = {
                    "ctaid": "%",
                    "companyid": userdetail?.companyID ? userdetail.companyID : "",
                    "deptid": userdetail?.departmentID ? userdetail.departmentID : ""
                }
                const headers = {
                    "Content-Type": "application/json",
                    Accept: "*/*",
                };

                axios({
                    method: "POST",
                    url: baseUrl.Url + "/backend/api/GET_CategoryProduct",
                    data: JSON.stringify(payload),
                    headers: headers,
                })
                    .then((response) => {
                        if (response.status != 200) throw new Error("Failed to Fetching Data");

                        setproductData(response.data)
                        console.log(Productdataa)
                    })

            } catch (error) {
                console.error("Error fetching Access Right Data:", error);
            }

        };
        fetchProductData();
        fetchImplications();

        fetchstates();

    }, []);



    useEffect(() => {
        if (praid) {
            const fetchrequisionDetails = async () => {
                console.log("find master id", praid)
                try {
                    const payload = {
                        praid: praid,
                        companyid: userdetail?.companyID || "",
                        deptid: userdetail?.departmentID || "",
                    };
                    const headers = {
                        "Content-Type": "application/json",
                        Accept: "*/*",
                    };

                    const response = await axios.post(
                        `${baseUrl.Url}/backend/api/GET_PRequisitionDetails`,
                        payload,
                        { headers }
                    );

                    if (response.status !== 200) throw new Error("Failed to fetch vendor data");

                    console.log("Requisition details:", response.data);


                    if (!Array.isArray(response.data) || response.data.length === 0) {
                        console.warn("No requisition details found.");
                        setProducts([]);
                        setdetailData([]);
                        return;
                    }

                    let updatedProducts = response.data;

                    if (CSTATE !== '') {
                        updatedProducts = response.data.map((product) => {
                            let CGST = 0, SGST = 0, IGST = 0;
                            const AMT = product.rate * product.prdquantity;

                            if (CSTATE !== userdetail?.companystate && product.rate === 0) {
                                IGST = product.pigst;
                            } else if (CSTATE === userdetail?.companystate && product.rate === 0) {
                                CGST = product.pcgst;
                                SGST = product.psgst;
                            }

                            const TOTAL = AMT + CGST + SGST + IGST;

                            console.log("Updated Row:", { ...product, pcgst: CGST, psgst: SGST, pigst: IGST, ptotal: TOTAL });

                            return { ...product, pcgst: CGST, psgst: SGST, pigst: IGST, ptotal: TOTAL };
                        });
                    }

                    setProducts(updatedProducts);
                    setdetailData(response.data);

                } catch (error) {
                    console.error("Error fetching vendor data:", error);
                }
            };

            const fetchrequisionMAster = async () => {
                try {
                    const payload = {
                        praid: praid,
                        keyword: "%",
                        companyid: userdetail?.companyID || "",
                        deptid: userdetail?.departmentID || "",
                    };
                    const headers = {
                        "Content-Type": "application/json",
                        Accept: "*/*",
                    };
                    const response = await axios.post(
                        `${baseUrl.Url}/backend/api/GET_PRequisitionMaster`,
                        payload,
                        { headers }
                    );
                    if (response.status !== 200)
                        throw new Error("Failed to fetch vendor data");
                    console.log("requisition master", response.data)
                    if (response.data.length > 0) {
                        setFormData(prevState => ({
                            ...prevState,
                            requisition_no: response.data[0].prno,
                        }));
                    }
                } catch (error) {
                    console.error("Error fetching vendor data:", error);
                }
            };
            fetchrequisionMAster();
            fetchrequisionDetails();
        }
    }, [praid, userdetail]);

    useEffect(() => {
        if (caid) {
            const fetchVendors = async () => {
                console.log("find vendor id", caid)
                try {
                    const payload = {
                        pkid: caid,
                        companyid: userdetail?.companyID || "",
                        deptid: userdetail?.departmentID || "",
                    };
                    const headers = {
                        "Content-Type": "application/json",
                        Accept: "*/*",
                    };
                    const response = await axios.post(
                        `${baseUrl.Url}/backend/api/GET_CustomerDetailsByID`,
                        payload,
                        { headers }
                    );
                    if (response.status !== 200)
                        throw new Error("Failed to fetch vendor data");
                    console.log("venderos", response.data.length)
                    if (response.data.length > 0) {
                        setFormData(prevState => ({
                            ...prevState,
                            QVAIDID: response.data[0].caid,
                            QVAID: response.data[0].ccompanyname,
                            QVCONTACT: response.data[0].ccontactpersonmobile,
                            QVEMAIL: response.data[0].cemail,
                            QVADDRESS: states.find((state) => state.value == response.data[0].cstate)?.value || "",
                            BankName: response.data[0].cbankname,
                            AccountNumber: response.data[0].caccountnumber,
                            IFSCCode: response.data[0].cifsc,
                            BranchName: response.data[0].cbranchname
                        }));

                    }
                } catch (error) {
                    console.error("Error fetching vendor data:", error);
                }
            };

            fetchVendors();
        }

    }, [caid, userdetail]);



    const handleSubmit = (event) => {
        event.preventDefault();
        const form = event.target.closest("form");
        if (!form.checkValidity()) {
            form.reportValidity();
            return;
        }
        console.log("Product Data: ", products);
        const invalidProduct = products.find(p => !p.rate || parseFloat(p.rate) <= 0);

        if (invalidProduct) {
            Swal.fire({
                icon: "warning",
                title: "Missing Rate",
                text: "Every product must have a valid rate.",
            });
            return; // Stop further execution
        }
        showConfirmationAlert(event);
    };


    const handleInputChange = (event) => {
        const { name, value } = event.target;
        const updatedValue = value.replace(/^\s+/, "");

        setFormData({
            ...formData,
            [name]: updatedValue,
        });
    };


    // const handleProductChange = (e, index, field) => {
    //     const updatedValue = e.target.value;
    //     setProducts((prevProducts) =>
    //         prevProducts.map((item, i) =>
    //             i === index ? { ...item, [field]: updatedValue } : item
    //         )
    //     );
    // };


    const handleProductChange = (e, index, field) => {
        const value = parseFloat(e.target.value) || 0;

        const updatedProducts = [...products];
        const product = { ...updatedProducts[index], [field]: value };

        // Calculate tax and total if rate is changed
        if (field === "rate") {
            const quantity = parseFloat(product.prdquantity) || 0;
            const amount = value * quantity;

            if (formData.QVADDRESS !== userdetail?.companystate) {
                // Inter-state: IGST only (18%)
                const IGST = Math.round(amount * 0.18);
                product.pigst = IGST;
                product.pcgst = "";
                product.psgst = "";
            } else {
                // Intra-state: CGST (9%) + SGST (9%)
                const halfGST = (amount * 0.09); // 9% each
                product.pcgst = Math.round(halfGST);
                product.psgst = Math.round(halfGST);
                product.pigst = "";
            }

            const total =
                amount +
                parseFloat(product.pigst || 0) +
                parseFloat(product.pcgst || 0) +
                parseFloat(product.psgst || 0);

            product.ptotal = Math.round(total);
        }

        updatedProducts[index] = product;
        setProducts(updatedProducts);


    };





    const onRateChange = (e, product, field) => {
        console.log(e.target.value, "e");
        console.log(product);
        const updatedValue = parseFloat(e.target.value) || 0;
        const P = products.find((item) => item.prdproduct === product.prdproduct);
        const filteredProducts = detailData.filter((item) => item.prdproduct === P.prdproduct)[0];
        if (P) {
            const AMT = P.prdquantity * updatedValue;
            let CGST = 0, SGST = 0, IGST = 0;
            if (formData.QVADDRESS !== userdetail?.companystate && e.target.value == 0) {
                IGST = filteredProducts.pigst;
            } else if (formData.QVADDRESS == userdetail?.companystate && e.target.value == 0) {
                CGST = filteredProducts.pcgst;
                SGST = filteredProducts.psgst;
            }
            if (formData.QVADDRESS !== userdetail?.companystate && e.target.value > 0) {
                IGST = (AMT * filteredProducts.pigst) / 100;
            } else if (formData.QVADDRESS == userdetail?.companystate && e.target.value > 0) {
                CGST = (AMT * filteredProducts.pcgst) / 100;
                SGST = (AMT * filteredProducts.psgst) / 100;
            }
            const TOTAL = AMT + CGST + SGST + IGST;
            console.log("Updated Row:", P);
            const updatedProducts = products.map((item) =>
                item.prdproduct === product.prdproduct
                    ? { ...item, [field]: updatedValue, pcgst: CGST, psgst: SGST, pigst: IGST, ptotal: TOTAL }
                    : item
            );
            setProducts(updatedProducts);
        }


    };



    const QDUEDATERef = useRef(null);
    const QTRANSPORTTERMRef = useRef(null);
    const QPAYMENTTERMRef = useRef(null);


    const checkFormValidity = (e) => {
        const { QDUEDATE, QTRANSPORTTERM, QPAYMENTTERM } = formData;

        if (!QDUEDATE) {
            Swal.fire({
                icon: "error",
                title: "Validation Error",
                text: "Please enter a valid date.",
            }).then(() => {
                QDUEDATERef.current?.focus();
            });
            return false;
        }

        const today = new Date().toISOString().split('T')[0];
        if (new Date(QDUEDATE) <= new Date(today)) {
            Swal.fire({
                icon: "error",
                title: "Validation Error",
                text: "Due Date must be later than today's date.",
            }).then(() => {
                QDUEDATERef.current?.focus();
            });
            return false;
        }

        if (!QTRANSPORTTERM) {
            Swal.fire({
                icon: "error",
                title: "Validation Error",
                text: "Please enter transport terms.",
            }).then(() => {
                QTRANSPORTTERMRef.current?.focus();
            });
            return;
        }

        if (!QPAYMENTTERM) {
            Swal.fire({
                icon: "error",
                title: "Validation Error",
                text: "Please enter payment terms.",
            }).then(() => {
                QPAYMENTTERMRef.current?.focus();
            });
            return;
        }
        handleSubmit(e);

        return true;

    };

    useEffect(() => {
        const handleShortcut = (e) => {
            if (e.ctrlKey && e.key === 'e') {
                e.preventDefault();
                showExitAlert();

            }
            if (e.ctrlKey && (e.key === 's' || e.key === 'S')) {
                e.preventDefault();
                checkFormValidity(e);
            }
        };

        window.addEventListener('keydown', handleShortcut);

        return () => {
            window.removeEventListener('keydown', handleShortcut);
        };
    }, [formData, navigate]);

    useEffect(() => {

        const today = new Date().toISOString().split('T')[0];
        setFormData(prevData => ({
            ...prevData,
            QDATE: today
        }));
    }, []);

    // const totalAmount = products?.reduce(
    //     (total, product) => {
    //         return total +
    //             (parseFloat(product.rate) || 0) +
    //             (parseFloat(product.pcgst) || 0) +
    //             (parseFloat(product.psgst) || 0) +
    //             (parseFloat(product.pigst) || 0) +
    //             (parseFloat(product.pcess) || 0);
    //     },
    //     0
    // ).toFixed(2);

    const totalAmount = products.reduce((sum, item) => {
        return sum + parseFloat(item.ptotal || 0);
    }, 0);

    const GrossAmount = products.reduce((sum, item) => {
        const quantity = parseFloat(item.prdquantity) || 0;
        const rate = parseFloat(item.rate) || 0;
        return sum + (quantity * rate);
    }, 0);


    const showConfirmationAlert = (event) => {
        MySwal.fire({
            title: "Are you sure?",
            text: "Do you want to save this data?",
            showCancelButton: true,
            confirmButtonColor: "#00ff00",
            confirmButtonText: "SAVE",
            cancelButtonColor: "#092C4C",
            cancelButtonText: "CANCLE",
        }).then((result) => {
            if (result.isConfirmed) {
                handleFormSubmission(event);
            }
        });
    };


    const showExitAlert = () => {
        MySwal.fire({
            title: "Are you sure?",
            text: "Do you want to Exit?",
            showCancelButton: true,
            confirmButtonColor: "#00ff00",
            confirmButtonText: "YES",
            cancelButtonColor: "#092C4C",
            cancelButtonText: "NO",
        }).then((result) => {
            if (result.isConfirmed) {



                // const modal = document.getElementById("onprocedquatation");
                // if (modal) {
                //     modal.classList.remove("show");
                //     modal.style.display = "none";
                //     modal.setAttribute("aria-hidden", "true");


                //     document.querySelectorAll(".modal-backdrop").forEach((backdrop) => {
                //         backdrop.remove();
                //     });


                //     document.body.classList.remove("modal-open");
                //     document.body.style.overflow = "";
                //     document.body.style.paddingRight = "";
                // }
                setFormData({
                    QAMAID: "",
                    QNO: "",
                    QDATE: "",
                    requisition_no: "",
                    QDUEDATE: "",
                    QVAIDID: "",
                    QVAID: "",
                    QVCONTACT: "",
                    QVEMAIL: "",
                    QVADDRESS: "",
                    BankName: "",
                    AccountNumber: "",
                    IFSCCode: "",
                    BranchName: "",
                    QTRANSPORTTERM: "",
                    QPAYMENTTERM: "",
                    QPDUEDATE: "",
                    QNODAYS: 0,
                })
                setProducts();
            }
        });
    };

    const MySwal = withReactContent(Swal);
    const GUID = ACSPLGUID.getNew();
    const handleFormSubmission = async () => {
        try {
            const payload = {
                "caid": formData.QVAIDID,
                "ccontactpersonname": formData.QVAID,
                "ccontactpersonmobile": formData.QVCONTACT,
                "cemail": formData.QVEMAIL,
                "cstate": formData.QVADDRESS.toString(),
                "cbankname": formData.BankName,
                "caccountnumber": formData.AccountNumber,
                "cifsc": formData.IFSCCode,
                "cbranchname": formData.BranchName,
                "companyid": userdetail?.companyID || "",
                "deptid": userdetail?.departmentID || "",
            };
            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };
            const response = await axios.post(
                `${baseUrl.Url}/backend/api/SP_UpdateCustomerDetails`,
                payload,
                { headers }
            );
            if (response.status !== 200)
                throw new Error("Failed to fetch vendor data");
        } catch (error) {
            console.error("Error fetching vendor data:", error);
        }
        try {
            const payload = {
                qamaid: GUID,
                qvaid: formData.QVAIDID,
                qvcontact: formData.QVCONTACT,
                qvemail: formData.QVEMAIL,
                qvaddress: formData.QVADDRESS.toString(),
                prno: formData.requisition_no,
                qmnamt: totalAmount,
                qgamt: GrossAmount,
                qno: formData.QNO,
                qdate: formData.QDATE,
                qduedate: formData.QDUEDATE,
                companyid: userdetail?.companyID || "",
                deptid: userdetail?.departmentID || "",
                qtransportterms: formData.QTRANSPORTTERM,
                qpaymentterms: formData.QPAYMENTTERM,
                qnodays: formData.QNODAYS,
                qpduedate: formData.QPDUEDATE,
                prreff: praid,
            };

            console.log("Payload:", payload);
            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };

            const response1 = await axios.post(
                baseUrl.Url + "/backend/api/AddQuatationMaster",
                payload,
                { headers }
            );


            if (response1.status === 200) {
                const detailPayload = products
                    .filter(product => product.rate > 0)
                    .map(product => ({

                        qamdaid: ACSPLGUID.getNew(),
                        qamaid: GUID,
                        qproduct: product.prdproduct,
                        qpbaseqty: product.prbaseqty || 0,
                        qquantity: parseFloat(product.prdquantity || "0").toString(),
                        quom: product.prduom,
                        qrate: product.rate,
                        qtamt: product.prdquantity * product.rate,
                        qigst: product.pigst,
                        psgst: product.psgst,
                        pcgst: product.pcgst,
                        qcess: product.pcess,
                        companyid: userdetail?.companyID || "",
                        deptid: userdetail?.departmentID || "",
                        hsncode: product.hsncode,
                    }));


                console.log("Detail Payload:", detailPayload);

                const response2 = await axios.post(
                    baseUrl.Url + "/backend/api/AddUpdQuatationDetails",
                    detailPayload,
                    { headers }
                );

                if (response2.status === 200) {
                    Swal.fire({
                        icon: "success",
                        title: "Saved!",
                        text: "Data saved successfully.",
                        confirmButtonText: "OK",
                    })
                    // .then((result) => {
                    //     if (result.isConfirmed) {
                    //         const modal = document.getElementById("onprocedquatation");
                    //         if (modal) {
                    //             modal.classList.remove("show");
                    //             modal.style.display = "none";
                    //             document.body.classList.remove("modal-open");
                    //             document.body.style.overflow = "auto";

                    //             const backdrop = document.querySelector(".modal-backdrop");
                    //             if (backdrop) {
                    //                 backdrop.remove();
                    //             }
                    //         }

                    //         setFormData({
                    //             QAMAID: "",
                    //             QNO: "",
                    //             QDATE: "",
                    //             requisition_no: "",
                    //             QDUEDATE: "",
                    //             QVAIDID: "",
                    //             QVAID: "",
                    //             QVCONTACT: "",
                    //             QVEMAIL: "",
                    //             QVADDRESS: "",
                    //             BankName: "",
                    //             AccountNumber: "",
                    //             IFSCCode: "",
                    //             BranchName: "",
                    //             QTRANSPORTTERM: "",
                    //             QPAYMENTTERM: "",
                    //             QPDUEDATE: "",
                    //             QNODAYS: 0,
                    //         });
                    //         setProducts([]);
                    //         const backdrop = document.querySelector(".modal-backdrop");
                    //         if (backdrop) {
                    //             backdrop.remove();
                    //         }
                    //     }
                    // });
                } else {
                    throw new Error("Failed to save quotation details.");
                }
            } else {
                throw new Error("Failed to save quotation master.");
            }
        } catch (error) {
            console.error("Submission Error:", error);
            Swal.fire({
                icon: "error",
                title: "Error",
                text: "Failed to save data. Please try again.",
            });
        }
    };


    const handleDropdownChange = (selectedOption, field) => {
        setFormData(prevData => ({
            ...prevData,
            [field]: selectedOption ? selectedOption.value : "",
        }));
        // const updatedProducts = products.map((product) => {
        //     let CGST = 0, SGST = 0, IGST = 0;
        //     const filteredProducts = detailData.filter((item) => item.prdproduct === product.prdproduct)[0];
        //     const AMT = product.rate * product.prdquantity;

        //     if (selectedOption.value !== userdetail?.companystate && product.rate == 0) {
        //         IGST = filteredProducts.pigst;
        //     } else if (selectedOption.value == userdetail?.companystate && product.rate == 0) {
        //         CGST = filteredProducts.pcgst;
        //         SGST = filteredProducts.psgst;
        //     }

        //     if (selectedOption.value !== userdetail?.companystate && product.rate > 0) {
        //         IGST = (AMT * filteredProducts.pigst) / 100;
        //     } else if (selectedOption.value == userdetail?.companystate && product.rate > 0) {
        //         CGST = (AMT * filteredProducts.pcgst) / 100;
        //         SGST = (AMT * filteredProducts.psgst) / 100;
        //     }

        //     const TOTAL = AMT + CGST + SGST + IGST;

        //     console.log("Updated Row:", product);

        //     return product.prdproduct === product.prdproduct
        //         ? { ...product, pcgst: CGST, psgst: SGST, pigst: IGST, ptotal: TOTAL }
        //         : product;
        // });

        // setProducts(updatedProducts);

    }



    // const onRateChange = (e, product, field) => {
    //     console.log(e.target.value, "e");
    //     console.log(product);
    //     const updatedValue = parseFloat(e.target.value) || 0;
    //     const P = products.find((item) => item.prdproduct === product.prdproduct);
    //     const filteredProducts = detailData.filter((item) => item.prdproduct === P.prdproduct)[0];
    //     if (P) {
    //         const AMT = P.prdquantity * updatedValue;
    //         let CGST = 0, SGST = 0, IGST = 0;
    //         if (formData.QVADDRESS !== userdetail?.companystate && e.target.value == 0) {
    //             IGST = filteredProducts.pigst;
    //         } else if (formData.QVADDRESS == userdetail?.companystate && e.target.value == 0) {
    //             CGST = filteredProducts.pcgst;
    //             SGST = filteredProducts.psgst;
    //         }
    //         if (formData.QVADDRESS !== userdetail?.companystate && e.target.value > 0) {
    //             IGST = (AMT * filteredProducts.pigst) / 100;
    //         } else if (formData.QVADDRESS == userdetail?.companystate && e.target.value > 0) {
    //             CGST = (AMT * filteredProducts.pcgst) / 100;
    //             SGST = (AMT * filteredProducts.psgst) / 100;
    //         }
    //         const TOTAL = AMT + CGST + SGST + IGST;
    //         console.log("Updated Row:", P);
    //         const updatedProducts = products.map((item) =>
    //             item.prdproduct === product.prdproduct
    //                 ? { ...item, [field]: updatedValue, pcgst: CGST, psgst: SGST, pigst: IGST, ptotal: TOTAL }
    //                 : item
    //         );
    //         setProducts(updatedProducts);
    //     }


    // };

    // const onTaxesChange = (e, product, field) => {
    //     const updatedValue = parseFloat(e.target.value) || 0;
    //     const P = products.find((item) => item.prdproduct === product.prdproduct);

    //     if (P) {
    //         const AMT = P.prdquantity * P.rate;
    //         let CGST = P.pcgst, SGST = P.psgst, IGST = P.pigst;


    //         const TOTAL = AMT + CGST + SGST + IGST;

    //         console.log("Updated Row:", P);

    //         const updatedProducts = products.map((item) =>
    //             item.prdproduct === product.prdproduct
    //                 ? { ...item, [field]: updatedValue, pcgst: CGST, psgst: SGST, pigst: IGST, ptotal: TOTAL }
    //                 : item
    //         );

    //         setProducts(updatedProducts);
    //     }
    // };

    const onTaxesChange = (e, product, field) => {
        const updatedValue = parseFloat(e.target.value) || 0;
        const P = products.find((item) => item.prdproduct === product.prdproduct);

        if (P) {
            const AMT = parseFloat(P.prdquantity) * parseFloat(P.rate || 0);
            let CGST = parseFloat(field === "pcgst" ? updatedValue : P.pcgst || 0);
            let SGST = parseFloat(field === "psgst" ? updatedValue : P.psgst || 0);
            let IGST = parseFloat(field === "pigst" ? updatedValue : P.pigst || 0);

            const TOTAL = AMT + CGST + SGST + IGST;

            const updatedProducts = products.map((item) =>
                item.prdproduct === product.prdproduct
                    ? {
                        ...item,
                        [field]: updatedValue,
                        pcgst: Math.round(CGST),
                        psgst: Math.round(SGST),
                        pigst: Math.round(IGST),
                        ptotal: Math.round(TOTAL),
                    }
                    : item
            );

            setProducts(updatedProducts);
        }
    };


    return (
        <div>
            <div
                className="modal fade"
                id="onprocedquatation"
                tabIndex={-1}
                aria-labelledby="exampleModalFullscreenLabel"
                aria-hidden="true"
            >
                <div className="modal-dialog modal-fullscreen">
                    <div className="modal-content mbgcolor">
                        <div className="modal-body mbgcolor">
                            <div className="modal-content mbgcolor">
                                <div className="page-wrapper-new p-0">
                                    <div className="content mbgcolor">
                                        <div className="modal-header border-0 custom-modal-header">
                                            <div className="page-title">
                                                <h4>Add Quatation</h4>
                                            </div>
                                            <div className="d-flex justify-content-between align-items-center">
                                                <ul className="table-top-head">
                                                    <li>
                                                        <div className="page-btn">
                                                            <button
                                                                className="btn btn-secondary"
                                                                aria-label="Close"

                                                                onClick={showExitAlert}
                                                            >
                                                                <ArrowLeft className="me-2" />
                                                                Back to Editpurchase
                                                            </button>
                                                        </div>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>

                                        <div className="modal-body custom-modal-body">
                                            <form onSubmit={handleSubmit}>
                                                <div className="row mb-3">
                                                    <div className="col-lg-3 col-md-4 col-12">
                                                        <div className="">
                                                            <label className="form-label required">Quotation No:</label>
                                                            <input
                                                                name="QNO"
                                                                type="text"
                                                                className="form-control"
                                                                value={formData.QNO}
                                                                onChange={handleInputChange}
                                                                required
                                                                autoFocus
                                                            />
                                                        </div>
                                                    </div>

                                                    <div className="col-lg-3 col-md-4 col-12">
                                                        <div className="form-label">
                                                            <label className="form-label required">Quotation Date:</label>
                                                            <input
                                                                type="date"
                                                                name="QDATE"
                                                                className="form-control"
                                                                value={formData.QDATE}
                                                                onChange={handleInputChange}
                                                                required
                                                            />
                                                        </div>
                                                    </div>

                                                    <div className="col-lg-3 col-md-4 col-12">
                                                        <div className="form-label">
                                                            <label className="form-label">Requisition No:</label>
                                                            <input
                                                                name="requisition_no"
                                                                className="form-control"
                                                                value={formData.requisition_no}
                                                                onChange={handleInputChange}
                                                                readOnly
                                                            />
                                                        </div>
                                                    </div>

                                                    <div className="col-lg-3 col-md-4 col-12">
                                                        <div className="form-label">
                                                            <label className="form-label required">Quotation Validity Date:</label>
                                                            <input
                                                                type="date"
                                                                name="QDUEDATE"
                                                                className="form-control"
                                                                ref={QDUEDATERef}
                                                                value={formData.QDUEDATE}
                                                                onChange={handleInputChange}
                                                                required
                                                                min={
                                                                    formData.QDATE
                                                                        ? new Date(new Date(formData.QDATE).getTime() + 86400000).toISOString().split('T')[0]
                                                                        : undefined
                                                                }
                                                            />
                                                        </div>
                                                    </div>
                                                </div>

                                                <div className="row mb-3 mbgcolor">
                                                    <div className="">
                                                        <h5>
                                                            <span>Vendor Details :</span>
                                                        </h5>
                                                    </div>
                                                    <div className="col-lg-3 col-md-3 col-12">
                                                        <div className="form-label">
                                                            <label className="form-label ">Vendor Name:</label>
                                                            <input
                                                                type="text"
                                                                name="QVAID"
                                                                className="form-control"
                                                                value={formData.QVAID}
                                                                onChange={handleInputChange}
                                                            />
                                                        </div>
                                                    </div>

                                                    <div className="col-lg-3 col-md-3 col-12">
                                                        <div className="form-label">
                                                            <label className="form-label ">Vendor Contact:</label>
                                                            <input
                                                                type="number"
                                                                name="QVCONTACT"
                                                                className="form-control"
                                                                value={formData.QVCONTACT}
                                                                onChange={handleInputChange}
                                                            />
                                                        </div>
                                                    </div>

                                                    <div className="col-lg-3 col-md-3 col-12">
                                                        <div className="form-label">
                                                            <label className="form-label ">Vendor Email:</label>
                                                            <input
                                                                type="email"
                                                                name="QVEMAIL"
                                                                className="form-control"
                                                                value={formData.QVEMAIL}
                                                                onChange={handleInputChange}
                                                            />
                                                        </div>
                                                    </div>

                                                    <div className="col-lg-3 col-md-3 col-12">
                                                        <div className="form-label">
                                                            <label className="form-label ">Vendor State:</label>
                                                            <Select
                                                                classNamePrefix="react-select"
                                                                options={states}
                                                                openMenuOnFocus={true}
                                                                value={states.find(option => option.value == formData.QVADDRESS) || null}
                                                                onChange={(selectedOption) => handleDropdownChange(selectedOption, "QVADDRESS")}
                                                            />
                                                        </div>
                                                    </div>
                                                </div>

                                                <div className="accordion-card-one accordion" id="accordionExample">
                                                    <div className="accordion-item mbgcolor">
                                                        <div className="accordion-header" id="headingOne">
                                                            <div
                                                                className=""
                                                                data-bs-toggle="collapse"
                                                                data-bs-target="#collapseOne"
                                                                aria-controls="collapseOne"
                                                            >
                                                                <div className="addproduct-icon">
                                                                    <h5>
                                                                        <span>Bank Details :</span>
                                                                    </h5>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div
                                                            id="collapseOne"
                                                            className="accordion-collapse collapse show"
                                                            aria-labelledby="headingOne"
                                                            data-bs-parent="#accordionExample"
                                                        >
                                                            <div className="accordion-body">
                                                                <div className="row">
                                                                    <div className="col-lg-3 col-sm-6 col-12 mb-3">
                                                                        <label className="form-label">Bank Name:</label>
                                                                        <input
                                                                            type="text"
                                                                            className="form-control"
                                                                            name="BankName"
                                                                            value={formData.BankName}
                                                                            onChange={handleInputChange}
                                                                        />
                                                                    </div>

                                                                    <div className="col-lg-3 col-sm-6 col-12 mb-3">
                                                                        <label className="form-label">Account Number:</label>
                                                                        <input
                                                                            type="text"
                                                                            className="form-control"
                                                                            name="AccountNumber"
                                                                            value={formData.AccountNumber}
                                                                            onChange={handleInputChange}
                                                                        />
                                                                    </div>

                                                                    <div className="col-lg-3 col-sm-6 col-12 mb-3">
                                                                        <label className="form-label">IFSC Code:</label>
                                                                        <input
                                                                            type="text"
                                                                            className="form-control"
                                                                            name="IFSCCode"
                                                                            value={formData.IFSCCode}
                                                                            onChange={handleInputChange}
                                                                        />
                                                                    </div>

                                                                    <div className="col-lg-3 col-sm-6 col-12 mb-3">
                                                                        <label className="form-label">Branch Name:</label>
                                                                        <input
                                                                            type="text"
                                                                            className="form-control"
                                                                            name="BranchName"
                                                                            value={formData.BranchName}
                                                                            onChange={handleInputChange}
                                                                        />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div className="col-lg-12">
                                                    <div className="border p-3 rounded shadow-sm mb-4">
                                                        <div className="modal-body-table">
                                                            <div className="table-responsive">
                                                                <table className="table table-bordered">
                                                                    <thead className="thead-dark">
                                                                        <tr>
                                                                            <th className="col-4">Product</th>
                                                                            <th className="col-1">HSN Code</th>
                                                                            <th className="col-1">Quantity</th>
                                                                            <th className="col-1">UOM</th>
                                                                            <th className="col-1">Rate</th>
                                                                            <th className="col-1">CGST</th>
                                                                            <th className="col-1">SGST</th>
                                                                            <th className="col-1">IGST</th>
                                                                            {/* <th className="col-1">CESS</th> */}
                                                                            <th className="col-1">Total</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        {products?.map((product, index) => (
                                                                            <tr key={index}>
                                                                                <td className="col-4" style={{ padding: "5px 10px" }}>
                                                                                    {product.pname}
                                                                                </td>
                                                                                <td className="col-1" style={{ padding: "5px 10px" }}>
                                                                                    {product.hsncode}
                                                                                </td>
                                                                                <td className="col-1" style={{ padding: "5px 10px" }}>
                                                                                    {product.prdquantity}
                                                                                </td>
                                                                                <td className="col-1" style={{ padding: "5px 10px" }}>
                                                                                    {product.uomtitle}
                                                                                </td>
                                                                                <td className="col-1" style={{ padding: "5px 10px" }}>
                                                                                    <input
                                                                                        value={product.rate}
                                                                                        className="form-control form-control-sm border-0 border-bottom border-primary"
                                                                                        onChange={(e) => handleProductChange(e, index, "rate")}
                                                                                        // onBlur={(e) => onRateChange(e, product, "rate")}
                                                                                        style={{ height: "30px" }}
                                                                                        required
                                                                                    />
                                                                                </td>
                                                                                <td className="col-1" style={{ padding: "5px 10px" }}>
                                                                                    <input
                                                                                        value={product.pcgst}
                                                                                        className="form-control form-control-sm border-0 border-bottom border-primary"
                                                                                        onChange={(e) => handleProductChange(e, index, "pcgst")}
                                                                                        style={{ height: "30px" }}
                                                                                        onBlur={(e) => onTaxesChange(e, product, "pcgst")}
                                                                                        readOnly={formData.QVADDRESS !== userdetail?.companystate}
                                                                                    />
                                                                                </td>
                                                                                <td className="col-1" style={{ padding: "5px 10px" }}>
                                                                                    <input
                                                                                        value={product.psgst}
                                                                                        className="form-control form-control-sm border-0 border-bottom border-primary"
                                                                                        onChange={(e) => handleProductChange(e, index, "psgst")}
                                                                                        style={{ height: "30px" }}
                                                                                        onBlur={(e) => onTaxesChange(e, product, "psgst")}
                                                                                        readOnly={formData.QVADDRESS !== userdetail?.companystate}
                                                                                    />
                                                                                </td>
                                                                                <td className="col-1" style={{ padding: "5px 10px" }}>
                                                                                    <input
                                                                                        value={product.pigst}
                                                                                        className="form-control form-control-sm border-0 border-bottom border-primary"
                                                                                        onChange={(e) => handleProductChange(e, index, "pigst")}
                                                                                        style={{ height: "30px" }}
                                                                                        onBlur={(e) => onTaxesChange(e, product, "pigst")}
                                                                                        readOnly={formData.QVADDRESS == userdetail?.companystate}
                                                                                    />
                                                                                </td>

                                                                                <td className="col-1" style={{ padding: "5px 10px" }}>
                                                                                    <span>
                                                                                        {product?.ptotal || 0}
                                                                                    </span>
                                                                                </td>
                                                                            </tr>
                                                                        ))}
                                                                    </tbody>
                                                                    <tfoot>
                                                                        <tr>
                                                                            <td colSpan="8" className="text-end">Total:</td>
                                                                            <td className="col-1">
                                                                                <span>{totalAmount}</span>
                                                                            </td>
                                                                        </tr>
                                                                    </tfoot>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div className="col-12">
                                                    <div className="mb-3">
                                                        <label className="form-label required">Transport Terms:</label>
                                                        <textarea
                                                            rows="5"
                                                            className="form-control"
                                                            placeholder="Enter text"
                                                            name="QTRANSPORTTERM"
                                                            ref={QTRANSPORTTERMRef}
                                                            value={formData.QTRANSPORTTERM}
                                                            onChange={handleInputChange}
                                                            required
                                                        />
                                                    </div>
                                                </div>

                                                <div className="col-12">
                                                    <div className="mb-3">
                                                        <div className="row mb-3">
                                                            <div className="addproduct-icon">
                                                                <h5>
                                                                    <span>Payment Details :</span>
                                                                </h5>
                                                            </div>

                                                            <div className="col-lg-3 col-md-3 col-12">
                                                                <div className="form-label">
                                                                    <label className="form-label ">Payment Due Date:</label>
                                                                    <Select
                                                                        classNamePrefix="react-select"
                                                                        options={paymentDues}
                                                                        openMenuOnFocus={true}
                                                                        value={paymentDues.find(option => option.value === formData.QPDUEDATE) || null}
                                                                        onChange={(selectedOption) => handleDropdownChange(selectedOption, "QPDUEDATE")}
                                                                    />
                                                                </div>
                                                            </div>

                                                            <div className="col-lg-3 col-md-3 col-12">
                                                                <div className="form-label">
                                                                    {formData.QPDUEDATE === "2" && (
                                                                        <>
                                                                            <div>
                                                                                <label className="form-label required">Enter Number of Days:</label>
                                                                            </div>
                                                                            <input
                                                                                type="number"
                                                                                className="form-control"
                                                                                name="QNODAYS"
                                                                                value={formData.QNODAYS}
                                                                                onChange={handleInputChange}
                                                                                required
                                                                                min="1"
                                                                                max="365"
                                                                                title="Please enter a valid number between 1 and 365"
                                                                                pattern="\d*"
                                                                            />
                                                                        </>
                                                                    )}
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <label className="form-label required">Payment Terms:</label>
                                                        <textarea
                                                            rows="5"
                                                            className="form-control"
                                                            placeholder="Enter text"
                                                            name="QPAYMENTTERM"
                                                            ref={QPAYMENTTERMRef}
                                                            value={formData.QPAYMENTTERM}
                                                            onChange={handleInputChange}
                                                            required
                                                        />
                                                    </div>
                                                </div>

                                                <div className="btn-addproduct mb-4 mt-3">
                                                    <button type="button" className="btn btn-cancel me-2" onClick={showExitAlert}>
                                                        Exit
                                                    </button>

                                                    <button type="submit" className="btn btn-submit">
                                                        Save
                                                    </button>
                                                </div>
                                            </form>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            {/* )} */}
        </div>
    )
}

export default OnProccedQuatation
