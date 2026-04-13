import React, { useState, useEffect, useRef } from "react";
import {
    ArrowLeft,
} from "feather-icons-react/build/IconComponents";
import Select from "react-select";
import { useNavigate } from "react-router-dom";
import Swal from "sweetalert2";
import withReactContent from "sweetalert2-react-content";
import axios from 'axios';
import { getUserData } from "../../Context/UserData";
import { all_routes } from "../../Router/all_routes";
import { ACSPLGUID, baseUrl, convertToISODate } from "../../core/json/custom";
const ProceedRequisition = ({ SQAMAID, srno, onRefresh }) => {
    const { userdetail } = getUserData();
    console.log(SQAMAID, "sqamaid")
    const route = all_routes;
    const MySwal = withReactContent(Swal);
    const navigate = useNavigate();
    const [states, setstates] = useState([]);
    const [customer, setcustomer] = useState([]);
    const [transporter, settransporter] = useState([]);
    // const [product, setproduct] = useState([]);
    // const [HSNDATA, setHSNDATA] = useState([]);

    // const [srno, setsrno] = useState([]);
    // const [uom, setuom] = useState([]);
    // const [REQNNO, setREQNNO] = useState();

    const SQDATERef = useRef(null);
    const SQCEMAILRef = useRef(null);
    const SQCADDRESSRef = useRef(null);
    const SQTRANSPORTTERMSRef = useRef(null);
    const STADDRESSRef = useRef(null);
    const SQTRANSPORTIDRef = useRef(null);
    // const SQTRANSPORTID = useRef(null);

    // const [Data, setData] = useState([]);
    const [Tdata, setTdata] = useState([]);


    const SPRNORef = useRef(null);
    // const SQPRODUCTRef = useRef(null);
    // const SQUOMRef = useRef(null);
    // const SQQUANTITYRef = useRef(null);
    const SQRATERef = useRef(null);
    // const SQTAMTRef = useRef(null);
    // const SQCGSTRef = useRef(null);
    // const SQSGSTRef = useRef(null);
    // const SQIGSTRef = useRef(null);
    // const SREQNNOref = useRef(null);
    const srnoref = useRef(null);
    // const [showForm, setShowForm] = useState(false);

    const [rows, setRows] = useState([]);

    const [formData, setFormData] = useState({
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
        SQMNAMT: '',
        SQGAMT: '',
        srno: '',
        CUSTOMERNAME: '',

    });

    const handleSubmit = (e, event) => {
        e.preventDefault();

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
                setRows([]);
                setFormData({
                    SQNO: '',
                    SQDATE: '',
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
                    SQMNAMT: '',
                    SQGAMT: '',
                    srno: '',
                    CUSTOMERNAME: '',

                });


                if (result.isConfirmed) {
                    const modal = document.getElementById("ProceedRequisition");

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
                    if (onRefresh) {
                        onRefresh();
                    }
                }
            }
        });
    }


    const SQDUEDATERef = useRef(null);
    const SQTRANSPORTTERMRef = useRef(null);
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
                "sprno": formData.SPRNO || srno || "",
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
                "SATATUSID": '2'

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
                "sqamdaid": row.SQAMDAID ? row.SQAMDAID : ACSPLGUID.getNew(),
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
                "companyid": userdetail?.companyID || "",
                "deptid": userdetail?.departmentID || "",
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

                    setFormData({
                        SQNO: '',
                        SQDATE: '',
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
                        SQMNAMT: '',
                        SQGAMT: '',
                        srno: '',
                        CUSTOMERNAME: '',
                    });
                    setRows([]);

                    const modal = document.getElementById("ProceedRequisition");
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
                    if (onRefresh) {
                        onRefresh();
                    }
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



    const handleDropdownChange = (selectedOption, field) => {
        setFormData(prevData => ({
            ...prevData,
            [field]: selectedOption ? selectedOption.value : "",
        }));
    }

    useEffect(() => {

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
                        // setData(DATA);
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
                        console.log("formoftransporterData formoftransporterData", formoftransporterData)
                        console.log("DATA DATADATA DATA DATA", DATA)
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
        // fetchProductData();
        // fetchImplications();

    }, [srno]);

    useEffect(() => {
        if (!SQAMAID) return;
        // setShowForm(true);
        try {
            const payload1 = {
                // sqamaid: SQAMAID,
                // "companyid": userdetail?.companyID || "",
                // "deptid": userdetail?.departmentID || "",
                "sqamaid": SQAMAID,
                "keyword": "%",
                "companyid": userdetail?.companyID || "",
                "deptid": userdetail?.departmentID || "",
                "statusid": '2'
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
                        "companyid": userdetail?.companyID || "",
                        "deptid": userdetail?.departmentID || "",
                        srreff: "",
                    }));

                    console.log("quatation master data", apiData);

                    // const selectedProduct = Data.find(p => p.caid == apiData.sqcaid);
                    // if (selectedProduct) {
                    //     setFormData(prevData => ({
                    //         ...prevData,
                    //         SQCADDRESS: selectedProduct.sstatename,
                    //         SQCEMAIL: selectedProduct.cemail,
                    //         SQCCONTACT: selectedProduct.ccontactpersonmobile,
                    //     }));
                    // }

                    const selectedtransporter = Tdata.find(p => p.caid == apiData.sqtransportid);
                    if (selectedtransporter) {
                        setFormData(prevData => ({
                            ...prevData,
                            STADDRESS: selectedtransporter.sstatename,
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
                "companyid": userdetail?.companyID || "",
                "deptid": userdetail?.departmentID || "",
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


    // From Sales REQUISITION Proceed.  
    useEffect(() => {
        const fetchRequisitionData = async () => {
            if (srno) {
                try {
                    const payload = {
                        srno: srno,
                        companyid: userdetail?.companyID || "",
                        deptid: userdetail?.departmentID || "",
                    };
                    const headers = {
                        "Content-Type": "application/json",
                        Accept: "*/*",
                    };

                    const response = await axios.post(
                        `${baseUrl.Url}/backend/api/GET_SRequisitionNoSearch`,
                        payload,
                        { headers }
                    );

                    if (response.status !== 200)
                        throw new Error("Failed to fetch vendor data");

                    console.log("Reqnodata", response.data);

                    if (response.data.length > 0) {
                        const mappedProducts = response.data.map((item) => ({
                            PRODUCTNAME: item.productname,
                            UOMTITLE: item.uomtitle,
                            SQQUANTITY: item.srdquantity,
                            SQPRODUCT: item.srdproduct,
                            SQUOM: item.srduom,
                        }));

                        setRows(mappedProducts);

                        const totalTaxable = mappedProducts
                            .reduce((acc, row) => acc + parseFloat(row.SQTAXBALEAMOUNT || 0), 0)
                            .toFixed(2);
                        const totalAmount = mappedProducts
                            .reduce((acc, row) => acc + parseFloat(row.SQTAMT || 0), 0)
                            .toFixed(2);

                        setFormData(prevState => ({
                            ...prevState,
                            CUSTOMERNAME: response.data[0].vendorname,
                            SQCAID: response.data[0].srvandorid,
                            SQCCONTACT: response.data[0].contactnumber,
                            SQCEMAIL: response.data[0].email,
                            SQGAMT: totalTaxable,
                            SQMNAMT: totalAmount,
                            srno: response.data[0].srno,
                            SQCADDRESS: response.data[0].sstatename,
                        }));
                    }

                } catch (error) {
                    console.error("Error fetching vendor data:", error);
                }
            }
        };

        fetchRequisitionData();
    }, [srno]);


    // const handleInputChange = (e, index, field) => {
    //     const value = parseFloat(e.target.value) || 0;

    //     setRows((prevRows) => {
    //         const updatedRows = [...prevRows];
    //         const row = updatedRows[index];

    //         // Update the specific field in the row
    //         row[field] = value;

    //         // Recalculate taxable amount and total cost when quantity or rate changes
    //         if (field === "SQQUANTITY" || field === "SQRATE") {
    //             row.SQTAXBALEAMOUNT = (row.SQQUANTITY || 0) * (row.SQRATE || 0);
    //             row.SQTAMT = row.SQTAXBALEAMOUNT + (row.SQCGST || 0) + (row.SQSGST || 0) + (row.SQIGST || 0);

    //             if (userdetail.customer != formData.STADDRESS) {
    //                 row.SQCGST = 0;
    //                 row.SQSGST = 0;
    //                 row.SQIGST = parseFloat(((row.SQTAXBALEAMOUNT * 18) / 100).toFixed(2));
    //             } else {
    //                 row.SQCGST = parseFloat(((row.SQTAXBALEAMOUNT * 9) / 100).toFixed(2));
    //                 row.SQSGST = parseFloat(((row.SQTAXBALEAMOUNT * 9) / 100).toFixed(2));
    //                 row.SQIGST = 0;
    //             }
    //             return updatedRows;

    //         }

    //         // Recalculate total amount when GST values change
    //         if (field === "SQCGST" || field === "SQSGST" || field === "SQIGST") {
    //             row.SQTAMT = (row.SQTAXBALEAMOUNT || 0) + (row.SQCGST || 0) + (row.SQSGST || 0) + (row.SQIGST || 0);
    //         }

    //         // Return the updated rows
    //         return updatedRows;
    //     });
    // };

    const handleInputChange = (e, index, field) => {
        const raw = e.target.value;
        const value = raw === "" || parseFloat(raw) === 0 || isNaN(parseFloat(raw)) ? "" : parseFloat(raw);

        setRows((prevRows) => {
            const updatedRows = [...prevRows];
            const row = updatedRows[index];

            // Update the specific field in the row
            row[field] = value;

            // Recalculate taxable amount and total cost when quantity or rate changes
            if (field === "SQQUANTITY" || field === "SQRATE") {
                row.SQTAXBALEAMOUNT = Math.round((row.SQQUANTITY || 0) * (row.SQRATE || 0));
                row.SQTAMT = Math.round(row.SQTAXBALEAMOUNT + (row.SQCGST || 0) + (row.SQSGST || 0) + (row.SQIGST || 0));

                if (userdetail.customer != formData.STADDRESS) {
                    row.SQCGST = 0;
                    row.SQSGST = 0;
                    row.SQIGST = Math.round(parseFloat(((row.SQTAXBALEAMOUNT * 18) / 100).toFixed(2)));
                } else {
                    row.SQCGST = Math.round(parseFloat(((row.SQTAXBALEAMOUNT * 9) / 100).toFixed(2)));
                    row.SQSGST = Math.round(parseFloat(((row.SQTAXBALEAMOUNT * 9) / 100).toFixed(2)));
                    row.SQIGST = 0;
                }
                return updatedRows;
            }

            // Recalculate total amount when GST values change
            if (field === "SQCGST" || field === "SQSGST" || field === "SQIGST") {
                row.SQTAMT = Math.round((row.SQTAXBALEAMOUNT || 0) + (row.SQCGST || 0) + (row.SQSGST || 0) + (row.SQIGST || 0));
            }

            // Return the updated rows
            return updatedRows;
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
                SQGAMT: rows.reduce((acc, row) => acc + parseFloat(row.SQTAXBALEAMOUNT || 0), 0),
                SQMNAMT: rows.reduce((acc, row) => acc + parseFloat(row.SQTAMT || 0), 0),
            });
        }
    }, [rows]);


    // c+s validation
    const checkFormValidity = (e) => {
        const { SQNO, } = formData;

        // Check for each field and show validation errors

        if (!formData.SQDATE) {
            Swal.fire({
                icon: "error",
                title: "Quotation Date Validation Error ",
                text: "Please select the date.",
            }).then(() => {
                SQDATERef.current.focus();
            });
            return;
        }

        if (!formData.SQDUEDATE) {
            Swal.fire({
                icon: "error",
                title: "Quotation Validity Date Validation Error ",
                text: "Please select the date(Due Date).",
            }).then(() => {
                SQDUEDATERef.current.focus();
            });
            return;
        }

        if (!formData.SQTRANSPORTID) {
            Swal.fire({
                icon: "error",
                title: "Transporter Name Validation Error",
                text: "Please Select Transporter Name",
            }).then(() => {
                SQTRANSPORTIDRef.current.focus();
            });
            return;
        }

        if (!formData.SQTRANSPORTID) {
            Swal.fire({
                icon: "error",
                title: "Transporter Name Validation Error",
                text: "Please Select Transporter Name",
            }).then(() => {
                SQTRANSPORTIDRef.current.focus();
            });
            return;
        }

        if (!formData.STADDRESS) {
            Swal.fire({
                icon: "error",
                title: "Transporter State Validation Error",
                text: "Please Select Transporter State",
            }).then(() => {
                STADDRESSRef.current.focus();
            });
            return;
        }

        if (!formData.SQTRANSPORTTERMS) {
            Swal.fire({
                icon: "error",
                title: "Transporter Terms validation Error",
                text: "Please Enter The Transporter Terms",
            }).then(() => {
                SQTRANSPORTTERMSRef.current.focus();
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

        // ✅ Validate Rate (Required in every row + pattern check)
        const batchRegex = /^(?!\s*$)[a-zA-Z0-9\s.,'-]+$/;

        for (let i = 0; i < rows.length; i++) {

            const rate = rows[i].SQRATE?.trim();

            if (!rate || !batchRegex.test(rate)) {
                SQRATERef.current.focus();
                Swal.fire({
                    icon: "error",
                    title: "Rate Validation Error",
                    text: `Rate is required in row number  ${i + 1} .`,
                    allowOutsideClick: false,
                    allowEscapeKey: false
                });
                return;
            }

        }

        handleSubmit(e);
    };



    const handleKeyDown = (e, nextInputRef) => {
        if (e.key === 'Enter') {
            e.preventDefault(); // Prevent default form submission on Enter
            if (nextInputRef && nextInputRef.current) {
                nextInputRef.current.focus(); // Move focus to the next input
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

    const OnTranspoterChange = (selectedOption) => {
        const selectedProduct = Tdata.find(p => p.caid == selectedOption.value);
        console.log("Selected Product:", selectedProduct);
        if (selectedProduct) {
            setFormData(prevData => ({
                ...prevData,
                STADDRESS: selectedProduct.sstatename,
                SQTCONTACT: selectedProduct.ccontactpersonmobile,
            }));

            console.log("STADDRESS STADDRESSSTADDRESSSTADDRESSSTADDRESS selectedProduct.sstatename", selectedProduct.SQTCONTACT)

            console.log("STADDRESS STADDRESSSTADDRESSSTADDRESSSTADDRESS", selectedProduct.ccontactpersonmobile)
        }

    }



    return (
        <div>
            <div className="modal fade" id="ProceedRequisition">
                <div className="modal-dialog modal-dialog-centered modal-fullscreen">
                    <div className="modal-content">
                        <div className="page-wrapper-new p-0">
                            <div className="content">
                                <div className="modal-header border-0 custom-modal-header">
                                    <div className="page-title">
                                        <h4> Proceed Requisition To Quatation</h4>
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
                                                        type="number"
                                                        className="form-control"
                                                        value={formData.SQNO}
                                                        onChange={handleChange}
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
                                                        name="srno"
                                                        className="form-control"
                                                        value={srno}
                                                        onChange={handleChange}
                                                        required
                                                        readOnly
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
                                                        onKeyDown={(e) => handleKeyDown(e, SQTRANSPORTIDRef)}
                                                        required
                                                    />
                                                </div>
                                            </div>

                                            <div className="col-lg-2 col-md-3 col-12">
                                                <div className="form-label">
                                                    <label>Customer Name:</label>
                                                    <input
                                                        type="text"
                                                        name="CUSTOMERNAME"
                                                        className="form-control"
                                                        value={formData.CUSTOMERNAME}
                                                        onChange={handleChange}
                                                        // onKeyDown={(e) => handleKeyDown(e, SQTRANSPORTIDRef)}
                                                        readOnly
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
                                                        onChange={handleChange}
                                                        readOnly
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
                                                        onChange={handleChange}
                                                        // onKeyDown={(e) => handleKeyDown(e, SQCADDRESSRef)}
                                                        ref={SQCEMAILRef}
                                                        pattern="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"
                                                        title="please corect email"
                                                        placeholder="Enter email"
                                                        readOnly
                                                    />
                                                </div>
                                            </div>
                                        </div>


                                        <div className="row mb-0">
                                            {/* <div className="col-lg-2 col-md-2 col-12">
                                                <div className="form-label">
                                                    <label className="required">Customer State:</label>
                                                    <Select
                                                        classNamePrefix="react-select"
                                                        options={states}
                                                        value={states.find(option => option.value === formData.SQCADDRESS) || null}
                                                        onChange={(selectedOption) => handleDropdownChange(selectedOption, "SQCADDRESS")}
                                                        // onKeyDown={(e) => handleKeyDown(e, SQTRANSPORTIDRef)}
                                                        onKeyDown={(e) => {
                                                            // Allow Enter to select the dropdown option
                                                            if (e.key === "Enter" && e.target.tagName !== "INPUT") {
                                                                e.preventDefault(); // Optional: prevent form submit
                                                            } else if (e.key === "Tab") {
                                                                handleKeyDown(e, STADDRESSRef); // your existing logic
                                                            }
                                                        }}
                                                        ref={SQCADDRESSRef}
                                                        openMenuOnFocus={true}
                                                    />
                                                </div>
                                            </div> */}
                                            <div className="col-lg-2 col-md-2 col-12">
                                                <div className="form-label">
                                                    <label className="required">Customer State:</label>
                                                    <input
                                                        type="text"
                                                        name="SQCADDRESS"
                                                        className="form-control"
                                                        value={formData.SQCADDRESS}
                                                        onChange={handleChange}
                                                        placeholder="Tranporter State"

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
                                                        }}
                                                        // onKeyDown={(e) => handleKeyDown(e, STADDRESSRef)}
                                                        onKeyDown={(e) => {
                                                            // Allow Enter to select the dropdown option
                                                            if (e.key === "Enter" && e.target.tagName !== "INPUT") {
                                                                e.preventDefault(); // Optional: prevent form submit
                                                            } else if (e.key === "Tab") {
                                                                handleKeyDown(e, SQTRANSPORTTERMRef); // your existing logic
                                                            }
                                                        }}
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
                                                        placeholder="Tranporter Contact"
                                                        readOnly
                                                    />
                                                </div>
                                            </div>

                                            <div className="col-lg-2 col-md-2 col-12">
                                                <div className="form-label">
                                                    <label className="required">Transporter State:</label>
                                                    <input
                                                        type="text"
                                                        name="STADDRESS"
                                                        className="form-control"
                                                        value={formData.STADDRESS}
                                                        onChange={handleChange}
                                                        placeholder="Tranporter State"

                                                    />

                                                </div>
                                            </div>

                                            <div className="col-lg-4 col-md-2 col-12">
                                                <div className="form-label">
                                                    <label className="required">Transport Terms:</label>
                                                    <input
                                                        rows="3"
                                                        className="form-control"
                                                        placeholder="Enter text"
                                                        name="SQTRANSPORTTERMS"
                                                        ref={SQTRANSPORTTERMRef}
                                                        value={formData.SQTRANSPORTTERMS}
                                                        onChange={handleChange}
                                                        required
                                                    />
                                                </div>
                                            </div>
                                        </div>

                                        <div className="col-lg-12">
                                            <div className="border p-3 rounded shadow-sm mb-0">
                                                <div className="modal-body-table">
                                                    <div className="table-responsive">
                                                        <table className="table table-bordered">
                                                            <thead className="thead-dark">
                                                                <tr>
                                                                    <th className="col-4">Product</th>
                                                                    <th className="col-1">Quantity</th>
                                                                    <th className="col-1">UOM</th>
                                                                    <th className="col-1">Rate</th>
                                                                    <th className="col-1">Taxable Value</th>
                                                                    <th className="col-1">CGST</th>
                                                                    <th className="col-1">SGST</th>
                                                                    <th className="col-1">IGST</th>
                                                                    <th className="col-1">Total Cost</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                {rows.map((row, index) => (
                                                                    <tr key={index}>
                                                                        <td className="col-4" style={{ padding: '5px 10px' }}>
                                                                            {row.PRODUCTNAME}
                                                                        </td>
                                                                        <td className="col-1" style={{ padding: '5px 10px' }}>
                                                                            {row.SQQUANTITY}
                                                                        </td>
                                                                        <td className="col-1" style={{ padding: '5px 10px' }}>
                                                                            {row.UOMTITLE}
                                                                        </td>
                                                                        <td className="col-1" style={{ padding: '5px 10px' }}>
                                                                            <input
                                                                                type="number"
                                                                                className="form-control form-control-sm border-0 border-bottom border-warning"
                                                                                value={row.SQRATE}
                                                                                onChange={(e) => handleInputChange(e, index, 'SQRATE')}
                                                                                required
                                                                                ref={SQRATERef}
                                                                            />
                                                                        </td>
                                                                        <td className="col-1" style={{ padding: '5px 10px' }}>
                                                                            <input
                                                                                type="number"
                                                                                className="form-control form-control-sm border-0 border-bottom border-warning"
                                                                                value={row.SQTAXBALEAMOUNT}
                                                                                onChange={(e) => handleInputChange(e, index, 'SQTAXBALEAMOUNT')}
                                                                            />
                                                                        </td>
                                                                        <td className="col-1" style={{ padding: '5px 10px' }}>
                                                                            <input
                                                                                type="number"
                                                                                className="form-control form-control-sm border-0 border-bottom border-warning"
                                                                                value={row.SQCGST}
                                                                                onChange={(e) => handleInputChange(e, index, 'SQCGST')}
                                                                            />
                                                                        </td>
                                                                        <td className="col-1" style={{ padding: '5px 10px' }}>
                                                                            <input
                                                                                type="number"
                                                                                className="form-control form-control-sm border-0 border-bottom border-warning"
                                                                                value={row.SQSGST}
                                                                                onChange={(e) => handleInputChange(e, index, 'SQSGST')}
                                                                            />
                                                                        </td>
                                                                        <td className="col-1" style={{ padding: '5px 10px' }}>
                                                                            <input
                                                                                type="number"
                                                                                className="form-control form-control-sm border-0 border-bottom border-warning"
                                                                                value={row.SQIGST}
                                                                                onChange={(e) => handleInputChange(e, index, 'SQIGST')}
                                                                            />
                                                                        </td>
                                                                        <td className="col-1" style={{ padding: '5px 10px' }}>
                                                                            {row.SQTAMT}
                                                                        </td>
                                                                    </tr>
                                                                ))}
                                                                <tr>
                                                                    <td colSpan="3"></td>
                                                                    <td className="text-end">
                                                                        <strong>Total Taxable:</strong>
                                                                    </td>
                                                                    <td>
                                                                        <input
                                                                            type="text"
                                                                            className="form-control form-control-sm border-0 border-bottom border-warning text-end"
                                                                            value={formData.SQGAMT || '0'}
                                                                            readOnly
                                                                        />
                                                                    </td>

                                                                    <td colSpan="2"></td>
                                                                    <td className="text-end">
                                                                        <strong>Total:</strong>
                                                                    </td>
                                                                    <div className="d-flex justify-content-end">
                                                                        <input
                                                                            name="total"
                                                                            value={formData.SQMNAMT || '0'}
                                                                            className="border-0 border-bottom border-warning w-100 text-end"
                                                                            readOnly
                                                                        />
                                                                    </div>
                                                                </tr>
                                                            </tbody>
                                                        </table>
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
            </div>
        </div>
    );
};

export default ProceedRequisition;

