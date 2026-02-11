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
const AddCropInvert = ({ SHAID, VDAID, FAID }) => {
    const [EndDate, setEndDate] = useState('');
    const [StartDate, setStartDate] = useState('');
    const [Employee, setEmployee] = useState([]);
    const [packingsize, setpackingsize] = useState([]);
    const { isAuthenticated, userdetail } = getUserData();
    const ScheduleDateref = useRef(null);
    const packerRef = useRef(null);
    const VehicleNoRef = useRef(null);
    const CrateCountRef = useRef(null);
    const PackingMaterialRef = useRef(null);
    const WeightRef = useRef(null);
    const AmountRef = useRef(null);
    const [Transections, setTransections] = useState();
    const [isEditing, setIsEditing] = useState(false);

    if (isAuthenticated == true) {
        console.log("user", userdetail);
        console.log("getUserData", getUserData);
    }
    console.log("AddCropInvert props", SHAID, VDAID, FAID);
    const MySwal = withReactContent(Swal);
    useEffect(() => {
        getTodayDate();
    }, [SHAID,]);
    const getTodayDate = () => {
        const today = new Date();
        const formatted = today.toLocaleDateString("en-GB", {
            day: "2-digit",
            month: "short",
            year: "numeric",
        });
        setEndDate(formatted);
    };
    function getTodayDate2() {
        const today = new Date();
        const year = today.getFullYear();
        const month = String(today.getMonth() + 1).padStart(2, '0');
        const day = String(today.getDate()).padStart(2, '0');
        return `${year}-${month}-${day}`;
    }
    const formatDateToDisplay = (dateStr) => {
        if (!dateStr) return "";
        const date = new Date(dateStr);
        const day = String(date.getDate()).padStart(2, "0");
        const month = date.toLocaleString("en-US", { month: "short" });
        const year = date.getFullYear();
        return `${day} ${month} ${year}`;
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
        ExpectedHarvesting: "",
        CropVariety: "",
        Narration: "",
        cropInvert: "",
        Rate: "",
        ctype: "",
        Weight: "",
        caret: "",
        Amount: "",
    });
    const [detailData, setDetailData] = useState({
        ScheduleDate: "",
        cdaid: "",
        shaid: "",
        SHDAID: "",
        staid: "",
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
        GradeName: "",
        Grade: "",
        Quantity: "",
        isdeleted: 0,
    });

    const GUID = ACSPLGUID.getNew();
    // const totalAmount = TableData.reduce((sum, row) => sum + (parseFloat(row.total) || 0), 0).toFixed(2);
    const RefOutWardStockWeight = useRef(null);
    const RefNaration = useRef(null);
    const Refvyavarmode = useRef(null)
    const SaveRef = useRef(null);
    const handleSelectChange = (selectedOption, { name }) => {
        setFormData((prevState) => ({
            ...prevState,
            [name]: selectedOption?.value || "",
        }));
    };

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


    const handleSubmit = (e) => {
        e.preventDefault();
        const form = e.target.closest("form");
        if (!form.checkValidity()) {
            form.reportValidity();
            return;
        }
        const isValid = validateForm();
        if (!isValid) return;
        showConfirmationAlert(e);
    };

    const validateForm = () => {
        if (!formData.Rate) {
            Swal.fire({
                icon: "error",
                title: "Error",
                text: "Please Enter Rate",
            }).then(() => {
                CrateCountRef?.current?.focus();
            });
            return false;
        }
        if (!formData.Weight) {
            Swal.fire({
                icon: "error",
                title: "Error",
                text: "Please Enter Weight",
            }).then(() => {
                WeightRef?.current?.focus();
            });
            return false;
        }
        if (!formData.caret) {
            Swal.fire({
                icon: "error",
                title: "Error",
                text: "Please Enter Crate Count",
            }).then(() => {
                CrateCountRef?.current?.focus();
            });
            return false;
        }
        if (!formData.Amount) {
            Swal.fire({
                icon: "error",
                title: "Error",
                text: "Please Enter Amount",
            }).then(() => {
                AmountRef?.current?.focus();
            });
            return false;
        }
        return true;
    };

    const showConfirmationAlert = (e) => {
        MySwal.fire({
            title: "Are you sure?",
            text: "Do you want to save this data?",
            showCancelButton: true,
            confirmButtonColor: "#00ff00",
            confirmButtonText: "Save",
            cancelButtonColor: "#092C4C",
            cancelButtonText: "Cancel",
            allowOutsideClick: false,
            allowEscapeKey: false,
        }).then((result) => {
            if (result.isConfirmed) {
                handleFormSubmission(e);
            }
        });
    };
    const checkDateExists = async (SHAID, SHSDATE, Grade) => {
        try {
            const response = await axios.post(
                `${baseUrl.Url}/backend/api/GET_CropInvertDateCheck`,
                {
                    shaid: SHAID,
                    shsdate: SHSDATE,
                    cgrade: Grade
                },
                { headers: { 'Content-Type': 'application/json' } }
            );
            const dateExistsValue = response?.data?.[0]?.dateExists;
            if (dateExistsValue === 0) {
                return 0;
            } else {
                return 1;
            }
        } catch (error) {
            console.error("Date check failed:", error);
            return 1;
        }
    };

    function formatDateString(dateStr) {
        if (!dateStr) return "";
        const date = new Date(dateStr);
        const day = date.getDate(); // 1-31
        const month = date.toLocaleString('en-US', { month: 'short' }); // Jan, Feb, ...
        const year = date.getFullYear();
        return `${day} ${month} ${year}`;
    }

    // const handleFormSubmission = async (e) => {
    //     e.preventDefault();
    //     let cropidToRollback = null;
    //     let cdaidToRollback = null;
    //     const NewPKID = ACSPLGUID.getNew();
    //     const activeDetails = TableData.filter(row => parseInt(row.isdeleted) !== 1);
    //     if (activeDetails.length === 0) {
    //         Swal.fire({
    //             icon: 'error',
    //             title: 'Error',
    //             text: 'Please add at least one detail record.'
    //         });
    //         return;
    //     }
    //     try {
    //         const latestTransactions = await fetchLatestTransactions(VDAID, FAID);

    //         const masterPayload = {
    //             cropid: formData.cropid ? formData.cropid : NewPKID,
    //             crdate: TodaysDate,
    //             faid: formData.faid,
    //             ctype: formData.ctype,
    //             carea: formData.CropArea,
    //             contactno: formData.PhoneNo,
    //             shaid: SHAID,
    //             narr: formData.Narration || "",
    //             eproduction: formData.ExpectedHarvesting,
    //             cvariety: formData.CropVariety,
    //             cropInvert: formData.cropInvert,
    //             Rate: formData.Rate,
    //             weight: formData.Weight,
    //             caret: formData.caret,
    //             amount: formData.Amount,
    //             companyid: userdetail?.companyID || "",
    //             deptid: userdetail?.departmentID || "",
    //             iscomplited: "1"
    //         };
    //         const masterResponse = await axios.post(
    //             `${baseUrl.Url}/backend/api/SP_AddUpdCropInvertMaster`,
    //             masterPayload,
    //             { headers: { 'Content-Type': 'application/json' } }
    //         );
    //         if (masterResponse.status !== 200) throw new Error("Master save failed");
    //         cropidToRollback = masterPayload.cropid;
    //         const formatDate = (date) => {
    //             if (!date) return null;
    //             const d = new Date(date);
    //             if (isNaN(d.getTime())) return null;
    //             return d.toISOString().split('T')[0];
    //         };
    //         const detailPayloadList = [];
    //         const schedulerPayloadList = [];
    //         for (const row of TableData) {

    //             console.log("Editing row:", row.cdaid, row.shdaid, row.staid);
    //             // const formattedDate = formatDate(row.ScheduleDate);
    //             // if (!formattedDate) {
    //             //     throw new Error("Schedule Date is missing or invalid for some rows.");
    //             // }



    //             let shdaid = null;
    //             shdaid = ACSPLGUID.getNew();
    //             const schedulerPayload = {
    //                 shdaid: row?.shdaid ? row?.shdaid : shdaid,
    //                 shaid: SHAID,
    //                 shsdate: formatDateString(row.ScheduleDate),
    //                 vdaid: VDAID || "",
    //                 packer: Array.isArray(row.packer) ? row.packer.join(',') : row.packer,
    //                 worker: Array.isArray(row.labour) ? row.labour.join(',') : row.labour,
    //                 vehno: row.VehicleNo,
    //                 psize: row.packingMaterial,
    //                 pquntity: row.crateCount.toString(),
    //                 isdeleted: row.isdeleted === 1 || row.isdeleted === true ? true : false,
    //                 cgrade: row?.Grade || ""
    //             };
    //             schedulerPayloadList.push(schedulerPayload);
    //             const CDAID = ACSPLGUID.getNew();
    //             if (parseFloat(row.crateCount) > 0 && parseFloat(row.Weight) > 0) {
    //                 const detailPayload = {
    //                     cdaid: row.cdaid ? row.cdaid : CDAID,
    //                     cropid: cropidToRollback,
    //                     shaid: SHAID,
    //                     shdaid: shdaid,
    //                     shsdate: formatDateString(row.ScheduleDate),
    //                     vehno: row.VehicleNo,
    //                     packer: Array.isArray(row.packer) ? row.packer.join(',') : row.packer,
    //                     worker: Array.isArray(row.labour) ? row.labour.join(',') : row.labour,
    //                     psize: row.packingMaterial,
    //                     pquntity: row.Quantity || row.crateCount,
    //                     cratecount: row.crateCount.toString(),
    //                     cropinvert: row?.Grade || "",
    //                     Weight: row.Weight,
    //                     companyid: userdetail?.companyID || "",
    //                     deptid: userdetail?.departmentID || "",
    //                     isdeleted: row.isdeleted === 1 || row.isdeleted === true ? true : false,
    //                     cgrade: row?.Grade || "",
    //                     vdaid: VDAID || ""
    //                 };


    //                 detailPayloadList.push(detailPayload);
    //                 cdaidToRollback = detailPayload.cdaid;
    //                 const crateCount = parseFloat(detailPayload.pquntity) || 0;
    //                 const weight = parseFloat(detailPayload.Weight) || 0;
    //                 if (crateCount > 0 && weight > 0) {
    //                     const stockDetailPayload = {
    //                         staid: row.cdaid ? row.cdaid : CDAID,
    //                         trndt: TodaysDate,
    //                         crop: formData.ctype,
    //                         faid: formData.faid,
    //                         ciaid: cdaidToRollback,
    //                         totcr: crateCount.toString(),
    //                         totwt: weight,
    //                         trntid: "I",
    //                         cgrade: detailPayload.cgrade,
    //                         isdeleted: detailPayload.isdeleted,
    //                         companyid: userdetail?.companyID || "",
    //                         deptid: userdetail?.departmentID || "",
    //                         reff: "",
    //                         rtotcr: crateCount.toString()
    //                     };


    //                     const lastTransaction = latestTransactions.reduce((max, tx) => {
    //                         return tx.trnsr > max.trnsr ? tx : max;
    //                     }, { trnsr: 0, cbal: 0 });

    //                     const nextTrnsr = lastTransaction.trnsr + 1;
    //                     const newAmount = parseFloat(row.Weight) * parseFloat(formData.Rate) || 0;
    //                     const nextCbal = lastTransaction.cbal + newAmount;

    //                     let ADVANCEResponse;
    //                     if (lastTransaction.traid !== row.cdaid) {
    //                         const advancepaylod = {
    //                             traid: CDAID,
    //                             trnsr: nextTrnsr,
    //                             trnsid: "40",
    //                             trntid: "10",
    //                             trndt: formatDateString(row.ScheduleDate),
    //                             refaid: VDAID,
    //                             maid: FAID || "",
    //                             pamt: newAmount,
    //                             cbal: nextCbal,
    //                             advamt: lastTransaction.advamt || 0,
    //                             asid: SHAID || ACSPLGUID.getNew(),
    //                             isdeleted: false,
    //                             companyid: userdetail?.companyID || "",
    //                             deptid: userdetail?.departmentID || "",
    //                         };

    //                         ADVANCEResponse = await axios.post(
    //                             `${baseUrl.Url}/backend/api/SP_AddUpdAllTransections`,
    //                             [advancepaylod],
    //                             { headers: { 'Content-Type': 'application/json' } }
    //                         );
    //                     }

    //                     const stockResponse = await axios.post(
    //                         `${baseUrl.Url}/backend/api/SP_AddUpdStockDetails`,
    //                         [stockDetailPayload],
    //                         { headers: { 'Content-Type': 'application/json' } }
    //                     );

    //                     console.log("StockDetails Saved:", stockResponse.data);
    //                     if (ADVANCEResponse?.status === 200) {
    //                         console.log("ADVANCEResponse Saved:", ADVANCEResponse.data);
    //                         latestTransactions.push({
    //                             trnsr: nextTrnsr,
    //                             cbal: nextCbal,
    //                             traid: CDAID
    //                         });
    //                     }
    //                     // } catch (stockErr) {
    //                     //     console.error("StockDetails Save Failed:", stockErr);
    //                     //     throw new Error("Failed to save Stock Details");
    //                     // }
    //                 }
    //             }
    //         }
    //         const detailResponse = await axios.post(
    //             `${baseUrl.Url}/backend/api/SP_AddUpdCropInvertDetail`,
    //             detailPayloadList,
    //             { headers: { 'Content-Type': 'application/json' } }
    //         );
    //         if (detailResponse.status !== 200) throw new Error("Detail save failed");
    //         if (schedulerPayloadList.length > 0) {
    //             const schedulerResponse = await axios.post(
    //                 `${baseUrl.Url}/backend/api/SP_AddUpdShedulerDetail_Invard`,
    //                 schedulerPayloadList,
    //                 { headers: { 'Content-Type': 'application/json' } }
    //             );
    //             if (schedulerResponse.status !== 200) throw new Error("Scheduler detail save failed");
    //         }
    //         Swal.fire({
    //             icon: 'success',
    //             title: 'Success',
    //             text: 'Data has been saved successfully!',
    //             allowOutsideClick: false
    //         }).then(() => {

    //             const modal = document.getElementById("add-units-category");
    //             if (modal) {
    //                 modal.classList.remove("show");
    //                 modal.style.display = "none";
    //                 modal.setAttribute("aria-hidden", "true");
    //                 document.querySelectorAll(".modal-backdrop").forEach(backdrop => {
    //                     backdrop.remove();
    //                 });
    //                 document.body.classList.remove("modal-open");
    //                 document.body.style.overflow = "";
    //                 document.body.style.paddingRight = "";
    //             }

    //             if (!formData.cropid) {
    //                 setTableData([]);
    //                 setRows([]);
    //                 setFormData({
    //                     cropid: "",
    //                     FarmerName: "",
    //                     PhoneNo: "",
    //                     CropName: "",
    //                     CropArea: "",
    //                     ExpectedHarvesting: "",
    //                     CropVariety: "",
    //                     cropInvert: "",
    //                     Rate: "",
    //                     Narration: "",
    //                     Weight: "",
    //                     caret: "",
    //                     faid: "",
    //                     ctype: "",
    //                     Amount: "",
    //                 });
    //                 setDetailData({
    //                     ScheduleDate: "",
    //                     packer: "",
    //                     labour: "",
    //                     VehicleNo: "",
    //                     crateCount: "",
    //                     packingMaterial: "",
    //                     isdeleted: 0
    //                 });
    //             }
    //         });
    //     } catch (error) {
    //         console.error("Save failed:", error);
    //         Swal.fire({
    //             icon: 'error',
    //             title: 'Save Error',
    //             text: error.message || 'Server Error'
    //         });
    //     }
    // };

    const handleFormSubmission = async (e) => {
        e.preventDefault();

        let cropidToRollback = null;
        let cdaidToRollback = null;
        const NewPKID = ACSPLGUID.getNew();

        // Filter active rows
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
            // Get fresh transactions sorted ascending
            let latestTransactions = await fetchLatestTransactions(VDAID, FAID);
            if (!Array.isArray(latestTransactions)) latestTransactions = [];
            latestTransactions.sort((a, b) => (Number(a.trnsr) || 0) - (Number(b.trnsr) || 0));

            // Save master
            const masterPayload = {
                cropid: formData.cropid || NewPKID,
                crdate: TodaysDate,
                faid: formData.faid,
                ctype: formData.ctype,
                carea: formData.CropArea,
                contactno: formData.PhoneNo,
                shaid: SHAID,
                narr: formData.Narration || "",
                eproduction: formData.ExpectedHarvesting,
                cvariety: formData.CropVariety,
                cropInvert: formData.cropInvert,
                Rate: formData.Rate,
                weight: formData.Weight,
                caret: formData.caret,
                amount: formData.Amount,
                companyid: userdetail?.companyID || "",
                deptid: userdetail?.departmentID || "",
                iscomplited: "1"
            };

            const masterResponse = await axios.post(
                `${baseUrl.Url}/backend/api/SP_AddUpdCropInvertMaster`,
                masterPayload,
                { headers: { 'Content-Type': 'application/json' } }
            );
            if (masterResponse.status !== 200) throw new Error("Master save failed");
            cropidToRollback = masterPayload.cropid;

            const formatDate = (date) => {
                if (!date) return null;
                const d = new Date(date);
                return isNaN(d.getTime()) ? null : d.toISOString().split('T')[0];
            };

            const detailPayloadList = [];
            const schedulerPayloadList = [];

            for (const row of TableData) {
                let shdaid = null;
                shdaid = ACSPLGUID.getNew();
                schedulerPayloadList.push({
                    shdaid: row?.shdaid ? row?.shdaid : shdaid,
                    shaid: SHAID,
                    shsdate: formatDate(row.ScheduleDate),
                    vdaid: VDAID || "",
                    packer: Array.isArray(row.packer) ? row.packer.join(',') : row.packer,
                    worker: Array.isArray(row.labour) ? row.labour.join(',') : row.labour,
                    vehno: row.VehicleNo,
                    psize: row.packingMaterial,
                    pquntity: row.crateCount.toString(),
                    isdeleted: row.isdeleted === 1 || row.isdeleted === true,
                    cgrade: row?.Grade || ""
                });

                const CDAID = row?.cdaid || ACSPLGUID.getNew();

                if (parseFloat(row.crateCount) > 0 && parseFloat(row.Weight) > 0) {
                    detailPayloadList.push({
                        cdaid: CDAID,
                        cropid: cropidToRollback,
                        shaid: SHAID,
                        shdaid,
                        shsdate: formatDate(row.ScheduleDate),
                        vehno: row.VehicleNo,
                        packer: Array.isArray(row.packer) ? row.packer.join(',') : row.packer,
                        worker: Array.isArray(row.labour) ? row.labour.join(',') : row.labour,
                        psize: row.packingMaterial,
                        pquntity: row.Quantity || row.crateCount,
                        cratecount: row.crateCount.toString(),
                        cropinvert: row?.Grade || "",
                        Weight: row.Weight,
                        companyid: userdetail?.companyID || "",
                        deptid: userdetail?.departmentID || "",
                        isdeleted: row.isdeleted === 1 || row.isdeleted === true,
                        cgrade: row?.Grade || "",
                        vdaid: VDAID || ""
                    });

                    cdaidToRollback = CDAID;

                    // REFRESH latest transactions for accurate CBAL/TRNSR
                    let freshTx = await fetchLatestTransactions(VDAID, FAID);
                    if (!Array.isArray(freshTx)) freshTx = [];
                    freshTx.sort((a, b) => (Number(a.trnsr) || 0) - (Number(b.trnsr) || 0));

                    const lastTx = freshTx.length > 0
                        ? freshTx[freshTx.length - 1]
                        : { trnsr: 0, cbal: 0, advamt: 0, traid: null };

                    const nextTrnsr = (Number(lastTx.trnsr) || 0) + 1;
                    const newAmount = (parseFloat(row.Weight) || 0) * (parseFloat(formData.Rate) || 0);
                    const nextCbal = (Number(lastTx.cbal) || 0) + newAmount;

                    // Advance payload
                    const advancePayload = {
                        traid: CDAID,
                        trnsr: nextTrnsr,
                        trnsid: "40",
                        trntid: "10",
                        trndt: formatDate(row.ScheduleDate),
                        refaid: VDAID,
                        maid: FAID || "",
                        pamt: newAmount,
                        cbal: nextCbal,
                        advamt: lastTx.advamt || 0,
                        asid: SHAID || ACSPLGUID.getNew(),
                        isdeleted: false,
                        companyid: userdetail?.companyID || "",
                        deptid: userdetail?.departmentID || "",
                    };

                    await axios.post(
                        `${baseUrl.Url}/backend/api/SP_AddUpdAllTransections`,
                        [advancePayload],
                        { headers: { 'Content-Type': 'application/json' } }
                    );

                    // Stock payload
                    const stockDetailPayload = {
                        staid: CDAID,
                        trndt: TodaysDate,
                        crop: formData.ctype,
                        faid: formData.faid,
                        ciaid: cdaidToRollback,
                        totcr: row.crateCount.toString(),
                        totwt: row.Weight,
                        trntid: "I",
                        cgrade: row?.Grade || "",
                        isdeleted: row.isdeleted === 1 || row.isdeleted === true,
                        companyid: userdetail?.companyID || "",
                        deptid: userdetail?.departmentID || "",
                        reff: "",
                        rtotcr: row.crateCount.toString()
                    };

                    await axios.post(
                        `${baseUrl.Url}/backend/api/SP_AddUpdStockDetails`,
                        [stockDetailPayload],
                        { headers: { 'Content-Type': 'application/json' } }
                    );
                }
            }

            // Save details
            if (detailPayloadList.length > 0) {
                await axios.post(
                    `${baseUrl.Url}/backend/api/SP_AddUpdCropInvertDetail`,
                    detailPayloadList,
                    { headers: { 'Content-Type': 'application/json' } }
                );
            }

            // Save scheduler
            if (schedulerPayloadList.length > 0) {
                await axios.post(
                    `${baseUrl.Url}/backend/api/SP_AddUpdShedulerDetail_Invard`,
                    schedulerPayloadList,
                    { headers: { 'Content-Type': 'application/json' } }
                );
            }

            Swal.fire({
                icon: 'success',
                title: 'Success',
                text: 'Data has been saved successfully!',
                allowOutsideClick: false
            }).then(() => {
                // Reset modal & form
                const modal = document.getElementById("add-units-category");
                if (modal) {
                    modal.classList.remove("show");
                    modal.style.display = "none";
                    modal.setAttribute("aria-hidden", "true");
                    document.querySelectorAll(".modal-backdrop").forEach(backdrop => backdrop.remove());
                    document.body.classList.remove("modal-open");
                    document.body.style.overflow = "";
                    document.body.style.paddingRight = "";
                }
                if (!formData.cropid) {
                    setTableData([]);
                    setRows([]);
                    setFormData({
                        cropid: "", FarmerName: "", PhoneNo: "", CropName: "", CropArea: "",
                        ExpectedHarvesting: "", CropVariety: "", cropInvert: "", Rate: "",
                        Narration: "", Weight: "", caret: "", faid: "", ctype: "", Amount: "",
                    });
                    setDetailData({
                        ScheduleDate: "", packer: "", labour: "", VehicleNo: "",
                        crateCount: "", packingMaterial: "", isdeleted: 0
                    });
                }
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

    const handleAddRow = () => {
        if (

            !detailData.ScheduleDate ||
            !detailData.packingMaterial ||
            !detailData.crateCount ||
            !detailData.Weight ||
            !detailData.Quantity ||
            !detailData.Grade ||
            !detailData.packer?.length ||
            !detailData.labour?.length
        ) {
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'Please fill in all required fields',
            });
            return;
        }
        const selectedPackerLabels = Employee
            .filter(opt => detailData.packer?.includes(opt.value))
            .map(opt => opt.value)
            .join(", ");
        const selectedLabourLabels = Employee
            .filter(opt => detailData.labour?.includes(opt.value))
            .map(opt => opt.value)
            .join(", ");
        let updatedRows;

        const detailPayload = {
            ScheduleDate: detailData.ScheduleDate ? detailData.ScheduleDate : "",
            cdaid: detailData.cdaid ? detailData.cdaid : "",
            shaid: SHAID,
            SHDAID: detailData.SHDAID ? detailData.SHDAID : "",
            staid: detailData.staid ? detailData.staid : "",
            shdaid: detailData.shdaid ? detailData.shdaid : "",
            StartDate: detailData.StartDate ? detailData.StartDate : "",
            VehicleNo: detailData.VehicleNo ? detailData.VehicleNo : "",
            packer: detailData.packer ? detailData.packer : "",
            labour: detailData.labour ? detailData.labour : "",
            packingMaterial: detailData.packingMaterial ? detailData.packingMaterial : "",
            crateCount: detailData.crateCount ? detailData.crateCount : "",
            packingsize: detailData.packingsize ? detailData.packingsize : "",
            cropInvert: detailData.cropInvert ? detailData.cropInvert : "",
            Weight: detailData.Weight ? detailData.Weight : "",
            GradeName: detailData.GradeName ? detailData.GradeName : "",
            Grade: detailData.Grade ? detailData.Grade : "",
            Quantity: detailData.Quantity ? detailData.Quantity : "",
            isdeleted: 0,
        };
        if (detailPayload.cdaid) {
            updatedRows = rows.map(row =>
                row.cdaid === detailPayload.cdaid
                    ? { ...row, ...detailPayload }
                    : row
            );
        } else {
            updatedRows = [...rows, { ...detailPayload, cdaid: ACSPLGUID.getNew(), }];
        }
        setRows(updatedRows);
        setTableData(updatedRows)
        console.log("Updated Rows:", updatedRows);
        const filteredRows = updatedRows.filter(row => row.isdeleted === 0 || row.isdeleted === false);
        const totalWeight = filteredRows.reduce((sum, row) => sum + parseFloat(row.Weight || 0), 0);
        const totalQuantity = filteredRows.reduce((sum, row) => sum + parseFloat(row.Quantity || 0), 0);

        console.log("Total Weight:", totalWeight);
        console.log("Total Quantity:", totalQuantity);
        const AMT = parseFloat((parseFloat(formData.Rate || 0) * parseFloat(totalWeight || 0)).toFixed(2));
        setFormData(prev => ({
            ...prev,
            Weight: totalWeight,
            caret: totalQuantity,
            Amount: AMT,
        }));
        setDetailData(prev => ({
            ...prev,
            // ScheduleDate: "",
            // VehicleNo: "",
            // packer: [],
            // labour: [],
            // packingMaterial: "",
            crateCount: totalQuantity,
            // packingsize: "",
            // cropInvert: "",
            Quantity: "",
            Weight: "",
            Grade: "",
            isdeleted: 0,
        }));
    };
    const handleDelete = (shdaidToDelete) => {
        console.log("Deleting:", shdaidToDelete);

        setRows(prev => {
            const updated = prev.map(item =>
                item.shdaid === shdaidToDelete
                    ? { ...item, isdeleted: 1 }
                    : item
            );
            setTableData(updated);
            console.log("After delete:", updated);
            return updated;
        });
    };
    function formatDateToInput(dateStr) {
        const date = new Date(dateStr);
        if (isNaN(date)) return "";
        const yyyy = date.getFullYear();
        const mm = String(date.getMonth() + 1).padStart(2, "0");
        const dd = String(date.getDate()).padStart(2, "0");
        return `${yyyy}-${mm}-${dd}`;
    }
    const handleEdit = (cdaid) => {
        const filteredProducts = rows.find((row) => row.cdaid === cdaid);
        const updatedDetail = {
            cdaid: filteredProducts.cdaid || "",
            shaid: filteredProducts.shaid,
            shdaid: filteredProducts.shdaid || "",
            staid: filteredProducts.staid || "",
            ScheduleDate: formatDateToInput(filteredProducts.ScheduleDate),
            packer: filteredProducts.packer,
            labour: filteredProducts.labour,
            VehicleNo: filteredProducts.VehicleNo,
            crateCount: filteredProducts.crateCount,
            packingMaterial: filteredProducts.packingMaterial,
            cropInvert: filteredProducts.cropInvert,
            Quantity: filteredProducts.Quantity,
            Weight: filteredProducts.Weight,
            Grade: filteredProducts.Grade,
            isdeleted: 0,
        };
        setDetailData(updatedDetail);
    };
    useEffect(() => {
        if (!SHAID) return;
        const fetchMasterData = async () => {
            try {
                const payload1 = {
                    shaid: SHAID,
                    companyid: userdetail?.companyID || "",
                    deptid: userdetail?.departmentID || ""
                };
                const headers = {
                    "Content-Type": "application/json",
                    Accept: "*/*"
                };
                const response = await axios.post(
                    baseUrl.Url + "/backend/api/GET_MasterDetailInvertData",
                    payload1,
                    { headers }
                );
                if (response.status !== 200) throw new Error("Failed to fetch data");
                if (response.data == 0) {
                    try {
                        const payload1 = {
                            shaid: SHAID,
                            keyword: "%",
                            companyid: userdetail?.companyID || "",
                            deptid: userdetail?.departmentID || "",
                            date: userdetail?.APPDT
                        };

                        const headers = {
                            "Content-Type": "application/json",
                            Accept: "*/*"
                        };

                        const response1 = await axios.post(
                            baseUrl.Url + "/backend/api/GET_CropInvertMaster",
                            payload1,
                            { headers }
                        );

                        if (response1.status !== 200) throw new Error("Failed to fetch data");
                        let apiData = response1.data[0];
                        setFormData(prev => ({
                            ...prev,
                            cropid: apiData?.cropid || "",
                            faid: apiData?.faid || "",
                            StartDate: apiData?.shdate || "",
                            PhoneNo: apiData?.contactno || "",
                            FarmerName: apiData?.farmername || "",
                            faid: apiData?.faid || "",
                            // CropName: apiData.cropname,
                            CropName: apiData?.cropname || "",
                            ctype: apiData?.ctype || "",
                            CropArea: apiData?.carea || "",
                            CropVariety: apiData?.cvariety || "",
                            ExpectedHarvesting: apiData?.eproduction || "",
                            Rate: apiData?.rate || "",
                            Weight: apiData?.weight || "",
                            caret: apiData?.caret || "",
                            Amount: apiData?.amount || "",
                            Narration: apiData?.narr || "",
                        }));
                    } catch (error) {
                        console.error("Error in Master API Call:", error);
                    }
                } else {
                    let apiData = response.data[0];
                    setFormData(prev => ({
                        ...prev,
                        cropid: apiData.cropid,
                        faid: apiData.faid,
                        StartDate: apiData.shdate,
                        PhoneNo: apiData.contactno,
                        FarmerName: apiData.fname,
                        faid: apiData.faid,
                        // CropName: apiData.cropname,
                        CropName: apiData.itemne,
                        ctype: apiData.ctype,
                        CropArea: apiData.carea,
                        CropVariety: apiData.cvariety,
                        ExpectedHarvesting: apiData.eproduction,
                        Rate: apiData.rate,
                        Weight: apiData.weight,
                        caret: apiData.caret,
                        Amount: apiData.amount,
                        Narration: apiData.narr,
                    }));
                    if (response.data.length > 0) {
                        let item = response.data[0];
                        setDetailData({
                            ScheduleDate: formatDateToInput(item.shsdate),
                            VehicleNo: item.vehno || "",
                            packer: item.packer || [],
                            labour: item.worker || [],
                            packingMaterial: item.psize || "",
                            crateCount: item.cratecount || "",
                            //Quantity: item.pquntity || "",
                            // Weight: item.dweight || "",
                            // Grade: item.cgrade || ""
                        });
                        const mappedProducts = response.data.map(item => ({
                            shaid: item.shaid,
                            cdaid: item.cdaid || "",
                            shdaid: item.shdaid || "",
                            staid: item.staid || "",
                            shaid: item.shaid,
                            cdaid: item.cdaid || "",
                            ScheduleDate: item.shsdate,
                            VehicleNo: item.vehno,
                            packer: item.packer,
                            labour: item.worker,
                            packingMaterial: item.psize,
                            crateCount: item.cratecount,
                            cropInvert: item.cropinvert,
                            Quantity: item.pquntity,
                            Weight: item.dweight,
                            isdeleted: item?.isdeleted || false,
                            Grade: item?.cgrade
                        }));
                        setRows(mappedProducts);
                        setTableData(mappedProducts)
                    }
                }
            } catch (error) {
                console.error("Error in Master API Call:", error);
            }
        };
        const fetchDetailsData = async () => {
            try {
                const payload = {
                    shaid: SHAID,
                    companyid: userdetail?.companyID || "",
                    deptid: userdetail?.departmentID || "",
                };
                const headers = {
                    "Content-Type": "application/json",
                    Accept: "*/*"
                };
                const response = await axios.post(
                    baseUrl.Url + "/backend/api/GET_ScheduleDetails",
                    payload,
                    { headers }
                );
                if (response.status !== 200) throw new Error("Failed to fetch details data");
                console.log("Quotation Detail Data:", response.data);
                if (response.data.length > 0) {
                    let filteredProducts = response.data[0];
                    setDetailData(prev => ({
                        shaid: SHAID,
                        cdaid: "",
                        shdaid: "",
                        staid: "",
                        shaid: SHAID,
                        cdaid: "",
                        ScheduleDate: "",
                        VehicleNo: filteredProducts.vehno || "",
                        packer: filteredProducts.packer || "",
                        labour: filteredProducts.worker || "",
                        packingMaterial: filteredProducts.psize || "",
                        crateCount: filteredProducts.pquntity || "",
                        cropInvert: "",
                        Quantity: "",
                        Weight: "",
                        isdeleted: 0,
                        Grade: ""
                    }));
                }
            } catch (error) {
                console.error("Error fetching details data:", error);
            }
        };
        fetchMasterData();
        fetchDetailsData();
    }, [SHAID]);

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


    const fetchLatestTransactions = async (VDAID, FAID) => {
        try {
            const payload = {
                companyid: userdetail?.companyID || "",
                deptid: userdetail?.departmentID || "",
                refaid: VDAID || "",
                maid: FAID || ""
            };

            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };

            const response = await axios.post(
                `${baseUrl.Url}/backend/api/GET_AllTransectionsData`,
                payload,
                { headers }
            );

            console.log("fetchLatestTransactions raw:", response.data);

            let data = [];
            if (Array.isArray(response.data)) {
                data = response.data;
            } else if (Array.isArray(response.data[0])) {
                data = response.data[0];
            }

            // Ensure numeric sorting for latest transaction
            data.sort((a, b) => (Number(a.trnsr) || 0) - (Number(b.trnsr) || 0));

            return data;
        } catch (err) {
            console.error("Error fetching latest transactions:", err);
            return [];
        }
    };

    // useEffect(() => {

    //     fetchtransectiondata();
    // }, [VDAID, FAID]);


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

    const showExitAlert = (e) => {
        MySwal.fire({
            title: "Are you sure?",
            text: "Do you want to go back?",
            showCancelButton: true,
            confirmButtonColor: "#00ff00",
            confirmButtonText: "Yes",
            cancelButtonColor: "#092C4C",
            cancelButtonText: "No",
            allowOutsideClick: false,
            backdrop: true
        }).then((result) => {
            if (result.isConfirmed) {
                if (TableData.length > 0) {
                    handleFormSubmission(e);
                }
                setDetailData({
                    cdaid: "",
                    shaid: "",
                    StartDate: "",
                    VehicleNo: "",
                    packer: [],
                    labour: [],
                    packingMaterial: "",
                    crateCount: "",
                    packingsize: "",
                    cropInvert: "",
                    Weight: "",
                    Quantity: "",
                    isdeleted: 0,
                });
                setTableData([]);
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
            }
        });
    };
    const [Grade, setGradeOptions] = useState([]);
    useEffect(() => {
        const fetchGradeOptions = async () => {
            try {
                const response = await axios.get(baseUrl.Url + "/backend/api/Implications/CGRADE");
                if (response.status !== 200) throw new Error("Failed to fetch brand data");
                const data = response.data;
                const dropdownOptions = data.map(({ iTitle, iValue }) => ({
                    label: iTitle,
                    value: iValue,
                }));
                setGradeOptions(dropdownOptions);
            } catch (error) {
                console.error("Error fetching brand options:", error);
            }
        };
        fetchGradeOptions();
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
                                        <h4>Crop Collection Entry</h4>
                                    </div>

                                    <div className="page-btn">
                                        <Link className="btn btn-secondary"
                                            // data-bs-dismiss="modal"
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
                                                            <label htmlFor="StartDate" className="form-label">Harvesting Date</label>
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
                                                                value={
                                                                    formData.CropVariety
                                                                }
                                                                readOnly
                                                            />
                                                        </div>
                                                    </div>
                                                    <div className="col-6 col-md-6 col-lg-2 mb-3">
                                                        <div className="mb-0 add-product form-label">
                                                            <label htmlFor="shopLocation" className="form-label">Crop Area</label>
                                                            <div className="position-relative">
                                                                <input
                                                                    id="CropArea"
                                                                    type="text"
                                                                    className="form-control"
                                                                    placeholder=" Enter Crop Area"
                                                                    name="ThirdPartyName"
                                                                    onChange={handleChange}
                                                                    value={formData.CropArea}
                                                                    readOnly
                                                                />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div className="col-6 col-md-6 col-lg-2 mb-3">
                                                        <div className="mb-0 add-product">
                                                            <label htmlFor="ExpectedHarvesting" className="form-label" style={{ whiteSpace: "nowrap" }}>Expected Harvesting</label>
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
                                                    </div>
                                                    <div className="col-6 col-md-6 col-lg-2 mb-3">
                                                        <div className="mb-0 add-product">
                                                            <label htmlFor="Crop Invert" className="form-label required">Rate</label>
                                                            <input
                                                                ref={CrateCountRef}
                                                                className="form-control"
                                                                name="Rate"
                                                                type="number"
                                                                value={formData.Rate}
                                                                onChange={(e) => setFormData({ ...formData, Rate: e.target.value })}
                                                                required
                                                            />
                                                        </div>
                                                    </div>
                                                    <div className="col-6 col-md-6 col-lg-2 mb-3">
                                                        <div className="mb-0 add-product">
                                                            <label htmlFor="Crop Invert" className="form-label required" style={{ whiteSpace: "nowrap" }}>Total Weight(KG)</label>
                                                            <input
                                                                ref={WeightRef}
                                                                className="form-control"
                                                                name="Weight"
                                                                value={formData.Weight}
                                                                onChange={(e) => setFormData({ ...formData, Weight: e.target.value })}
                                                                onFocus={(e) => {
                                                                    if (e.target.value === "0") {
                                                                        setFormData({ ...formData, Weight: "" });
                                                                    }
                                                                }}
                                                                required
                                                            />
                                                        </div>
                                                    </div>
                                                    <div className="col-6 col-md-6 col-lg-2 mb-3">
                                                        <div className="mb-0 add-product">
                                                            <label htmlFor="Crop Invert" className="form-label required">Total Create</label>
                                                            <input
                                                                ref={CrateCountRef}
                                                                className="form-control"
                                                                name="caret"
                                                                value={formData.caret}
                                                                onChange={(e) => setFormData({ ...formData, caret: e.target.value })}
                                                                onFocus={(e) => {
                                                                    if (e.target.value === "0") {
                                                                        setFormData({ ...formData, caret: "" });
                                                                    }
                                                                }}
                                                                required
                                                            />
                                                        </div>
                                                    </div>
                                                    <div className="col-6 col-md-6 col-lg-2 mb-3">
                                                        <div className="mb-0 add-product">
                                                            <label htmlFor="Crop Invert" className="form-label required">Amount</label>
                                                            <input
                                                                ref={AmountRef}
                                                                className="form-control"
                                                                name="Amount"
                                                                value={formData.Amount}
                                                                onChange={(e) => setFormData({ ...formData, Amount: e.target.value })}
                                                                onFocus={(e) => {
                                                                    if (e.target.value === "0") {
                                                                        setFormData({ ...formData, Amount: "" });
                                                                    }
                                                                }}
                                                                required
                                                            />
                                                        </div>
                                                    </div>

                                                    <div className="col-lg-7 col-md-6 mb-2">
                                                        <label htmlFor="Narration" className="form-label">Narration</label>
                                                        <input
                                                            ref={RefNaration}
                                                            id="Narration"
                                                            className="form-control"
                                                            name="Narration"
                                                            value={formData.Narration || ''}
                                                            onChange={(e) => setFormData({ ...formData, Narration: e.target.value })}
                                                            placeholder="Enter Description"
                                                            onKeyDown={(e) => handleKeyDown(e, Refvyavarmode)}
                                                        />
                                                    </div>

                                                </div>

                                                <div className="border p-3 rounded shadow-sm mb-1 mt-2">
                                                    <div className="row">
                                                        <div className="col-6 col-md-6 col-lg-2 mb-3">
                                                            <div className="mb-0 add-product">
                                                                <label htmlFor="ScheduleDate" className="form-label required" style={{ whiteSpace: "nowrap" }}>Schedule Date</label>
                                                                <input
                                                                    ref={ScheduleDateref}
                                                                    type="date"
                                                                    className="form-control"
                                                                    name="ScheduleDate"
                                                                    value={detailData.ScheduleDate}
                                                                    max={getTodayDate2()}
                                                                    onChange={(e) => setDetailData({ ...detailData, ScheduleDate: e.target.value })}
                                                                />
                                                            </div>
                                                        </div>
                                                        <div className="col-6 col-md-6 col-lg-2 mb-3">
                                                            <div className="mb-0 add-product">
                                                                <label className="form-label required">Vehicle No.</label>
                                                                <input
                                                                    ref={VehicleNoRef}
                                                                    className="form-control"
                                                                    name="VehicleNo"
                                                                    value={detailData.VehicleNo}
                                                                    onChange={(e) => setDetailData({ ...detailData, VehicleNo: e.target.value })}
                                                                />
                                                            </div>
                                                        </div>
                                                        <div className="col-12 col-md-6 col-lg-4 mb-2">
                                                            <div className="mb-0">
                                                                <label className="form-label required">packer</label>
                                                                <Select
                                                                    ref={packerRef}
                                                                    openMenuOnFocus={true}
                                                                    isMulti
                                                                    placeholder="Select packer"
                                                                    classNamePrefix="react-select"
                                                                    options={Employee.filter(emp => !(detailData.labour || []).includes(emp.value))}
                                                                    value={Employee.filter(option => (detailData.packer || []).includes(option.value))}
                                                                    onChange={(selectedOptions) => {
                                                                        const selectedValues = selectedOptions.map(option => option.value);
                                                                        setDetailData(prev => ({ ...prev, packer: selectedValues }));
                                                                    }}
                                                                    styles={{
                                                                        control: (provided) => ({
                                                                            ...provided,
                                                                            minHeight: '60px',
                                                                            maxHeight: '150px',
                                                                            overflowY: 'auto',
                                                                            flexWrap: 'wrap',
                                                                        }),
                                                                        valueContainer: (provided) => ({
                                                                            ...provided,
                                                                            display: 'flex',
                                                                            flexWrap: 'wrap',
                                                                            maxHeight: '130px',
                                                                            overflowY: 'auto',
                                                                            scrollBehavior: 'smooth',
                                                                        }),
                                                                        multiValue: (provided) => ({
                                                                            ...provided,
                                                                            backgroundColor: '#e0f0ff',
                                                                            margin: '2px',
                                                                            borderRadius: '4px',
                                                                            padding: '2px 5px',
                                                                        }),
                                                                        menu: (provided) => ({
                                                                            ...provided,
                                                                            zIndex: 9999,
                                                                            position: 'absolute',
                                                                        }),
                                                                    }}
                                                                    title="Please Select packer"
                                                                    readOnly
                                                                />
                                                            </div>
                                                        </div>

                                                        <div className="col-12 col-md-6 col-lg-4 mb-2">
                                                            <div className="mb-2">
                                                                <label className="form-label required">Worker</label>
                                                                <Select
                                                                    isMulti
                                                                    openMenuOnFocus={true}
                                                                    placeholder="Select Worker"
                                                                    classNamePrefix="react-select"
                                                                    options={Employee.filter(emp => !(detailData.packer || []).includes(emp.value))}
                                                                    value={Employee.filter(option => (detailData.labour || []).includes(option.value))}
                                                                    onChange={(selectedOptions) => {
                                                                        const selectedValues = selectedOptions.map(option => option.value);
                                                                        setDetailData(prev => ({ ...prev, labour: selectedValues }));
                                                                    }}
                                                                    styles={{
                                                                        control: (provided) => ({
                                                                            ...provided,
                                                                            minHeight: '60px',
                                                                            maxHeight: '150px',
                                                                            overflowY: 'auto',
                                                                            flexWrap: 'wrap',
                                                                        }),
                                                                        valueContainer: (provided) => ({
                                                                            ...provided,
                                                                            display: 'flex',
                                                                            flexWrap: 'wrap',
                                                                            maxHeight: '130px',
                                                                            overflowY: 'auto',
                                                                            scrollBehavior: 'smooth',
                                                                        }),
                                                                        multiValue: (provided) => ({
                                                                            ...provided,
                                                                            backgroundColor: '#e0f0ff',
                                                                            margin: '2px',
                                                                            borderRadius: '4px',
                                                                            padding: '2px 5px',
                                                                        }),
                                                                        menu: (provided) => ({
                                                                            ...provided,
                                                                            zIndex: 9999,
                                                                            position: 'absolute',
                                                                        }),
                                                                    }}
                                                                    title="Please Select Worker"
                                                                    readOnly
                                                                />
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div className="row">
                                                        <div className="col-6 col-md-6 col-lg-2 mb-3">
                                                            <label className="form-label required">Packing Size</label>
                                                            <Select
                                                                ref={PackingMaterialRef}
                                                                openMenuOnFocus={true}
                                                                placeholder="Select Packing Size"
                                                                classNamePrefix="react-select"
                                                                options={packingsize}
                                                                value={packingsize.find(option => option.value === detailData.packingMaterial) || ""}
                                                                onChange={(selectedOption) => {
                                                                    setDetailData(prev => ({ ...prev, packingMaterial: selectedOption?.value || "" }));
                                                                }}
                                                                styles={{
                                                                    menu: (base) => ({
                                                                        ...base,
                                                                        zIndex: 1050,
                                                                    }),
                                                                }}
                                                            />
                                                        </div>

                                                        <div className="col-6 col-md-6 col-lg-2 mb-3">
                                                            <div className="mb-0 add-product">
                                                                <label className="form-label required">Create Count</label>
                                                                <input
                                                                    ref={CrateCountRef}
                                                                    className="form-control"
                                                                    name="crateCount"
                                                                    value={detailData.crateCount}
                                                                    onChange={(e) => setDetailData({ ...detailData, crateCount: e.target.value })}
                                                                />
                                                            </div>
                                                        </div>
                                                        <div className="col-6 col-md-6 col-lg-2 mb-3">
                                                            <div className="mb-0 add-product">
                                                                <label className="form-label required">Weight(KG)</label>
                                                                <input
                                                                    ref={CrateCountRef}
                                                                    className="form-control"
                                                                    name="Weight"
                                                                    value={detailData.Weight}
                                                                    onChange={(e) => setDetailData({ ...detailData, Weight: e.target.value })}
                                                                />
                                                            </div>
                                                        </div>

                                                        <div className="col-6 col-md-6 col-lg-2 mb-3">
                                                            <label htmlFor="Quantity" className="form-label required">Inword Create</label>
                                                            <input
                                                                ref={PackingMaterialRef}
                                                                className="form-control"
                                                                name="Quantity"
                                                                value={detailData.Quantity}
                                                                onChange={(e) => setDetailData({ ...detailData, Quantity: e.target.value })}
                                                            />
                                                        </div>
                                                        <div className="col-6 col-md-6 col-lg-2 mb-3">
                                                            <div className="mb-0 add-product">
                                                                <label htmlFor="Crop Invert" className="form-label required">Grade</label>
                                                                <Select
                                                                    classNamePrefix="react-select"
                                                                    options={Grade}
                                                                    placeholder="Choose"
                                                                    name="Grade"
                                                                    value={Grade.find(option => option.value === detailData.Grade) || null}
                                                                    onChange={(selectedOption) => {
                                                                        setDetailData(prev => ({
                                                                            ...prev,
                                                                            Grade: selectedOption?.value || ""
                                                                        }));
                                                                    }}

                                                                    styles={{
                                                                        menu: (base) => ({
                                                                            ...base,
                                                                            zIndex: 1050,
                                                                        }),
                                                                    }}
                                                                />
                                                            </div>
                                                        </div>
                                                        <div className="col-12 col-md-6 col-lg-2 mb-3 mt-4 d-flex justify-content-center align-items-center">
                                                            <div className="w-100 w-md-auto">
                                                                <button
                                                                    type="button"
                                                                    className="btn btn-secondary w-100 w-md-auto"
                                                                    onClick={handleAddRow}
                                                                >
                                                                    Add Data
                                                                </button>
                                                            </div>
                                                        </div>
                                                    </div>
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
                                                                                    {/* <th className="text-center" style={{ height: "10px", width: "90px" }}>Inword Create</th> */}
                                                                                    <th className="text-center" style={{ height: "10px", width: "90px" }}>Weight</th>
                                                                                    <th className="text-center" style={{ height: "10px", width: "105px" }}>Inword Create</th>
                                                                                    <th className="text-center" style={{ height: "10px", width: "90px" }}>Grade</th>
                                                                                    <th className="text-center" style={{ height: "10px", width: "90px" }}>Action</th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody style={{ maxHeight: "325px", overflowY: "auto" }}>
                                                                                {
                                                                                    rows.filter((row) => row.isdeleted == 0 || row.isdeleted == false).length > 0 ? (
                                                                                        rows
                                                                                            .filter((row) => row.isdeleted == 0)
                                                                                            .map((row, index) => (
                                                                                                <tr key={row.cdaid || index}>
                                                                                                    <td className="text-center">
                                                                                                        {formatDateToDisplay(row.ScheduleDate)}
                                                                                                    </td>
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
                                                                                                            const opt = packingsize.find(o => o.value === row.packingMaterial);
                                                                                                            return opt?.label || row.packingMaterial || "-";
                                                                                                        })()}
                                                                                                    </td>

                                                                                                    {/* <td className="text-center">{row.crateCount}</td> */}
                                                                                                    <td className="text-center">{row.Weight}</td>
                                                                                                    <td className="text-center">{row.Quantity}</td>

                                                                                                    <td className="text-center">
                                                                                                        {
                                                                                                            (() => {
                                                                                                                const opt = Grade.find(o => o.value.toString() === row.Grade?.toString());
                                                                                                                return opt?.label || row.Grade || "-";
                                                                                                            })()
                                                                                                        }
                                                                                                    </td>


                                                                                                    <td className="text-center">
                                                                                                        <Link to="#" onClick={() => handleEdit(row.cdaid)} className="me-2 p-1" style={{ color: 'lightblue' }}>
                                                                                                            <Edit className="feather-edit" />
                                                                                                        </Link>
                                                                                                        <Link to="#" className="confirm-text p-2" onClick={(e) => { e.preventDefault(); handleDelete(row.shdaid); }}>
                                                                                                            <Trash2 className="feather-trash-2 text-danger" />
                                                                                                        </Link>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            ))
                                                                                    ) : (
                                                                                        <tr>
                                                                                            <td colSpan="9" className="text-center">Data Not Available</td>
                                                                                        </tr>
                                                                                    )
                                                                                }
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
                                                            <div className="col-12 col-md-1 mb-2 order-1 order-md-2">
                                                                <button
                                                                    type="submit"
                                                                    className="btn btn-submit w-100"
                                                                    ref={SaveRef}
                                                                    onClick={handleSubmit}
                                                                >
                                                                    Save
                                                                </button>
                                                            </div>

                                                            <div className="col-12 col-md-1 mb-2 order-2 order-md-1">
                                                                <button
                                                                    type="button"
                                                                    className="btn btn-cancel w-100"
                                                                    // data-bs-dismiss="modal"
                                                                    aria-label="Close"
                                                                    onClick={showExitAlert}
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
                                                            type="button"
                                                            className="btn btn-submit w-100 w-md-auto"
                                                            ref={SaveRef}
                                                            onClick={handleSubmit}
                                                        >
                                                            Save
                                                        </button>
                                                        <button
                                                            type="button"
                                                            className="btn btn-cancel w-100 w-md-auto"
                                                            // data-bs-dismiss="modal"
                                                            aria-label="Close"
                                                            onClick={showExitAlert}
                                                        >
                                                            Back
                                                        </button>
                                                    </div>
                                                </div> */}
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

export default AddCropInvert;
