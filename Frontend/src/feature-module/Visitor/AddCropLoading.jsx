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
import BillReceipt from "./BillReceipt";
const AddCropLoading = ({ onRefresh, SLVMID, onSaved, refreshKey }) => {
    console.log("VAIDVAIDVAID", SLVMID);
    const { userdetail } = getUserData();
    const GUID = ACSPLGUID.getNew();
    const [Trader, setTrader] = useState([]);
    const MySwal = withReactContent(Swal);
    const Cropref = useRef(null);
    const Graderef = useRef(null);
    const Traderref = useRef(null);
    const VehicleNumberref = useRef(null);
    const CaretCountref = useRef(null);
    const SaveRef = useRef(null);


    const [formData, setFormData] = useState({
        Crop: [],
        Grade: [],
        Trader: "",
        VehicleNumber: "",
        CaretCount: "",
        PoNo: "",
        date: userdetail?.APPDT || "",
    });

    useEffect(() => {
        setFormData(prev => ({
            ...prev,
            date: userdetail?.APPDT || "",
        }));
    }, []);


    useEffect(() => {
        const handleShortcut = (e) => {
            if (e.ctrlKey && e.key === 's') {
                e.preventDefault();
                handleFinalSave();
            }
            if (e.ctrlKey && e.key === 'e') {
                e.preventDefault();
                // navigate(route.CropLoading);
                showExitAlert();
            }
        };
        window.addEventListener('keydown', handleShortcut);
        return () => {
            window.removeEventListener('keydown', handleShortcut);
        };
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
                        const crops = data.croP1 ? data.croP1.split(',') : [];
                        const grades = data.gradE1 ? data.gradE1.split(',') : [];
                        setFormData({
                            Crop: crops,
                            Grade: grades,
                            Trader: data.tradeR1 || "",
                            VehicleNumber: data.vehicleno || "",
                            CaretCount: data.caretcount || "",
                            PoNo: data.pono || ""
                        });
                        return { crops, grades };
                    }
                } catch (error) {
                    console.error("Error fetching data:", error);
                }
                return null;
            };
            const fetchDetailTableData = async (crops, grades) => {
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
                        `${baseUrl.Url}/backend/api/GET_StockLoadingVehicleDetail`,
                        payload1,
                        { headers }
                    );
                    if (response.status !== 200)
                        throw new Error("Failed to fetch SchedulerMaster data");
                    if (response.data.length > 0) {
                        let detailData = response.data;

                        // Fetch msize from CropVehicleLoadingData to calculate expense
                        if (crops && crops.length > 0 && grades && grades.length > 0) {
                            try {
                                const cropPayload = {
                                    "companyid": userdetail?.companyID || "",
                                    "deptid": userdetail?.departmentID || "",
                                    crop: crops.join(','),
                                    cgrade: grades.join(','),
                                };
                                const cropResponse = await axios.post(
                                    `${baseUrl.Url}/backend/api/GET_CropVehicleLoadingData`,
                                    cropPayload,
                                    { headers }
                                );
                                if (cropResponse.status === 200 && cropResponse.data.length > 0) {
                                    detailData = detailData.map(item => {
                                        const match = cropResponse.data.find(c => c.staid === item.staid);
                                        const msize = match ? parseFloat(match.msize || 0) : 0;
                                        const count = parseFloat(item.count || 0);
                                        const charges = parseFloat(item.charges || 0);
                                        const totalWeight = count * msize;
                                        const expense = msize > 0 ? totalWeight * charges : parseFloat(item.expense || 0);
                                        return { ...item, msize: match ? match.msize : "", expense };
                                    });
                                }
                            } catch (err) {
                                console.error("Error fetching msize data:", err);
                            }
                        }

                        setTableData1(detailData);
                    }
                } catch (error) {
                    console.error("Error fetching data:", error);
                }
            };
            fetchData().then((result) => {
                if (result) {
                    fetchDetailTableData(result.crops, result.grades);
                } else {
                    fetchDetailTableData();
                }
            });
        }
    }, [SLVMID, refreshKey]);

    useEffect(() => {
        const modal = document.getElementById('AddVehicleLoading');
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
        const { expenseTotal, amountTotal } = TableData1
            .filter(row => parseInt(row.isdeleted || 0) !== 1)
            .reduce(
                (acc, row) => {
                    acc.expenseTotal += parseFloat(row.expense) || 0;
                    acc.amountTotal += parseFloat(row.totalamount) || 0;
                    return acc;
                },
                { expenseTotal: 0, amountTotal: 0 }
            );
        try {
            const masterPayload = {
                slvmid: SLVMID || GUID,
                crop: formData.Crop?.join(',') || "",
                trader: formData.Trader,
                vehicleno: formData.VehicleNumber,
                caretcount: formData.CaretCount?.toString(),
                grade: formData.Grade?.join(',') || "",
                pono: formData.PoNo || "",
                uaid: userdetail?.uaid || "",
                companyid: userdetail?.companyID || "",
                deptid: userdetail?.departmentID || "",
                totex: expenseTotal?.toFixed(2) || 0,
                totiwdamt: amountTotal?.toFixed(2) || 0,
                ldate: userdetail?.APPDT || "01 jan 1900"
            };
            const masterResponse = await axios.post(
                `${baseUrl.Url}/backend/api/SP_AddUpdtStockLoadingVehicleMaster`,
                masterPayload,
                { headers: { 'Content-Type': 'application/json' } }
            );
            if (masterResponse.status !== 200) throw new Error("Master save failed");
            let updatedRows = [...TableData1];
            for (const row of TableData1.filter(r => parseFloat(r.count) > 0)) {
                const slvdidExists = updatedRows.some(item => item.slvdid === row.slvdid && item.slvdid !== undefined);
                if (slvdidExists) {
                    updatedRows = updatedRows.map(item =>
                        item.slvdid === row.slvdid
                            ? {
                                ...item,
                                count: row.count,
                                expense: row.expense,
                                totalamount: row.totalamount,
                            }
                            : item
                    );
                } else {
                    const newSlvdid = ACSPLGUID.getNew();
                    row.slvdid = newSlvdid;
                    updatedRows.push({ ...row });
                }
                const detailPayload = {
                    slvdid: row.slvdid,
                    slvmid: SLVMID || GUID,
                    date: row.trndt ? new Date(row.trndt).toISOString().split("T")[0] : null,
                    farmername: row?.faid || "",
                    totalcaret: row.totcr,
                    count: row.count,
                    expense: row.expense,
                    totalinwardamount: row.totalamount,
                    salevalue: 0,
                    uaid: userdetail?.uaid || "",
                    companyid: userdetail?.companyID || "",
                    deptid: userdetail?.departmentID || "",
                    cgrade: row?.cgrade || "",
                    crop: row?.crop || "",
                    staid: row?.staid || ""
                };
                console.log(detailPayload, "detailPayload")
                const detailResponse = await axios.post(
                    `${baseUrl.Url}/backend/api/SP_AddUpdStockLoadingVehicleDetail`,
                    detailPayload,
                    { headers: { 'Content-Type': 'application/json' } }
                );
                if (detailResponse.status !== 200) throw new Error("Detail save failed");
                const stockDetailPayload = {
                    staid: row?.slvdid || "",
                    trndt: userdetail?.APPDT || "",
                    crop: row?.crop || "",
                    faid: row.faid || "",
                    ciaid: row?.ciaid || "",
                    totcr: row?.count.toString() || "",
                    totwt: parseInt(row?.count) * parseInt(row?.msize || 0) || 0,
                    trntid: "O",
                    cgrade: row?.cgrade || "",
                    isdeleted: false,
                    companyid: userdetail?.companyID || "",
                    deptid: userdetail?.departmentID || "",
                    reff: row?.staid || "",
                    rtotcr: (parseFloat(row.totcr || 0) - parseFloat(row.count || 0)).toString()
                };
                const stockDetailResponse = await axios.post(
                    `${baseUrl.Url}/backend/api/SP_AddUpdStockDetails`,
                    [stockDetailPayload],
                    { headers: { 'Content-Type': 'application/json' } }
                );

                if (stockDetailResponse.status !== 200) throw new Error("Stock detail save failed");
            }
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
                    const modal = document.getElementById("AddVehicleLoading");
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
                PoNo: "",
            });
            setTableData1([]);

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
                PoNo: "",
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
                    PoNo: "",
                })
                setTableData1([])
                // const modal = document.getElementById("AddVehicleLoading");
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


                const modalElement = document.getElementById("AddVehicleLoading");
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
        const modalEl = document.getElementById('AddVehicleLoading');
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
        if (name === 'count' && inputCount > parseFloat(row.totcr || 0)) {
            Swal.fire({
                icon: 'warning',
                title: 'Invalid Count',
                text: `Count cannot be greater than Total Caret (${row.totcr})`,
            });
            return;
        }

        const updatedData = TableData1.map(item => {
            if (item === row) {
                const newCount = name === 'count' ? inputCount : parseFloat(item.count || 0);
                const rate = parseFloat(row.rate || 0);
                const charges = parseFloat(item.charges || 0);
                const msize = parseFloat(item.msize || 0);
                const totalWeight = newCount * msize;
                const totalamount = newCount * rate;
                let expense;
                if (name === 'expense') {
                    expense = parseFloat(value) || 0;
                } else if (msize > 0) {
                    expense = totalWeight * charges;
                } else {
                    expense = parseFloat(item.expense || 0);
                }
                return {
                    ...item,
                    [name]: value,
                    count: newCount,
                    totalamount: isNaN(totalamount) ? 0 : totalamount,
                    expense: isNaN(expense) ? 0 : expense,
                };
            }
            return item;
        });

        setTableData1(updatedData);

        const totalCaretCount = updatedData.reduce((sum, item) => {
            return sum + (parseFloat(item.count) || 0);
        }, 0);

        setFormData(prevForm => ({
            ...prevForm,
            CaretCount: totalCaretCount
        }));
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
    const closeModal = () => {
        const modal = document.getElementById("AddVehicleLoading");
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
        const modal = document.getElementById("AddVehicleLoading");
        if (modal && modal.classList.contains("show")) {
            closeModal();
        }
    });
    return (
        <>
            <div className="modal fade" id="AddVehicleLoading">
                <div className="modal-dialog modal-dialog-centered modal-fullscreen">
                    <div className="modal-content mbgcolor">
                        <div className="page-wrapper-new p-0">
                            <div className="content">
                                <div className="modal-header border-0 custom-modal-header" style={{ padding: '5px' }}>
                                    <div className="page-title">
                                        <h4>Add Vehicle Loading</h4>
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
                                                        <div className="col-lg-1 col-sm-6 mb-3">
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
                                                        <div className="col-lg-3 col-md-6 col-12 mb-3">
                                                            <label className="form-label required">Crop</label>
                                                            <Select
                                                                openMenuOnFocus={true}
                                                                isMulti
                                                                ref={Cropref}
                                                                placeholder="Please Select Crop"
                                                                classNamePrefix="react-select"
                                                                required
                                                                options={Cropselect}
                                                                value={Cropselect.filter(option => formData.Crop.includes(option.value))}
                                                                onChange={(selectedOptions) => {
                                                                    console.log("Crop changed:", selectedOptions);
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
                                                                openMenuOnFocus={true}
                                                                classNamePrefix="react-select"
                                                                required
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
                                                                name="VehicleNumber"
                                                                required
                                                                value={formData.VehicleNumber}
                                                                onChange={handleChange}
                                                            />
                                                        </div>
                                                        <div className="col-lg-2 col-md-6 mb-3 col-6">
                                                            <label htmlFor="PoNo" className="form-label">PO No</label>
                                                            <input
                                                                placeholder="Enter PO No"
                                                                type="text"
                                                                id="PoNo"
                                                                className="form-control"
                                                                name="PoNo"
                                                                value={formData.PoNo}
                                                                onChange={handleChange}
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
                                                    </div>
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
                                                                                <th className="text-center">Date</th>
                                                                                <th className="text-center" style={{ width: "250px" }}>Farmer</th>
                                                                                <th className="text-center">Grade</th>
                                                                                <th className="text-center">Total Crate</th>
                                                                                <th className="text-center">Count</th>
                                                                                <th className="text-center"> Expense</th>
                                                                                <th className="text-center">Total Inward Amount</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            {
                                                                                TableData1.length > 0 ? (
                                                                                    TableData1.map((row, index) => (
                                                                                        <tr key={index}>
                                                                                            <td className="text-center">{row.trndt || index}</td>
                                                                                            <td className="text-center">{row.fname}</td>
                                                                                            <td className="text-center">{row.grade}</td>
                                                                                            <td className="text-center">{row.totcr}</td>
                                                                                            <td className="text-center">
                                                                                                <input
                                                                                                    min={0}
                                                                                                    type="number"
                                                                                                    name="count"
                                                                                                    className="form-control text-center"
                                                                                                    value={row.count || ""}
                                                                                                    onChange={(e) => handleRateChange(e, row)}
                                                                                                />
                                                                                            </td>
                                                                                            <td className="text-center">
                                                                                                <input
                                                                                                    min={0}
                                                                                                    type="number"
                                                                                                    name="expense"
                                                                                                    className="form-control text-center"
                                                                                                    value={row.expense || ""}
                                                                                                    onChange={(e) => handleRateChange(e, row)}
                                                                                                />
                                                                                            </td>
                                                                                            <td className="text-center">
                                                                                                ₹ {row.totalamount ? row.totalamount.toFixed(2) : "0.00"}
                                                                                            </td>
                                                                                        </tr>
                                                                                    ))
                                                                                ) : (
                                                                                    <tr>
                                                                                        <td colSpan="6" className="text-center">No Data Available</td>
                                                                                    </tr>
                                                                                )
                                                                            }
                                                                        </tbody>
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
                                                            </div>
                                                            <div style={{
                                                                display: 'flex',
                                                                flexDirection: isMobile ? 'column' : 'row',
                                                                gap: '10px',
                                                                width: isMobile ? '100%' : 'auto'
                                                            }}>
                                                                {(() => {
                                                                    const activeRows = TableData1.filter(row => parseInt(row.isdeleted || 0) !== 1);
                                                                    const expenseTotal = activeRows.reduce((sum, row) => sum + (parseFloat(row.expense) || 0), 0);
                                                                    const amountTotal = activeRows.reduce((sum, row) => sum + (parseFloat(row.totalamount) || 0), 0);
                                                                    const grandTotal = expenseTotal + amountTotal;
                                                                    return (
                                                                        <div style={{
                                                                            fontWeight: 'bold',
                                                                            fontSize: '16px',
                                                                            border: '2px solid #092C4C',
                                                                            padding: '8px 20px',
                                                                            borderRadius: '6px',
                                                                            backgroundColor: '#fff3cd',
                                                                            minWidth: '200px',
                                                                            textAlign: 'right',
                                                                            width: isMobile ? '100%' : 'auto',
                                                                            color: '#092C4C'
                                                                        }}>
                                                                            <span>₹ {expenseTotal.toFixed(2)} (Expense)</span>
                                                                            <span style={{ margin: '0 6px' }}>+</span>
                                                                            <span>₹ {amountTotal.toFixed(2)} (Total)</span>
                                                                            <span style={{ margin: '0 6px' }}>=</span>
                                                                            <span style={{
                                                                                fontSize: '18px',
                                                                                fontWeight: '800',
                                                                                color: '#d63384',
                                                                                backgroundColor: '#fff',
                                                                                padding: '2px 10px',
                                                                                borderRadius: '4px',
                                                                                border: '1px solid #d63384'
                                                                            }}>
                                                                                ₹ {grandTotal.toFixed(2)}
                                                                            </span>
                                                                        </div>
                                                                    );
                                                                })()}
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div className="col-lg-12">
                                                    <div className="text-end">
                                                        <div className="row justify-content-end">
                                                            <div className="col-12 col-md-1 mb-2 order-1 order-md-3">
                                                                <button
                                                                    type="button"
                                                                    className="btn btn-submit w-100"
                                                                    ref={SaveRef} onClick={handleFinalSave}
                                                                >
                                                                    Save
                                                                </button>
                                                            </div>
                                                            <div className="col-12 col-md-1 mb-2 order-2 order-md-2">
                                                                <button
                                                                    type="button"
                                                                    className="btn btn-info w-100 text-white"
                                                                    onClick={() => {
                                                                        const modalElement = document.getElementById('loading-bill-receipt');
                                                                        if (modalElement) {
                                                                            const modal = window.bootstrap.Modal.getOrCreateInstance(modalElement);
                                                                            modal.show();
                                                                        }
                                                                    }}
                                                                >
                                                                    Print Bill
                                                                </button>
                                                            </div>
                                                            <div className="col-12 col-md-1 mb-2 order-3 order-md-1">
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

            <BillReceipt
                modalId="loading-bill-receipt"
                billNo={formData.PoNo || SLVMID || ''}
                date={formData.date ? formatDate_1(formData.date) : ''}
                farmerName={Trader.find(t => t.value === formData.Trader)?.label || ''}
                mobileNo=""
                vehNo={formData.VehicleNumber || ''}
                tableData={TableData1.filter(r => parseInt(r.isdeleted || 0) !== 1 && parseFloat(r.count) > 0).map(r => ({
                    cratecount: r.count,
                    rate: r.expense ? (r.expense / (parseFloat(r.count) || 1)).toFixed(2) : '0',
                    weight: r.totalamount ? r.totalamount.toFixed(2) : '0'
                }))}
                totalAmount={TableData1.filter(r => parseInt(r.isdeleted || 0) !== 1).reduce((sum, r) => sum + (parseFloat(r.totalamount) || 0), 0)}
            />
        </>
    );
};
export default AddCropLoading;

