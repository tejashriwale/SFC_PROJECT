import React, { useRef, useEffect, useState } from "react";
import { Link } from "react-router-dom";
import Select from "react-select";
import { all_routes } from "../../Router/all_routes";
import withReactContent from "sweetalert2-react-content";
import Swal from "sweetalert2";
import axios from 'axios';
import { baseUrl, convertToISODate, formatToDateTimeLocal } from "../../core/json/custom";
import { ACSPLGUID } from "../../core/json/custom";
import { getUserData } from "../../Context/UserData";
import { useNavigate } from "react-router-dom";
import { useLocation } from 'react-router-dom';
import { OverlayTrigger, Tooltip } from "react-bootstrap";
import {
    ArrowLeft,
    Info,
    Edit,
    Trash2,
} from "feather-icons-react/build/IconComponents";
const CropTransportInvoiceProceed = ({ STATUS, TREDER, onRefresh, SLVMID, onSaved, refreshKey }) => {
    console.log("VAIDVAIDVAID", SLVMID);
    console.log("VAIDVAIDVAID", TREDER);
    console.log("STATUS", STATUS);
    const { userdetail } = getUserData();
    const GUID = ACSPLGUID.getNew();
    const [Trader, setTrader] = useState([]);
    const [Transections, setTransections] = useState([]);
    const MySwal = withReactContent(Swal);
    const Cropref = useRef(null);
    const Graderef = useRef(null);
    const Traderref = useRef(null);
    const VehicleNumberref = useRef(null);
    const CaretCountref = useRef(null);
    const SaveRef = useRef(null);
    const salevalueref = useRef(null);
    const [formData, setFormData] = useState({
        Crop: [],
        Grade: [],
        Trader: "",
        VehicleNumber: "",
        CaretCount: "",
        date: userdetail?.APPDT || "",
        salevalue: 0,
        Advance: 0
    });
    const [advanceInfo, setAdvanceInfo] = useState(null);
    const [TRAID, setTRAID] = useState(null);
    const [ADVAMT, setADVAMT] = useState(null);
    const [detailData, setDetailData] = useState([]);
    useEffect(() => {
        setFormData(prev => ({
            ...prev,
            date: userdetail?.APPDT || "",
        }));
    }, []);
    const formatDate_1 = (dateStr) => {
        const options = { day: '2-digit', month: 'short', year: 'numeric' };
        return new Date(dateStr).toLocaleDateString('en-GB', options); // e.g., "14 Jul 2025"
    };
    const [isMobile, setIsMobile] = useState(window.innerWidth < 768);
    useEffect(() => {
        const handleResize = () => setIsMobile(window.innerWidth < 768);
        window.addEventListener('resize', handleResize);
        return () => window.removeEventListener('resize', handleResize);
    }, []);
    useEffect(() => {
        if (SLVMID) {
            const fetchData = async () => {
                try {
                    const payload1 = {
                        "slvmid": SLVMID,
                        "keyword": "%",
                        "companyid": userdetail?.companyID ? userdetail.companyID : "",
                        "deptid": userdetail?.departmentID ? userdetail.departmentID : ""
                    };
                    const headers = {
                        "Content-Type": "application/json",
                        Accept: "*/*",
                    };
                    const response1 = await axios.post(
                        `${baseUrl.Url}/backend/api/GET_StockLoadingVehicleMaster`,
                        payload1,
                        { headers }
                    );
                    if (response1.status !== 200)
                        throw new Error("Failed to fetch SchedulerMaster data");
                    if (response1.data.length > 0) {
                        const data = response1.data[0];
                        console.log(response1.data, "response1.dataresponse1.data")
                        setFormData({
                            Crop: data.croP1 ? data.croP1.split(',') : [],
                            Grade: data.gradE1 ? data.gradE1.split(',') : [],
                            Trader: data.tradeR1 || "",
                            VehicleNumber: data.vehicleno || "",
                            CaretCount: data.caretcount || "",
                            salevalue: data.samt || 0
                        });
                        console.log(data, "hrutikhghvjhjh")
                    }
                } catch (error) {
                    console.error("Error fetching data:", error);
                }
            };
            const fetchDetailTableData = async () => {
                try {
                    const payload1 = {
                        "svvmid": SLVMID,
                        "companyid": userdetail?.companyID ? userdetail.companyID : "",
                        "deptid": userdetail?.departmentID ? userdetail.departmentID : ""
                    };
                    const headers = {
                        "Content-Type": "application/json",
                        Accept: "*/*",
                    };
                    const response = await axios.post(
                        `${baseUrl.Url}/backend/api/GET_CropTransportInvoiceData`,
                        payload1,
                        { headers }
                    );
                    if (response.status !== 200)
                        throw new Error("Failed to fetch SchedulerMaster data");
                    if (response.data.length > 0) {
                        const data = response.data[0];
                        console.log(response.data, "response1.dataresponse.data")
                        setTableData1(response.data);
                        console.log(data, "hrutikhghvjhjh")
                    }
                } catch (error) {
                    console.error("Error fetching data:", error);
                }
            };
            const fetchTranData = async () => {
                try {
                    const payload = {
                        companyid: userdetail?.companyID || "",
                        deptid: userdetail?.departmentID || "",
                        maid: TREDER || ""
                    };
                    const headers = {
                        "Content-Type": "application/json",
                        Accept: "*/*",
                    };
                    const response = await axios.post(
                        `${baseUrl.Url}/backend/api/GET_AllTranDataCustomer`,
                        payload,
                        { headers }
                    );

                    const transactions = response.data?.[0] || [];
                    setTransections(transactions);

                    const advamt = response.data?.[0]?.advamt || 0;

                    setFormData(prev => ({
                        ...prev,
                        Advance: advamt || 0,
                    }));

                    if (advamt > 0) {
                        setAdvanceInfo({
                            amount: advamt,
                            date: response.data[0].trndt,
                        });
                        setTRAID(response.data[0].traid || "");
                        setADVAMT(advamt)
                    } else {
                        setAdvanceInfo(null);
                        setTRAID("");
                        setADVAMT("");
                    }
                    return transactions;

                } catch (err) {
                    console.error("Error fetching latest transactions:", err);
                    return [];
                }
            }
            fetchTranData();
            fetchData();
            fetchDetailTableData();
        }
    }, [SLVMID, refreshKey]);
    useEffect(() => {
        const modal = document.getElementById('AddTransportProcced');
        const handleShown = () => {
            Cropref.current?.focus();
        };
        if (modal) {
            modal.addEventListener('shown.bs.modal', handleShown);
        }
        return () => {
            if (modal) {
                modal.removeEventListener('shown.bs.modal', handleShown);
            }
        };
    }, []);


    const handleFinalSave = (e) => {
        const { Crop, Trader, VehicleNumber } = formData;
        if (!formData.salevalue || formData.salevalue <= 0) {
            Swal.fire({
                icon: "warning",
                title: "Invalid Amount",
                text: "Please enter a valid Sales amount greater than 0.",
                confirmButtonText: "OK",
            }).then(() => {
                if (salevalueref && salevalueref.current) {
                    salevalueref.current.focus();
                }
            });
            return;
        }
        if (!formData.Crop || formData.Crop.length === 0) {
            Cropref.current?.focus();
            Swal.fire({
                icon: "error",
                title: "Validation Error",
                text: "Please select at least one Crop.",
            })
            return;
        }
        if (!formData.Grade || formData.Grade.length === 0) {
            Graderef.current?.focus();
            Swal.fire({
                icon: "error",
                title: "Validation Error",
                text: "Please select at least one Grade.",
            });
            return;
        }
        if (!Trader) {
            Traderref.current?.focus();
            Swal.fire({
                icon: "error",
                title: "Validation Error",
                text: "Please select a Trader.",
            })
            return;
        }
        if (!VehicleNumber || !/^[A-Z]{2}[0-9]{2}[A-Z]{0,2}[0-9]{4}$/i.test(VehicleNumber)) {
            VehicleNumberref.current?.focus();
            Swal.fire({
                icon: "error",
                title: "Validation Error",
                text: "Please enter a valid vehicle number (e.g., MH17AB1234).",
            })
            return;
        }
        if (!formData.CaretCount) {
            CaretCountref.current.focus();
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'Please enter the Caret Count.'
            });
            return;
        }
        Swal.fire({
            title: 'Do you want to save the information?',
            icon: 'question',
            showCancelButton: true,
            confirmButtonText: 'Yes',
            cancelButtonText: 'No'
        }).then((result) => {
            if (result.isConfirmed) {
                handleSubmit(e);
            }
        });
    };
    const handleSubmit = async (e) => {
        const { expenseTotal, amountTotal, salesamount } = TableData1
            .filter(row => parseInt(row.isdeleted || 0) !== 1)
            .reduce(
                (acc, row) => {
                    acc.expenseTotal += parseFloat(row.totexpense) || 0;
                    acc.amountTotal += parseFloat(row.totamount) || 0;
                    acc.salesamount += parseFloat(row.svalue) || 0;
                    return acc;
                },
                { expenseTotal: 0, amountTotal: 0, salesamount: 0 }
            );
        const transectionPayloadList = [];

        const farmerstatus = {
            traid: TRAID || "",
            companyid: userdetail?.companyID || "",
            deptid: userdetail?.departmentID || ""
        };

        if (farmerstatus.traid) {
            try {
                const response = await axios.post(
                    `${baseUrl.Url}/backend/api/SP_UpdFarmerStatus`,
                    farmerstatus,
                    { headers: { "Content-Type": "application/json" } }
                );
                console.log("Farmer status updated:", response.data);
            } catch (err) {
                console.error("Error updating farmer status:", err);
            }
        } else {
            console.warn("TRAID is empty. Skipping Farmer Status API call.");
        }
        const NewPKID = ACSPLGUID.getNew();


        try {
            const masterPayload = {
                "ctaid": SLVMID || GUID,
                "crop": formData.Crop?.join(',') || "",
                "treader": formData?.Trader || "",
                "vehchno": formData?.VehicleNumber || "",
                "crtcnt": parseInt(formData?.CaretCount || 0),
                "totsamt": parseFloat(formData?.salevalue || 0),
                "totex": parseFloat(expenseTotal?.toFixed(2) || 0),
                "totiwdamt": parseFloat(amountTotal?.toFixed(2) || 0),
                "companyid": userdetail?.companyID || "",
                "deptid": userdetail?.departmentID || "",
                "uaid": userdetail?.uaid || "",
            };
            console.log(masterPayload, "masterPayload")
            const masterResponse = await axios.post(
                `${baseUrl.Url}/backend/api/SP_AddUpdCropTransportInvoice`,
                masterPayload,
                { headers: { 'Content-Type': 'application/json' } }
            );
            if (masterResponse.status !== 200) throw new Error("Master save failed");
            transectionPayloadList.push({
                traid: ACSPLGUID.getNew(),
                trnsr: 1,
                trnsid: "10",
                trntid: "20",
                trndt: userdetail?.APPDT || "",
                cropid: formData.Crop?.join(',') || "",
                refaid: SLVMID || GUID,
                maid: TREDER || "",
                pamt: parseFloat(ADVAMT || 0) || 0,
                cbal: 0,
                advamt: parseInt(ADVAMT) || 0,
                asid: ACSPLGUID.getNew(),
                isdeleted: false,
                companyid: userdetail?.companyID || "",
                deptid: userdetail?.departmentID || "",
                PAYMENTDT: userdetail?.APPDT || new Date().toISOString()
            });
            transectionPayloadList.push({
                traid: NewPKID,
                trnsr: 2,
                trnsid: "40",
                trntid: "20",
                trndt: userdetail?.APPDT || "",
                cropid: formData.Crop?.join(',') || "",
                refaid: SLVMID || GUID,
                maid: TREDER || "",
                pamt: parseFloat(formData?.salevalue || 0) || 0,
                cbal: parseFloat(formData?.salevalue || 0) || 0,
                advamt: parseInt(ADVAMT) || 0,
                asid: NewPKID,
                isdeleted: false,
                companyid: userdetail?.companyID || "",
                deptid: userdetail?.departmentID || "",
                PAYMENTDT: userdetail?.APPDT || new Date().toISOString()
            });

            if (
                transectionPayloadList.length > 0 &&
                transectionPayloadList.some(t => Number(t.pamt) > 0)
            ) {
                await axios.post(
                    `${baseUrl.Url}/backend/api/SP_AddUpdAllTransections`,
                    transectionPayloadList,
                    { headers: { 'Content-Type': 'application/json' } }
                );
            }

            let updatedRows = [...TableData1];
            for (const row of TableData1) {
                // const slvdidExists = updatedRows.some(item => item.ctdaid === row.ctdaid && item.ctdaid !== undefined);

                // if (slvdidExists) {
                //     updatedRows = updatedRows.map(item =>
                //         item.ctdaid === row.ctdaid
                //             ? {
                //                 ...item,
                //                 totcount: row.totcount,
                //                 totexpense: row.totexpense,
                //                 totamount: row.totamount,
                //             }
                //             : item
                //     );
                // } else {
                //     const newSlvdid = ACSPLGUID.getNew();
                //     row.ctdaid = newSlvdid;
                //     updatedRows.push({ ...row });
                // }
                const detailPayload = {
                    "ctdaid": row?.ctdaid || ACSPLGUID.getNew(),
                    "ctaid": SLVMID || GUID,
                    "slvmid": SLVMID,
                    "date": userdetail?.APPDT || "",
                    "crtcnt": row?.totcount || 0,
                    "examt": row?.totexpense || 0,
                    "iwdamt": row?.totamount || 0,
                    "cgrade": row?.cgrade || "",
                    "crop": row?.cropid || "",
                    "samt": row?.svalue || 0,
                };

                console.log(detailPayload, "detailPayload")
                const detailResponse = await axios.post(
                    `${baseUrl.Url}/backend/api/SP_AddUpdCropTransportInvoiceDetail`,
                    detailPayload,
                    { headers: { 'Content-Type': 'application/json' } }
                );
                if (detailResponse.status !== 200) throw new Error("Detail save failed");
                setTableData1(updatedRows);
                Swal.fire({
                    icon: 'success',
                    title: 'Success',
                    text: 'Data has been saved successfully!',
                    confirmButtonText: 'OK'
                }).then((result) => {
                    setTableData1([]);
                    if (onRefresh) onRefresh();
                    if (onSaved) onSaved();
                    if (result.isConfirmed) {
                        const modal = document.getElementById("AddTransportProcced");
                        if (modal) {
                            modal.classList.remove("show");
                            modal.style.display = "none";
                            modal.setAttribute("aria-hidden", "true");

                            const backdrops = document.querySelectorAll(".modal-backdrop");
                            backdrops.forEach((backdrop) => backdrop.parentNode.removeChild(backdrop));
                            document.body.classList.remove("modal-open");
                            document.body.style.overflow = "auto";
                            document.body.style.paddingRight = "";
                        }
                    }
                });
                setFormData({
                    Crop: [],
                    Grade: [],
                    Trader: "",
                    VehicleNumber: "",
                    CaretCount: "",
                });
                setTableData1([]);
            }

        } catch (error) {
            console.error("Save failed:", error);
            Swal.fire({
                icon: 'error',
                title: 'Error while saving',
                text: error.message || 'Server error'
            });
        }
    };
    useEffect(() => {
        const fetchTrader = async () => {
            try {
                const headers = {
                    "Content-Type": "application/json",
                    Accept: "*/*",
                };
                const payload = {
                    "ctype": "3",
                    "companyid": userdetail?.companyID ? userdetail.companyID : "",
                    "deptid": userdetail?.departmentID ? userdetail.departmentID : ""
                };
                const response = await axios.post(
                    `${baseUrl.Url}/backend/api/GET_TraderNameDropdown`,
                    payload,
                    { headers }
                );
                if (response.status !== 200)
                    throw new Error("Failed to fetch Employee data");

                const data = response.data;
                const TraderData = data.map(({ ccompanyname, caid }) => ({
                    label: ccompanyname,
                    value: caid
                }));
                setTrader(TraderData);
            } catch (error) {
                console.error("Error fetching Employee data:", error);
            }
        };
        fetchTrader();
    }, []);
    useEffect(() => {
        if (!SLVMID) {
            setFormData({
                Crop: [],
                Grade: [],
                Trader: "",
                VehicleNumber: "",
                CaretCount: "",
            });
            setTableData1([]);
        }
    }, [SLVMID]);
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
                    onRefresh();
                }
                if (onSaved) {
                    onSaved();
                }
                setFormData({
                    Crop: [],
                    Grade: [],
                    Trader: "",
                    VehicleNumber: "",
                    CaretCount: "",
                })
                setTableData1([])
                // const modal = document.getElementById("AddTransportProcced");
                // if (modal) {
                //     modal.classList.remove("show");
                //     modal.style.display = "none";
                //     modal.setAttribute("aria-hidden", "true");

                //     const backdrops = document.querySelectorAll(".modal-backdrop");
                //     backdrops.forEach((backdrop) => {
                //         backdrop.parentNode.removeChild(backdrop);
                //     });
                //     document.body.classList.remove("modal-open");
                //     document.body.style.overflow = "auto";
                //     document.body.style.paddingRight = "";
                // }


                const modalElement = document.getElementById("AddTransportProcced");
                if (modalElement) {
                    const modalInstance = bootstrap.Modal.getInstance(modalElement);
                    if (modalInstance) {
                        modalInstance.hide();
                        modalInstance.dispose();
                    }
                    document.querySelectorAll(".modal-backdrop").forEach((el) => el.remove());
                    document.body.classList.remove("modal-open");

                    document.body.style.overflow = "auto";
                    document.body.style.paddingRight = "";
                    modalElement.setAttribute("aria-hidden", "true");
                    modalElement.style.display = "none";
                }


            }
        });
    };

    const closeModal = () => {
        const modal = document.getElementById("AddTransportProcced");
        if (modal) {
            modal.classList.remove("show");
            modal.style.display = "none";
            modal.setAttribute("aria-hidden", "true");

            const backdrops = document.querySelectorAll(".modal-backdrop");
            backdrops.forEach((backdrop) => {
                backdrop.parentNode.removeChild(backdrop);
            });

            document.body.classList.remove("modal-open");
            document.body.style.overflow = "auto";
            document.body.style.paddingRight = "";
        }
        if (onRefresh) {
            onRefresh();
        }
    };
    window.addEventListener("popstate", () => {
        const modal = document.getElementById("AddTransportProcced");
        if (modal && modal.classList.contains("show")) {
            closeModal();
        }
    });
    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData((prev) => ({
            ...prev,
            [name]: value,
        }));
    };
    const handleTraderChange = (selectedOption) => {
        setFormData((prev) => ({
            ...prev,
            Trader: selectedOption?.value || "",
        }));
    };
    useEffect(() => {
        const modalEl = document.getElementById('AddTransportProcced');
        if (modalEl) {
            modalEl.addEventListener('shown.bs.modal', () => {
                if (Cropref.current) {
                    Cropref.current.focus();
                }
            });
        }
        return () => {
            if (modalEl) {
                modalEl.removeEventListener('shown.bs.modal', () => { });
            }
        };
    }, []);
    const [Gradeselect, setGrade] = useState([]);
    useEffect(() => {
        const fetchGrade = async () => {
            try {
                const response = await axios.get(
                    baseUrl.Url + "/backend/api/Implications/CGRADE",
                );
                if (response.status !== 200) throw new Error("Failed to fetch implications data");
                const data = response.data;
                const implicationsDropdown = data.map(({ iTitle, iValue }) => ({
                    label: iTitle,
                    value: iValue,
                }));
                setGrade(implicationsDropdown);
            } catch (error) {
                console.error("Error fetching implications:", error);
            }
        };
        fetchGrade();
    }, []);
    const [Cropselect, setCropselect] = useState([]);
    useEffect(() => {
        const fetchImplications = async () => {
            try {
                const headers = {
                    "Content-Type": "application/json",
                    Accept: "*/*",
                };
                const payload = {
                    "companyid": userdetail?.companyID ? userdetail.companyID : "",
                    "deptid": userdetail?.departmentID ? userdetail.departmentID : ""
                };
                const response = await axios.post(
                    `${baseUrl.Url}/backend/api/GETItemDropdown`,
                    payload,
                    { headers }
                );
                if (response.status !== 200) throw new Error("Failed to fetch Farmer data");
                const data = response.data;
                const implicationsDropdown = data.map(({ itemaid, itemne }) => ({
                    label: itemne,
                    value: itemaid,
                }));
                setCropselect(implicationsDropdown);
            } catch (error) {
                console.error("Error fetching Farmer data:", error);
            }
        };
        fetchImplications();
    }, []);
    const [TableData1, setTableData1] = useState([]);
    const handleRateChange = (e, row) => {
        const { name, value } = e.target;
        const inputCount = parseFloat(value) || 0;
        const updatedData = TableData1.map(item => {
            if (item === row) {
                const newCount = name === 'svalue' ? inputCount : parseFloat(item.svalue || 0);
                return {
                    ...item,
                    [name]: value,
                };
            }
            return item;
        });
        setTableData1(updatedData);
    };

    const fetchDetailData = async (crop, grade) => {
        try {
            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };
            const payload = {
                "companyid": userdetail?.companyID ? userdetail.companyID : "",
                "deptid": userdetail?.departmentID ? userdetail.departmentID : "",
                crop: Array.isArray(crop) ? crop.join(',') : "",
                cgrade: Array.isArray(grade) ? grade.join(',') : "",
            };
            const response = await axios.post(
                `${baseUrl.Url}/backend/api/GET_CropVehicleLoadingData`,
                payload,
                { headers }
            );
            if (response.status !== 200)
                throw new Error("Failed to fetch Employee data");
            setTableData1(response.data);
            console.log("respone table ", response.data)
        } catch (error) {
            console.error("Error fetching Detail table data:", error);
        }
    };

    useEffect(() => {
        let isMounted = true;
        const fetchTranData = async () => {
            try {
                const tranPayload = {
                    refaid: SLVMID || "",
                    maid: TREDER || "",
                    companyid: userdetail?.companyID || "",
                    deptid: userdetail?.departmentID || "",
                };

                const tranResponse = await axios.post(
                    `${baseUrl.Url}/backend/api/GET_CustomerTranData`,
                    tranPayload,
                    { headers: { "Content-Type": "application/json", Accept: "*/*" } }
                );

                if (isMounted) {
                    setDetailData(tranResponse.data || []);
                }
            } catch (error) {
                if (isMounted) {
                    console.error("❌ Error fetching customer transaction data:", error);
                }
            }
        };

        fetchTranData();

        return () => {
            isMounted = false;
        };
    }, [SLVMID, TREDER]);


    return (
        <>
            <div className="modal fade" id="AddTransportProcced">
                <div className="modal-dialog modal-dialog-centered modal-fullscreen">
                    <div className="modal-content mbgcolor">
                        <div className="page-wrapper-new p-0">
                            <div className="content">
                                <div className="modal-header border-0 custom-modal-header" style={{ padding: '5px' }}>
                                    <div className="page-title">
                                        <h4>Add Crop Transport Vehicle Bill</h4>
                                    </div>
                                    <div className="page-btn">
                                        <Link className="btn btn-secondary"
                                            onClick={showExitAlert}
                                        >
                                            <ArrowLeft className="me-2" />
                                            Back
                                        </Link>
                                    </div>
                                </div>
                                <div className="modal-body custom-modal-body" style={{
                                    overflow: "hidden",
                                    padding: '5px'
                                }}>
                                    <form onSubmit={handleSubmit}>
                                        <div
                                            id="collapseOne"
                                            className="accordion-collapse collapse show"
                                            aria-labelledby="headingOne"
                                            data-bs-parent="#accordionExample"
                                        >
                                            <div className="accordion-body">
                                                <div className="mb-0 add-product ">
                                                    <div className="row">
                                                        <div className="col-lg-2 col-sm-6 mb-3">
                                                            <label className="form-label required">Date</label>
                                                            <input
                                                                type="text"
                                                                className="form-control"
                                                                name="date"
                                                                required
                                                                value={formatDate_1(userdetail?.APPDT)}
                                                                readOnly
                                                            />
                                                        </div>
                                                        <div className="col-lg-4 col-md-6 col-12 mb-3">
                                                            <label className="form-label required">Crop</label>
                                                            <Select
                                                                openMenuOnFocus={true}
                                                                isMulti
                                                                ref={Cropref}
                                                                isDisabled={true}
                                                                required
                                                                placeholder="Please Select Crop"
                                                                classNamePrefix="react-select"
                                                                options={Cropselect}
                                                                value={Cropselect.filter(option => formData.Crop.includes(option.value))}

                                                                onChange={(selectedOptions) => {
                                                                    console.log("Crop changed:", selectedOptions); // ✅ check this
                                                                    const selectedValues = selectedOptions ? selectedOptions.map(opt => opt.value) : [];
                                                                    setFormData(prev => {
                                                                        const newForm = { ...prev, Crop: selectedValues };

                                                                        if (selectedValues.length > 0 && newForm.Grade.length > 0) {
                                                                            fetchDetailData(selectedValues, newForm.Grade);
                                                                        } else {
                                                                            fetchDetailData("", "");
                                                                        }
                                                                        return newForm;
                                                                    });
                                                                }}
                                                                styles={{
                                                                    control: (provided) => ({
                                                                        ...provided, minHeight: '60px', maxHeight: '150px', overflowY: 'auto', flexWrap: 'wrap',
                                                                    }),
                                                                    valueContainer: (provided) => ({
                                                                        ...provided, display: 'flex', flexWrap: 'wrap', maxHeight: '130px', overflowY: 'auto', scrollBehavior: 'smooth',
                                                                    }),
                                                                    multiValue: (provided) => ({
                                                                        ...provided, backgroundColor: '#e0f0ff', margin: '2px', borderRadius: '4px', padding: '2px 5px',
                                                                    }),
                                                                    menu: (provided) => ({ ...provided, zIndex: 9999, position: 'absolute' }),
                                                                    menuList: (provided) => ({
                                                                        ...provided,
                                                                        maxHeight: '150px',
                                                                        overflowY: 'auto',
                                                                    }),
                                                                }}
                                                            />
                                                        </div>
                                                        <div className="col-lg-4 col-md-6 col-12 mb-3">
                                                            <label className="form-label required">Grade</label>
                                                            <Select
                                                                ref={Graderef}
                                                                openMenuOnFocus={true}
                                                                isMulti
                                                                isDisabled={true}
                                                                placeholder="Please Select Grade"
                                                                classNamePrefix="react-select"
                                                                required
                                                                options={Gradeselect}
                                                                value={Gradeselect.filter(option => formData.Grade.includes(option.value))}
                                                                onChange={(selectedOptions) => {
                                                                    console.log("Grade changed:", selectedOptions); // ✅ check this
                                                                    const selectedValues = selectedOptions ? selectedOptions.map(opt => opt.value) : [];
                                                                    setFormData(prev => {
                                                                        const newForm = { ...prev, Grade: selectedValues };

                                                                        if (newForm.Crop.length > 0 && selectedValues.length > 0) {
                                                                            fetchDetailData(newForm.Crop, selectedValues);
                                                                        } else {
                                                                            fetchDetailData("", "");
                                                                        }
                                                                        return newForm;
                                                                    });
                                                                }}
                                                                styles={{
                                                                    control: (provided) => ({
                                                                        ...provided, minHeight: '60px', maxHeight: '150px', overflowY: 'auto', flexWrap: 'wrap',
                                                                    }),
                                                                    valueContainer: (provided) => ({
                                                                        ...provided, display: 'flex', flexWrap: 'wrap', maxHeight: '130px', overflowY: 'auto', scrollBehavior: 'smooth',
                                                                    }),
                                                                    multiValue: (provided) => ({
                                                                        ...provided, backgroundColor: '#e0f0ff', margin: '2px', borderRadius: '4px', padding: '2px 5px',
                                                                    }),
                                                                    menu: (provided) => ({ ...provided, zIndex: 9999, position: 'absolute' }),
                                                                    menuList: (provided) => ({
                                                                        ...provided,
                                                                        maxHeight: '150px',
                                                                        overflowY: 'auto',
                                                                    }),
                                                                }}
                                                                title="Please Select Grade"
                                                            />
                                                        </div>
                                                        <div className="col-lg-4 col-md-6 col-12 mb-3">
                                                            <label htmlFor="serviceName" className="form-label required">
                                                                Customer Name
                                                            </label>
                                                            <Select
                                                                ref={Traderref}
                                                                isDisabled={true}
                                                                openMenuOnFocus={true}
                                                                required
                                                                classNamePrefix="react-select"
                                                                placeholder="Select Trader"
                                                                options={Trader}
                                                                value={Trader.find(option => option.value === formData.Trader) || null}
                                                                onChange={handleTraderChange}
                                                                styles={{
                                                                    control: (provided) => ({
                                                                        ...provided, minHeight: '60px', maxHeight: '150px', overflowY: 'auto', flexWrap: 'wrap',
                                                                    }),
                                                                    valueContainer: (provided) => ({
                                                                        ...provided, display: 'flex', flexWrap: 'wrap', maxHeight: '130px', overflowY: 'auto', scrollBehavior: 'smooth',
                                                                    }),
                                                                    multiValue: (provided) => ({
                                                                        ...provided, backgroundColor: '#e0f0ff', margin: '2px', borderRadius: '4px', padding: '2px 5px',
                                                                    }),
                                                                    menu: (provided) => ({ ...provided, zIndex: 9999, position: 'absolute' }),
                                                                    menuList: (provided) => ({
                                                                        ...provided,
                                                                        maxHeight: '150px',
                                                                        overflowY: 'auto',
                                                                    }),
                                                                }}
                                                            />
                                                        </div>

                                                        <div className="col-lg-2  col-md-6 mb-3 col-6">
                                                            <label htmlFor="Vehicle Number" className="form-label required">Vehicle Number</label>
                                                            <input
                                                                ref={VehicleNumberref}
                                                                placeholder="Enter Vehicle Number "
                                                                type="text"
                                                                id="Vehicle Number"
                                                                className="form-control"
                                                                required
                                                                name="VehicleNumber"
                                                                value={formData.VehicleNumber}
                                                                onChange={handleChange}
                                                                readOnly
                                                            />
                                                        </div>
                                                        <div className="col-lg-2  col-md-6 mb-3 col-6">
                                                            <label htmlFor="Caret Count" className="form-label">Crate Count</label>
                                                            <input
                                                                placeholder="Enter Caret count"
                                                                ref={CaretCountref}
                                                                type="number"
                                                                id="Caret Count"
                                                                className="form-control"
                                                                name="CaretCount"
                                                                value={formData.CaretCount}
                                                                onChange={handleChange}
                                                                readOnly
                                                            />
                                                        </div>

                                                        <div className="col-lg-2  col-md-6 mb-3 col-6">
                                                            <label htmlFor="salevalue" className="form-label">Total Sale's Value</label>
                                                            <input
                                                                placeholder="Enter Caret count"
                                                                ref={salevalueref}
                                                                type="number"
                                                                id="Caret Count"
                                                                className="form-control"
                                                                name="salevalue"
                                                                value={formData.salevalue}
                                                                onChange={handleChange}
                                                                readOnly={STATUS === "COMPLETED"}
                                                            />
                                                        </div>
                                                        <div className="col-lg-2  col-md-6 mb-3 col-6">
                                                            <label htmlFor="Advance" className="form-label">Advance Amount</label>
                                                            <input
                                                                placeholder="Enter Caret count"
                                                                type="number"
                                                                id="Advance"
                                                                className="form-control"
                                                                name="Advance"
                                                                value={formData.Advance}
                                                                readOnly
                                                            />
                                                        </div>

                                                    </div>

                                                    {advanceInfo && (
                                                        <div className="col-lg-12">
                                                            <div className="alert alert-success text-center fw-bold">
                                                                <span style={{ color: 'black' }}>Advance of ₹{advanceInfo.amount} already given to this farmer on {advanceInfo.date}.</span>
                                                            </div>
                                                        </div>
                                                    )}
                                                </div>
                                                <div className="border p-3 rounded shadow-sm mb-1 mt-2">
                                                    <div className="row">
                                                        <div className="col-lg-12">
                                                            <div className="modal-body-table">
                                                                <div className="table-responsive" style={{ maxHeight: "325px", overflowY: "auto" }}>
                                                                    <table className="table table-bordered datanew" style={{ tableLayout: "fixed", width: "100%" }}>
                                                                        <thead className="thead-dark" style={{
                                                                            position: "sticky",
                                                                            top: 0,
                                                                            zIndex: 1,
                                                                            backgroundColor: "#343a40",
                                                                            color: "white"
                                                                        }}>
                                                                            <tr>
                                                                                <th className="text-center">Crop</th>
                                                                                <th className="text-center">Grade</th>
                                                                                <th className="text-center">Total Crate</th>
                                                                                <th className="text-center">Total Expense</th>
                                                                                <th className="text-center">Total Inward Amount</th>
                                                                                {/* <th className="text-center">Sale's Value</th> */}
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            {
                                                                                TableData1.length > 0 ? (
                                                                                    TableData1.map((row, index) => (
                                                                                        <tr key={index}>
                                                                                            <td className="text-center">{row.itemname || index}</td>
                                                                                            <td className="text-center">{row.grade}</td>
                                                                                            <td className="text-center">{row.totcount}</td>
                                                                                            <td className="text-center">
                                                                                                ₹ {row.totexpense ? row.totexpense.toFixed(2) : "0.00"}
                                                                                            </td>
                                                                                            <td className="text-center">
                                                                                                ₹ {row.totamount ? row.totamount.toFixed(2) : "0.00"}
                                                                                            </td>
                                                                                            {/* <td className="text-center">
                                                                                                <input
                                                                                                    min={0}
                                                                                                    type="number"
                                                                                                    name="svalue"
                                                                                                    className="form-control text-center"
                                                                                                    value={row.svalue || 0}
                                                                                                    onChange={(e) => handleRateChange(e, row)}
                                                                                                />
                                                                                            </td> */}
                                                                                        </tr>
                                                                                    ))
                                                                                ) : (
                                                                                    <tr>
                                                                                        <td colSpan="5" className="text-center">No Data Available</td>
                                                                                    </tr>
                                                                                )
                                                                            }
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div className="col-md-12 d-flex justify-content-end ">
                                                            <div className="d-flex justify-content-end me-2">
                                                                <div
                                                                    className="p-1 rounded text-center fw-bold"
                                                                    style={{
                                                                        backgroundColor: "rgb(224 231 228)",
                                                                        fontSize: "15px",
                                                                        color: "black",
                                                                        border: "2px solid #343a40",
                                                                        minWidth: "220px" // optional: keeps it neat
                                                                    }}
                                                                >
                                                                    Total Expense:{" "}
                                                                    ₹ {TableData1
                                                                        .filter(row => parseInt(row.isdeleted || 0) !== 1)
                                                                        .reduce((sum, row) => sum + (parseFloat(row.totexpense) || 0), 0)
                                                                        .toFixed(2)}
                                                                </div>
                                                            </div>
                                                            <div className="d-flex justify-content-end">
                                                                <div
                                                                    className="p-1 rounded text-center fw-bold"
                                                                    style={{
                                                                        backgroundColor: "rgb(224 231 228)",
                                                                        fontSize: "15px",
                                                                        color: "black",
                                                                        border: "2px solid #343a40",
                                                                        minWidth: "220px" // optional: keeps it neat
                                                                    }}
                                                                >
                                                                    Total Inward :{" "}
                                                                    ₹ {TableData1
                                                                        .filter(row => parseInt(row.isdeleted || 0) !== 1)
                                                                        .reduce((sum, row) => sum + (parseFloat(row.totamount) || 0), 0)
                                                                        .toFixed(2)}
                                                                </div>
                                                            </div>
                                                        </div>

                                                        {/* <div style={{
                                                            display: 'flex',
                                                            flexDirection: isMobile ? 'column' : 'row',
                                                            justifyContent: 'space-between',
                                                            alignItems: isMobile ? 'flex-start' : 'center',
                                                            backgroundColor: '#d3d3d3',
                                                            padding: '10px 20px',
                                                            marginTop: '10px',
                                                            borderRadius: '4px',
                                                            gap: isMobile ? '12px' : '0px'
                                                        }}>
                                                            <div style={{ display: 'flex', gap: '12px' }}>
                                                            </div>
                                                            <div style={{
                                                                display: 'flex',
                                                                flexDirection: isMobile ? 'column' : 'row',
                                                                gap: '10px',
                                                                width: isMobile ? '100%' : 'auto'
                                                            }}>
                                                                {/* <div style={{
                                                                    fontWeight: 'bold',
                                                                    border: '1px solid #ccc',
                                                                    padding: '6px 16px',
                                                                    borderRadius: '4px',
                                                                    backgroundColor: '#f9f9f9',
                                                                    minWidth: '140px',
                                                                    textAlign: 'right',
                                                                    width: isMobile ? '100%' : 'auto'
                                                                }}>
                                                                    ₹ {TableData1
                                                                        .filter(row => parseInt(row.isdeleted || 0) !== 1)
                                                                        .reduce((sum, row) => sum + (parseFloat(row.totexpense) || 0), 0)
                                                                        .toFixed(2)} (Expense)
                                                                </div> */}
                                                        {/* <div style={{
                                                                    fontWeight: 'bold',
                                                                    border: '1px solid #ccc',
                                                                    padding: '6px 16px',
                                                                    borderRadius: '4px',
                                                                    backgroundColor: '#f9f9f9',
                                                                    minWidth: '140px',
                                                                    textAlign: 'right',
                                                                    width: isMobile ? '100%' : 'auto'
                                                                }}>
                                                                    ₹ {TableData1
                                                                        .filter(row => parseInt(row.isdeleted || 0) !== 1)
                                                                        .reduce((sum, row) => sum + (parseFloat(row.totamount) || 0), 0)
                                                                        .toFixed(2)} (Total Inward)
                                                                </div> */}
                                                        {/* <div style={{
                                                                    fontWeight: 'bold',
                                                                    border: '1px solid #ccc',
                                                                    padding: '6px 16px',
                                                                    borderRadius: '4px',
                                                                    backgroundColor: '#f9f9f9',
                                                                    minWidth: '140px',
                                                                    textAlign: 'right',
                                                                    width: isMobile ? '100%' : 'auto'
                                                                }}>
                                                                    ₹ {TableData1
                                                                        .filter(row => parseInt(row.isdeleted || 0) !== 1)
                                                                        .reduce((sum, row) => sum + (parseFloat(row.svalue) || 0), 0)
                                                                        .toFixed(2)} (Total sale's)
                                                                </div> 
                                                            </div>
                                                        </div> */}
                                                    </div>


                                                    <div className="row mt-3">
                                                        <div className="col-lg-12">
                                                            <div className="modal-body-table">
                                                                <div className="table-responsive" style={{ maxHeight: "300px", overflowY: "auto" }}>
                                                                    <table className="table table-bordered table-sm mb-0">
                                                                        <thead
                                                                            className="thead-dark"
                                                                            style={{
                                                                                position: "sticky",
                                                                                top: 0,
                                                                                backgroundColor: "#343a40",
                                                                                color: "white",
                                                                                zIndex: 1000,
                                                                            }}
                                                                        >
                                                                            <tr>
                                                                                <th className="text-center">Date</th>
                                                                                <th className="text-center">Narration</th>
                                                                                <th className="text-center">Receipt Amount</th>
                                                                            </tr>
                                                                        </thead>

                                                                        <tbody>
                                                                            {detailData.map((row) => (
                                                                                <tr key={row.traid}>
                                                                                    <td className="text-center">{row.trndt}</td>
                                                                                    <td className="text-center">{row.narr}</td>
                                                                                    <td className="text-center">{row.pamt}</td>
                                                                                </tr>
                                                                            ))}
                                                                        </tbody>
                                                                    </table>
                                                                </div>


                                                            </div>
                                                            <div className="col-md-12">
                                                                <div className="d-flex justify-content-end">
                                                                    <div
                                                                        className="p-1 rounded text-center fw-bold"
                                                                        style={{
                                                                            backgroundColor: "rgb(224 231 228)",
                                                                            fontSize: "15px",
                                                                            color: "black",
                                                                            border: "2px solid #343a40",
                                                                            minWidth: "220px" // optional: keeps it neat
                                                                        }}
                                                                    >
                                                                        Total Recive Amount:{" "}
                                                                        ₹ {detailData.reduce((sum, row) => sum + Number(row.pamt || 0), 0)}
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div className="col-lg-12">
                                                    <div className="text-end">
                                                        <div className="row justify-content-end">
                                                            <div className="col-12 col-md-1 mb-2 order-1 order-md-2">
                                                                <button
                                                                    type="button"
                                                                    className="btn btn-submit w-100"
                                                                    ref={SaveRef} onClick={handleFinalSave}
                                                                    disabled={STATUS === "COMPLETED"}
                                                                >
                                                                    Save
                                                                </button>
                                                            </div>

                                                            <div className="col-12 col-md-1 mb-2 order-2 order-md-1">
                                                                <button
                                                                    type="button"
                                                                    className="btn btn-cancel w-100"
                                                                    onClick={showExitAlert}
                                                                >
                                                                    Back
                                                                </button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </form>
                                </div>
                            </div >
                        </div >
                    </div >
                </div >
            </div >
        </>
    );
};
export default CropTransportInvoiceProceed;

