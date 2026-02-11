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
const ScheduleProceed = ({ VAID, SHAID }) => {
    console.log("VAIDVAIDVAID", VAID, SHAID);
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


    const handleKeyDown = (e, nextRef, isLastField = false) => {
        if (e.key === "Enter") {


            if (isLastField) {
                SaveRef.current?.focus();
                showConfirmationAlert(e);
            } else {
                nextRef?.current?.focus();
            }
        }
    };

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
            title: 'तुम्हाला माहिती सेव्ह करायची आहे का?',
            icon: 'question',
            showCancelButton: true,
            confirmButtonText: 'हो',
            cancelButtonText: 'नाही'
        }).then((result) => {
            if (result.isConfirmed) {
                handleSubmit(e);  // Call your main save function
            }
        });
    };


    const handleSubmit = async (e) => {

        if (!formData.FarmerName || !formData.CropName || !formData.CropArea || !formData.ExpectedHarvesting || !formData.CropVariety) {
            Swal.fire({
                icon: 'error',
                title: 'त्रुटी',
                text: 'कृपया सर्व मास्टर डेटा पूर्ण भरा.'
            });
            return;
        }

        // Detail validation
        const activeDetails = TableData.filter(row => parseInt(row.isdeleted) !== 1);
        if (activeDetails.length === 0) {
            Swal.fire({
                icon: 'error',
                title: 'त्रुटी',
                text: 'कृपया किमान एक डीटेल रेकॉर्ड जोडा.'
            });
            return;
        }

        try {
            const masterPayload = {
                shaid: SHAID ? SHAID : GUID,
                faid: formData.FarmerName,
                ctype: formData.CropName,
                carea: formData.CropArea,
                vaid: VAID,
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
                const detailPayload = {
                    shdaid: row?.shdaid || "",
                    shaid: SHAID ? SHAID : GUID,
                    shsdate: row.ScheduleDate,
                    vdaid: VAID,
                    packer: row.packer,
                    worker: row.labour,
                    vehno: row.VehicleNo,
                    psize: row.packingMaterial,
                    pquntity: row.crateCount,
                    isdeleted: row.isdeleted === 1 ? true : false
                };
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
                title: 'यशस्वी',
                text: 'डेटा यशस्वीरित्या सेव्ह झाला आहे!'
            });

            setTableData([]);
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
                title: 'सेव्ह मध्ये त्रुटी',
                text: error.message || 'सर्व्हर एरर'
            });
        }
    };


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
                TotalDays: diffDays >= 0 ? diffDays : 0, // Prevent negative
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

            // Prevent API call if WeightInkg < OutWardStockWeight
            if (out > net) {
                MySwal.fire({
                    text: 'Outward वजन हे एकूण वजनापेक्षा जास्त असू शकत नाही.',
                    icon: 'error',
                    confirmButtonColor: '#00ff00',
                    confirmButtonText: 'ठीक आहे',
                });
                return; // Do not continue
            }

            if (!value || value.trim() === "") {
                setFormData(prev => ({
                    ...prev,
                    OutWardStockWeight: "",
                    RemainingStockWeight: "",
                }));
                setTableData([]); // << this clears the table
                return;
            }


            const updateddata = {
                ...formData,
                OutWardStockWeight: value,
                RemainingStockWeight: remaining === 0 ? "00" : remaining.toFixed(2),
            };

            setFormData(updateddata);

            // Call API after validation passes
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

        // ✅ Correctly reset with proper types
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

    const validateinput = (e) => {
        const { ScheduleDate, packer, labour, VehicleNo, crateCount, packingMaterial } = detailData;

        if (!ScheduleDate || !/^\d{4}-\d{2}-\d{2}$/.test(ScheduleDate)) {
            Swal.fire({ icon: "error", title: "त्रुटी", text: "वैध दिनांक द्या." });
            return;
        }
        if (!packer || packer.length === 0) {
            Swal.fire({ icon: "error", title: "त्रुटी", text: "पॅकर निवडा." });
            return;
        }
        if (!labour || labour.length === 0) {
            Swal.fire({ icon: "error", title: "त्रुटी", text: "मजूर निवडा." });
            return;
        }
        if (!VehicleNo || !/^[A-Z]{2}\d{1,2}[A-Z]{1,3}\d{1,4}$/.test(VehicleNo)) {
            Swal.fire({ icon: "error", title: "त्रुटी", text: "वैध गाडी क्रमांक द्या." });
            return;
        }
        if (!crateCount || !/^\d{1,4}$/.test(crateCount)) {
            Swal.fire({ icon: "error", title: "त्रुटी", text: "क्रेट संख्या चुकीची." });
            return;
        }
        if (!packingMaterial) {
            Swal.fire({ icon: "error", title: "त्रुटी", text: "Packing Material निवडा." });
            return;
        }
        handleAddRow(e);
    };


    const handleEdit = (row, index) => {
        const packerIds = row.packer.split(",").map(label => {
            const emp = Employee.find(e => e.label.trim() === label.trim());
            return emp?.value;
        }).filter(Boolean);
        const labourIds = row.labour.split(",").map(label => {
            const emp = Employee.find(e => e.label.trim() === label.trim());
            return emp?.value;
        }).filter(Boolean);

        setDetailData({
            ScheduleDate: row.ScheduleDate,
            packer: packerIds,
            labour: labourIds,
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
                        companyid: "COMP123456789",
                        deptid: "D001",
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
                            CropName: data.ctype,
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

                // const payload = { companyid: "" };

                const response = await axios.post(
                    `${baseUrl.Url}/backend/api/GET_PackingMaterialDropdown`,
                    // payload,
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

                setFarmer(options);  // <-- You need to create this new state

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
                    "companyid": "COMP123456789",
                    "deptid": "D001",
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
    return (

        <>



            <div className="modal fade" id="add-units-category">

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
                                            data-bs-dismiss="modal"
                                            aria-label="Close">
                                            <ArrowLeft className="me-2" />
                                            मागे
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
                                                        <label htmlFor="StartDate" className="form-label required">कापणी दिनांक</label>
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
                                                        <label htmlFor="PhoneNo" className="form-label">फोन.क्र</label>
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
                                                        <label htmlFor="shopName" className="form-label required">शेतकरी नाव</label>
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
                                                        <label htmlFor="CropName" className="form-label ">पीक नाव</label>
                                                        <input
                                                            type="text"
                                                            className="form-control"
                                                            name="CropName"
                                                            value={
                                                                cropType.find(option => option.value === formData.CropName)?.label || ''
                                                            }
                                                            readOnly
                                                        />

                                                    </div>
                                                    <div className="col-lg-2  col-md-6 mb-3 col-6">
                                                        <div className="mb-0 add-product form-label">
                                                            <div className="d-flex justify-content-between align-items-center">
                                                                <label htmlFor="shopLocation" className="form-label required"> क्षेत्र</label>
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
                                                        <label htmlFor="ExpectedHarvesting" className="form-label ">अपेक्षित उत्पादन</label>

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
                                                        <label htmlFor="CropVariety" className="form-label">पीक जात </label>
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
                                                        <label htmlFor="Narration" className="form-label">स्पष्टीकरण</label>
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

                                                    {/* First Row */}
                                                    <div className="row">
                                                        <div className="col-lg-2 col-md-6 col-6 mb-3">
                                                            <label htmlFor="StartDate" className="form-label required">नियोजित दिनांक</label>
                                                            <input
                                                                ref={ScheduleDateref}
                                                                type="date"
                                                                className="form-control"
                                                                name="ScheduleDate"
                                                                value={detailData.ScheduleDate}
                                                                onChange={(e) => setDetailData({ ...detailData, ScheduleDate: e.target.value })}

                                                            />
                                                        </div>

                                                        <div className="col-lg-2 col-md-6 col-6 mb-3 ">
                                                            <label className="form-label">गाडी.क्र</label>
                                                            <input
                                                                ref={VehicleNoRef}
                                                                className="form-control"
                                                                name="VehicleNo"
                                                                value={detailData.VehicleNo}
                                                                onChange={(e) => setDetailData({ ...detailData, VehicleNo: e.target.value })}

                                                            />
                                                        </div>

                                                        <div className="col-lg-4 col-md-6 col-12 mb-3">
                                                            <label className="form-label required">पॅकर</label>
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
                                                                }}
                                                                title="Please Select Packer"

                                                            />
                                                        </div>



                                                        <div className="col-lg-4 col-md-6 col-12 mb-3">
                                                            <label className="form-label required">मजूर</label>
                                                            <Select
                                                                isMulti
                                                                openMenuOnFocus={true}
                                                                placeholder="निवडा मजूर"
                                                                classNamePrefix="react-select"
                                                                options={Employee.filter(emp => !(detailData.packer || []).includes(emp.value))}
                                                                value={Employee.filter(option => (detailData.labour || []).includes(option.value))}
                                                                onChange={(selectedOptions) => {
                                                                    const selectedValues = selectedOptions.map(option => option.value);
                                                                    setDetailData(prev => ({ ...prev, labour: selectedValues }));
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
                                                                }}

                                                            />
                                                        </div>




                                                    </div>

                                                    {/* Second Row */}
                                                    <div className="row">
                                                        <div className="col-lg-4 col-md-6 col-12 mb-3">
                                                            <label className="form-label required">पॅकिंग साइज</label>
                                                            <Select
                                                                ref={PackingMaterialRef}
                                                                openMenuOnFocus={true}
                                                                placeholder="निवडा पॅकिंग साइज"
                                                                classNamePrefix="react-select"
                                                                options={packingsize}
                                                                value={packingsize.find(option => option.value === detailData.packingMaterial) || ""}
                                                                onChange={(selectedOption) => {
                                                                    setDetailData(prev => ({ ...prev, packingMaterial: selectedOption?.value || "" }));
                                                                }}

                                                            />


                                                        </div>

                                                        <div className="col-lg-2 col-md-6 col-12 mb-3">
                                                            <label className="form-label required">पॅकिंग संख्या</label>
                                                            <input
                                                                ref={CrateCountRef}
                                                                className="form-control"
                                                                name="crateCount"
                                                                value={detailData.crateCount}
                                                                onChange={(e) => setDetailData({ ...detailData, crateCount: e.target.value })}

                                                            />
                                                        </div>
                                                        <div className="col-4"></div>
                                                        <div className="col-12 col-md-6 col-lg-2 mb-3 mt-4 d-flex justify-content-lg-end align-items-center">
                                                            <div className="w-100 w-md-auto">
                                                                <button
                                                                    type="button"
                                                                    className="btn btn-submit w-100 w-md-auto"
                                                                    onClick={handleDetailsave}

                                                                >
                                                                    जोडा
                                                                </button>
                                                            </div>
                                                        </div>

                                                    </div>

                                                    {/* Table */}
                                                    <div className="row">
                                                        <div className="col-lg-12">
                                                            <div className="modal-body-table">
                                                                <div className="table-responsive">
                                                                    <div style={{ maxHeight: "325px", overflowY: "auto" }}>
                                                                        <table className="table datanew table-bordered" style={{ tableLayout: "fixed", width: "100%" }}>
                                                                            <thead className="thead-dark" style={{ position: "sticky", top: 0, backgroundColor: "#343a40", color: "white", zIndex: 1000 }}>
                                                                                <tr>
                                                                                    <th className="text-center">दिनांक</th>
                                                                                    <th className="text-center">पॅकर</th>
                                                                                    <th className="text-center">मजूर</th>
                                                                                    <th className="text-center">क्रेट संख्या</th>
                                                                                    <th className="text-center">गाडी.क्र</th>
                                                                                    <th className="text-center">पॅकिंग साहित्य</th>
                                                                                    <th className="text-center">कृती</th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                {TableData.filter(row => parseInt(row.isdeleted || 0) !== 1).length > 0 ? (
                                                                                    TableData.filter(row => parseInt(row.isdeleted || 0) !== 1).map((row, index) => (
                                                                                        <tr key={row.SCHEDULE || index}>
                                                                                            <td className="text-center">{row.ScheduleDate}</td>
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

                                                                                            {/* <td className="text-center" style={{ wordBreak: "break-word", whiteSpace: "normal" }}>{row.packingMaterial}</td> */}
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
                                                                                        <td colSpan="7" className="text-center">डेटा उपलब्ध नाही</td>
                                                                                    </tr>
                                                                                )}
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                                <div className="col-lg-12">
                                                    <div className="text-end">
                                                        <div className="row justify-content-end">
                                                            {/* Save Button - First on mobile, second on desktop */}
                                                            <div className="col-12 col-md-1 mb-2 order-1 order-md-2">
                                                                <button
                                                                    type="button"
                                                                    className="btn btn-submit w-100"
                                                                    ref={SaveRef} onClick={handleFinalSave}
                                                                >
                                                                    सेव्ह
                                                                </button>
                                                            </div>

                                                            {/* Back Button - Second on mobile, first on desktop */}
                                                            <div className="col-12 col-md-1 mb-2 order-2 order-md-1">
                                                                <button
                                                                    type="button"
                                                                    className="btn btn-cancel w-100"
                                                                    data-bs-dismiss="modal"
                                                                    aria-label="Close"
                                                                >
                                                                    मागे
                                                                </button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                {/* <div className="col-lg-12">
                                                    <div className="btn-addproduct mb-1 ">
                                                        <button type="button"
                                                            className="btn btn-cancel me-2"
                                                            data-bs-dismiss="modal"
                                                            aria-label="Close"
                                                        >
                                                            मागे
                                                        </button>
                                                        <button type="button" className="btn btn-submit" ref={SaveRef} onClick={handleFinalSave}>
                                                            सेव्ह
                                                        </button>

                                                    </div>
                                                </div> */}
                                            </div>
                                        </div>

                                    </form>
                                </div>
                            </div >
                            {/* <AddCash onClose={handleModalClose} totalAmount1={totalAmount} /> */}
                        </div >
                    </div >
                </div >
            </div >

        </>
    );
};




export default ScheduleProceed;
