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
import { Weight } from "lucide-react";
const AddCropBill = ({ CROPID, }) => {
    // const userdetail = getUserData();
    const [EndDate, setEndDate] = useState('');
    const [StartDate, setStartDate] = useState('');
    const [editIndex, setEditIndex] = useState(null);
    const [vaid, setVaid] = useState("");
    const [Employee, setEmployee] = useState([]);
    const [packingsize, setpackingsize] = useState([]);
    const { isAuthenticated, userdetail } = getUserData();
    const ScheduleDateref = useRef(null);
    const packerRef = useRef(null);
    const labourRef = useRef(null);
    const VehicleNoRef = useRef(null);
    const CrateCountRef = useRef(null);
    const PackingMaterialRef = useRef(null);
    const [isMobile, setIsMobile] = useState(window.innerWidth < 768);
    useEffect(() => {
        const handleResize = () => setIsMobile(window.innerWidth < 768);
        window.addEventListener('resize', handleResize);
        return () => window.removeEventListener('resize', handleResize);
    }, []);
    if (isAuthenticated == true) {
        console.log("user", userdetail);
        console.log("getUserData", getUserData);
    }
    console.log("AddCropInvert props", { CROPID });
    const MySwal = withReactContent(Swal);
    useEffect(() => {
        getTodayDate();
    }, [CROPID,]);
    const [farmeName, setfarmeName] = useState([]);
    const [Date1, setDate] = useState([]);
    const [Date123, setDate123] = useState([]);
    const [MoblieNo, setMoblieNo] = useState([]);
    const [tabledata, settabledata] = useState([]);
    const [Crop, setCrop] = useState([]);
    const [selectedData, setSelectedData] = useState({ cropid: null });
    // const totalAmount = tabledata.reduce((sum, row) => {
    //     const value = parseFloat(row.cropinvert);
    //     return sum + (isNaN(value) ? 0 : value);
    // }, 0);
    // const openModal = (cropid) => {
    //     setSelectedData({ cropid });

    //     setTimeout(() => {
    //         setSelectedData(cropid);
    //     }, 10);
    // };
    const openModal = (cropid) => {
        setSelectedData({ cropid });
        setTimeout(() => {
            const modal = new window.bootstrap.Modal(document.getElementById("print-receipt"));
            modal.show();
        }, 100);
    };
    useEffect(() => {
        const modalElement = document.getElementById('print-receipt');
        if (!modalElement) return;

        const handleHidden = () => {

            const backdrops = document.getElementsByClassName('modal-backdrop');
            while (backdrops.length > 0) {
                backdrops[0].parentNode.removeChild(backdrops[0]);
            }
        };
        modalElement.addEventListener('hidden.bs.modal', handleHidden);

        return () => {
            modalElement.removeEventListener('hidden.bs.modal', handleHidden);
        };
    }, []);

    useEffect(() => {
        if (selectedData.cropid) {
            fetchbill();
        }
    }, [selectedData.cropid]);
    const fetchbill = async () => {
        try {
            const payload = {
                cropid: selectedData.cropid,
                companyid: userdetail?.companyID || "",
                deptid: userdetail?.departmentID || "",
            };
            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };
            const response = await axios.post(`${baseUrl.Url}/backend/api/GET_CropInvertBill`, payload, { headers });

            if (response.status !== 200) throw new Error("Failed to fetch vendor data");

            console.log("Farmer bill", response.data);

            if (Array.isArray(response.data) && response.data.length > 0) {
                const data = response.data[0];
                setfarmeName(data.farmername);
                setDate123(data.date);
                setMoblieNo(data.contactno);
                settabledata(response.data);
                setCrop(data.ctype);
            } else {
                console.warn("GET_CropInvertBill returned empty or invalid data:", response.data);
            }
        } catch (error) {
            console.error("Error fetching Bill data:", error);
        }
    };

    const getTodayDate = () => {
        const today = new Date();
        const formatted = today.toLocaleDateString("en-GB", {
            day: "2-digit",
            month: "short",
            year: "numeric",
        });
        setEndDate(formatted);
    };

    const getTodayDate2 = () => {
        const today = new Date();
        const yyyy = today.getFullYear();
        const mm = String(today.getMonth() + 1).padStart(2, "0");
        const dd = String(today.getDate()).padStart(2, "0");
        return `${yyyy}-${mm}-${dd}`;
    };

    const [TableData, setTableData] = useState([]);
    const [rows, setRows] = useState([]);
    const [formData, setFormData] = useState({
        cropid: "",
        StartDate: "",
        FarmerName: "",
        faid: "",
        PhoneNo: "",
        CropName: "",
        CropArea: "",
        CropVariety: "",
        Narration: "",
        cropInvert: "",
        Rate: "",
        ctype: "",
        Weight: "",
        caret: "",
        Amount: "",
        Advance: 0
    });

    const [detailData, setDetailData] = useState({
        cropid: "",
        cdaid: "",
        shaid: "",
        SHDAID: "",
        shdaid: "",
        StartDate: "",
        VehicleNo: "",
        packer: "",
        labour: "",
        packingMaterial: "",
        crateCount: "",
        packingsize: "",
        cropInvert: "",
        Weight: "",
        Quantity: "",
        isdeleted: 0,
    });

    const GUID = ACSPLGUID.getNew();
    // const totalAmount = TableData.reduce((sum, row) => sum + (parseFloat(row.total) || 0), 0).toFixed(2);
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
        e.preventDefault();
        let cropidToRollback = null;
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
            const CBKID = ACSPLGUID.getNew();
            const masterPayload = {
                cbkid: formData.cbkid || CBKID,
                cropid: CROPID,
                // faid: formData.FarmerName,
                faid: formData.faid,
                date: TodaysDate,
                contactno: formData.PhoneNo,
                ctype: formData.ctype,
                carea: formData.CropArea,
                cvariety: formData.CropVariety,
                rate: formData.Rate,
                narr: formData.Narration || "",
                iscompleted: "1",
                companyid: userdetail?.companyID || "",
                deptid: userdetail?.departmentID || "",
            };
            const masterResponse = await axios.post(
                `${baseUrl.Url}/backend/api/SP_AddUpdCropBill`,
                masterPayload,
                { headers: { 'Content-Type': 'application/json' } }
            );



            const avdtran = {
                "avdaid": CBKID,
                "adate": userdetail?.APPDT || "",
                "cdaid": formData.cbkid || CBKID,
                "shaid": "",
                "maid": formData.faid,
                "avdamt": 0,
                "exavdamt": 0,
                "totamt": 0,
                "pamt": 0,
                "companyid": "string",
                "deptid": "string",
                "asid": "string",
                "isdeleted": true
            }

            if (masterResponse.status !== 200) throw new Error("Master save failed");
            cropidToRollback = masterPayload.cbkid;
            const formatDate = (date) => {
                if (!date) return null;
                const d = new Date(date);
                if (isNaN(d.getTime())) return null;
                return d.toISOString().split('T')[0];
            };
            const detailPayloadList = activeDetails.map(row => ({
                cdbid: row.cdbid ? row.cdbid : ACSPLGUID.getNew(),
                cbkid: cropidToRollback,
                cdaid: CROPID ? CROPID : GUID,
                date: formatDate(row.ScheduleDate),
                vehno: row.VehicleNo,
                packer: Array.isArray(row.packer) ? row.packer.join(',') : row.packer,
                worker: Array.isArray(row.labour) ? row.labour.join(',') : row.labour,
                psize: row.packingMaterial,
                pquntity: row.Quantity || row.crateCount,
                cratecount: row.crateCount,
                cropinvert: (formData.Rate || "0").toString(),
                weight: row.Weight,
                companyid: userdetail?.companyID || "",
                deptid: userdetail?.departmentID || "",
            }));

            const detailResponse = await axios.post(
                `${baseUrl.Url}/backend/api/SP_AddUpdCropBillDetail`,
                detailPayloadList,
                { headers: { 'Content-Type': 'application/json' } }
            );
            if (detailResponse.status !== 200) throw new Error("Detail save failed");

            Swal.fire({
                icon: 'success',
                title: 'Ok',
                text: 'Data Save Succesfully!'
            });

            setTableData([]);
            setFormData({
                cropid: "",
                FarmerName: "",
                PhoneNo: "",
                CropName: "",
                CropArea: "",
                ExpectedHarvesting: "",
                CropVariety: "",
                cropInvert: "",
                Rate: "",
                Narration: "",
                Advance: 0,
                ExtraAdvance: 0,
                TotalAmt: 0
            });

            setDetailData({
                ScheduleDate: "",
                packer: "",
                labour: "",
                VehicleNo: "",
                crateCount: "",
                packingMaterial: "",
                isdeleted: 0
            });

        } catch (error) {
            console.error("Save failed:", error);
            Swal.fire({
                icon: 'error',
                title: 'Save Error',
                text: error.message || 'Server Error'
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
                TotalDays: diffDays >= 0 ? diffDays : 0,
            }));
        }
    }, [StartDate, EndDate]);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setDetailData(prev => ({
            ...prev,
            [name]: value
        }));
    };

    const [cbkid, setCbkid] = useState("");

    useEffect(() => {
        if (!CROPID) return;
        const fetchMasterData = async () => {
            try {
                const payload1 = {
                    cropid: CROPID,
                    companyid: userdetail?.companyID || "",
                    deptid: userdetail?.departmentID || "",
                };

                const headers = {
                    "Content-Type": "application/json",
                    Accept: "*/*"
                };

                const response = await axios.post(
                    baseUrl.Url + "/backend/api/GET_CropBill",
                    payload1,
                    { headers }
                );

                if (response.status !== 200) throw new Error("Failed to fetch master data");

                const apiData = response.data[0];

                setCbkid(apiData.cbkid || "");

                setFormData(prev => ({
                    ...prev,
                    cbkid: apiData.cbkid,
                    cropid: apiData.cropid,
                    StartDate: apiData.visitingdate,
                    PhoneNo: apiData.phonenumber,
                    FarmerName: apiData.farmername,
                    faid: apiData.faid || "",
                    CropName: apiData.cropname,
                    ctype: apiData.ctype,
                    CropArea: apiData.carea,
                    CropVariety: apiData.cvariety,
                    ExpectedHarvesting: apiData.eproduction,
                    Rate: apiData.rate,
                    Weight: apiData.weight,
                    caret: apiData.caret,
                    Amount: apiData.amount,
                    Narration: apiData.narr,
                    Advance: apiData.advance,
                    ExtraAdvance: 0,
                    TotalAmt: 0
                }));
            } catch (error) {
                console.error("Error in Master API Call:", error);
            }
        };
        fetchMasterData();
    }, [CROPID]);
    useEffect(() => {
        if (!CROPID) return;
        const fetchDetailsData = async () => {
            try {
                if (!CROPID) {
                    console.warn("CROPID missing, skipping detail fetch");
                    return;
                }
                const payload = {
                    cropid: CROPID,
                    cdaid: "",
                    cbkid: cbkid,
                    companyid: userdetail?.companyID || "",
                    deptid: userdetail?.departmentID || "",
                };
                const headers = {
                    "Content-Type": "application/json",
                    Accept: "*/*"
                };
                const response = await axios.post(
                    baseUrl.Url + "/backend/api/GET_CropBillDetail",
                    payload,
                    { headers }
                );
                if (response.status !== 200) throw new Error("Failed to fetch details data");
                if (response.data.length > 0) {
                    const mappedProducts = response.data.map(item => ({
                        cdbid: item.cdbid,
                        cropid: item.cropid,
                        ScheduleDate: item.visitingdate,
                        VehicleNo: item.vehno,
                        packer: item.packername,
                        labour: item.workername,
                        packingMaterial: item.size,
                        crateCount: item.count,
                        cropInvert: item.amount,
                        Quantity: item.pquntity,
                        Weight: item.weight,
                        isdeleted: item.isdeleted,
                    }));
                    setTableData(mappedProducts);

                    const totalAmount1 = response.data.reduce((sum, r) => {
                        const rate = parseFloat(r.rate);
                        const weight = parseFloat(r.weight || 0);
                        return sum + rate * weight;
                    }, 0);
                    console.log(totalAmount1, "totalAmount1")
                    setFormData(prev => ({
                        ...prev,
                        TotalAmt: totalAmount1 - response.data[0].advance || 0
                    }));
                } else {
                    console.log("No details data found, might be new.");
                }
            } catch (error) {
                console.error("Error fetching detail data:", error);
            }
        };
        fetchDetailsData();
    }, [CROPID, cbkid]);
    useEffect(() => {
        const fetchEmployee = async () => {
            try {
                const headers = {
                    "Content-Type": "application/json",
                    Accept: "*/*",
                };
                const payload = {
                    companyid: userdetail?.companyID || "",
                    deptid: userdetail?.departmentID || "",
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
        const fetchpacking = async () => {
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
        fetchpacking();
    }, []);

    const showExitAlert = () => {
        setDetailData({
            cdaid: "",
            shaid: "",
            StartDate: "",
            VehicleNo: "",
            packer: "",
            labour: "",
            packingMaterial: "",
            crateCount: "",
            packingsize: "",
            cropInvert: "",
            Weight: "",
            Quantity: "",
            isdeleted: 0,
        });
        const modal = document.getElementById("add-units-category");
        if (modal) {
            modal.classList.remove("show");
            modal.style.display = "none";
            modal.setAttribute("aria-hidden", "true");

            document.querySelectorAll(".modal-backdrop").forEach(backdrop => {
                backdrop.remove();
            });
            document.body.classList.remove("modal-open");
            document.body.style.overflow = "";
            document.body.style.paddingRight = "";
        }
    };

    useEffect(() => {
        const Rate = parseFloat(formData.Rate) || 0;
        const weight = parseFloat(detailData.Weight) || 0;
        const cropInvert = Rate * weight;
        setFormData(prev => ({
            ...prev,
            cropInvert: cropInvert.toString()
        }));
    }, [formData.Rate, detailData.Weight]);

    const handlePrint = () => {
        const printContents = document.getElementById("printableArea").innerHTML;
        const printWindow = window.open("", "", "height=800,width=1000");
        printWindow.document.write(`
            <html>
            <head>
                <title>Print Bill</title>
                <style>
                body { font-family: Arial, sans-serif; padding: 20px; }
                table { width: 100%; border-collapse: collapse; }
                th, td { padding: 8px; border: 1px solid #ccc; text-align: center; }
                th { background-color: #add8e6; }
                h3, h6 { margin: 10px 0; }
                </style>
            </head>
            <body>
                ${printContents}
            </body>
            </html>
        `);
        printWindow.document.close();
        printWindow.focus();
        printWindow.print();
        printWindow.close();
    };

    const activeRows = TableData.filter(r => parseInt(r.isdeleted || 0) !== 1);

    const totalAmount = activeRows.reduce((sum, r) => {
        const rate = parseFloat(r.Rate ?? formData.Rate ?? 0);
        const weight = parseFloat(r.Weight || 0);
        return sum + rate * weight;
    }, 0);

    console.log(totalAmount, "totalAmount")
    useEffect(() => {
        const printModal = document.getElementById('print-receipt');
        if (!printModal) return;
        const handleHidden = () => {
            const fullModal = document.getElementById('add-units-category');
            if (fullModal && !fullModal.classList.contains('show')) {
                fullModal.classList.add('show');
                fullModal.style.display = 'block';
                document.body.classList.add('modal-open');
            }
        };
        printModal.addEventListener('hidden.bs.modal', handleHidden);
        return () => {
            printModal.removeEventListener('hidden.bs.modal', handleHidden);
        };
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
                                        <h4>Crop Bill</h4>
                                    </div>
                                    <div className="page-btn">
                                        <Link className="btn btn-secondary"
                                            data-bs-dismiss="modal"
                                            aria-label="Close"
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
                                                    <div className="col-6 col-md-6 col-lg-2 mb-3">
                                                        <div className="mb-0 add-product">
                                                            <label htmlFor="StartDate" className="form-label"> Date</label>
                                                            <input
                                                                type="date"
                                                                className="form-control"
                                                                name="StartDate"
                                                                value={TodaysDate}
                                                                readOnly
                                                            />
                                                        </div>
                                                    </div>
                                                    <div className="col-6 col-md-6 col-lg-2 mb-3">
                                                        <div className="mb-0 add-product">
                                                            <label htmlFor="PhoneNo" className="form-label required">Contact No.</label>
                                                            <input
                                                                type="text"
                                                                id="PhoneNo"
                                                                className="form-control"
                                                                name="PhoneNo"
                                                                value={formData.PhoneNo}
                                                                readOnly
                                                            />
                                                        </div>
                                                    </div>
                                                    <div className="col-12 col-md-6 col-lg-4 mb-2">
                                                        <label htmlFor="shopName" className="form-label required">Farmer Name</label>
                                                        <input
                                                            type="text"
                                                            className="form-control"
                                                            name="FarmerName"
                                                            value={formData.FarmerName || ''}
                                                            onChange={(e) => setFormData({ ...formData, FarmerName: e.target.value })}
                                                            readOnly
                                                        />
                                                        {/* <input
                                                        type="text"
                                                        name="faid"
                                                        value={formData.faid}
                                                        onChange={(e) => setFormData({ ...formData, faid: e.target.value })}
                                                        /> */}
                                                    </div>
                                                    <div className="col-6 col-md-6 col-lg-2 mb-3">
                                                        <div className="mb-0 add-product">
                                                            <label htmlFor="CropName" className="form-label required">Crop Name</label>
                                                            <input
                                                                type="text"
                                                                className="form-control"
                                                                name="CropName"
                                                                value={formData.CropName || ''}
                                                                onChange={(e) => setFormData({ ...formData, CropName: e.target.value })}
                                                                readOnly
                                                            />
                                                        </div>
                                                    </div>
                                                    <div className="col-6 col-md-6 col-lg-2 mb-3">
                                                        <div className="mb-0 add-product">
                                                            <label htmlFor="CropVariety" className="form-label">Crop Variety</label>
                                                            <input
                                                                id="CropVariety"
                                                                type="text"
                                                                className="form-control"
                                                                name="CropVariety"
                                                                onChange={handleChange}
                                                                value={formData.CropVariety}
                                                                readOnly
                                                            />
                                                        </div>
                                                    </div>
                                                    <div className="col-6 col-md-6 col-lg-2 mb-3">
                                                        <div className="mb-0 add-product">
                                                            <label htmlFor="Crop Invert" className="form-label required">Rate</label>
                                                            <input
                                                                ref={CrateCountRef}
                                                                className="form-control"
                                                                name="Rate"
                                                                value={formData.Rate}
                                                                onChange={(e) => setFormData({ ...formData, Rate: e.target.value })}
                                                                readOnly
                                                            />
                                                        </div>
                                                    </div>
                                                    <div className="col-6 col-md-6 col-lg-2 mb-3">
                                                        <div className="mb-0 add-product">
                                                            <label htmlFor="Crop Invert" className="form-label required" style={{ whiteSpace: "nowrap" }}>Weight(KG)</label>
                                                            <input
                                                                ref={CrateCountRef}
                                                                className="form-control"
                                                                name="Weight"
                                                                value={formData.Weight}
                                                                onChange={(e) => setFormData({ ...formData, Weight: e.target.value })}
                                                                readOnly
                                                            />
                                                        </div>
                                                    </div>
                                                    <div className="col-6 col-md-6 col-lg-2 mb-3">
                                                        <div className="mb-0 add-product">
                                                            <label htmlFor="Crop Invert" className="form-label required">Total Caret</label>
                                                            <input
                                                                ref={CrateCountRef}
                                                                className="form-control"
                                                                name="caret"
                                                                value={formData.caret}
                                                                onChange={(e) => setFormData({ ...formData, caret: e.target.value })}
                                                                readOnly
                                                            />
                                                        </div>
                                                    </div>
                                                    {/* <div className="col-6 col-md-6 col-lg-1 mb-3">
                                                        <div className="mb-0 add-product">
                                                            <label htmlFor="Crop Invert" className="form-label required">Amount</label>
                                                            <input
                                                                ref={CrateCountRef}
                                                                className="form-control"
                                                                name="Amount"
                                                                value={formData.Amount}
                                                                onChange={(e) => setFormData({ ...formData, Amount: e.target.value })}
                                                                readOnly
                                                            />
                                                        </div>
                                                    </div> */}

                                                </div>
                                                <div className="border p-3 rounded shadow-sm mb-1 mt-2">
                                                    {/* Table */}
                                                    <div className="row">
                                                        <div className="col-lg-12">
                                                            <div className="modal-body-table">
                                                                <div className="table-responsive">
                                                                    <div>
                                                                        <table className="table datanew table-bordered table-responsive-sm" style={{ tableLayout: "fixed", width: "100%" }}>
                                                                            <thead className="thead-dark" style={{ position: "sticky", top: 0, backgroundColor: "#343a40", color: "white", zIndex: 1000 }}>
                                                                                <tr>
                                                                                    <th className="text-center" style={{ height: "10px", width: "100px" }}>Date</th>
                                                                                    <th className="text-center" style={{ height: "10px", width: "120px" }}>Veh.no</th>
                                                                                    <th className="text-center" style={{ height: "10px" }}>Packer</th>
                                                                                    <th className="text-center" style={{ height: "10px" }}>Worker</th>
                                                                                    <th className="text-center" style={{ height: "10px" }}>Packing Size</th>
                                                                                    <th className="text-center" style={{ height: "10px", width: "90px" }}>Quantity</th>
                                                                                    {/* <th className="text-center" style={{ height: "10px", width: "90px" }}>Caret Count</th> */}
                                                                                    <th className="text-center" style={{ height: "10px", width: "90px" }}>Weight</th>
                                                                                    <th className="text-center" style={{ height: "10px", width: "90px" }}>Rate</th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody style={{ maxHeight: "325px", overflowY: "auto" }}>
                                                                                {
                                                                                    TableData.filter(row => parseInt(row.isdeleted || 0) !== 1).length > 0 ? (
                                                                                        TableData.filter(row => parseInt(row.isdeleted || 0) !== 1)
                                                                                            .map((row, index, record) => (
                                                                                                <tr key={row.shaid || index}>
                                                                                                    <td className="text-center">{row.ScheduleDate}</td>
                                                                                                    <td className="text-center">{row.VehicleNo}</td>
                                                                                                    <td className="text-center" style={{ wordBreak: "break-word", whiteSpace: "normal" }}>
                                                                                                        {Array.isArray(row.packer)
                                                                                                            ? row.packer.map(val => {
                                                                                                                const opt = Employee.find(o => o.value === val);
                                                                                                                return opt ? opt.label : val;
                                                                                                            }).join(", ")
                                                                                                            : row.packer
                                                                                                                ? row.packer.split(",").map(val => {
                                                                                                                    const opt = Employee.find(o => o.value === val.trim());
                                                                                                                    return opt ? opt.label : val.trim();
                                                                                                                }).join(", ")
                                                                                                                : ""
                                                                                                        }
                                                                                                    </td>
                                                                                                    <td className="text-center" style={{ wordBreak: "break-word", whiteSpace: "normal" }}>
                                                                                                        {Array.isArray(row.labour)
                                                                                                            ? row.labour.map(val => {
                                                                                                                const opt = Employee.find(o => o.value === val);
                                                                                                                return opt ? opt.label : val;
                                                                                                            }).join(", ")
                                                                                                            : row.labour
                                                                                                                ? row.labour.split(",").map(val => {
                                                                                                                    const opt = Employee.find(o => o.value === val.trim());
                                                                                                                    return opt ? opt.label : val.trim();
                                                                                                                }).join(", ")
                                                                                                                : ""
                                                                                                        }
                                                                                                    </td>
                                                                                                    <td className="text-center" style={{ wordBreak: "break-word", whiteSpace: "normal" }}>

                                                                                                        {(() => {
                                                                                                            const opt = packingsize.find(o => String(o.value) === String(row.packingMaterial));
                                                                                                            return opt?.label || row.packingMaterial || "-";
                                                                                                        })()}
                                                                                                    </td>
                                                                                                    <td className="text-center">{row.Quantity}</td>
                                                                                                    {/* <td className="text-center">{row.crateCount}</td> */}
                                                                                                    <td className="text-center">{row.Weight}</td>
                                                                                                    <td className="text-center">{formData.Rate}</td>
                                                                                                </tr>
                                                                                            ))
                                                                                    ) : (
                                                                                        <tr>
                                                                                            <td colSpan="9" className="text-center">डेटा उपलब्ध नाही</td>
                                                                                        </tr>
                                                                                    )}
                                                                            </tbody>
                                                                            {/* <tfoot>
                                                                                <tr>
                                                                                    <td colSpan="7" className="text-end"><strong>Total Amount:</strong></td>
                                                                                    <td className="text-center"><strong>{totalAmount.toFixed(2)}</strong></td>
                                                                                </tr>
                                                                            </tfoot> */}
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div style={{
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
                                                                    {/* Add additional content here if needed */}
                                                                </div>
                                                                <div style={{
                                                                    display: 'flex',
                                                                    flexDirection: isMobile ? 'column' : 'row',
                                                                    gap: '10px',
                                                                    width: isMobile ? '100%' : 'auto'
                                                                }}>
                                                                    <div style={{
                                                                        fontWeight: 'bold',
                                                                        border: '1px solid #ccc',
                                                                        padding: '6px 16px',
                                                                        borderRadius: '4px',
                                                                        backgroundColor: 'rgb(52, 58, 64)',
                                                                        minWidth: '140px',
                                                                        textAlign: 'right',
                                                                        width: isMobile ? '100%' : 'auto',
                                                                        color: 'white',
                                                                        size: '14px',
                                                                        fontWeight: 'bold'

                                                                    }}>
                                                                        ₹ {totalAmount.toFixed(2)}(Total Amount)
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div className="row">
                                                    <div className="col-6 col-md-6 col-lg-2 mb-3">
                                                        <div className="mb-0 add-product">
                                                            <label htmlFor="total" className="form-label required">Total Bill Amount</label>
                                                            <input
                                                                type="number"
                                                                className="form-control"
                                                                name="total"
                                                                value={totalAmount.toFixed(2)}
                                                                readOnly
                                                            />
                                                        </div>
                                                    </div>

                                                    <div className="col-6 col-md-6 col-lg-2 mb-3">
                                                        <div className="mb-0 add-product">
                                                            <label htmlFor="Advance" className="form-label required">Advance</label>
                                                            <input
                                                                type="number"
                                                                className="form-control"
                                                                name="Advance"
                                                                value={formData.Advance}
                                                                onChange={(e) => setFormData({ ...formData, Advance: e.target.value })}
                                                                readOnly
                                                            />
                                                        </div>
                                                    </div>

                                                    <div className="col-6 col-md-6 col-lg-2 mb-3">
                                                        <div className="mb-0 add-product">
                                                            <label htmlFor="TotalAmt" className="form-label required">Total Amount</label>
                                                            <input
                                                                type="number"
                                                                className="form-control"
                                                                name="TotalAmt"
                                                                value={formData.TotalAmt}
                                                                onChange={(e) => setFormData({ ...formData, TotalAmt: e.target.value })}
                                                            />
                                                        </div>
                                                    </div>

                                                    <div className="col-6 col-md-6 col-lg-2 mb-3">
                                                        <div className="mb-0 add-product">
                                                            <label htmlFor="ExtraAdvance" className="form-label">Extra Advance</label>
                                                            <input
                                                                type="number"
                                                                className="form-control"
                                                                name="ExtraAdvance"
                                                                value={formData.ExtraAdvance}
                                                                onChange={(e) => setFormData({ ...formData, ExtraAdvance: e.target.value })}

                                                            />
                                                        </div>
                                                    </div>

                                                    <div className="col-lg-12 col-md-12 mb-2">
                                                        <label htmlFor="Narration" className="form-label">Narration</label>
                                                        <input
                                                            ref={RefNaration}
                                                            id="Narration"
                                                            className="form-control"
                                                            name="Narration"
                                                            onChange={(e) => setFormData({ ...formData, Narration: e.target.value })}
                                                            value={formData.Narration}
                                                            placeholder="Enter Discription"
                                                        />
                                                    </div>
                                                </div>

                                                <div className="col-lg-12">
                                                    <div className="text-end">
                                                        <div className="row justify-content-end">
                                                            <div className="col-12 col-md-1 mb-2 order-2 order-md-1">
                                                                <button
                                                                    type="button"
                                                                    className="btn btn-submit w-100"
                                                                    to="#" data-bs-toggle="modal"
                                                                    data-bs-target="#print-receipt"
                                                                    onClick={e => {
                                                                        e.preventDefault();
                                                                        openModal(CROPID);
                                                                    }}
                                                                >
                                                                    Print Bill
                                                                </button>
                                                            </div>
                                                            {/* Save Button - First on mobile, second on desktop */}
                                                            <div className="col-12 col-md-1 mb-2 order-1 order-md-2">
                                                                <button
                                                                    type="button"
                                                                    className="btn btn-submit w-100"
                                                                    ref={SaveRef}
                                                                    onClick={handleFinalSave}
                                                                >
                                                                    Save
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
                                                                    Back
                                                                </button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                {/* <div className="col-12 col-md-6 col-lg-5 mb-3">
                                                    <div className="d-flex justify-content-center justify-content-lg-end align-items-center flex-column flex-md-row gap-2 mt-4">
                                                        <button
                                                            type="submit"
                                                            className="btn btn-submit w-100 w-md-auto"
                                                            ref={SaveRef}
                                                        >
                                                            Save
                                                        </button>
                                                        <button
                                                            type="button"
                                                            className="btn btn-cancel w-100 w-md-auto"
                                                            data-bs-dismiss="modal"
                                                            aria-label="Close"
                                                        >
                                                            Back
                                                        </button>

                                                        <Link
                                                            // className="me-2 p-2"
                                                            to="#" data-bs-toggle="modal"
                                                            data-bs-target="#print-receipt"
                                                            className="btn btn-primary"
                                                            // style={{ color: 'orange' }}
                                                            // onClick={() => openModal(record.cropid)}
                                                            onClick={e => {
                                                                e.preventDefault();
                                                                openModal(CROPID);
                                                            }}
                                                        >

                                                            Print Bill
                                                        </Link>
                                                    </div>
                                                </div> */}
                                                <div className="text-center invoice-bar mt-2 no-print">
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
            <div
                className="modal fade modal-default"
                id="print-receipt"
                aria-labelledby="print-receipt"
            >
                <div className="modal-dialog modal-dialog-centered">
                    <div className="modal-content rounded-4 shadow border border-success" style={{ backgroundColor: "#f0f8ff" }}>
                        <div className="d-flex justify-content-end p-2">
                            <button
                                type="button"
                                className="close p-1"
                                data-bs-dismiss="modal"
                                aria-label="Close"
                            >
                                <span aria-hidden="true" class="fs-2">×</span>
                            </button>
                        </div>
                        <div className="modal-body" id="printableArea" style={{ backgroundColor: "#f0f8ff" }}>
                            <div className="icon-head text-center mb-3">
                                <h3 className="text-center" style={{ color: 'orange' }}>
                                    {userdetail?.departmentname}

                                </h3>
                            </div>
                            <div className="tax-invoice" style={{ backgroundColor: "#f0f8ff" }}>
                                <h6 className="text-center text-success">Farmer Bill</h6>
                                <div className="row mb-3">
                                    <div className="col-sm-12 col-md-6">
                                        <div className="invoice-user-name">
                                            <span className="fw-bold " style={{ fontWeight: 'bold' }}>Farmer: </span>
                                            <span style={{ fontWeight: 'bold' }}>
                                                {farmeName}
                                            </span>
                                        </div>
                                        <div className="invoice-user-name">
                                            <span className="fw-bold ">Mobile No: </span>
                                            <span style={{ fontWeight: 'bold' }}>
                                                {MoblieNo}
                                            </span>
                                        </div>
                                    </div>
                                    <div className="col-sm-12 col-md-6">
                                        <div className="invoice-user-name">
                                            <span className="fw-bold ">Date: </span>
                                            <span style={{ fontWeight: 'bold' }}>
                                                {Date123}
                                            </span>
                                        </div>
                                        <div className="invoice-user-name">
                                            <span className="fw-bold ">Crop: </span>
                                            <span style={{ fontWeight: 'bold' }}>
                                                {Crop}
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <table className="table-borderless w-100 table-fit">
                                <thead style={{ backgroundColor: '#add8e6' }}>
                                    <tr>
                                        <th className="text-center">Sr.No</th>
                                        {/* <th className="text-center">Variety </th>
                                            <th className="text-center">Packing </th> */}
                                        <th className="text-center">Crate </th>
                                        <th className="text-center">Rate </th>
                                        <th className="text-center">Weigth </th>
                                        <th className="text-end">Total </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {tabledata.length > 0 ? (
                                        tabledata.map((row, index) => (
                                            <tr key={index}>
                                                <td className="text-center" style={{ color: '#333' }}><strong>{index + 1}</strong></td>
                                                {/* <td className="text-center" style={{ color: '#333' }}><strong>{row.cvariety}</strong></td>
                                                    <td className="text-center" style={{ color: '#333' }}><strong>{row.psize}</strong></td> */}
                                                <td className="text-center" style={{ color: '#333' }}><strong>{row.cratecount}</strong></td>
                                                <td className="text-center" style={{ color: '#333' }}><strong>{row.rate}</strong></td>
                                                <td className="text-center" style={{ color: '#333' }}><strong>{row.weight}</strong></td>
                                                <td className="text-center" style={{ color: '#333' }}><strong>{row.rate * row.weight}</strong></td>
                                            </tr>
                                        ))
                                    ) : (
                                        <tr>
                                            <td colSpan="5" className="text-center">No Data Available</td>
                                        </tr>
                                    )}
                                </tbody>
                                <tfoot>
                                    <tr>
                                        <td colSpan="4" className="text-end"><strong>Total Amount:</strong></td>
                                        <td className="text-end" style={{ color: '#000' }}><strong>{totalAmount.toFixed(2)}</strong></td>
                                    </tr>
                                </tfoot>
                            </table>
                            {/* <div className="text-center invoice-bar mt-3">
                                <Link
                                    className="me-2 p-2"
                                    to="#"
                                    style={{ color: 'orange' }}
                                    onClick={() => openModal(row.cropid)} 
                                >
                                </Link>
                            </div> */}
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
};

export default AddCropBill;
