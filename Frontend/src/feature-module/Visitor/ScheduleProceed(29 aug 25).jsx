import React, { useRef, useEffect, useState } from "react";
import { Link } from "react-router-dom";
import Select from "react-select";
import { all_routes } from "../../Router/all_routes";
import withReactContent from "sweetalert2-react-content";
import Swal from "sweetalert2";
import axios from 'axios';
import { baseUrl, convertToISODate } from "../../core/json/custom";
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
const ScheduleProceed = ({ VAID, SHAID, onRefresh, ID }) => {
    console.log("VAIDVAIDVAID", VAID, SHAID, ID);
    const { userdetail } = getUserData();
    const [EndDate, setEndDate] = useState('');
    const [StartDate, setStartDate] = useState('');
    const [editIndex, setEditIndex] = useState(null);
    const [Employee, setEmployee] = useState([]);
    const [packingsize, setpackingsize] = useState([]);
    const [Farmer, setFarmer] = useState([]);
    const [cropType, setCropType] = useState([]);
    const MySwal = withReactContent(Swal);
    useEffect(() => {
        getTodayDate();
    }, [VAID]);
    const getTodayDate = () => {
        const today = new Date();
        const formatted = today.toLocaleDateString("en-GB", {
            day: "2-digit",
            month: "short",
            year: "numeric",
        });
        setEndDate(formatted);
        console.log('formattedformattedformattedformatted', formatted)
    };
    const [TableData, setTableData] = useState([]);
    const [formData, setFormData] = useState({
        FarmerName: "",
        PhoneNo: "",
        CropName: "",
        CropArea: "",
        ExpectedHarvesting: "",
        CropVariety: "",
        ctype: ""
    });
    const [detailData, setDetailData] = useState({
        ScheduleDate: "",
        packer: "",
        labour: "",
        VehicleNo: "",
        crateCount: "",
        packingMaterial: "",
        isdeleted: 0,
        shaid: "",
        shdaid: ""
    });
    const GUID = ACSPLGUID.getNew();
    const totalAmount = TableData.reduce((sum, row) => sum + (parseFloat(row.total) || 0), 0).toFixed(2);
    const RefOutWardStockWeight = useRef(null);
    const RefNaration = useRef(null);
    const Refvyavarmode = useRef(null)
    const SaveRef = useRef(null);
    useEffect(() => {
        if (VAID != '') {
            const fetchData = async () => {
                try {
                    const payload1 = {
                        vaid: VAID,
                        shaid: SHAID,
                        companyid: userdetail?.companyID ? userdetail.companyID : "",
                        deptid: userdetail?.departmentID ? userdetail.departmentID : "",
                    };
                    const headers = {
                        "Content-Type": "application/json",
                        Accept: "*/*",
                    };
                    const response = await axios.post(
                        `${baseUrl.Url}/backend/api/GET_ShedulerDetailData`,
                        payload1,
                        { headers }
                    );
                    console.log(response.data, "response.data")
                    if (response.data.length > 0) {
                        const mappedProducts = response.data.map((item) => ({
                            ScheduleDate: convertToISODate(item?.date || ""),
                            VehicleNo: item?.vehno || "",
                            packer: item?.packer || "",
                            labour: item?.worker || "",
                            packingMaterial: item?.size || "",
                            crateCount: item?.pquntity || "",
                            shaid: item?.shaid || "",
                            shdaid: item?.shdaid || "",
                            isdeleted: 0
                        }));
                        console.log("Mapped Table Data on proceed", mappedProducts);
                        setTableData(mappedProducts);
                    }
                } catch (error) {
                    console.error("Error fetching data:", error);
                }
            };
            fetchData();
        }
    }, [VAID])
    useEffect(() => {
        const modal = document.getElementById('add-units-category');
        const handleShown = () => {
            RefOutWardStockWeight.current?.focus();
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
        if (!formData.FarmerName || !formData.CropName || !formData.CropArea || !formData.ExpectedHarvesting || !formData.CropVariety) {
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'Please complete all the master data.'
            });
            return;
        }
        const activeDetails = TableData.filter(row => parseInt(row.isdeleted) !== 1);
        if (activeDetails.length === 0) {
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'Please add at least one detail record.'
            });
            return;
        }
        try {
            const masterPayload = {
                shaid: SHAID ? SHAID : GUID,
                faid: formData.FarmerName,
                ctype: formData.ctype,
                carea: formData.CropArea,
                vaid: VAID,
                vdaid: ID,
                shdate: TodaysDate,
                narr: formData.Narration || "",
                iscomplited: '1',
                companyid: userdetail?.companyID ? userdetail.companyID : "",
                deptid: userdetail?.departmentID ? userdetail.departmentID : "",
            };
            console.log(masterPayload, "masterPayload masterPayload")
            const masterResponse = await axios.post(
                `${baseUrl.Url}/backend/api/SP_AddUpdShedulerMaster`,
                masterPayload,
                { headers: { 'Content-Type': 'application/json' } }
            );
            if (masterResponse.status !== 200) {
                throw new Error("Master save failed");
            }
            console.log("Master Saved Successfully", TableData);
            for (const row of TableData) {
                const detailPayload = [{
                    shdaid: row?.shdaid || "",
                    shaid: SHAID ? SHAID : GUID,
                    shsdate: row.ScheduleDate,
                    vdaid: VAID,
                    packer: row.packer,
                    worker: row.labour,
                    vehno: row.VehicleNo,
                    psize: row.packingMaterial,
                    pquntity: row.crateCount,
                    isdeleted: row.isdeleted === 1 ? true : false,
                    cgrade: ""
                }];
                console.log(detailPayload, "detailPayload detailPayload")
                const detailResponse = await axios.post(
                    `${baseUrl.Url}/backend/api/SP_AddUpdShedulerDetail`,
                    detailPayload,
                    { headers: { 'Content-Type': 'application/json' } }
                );
                if (detailResponse.status !== 200) {
                    throw new Error("Detail save failed for row: " + row.ScheduleDate);
                }
            }
            Swal.fire({
                icon: 'success',
                title: 'Success',
                text: 'Data has been saved successfully!',
                confirmButtonText: 'OK'
            }).then((result) => {
                if (result.isConfirmed) {
                    const modal = document.getElementById("add-units-category");
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
                }
            });
            setDetailData({
                ScheduleDate: "",
                packer: "",
                labour: "",
                VehicleNo: "",
                crateCount: "",
                packingMaterial: "",
                shaid: "",
                shdaid: "",
                isdeleted: 0
            });
        } catch (error) {
            console.error("Save failed:", error);
            Swal.fire({
                icon: 'error',
                title: 'Error while saving',
                text: error.message || 'Server error'
            });
        }
    };
    const closeModal = () => {
        const modal = document.getElementById("add-units-category");
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
        const modal = document.getElementById("add-units-category");
        if (modal && modal.classList.contains("show")) {
            closeModal();
        }
    });
    const getTodayDate1 = () => {
        const today = new Date();
        const yyyy = today.getFullYear();
        const mm = String(today.getMonth() + 1).padStart(2, '0');
        const dd = String(today.getDate()).padStart(2, '0');
        return `${yyyy}-${mm}-${dd}`;
    };
    const TodaysDate = getTodayDate1()
    useEffect(() => {
        if (StartDate && EndDate) {
            const start = new Date(StartDate);
            const end = new Date(EndDate);
            const diffTime = end.getTime() - start.getTime();
            const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));
            formData((prev) => ({
                ...prev,
                TotalDays: diffDays >= 0 ? diffDays : 0,
            }));
        }
    }, [StartDate, EndDate]);
    const handleChange = async (e) => {
        const { name, value } = e.target;
        if (name === "OutWardStockWeight") {
            const net = parseFloat(formData.WeightInkg) || 0;
            const out = parseFloat(value) || 0;
            let remaining = net - out;
            if (remaining < 0) remaining = 0;
            if (out > net) {
                MySwal.fire({
                    text: 'Outward वजन हे एकूण वजनापेक्षा जास्त असू शकत नाही.',
                    icon: 'error',
                    confirmButtonColor: '#00ff00',
                    confirmButtonText: 'ठीक आहे',
                });
                return;
            }
            if (!value || value.trim() === "") {
                setFormData(prev => ({
                    ...prev,
                    OutWardStockWeight: "",
                    RemainingStockWeight: "",
                }));
                setTableData([]);
                return;
            }
            const updateddata = {
                ...formData,
                OutWardStockWeight: value,
                RemainingStockWeight: remaining === 0 ? "00" : remaining.toFixed(2),
            };
            setFormData(updateddata);
            await fetchDataTable(value);
        } else {
            setFormData({
                ...formData,
                [name]: value,
            });
        }
    };
    const handleDetailsave = (e) => {
        validateinput(e);
    }
    const handleAddRow = (e) => {
        const selectedPackerLabels = Employee
            .filter(opt => detailData.packer?.includes(opt.value))
            .map(opt => opt.value)
            .join(", ");

        const selectedLabourLabels = Employee
            .filter(opt => detailData.labour?.includes(opt.value))
            .map(opt => opt.value)
            .join(", ");
        const newRow = {
            ScheduleDate: detailData.ScheduleDate,
            packer: selectedPackerLabels,
            labour: selectedLabourLabels,
            VehicleNo: detailData.VehicleNo,
            crateCount: detailData.crateCount,
            packingMaterial: detailData.packingMaterial,
            shaid: SHAID ? SHAID : ACSPLGUID.getNew(),
            shdaid: detailData?.shdaid ? detailData.shdaid : ACSPLGUID.getNew()
        };
        if (editIndex !== null) {
            setTableData(prev => {
                const updated = [...prev];
                updated[editIndex] = newRow;
                return updated;
            });
            setEditIndex(null);
        } else {
            setTableData(prev => [...prev, newRow]);
        }
        setDetailData({
            ScheduleDate: "",
            packer: "",
            labour: "",
            VehicleNo: "",
            crateCount: "",
            packingMaterial: "",
            shaid: "",
            shdaid: "",
            isdeleted: 0
        });
    };
    const handleDelete = (row) => {
        setTableData(prev =>
            prev.map(item =>
                item.shdaid === row.shdaid
                    ? { ...item, isdeleted: 1 }
                    : item
            )
        );
    };
    const ScheduleDateref = useRef(null);
    const packerRef = useRef(null);
    const labourRef = useRef(null);
    const VehicleNoRef = useRef(null);
    const CrateCountRef = useRef(null);
    const PackingMaterialRef = useRef(null);
    const Addref = useRef(null);
    const [swalActive, setSwalActive] = useState(false);
    const showValidationSwal = (message, refToFocus) => {
        setSwalActive(true);
        Swal.fire({
            icon: "error",
            title: "Error",
            text: message,
            confirmButtonText: "OK",
            allowEnterKey: true,
            allowOutsideClick: false,
            allowEscapeKey: true,
            focusConfirm: true,
            willClose: () => {
                setSwalActive(false);
                setTimeout(() => {
                    refToFocus?.current?.focus();
                }, 0);
            }
        });
    };
    const validateinput = async (e) => {
        const { ScheduleDate, packer, labour, VehicleNo, crateCount, packingMaterial } = detailData;
        if (!ScheduleDate || !/^\d{4}-\d{2}-\d{2}$/.test(ScheduleDate)) {
            showValidationSwal("Please enter a valid date.", ScheduleDateref);
            return;
        }
        const inputDate = new Date(ScheduleDate);
        const today = new Date();
        today.setHours(0, 0, 0, 0);
        if (inputDate < today) {
            showValidationSwal("Past dates are not allowed. Please select today or a future date.", ScheduleDateref);
            return;
        }
        if (!VehicleNo || !/^[A-Z]{2}\d{1,2}[A-Z]{1,3}\d{1,4}$/.test(VehicleNo)) {
            showValidationSwal("Please enter a valid vehicle number. eg: MH12FG1234", VehicleNoRef);
            return;
        }
        if (!packer || packer.length === 0) {
            showValidationSwal("Please select a packer.", packerRef);
            return;
        }
        if (!labour || labour.length === 0) {
            showValidationSwal("Please select labour.", labourRef);
            return;
        }
        if (!packingMaterial) {
            showValidationSwal("Please select Packing Size.", PackingMaterialRef);
            return;
        }
        if (!crateCount || !/^\d{1,4}$/.test(crateCount)) {
            showValidationSwal("Invalid Packing Quantity.", CrateCountRef);
            return;
        }
        handleAddRow(e);
    };
    const handleEdit = (row, index) => {
        setDetailData({
            ScheduleDate: row.ScheduleDate,
            packer: row.packer,
            labour: row.labour,
            VehicleNo: row.VehicleNo,
            crateCount: row.crateCount,
            packingMaterial: row.packingMaterial,
            shaid: row.shaid,
            shdaid: row.shdaid,
            isdeleted: 0
        });
        setEditIndex(index);
    };
    useEffect(() => {
        if (VAID != "") {
            const fetchData = async () => {
                try {
                    const payload1 = {
                        vaid: VAID,
                        companyid: userdetail?.companyID || "",
                        deptid: userdetail?.departmentID || "",
                        keyword: "%",
                        date: '19 jun 2025'
                    };
                    const headers = {
                        "Content-Type": "application/json",

                        Accept: "*/*",
                    };
                    const response1 = await axios.post(
                        `${baseUrl.Url}/backend/api/GET_ShedulerMasterData`,
                        payload1,
                        { headers }
                    );
                    if (response1.status !== 200)
                        throw new Error("Failed to fetch SchedulerMaster data");
                    if (response1.data.length > 0) {
                        const data = response1.data[0];
                        console.log(response1.data, "response1.dataresponse1.data")
                        setFormData({
                            FarmerName: data.faid,
                            PhoneNo: data.contactno,
                            CropName: data.cropname,
                            ctype: data.ctype,
                            CropArea: data.area,
                            ExpectedHarvesting: data.eproduction,
                            CropVariety: data.cvariety
                        });
                        setStartDate(data.date);
                    }
                } catch (error) {
                    console.error("Error fetching data:", error);
                }
            };
            fetchData(VAID);
        }
    }, [VAID]);
    useEffect(() => {
        const fetchpackingdrop = async () => {
            try {
                const headers = {
                    "Content-Type": "application/json",
                    Accept: "*/*",
                };
                const response = await axios.post(
                    `${baseUrl.Url}/backend/api/GET_PackingMaterialDropdown`,
                    { headers }
                );
                if (response.status !== 200)
                    throw new Error("Failed to fetch Farmer data");
                console.log("packing material ", response.data);
                const options = response.data.map(({ packingMaterial, maid }) => ({
                    label: packingMaterial,
                    value: maid,
                }));
                setpackingsize(options)
            } catch (error) {
                console.error("Error fetching Farmer data:", error);
            }
        };
        fetchpackingdrop();
    }, []);
    useEffect(() => {
        const fetchFarmer = async () => {
            try {
                const headers = {
                    "Content-Type": "application/json",
                    Accept: "*/*",
                };
                const payload = { companyid: "" };
                const response = await axios.post(
                    `${baseUrl.Url}/backend/api/GET_FarmerName`,
                    payload,
                    { headers }
                );
                if (response.status !== 200)
                    throw new Error("Failed to fetch Farmer data");
                console.log("Farmer Detail", response.data);
                const options = response.data.map(({ fname, faid }) => ({
                    label: fname,
                    value: faid,
                }));
                setFarmer(options);
            } catch (error) {
                console.error("Error fetching Farmer data:", error);
            }
        };
        fetchFarmer();
    }, []);
    useEffect(() => {
        const fetchEmployee = async () => {
            try {
                const headers = {
                    "Content-Type": "application/json",
                    Accept: "*/*",
                };
                const payload = {
                    "companyid": userdetail?.companyID || "",
                    "deptid": userdetail?.departmentID || "",
                };
                const response = await axios.post(
                    `${baseUrl.Url}/backend/api/GET_EMPLOYEE`,
                    payload,
                    { headers }
                );
                if (response.status !== 200)
                    throw new Error("Failed to fetch Employee data");
                const data = response.data;
                const EmployeeData = data.map(({ empname, empid }) => ({
                    label: empname,
                    value: empid
                }));
                setEmployee(EmployeeData);
            } catch (error) {
                console.error("Error fetching Employee data:", error);
            }
        };
        fetchEmployee();
    }, []);
    useEffect(() => {
        const fetchImplications = async () => {
            try {
                const headers = {
                    "Content-Type": "application/json",
                    Accept: "*/*",
                };
                const payload = { "companyid": "", "deptid": "" };
                const response = await axios.post(
                    `${baseUrl.Url}/backend/api/GETItemDropdown`,
                    payload,
                    { headers }
                );
                if (response.status !== 200) throw new Error("Failed to fetch Farmer data");
                const data = response.data;
                const implicationsDropdown = data.map(({ itemaid, itemnm }) => ({
                    label: itemnm,
                    value: itemaid,
                }));
                setCropType(implicationsDropdown);
            } catch (error) {
                console.error("Error fetching Farmer data:", error);
            }
        };
        fetchImplications();
    }, []);
    useEffect(() => {
        if (!VAID || !SHAID) {
            setDetailData({
                ScheduleDate: "",
                packer: "",
                labour: "",
                VehicleNo: "",
                crateCount: "",
                packingMaterial: "",
            });
            setTableData([]);
        }
    }, [VAID, SHAID]);
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
                setDetailData({
                    ScheduleDate: "",
                    packer: "",
                    labour: "",
                    VehicleNo: "",
                    crateCount: "",
                    packingMaterial: "",
                });
                const modal = document.getElementById("add-units-category");
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
            }
        });
    };
    const handleKeyDown = (e, nextRef, isLastField = false) => {
        if (e.key === "Enter") {
            e.preventDefault();
            if (isLastField) {
                Addref.current?.click();
            } else {
                nextRef?.current?.focus();
            }
        }
    };
    useEffect(() => {
        const modalEl = document.getElementById('add-units-category');
        if (modalEl) {
            modalEl.addEventListener('shown.bs.modal', () => {
                if (ScheduleDateref.current) {
                    ScheduleDateref.current.focus();
                }
            });
        }
        return () => {
            if (modalEl) {
                modalEl.removeEventListener('shown.bs.modal', () => { });
            }
        };
    }, []);

    return (
        <>
            <div
                div className="modal fade"
                id="add-units-category"
                tabIndex={-1}
                aria-labelledby="exampleModalFullscreenLabel"
                aria-hidden="true"
                style={{ display: "none" }}
            >
                <div className="modal-dialog modal-dialog-centered modal-fullscreen">
                    <div className="modal-content mbgcolor">
                        <div className="page-wrapper-new p-0">
                            <div className="content">
                                <div className="modal-header border-0 custom-modal-header" style={{ padding: '5px' }}>
                                    <div className="page-title">
                                        <h4>Scheduler</h4>
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
                                                <div className="row">
                                                    <div className="col-lg-2  col-md-6 mb-3 col-6">
                                                        <label htmlFor="StartDate" className="form-label required">Harvest date</label>
                                                        <input
                                                            type="date"
                                                            className="form-control  "
                                                            name="StartDate"
                                                            value={TodaysDate}
                                                            readOnly
                                                            required
                                                        />
                                                    </div>
                                                    <div className="col-lg-2  col-md-6 mb-3 col-6">
                                                        <label htmlFor="PhoneNo" className="form-label">Phone.No</label>
                                                        <input
                                                            type="text"
                                                            id="PhoneNo"
                                                            className="form-control"
                                                            name="PhoneNo"
                                                            value={formData.PhoneNo}
                                                            readOnly
                                                        />
                                                    </div>
                                                    <div className="col-lg-4  col-md-6 mb-3">
                                                        <label htmlFor="shopName" className="form-label required">Farmer Name</label>
                                                        <input
                                                            type="text"
                                                            className="form-control"
                                                            name="FarmerName"
                                                            value={
                                                                Farmer.find(option => option.value === formData.FarmerName)?.label || ''
                                                            }
                                                            readOnly
                                                            required
                                                        />
                                                    </div>
                                                    <div className="col-lg-2  col-md-6 mb-3 col-6">
                                                        <label htmlFor="CropName" className="form-label ">Crop</label>
                                                        <input
                                                            type="text"
                                                            className="form-control"
                                                            name="CropName"
                                                            value={formData.CropName}
                                                            readOnly
                                                        />
                                                    </div>
                                                    <div className="col-lg-2  col-md-6 mb-3 col-6">
                                                        <div className="mb-0 add-product form-label">
                                                            <div className="d-flex justify-content-between align-items-center">
                                                                <label htmlFor="shopLocation" className="form-label required"> Area</label>
                                                                <Link
                                                                    to="#"
                                                                    data-bs-toggle="modal"
                                                                    data-bs-target="#add-units-category"
                                                                    className="ms-2"
                                                                >
                                                                </Link>
                                                            </div>
                                                            <div className="position-relative">
                                                                <input
                                                                    id="CropArea"
                                                                    type="text"
                                                                    className="form-control"
                                                                    placeholder=" तीसरा पक्ष नाव प्रविष्ट करा"
                                                                    name="ThirdPartyName"
                                                                    onChange={handleChange}
                                                                    value={formData.CropArea}
                                                                    readOnly
                                                                />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div className="row">
                                                    <div className="col-lg-2 col-md-6 mb-3 col-6">
                                                        <label htmlFor="ExpectedHarvesting" className="form-label ">Expected Production</label>

                                                        <input
                                                            type="text"
                                                            className="form-control"
                                                            name="ExpectedHarvesting"
                                                            value={
                                                                formData.ExpectedHarvesting
                                                            }
                                                            readOnly
                                                        />
                                                    </div>

                                                    <div className="col-lg-3 col-md-6 mb-3 col-6">
                                                        <label htmlFor="CropVariety" className="form-label">Variety</label>
                                                        <input
                                                            id="CropVariety"
                                                            type="text"
                                                            className="form-control"
                                                            name="CropVariety"
                                                            onChange={handleChange}
                                                            value={
                                                                formData.CropVariety
                                                            }
                                                            readOnly
                                                        />
                                                    </div>
                                                    <div className="col-lg-7 col-md-6 mb-2 col-12">
                                                        <label htmlFor="Narration" className="form-label">Narration</label>
                                                        <input
                                                            ref={RefNaration}
                                                            id="Narration"
                                                            className="form-control"
                                                            name="Narration"
                                                            onChange={handleChange}
                                                            value={formData.Narration}
                                                            placeholder="  स्पष्टीकरण प्रविष्ट करा"
                                                            onKeyDown={(e) => handleKeyDown(e, Refvyavarmode)}
                                                        />
                                                    </div>
                                                </div>
                                                <div className="border p-3 rounded shadow-sm mb-1 mt-2">
                                                    <div className="row">
                                                        <div className="col-lg-2 col-md-6 col-6 mb-3">
                                                            <label htmlFor="StartDate" className="form-label required">Scheduled Date</label>
                                                            <input
                                                                ref={ScheduleDateref}
                                                                type="date"
                                                                className="form-control"
                                                                name="ScheduleDate"
                                                                value={detailData.ScheduleDate}
                                                                onChange={(e) => setDetailData({ ...detailData, ScheduleDate: e.target.value })}
                                                                onKeyDown={(e) => handleKeyDown(e, VehicleNoRef)}
                                                            />
                                                        </div>
                                                        <div className="col-lg-2 col-md-6 col-6 mb-3 ">
                                                            <label className="form-label">Vehicle No</label>
                                                            <input
                                                                ref={VehicleNoRef}
                                                                className="form-control"
                                                                name="VehicleNo"
                                                                value={detailData.VehicleNo}
                                                                onChange={(e) => setDetailData({ ...detailData, VehicleNo: e.target.value })}
                                                                onKeyDown={(e) => handleKeyDown(e, packerRef)}
                                                            />
                                                        </div>
                                                        <div className="col-lg-4 col-md-6 col-12 mb-3">
                                                            <label className="form-label required">Packer</label>
                                                            <Select
                                                                ref={packerRef}
                                                                openMenuOnFocus={true}
                                                                isMulti
                                                                placeholder="निवडा पॅकर"
                                                                classNamePrefix="react-select"
                                                                options={Employee.filter(emp => !(detailData.labour || []).includes(emp.value))}
                                                                value={Employee.filter(option => (detailData.packer || []).includes(option.value))}
                                                                onChange={(selectedOptions) => {
                                                                    const selectedValues = selectedOptions.map(option => option.value);
                                                                    setDetailData(prev => ({ ...prev, packer: selectedValues }));
                                                                    if (labourRef.current) {
                                                                        labourRef.current.focus(); // Move focus to save button
                                                                    }
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
                                                                        maxHeight: '150px', // Set max height for dropdown list
                                                                        overflowY: 'auto',   // Enable scrolling
                                                                    }),
                                                                }}
                                                                title="Please Select Packer"
                                                            />
                                                        </div>
                                                        <div className="col-lg-4 col-md-6 col-12 mb-3">
                                                            <label className="form-label required">Labour</label>
                                                            <Select
                                                                ref={labourRef}
                                                                isMulti
                                                                openMenuOnFocus={true}
                                                                placeholder="निवडा मजूर"
                                                                classNamePrefix="react-select"
                                                                options={Employee.filter(emp => !(detailData.packer || []).includes(emp.value))}
                                                                value={Employee.filter(option => (detailData.labour || []).includes(option.value))}
                                                                onChange={(selectedOptions) => {
                                                                    const selectedValues = selectedOptions.map(option => option.value);
                                                                    setDetailData(prev => ({ ...prev, labour: selectedValues }));
                                                                    if (PackingMaterialRef.current) {
                                                                        PackingMaterialRef.current.focus(); // Move focus to save button
                                                                    }
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
                                                                        maxHeight: '150px', // Set max height for dropdown list
                                                                        overflowY: 'auto',   // Enable scrolling
                                                                    }),
                                                                }}
                                                            />
                                                        </div>
                                                    </div>
                                                    <div className="row">
                                                        <div className="col-lg-4 col-md-6 col-12 mb-3">
                                                            <label className="form-label required">Packing Size</label>
                                                            <Select
                                                                ref={PackingMaterialRef}
                                                                openMenuOnFocus={true}
                                                                placeholder="निवडा पॅकिंग साइज"
                                                                classNamePrefix="react-select"
                                                                options={packingsize}
                                                                value={packingsize.find(option => option.value === detailData.packingMaterial) || ""}
                                                                onChange={(selectedOption) => {
                                                                    setDetailData(prev => ({ ...prev, packingMaterial: selectedOption?.value || "" }));
                                                                    if (CrateCountRef.current) {
                                                                        CrateCountRef.current.focus(); // Move focus to save button
                                                                    }
                                                                }}
                                                                styles={{
                                                                    menu: (provided) => ({ ...provided, zIndex: 9999, position: 'absolute' }),
                                                                    menuList: (provided) => ({
                                                                        ...provided,
                                                                        maxHeight: '150px',
                                                                        overflowY: 'auto',
                                                                    }),
                                                                }}
                                                            />
                                                        </div>
                                                        <div className="col-lg-2 col-md-6 col-12 mb-3">
                                                            <label className="form-label required">Packing Quantity</label>
                                                            <input
                                                                ref={CrateCountRef}
                                                                className="form-control"
                                                                name="crateCount"
                                                                value={detailData.crateCount}
                                                                onChange={(e) => setDetailData({ ...detailData, crateCount: e.target.value })}
                                                                onKeyDown={(e) => handleKeyDown(e, Addref)}
                                                            />
                                                        </div>
                                                        <div className="col-4"></div>
                                                        <div className="col-12 col-md-6 col-lg-2 mb-3 mt-4 d-flex justify-content-lg-end align-items-center">
                                                            <div className="w-100 w-md-auto">
                                                                <button
                                                                    ref={Addref}
                                                                    type="button"
                                                                    className="btn btn-submit w-100 w-md-auto"
                                                                    onClick={handleDetailsave}
                                                                >
                                                                    Add
                                                                </button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    {/* Table */}
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
                                                                                <th className="text-center">Date</th>
                                                                                <th className="text-center">Packer</th>
                                                                                <th className="text-center">Labour</th>
                                                                                <th className="text-center">Packing Quantity</th>
                                                                                <th className="text-center">Vehicle No</th>
                                                                                <th className="text-center">Packing Size</th>
                                                                                <th className="text-center">Action</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            {TableData.filter(row => parseInt(row.isdeleted || 0) !== 1).length > 0 ? (
                                                                                TableData.filter(row => parseInt(row.isdeleted || 0) !== 1).map((row, index) => (
                                                                                    <tr key={row.SCHEDULE || index}>
                                                                                        <td className="text-center">{row.ScheduleDate || index}</td>
                                                                                        <td className="text-center" style={{ wordBreak: "break-word", whiteSpace: "normal" }}>
                                                                                            {row.packer
                                                                                                ? row.packer.split(",").map(val => {
                                                                                                    const trimmedVal = val.trim();
                                                                                                    const opt = Employee.find(o => o.value === trimmedVal);
                                                                                                    return opt ? opt.label : trimmedVal;
                                                                                                }).join(', ')
                                                                                                : ''}
                                                                                        </td>
                                                                                        <td className="text-center" style={{ wordBreak: "break-word", whiteSpace: "normal" }}>
                                                                                            {row.labour
                                                                                                ? row.labour.split(",").map(val => {
                                                                                                    const trimmedVal = val.trim();
                                                                                                    const opt = Employee.find(o => o.value === trimmedVal);
                                                                                                    return opt ? opt.label : trimmedVal;
                                                                                                }).join(', ')
                                                                                                : ''}
                                                                                        </td>
                                                                                        <td className="text-center">{row.crateCount}</td>
                                                                                        <td className="text-center">{row.VehicleNo}</td>
                                                                                        <td className="text-center" style={{ wordBreak: "break-word", whiteSpace: "normal" }}>
                                                                                            {(() => {
                                                                                                const opt = packingsize.find(o => o.value === row.packingMaterial);
                                                                                                return opt?.label || row.packingMaterial || "-";
                                                                                            })()}
                                                                                        </td>
                                                                                        <td className="text-center">
                                                                                            <Link to="#" onClick={() => handleEdit(row, index)} className="me-2 p-1" style={{ color: 'lightblue' }}>
                                                                                                <Edit className="feather-edit" />
                                                                                            </Link>
                                                                                            <Link to="#" className="confirm-text p-2" onClick={(e) => { e.preventDefault(); handleDelete(row); }}>
                                                                                                <Trash2 className="feather-trash-2 text-danger" />
                                                                                            </Link>
                                                                                        </td>
                                                                                    </tr>
                                                                                ))
                                                                            ) : (
                                                                                <tr>
                                                                                    <td colSpan="7" className="text-center">No Data Available</td>
                                                                                </tr>
                                                                            )}
                                                                        </tbody>
                                                                    </table>
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
export default ScheduleProceed;
