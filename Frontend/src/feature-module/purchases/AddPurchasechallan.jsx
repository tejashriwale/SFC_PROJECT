import { useNavigate } from 'react-router-dom';
import React, { useRef, useEffect, useState } from "react";
import { Link } from "react-router-dom";
import Select from "react-select";
import withReactContent from "sweetalert2-react-content";
import Swal from "sweetalert2";
import { ArrowLeft, Trash2, Edit } from "feather-icons-react/build/IconComponents";
import { ACSPLGUID, baseUrl, convertToISODate } from '../../core/json/custom';
import axios from "axios";
import { getUserData } from '../../Context/UserData';

function AddPurchasechallan({ pcaid, onRefresh }) {

    const { userdetail } = getUserData();
    const GUID = ACSPLGUID.getNew();
    const PCTRNNORef = useRef(null);
    const PCDATERef = useRef(null);
    const PCVNORef = useRef(null);
    const PCPOSUPPLYRef = useRef(null);
    const PCCONSIGNERRef = useRef(null);
    const PCNARRATIONRef = useRef(null);
    const PCSELLERRef = useRef(null);
    const PCDPRODUCTRef = useRef(null);
    const PCDUOMRef = useRef(null);
    const PCDQUANTITYRef = useRef(null);
    const PCDRATERef = useRef(null);
    const PCDTAXABLERef = useRef(null);
    const PCDCGSTRef = useRef(null);
    const PCDSGSTRef = useRef(null);
    const PCDIGSTRef = useRef(null);
    const [product, setproduct] = useState([]);
    const [paymentmode, setpaymentmode] = useState([]);
    const [HSNData, setHSNData] = useState([]);
    const [vendor, setvendor] = useState([]);
    const [uom, setuom] = useState([]);



    const isValidDate = (dateString) => {
        const regex = /^\d{4}-\d{2}-\d{2}$/;
        if (!dateString || !regex.test(dateString)) return false;

        const date = new Date(dateString);
        return date instanceof Date && !isNaN(date);
    };


    const [rows, setRows] = useState([]);
    const [states, setstates] = useState([]);
    const [formData, setFormData] = useState({
        PCTRNNO: '',
        PCDATE: '',
        PCVNO: '',
        PCPOSUPPLY: '',
        PCSELLER: '',
        PCCONSIGNER: '',
        PCNARRATION: '',
        PCNATE: '',
        PCGATE: '',
        POREFF: '',
        PMODE: '',
        BranchName: '',
        IFSCCode: '',
        AccountNumber: '',
        BankName: '',
        QVADDRESS: '',
        QVEMAIL: '',
        QVCONTACT: ''
    });
// console.log("formData.PCSELLER formData.PCSELLER",formData.PCSELLER)
    const [productData, setProductData] = useState({
        PCDPRODUCT: '',
        PRODUCTNAME: '',
        UOMTITLE: '',
        PCDUOM: '',
        PCDQUANTITY: 0,
        PCDRATE: 0,
        PCDTAXABLE: 0,
        PCDCGST: 0,
        PCDSGST: 0,
        PCDIGST: 0,
        PCDTAMT: 0,
        BATCHNO: '',
        EXPIRYDATE: '',
        ISDELETED: '',
        PCDAID: '',
        HSNCODE: '',
        PCAID: '',
    });


    useEffect(() => {
        if (pcaid) {
            const fetchMasterData = async () => {
                try {
                    const payload = {
                        pcaid: pcaid,
                        keyword: "%",
                        companyid: userdetail?.companyID || "",
                        deptid: userdetail?.departmentID || "",
                        statusid: "1",
                    };
                    const headers = {
                        "Content-Type": "application/json",
                        Accept: "*/*",
                    };
                    const response = await axios.post(
                        `${baseUrl.Url}/backend/api/GET_PChallanMaster`,
                        payload,
                        { headers }
                    );
                    if (response.status !== 200)
                        throw new Error("Failed to fetch vendor data");
                    console.log("challan Number information", response.data)

                    const Data = response.data[0];
                    setFormData({
                        PCTRNNO: Data.pctrnno,
                        PCDATE: convertToISODate(Data.pcdate),
                        PCVNO: Data.pcvno,
                        PCPOSUPPLY: Data.pcposupply,
                        // PCSELLER: vendor.find(item => item.value === Data.pcseller)?.value || '',
                        PCSELLER: Data.pcseller,
                        PCCONSIGNER: Data.pcconsigner,
                        PCNARRATION: Data.pcnarration,
                        PCNATE: Data.pcnamt,
                        PCGATE: Data.pcgamt,
                        PVENDORNAME: Data.vendorname,
                        PCVEMAIL: Data.vendoremail,
                        PCVCONTACT: Data.vendorcontact,
                        POREFF: Data.poreff,
                        // QVADDRESS: Data.sstatename,
                        PMODE: paymentmode.find(item => item.value === Data.paymentmode)?.value || ''
                    });
                    console.log("vendornameeeeeeeeeeee",formData.QVADDRESS)
                    console.log("vendornameeeeeeeeeeeeData.pcseller",Data.sstatename)
                } catch (error) {
                    console.error("Error fetching vendor data:", error);
                }
            };



            const fetchDetailData = async () => {
                try {
                    const payload = {
                        pcaid: pcaid,
                        companyid: userdetail?.companyID || "",
                        deptid: userdetail?.departmentID || "",
                    };
                    const headers = {
                        "Content-Type": "application/json",
                        Accept: "*/*",
                    };
                    const response = await axios.post(
                        `${baseUrl.Url}/backend/api/GET_PChallanDetailsById`,
                        payload,
                        { headers }
                    );
                    if (response.status !== 200)
                        throw new Error("Failed to fetch vendor data");
                    console.log("venderos", response.data)
                    if (response.data.length > 0) {
                        const mappedProducts = response.data.map((item) => ({
                            PRODUCTNAME: item.productname,
                            UOMTITLE: item.uomtitle,
                            PCDPRODUCT: item.pcdproduct,
                            PCDUOM: item.pcduom,
                            PCDQUANTITY: item.pcdquantity,
                            PCDRATE: item.pcdrate,
                            PCDTAXABLE: item.pcdtaxable,
                            PCDCGST: item.pcdcgst,
                            PCDSGST: item.pcdsgst,
                            PCDIGST: item.pcdigst,
                            total: item.pcdtaxable + item.pcdcgst + item.pcdsgst + item.pcdigst,
                            PCDAID: item.pcdaid,
                            ISDELETED: item.isdeleted,
                            BATCHNO: item.batchno,
                            EXPIRYDATE: item.expirydate,
                            PCDTAMT: item.pcdtamt,
                            PCDPRODUCT_LABEL: item.productname,
                            PCDUOM_LABEL: item.uomtitle,
                            PCAID: item.pcaid,
                            HSNCODE: item.hsncode
                        }));
                        const totalTaxable = mappedProducts.reduce((acc, row) => acc + parseFloat(row.PCDTAXABLE || 0), 0).toFixed(2);
                        const totalAmount = mappedProducts.reduce((acc, row) => acc + parseFloat(row.total || 0), 0).toFixed(2);

                        setFormData({
                            ...formData,
                            PCGATE: totalTaxable,
                            PCNATE: totalAmount,
                        });

                        setRows(mappedProducts);
                    }

                } catch (error) {
                    console.error("Error fetching vendor data:", error);
                }
            };
            fetchMasterData();
            fetchDetailData();
            
        }
    }, [pcaid]);


    useEffect(() => {
        if (formData.PCSELLER) {
            const fetchVendors = async () => {
                console.log("formData.PCSELLER",formData.PCSELLER)
                try {
                    const payload = {
                        pkid: formData.PCSELLER,
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
                    console.log("venderos", response.data)
                    if (response.data.length > 0) {
                        setFormData(prevState => ({
                            ...prevState,
                            // PCSELLER: vendor.find(item => item.value === response.data[0].pcseller)?.value || '',
                            // PCSELLER: response.data[0].pcseller,
                            QVAID: response.data[0].ccompanyname,
                            QVCONTACT: response.data[0].ccontactpersonmobile,
                            QVEMAIL: response.data[0].cemail,
                            QVADDRESS: response.data[0].cstate,
                            // QVADDRESS: states.find((state) => state.value === response.data[0].cstate)?.value || "",
                            BankName: response.data[0].cbankname,
                            AccountNumber: response.data[0].caccountnumber,
                            IFSCCode: response.data[0].cifsc,
                            BranchName: response.data[0].cbranchname
                        }));
                        console.log("vendor address ",response.data[0].cstate)
                        console.log("vendor Address labrl ",formData.QVADDRESS)
                    }
                } catch (error) {
                    console.error("Error fetching vendor data:", error);
                }
            };

            fetchVendors();
        }

    }, [formData.PCSELLER, userdetail]);




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
                        const DATA = response.data;
                        const formofproductData = DATA
                            .map(({ pname, paid }) => ({
                                label: pname,
                                value: paid,
                            }));
                        setproduct(formofproductData);
                        setHSNData(response.data);
                    })

            } catch (error) {
                console.error("Error fetching Access Right Data:", error);
            }

        };
        const fetchVendorData = async () => {
            try {
                const payload = {
                    "ctaid": "%",
                    "ctype": "3",
                    "companyid": userdetail?.companyID ? userdetail.companyID : "",
                    "deptid": userdetail?.departmentID ? userdetail.departmentID : ""
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


                        const formofvendorData = DATA
                            .map(({ ccompanyname, caid }) => ({
                                label: ccompanyname,
                                value: caid,
                            }));

                        setvendor(formofvendorData);
                    })

            } catch (error) {
                console.error("Error fetching Access Right Data:", error);
            }
        };


        const fetchImplicationsPayamentData = async () => {
            try {
                const response = await axios.get(
                    baseUrl.Url + "/backend/api/Implications/PAYMENT",

                );

                if (response.status !== 200) throw new Error("Failed to fetch implications data");

                const data = response.data;


                const implicationsDropdown = data.map(({ iTitle, iValue }) => ({
                    label: iTitle,
                    value: iValue,
                }));

                setpaymentmode(implicationsDropdown);
            } catch (error) {
                console.error("Error fetching implications:", error);
            }
        };
        fetchImplicationsPayamentData()

        fetchVendorData();
        fetchProductData();
        fetchstates();
    }, []);


    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData((prevData) => ({
            ...prevData,
            [name]: value.trimStart(),
        }));
    };



    const handleProductChange = (e) => {
        const { name, value } = e.target;
        let numericValue = parseFloat(value) || 0;

        setProductData((prevData) => {
            const updatedData = { ...prevData, [name]: numericValue };

            if (name === "PCDQUANTITY" || name === "PCDRATE") {
                const quantity = parseFloat(updatedData.PCDQUANTITY) || 0;
                const rate = parseFloat(updatedData.PCDRATE) || 0;
                updatedData.PCDTAXABLE = parseFloat((quantity * rate).toFixed(2));
            }

            const taxable = parseFloat(updatedData.PCDTAXABLE) || 0;

            if (formData.VENDORSTATE === userdetail?.companystate) {
                updatedData.PCDIGST = parseFloat((Math.round(taxable * 0.18)));
                updatedData.PCDCGST = 0;
                updatedData.PCDSGST = 0;
            } else {
                // const halfGST = 1 / 2;
                updatedData.PCDIGST = 0;
                updatedData.PCDCGST = parseFloat((Math.round((taxable * 9)/100)));
                updatedData.PCDSGST = parseFloat((Math.round((taxable * 9)/100)));
            }

            const cgst = parseFloat(updatedData.PCDCGST) || 0;
            const sgst = parseFloat(updatedData.PCDSGST) || 0;
            const igst = parseFloat(updatedData.PCDIGST) || 0;

            updatedData.PCDTAMT = parseFloat((taxable + cgst + sgst + igst).toFixed(2));

            console.log("updatedDataupdatedDataupdatedDataupdatedData",updatedData)
            return updatedData;
            
        });
    };




    //  const updatedRows = [...rows];
    //         if (field === 'PCDQUANTITY' || field === 'PCDRATE') {
    //         const quantity = parseFloat(updatedRows[index].PCDQUANTITY || 0) || 0;
    //         const rate = parseFloat(updatedRows[index].PCDRATE || 0) || 0;
    //         const taxableValue = quantity * rate;

    //         updatedRows[index].PCDTAXABLE = Math.round(taxableValue);


    //         if (formData.QVADDRESS !== userdetail?.companystate) {
    //             updatedRows[index].PCDIGST = parseFloat((Math.round(taxableValue * 0.18)));
    //             updatedRows[index].PCDCGST = 0;
    //             updatedRows[index].PCDSGST = 0;
    //         } else {
    //             // const halfGST = 1 / 2;
    //             updatedRows[index].PCDIGST = 0;
    //             updatedRows[index].PCDCGST = parseFloat((Math.round((taxableValue * 9)/100)));
    //             updatedRows[index].PCDSGST = parseFloat((Math.round((taxableValue * 9)/100)));
    //         }


    //            updatedRows[index].total = taxableValue + updatedRows[index].PCDIGST + updatedRows[index].PCDCGST + updatedRows[index].PCDSGST;
        
    //         setRows(updatedRows);
    //                 }
                
    //                 setRows(updatedRows);
    //         };


    const handleProductChange1 = (e) => {
        const { name, value } = e.target;

        setProductData((prevData) => {
            const updatedData = { ...prevData, [name]: value };
            return updatedData;
        });
    };



    const MySwal = withReactContent(Swal);
    const handleAddProduct = async () => {

        try {
            const payload = {
                "pcaid": (pcaid == undefined || pcaid == "") ? "" : pcaid,
                "companyid": userdetail?.companyID || "",
                "deptid": userdetail?.departmentID || ""
            };

            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };

            const response = await axios.post(
                `${baseUrl.Url}/backend/api/SP_MeassagePChallan`,
                JSON.stringify(payload),
                { headers }
            );

            if (response.status !== 200) {
                throw new Error("Failed to Fetch Data");
            }

            const responseMessage = response.data?.[0]?.responseMessage;
            if (responseMessage === "Edit is not allowed because the next transaction has been successfully completed.") {
                await Swal.fire({
                    icon: 'warning',
                    title: 'Edit Not Allowed',
                    text: responseMessage,
                    confirmButtonText: 'OK'
                });
                return;
            }

            if (
                !productData.PCDPRODUCT ||
                !productData.PCDUOM ||
                !productData.PCDQUANTITY
            ) {
                MySwal.fire({
                    title: 'Error!',
                    text: 'Please fill in all the product fields before adding.',
                    icon: 'error',
                });
                return;
            }

            let updatedTableData;
            if (productData.PCDAID) {
                updatedTableData = rows.map((item) =>
                    item.PCDAID === productData.PCDAID ? {
                        ...item, ...productData, PCDPRODUCT_LABEL: product.find(item => item.value === productData.PCDPRODUCT)?.label || '',
                        PCDUOM_LABEL: uom.find(item => item.value === productData.PCDUOM)?.label || '',
                    } : item
                );
            } else {
                updatedTableData = [...rows, {
                    ...productData, PCDAID: ACSPLGUID.getNew(), PCDPRODUCT_LABEL: product.find(item => item.value === productData.PCDPRODUCT)?.label || '',
                    PCDUOM_LABEL: uom.find(item => item.value === productData.PCDUOM)?.label || '',
                }];
            }

            setRows(updatedTableData);

            const totalTaxable = rows.reduce((acc, row) => acc + parseFloat(row.PCDTAXABLE || 0), 0).toFixed(2);
            const totalAmount = rows.reduce((acc, row) => acc + parseFloat(row.total || 0), 0).toFixed(2);

            setFormData({
                ...formData,
                PCGATE: totalTaxable,
                PCNATE: totalAmount,
            });

            setProductData({
                PCDPRODUCT: '',
                PRODUCTNAME: '',
                UOMTITLE: '',
                PCDUOM: '',
                PCDQUANTITY: 0,
                PCDRATE: 0,
                PCDTAXABLE: 0,
                PCDCGST: 0,
                PCDSGST: 0,
                PCDIGST: 0,
                PCDTAMT: 0,
                BATCHNO: '',
                EXPIRYDATE: '',
                ISDELETED: '',
                PCDAID: '',
                HSNCODE: '',
                PCAID: '',
            });

        } catch (error) {
            console.error("Submission Error:", error);
            await Swal.fire({
                icon: "error",
                title: "Error",
                text: "Failed to save data. Please try again.",
            });
        }
    };


    const handleSubmit = (e) => {
        e.preventDefault();
        const form = e.target.closest("form");
        if (!form.checkValidity()) {
            form.reportValidity();
            return;
        }
        showConfirmationAlert();
    };


    const handleKeyDown = (e, nextRef) => {
        if (e.key === 'Enter' && nextRef.current) {
            nextRef.current.focus();
            e.preventDefault();
        }
    };


    const showConfirmationAlert = async () => {
        try {
            const payload = {
                "pcaid": (pcaid == undefined || pcaid == "") ? "" : pcaid,
                "companyid": userdetail?.companyID || "",
                "deptid": userdetail?.departmentID || ""
            };

            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };

            const response = await axios.post(
                `${baseUrl.Url}/backend/api/SP_MeassagePChallan`,
                JSON.stringify(payload),
                { headers }
            );

            if (response.status !== 200) {
                throw new Error("Failed to Fetch Data");
            }

            const responseMessage = response.data?.[0]?.responseMessage;
            if (responseMessage === "Edit is not allowed because the next transaction has been successfully completed.") {
                await Swal.fire({
                    icon: 'warning',
                    title: 'Edit Not Allowed',
                    text: responseMessage,
                    confirmButtonText: 'OK'
                });
                return;
            }
            MySwal.fire({
                title: 'Are you sure?',
                text: 'Do you want to save this data?',
                showCancelButton: true,
                confirmButtonColor: '#00ff00',
                confirmButtonText: 'SAVE',
                cancelButtonColor: '#092C4C',
                cancelButtonText: 'CANCEL',
            }).then((result) => {
                if (result.isConfirmed) {
                    handleFormSubmission();
                    handleModalConfirm();
                }
            });

        } catch (error) {
            console.error("Submission Error:", error);
            await Swal.fire({
                icon: "error",
                title: "Error",
                text: "Failed to save data. Please try again.",
            });
        }
    };

    const handleModalConfirm = () => {
        console.log('Form Data:', formData);
        console.log('Rows Data:', rows);

    };

    const navigate = useNavigate();


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

                if (onRefresh) {
                    onRefresh(); // refreshReceiptData from Bills.jsx
                }

                const modal = document.getElementById("AddPurchasechallan");

                if (modal) {
                    modal.classList.remove("show");
                    modal.style.display = "none";
                    modal.setAttribute("aria-hidden", "true");


                    document.querySelectorAll(".modal-backdrop").forEach((backdrop) => {
                        backdrop.remove();
                    });


                    document.body.classList.remove("modal-open");
                    document.body.style.overflow = "";
                    document.body.style.paddingRight = "";
                }


                setFormData({
                    PCTRNNO: '',
                    PCDATE: '',
                    PCVNO: '',
                    PCPOSUPPLY: '',
                    PCSELLER: '',
                    PCCONSIGNER: '',
                    PCNARRATION: '',
                    PCNATE: '',
                    PCGATE: '',
                    POREFF: '',
                    PMODE: '',
                });


                setProductData({
                    PCDPRODUCT: '',
                    PRODUCTNAME: '',
                    UOMTITLE: '',
                    PCDUOM: '',
                    PCDQUANTITY: 0,
                    PCDRATE: 0,
                    PCDTAXABLE: 0,
                    PCDCGST: 0,
                    PCDSGST: 0,
                    PCDIGST: 0,
                    PCDTAMT: 0,
                    BATCHNO: '',
                    EXPIRYDATE: '',
                    ISDELETED: '',
                    PCDAID: '',
                    HSNCODE: '',
                    PCAID: '',
                });
                setRows([]);
            }
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
            const filteredRows = rows.filter(row => row.ISDELETED !== 1);

            setFormData((prevFormData) => ({
                ...prevFormData,
                PCGATE: filteredRows
                    .reduce((acc, row) => acc + parseFloat(row.PCDTAXABLE || 0), 0)
                    .toFixed(2),
                PCNATE: filteredRows
                    .reduce((acc, row) => acc + parseFloat(row.PCDTAMT || 0), 0)
                    .toFixed(2),
            }));
        }
    }, [rows]);


    const checkFormValidity = (e) => {
        const {
            PCTRNNO,
            PCDATE,
            PCVNO,
            PCPOSUPPLY,
            PCSELLER,
            PCCONSIGNER,
            PCNARRATION,
        } = formData;


        if (!PCTRNNO) {
            Swal.fire({
                icon: "error",
                title: "Validation Error",
                text: "Challan No is required",
            }).then(() => {
                PCTRNNORef.current.focus();
            });
            return;
        }

        if (!PCDATE || !isValidDate(PCDATE)) {
            Swal.fire({
                icon: "error",
                title: "Validation Error",
                text: "Challan Date is required and must be a valid date.",
            }).then(() => {
                PCDATERef.current.focus();
            });
            return;
        }

        if (!PCVNO) {
            Swal.fire({
                icon: "error",
                title: "Validation Error",
                text: "Vehicle No is required",
            }).then(() => {
                PCVNORef.current.focus();
            });
            return;
        }

        if (!PCPOSUPPLY) {
            Swal.fire({
                icon: "error",
                title: "Validation Error",
                text: "Place of Supply is required",
            }).then(() => {
                PCPOSUPPLYRef.current.focus();
            });
            return;
        }

        if (!PCSELLER) {
            Swal.fire({
                icon: "error",
                title: "Validation Error",
                text: "Supplier is required",
            }).then(() => {
                PCSELLERRef.current.focus();
            });
            return;
        }


        if (!PCCONSIGNER) {
            Swal.fire({
                icon: "error",
                title: "Validation Error",
                text: "Consigner is required",
            }).then(() => {
                PCCONSIGNERRef.current.focus();
            });
            return;
        }

        if (!PCNARRATION) {
            Swal.fire({
                icon: "error",
                title: "Validation Error",
                text: "Narration is required",
            }).then(() => {
                PCNARRATIONRef.current.focus();
            });
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



    const handleFormSubmission = async () => {

        try {
            const payload = {
                "caid": formData.PCSELLER,
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
            const payload1 = {
                "pcaid": pcaid ? pcaid : GUID,
                "pctrnno": formData.PCTRNNO,
                "pono": "",
                "pcseller": formData.PCSELLER,
                "pcconsigner": formData.PCCONSIGNER,
                "pcvno": formData.PCVNO,
                "pcposupply": formData.PCPOSUPPLY,
                "pcnarration": formData.PCNARRATION,
                "pcdate": formData.PCDATE,
                "pcnamt": formData.PCNATE,
                "pcgamt": formData.PCGATE,
                "companyid": userdetail?.companyID || "",
                "deptid": userdetail?.departmentID || "",
                "poreff": formData.POREFF ? formData.POREFF : "",
                "statusid": "1",
                "paymentmode": formData.PMODE,
            };

            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };

            const response1 = await axios.post(baseUrl.Url + "/backend/api/AddUpdPChallanMaster", payload1, { headers });

            if (response1.status === 200) {

                const payload2 = rows.map((row) => ({
                    "pcdaid": row.PCDAID ? row.PCDAID : ACSPLGUID.getNew(),
                    "pcaid": pcaid ? pcaid : GUID,
                    "pcdproduct": row.PCDPRODUCT,
                    "pcduom": row.PCDUOM,
                    "pcdquantity": row.PCDQUANTITY.toString(),
                    "pcdrate": parseFloat(row.PCDRATE),
                    "pcdtamt": parseFloat(row.PCDTAMT),
                    "pcdtaxable": row.PCDTAXABLE.toString(),
                    "pcdigst": parseFloat(row.PCDIGST),
                    "pcdsgst": parseFloat(row.PCDSGST),
                    "pcdcgst": parseFloat(row.PCDCGST),
                    "hsncode": parseFloat(row.HSNCODE),
                    "companyid": userdetail?.companyID || "",
                    "deptid": userdetail?.departmentID || "",
                    "isdeleted": row.ISDELETED == 1 || row.ISDELETED == true ? true : false,
                    "batchno": row.BATCHNO,
                    "expirydate": row.EXPIRYDATE
                }));

                const response2 = await axios.post(baseUrl.Url + "/backend/api/AddUpdPChallanDetails", payload2, { headers });
                if (response2.status === 200) {
                    Swal.fire({
                        icon: "success",
                        title: "Saved!",
                        text: "Data saved successfully.",
                        confirmButtonText: "OK",
                    })
                    // .then((result) => {
                    //     if (result.isConfirmed) {
                    //         const modal = document.getElementById("AddPurchasechallan");
                    //         if (modal) {
                    //             modal.classList.remove("show");
                    //             modal.style.display = "none";
                    //             document.body.classList.remove("modal-open");
                    //             document.body.style.overflow = "auto";

                    //             const backdrop = document.querySelector(".modal-backdrop");
                    //             if (backdrop) {
                    //                 backdrop.remove();
                    //             }

                    //             setFormData({
                    //                 PCTRNNO: '',
                    //                 PCDATE: '',
                    //                 PCVNO: '',
                    //                 PCPOSUPPLY: '',
                    //                 PCSELLER: '',
                    //                 PCCONSIGNER: '',
                    //                 PCNARRATION: '',
                    //                 PCNATE: '',
                    //                 PCGATE: '',
                    //                 POREFF: '',
                    //                 PMODE: '',
                    //             });
                    //             setProductData({
                    //                 PCDPRODUCT: '',
                    //                 PRODUCTNAME: '',
                    //                 UOMTITLE: '',
                    //                 PCDUOM: '',
                    //                 PCDQUANTITY: 0,
                    //                 PCDRATE: 0,
                    //                 PCDTAXABLE: 0,
                    //                 PCDCGST: 0,
                    //                 PCDSGST: 0,
                    //                 PCDIGST: 0,
                    //                 PCDTAMT: 0,
                    //                 BATCHNO: '',
                    //                 EXPIRYDATE: '',
                    //                 ISDELETED: '',
                    //                 PCDAID: '',
                    //                 HSNCODE: '',
                    //                 PCAID: '',
                    //             });
                    //             setRows([]);
                    //         }
                    //         const backdrop = document.querySelector(".modal-backdrop");
                    //         if (backdrop) {
                    //             backdrop.remove();
                    //         }
                    //     }
                    // });
                } else {
                    throw new Error("Failed to save product details.");
                }
            } else {
                throw new Error("Failed to save master data.");
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



    const handleDelete = async (PCDAID, index) => {
        MySwal.fire({
            title: "Are you sure?",
            text: "You won't be able to revert this!",
            showCancelButton: true,
            confirmButtonColor: "#00ff00",
            confirmButtonText: "Yes, delete it!",
            cancelButtonColor: "#ff0000",
            cancelButtonText: "Cancel",
        }).then((result) => {
            if (result.isConfirmed) {
                setRows((prevData) => {
                    const updatedRows = prevData.map((data, idx) =>
                        (PCDAID && data.PCDAID === PCDAID) || (PCDAID === "" && idx === index)
                            ? { ...data, ISDELETED: 1 }
                            : data
                    );

                    const filteredRows = updatedRows.filter(row => row.ISDELETED == 0 || row.ISDELETED == false);

                    const totalTaxable = filteredRows
                        .reduce((acc, row) => acc + parseFloat(row.PCDTAXABLE || 0), 0)
                        .toFixed(2);

                    const totalAmount = filteredRows
                        .reduce((acc, row) => acc + parseFloat(row.total || 0), 0)
                        .toFixed(2);

                    setFormData((prevFormData) => ({
                        ...prevFormData,
                        PCGATE: totalTaxable,
                        PCNATE: totalAmount,
                    }));

                    return updatedRows;
                });

                Swal.fire({
                    icon: "success",
                    title: "Deleted!",
                    text: "Record marked as deleted",
                    confirmButtonText: "OK",
                });
            }

        });
    };


    // const fetchProductData = async () => {
    //         try {
    //             const payload = {
    //                 "ctaid": "%",
    //                 "companyid": userdetail?.companyID ? userdetail.companyID : "",
    //                 "deptid": userdetail?.departmentID ? userdetail.departmentID : ""
    //             }
    //             const headers = {
    //                 "Content-Type": "application/json",
    //                 Accept: "*/*",
    //             };

    //             axios({
    //                 method: "POST",
    //                 url: baseUrl.Url + "/backend/api/GET_CategoryProduct",
    //                 data: JSON.stringify(payload),
    //                 headers: headers,
    //             })
    //                 .then((response) => {
    //                     if (response.status != 200) throw new Error("Failed to Fetching Data");
    //                     const DATA = response.data;
    //                     const formofproductData = DATA
    //                         .map(({ pname, paid }) => ({
    //                             label: pname,
    //                             value: paid,
    //                         }));
    //                     setproduct(formofproductData);
    //                     setHSNData("hsn",response.data);
    //                 })

    //         } catch (error) {
    //             console.error("Error fetching Access Right Data:", error);
    //         }

    //     };


    const handleEdit = (PCDAID, index) => {
       
        let item;

        if (PCDAID) {
            item = rows.find((data) => data.PCDAID == PCDAID);
        } else if (index !== null && index >= 0 && index < rows.length) {
            item = rows[index];
        }

        if (!item) return;
        OnProductChange({ value: item.PCDPRODUCT });
        // fetchProductData();
        setProductData({
            PCDPRODUCT: product.find(p => p.value === item.PCDPRODUCT)?.value || '',
            // PRODUCTNAME: item.PRODUCTNAME,
            UOMTITLE: item.UOMTITLE,
            PCDUOM: uom.find(u => u.value === item.PCDUOM)?.value || '',
            PCDQUANTITY: item.PCDQUANTITY,
            PCDRATE: item.PCDRATE,
            PCDTAXABLE: item.PCDTAXABLE,
            PCDCGST: item.PCDCGST,
            PCDSGST: item.PCDSGST,
            PCDIGST: item.PCDIGST,
            PCDTAMT: (parseFloat(item.PCDTAXABLE) || 0) +
                (parseFloat(item.PCDCGST) || 0) +
                (parseFloat(item.PCDSGST) || 0) +
                (parseFloat(item.PCDIGST) || 0),
            BATCHNO: item.BATCHNO,
            EXPIRYDATE: convertToISODate(item.EXPIRYDATE),
            ISDELETED: item.ISDELETED,
            PCDAID: item.PCDAID,
            HSNCODE: item.HSNCODE,
            PCAID: item.HSNCODE,
        });
        // OnProductChange(productData);
        console.log("productData productData productData",item.HSNCODE)
         console.log("productData productData productData",formData.HSNCODE)
    };



    const OnProductChange = (selectedOption) => {
        const selectedProduct = HSNData.find(p => p.paid === selectedOption.value);
        if (selectedProduct) {
            setProductData(prevData => ({
                ...prevData,
                HSNCODE: selectedProduct.hsncode,
            }));
        
            try {
                const payload = {
                    "psale": selectedProduct.psales,

                }
                const headers = {
                    "Content-Type": "application/json",
                    Accept: "*/*",
                };

                axios({
                    method: "POST",
                    url: baseUrl.Url + "/backend/api/GET_UOMImplication",
                    data: JSON.stringify(payload),
                    headers: headers,
                })
                    .then((response) => {
                        if (response.status != 200) throw new Error("Failed to Fetching Data");
                        const data = response.data;
                        const implicationsDropdown = data.map(({ iTitle, iValue }) => ({
                            label: iTitle,
                            value: iValue,
                        }));

                        setuom(implicationsDropdown);
                    })

            } catch (error) {
                console.error("Error fetching Access Right Data:", error);
            }
        }
    }


    return (
        <div
            className="modal fade"
            id="AddPurchasechallan"
            tabIndex={-1}
            aria-labelledby="exampleModalFullscreenLabel"
            aria-hidden="true"
        >
            <div className="modal-dialog modal-fullscreen">
                <div className="modal-content mbgcolor">

                    <div className="modal-body mbgcolor">
                        <div className="content mbgcolor">
                            <div className="modal-header border-0 custom-modal-header mbgcolor">
                                <div className="page-title">
                                    <h4>Add New Purchase challan</h4>
                                </div>
                                <div className="page-btn">
                                    <Link className="btn btn-secondary"
                                        aria-label="Close"
                                        onClick={showExitAlert}
                                    >
                                        <ArrowLeft className="me-2" />
                                        Back to index
                                    </Link>
                                </div>
                            </div>
                            <div className="modal-body custom-modal-body">
                                <form onSubmit={handleSubmit}>

                                    <div className="row">
                                        <div className="col-lg-3 col-md-6 col-sm-12">
                                            <div className="mb-3 add-product form-label">
                                                <label className="form-label text-dark required">Challan No</label>
                                                <input
                                                    ref={PCTRNNORef}
                                                    type="text"
                                                    className="form-control border"
                                                    name="PCTRNNO"
                                                    value={formData.PCTRNNO}
                                                    onChange={handleChange}
                                                    required
                                                />
                                            </div>
                                        </div>
                                        <div className="col-lg-3 col-sm-6 col-12">
                                            <div className="input-blocks">
                                                <label className="form-label text-dark required">Purchase Date</label>
                                                <div className="input-groupicon calender-input">
                                                    <div className="info-img" />
                                                    <input
                                                        ref={PCDATERef}
                                                        type="date"
                                                        className="form-control"
                                                        value={formData.PCDATE}
                                                        placeholder="Choose Date"
                                                        onChange={(e) =>
                                                            setFormData({
                                                                ...formData,
                                                                PCDATE: e.target.value,
                                                            })
                                                        }
                                                        required
                                                        onKeyDown={(e) => handleKeyDown(e, PCVNORef)}
                                                    />
                                                </div>
                                            </div>
                                        </div>
                                        <div className="col-lg-3 col-md-4 col-sm-12">
                                            <div className="mb-3 add-product form-label">
                                                <label className="form-label text-dark required">Vehicle No</label>
                                                <input
                                                    ref={PCVNORef}
                                                    type="text"
                                                    className="form-control border"
                                                    name="PCVNO"
                                                    value={formData.PCVNO}
                                                    onChange={handleChange}
                                                    required
                                                />
                                            </div>
                                        </div>
                                        <div className="col-lg-3 col-md-4 col-sm-12">
                                            <div className="mb-3 add-product form-label">
                                                <label className="form-label text-dark required">Place of supply</label>
                                                <Select
                                                    ref={PCPOSUPPLYRef}
                                                    classNamePrefix="react-select"
                                                    options={states}
                                                    placeholder="Choose"
                                                    value={states.find((option) => option.value === formData.PCPOSUPPLY) || null}
                                                    onChange={(selectedOption) => {
                                                        setFormData((prevData) => ({
                                                            ...prevData,
                                                            PCPOSUPPLY: selectedOption ? selectedOption.value : '',
                                                        }));
                                                    }}
                                                    required
                                                    openMenuOnFocus={true}
                                                />
                                            </div>
                                        </div>
                                    </div>


                                    <div className="row">
                                        <div className="col-lg-5 col-md-4 col-sm-12">
                                        </div>

                                        <div className="row">
                                            <div className='col-lg-4 col-sm-12'>
                                                <label className="form-label text-dark required">Payment Mode</label>
                                                <Select
                                                    classNamePrefix="react-select"
                                                    options={paymentmode}
                                                    placeholder="Choose"
                                                    value={paymentmode.find((option) => option.value === formData.PMODE) || null}
                                                    onChange={(selectedOption) => {
                                                        setFormData((prevData) => ({
                                                            ...prevData,
                                                            PMODE: selectedOption ? selectedOption.value : '',
                                                        }));
                                                    }}
                                                    openMenuOnFocus={true}
                                                />
                                            </div>


                                            <div className='col-lg-8 col-sm-12'>
                                                <label className="form-label text-dark">Consigner</label>
                                                <input
                                                    ref={PCCONSIGNERRef}
                                                    type="text"
                                                    className="form-control"
                                                    name="PCCONSIGNER"
                                                    onChange={handleChange}
                                                    value={formData.PCCONSIGNER}
                                                    required
                                                />
                                            </div>
                                        </div>
                                    </div>


                                    <div className="row mb-3 mt-3">
                                        <div className="addproduct-icon">
                                            <h5>
                                                <span>Vendor Details :</span>
                                            </h5>
                                        </div>
                                        <div className="col-lg-3 col-md-3 col-12">
                                            <div className="form-label">
                                                <label className='form-label required'>Vendor Name:</label>
                                                <Select
                                                    ref={PCSELLERRef}
                                                    classNamePrefix="react-select"
                                                    options={vendor}
                                                    placeholder="Choose"
                                                    value={vendor.find((option) => option.value === formData.PCSELLER)}
                                                    onChange={(selectedOption) => {
                                                        setFormData((prevData) => ({
                                                            ...prevData,
                                                            PCSELLER: selectedOption ? selectedOption.value : '',
                                                        }));
                                                    }}
                                                    required
                                                    openMenuOnFocus={true}
                                                />
                                            </div>
                                        </div>

                                        <div className="col-lg-3 col-md-3 col-12">
                                            <div className="form-label">
                                                <label>Vendor Contact:</label>
                                                <input
                                                    type="number"
                                                    name="QVCONTACT"
                                                    className="form-control"
                                                    value={formData.QVCONTACT}
                                                    onChange={handleChange}

                                                />
                                            </div>
                                        </div>

                                        <div className="col-lg-3 col-md-3 col-12">
                                            <div className="form-label">
                                                <label>Vendor Email:</label>
                                                <input
                                                    type="email"
                                                    name="QVEMAIL"
                                                    className="form-control"
                                                    value={formData.QVEMAIL}
                                                    onChange={handleChange}
                                                />
                                            </div>
                                        </div>


                                        <div className="col-lg-3 col-md-3 col-12">
                                            <div className="form-label">
                                                <label>Vendor State:</label>
                                                <Select
                                                    classNamePrefix="react-select"
                                                    options={states}
                                                    openMenuOnFocus={true}
                                                    value={states.find((option) => option.value === formData.QVADDRESS)}
                                                    onChange={(selectedOption) => {
                                                        setFormData((prevData) => ({
                                                            ...prevData,
                                                            QVADDRESS: selectedOption ? selectedOption.value : '',
                                                        }));
                                                    }}
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
                                                                onChange={handleChange}
                                                            />
                                                        </div>
                                                        <div className="col-lg-3 col-sm-6 col-12 mb-3">
                                                            <label className="form-label">Account Number:</label>
                                                            <input
                                                                type="text"
                                                                className="form-control"
                                                                name="AccountNumber"
                                                                value={formData.AccountNumber}
                                                                onChange={handleChange}
                                                            />
                                                        </div>

                                                        <div className="col-lg-3 col-sm-6 col-12 mb-3">
                                                            <label className="form-label">IFSC Code:</label>
                                                            <input
                                                                type="text"
                                                                className="form-control"
                                                                name="IFSCCode"
                                                                value={formData.IFSCCode}
                                                                onChange={handleChange}
                                                            />
                                                        </div>

                                                        <div className="col-lg-3 col-sm-6 col-12 mb-3">
                                                            <label className="form-label">Branch Name:</label>
                                                            <input
                                                                type="text"
                                                                className="form-control"
                                                                name="BranchName"
                                                                value={formData.BranchName}
                                                                onChange={handleChange}
                                                            />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                    <div className="row">
                                        <div className="col-12">
                                            <div className="form-label add-product form-label">
                                                <label className="form-label text-dark required">Narration</label>
                                                <textarea
                                                    ref={PCNARRATIONRef}
                                                    rows={2}
                                                    cols={2}
                                                    className="form-control border text-secondary"
                                                    name="PCNARRATION"
                                                    onChange={handleChange}
                                                    value={formData.PCNARRATION}
                                                    required
                                                    onKeyDown={(e) => handleKeyDown(e, PCDPRODUCTRef)}

                                                />
                                            </div>
                                        </div>
                                    </div>


                                    <div className="border p-3 rounded shadow-sm mb-4 mt-5">
                                        <div className="row">
                                            <div className="col-lg-12">
                                                <div className="modal-body-table">
                                                    <div className="table-responsive">
                                                        <table className="table datanew">
                                                            <table className="table table-bordered">
                                                                <thead className="thead-dark">
                                                                    <tr>
                                                                        <th className="col-3 text-center">Product</th>
                                                                        <th className="col-1 text-center">UOM</th>
                                                                        <th className="col-1 text-center">HSN Code</th>
                                                                        <th className="col-1 text-center">Quantity</th>
                                                                        <th className="col-1 text-center">Rate</th>
                                                                        <th className="col-1 text-center">Taxable Value</th>
                                                                        <th className="col-1 text-center">CGST</th>
                                                                        <th className="col-1 text-center">SGST</th>
                                                                        <th className="col-1 text-center">IGST</th>
                                                                        <th className="col-1 text-center">Total</th>
                                                                        <th className="col-1 text-center">Action</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    {rows && rows
                                                                        .filter((row) => row.ISDELETED == 0) // Filter out deleted rows
                                                                        .map((row, index) => (
                                                                            <tr key={index}>
                                                                                <td>{row.PCDPRODUCT_LABEL}</td> {/* Displaying label */}
                                                                                <td>{row.PCDUOM_LABEL}</td> {/* Displaying label */}
                                                                                <td className='text-end'>{row.HSNCODE}</td>
                                                                                <td className='text-end'>{row.PCDQUANTITY}</td>
                                                                                <td className='text-end'>{row.PCDRATE}</td>
                                                                                <td className='text-end'>{row.PCDTAXABLE}</td>
                                                                                <td className='text-end'>{row.PCDCGST}</td>
                                                                                <td className='text-end'>{row.PCDSGST}</td>
                                                                                <td className='text-end'>{row.PCDIGST}</td>
                                                                                <td className='text-end'>{row.PCDTAMT}</td>
                                                                                <td className="col-1 text-center">
                                                                                    <div className="edit-delete-action">
                                                                                        <Link
                                                                                            to="#"
                                                                                            onClick={() => handleEdit(row.PCDAID, index)}
                                                                                            className="me-2 p-1"
                                                                                            style={{ color: 'lightblue' }}
                                                                                        >
                                                                                            <Edit className="feather-edit" />
                                                                                        </Link>

                                                                                        {/* Delete Button */}
                                                                                        <Link
                                                                                            className="confirm-text p-1 me-2"
                                                                                            to="#"
                                                                                            style={{ color: 'red' }}
                                                                                            onClick={() => handleDelete(row.PCDAID, index)}
                                                                                        >
                                                                                            <Trash2 className="feather-trash-2" />
                                                                                        </Link>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                        ))}
                                                                </tbody>

                                                                <tfoot>

                                                                    <td colSpan="4"></td>
                                                                    <td><strong>Total Taxable:</strong></td>
                                                                    <td>
                                                                        <input
                                                                            type="text"
                                                                            className="form-control border-0 text-end "
                                                                            name="PCGATE"
                                                                            value={formData.PCGATE || '0'}
                                                                            readOnly
                                                                        />
                                                                    </td>
                                                                    <td colSpan="2"></td>
                                                                    <td className='text-end'><strong>Total:</strong></td>
                                                                    <td>
                                                                        <input
                                                                            type="text"
                                                                            className="form-control border-0 text-end"
                                                                            name="PCNATE"
                                                                            value={formData.PCNATE || '0'}
                                                                            readOnly
                                                                        />
                                                                    </td>
                                                                    <td></td>

                                                                </tfoot>
                                                            </table>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>


                                        <div className="row mt-5">
                                            <div className="col-lg-7 col-sm-12 col-12">
                                                <div className="mb-3 add-product form-label">
                                                    <label className="form-label text-dark required">Product / Service</label>
                                                    <Select
                                                        ref={PCDPRODUCTRef}
                                                        classNamePrefix="react-select"
                                                        options={product}
                                                        value={product.find((option) => option.value === productData.PCDPRODUCT) || null}
                                                        onChange={(selectedOption) => {
                                                            setProductData((prevData) => ({
                                                                ...prevData,
                                                                PCDPRODUCT: selectedOption ? selectedOption.value : '',
                                                            }));
                                                            OnProductChange(selectedOption);
                                                        }}
                                                        openMenuOnFocus={true}
                                                    />
                                                </div>
                                            </div>
                                            <div className="col-lg-2 col-sm-6 col-12">
                                                <div className="mb-2 add-product form-label">
                                                    <label className="form-label text-dark required">HSN code</label>
                                                    <input
                                                        type="number"
                                                        className="form-control"
                                                        name="HSNCODE"
                                                        value={productData.HSNCODE}
                                                        onChange={handleProductChange}
                                                        readOnly

                                                    />
                                                </div>
                                            </div>
                                            <div className="col-lg-3 col-sm-6 col-12">
                                                <div className="mb-2 add-product form-label">
                                                    <label className="form-label text-dark required">Unit of Measurement</label>
                                                    <Select
                                                        ref={PCDUOMRef}
                                                        classNamePrefix="react-select"
                                                        options={uom}
                                                        value={uom.find((option) => option.value === productData.PCDUOM) || null}
                                                        onChange={(selectedOption) => {
                                                            setProductData((prevData) => ({
                                                                ...prevData,
                                                                PCDUOM: selectedOption ? selectedOption.value : '',
                                                            }));
                                                        }}
                                                        openMenuOnFocus={true}
                                                    />
                                                </div>
                                            </div>
                                        </div>


                                        <div className="row">
                                            <div className="col-lg-3 col-md-6 col-sm-12">
                                                <div className="mb-3 add-product form-label">
                                                    <label className="form-label text-dark required">Quantity</label>
                                                    <input
                                                        ref={PCDQUANTITYRef}
                                                        type="number"
                                                        className="form-control"
                                                        name="PCDQUANTITY"
                                                        value={productData.PCDQUANTITY}
                                                        onChange={handleProductChange}
                                                        onKeyDown={(e) => handleKeyDown(e, PCDRATERef)}
                                                    />
                                                </div>
                                            </div>

                                            <div className="col-lg-3 col-md-6 col-sm-12">
                                                <div className="mb-3 add-product form-label">
                                                    <label className="form-label text-dark">Rate</label>
                                                    <input
                                                        ref={PCDRATERef}
                                                        type="number"
                                                        className="form-control"
                                                        name="PCDRATE"
                                                        value={productData.PCDRATE}
                                                        onChange={handleProductChange}
                                                        onKeyDown={(e) => handleKeyDown(e, PCDTAXABLERef)}
                                                    />
                                                </div>
                                            </div>

                                            <div className="col-lg-3 col-md-6 col-sm-12">
                                                <div className="mb-3 add-product form-label">
                                                    <label className="form-label text-dark">Taxable Value</label>
                                                    <input
                                                        ref={PCDTAXABLERef}
                                                        type="number"
                                                        className="form-control"
                                                        name="PCDTAXABLE"
                                                        value={productData.PCDTAXABLE}
                                                        readOnly

                                                        onKeyDown={(e) => handleKeyDown(e, PCDCGSTRef)}
                                                    />
                                                </div>
                                            </div>
                                        </div>


                                        <div className="row">
                                            <div className="col-lg-2 col-md-6 col-sm-12">
                                                <div className="mb-3 add-product form-label">
                                                    <label className="form-label text-dark">CGST</label>
                                                    <input
                                                        ref={PCDCGSTRef}
                                                        type="number"
                                                        className="form-control"
                                                        name="PCDCGST"
                                                        value={productData.PCDCGST || 0}
                                                        onChange={handleProductChange}
                                                        onKeyDown={(e) => handleKeyDown(e, PCDSGSTRef)}
                                                    />
                                                </div>
                                            </div>
                                            <div className="col-lg-2 col-md-6 col-sm-12">
                                                <div className="mb-3 add-product form-label">
                                                    <label className="form-label text-dark">SGST</label>
                                                    <input
                                                        ref={PCDSGSTRef}
                                                        type="number"
                                                        className="form-control"
                                                        name="PCDSGST"
                                                        value={productData.PCDSGST || 0}
                                                        onChange={handleProductChange}
                                                        onKeyDown={(e) => handleKeyDown(e, PCDIGSTRef)}
                                                    />
                                                </div>
                                            </div>
                                            <div className="col-lg-2 col-md-6 col-sm-12">
                                                <div className="mb-3 add-product form-label">
                                                    <label className="form-label text-dark">IGST</label>
                                                    <input
                                                        ref={PCDIGSTRef}
                                                        type="number"
                                                        className="form-control"
                                                        name="PCDIGST"
                                                        value={productData.PCDIGST || 0}
                                                        onChange={handleProductChange}
                                                    />
                                                </div>
                                            </div>
                                            <div className="col-lg-2 col-md-6 col-sm-12">
                                                <div className="mb-3 add-product form-label">
                                                    <label className="form-label text-dark">Total</label>
                                                    <input
                                                        type="number"
                                                        className="form-control"
                                                        name="PCDTAMT"
                                                        value={productData.PCDTAMT}
                                                        readOnly

                                                    />
                                                </div>
                                            </div>
                                            <div className="col-lg-2 col-md-6 col-sm-12">
                                                <div className="mb-3 add-product form-label">
                                                    <label className="form-label text-dark">Batch No</label>
                                                    <input
                                                        type="text"
                                                        className="form-control"
                                                        name="BATCHNO"
                                                        value={productData.BATCHNO}
                                                        onChange={handleProductChange1}


                                                    />
                                                </div>
                                            </div>
                                            <div className="col-lg-2 col-md-6 col-sm-12">
                                                <div className="mb-3 add-product form-label">
                                                    <label className="form-label text-dark">Expiry Date</label>
                                                    <input
                                                        type="date"
                                                        className="form-control"
                                                        name="EXPIRYDATE"
                                                        value={productData.EXPIRYDATE}
                                                        onChange={handleProductChange1}

                                                    />
                                                </div>
                                            </div>
                                        </div>


                                        <div className="row">
                                            <div className="col-lg-12">
                                                <div className="modal-footer-btn">
                                                    <div className="col-lg-12 mt-3">
                                                        <button
                                                            type="button"
                                                            className="btn btn-primary"
                                                            onClick={handleAddProduct}
                                                        >
                                                            Add Product
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div className="col-lg-12">
                                        <div className="modal-footer-btn">
                                            <button
                                                type="button"
                                                className="btn btn-cancel me-2"

                                                onClick={showExitAlert}
                                            >
                                                Exit
                                            </button>
                                            <button
                                                type="submit"
                                                className="btn btn-submit"
                                            >
                                                Save
                                            </button>
                                        </div>
                                    </div>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default AddPurchasechallan



