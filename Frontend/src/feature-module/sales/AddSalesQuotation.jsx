import React, { useState, useEffect, useRef } from "react";
import {
    ArrowLeft,
    Trash2,
    Edit
} from "feather-icons-react/build/IconComponents";
import { Link } from "react-router-dom";
// import TextEditor from "../../../feature-module/inventory/texteditor";
// import ImageWithBasePath from "../../img/imagewithbasebath";
import Select from "react-select";
// import { DatePicker } from "antd";
// import { Calendar } from "feather-icons-react/build/IconComponents";
import { useNavigate } from "react-router-dom";
// import { ACSPLGUID, baseUrl, convertToISODate } from '../../json/custom';
import { baseUrl, ACSPLGUID, convertToISODate } from "../../core/json/custom";
import Swal from "sweetalert2";
import withReactContent from "sweetalert2-react-content";
import axios from 'axios';
// import { all_routes } from "../../../Router/all_routes";
import { getUserData } from "../../Context/UserData";
import { all_routes } from "../../Router/all_routes";
import { current } from "@reduxjs/toolkit";
const AddSaleQuotation = ({ SQAMAID, onRefresh }) => {
    const { userdetail } = getUserData();
    console.log(SQAMAID, "sqamaid")
    const route = all_routes;
    const MySwal = withReactContent(Swal);
    const navigate = useNavigate();
    const [states, setstates] = useState([]);
    const [customer, setcustomer] = useState([]);
    const [transporter, settransporter] = useState([]);
    const [product, setproduct] = useState([]);
    const [HSNDATA, setHSNDATA] = useState([]);

    // const [SPRNO, setSPRNO] = useState([]);
    const [uom, setuom] = useState([]);
    // const [REQNNO, setREQNNO] = useState();
    const SQNORef = useRef(null);
    const SQDATERef = useRef(null);
    const SQCEMAILRef = useRef(null);
    const SQCADDRESSRef = useRef(null);
    const SQTRANSPORTTERMSRef = useRef(null);
    const STADDRESSRef = useRef(null);
    const SQTRANSPORTIDRef = useRef(null);
    // const SQTRANSPORTID = useRef(null);
    const SQTCONTACTRef = useRef(null);
    const [Data, setData] = useState([]);
    const [Tdata, setTdata] = useState([]);
    const SQCCONTACTRef = useRef(null);
    const SQCAIDRef = useRef(null);
    const SPRNORef = useRef(null);
    const SQPRODUCTRef = useRef(null);
    const SQUOMRef = useRef(null);
    const SQQUANTITYRef = useRef(null);
    const SQRATERef = useRef(null);
    const SQTAMTRef = useRef(null);
    const SQCGSTRef = useRef(null);
    const SQSGSTRef = useRef(null);
    const SQIGSTRef = useRef(null);
    // const [showForm, setShowForm] = useState(false);

    const [rows, setRows] = useState([]);

    const [productData, setProductData] = useState({
        sqamdaid: '',
        SQPRODUCT: '',
        SQUOM: '',
        SQQUANTITY: '0',
        SQRATE: 0,
        SQTAMT: 0,
        SQCGST: 0,
        SQSGST: 0,
        SQIGST: 0,
        SQMNAMT: 0,
        HSNCODE: 0,
        IsDeleted: 0,
        SQTAXBALEAMOUNT: 0
    });

    const [formData, setFormData] = useState({
        SQAMAID: '',
        SQNO: '',
        SQDATE: userdetail?.APPDT || new Date().toISOString().split('T')[0],
        SPRNO: '',
        SQDUEDATE: '',
        SQCAID: '',
        SQCCONTACT: '',
        SQCEMAIL: '',
        SQCADDRESS: '',
        STADDRESS: '',
        CAID: '',
        SQTCONTACT: '',
        Net_Amount: '',
        SQTRANSPORTTERMS: '',
        SQPDUEDATE: '',
        SQNODAYS: '',
        SQPAYMENTTERMS: '',
        SQTRANSPORTID: '',
        SQMNAMT: 0,
        SQGAMT: 0,
    });

    const handleSubmit = (e, event) => {
        e.preventDefault();

        if (rows.length === 0) {
            Swal.fire({
                icon: "error",
                title: "Validation Error",
                text: "Please add at least one product before saving.",
            });
            return;
        }

        console.log('Form Data:', formData);
        showConfirmationAlert(event);

    };


    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData((prevData) => ({
            ...prevData,
            [name]: value,
        }));
    };

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

                const modal = document.getElementById("AddSaleQuotation");
                if (modal) {
                    // Hide modal
                    modal.classList.remove("show");
                    modal.style.display = "none";
                    modal.setAttribute("aria-hidden", "true");
                    // Remove backdrop if exists
                    const modalBackdrop = document.querySelector(".modal-backdrop");
                    if (modalBackdrop) {
                        modalBackdrop.remove();
                    }
                    // Clear modal-related attributes (important for fresh open)
                    modal.removeAttribute("aria-modal");
                    modal.removeAttribute("role");
                    // Clean up body classes and styles
                    document.body.classList.remove("modal-open");
                    document.body.style.overflow = "";
                    document.body.style.paddingRight = "";
                }
                const backdrop = document.querySelector(".modal-backdrop");
                if (backdrop) {
                    backdrop.remove();
                }

                // Reset formData
                setFormData({
                    SQNO: '',
                    SQDATE: userdetail?.APPDT || new Date().toISOString().split("T")[0],
                    SPRNO: '',
                    SQDUEDATE: '',
                    SQCAID: '',
                    SQCCONTACT: '',
                    SQCEMAIL: '',
                    SQCADDRESS: '',
                    STADDRESS: '',
                    CAID: '',
                    SQTCONTACT: '',
                    Net_Amount: '',
                    SQTRANSPORTTERMS: '',
                    SQPDUEDATE: '',
                    SQNODAYS: '',
                    SQPAYMENTTERMS: '',
                    SQTRANSPORTID: '',
                    SQMNAMT: 0,
                    SQGAMT: 0,
                    srno: '',
                });
                setRows([]);

                if (onRefresh) {
                    onRefresh();
                }

            }
        });
    }

    //             const modal = document.getElementById("AddSaleQuotation");

    //             if (modal) {
    //                 // Hide modal
    //                 modal.classList.remove("show");
    //                 modal.style.display = "none";
    //                 modal.setAttribute("aria-hidden", "true");

    //                 // Remove backdrop if exists
    //                 const modalBackdrop = document.querySelector(".modal-backdrop");
    //                 if (modalBackdrop) {
    //                     modalBackdrop.remove();
    //                 }

    //                 // Clear modal-related attributes (important for fresh open)
    //                 modal.removeAttribute("aria-modal");
    //                 modal.removeAttribute("role");

    //                 // Clean up body classes and styles
    //                 document.body.classList.remove("modal-open");
    //                 document.body.style.overflow = "";
    //                 document.body.style.paddingRight = "";
    //             }
    //             const backdrop = document.querySelector(".modal-backdrop");
    //             if (backdrop) {
    //                 backdrop.remove();
    //             }

    //             // if (result.isConfirmed) {
    //             //     // Navigate to SalesEnquiry after user clicks OK
    //             //     window.location.href = route.SalesEnquiry;
    //             // }
    //             if (onRefresh) {
    //                 onRefresh();
    //             }
    //         }


    //     });
    // };


    const SQDUEDATERef = useRef(null);
    // const SQTRANSPORTTERMRef = useRef(null);
    // const SQPAYMENTTERMSRef = useRef(null);

    // const location = useLocation();
    // const { SQAMAID } = location.state || {};
    // const { SQAMDAID } = location.state || {};
    const GUID = ACSPLGUID.getNew();
    // const GUAID = ACSPLGUID.getNew();

    const handleFormSubmission = async () => {
        try {
            const payload = {
                "sqamaid": SQAMAID ? SQAMAID : GUID,
                "sqcaid": formData.SQCAID,
                "caid": "",
                "sprno": formData.SPRNO || ACSPLGUID.getNew(),
                "sqmnamt": formData.SQMNAMT,
                "sqgamt": formData.SQGAMT,
                "sqno": formData.SQNO || ACSPLGUID.getNew(),
                "sqdate": formData.SQDATE,
                "sqduedate": formData.SQDUEDATE,
                "sqtransportterms": formData.SQTRANSPORTTERMS,
                "sqpaymentterms": "",
                "isapproved": true,
                "sqpduedate": "",
                "sqnodays": 0,
                "companyid": userdetail?.companyID || "",
                "deptid": userdetail?.departmentID || "",
                "srreff": "",
                "sqtransportid": formData.SQTRANSPORTID,
                "satatusid": '1'
            };

            console.log("payload", payload)
            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };

            // First API call to save the quotation master
            axios({
                method: "POST",
                // url: "http://adsvr:78/api/AddQuatationMaster",
                url: baseUrl.Url + "/backend/api/SP_AddUpdSQuatationMaster ",
                data: JSON.stringify(payload),
                headers: headers,
            });

            Swal.fire({
                icon: "success",
                title: "Saved!",
                text: "Data saved successfully.",
                confirmButtonText: "OK",
            });

            // Prepare data for second API call
            const payload2 = rows.map((row) => ({
                "sqamdaid": row.sqamdaid ? row.sqamdaid : ACSPLGUID.getNew(),
                "sqamaid": SQAMAID ? SQAMAID : GUID,
                "sqproduct": row.SQPRODUCT,
                "sqquantity": row.SQQUANTITY.toString(),
                "squom": row.SQUOM,
                "sqrate": row.SQRATE,
                "sqtamt": row.SQTAMT,
                "sqigst": row.SQIGST,
                "sqsgst": row.SQSGST,
                "sqcgst": row.SQCGST,
                "sqcess": row.SQCESS,
                "hsncode": row.HSNCODE,
                "sqtaxbaleamount": row.SQTAXBALEAMOUNT,
                "companyid": userdetail?.companyID || "",
                "deptid": userdetail?.departmentID || "",
                "isDeleted": row.IsDeleted === 1 || row.IsDeleted === true ? true : false,
                "statusid": '1'

            }));

            console.log("payload2", payload2);


            axios({
                method: "POST",
                // url: "http://adsvr:78/api/AddUpdQuatationDetails",
                url: baseUrl.Url + "/backend/api/SP_AddUpdSQuatationDetails",
                data: JSON.stringify(payload2),
                headers: headers,
            });


            Swal.fire({
                icon: "success",
                title: "Saved!",
                text: "Data saved successfully.",
                confirmButtonText: "OK",
            }).then((result) => {

                if (result.isConfirmed) {
                    const modal = document.getElementById("AddSaleQuotation");
                    if (modal) {
                        // Hide modal
                        modal.classList.remove("show");
                        modal.style.display = "none";
                        modal.setAttribute("aria-hidden", "true");
                        // Remove backdrop if exists
                        const modalBackdrop = document.querySelector(".modal-backdrop");
                        if (modalBackdrop) {
                            modalBackdrop.remove();
                        }
                        // Clear modal-related attributes (important for fresh open)
                        modal.removeAttribute("aria-modal");
                        modal.removeAttribute("role");
                        // Clean up body classes and styles
                        document.body.classList.remove("modal-open");
                        document.body.style.overflow = "";
                        document.body.style.paddingRight = "";
                    }
                    const backdrop = document.querySelector(".modal-backdrop");
                    if (backdrop) {
                        backdrop.remove();
                    }

                    setFormData({
                        SQAMAID: "",
                        SQNO: "",
                        SQDATE: "",
                        SQDUEDATE: "",
                        CAID: "",
                        SQVCONTACT: "",
                        SQVEMAIL: "",
                        SQVADDRESS: "",
                        SPRNO: "",
                        SQTRANSPORTTERMS: "",
                        QPAYMENTTERM: "",
                        QPDUEDATE: "",
                        QNODAYS: "",
                        SQTRANSPORTID: "",
                    });

                    setProductData({
                        sqamdaid: '',
                        SQPRODUCT: '',
                        SQUOM: '',
                        SQQUANTITY: '0',
                        SQRATE: 0,
                        SQTAMT: 0,
                        SQCGST: 0,
                        SQSGST: 0,
                        SQIGST: 0,
                        SQMNAMT: 0,
                        HSNCODE: 0,
                        IsDeleted: 0,
                        SQTAXBALEAMOUNT: 0
                    });
                    setRows([]);
                }
            });

        } catch (error) {
            console.error("Submission Error:", error);
            Swal.fire({
                icon: "error",
                title: "Error",
                text: "Failed to save data. Please try again.",
            });
        }
    };

    const handleDetailDropdownChange = (selectedOption, field) => {
        setProductData(prevData => ({
            ...prevData,
            [field]: selectedOption ? selectedOption.value : "",
        }));
        // if (field == 'product') {
        //     const selectedProduct = hsnData(response.data).find(p => p.paid === selectedOption.value);
        //     if (selectedProduct) {
        //         setProductData(prevData => ({
        //             ...prevData,
        //             HSNCODE: selectedProduct.hsncode,
        //         }));

        //     }

        // }


    };

    const onproductchange = (selectedOption) => {

        const newProductId = selectedOption ? selectedOption.value : '';


        const selectedProduct = HSNDATA.find(p => p.paid === newProductId);
        if (selectedProduct) {
            setProductData(prevData => ({
                ...prevData,
                HSNCODE: selectedProduct.hsncode,
            }));
        }


        const payload =
        {
            "companyid": userdetail?.companyID || "",
            "deptid": userdetail?.departmentID || "",
            "paid": newProductId,

        }

        const headers = {
            "Content-Type": "application/json",
            Accept: "*/*",
        };

        axios({
            method: "POST",
            url: baseUrl.Url + "/backend/api/GET_StockCount",
            data: payload,
            headers: headers,
        })



            .then(response => {
                console.log("Stock Count API Success:", response.data);
                console.log("Axios URL:", baseUrl.Url + "/backend/api/SP_StockCount");
                console.log("Axios Payload:", JSON.stringify(payload));
                const stockData = response.data;

                if (stockData && stockData.length > 0) {
                    setProductData(prevData => ({
                        ...prevData,

                        STOCK: stockData[0].totalQuantity
                    }));
                }

            })

    };
    const handleDropdownChange = (selectedOption, field) => {
        setFormData(prevData => ({
            ...prevData,
            [field]: selectedOption ? selectedOption.value : "",
        }));
    }



    useEffect(() => {

        // const fetchImplications = async () => {
        //     try {
        //         const response = await axios.get(
        //             baseUrl.Url + "/backend/api/Implications/SOLID|LIQUID|GAS",
        //             // "http://adsvr:78/api/Implications/SOLID|LIQUID|GAS"
        //         );

        //         if (response.status !== 200) throw new Error("Failed to fetch implications data");

        //         const data = response.data;

        //         // Combine all implications into one array
        //         const implicationsDropdown = data.map(({ iTitle, iValue }) => ({
        //             label: iTitle,
        //             value: iValue,
        //         }));

        //         setuom(implicationsDropdown);
        //     } catch (error) {
        //         console.error("Error fetching implications:", error);
        //     }
        // };
        const fetchProductData = async () => {
            try {
                const payload = {
                    "ctaid": "%",
                    "companyid": userdetail?.companyID || "",
                    "deptid": userdetail?.departmentID || "",
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
                        const DATA = response.data;
                        const formofproductData = DATA
                            .map(({ pname, paid }) => ({
                                label: pname,
                                value: paid,
                            }));
                        setproduct(formofproductData);
                        setHSNDATA(response.data);
                    })

            } catch (error) {
                console.error("Error fetching Access Right Data:", error);
            }

        };

        const fetchCustomerData = async () => {
            try {
                const payload = {
                    "ctaid": "%",
                    "companyid": userdetail?.companyID || "",
                    "deptid": userdetail?.departmentID || "",
                    "ctype": '2'
                }
                const headers = {
                    "Content-Type": "application/json",
                    Accept: "*/*",
                };

                axios({
                    method: "POST",
                    url: baseUrl.Url + "/backend/api/GET_CategoryCustomer",
                    data: JSON.stringify(payload),
                    headers: headers,
                })
                    .then((response) => {
                        if (response.status != 200) throw new Error("Failed to Fetching Data");
                        const DATA = response.data;
                        const formofcustomerData = DATA
                            .map(({ ccompanyname, caid }) => ({
                                label: ccompanyname,
                                value: caid,
                            }));
                        setcustomer(formofcustomerData);
                        setData(DATA);
                    })

            } catch (error) {
                console.error("Error fetching Access Right Data:", error);
            }

        };

        const fetchTransportorData = async () => {
            try {
                const payload = {
                    "ctaid": "%",
                    "companyid": userdetail?.companyID || "",
                    "deptid": userdetail?.departmentID || "",
                    "ctype": '0'
                }
                const headers = {
                    "Content-Type": "application/json",
                    Accept: "*/*",
                };

                axios({
                    method: "POST",
                    url: baseUrl.Url + "/backend/api/GET_CategoryCustomer",
                    data: JSON.stringify(payload),
                    headers: headers,
                })
                    .then((response) => {
                        if (response.status != 200) throw new Error("Failed to Fetching Data");
                        const DATA = response.data;
                        const formoftransporterData = DATA
                            .map(({ ccompanyname, caid }) => ({
                                label: ccompanyname,
                                value: caid,
                            }));
                        settransporter(formoftransporterData);
                        setTdata(DATA);
                    })

            } catch (error) {
                console.error("Error fetching Access Right Data:", error);
            }

        };


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


        fetchstates();
        fetchCustomerData();
        fetchTransportorData();
        fetchProductData();
        // fetchImplications();

    }, []);


    useEffect(() => {
        if (!SQAMAID) return;
        try {
            const payload1 = {
                "sqamaid": SQAMAID,
                "keyword": "%",
                "satatusid": '1',
                "companyid": userdetail?.companyID || "",
                "deptid": userdetail?.departmentID || "",
            }
            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };

            axios({
                method: "POST",
                url: baseUrl.Url + "/backend/api/GET_SQuatationMaster",
                data: JSON.stringify(payload1),
                headers: headers,
            })
                .then((response) => {
                    if (response.status != 200) throw new Error("Failed to Fetching Data");
                    let apiData = response.data[0];
                    setFormData(prev => ({
                        ...prev,
                        SQAMAID: apiData.sqamaid,
                        SQCAID: customer.find((cust) => cust.value == apiData.sqcaid)?.value || "",
                        SPRNO: apiData.sprno,
                        SQMNAMT: apiData.sqmnamt,
                        SQGAMT: apiData.sqgamt,
                        SQNO: apiData.sqno,
                        SQDATE: convertToISODate(apiData.sqdate),
                        SQDUEDATE: convertToISODate(apiData.sqduedate),
                        SQTRANSPORTTERMS: apiData.sqtransportterms,
                        SQTRANSPORTID: transporter.find((trans) => trans.value == apiData.sqtransportid)?.value || "",
                        sqpaymentterms: "",
                        isapproved: true,
                        sqpduedate: "",
                        sqnodays: 0,
                        companyid: userdetail?.companyID || "",
                        deptid: userdetail?.departmentID || "",
                        srreff: "",


                    }));


                    console.log("quatation master data", apiData)

                    const selectedProduct = Data.find(p => p.caid == apiData.sqcaid);
                    if (selectedProduct) {
                        const matchedProductState = states.find(
                            s => s.label.toLowerCase().trim() === selectedProduct.sstatename?.toLowerCase().trim()
                        );
                        setFormData(prevData => ({
                            ...prevData,
                            SQCADDRESS: matchedProductState?.value || "",
                            SQCEMAIL: selectedProduct.cemail,
                            SQCCONTACT: selectedProduct.ccontactpersonmobile,
                        }));
                    }

                    const selectedtransporter = Tdata.find(p => p.caid == apiData.sqtransportid);
                    if (selectedtransporter) {
                        const matchedState = states.find(
                            s => s.label.toLowerCase().trim() === selectedtransporter.sstatename?.toLowerCase().trim()
                        );

                        console.log("Transporter state name from backend:", selectedtransporter.sstatename);
                        console.log("Matched state object from states[]:", matchedState);

                        setFormData(prevData => ({
                            ...prevData,
                            STADDRESS: matchedState?.value || "",  // ✅ use value like "MH"
                            SQTCONTACT: selectedtransporter.ccontactpersonmobile,
                        }));
                    }
                })

        } catch (error) {
            console.error("Error fetching Access Right Data:", error);
        }

        try {
            const payload = {
                sqamaid: SQAMAID,
                companyid: userdetail?.companyID || "",
                deptid: userdetail?.departmentID || "",
            }
            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };

            axios({
                method: "POST",
                url: baseUrl.Url + "/backend/api/GET_SQuotationDetailById",
                data: JSON.stringify(payload),
                headers: headers,
            })
                .then((response) => {
                    console.log(response.data);

                    if (response.status != 200) throw new Error("Failed to Fetching Details Data");
                    console.log(response.data);
                    if (response.data.length > 0) {
                        const mappedProducts = response.data.map((item) => ({
                            SQAMAID: item.sqamaid,
                            sqamdaid: item.sqamdaid || "",
                            SQPRODUCT: item.sqproduct,
                            SQUOM: item.squom,
                            PRODUCTNAME: item.productname,
                            UOMTITLE: item.uomtitle,
                            HSNCODE: item.hsncode,
                            SQQUANTITY: parseFloat(item.sqquantity) || 0,
                            SQRATE: parseFloat(item.sqrate) || 0,
                            SQTAXBALEAMOUNT: parseFloat(item.sqquantity) * parseFloat(item.sqrate), // Corrected calculation
                            SQCGST: parseFloat(item.sqcgst) || 0,
                            SQSGST: parseFloat(item.sqsgst) || 0,
                            SQIGST: parseFloat(item.sqigst) || 0,
                            SQTAMT: (parseFloat(item.sqquantity) * parseFloat(item.sqrate)) + parseFloat(item.sqcgst) + parseFloat(item.sqsgst) + parseFloat(item.sqigst), // Corrected calculation
                            IsDeleted: item.isdeleted,
                        }));


                        console.log("++++++++++1", response.data)
                        console.log("++++++++++2", mappedProducts)
                        // setFormData(mappedProducts);
                        setRows(mappedProducts)
                    }
                })

        } catch (error) {
            console.error("Error fetching Access Right Data:", error);
        }

    }, [SQAMAID]);

    const handleAddProduct = (e) => {
        e.preventDefault();

        // Validate each field and add error alert for empty fields or invalid patterns
        if (!productData.SQPRODUCT) {
            SQPRODUCTRef.current.focus();
            Swal.fire({
                icon: "error",
                title: "Product Validation Error ",
                text: "Please select Product",
            })
            // .then(() => {
            //     SQPRODUCTRef.current.focus();
            // });
            return;
        }

        if (!productData.SQUOM) {
            SQUOMRef.current.focus();
            Swal.fire({
                icon: "error",
                title: "UOM Validation Error ",
                text: "Please select UOM",
            })
            // .then(() => {
            //     SQUOMRef.current.focus();
            // });
            return;
        }

        if (
            !productData.SQQUANTITY ||
            productData.SQQUANTITY === 0 ||
            productData.SQQUANTITY === '0'
        ) {
            SQQUANTITYRef.current.focus();
            Swal.fire({
                icon: "error",
                title: "Quantity Validation Error",
                text: "Please enter a valid Quantity (greater than 0)",
            });
            return;
        }


        if (!productData.SQRATE) {
            SQRATERef.current.focus();
            Swal.fire({
                icon: "error",
                title: "Rate Validation Error",
                text: "Please enter the Rate",
            })
            // .then(() => {
            //     SQRATERef.current.focus();
            // });
            return;
        }

        // if (!productData.SQCGST) {
        //     SQCGSTRef.current.focus();
        //     Swal.fire({
        //         icon: "error",
        //         title: "CGST Validation Error",
        //         text: "Please enter the CGST",
        //     })
        //     // .then(() => {
        //     //     SQCGSTRef.current.focus();
        //     // });
        //     return;
        // }

        // if (!productData.SQSGST) {
        //     SQSGSTRef.current.focus();
        //     Swal.fire({
        //         icon: "error",
        //         title: "SGST Validation Error",
        //         text: "Please enter the SGST",
        //     })
        //     // .then(() => {
        //     //     SQSGSTRef.current.focus();
        //     // });
        //     return;
        // }

        // if (!productData.SQIGST) {
        //     SQIGSTRef.current.focus();
        //     Swal.fire({
        //         icon: "error",
        //         title: "IGST Validation Error",
        //         text: "Please enter the IGST",
        //     })
        //     // .then(() => {
        //     //     SQIGSTRef.current.focus();
        //     // });
        //     return;
        // }



        // if (!productData.SQPRODUCT || !productData.SQUOM || !productData.SQQUANTITY) {
        //     MySwal.fire({
        //         title: "Error!",
        //         text: "Please fill in all the product fields before adding.",
        //         icon: "error",
        //     });
        //     return;
        // }

        const newProduct = {
            ...productData,
            PRODUCTNAME: product.find((item) => item.value === productData.SQPRODUCT)?.label || "",
            UOMTITLE: uom.find((item) => item.value === productData.SQUOM)?.label || "",
            SQTAXBALEAMOUNT: productData.SQQUANTITY * productData.SQRATE,
            SQTAMT: (productData.SQQUANTITY * productData.SQRATE) + productData.SQCGST + productData.SQSGST + productData.SQIGST,
        };

        setRows((prevRows) => {
            const existingIndex = prevRows.findIndex((row) => row.sqamdaid === productData.sqamdaid);

            if (existingIndex !== -1) {
                const updatedRows = [...prevRows];
                updatedRows[existingIndex] = newProduct;
                return updatedRows;
            } else {
                // Add new row
                return [...prevRows, { ...newProduct, sqamdaid: ACSPLGUID.getNew() }];
            }
        });

        setProductData({
            SQPRODUCT: '',
            HSNCODE: 0,
            SQUOM: '',
            SQQUANTITY: '0',
            SQRATE: 0,
            SQTAMT: 0,
            SQCGST: 0,
            SQSGST: 0,
            SQIGST: 0,
            SQTAXBALEAMOUNT: 0,
            IsDeleted: 0,
        });
    };

    const handleProductChange = (e) => {
        const { name, value } = e.target;

        setProductData((prevData) => {
            const updatedData = { ...prevData, [name]: parseFloat(value) || 0 };


            if (name === "SQQUANTITY" || name === "SQRATE") {
                updatedData.SQTAXBALEAMOUNT = (updatedData.SQQUANTITY || 0) * (updatedData.SQRATE || 0);
                updatedData.SQTAMT = updatedData.SQTAXBALEAMOUNT + (prevData.SQCGST || 0) + (prevData.SQSGST || 0) + (prevData.SQIGST || 0);

                if (userdetail.ccompanyname != formData.SQCADDRESS) {
                    updatedData.SQIGST = Math.round(updatedData.SQTAXBALEAMOUNT * 0.18);
                    updatedData.SQSGST = 0;
                    updatedData.SQCGST = 0;
                }
                else {
                    updatedData.SQIGST = 0;
                    updatedData.SQSGST = Math.round(updatedData.SQTAXBALEAMOUNT * 0.9);
                    updatedData.SQCGST = Math.round(updatedData.SQTAXBALEAMOUNT * 0.9);
                }
            }

            // Recalculate SCDTAMT when GST values change
            if (name === "SQCGST" || name === "SQSGST" || name === "SQIGST") {
                updatedData.SQTAMT = (prevData.SQTAXBALEAMOUNT || 0) + (updatedData.SQCGST || 0) + (updatedData.SQSGST || 0) + (updatedData.SQIGST || 0);
            }


            return updatedData;
        });
    };


    useEffect(() => {
        const handleShortcut = (e) => {
            if (e.ctrlKey && e.key === "e") {
                e.preventDefault();
                showExitAlert();
            }

            if (e.ctrlKey && (e.key === "s" || e.key === "S")) {
                e.preventDefault();
                checkFormValidity(e);
            }
        };

        window.addEventListener("keydown", handleShortcut);
        return () => {
            window.removeEventListener("keydown", handleShortcut);
        };
    }, [formData, rows, navigate]);


    useEffect(() => {
        if (rows && rows.length > 0) {
            setFormData({
                ...formData,
                SQGAMT: rows.reduce((acc, row) => acc + parseFloat(row.SQTAXBALEAMOUNT || 0), 0).toFixed(2),
                SQMNAMT: rows.reduce((acc, row) => acc + parseFloat(row.SQTAMT || 0), 0).toFixed(2),
            });
        }
    }, [rows]);


    // C+S validation
    const checkFormValidity = (e) => {


        // Check for each field and show validation errors
        if (!formData.SQDATE) {
            SQDATERef.current.focus();
            Swal.fire({
                icon: "error",
                title: "Quotation Date Validation Error",
                text: "Please Select Quotation Date",
            })
            // .then(() => {
            //     SQDATERef.current.focus();
            // });
            return;
        }

        if (!formData.SQDUEDATE) {
            SQDUEDATERef.current.focus();
            Swal.fire({
                icon: "error",
                title: "Quotation Validity Date Validation Error",
                text: "Please Select Quotation Validity Date",
            })
            // .then(() => {
            //     SQDUEDATERef.current.focus();
            // });
            return;
        }

        if (!formData.SQCAID) {
            SQCAIDRef.current.focus();
            Swal.fire({
                icon: "error",
                title: "Customer Name Validation Error",
                text: "Please Select Customer Name",
            })
            // .then(() => {
            //     SQCAIDRef.current.focus();
            // });
            return;
        }

        if (!formData.SQTRANSPORTID) {
            SQTRANSPORTIDRef.current.focus();
            Swal.fire({
                icon: "error",
                title: "Transporter Name Validation Error",
                text: "Please Select Transporter Name",
            })
            // .then(() => {
            //     SQTRANSPORTIDRef.current.focus();
            // });
            return;
        }

        if (!formData.SQTRANSPORTTERMS) {
            SQTRANSPORTTERMSRef.current.focus();
            Swal.fire({
                icon: "error",
                title: "Transporter Terms Validation Error",
                text: "Please enter Transporter Terms",
            })
            // .then(() => {
            //     SQTRANSPORTTERMSRef.current.focus();
            // });
            return;
        }

        if (rows.length === 0) {
            Swal.fire({
                icon: "error",
                title: "Validation Error",
                text: "Please add at least one product before saving.",
            });
            return;
        }
        handleSubmit(e);
    };



    const handleEdit = (sqamdaid) => {
        console.log("editttttt");


        const filteredProducts = rows.filter((row) => row.sqamdaid === sqamdaid);
        if (filteredProducts.length === 0) return; // Safety check

        const selectedRow = filteredProducts[0];
        const matchedProduct = product.find((data) => data.value == selectedRow.SQPRODUCT);
        // const matchedUOM = uom.find((data) => data.value == selectedRow.SQUOM);
        onproductchange({ value: matchedProduct?.value });

        // Wait 200ms before setting productData (to ensure UOM is set)
        setTimeout(() => {
            setProductData({
                sqamdaid: selectedRow.sqamdaid,
                SQPRODUCT: matchedProduct ? matchedProduct.value : "",
                SQUOM: selectedRow.SQUOM, // Just use the value directly
                PRODUCTNAME: selectedRow.PRODUCTNAME,
                UOMTITLE: selectedRow.UOMTITLE,
                HSNCODE: selectedRow.HSNCODE,
                SQQUANTITY: selectedRow.SQQUANTITY,
                SQRATE: selectedRow.SQRATE,
                SQTAXBALEAMOUNT: selectedRow.SQTAXBALEAMOUNT,
                SQCGST: selectedRow.SQCGST,
                SQSGST: selectedRow.SQSGST,
                SQIGST: selectedRow.SQIGST,
                SQTAMT: selectedRow.SQTAMT,
                IsDeleted: selectedRow.IsDeleted
            });
        }, 100);



    };


    const handleDelete = (sqamdaid) => {
        Swal.fire({
            title: "Are you sure?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#d33",
            cancelButtonColor: "#3085d6",
            confirmButtonText: "Yes, delete it!",
            cancelButtonText: "Cancel",
        }).then((result) => {
            if (result.isConfirmed) {
                setRows((prevData) =>
                    prevData.map((row) =>
                        row.sqamdaid === sqamdaid ? { ...row, IsDeleted: 1 } : row
                    )
                );
                Swal.fire(
                    'Deleted!',
                    'Your item has been deleted.',
                    'success'
                );
            }
        }).catch((error) => {
            console.error("Error during deletion:", error);
            Swal.fire(
                'Error!',
                'There was an error deleting the item.',
                'error'
            );
        });
    };

    const handleKeyDown = (e, nextInputRef) => {
        if (e.key === 'Enter') {
            e.preventDefault();
            if (nextInputRef && nextInputRef.current) {
                nextInputRef.current.focus();
            }
        }
    };



    // const OnCustomerChange = (selectedOption) => {
    //     const selectedProduct = Data.find(p => p.caid == selectedOption.value);
    //     console.log("Selected Product:", selectedProduct);
    //     if (selectedProduct) {
    //         setFormData(prevData => ({
    //             ...prevData,
    //             SQCADDRESS: selectedProduct.sstatename,
    //             SQCEMAIL: selectedProduct.cemail,
    //             SQCCONTACT: selectedProduct.ccontactpersonmobile,
    //         }));
    //     }

    // }
    const OnCustomerChange = (selectedOption) => {
        const selectedCustomer = Data.find(cust => cust.caid == selectedOption?.value);

        const matchedState = states.find(
            s => s.label.toLowerCase().trim() === selectedCustomer?.sstatename?.toLowerCase().trim()
        );

        setFormData(prev => ({
            ...prev,
            SQCAID: selectedOption?.value || "",
            SQCCONTACT: selectedCustomer?.ccontactpersonmobile || "",
            SQCEMAIL: selectedCustomer?.cemail || "",
            SQCADDRESS: matchedState?.value || "",   // ✅ updates correct state field
        }));
    };


    // const OnTranspoterChange = (selectedOption) => {
    //     const selectedProduct = Tdata.find(p => p.caid == selectedOption.value);
    //     console.log("Selected Product:", selectedProduct);
    //     if (selectedProduct) {
    //         setFormData(prevData => ({
    //             ...prevData,
    //             STADDRESS: selectedProduct.sstatename,
    //             SQTCONTACT: selectedProduct.ccontactpersonmobile,
    //         }));
    //     }

    // }
    const OnTranspoterChange = (selectedOption) => {
        const selectedTransporter = Tdata.find(trans => trans.caid == selectedOption?.value);

        const matchedState = states.find(
            s => s.label.toLowerCase().trim() === selectedTransporter?.sstatename?.toLowerCase().trim()
        );

        setFormData(prev => ({
            ...prev,
            SQTRANSPORTID: selectedOption?.value || "",
            SQTCONTACT: selectedTransporter?.ccontactpersonmobile || "",
            STADDRESS: matchedState?.value || "",   // ✅ updates correct state field
        }));
    };






    return (
        <div>
            <div className="modal fade" id="AddSaleQuotation">
                <div className="modal-dialog modal-dialog-centered modal-fullscreen mbgcolor">
                    <div className="modal-content mbgcolor">
                        <div className="page-wrapper-new p-0 mbgcolor">
                            <div className="content mbgcolor">
                                <div className="modal-header border-0 custom-modal-header">
                                    <div className="page-title">
                                        <h4>Add Sale Quotation </h4>
                                    </div>
                                    <div className="page-btn">
                                        <button
                                            className="btn btn-secondary"
                                            aria-label="Close"
                                            onClick={showExitAlert}
                                        >
                                            <ArrowLeft className="me-2" />
                                            Back to Page
                                        </button>
                                    </div>
                                </div>


                                <div className="modal-body custom-modal-body">

                                    <form onSubmit={handleSubmit}>

                                        <div className="row mb-0 ">
                                            <div className="col-lg-1 col-md-2 col-12  ">
                                                <div>
                                                    <label className="form-label required">Qua No:</label>
                                                    <input
                                                        name="SQNO"
                                                        type="text"
                                                        className="form-control"
                                                        value={formData.SQNO}
                                                        onChange={handleChange}
                                                        // onKeyDown={(e) => handleKeyDown(e, SPRNORef)}
                                                        ref={SQNORef}
                                                        autoFocus
                                                        readOnly
                                                        placeholder="Auto Generate The Quataion Number"
                                                    />
                                                </div>
                                            </div>

                                            <div className="col-lg-1 col-md-2 col-12">
                                                <div>
                                                    <label className="form-label required">Req No :</label>
                                                    <input
                                                        name="SPRNO"
                                                        className="form-control"
                                                        value={formData.SPRNO}
                                                        onChange={handleChange}
                                                        // onKeyDown={(e) => handleKeyDown(e, SQDATERef)}
                                                        ref={SPRNORef}
                                                        readOnly
                                                        placeholder="Auto Generate"
                                                    />
                                                </div>
                                            </div>

                                            <div className="col-lg-2 col-md-3 col-12">
                                                <div>
                                                    <label className="form-label required">Quotation Date:</label>
                                                    <input
                                                        type="date"
                                                        name="SQDATE"
                                                        className="form-control"
                                                        value={formData.SQDATE}
                                                        onChange={handleChange}
                                                        onKeyDown={(e) => handleKeyDown(e, SQDUEDATERef)}
                                                        ref={SQDATERef}
                                                        required
                                                    />
                                                </div>
                                            </div>

                                            <div className="col-lg-2 col-md-3 col-12">
                                                <div>
                                                    <label className="form-label required">Quotation Validity Date:</label>
                                                    <input
                                                        type="date"
                                                        name="SQDUEDATE"
                                                        className="form-control"
                                                        value={formData.SQDUEDATE}
                                                        ref={SQDUEDATERef}
                                                        onChange={handleChange}
                                                        onKeyDown={(e) => handleKeyDown(e, SQCAIDRef)}
                                                        required
                                                    />
                                                </div>
                                            </div>

                                            <div className="col-lg-2 col-md-3 col-12">
                                                <div className="form-label">
                                                    <label className="required">Customer Name:</label>
                                                    <Select
                                                        classNamePrefix="react-select"
                                                        options={customer}
                                                        placeholder="Choose Customer"
                                                        value={customer.find((option) => option.value === formData.SQCAID) || null}
                                                        onChange={(selectedOption) => {
                                                            setFormData((prevData) => ({
                                                                ...prevData,
                                                                SQCAID: selectedOption ? selectedOption.value : '',
                                                            }));
                                                            OnCustomerChange(selectedOption);
                                                            // ✅ Move focus to next field *after* selection
                                                            if (SQTRANSPORTIDRef?.current) {
                                                                SQTRANSPORTIDRef.current.focus();
                                                            }
                                                        }}
                                                        ref={SQCAIDRef}
                                                        // onKeyDown={(e) => handleKeyDown(e, SQTRANSPORTIDRef)}
                                                        openMenuOnFocus={true}
                                                        required
                                                    />


                                                </div>
                                            </div>

                                            <div className="col-lg-2 col-md-3 col-12">
                                                <div className="form-label">
                                                    <label>Customer Contact:</label>
                                                    <input
                                                        type="number"
                                                        name="SQCCONTACT"
                                                        className="form-control"
                                                        value={formData.SQCCONTACT}
                                                        readOnly
                                                        onChange={handleChange}
                                                        // onKeyDown={(e) => handleKeyDown(e, SQCEMAILRef)}
                                                        ref={SQCCONTACTRef}
                                                        placeholder="Customer Contact Auto Set"
                                                    />
                                                </div>
                                            </div>

                                            <div className="col-lg-2 col-md-3 col-12">
                                                <div className="form-label">
                                                    <label>Customer Email:</label>
                                                    <input
                                                        type="email"
                                                        name="SQCEMAIL"
                                                        className="form-control"
                                                        value={formData.SQCEMAIL}
                                                        readOnly
                                                        onChange={handleChange}
                                                        // onKeyDown={(e) => handleKeyDown(e, SQCADDRESSRef)}
                                                        ref={SQCEMAILRef}
                                                        placeholder="Customer Email Auto Set"
                                                    />
                                                </div>
                                            </div>
                                        </div>

                                        <div className="row mb-0">
                                            <div className="col-lg-2 col-md-2 col-12">
                                                <div className="form-label">
                                                    <label>Customer State:</label>
                                                    <input
                                                        type="text"
                                                        className="form-control"
                                                        value={
                                                            states.find(option => option.value == formData.SQCADDRESS)?.label || ""
                                                        }
                                                        ref={SQCADDRESSRef}
                                                        // onKeyDown={(e) => handleKeyDown(e, SQTRANSPORTIDRef)}
                                                        readOnly
                                                        placeholder="Customer State Auto Set"
                                                    />
                                                </div>
                                            </div>

                                            <div className="col-lg-2 col-md-2 col-12">
                                                <div className="form-label">
                                                    <label className="required">Transporter Name:</label>
                                                    <Select
                                                        classNamePrefix="react-select"
                                                        options={transporter}
                                                        placeholder="Choose"
                                                        value={transporter.find((option) => option.value === formData.SQTRANSPORTID) || null}
                                                        onChange={(selectedOption) => {
                                                            setFormData((prevData) => ({
                                                                ...prevData,
                                                                SQTRANSPORTID: selectedOption ? selectedOption.value : '',

                                                            }));
                                                            OnTranspoterChange(selectedOption);
                                                            // ✅ Move focus to next field *after* selection
                                                            if (SQTRANSPORTTERMSRef?.current) {
                                                                SQTRANSPORTTERMSRef.current.focus();
                                                            }
                                                        }}
                                                        // onChange={OnTranspoterChange}
                                                        // onKeyDown={(e) => handleKeyDown(e, SQTRANSPORTTERMSRef)}
                                                        ref={SQTRANSPORTIDRef}
                                                        required
                                                        openMenuOnFocus={true}
                                                    />

                                                </div>
                                            </div>

                                            <div className="col-lg-2 col-md-2 col-12">
                                                <div className="form-label">
                                                    <label>Transporter Contact:</label>
                                                    <input
                                                        type="number"
                                                        name="SQTCONTACT"
                                                        className="form-control"
                                                        value={formData.SQTCONTACT}
                                                        onChange={handleChange}
                                                        // onKeyDown={(e) => handleKeyDown(e, STADDRESSRef)}
                                                        ref={SQTCONTACTRef}
                                                        readOnly
                                                        placeholder="Transporter Contact Auto Set"
                                                    />
                                                </div>
                                            </div>

                                            <div className="col-lg-2 col-md-2 col-12">
                                                <div className="form-label">

                                                    <label>Transporter State:</label>
                                                    {/* <input
                                                        type="text"
                                                        className="form-control"
                                                        value={
                                                            states.find(option => option.value === formData.STADDRESS)?.label || ""
                                                        }
                                                        ref={SQCADDRESSRef}
                                                        onKeyDown={(e) => handleKeyDown(e, SQTRANSPORTIDRef)}
                                                        readOnly
                                                    /> */}
                                                    <input
                                                        type="text"
                                                        className="form-control"
                                                        value={states.find(option => option.value === formData.STADDRESS)?.label || ""}
                                                        ref={STADDRESSRef}   // ✅ Correct ref for Transporter state
                                                        // onKeyDown={(e) => handleKeyDown(e, SQTRANSPORTIDRef)}
                                                        placeholder="Transporter State Auto Set"
                                                        readOnly
                                                    />

                                                </div>
                                            </div>

                                            <div className="col-lg-4 col-md-2 col-12">
                                                <div className="form-label">
                                                    <label className="required">Transport Terms:</label>
                                                    <input
                                                        rows="3"
                                                        className="form-control"
                                                        placeholder="Enter Transport Terms"
                                                        name="SQTRANSPORTTERMS"
                                                        ref={SQTRANSPORTTERMSRef}
                                                        value={formData.SQTRANSPORTTERMS}
                                                        onChange={handleChange}
                                                        required
                                                    />
                                                </div>
                                            </div>
                                        </div>

                                        <div className="border p-3 rounded shadow-sm mb-0">

                                            <div className="row">
                                                <div className="col-lg-5 col-sm-6 col-12">
                                                    <div className="mb-0 add-product form-label">
                                                        <label className="required">Product / Service</label>
                                                        <Select
                                                            ref={SQPRODUCTRef}
                                                            classNamePrefix="react-select"
                                                            options={product}
                                                            value={product.find(option => option.value === productData.SQPRODUCT) || null}
                                                            onChange={(selectedOption) => {
                                                                setProductData((prevData) => ({
                                                                    ...prevData,
                                                                    SQPRODUCT: selectedOption ? selectedOption.value : '',
                                                                })); onproductchange(selectedOption);

                                                            }}
                                                            openMenuOnFocus={true}
                                                        />
                                                    </div>
                                                </div>
                                                <div className="col-lg-2 col-md-6 col-sm-12">
                                                    <div className="mb-0 add-product form-label">
                                                        <label>HSN Code</label>
                                                        <input
                                                            type="number"
                                                            className="form-control no-arrows"
                                                            name="HSNCODE"
                                                            value={productData.HSNCODE || ""}
                                                            placeholder="HSNCODE Auto Set"
                                                            readOnly
                                                        />
                                                    </div>
                                                </div>

                                                <div className="col-lg-2 col-sm-6 col-12">
                                                    <div className="mb-0 add-product form-label">
                                                        <label className="required">UOM</label>
                                                        <Select
                                                            ref={SQUOMRef}
                                                            classNamePrefix="react-select"
                                                            name="SQUOM"
                                                            options={uom}
                                                            value={uom.find(option => option.value === productData.SQUOM) || null}
                                                            onChange={(selectedOption) => handleDetailDropdownChange(selectedOption, "SQUOM")}
                                                            placeholder="Select Option"
                                                            openMenuOnFocus={true}
                                                        />
                                                    </div>
                                                </div>

                                                <div className="col-lg-1 col-md-6 col-sm-12">
                                                    <div className="mb-0 add-product form-label">
                                                        <label className="required">Quantity</label>
                                                        <input
                                                            ref={SQQUANTITYRef}
                                                            className="form-control no-arrows"
                                                            name="SQQUANTITY"
                                                            value={productData.SQQUANTITY}
                                                            onChange={handleProductChange}
                                                        />
                                                    </div>
                                                </div>
                                                <div className="col-lg-1 col-md-6 col-sm-12">
                                                    <div className="mb-0 add-product form-label">
                                                        <label>Stock</label>
                                                        <input
                                                            readOnly
                                                            className="form-control no-arrows"
                                                            name="STOCK"
                                                            value={productData.STOCK || ""}


                                                        />
                                                    </div>
                                                </div>

                                                <div className="col-lg-1 col-md-6 col-sm-12">
                                                    <div className="mb-0  add-product form-label">
                                                        <label className="required">Rate</label>
                                                        <input
                                                            ref={SQRATERef}
                                                            // type="number"
                                                            className="form-control no-arrows"
                                                            name="SQRATE"
                                                            value={productData.SQRATE}
                                                            onChange={handleProductChange}
                                                        />
                                                    </div>
                                                </div>



                                            </div>

                                            <div className="row mb-2">
                                                <div className="col-lg-2 col-md-6 col-sm-12">
                                                    <div className="mb-0  add-product form-label">
                                                        <label className="required">Amount</label>
                                                        <input
                                                            ref={SQTAMTRef}
                                                            // type="number"
                                                            className="form-control no-arrows"
                                                            name="SQTAXBALEAMOUNT"
                                                            value={productData.SQTAXBALEAMOUNT}
                                                            readOnly />
                                                    </div>
                                                </div>
                                                <div className="col-lg-2 col-md-6 col-sm-12">
                                                    <div>
                                                        <label className="form-label">CGST</label>
                                                        <input
                                                            ref={SQCGSTRef}
                                                            type="number"
                                                            className="form-control no-arrows"
                                                            name="SQCGST"
                                                            value={productData.SQCGST}
                                                            onChange={handleProductChange}
                                                            readOnly={userdetail.ccompanyname != formData.SQCADDRESS}
                                                        />
                                                    </div>
                                                </div>


                                                <div className="col-lg-2 col-md-6 col-sm-12">
                                                    <div>
                                                        <label className="form-label">SGST</label>
                                                        <input
                                                            ref={SQSGSTRef}
                                                            type="number"
                                                            className="form-control no-arrows"
                                                            name="SQSGST"
                                                            value={productData.SQSGST}
                                                            onChange={handleProductChange}
                                                            readOnly={userdetail.ccompanyname != formData.SQCADDRESS}
                                                        />
                                                    </div>
                                                </div>

                                                <div className="col-lg-2 col-md-6 col-sm-12">
                                                    <div>
                                                        <label className="form-label">IGST</label>
                                                        <input
                                                            ref={SQIGSTRef}
                                                            type="number"
                                                            className="form-control no-arrows"
                                                            name="SQIGST"
                                                            value={productData.SQIGST}
                                                            onChange={handleProductChange}
                                                            readOnly={userdetail.ccompanyname == formData.SQCADDRESS}
                                                        />
                                                    </div>
                                                </div>

                                                <div className="col-lg-2 col-md-6 col-sm-12">
                                                    <div>
                                                        <label className="form-label required">Total</label>
                                                        <input
                                                            type="number"
                                                            className="form-control no-arrows"
                                                            name="SQTAMT"
                                                            value={productData.SQTAMT}
                                                            readOnly
                                                        />
                                                    </div>
                                                </div>

                                                <div className="col-lg-2 col-sm-6 col-12 mt-4">
                                                    <div >
                                                        <button
                                                            type="button"
                                                            className="btn btn-primary"
                                                            onClick={handleAddProduct}
                                                        >
                                                            Add
                                                        </button>
                                                    </div>
                                                </div>



                                            </div>

                                            <div className="row">
                                                <div className="col-lg-12">
                                                    <div className="modal-body-table">
                                                        <div
                                                            className="table-responsive"
                                                            // ref={scrollableRef}
                                                            style={{
                                                                height: "calc(62vh - 120px)",
                                                                overflowY: "auto",
                                                                overflowX: "auto",
                                                            }}
                                                        >
                                                            <table className="table table-bordered">
                                                                <thead className="thead-dark">
                                                                    <tr>
                                                                        <th className="col-3" >Product</th>
                                                                        <th className="col-1" >HSN Code</th>
                                                                        <th className="col-1" >UOM</th>
                                                                        <th className="col-1" >Quantity</th>
                                                                        <th className="col-1" >Rate</th>
                                                                        <th className="col-1" >Taxable Value</th>
                                                                        <th className="col-1" >CGST</th>
                                                                        <th className="col-1" >SGST</th>
                                                                        <th className="col-1" >IGST</th>
                                                                        <th className="col-1">Total</th>
                                                                        <th className="col-1 text-center">Action</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    {rows.filter((row) => row.IsDeleted == 0).length > 0 ? (
                                                                        rows
                                                                            .filter((row) => row.IsDeleted == 0)
                                                                            .map((row, index) => (
                                                                                <tr key={row.SQAMAID || index}>
                                                                                    <td>{row.PRODUCTNAME}</td>
                                                                                    <td>{row.HSNCODE}</td>
                                                                                    <td>{row.UOMTITLE}</td>
                                                                                    <td>{row.SQQUANTITY}</td>
                                                                                    <td>{row.SQRATE}</td>
                                                                                    <td>{row.SQTAXBALEAMOUNT}</td>
                                                                                    <td>{row.SQCGST}</td>
                                                                                    <td>{row.SQSGST}</td>
                                                                                    <td>{row.SQIGST}</td>
                                                                                    <td>{row.SQTAMT}</td>
                                                                                    <td className=" text-center">
                                                                                        <Link
                                                                                            to="#"
                                                                                            onClick={() => handleEdit(row.sqamdaid)} // corrected here
                                                                                            style={{ color: 'lightblue' }}
                                                                                        >
                                                                                            <Edit className="feather-edit" />
                                                                                        </Link>

                                                                                        <Link
                                                                                            className=" text-center p-2"
                                                                                            to="#"
                                                                                            onClick={() => handleDelete(row.sqamdaid)}
                                                                                        // className="confirm-text"
                                                                                        >
                                                                                            <Trash2 className="feather-trash-2" />
                                                                                        </Link>
                                                                                    </td>
                                                                                </tr>
                                                                            ))
                                                                    ) : (
                                                                        <tr>
                                                                            <td colSpan="12">No data available</td>
                                                                        </tr>
                                                                    )}


                                                                </tbody>
                                                                <tfoot>
                                                                    <tr>
                                                                        <td colSpan="4"></td>
                                                                        <td><strong>Total Gross:</strong></td>
                                                                        <td>
                                                                            <span className="form-control form-control-sm border-0 border-bottom border-warning">
                                                                                {/* {formData.SQGAMT} */}
                                                                                {Math.round(formData.SQGAMT)}
                                                                            </span>
                                                                        </td>
                                                                        <td colSpan="2"></td>
                                                                        <td><strong>Total Net:</strong></td>
                                                                        <td>
                                                                            <span className="form-control form-control-sm border-0 border-bottom border-warning">
                                                                                {Math.round(formData.SQMNAMT)}
                                                                            </span>
                                                                        </td>
                                                                        <td></td>
                                                                    </tr>
                                                                </tfoot>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div className="btn-addproduct mb-0 mt-3">
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
            </div >
        </div >
    );
};

export default AddSaleQuotation;


