import React, { useRef, useEffect, useState } from "react";
import { ArrowLeft } from "feather-icons-react/build/IconComponents";
import { Modal } from "react-bootstrap";
import withReactContent from "sweetalert2-react-content";
import Swal from "sweetalert2";
import { Link } from "react-router-dom";
import axios from 'axios';
// import { useLocation } from 'react-router-dom';

import { ACSPLGUID, baseUrl, convertToISODate } from "../../core/json/custom";
import Select from "react-select";
import { getUserData } from "../../Context/UserData";
function ProceedQuotation({ SCAID, SQAMAID, SQNO, onRefresh }) {

    console.log("sales requisition number by proceed........", SQNO)

    const { userdetail } = getUserData();
    const GUID = ACSPLGUID.getNew()
    const SCTRNNORef = useRef(null);
    const SCDATERef = useRef(null);
    const SCVNORef = useRef(null);
    const SCPOSUPPLYRef = useRef(null);
    const PAYMENTMODERef = useRef(null);
    const SCNARRATIONRef = useRef(null);
    // const SCVENDORRef = useRef(null);
    // const SCCONSIGNERRef = useRef(null);


    const [states, setstates] = useState([]);
    // const [showForm, setShowForm] = useState(false);
    const MySwal = withReactContent(Swal);
    const [paymentmode, setpaymentmode] = useState([]);//payment dropdown
    const [showModal, setShowModal] = useState(false);
    const [transporter, settransporter] = useState([]);
    // const [Tdata, setTdata] = useState([]);


    const [rows, setRows] = useState([]);

    const [formData, setFormData] = useState({
        SCTRNNO: '',
        SCDATE: new Date().toISOString().split('T')[0], // Current date set here
        SCVNO: '',
        SQNO: '',
        SCPOSUPPLY: '',
        PAYMENTMODE: '',
        SCSELLER: '',      // Customer Id
        CUSTOMERNAME: '',  // Customer name            
        SCCONSIGNER: '',   // Transporter 
        SCNARRATION: '',
        SCGATE: '',
        SCNATE: '',
        TransporterID: '',
    });

    // Accept Master Inputs
    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData(prev => ({ ...prev, [name]: value }));
    };

    // Accept Detail Inputs
    const handleInputChange = (event, index, field) => {
        const updatedRows = [...rows];
        updatedRows[index][field] = event.target.value;

        if (field === 'SCDQUANTITY' || field === 'SCDRATE') {
            const quantity = parseFloat(updatedRows[index].SCDQUANTITY || 0) || 0;
            const rate = parseFloat(updatedRows[index].SCDRATE || 0) || 0;
            const taxableValue = quantity * rate;
            const cgst = Math.round((taxableValue * 9) / 100);
            const sgst = Math.round((taxableValue * 9) / 100);
            const igst = Math.round((taxableValue * 18) / 100);
            const SCDTAMT = Math.round(taxableValue + cgst + sgst + igst);

            updatedRows[index].SCDTAXABLE = taxableValue;
            updatedRows[index].SCDCGST = cgst;
            updatedRows[index].SCDSGST = sgst;
            updatedRows[index].SCDIGST = igst;
            updatedRows[index].SCDTAMT = SCDTAMT;
        }
        setRows(updatedRows);
    };


    // Fetch DropDown Data (Place of supply & Payment Mode)
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
        const fetchpaymentmodes = async () => {
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
                        // setTdata(DATA);
                    })

            } catch (error) {
                console.error("Error fetching Access Right Data:", error);
            }

        };

        fetchpaymentmodes();
        fetchstates();
        fetchTransportorData();
    }, []);


    // Fetch On Proceed Quotation Master Data and Set It.
    useEffect(() => {

        if (SQNO) {
            const fetchQuotationData = async () => {
                try {
                    const payload1 = {
                        sqno: SQNO,
                        companyid: userdetail?.companyID ? userdetail.companyID : "",
                        deptid: userdetail?.departmentID ? userdetail.departmentID : "",
                    };

                    const headers = {
                        "Content-Type": "application/json",
                        Accept: "*/*"
                    };

                    const response = await axios.post(
                        baseUrl.Url + "/backend/api/GET_SQuatationForProceed",
                        payload1,
                        { headers }
                    );

                    if (response.status !== 200) throw new Error("Failed to fetch GET_SQuatationForProceed data");
                    let apiData = response.data[0];
                    // Get Tranporter label label
                    const matchedTransport = transporter.find(
                        (trans) => trans.value == apiData.sqtransportid
                    );
                    setFormData((prev) => ({
                        ...prev,

                        SQNO: apiData.sqno || "",
                        CUSTOMERNAME: apiData.vendorname || "",
                        SQCAID: apiData.sqcaid || "",
                        // SCCONSIGNER: apiData.sqtransportid || "",
                        // SCCONSIGNER: transporter.find((trans) => trans.value == apiData.sqtransportid)?.value || "",
                        SCCONSIGNER: matchedTransport?.label || "", // 🟢 Set label here
                        TransporterID: apiData.sqtransportid || "",
                    }));

                    if (response.data.length > 0) {
                        const mappedProducts = response.data.map((item) => ({
                            PRODUCTNAME: item.productname,
                            UOMTITLE: item.uomtitle,
                            SCDPRODUCT: item.sqproduct,
                            SCDUOM: item.squom,
                            SHSN: item.hsncode,
                            SCDQUANTITY: item.qquantity,
                            SCDRATE: item.qrate,
                            SCDTAXABLE: item.qtamt,
                            SCDCGST: item.qcgst,
                            SCDSGST: item.qsgst,
                            SCDIGST: item.qigst,
                            SCDTAMT: item.qtamt + item.qcgst + item.qsgst + item.qigst,
                            SBATCHNO: item.batchno,
                            SEXPIRYDATE: item.expirydate,
                        }));

                        setRows(mappedProducts);
                    }
                } catch (error) {
                    console.error("Error in Get_VisitorData API Call:", error);
                }
            };

            fetchQuotationData();
        }
    }, [SQNO]);




    const showConfirmationAlert = () => {
        MySwal.fire({
            title: "Are you sure?",
            text: "Do you want to save this data?",
            showCancelButton: true,
            confirmButtonColor: "#00ff00",
            confirmButtonText: "SAVE",
            cancelButtonColor: "#092C4C",
            cancelButtonText: "CANCEL",
        }).then((result) => {
            if (result.isConfirmed) {
                handleFormSubmission();
                handleModalConfirm();
            }
        });
    };

    const handleModalConfirm = () => {
        console.log('Form Data:', formData);
        console.log('Rows Data:', rows);
        setShowModal(false);
    };

    const handleModalClose = () => setShowModal(false);

    const handleSubmit = (event) => {
        event.preventDefault();

        const form = event.target.closest("form");
        if (!form.checkValidity()) {
            form.reportValidity();
            return;
        }
        showConfirmationAlert();
    };

    const handleFormSubmission = async () => {
        try {
            const payload1 = {
                "scaid": SCAID ? SCAID : GUID,
                "sctrnno": ACSPLGUID.getNew(),          // Challan No
                "scdate": formData.SCDATE,
                "scvno": formData.SCVNO,
                "sqno": formData.SQNO,
                "scposupply": formData.SCPOSUPPLY,
                "paymentmode": formData.PAYMENTMODE,
                "scseller": formData.SQCAID,            // Customer Id
                // "scconsigner": formData.SCCONSIGNER,    //Tranporter
                "scconsigner": formData.TransporterID,
                "scnarration": formData.SCNARRATION,
                "scgamt": formData.SCGATE,
                "scnamt": formData.SCNATE,
                "soreff": "",
                "companyid": userdetail?.companyID || "",
                "deptid": userdetail?.departmentID || "",
                "statusid": '2',
            }
            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };

            axios({
                method: "POST",
                url: baseUrl.Url + "/backend/api/SP_AddUpdSChallanMaster",
                data: JSON.stringify(payload1),
                headers: headers,
            })
            console.log("payload1", payload1);

            Swal.fire({
                icon: "success",
                title: "Saved!",
                text: "Data saved successfully.",
                confirmButtonText: "OK",
            });

            const payload2 = rows.map((row) => ({
                "scdaid": ACSPLGUID.getNew(),
                "scaid": SCAID ? SCAID : GUID,
                "scdproduct": row.SCDPRODUCT,
                "scduom": row.SCDUOM,
                "hsncode": row.SHSN,
                "scdquantity": row.SCDQUANTITY.toString(),
                "scdrate": row.SCDRATE,
                "scdtamt": row.SCDTAMT,
                "scdtaxable": row.SCDTAXABLE.toString(),
                "scdigst": row.SCDIGST,
                "scdsgst": row.SCDSGST,
                "scdcgst": row.SCDCGST,
                "batchno": row.SCBATCH,
                "expirydate": row.SEXPIRYDATE,
                "companyid": userdetail?.companyID || "",
                "deptid": userdetail?.departmentID || "",
            }));

            const response = await axios({
                method: "POST",
                url: baseUrl.Url + "/backend/api/SP_AddUpdSChallanDetails",
                data: JSON.stringify(payload2),
                headers: headers,
            });

            console.log("Payload Sent:", payload2);
            console.log("Response Received:", response.data);
            Swal.fire({
                icon: "success",
                title: "Saved!",
                text: "Data saved successfully.",
                confirmButtonText: "OK",
            }).then((result) => {
                if (result.isConfirmed) {
                    const modal = document.getElementById("ProceedQuotation");
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
                        SCTRNNO: '',
                        SCDATE: new Date().toISOString().split('T')[0], // Current date set here
                        SCVNO: '',
                        SQNO: '',
                        SCPOSUPPLY: '',
                        PAYMENTMODE: '',
                        SCSELLER: '',      // Customer Id
                        CUSTOMERNAME: '',  // Customer name            
                        SCCONSIGNER: '',   // Transporter 
                        SCNARRATION: '',
                        SCGATE: '',
                        SCNATE: '',
                    });

                    if (onRefresh) {
                        onRefresh();
                    }

                    setRows([]);
                }
            });
            // navigate(route.HSNMaster);
        } catch (error) {
            console.error("Submission Error:", error);
            Swal.fire({
                icon: "error",
                title: "Error",
                text: "Failed to save data. Please try again.",
            });
        }
    };


    useEffect(() => {
        const handleKeydown = (event) => {
            if (event.ctrlKey && event.key === 's') {
                event.preventDefault();
                handleSubmit(event);
            }
            if (event.ctrlKey && event.key === 'e') {

                console.log("Ctrl+E Pressed");
            }
        };
        document.addEventListener('keydown', handleKeydown);
        return () => {
            document.removeEventListener('keydown', handleKeydown);
        };
    }, [rows, formData]);


    useEffect(() => {
        if (rows && rows.length > 0) {
            const totalTaxable = rows.reduce((acc, row) => acc + parseFloat(row.SCDTAXABLE || 0), 0);
            const totalAmount = rows.reduce((acc, row) => acc + parseFloat(row.SCDTAMT || 0), 0);

            setFormData({
                ...formData,
                SCGATE: parseInt(totalTaxable),
                SCNATE: parseInt(totalAmount),
            });
        }
    }, [rows]);

    const handleKeyDown = (e, nextRef) => {
        if (e.key === 'Enter' && nextRef.current) {
            nextRef.current.focus();
            e.preventDefault();
        }
    };


    // Handle keyboard shortcuts with validation
    useEffect(() => {
        const handleShortcut = (e) => {
            if (e.ctrlKey && (e.key === 'e' || e.key === 'E')) {
                e.preventDefault();
                showExitAlert();
            }
            if (e.ctrlKey && (e.key === 's' || e.key === 'S')) {
                e.preventDefault();
                validateinput();
            }
        };
        window.addEventListener('keydown', handleShortcut);
        return () => {
            window.removeEventListener('keydown', handleShortcut);
        };
    }, [formData, handleSubmit]);
    // validation on C+S only for master inputs
    const validateinput = () => {

        if (!formData.SCVNO || !/^[A-Z]{2}\d{2}[A-Z]{1,3}\d{4}$/i.test(formData.SCVNO.trim())) {
            SCVNORef.current.focus();
            Swal.fire({
                icon: "error",
                title: "Vehicle Number Validation Error",
                text: "Please enter a valid Vehicle Number (e.g., MH12AB1234). Only letters and numbers are allowed.",
                allowOutsideClick: false,
                allowEscapeKey: false
            });
            return;
        }

        if (!formData.SCPOSUPPLY || formData.SCPOSUPPLY.length === 0) {
            SCPOSUPPLYRef.current?.focus();
            Swal.fire({
                icon: "error",
                title: "Place of supply Validation Error",
                text: "Please select Place of supply.",
                allowOutsideClick: false,
                allowEscapeKey: false
            })
            return;
        }

        if (!formData.PAYMENTMODE || formData.PAYMENTMODE.length === 0) {
            PAYMENTMODERef.current?.focus();
            Swal.fire({
                icon: "error",
                title: "Payment Mode Validation Error",
                text: "Please select Payment Mode.",
                allowOutsideClick: false,
                allowEscapeKey: false
            })
            return;
        }


        if (formData.SCNARRATION && !/^(?!\s*$)[a-zA-Z0-9\s.,'-]+$/.test(formData.SCNARRATION.trim())) {
            SCNARRATIONRef.current?.focus();
            Swal.fire({
                icon: "error",
                title: "Narration Validation Error",
                text: "Please Enter Narration.",
                allowOutsideClick: false,
                allowEscapeKey: false
            })
        }

        // ✅ Validate Batch No & Expiry Date (Required in every row + pattern check)
        const batchRegex = /^(?!\s*$)[a-zA-Z0-9\s.,'-]+$/;

        for (let i = 0; i < rows.length; i++) {
            const batchValue = rows[i].SCBATCH?.trim();
            const expiryDate = rows[i].SEXPIRYDATE?.trim();

            if (!batchValue || !batchRegex.test(batchValue)) {
                Swal.fire({
                    icon: "error",
                    title: "Batch Number Validation Error",
                    text: `Batch No is required in row ${i + 1} and must not contain special characters.`,
                    allowOutsideClick: false,
                    allowEscapeKey: false
                });
                return;
            }
            if (!expiryDate) {
                Swal.fire({
                    icon: "error",
                    title: "Expiry Date Validation Error",
                    text: `Expiry Date is required in row ${i + 1}.`,
                    allowOutsideClick: false,
                    allowEscapeKey: false
                });
                return;
            }
        }

        handleSubmit(event);
    }


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

                const modal = document.getElementById("ProceedQuotation");
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
                    SCTRNNO: '',
                    SCDATE: new Date().toISOString().split('T')[0], // Current date set here
                    SCVNO: '',
                    SQNO: '',
                    SCPOSUPPLY: '',
                    PAYMENTMODE: '',
                    SCSELLER: '',      // Customer Id
                    CUSTOMERNAME: '',  // Customer name            
                    SCCONSIGNER: '',   // Transporter 
                    SCNARRATION: '',
                    SCGATE: '',
                    SCNATE: '',
                });

                setRows([]);

                if (onRefresh) {
                    onRefresh();
                }

            }
        });
    };

    return (
        <div>
            <div
                className="modal fade"
                id="ProceedQuotation"
                tabIndex={-1}
                aria-labelledby="exampleModalFullscreenLabel"
                aria-hidden="true"
                style={{ display: "none" }}
            >
                <div className="modal-dialog modal-fullscreen mbgcolor">
                    <div className="modal-content mbgcolor ">
                        <div className="modal-content mbgcolor">
                            <div className="page-wrapper-new p-0">
                                <div className="content mbgcolor">
                                    <div className="modal-header border-0 custom-modal-header">
                                        <div className="page-title ps-2">
                                            <h4>Proceed Quotation To chalan</h4>
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
                                        {/* {showForm && ( */}
                                        <form onSubmit={handleSubmit}>
                                            <div className="row custom-background">
                                                <div className="col-lg-2 col-md-6 col-sm-12">
                                                    <div className="mb-0 add-product form-label">
                                                        <label className="required">Challan No</label>
                                                        <input
                                                            ref={SCTRNNORef}
                                                            type="text"
                                                            className="form-control border"
                                                            name="SCTRNNO"
                                                            value={formData.SCTRNNO}
                                                            onChange={handleChange}
                                                            placeholder="Auto Generate"
                                                            readOnly
                                                        />
                                                    </div>
                                                </div>
                                                <div className="col-lg-2 col-sm-6 col-12">
                                                    <div className=" mb-0 add-product form-label">
                                                        <label className="required"> challan Date</label>
                                                        <div >
                                                            <input
                                                                ref={SCDATERef}
                                                                type="date"
                                                                className="form-control"
                                                                value={formData.SCDATE}
                                                                placeholder="Choose Date"
                                                                onChange={(e) =>
                                                                    setFormData({
                                                                        ...formData,
                                                                        SCDATE: e.target.value,
                                                                    })
                                                                }
                                                                onKeyDown={(e) => handleKeyDown(e, SCVNORef)}
                                                                required
                                                            />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div className="col-lg-2 col-md-4 col-sm-12">
                                                    <div className="mb-0 add-product form-label">
                                                        <label className="required">Vehicle No</label>
                                                        <input
                                                            ref={SCVNORef}
                                                            type="text"
                                                            className="form-control border"
                                                            name="SCVNO"
                                                            value={formData.SCVNO}
                                                            onChange={handleChange}
                                                            required
                                                            placeholder="Enter Vehicle Number"
                                                            pattern="^[A-Z]{2}[0-9]{2}[A-Z]{1,2}[0-9]{4}$"
                                                            title="please folow this way Eg.MH15GF2244"
                                                        />
                                                    </div>
                                                </div>

                                                <div className="col-lg-2 col-md-4 col-sm-12">
                                                    <div className="mb-0 add-product form-label">
                                                        <label className="required">Quatation No</label>
                                                        <input
                                                            type="text"
                                                            className="form-control border"
                                                            name="qno"
                                                            value={formData.SQNO}
                                                            onChange={handleChange}
                                                            readOnly
                                                        />
                                                    </div>
                                                </div>
                                                <div className="col-lg-4 col-md-4 col-sm-12">
                                                    <div className="mb-0 add-product form-label">
                                                        <label className="required">Place of supply</label>
                                                        <Select
                                                            ref={SCPOSUPPLYRef}
                                                            classNamePrefix="react-select"
                                                            options={states}
                                                            placeholder="Choose"
                                                            value={states.find((option) => option.value === formData.SCPOSUPPLY) || null}
                                                            onChange={(selectedOption) => {
                                                                setFormData((prevData) => ({
                                                                    ...prevData,
                                                                    SCPOSUPPLY: selectedOption ? selectedOption.value : '',
                                                                }));
                                                            }}
                                                            styles={{
                                                                menu: (base) => ({
                                                                    ...base,
                                                                    zIndex: 1050,
                                                                }),
                                                            }}
                                                            required
                                                            openMenuOnFocus={true}
                                                        />
                                                    </div>
                                                </div>
                                                <div className="col-lg-3 col-md-4 col-sm-12">

                                                </div>
                                            </div>
                                            <div className="row custom-background">
                                                <div className="col-lg-4 col-md-4 col-sm-12">
                                                    <div className="mb-0 add-product form-label">
                                                        <label className="required">Payment Mode</label>
                                                        <Select
                                                            classNamePrefix="react-select"
                                                            options={paymentmode}
                                                            placeholder="Choose"
                                                            value={paymentmode.find((option) => option.value === formData.PAYMENTMODE) || null}
                                                            onChange={(selectedOption) => {
                                                                setFormData((prevData) => ({
                                                                    ...prevData,
                                                                    PAYMENTMODE: selectedOption ? selectedOption.value : '',
                                                                }));
                                                            }}
                                                            ref={PAYMENTMODERef}
                                                            openMenuOnFocus={true}
                                                            required
                                                        />
                                                    </div>
                                                </div>
                                                <div className="col-lg-4 col-md-4 col-sm-12">
                                                    <div className="mb-0 add-product form-label">
                                                        <label>Customer name</label>
                                                        <input
                                                            type="text"
                                                            className="form-control border"
                                                            name="CUSTOMERNAME"
                                                            value={formData.CUSTOMERNAME}
                                                            onChange={handleChange}
                                                        />
                                                    </div>
                                                </div>

                                                <div className="col-lg-4 col-md-4 col-sm-12">
                                                    <div className="mb-0 add-product form-label">
                                                        <label>Transpoter</label>
                                                        <input
                                                            type="text"
                                                            className="form-control border"
                                                            name="SCCONSIGNER"
                                                            value={formData.SCCONSIGNER}
                                                            onChange={handleChange}
                                                        />
                                                    </div>
                                                </div>

                                                <div className="col-8">
                                                    <div className="mb-0 add-product form-label">
                                                        <label >Narration</label>
                                                        <textarea
                                                            ref={SCNARRATIONRef}
                                                            rows={1}
                                                            cols={1}
                                                            className="form-control border text-secondary"
                                                            name="SCNARRATION"
                                                            value={formData.SCNARRATION}
                                                            onChange={handleChange}
                                                            placeholder="Enter Narration"
                                                        />
                                                    </div>
                                                </div>
                                            </div>

                                            <div className="border p-3 rounded shadow-sm mb-4 mt-5">
                                                <div className="row">
                                                    <div className="col-lg-12">
                                                        <div className="modal-body-table">
                                                            <div className="table-responsive" style={{ height: "calc(60vh - 120px)" }}>
                                                                <table className="table datanew">
                                                                    <table className="table table-bordered" style={{ tableLayout: "fixed" }}>
                                                                        <thead className="thead-dark"
                                                                            style={{
                                                                                tablelayout: 'fixed',
                                                                                position: 'sticky',
                                                                                top: 0,
                                                                                zIndex: 1
                                                                            }}>
                                                                            <tr>
                                                                                <th className="col-3">Product</th>
                                                                                <th className="col-1">HSN</th>
                                                                                <th className="col-1">UOM</th>
                                                                                <th className="col-1">Batch No</th>
                                                                                <th className="col-1"> Ex.Date</th>
                                                                                <th className="col-1">Quantity</th>
                                                                                <th className="col-1">Rate</th>
                                                                                <th className="col-1">Taxable T</th>
                                                                                <th className="col-1">CGST</th>
                                                                                <th className="col-1">SGST</th>
                                                                                <th className="col-1">IGST</th>
                                                                                <th className="col-1">Total</th>

                                                                                {/* <th className="col-1">Action</th> */}
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            {rows?.map((row, index) => (
                                                                                <tr key={index}>
                                                                                    <td><span>{row.PRODUCTNAME}</span></td>
                                                                                    <td><span>{row.SHSN}</span></td>
                                                                                    <td><span>{row.UOMTITLE}</span></td>
                                                                                    <td>
                                                                                        <input
                                                                                            type="text"
                                                                                            className="form-control form-control-sm border-0 border-bottom border-warning"
                                                                                            value={row.SCBATCH}
                                                                                            onChange={(e) => handleInputChange(e, index, 'SCBATCH')}
                                                                                            required
                                                                                        />
                                                                                    </td>

                                                                                    <td>
                                                                                        <input
                                                                                            type="date"
                                                                                            className="form-control form-control-sm border-0 border-bottom border-warning"
                                                                                            value={row.SEXPIRYDATE}
                                                                                            onChange={(e) => handleInputChange(e, index, 'SEXPIRYDATE')}
                                                                                            required

                                                                                        />
                                                                                    </td>

                                                                                    {/* <td>
                                                                                        <input
                                                                                            type="number"
                                                                                            className="form-control form-control-sm border-0 border-bottom border-warning"
                                                                                            value={row.SCDQUANTITY}
                                                                                            onChange={(e) => handleInputChange(e, index, 'SCDQUANTITY')}
                                                                                            readOnly

                                                                                        />
                                                                                    </td> */}
                                                                                    <td><span>{row.SCDQUANTITY}</span></td>
                                                                                    <td><span>{row.SCDRATE}</span></td>
                                                                                    <td><span>{row.SCDTAXABLE}</span></td>
                                                                                    <td><span>{row.SCDCGST}</span></td>
                                                                                    <td><span>{row.SCDSGST}</span></td>
                                                                                    <td><span>{row.SCDIGST}</span></td>
                                                                                    <td><span>{row.SCDTAMT}</span></td>

                                                                                </tr>
                                                                            ))}
                                                                        </tbody>
                                                                        <tfoot>

                                                                            <td colSpan="6"></td>
                                                                            <td><strong>Total Taxable:</strong></td>
                                                                            <td>
                                                                                <input

                                                                                    type="text"
                                                                                    className="form-control form-control-sm border-0 border-bottom border-warning"
                                                                                    name="PCGATE"
                                                                                    value={formData.SCGATE || '0'}
                                                                                    readOnly
                                                                                />
                                                                            </td>
                                                                            <td colSpan="2"></td>
                                                                            <td><strong>Total:</strong></td>
                                                                            <td>
                                                                                <input
                                                                                    type="text"
                                                                                    className="form-control form-control-sm border-0 border-bottom border-warning"
                                                                                    name="PCNATE"
                                                                                    value={formData.SCNATE || '0'}
                                                                                    readOnly
                                                                                />
                                                                            </td>
                                                                            <td></td>
                                                                            <td></td>
                                                                            <td></td>
                                                                            <td></td>
                                                                        </tfoot>
                                                                    </table>
                                                                </table>
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
                                        {/* )} */}
                                    </div>
                                </div>
                            </div>
                        </div>
                        {/* </div> */}

                        {/* Confirmation Modal */}
                        <Modal show={showModal} onHide={handleModalClose} centered>
                            <Modal.Header closeButton>
                                <Modal.Title>Confirm Submission</Modal.Title>
                            </Modal.Header>
                            <Modal.Body style={{ color: "green" }}>
                                <h4>Do you want to save the purchase data?</h4>
                            </Modal.Body>
                            <Modal.Footer>
                                <button
                                    type="button"
                                    className="btn btn-secondary"
                                    onClick={handleModalClose}
                                >
                                    Cancel
                                </button>
                                <button
                                    type="button"
                                    className="btn btn-primary"
                                    onClick={handleModalConfirm}
                                >
                                    Save
                                </button>
                            </Modal.Footer>
                        </Modal>
                    </div>
                </div>
            </div>
        </div >
    )
}

export default ProceedQuotation
