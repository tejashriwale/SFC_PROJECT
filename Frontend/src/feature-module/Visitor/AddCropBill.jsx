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
import BillReceipt from "./BillReceipt";
const AddCropBill = ({ CROPID, cdaid, VDAID, MAID }) => {
    console.log(CROPID, cdaid, VDAID, MAID, "CROPID, cdaid, VDAID, MAID")
    // const userdetail = getUserData();
    const [EndDate, setEndDate] = useState('');
    const [BillNo, setBillNo] = useState([]);
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
    const [selectedRow, setSelectedRow] = useState(null);
    const navigate = useNavigate();
    const route = all_routes;


    const [isHistoryStatePushed, setIsHistoryStatePushed] = useState(false);

    useEffect(() => {
        const modal = document.getElementById("add-units-category");

        // Function to close the modal and clean up
        const closeModal = () => {
            if (modal) {
                modal.classList.remove("show");
                modal.style.display = "none";
                modal.setAttribute("aria-hidden", "true");

                const modalBackdrop = document.querySelector(".modal-backdrop");
                if (modalBackdrop) {
                    modalBackdrop.remove();
                }

                modal.removeAttribute("aria-modal");
                modal.removeAttribute("role");
                document.body.classList.remove("modal-open");
                document.body.style.overflow = "";
                document.body.style.paddingRight = "";
            }
        };

        const handlePopState = () => {
            // 1. Check if the modal is currently visible
            if (modal && modal.classList.contains("show")) {
                // 2. Immediate re-push to block the browser from leaving the page.
                window.history.pushState(null, "", window.location.pathname);

                // 3. Close the modal instead of navigating back.
                closeModal();
                // You can optionally show a message here instead of just closing.
            } else {
                // If the popstate event fires and the modal is NOT open, 
                // the user is trying to navigate back from the real previous page.
                // DO NOT re-push state, allow the default browser back action to proceed.
                // In a React Router setup, you might use navigate(-1) here if you 
                // had to manually control the back action, but here we just let the 
                // popstate event complete without interference.
            }
        };

        // This is the cleanup function that runs when the component unmounts
        // OR before the effect runs again (if dependencies change).
        window.addEventListener('popstate', handlePopState);

        return () => {
            window.removeEventListener('popstate', handlePopState);
        };
    }, [navigate, route.CropBill]); // Keeping your original dependencies for context

    // useEffect(() => {
    //     // Push dummy state so there is a history entry to catch
    //     window.history.pushState(null, "", window.location.pathname);

    //     const handlePopState = (event) => {
    //         // Prevent the navigation by pushing state again
    //         window.history.pushState(null, "", window.location.pathname);

    //         // Attempt to close the modal and remove backdrop
    //         closeModal();

    //         // Optionally, you could do navigate(...) if you want navigation after that
    //     };

    //     window.addEventListener("popstate", handlePopState);

    //     return () => {
    //         window.removeEventListener("popstate", handlePopState);
    //     };
    // }, [/* dependencies you need */]);

    const closeModal = () => {
        const modal = document.getElementById("add-units-category");
        if (!modal) return;
        // If the modal is currently shown
        if (modal.classList.contains("show")) {
            modal.classList.remove("show");
            modal.style.display = "none";
            modal.setAttribute("aria-hidden", "true");
            modal.removeAttribute("aria-modal");
            modal.removeAttribute("role");
            // Remove backdrop(s)
            document.querySelectorAll(".modal-backdrop").forEach((el) => el.remove());
            // Restore body scrolling
            document.body.classList.remove("modal-open");
            document.body.style.overflow = "";
            document.body.style.paddingRight = "";
        }
    };



    useEffect(() => {
        const handleResize = () => setIsMobile(window.innerWidth < 768);
        window.addEventListener('resize', handleResize);
        return () => window.removeEventListener('resize', handleResize);
    }, []);
    if (isAuthenticated == true) {
        console.log("user", userdetail);
        console.log("getUserData", getUserData);
    }
    console.log("AddCropInvert props", { CROPID, cdaid });
    const MySwal = withReactContent(Swal);
    useEffect(() => {
        getTodayDate();
    }, [CROPID, cdaid]);
    const [farmeName, setfarmeName] = useState([]);

    //const [Date1, setDate] = useState([]);
    const [Date123, setDate123] = useState([]);
    const [MoblieNo, setMoblieNo] = useState([]);
    const [tabledata, settabledata] = useState([]);
    const [Crop, setCrop] = useState([]);
    const [selectedData, setSelectedData] = useState({ cropid: null, cdaid: null });
    const [TableData, setTableData] = useState([]);
    const [rows, setRows] = useState([]);
    const [formData, setFormData] = useState({
        cropid: "",
        cdaid: "",
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
    const openModal = (cropid) => {
        setSelectedData({ cropid });
        setTimeout(() => {
            const modal = new window.bootstrap.Modal(document.getElementById("print-receipt1"));
            modal.show();
        }, 100);
    };
    useEffect(() => {
        const modalElement = document.getElementById('print-receipt1');
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
                setBillNo(data.billno);
            } else {
                console.warn("GET_CropInvertBill returned empty or invalid data:", response.data);
            }
        } catch (error) {
            console.error("Error fetching Bill data:", error);
        }
    };

    const [TranData, setTranData] = useState([]);

    useEffect(() => {
        const fetchTranData = async () => {
            try {
                if (!VDAID || !MAID) {
                    console.warn("Skipping fetch: VDAID or MAID is empty");
                    return;
                }

                const payload = {
                    refaid: VDAID,
                    maid: MAID,
                    companyid: userdetail?.companyID || "",
                    deptid: userdetail?.departmentID || "",
                };

                const headers = {
                    "Content-Type": "application/json",
                    Accept: "*/*",
                };

                const response = await axios.post(
                    `${baseUrl.Url}/backend/api/GET_FarmerTranData`,
                    payload,
                    { headers }
                );

                if (response.status !== 200) throw new Error("Failed to fetch vendor data");

                console.log("Farmer tran data", response.data);
                setTranData(response.data);

            } catch (error) {
                console.error("Error fetching Bill data:", error);
            }
        };

        fetchTranData();
    }, [VDAID, MAID]);

    useEffect(() => {
        const fetchTranData = async () => {
            try {
                if (!CROPID || !MAID) {
                    console.warn("Skipping fetch: VDAID or MAID is empty");
                    return;
                }

                const payload = {
                    refaid: CROPID,
                    maid: MAID,
                    companyid: userdetail?.companyID || "",
                    deptid: userdetail?.departmentID || "",
                };

                const headers = {
                    "Content-Type": "application/json",
                    Accept: "*/*",
                };

                const response = await axios.post(
                    `${baseUrl.Url}/backend/api/GET_FarmerTranData`,
                    payload,
                    { headers }
                );

                if (response.status !== 200) throw new Error("Failed to fetch vendor data");

                console.log("Farmer tran data", response.data);
                setTranData(response.data);

            } catch (error) {
                console.error("Error fetching Bill data:", error);
            }
        };

        fetchTranData();
    }, [CROPID, MAID]);

    useEffect(() => {
        if (CROPID != "") {
            const fetchBillDetailData = async () => {
                try {
                    const payload1 = {
                        "companyid": userdetail?.companyID ? userdetail.companyID : "",
                        "deptid": userdetail?.departmentID ? userdetail.departmentID : "",
                        "cropid": CROPID,

                    };
                    const headers = {
                        "Content-Type": "application/json",
                        Accept: "*/*"
                    };
                    const response = await axios.post(
                        baseUrl.Url + "/backend/api/GET_CropBillData",
                        payload1,
                        { headers }
                    );
                    if (response.status !== 200) throw new Error("Failed to fetch data");

                    if (response.data.length > 0) {
                        const mappedProducts = response.data.map((item) => ({
                            // vdaid: item.vdaid,
                            // cropType: item.croptype,
                            // cropvariety: item.cropvariety,
                            // croparea: item.croparea,
                            // expectedharvestingdate: convertToISODate(item.expectedharvestingdate),
                            // expectedproduction: item.expectedproduction,
                            // photoes: item.photoes ? item.photoes.split(",") : [],
                            // rate: item.rate,
                            // advancemoney: item.advancemoney,
                            // isdeleted: item.isdeleted,
                            cdaid: item.cdaid,
                            ScheduleDate: item.visitingdate,
                            VehicleNo: item.vehno,
                            packer: item.packername,
                            labour: item.workername,
                            packingsize: item.psize,
                            Quantity: item.pquntity,
                            Weight: item.weight,
                            rate: item.rate,
                            isdeleted: item.isdeleted,


                        }));

                        setTableData(mappedProducts);

                    }

                } catch (error) {
                    console.error("Error in fetchBillDetailData API Call:", error);
                }
            };
            fetchBillDetailData();
        }
    }, [CROPID]);

    const [cropInvertDetail, setCropInvertDetail] = useState([]);
    const [billViewMode, setBillViewMode] = useState(null); // null = full bill, object = single row data
    // const [cropInvertDetail, setCropInvertDetail] = useState({
    //     billno: "",
    //     date: "",
    //     FARMERNAME: "",
    //     CONTACTNO: "",
    //     SHSDATE: "",
    //     cvariety: "",
    //     CRATECOUNT: "",
    //     RATE: "",
    //     WEIGHT: "",
    //     AMOUNT: "",
    // });

    const fetchbill1 = async (cdaid) => {
        try {

            const payload = {
                cdaid: cdaid,
                companyid: userdetail?.companyID || "",
                deptid: userdetail?.departmentID || "",
            };
            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };
            const response = await axios.post(
                `${baseUrl.Url}/backend/api/GET_SingleRowCropInvertDetail`,
                payload,
                { headers }
            );

            if (response.status !== 200) throw new Error("Failed to fetch GET_SingleRowCropInvertDetail data");

            const data = response.data;

            // ✅ Save the entire response in one state
            setCropInvertDetail(data);

            console.log("setCropInvertDetail***************************************************", cropInvertDetail);
            return true; // ✅ success

        } catch (error) {
            console.error("Error fetching GET_SingleRowCropInvertDetail data:", error);
            return false;
        }
    };




    const getTodayDate = () => {
        const today = userdetail?.APPDT ? new Date(userdetail.APPDT) : new Date();
        const formatted = today.toLocaleDateString("en-GB", {
            day: "2-digit",
            month: "short",
            year: "numeric",
        });
        setEndDate(formatted);
    };

    const getTodayDate2 = () => {
        const today = userdetail?.APPDT ? new Date(userdetail.APPDT) : new Date();
        const yyyy = today.getFullYear();
        const mm = String(today.getMonth() + 1).padStart(2, "0");
        const dd = String(today.getDate()).padStart(2, "0");
        return `${yyyy}-${mm}-${dd}`;
    };



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

    // useEffect(() => {
    //     const modal = document.getElementById('add-units-category');
    //     const handleShown = () => {
    //         RefOutWardStockWeight.current?.focus();
    //     };
    //     if (modal) {
    //         modal.addEventListener('shown.bs.modal', handleShown);
    //     }
    //     return () => {
    //         if (modal) {
    //             modal.removeEventListener('shown.bs.modal', handleShown);
    //         }
    //     };
    // }, []);
    const handleFinalSave = (e) => {
        Swal.fire({
            title: 'तुम्हाला माहिती सेव्ह करायची आहे का?',
            icon: 'question',
            showCancelButton: true,
            confirmButtonText: 'हो',
            cancelButtonText: 'नाही'
        }).then((result) => {
            if (result.isConfirmed) {
                handleSubmit(e);
            }
        });
    };

    // Prevents duplicate bills from a double click while a save is in flight
    const isSavingRef = useRef(false);
    const handleSubmit = async (e) => {
        if (isSavingRef.current) {
            e?.preventDefault?.();
            return;
        }
        isSavingRef.current = true;
        try {
            await submitBill(e);
        } finally {
            isSavingRef.current = false;
        }
    };
    const submitBill = async (e) => {
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
            const masterPayload = {
                cbkid: formData.cbkid || ACSPLGUID.getNew(),
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
        const today = userdetail?.APPDT ? new Date(userdetail.APPDT) : new Date();
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
                cdaid: apiData.cdaid,
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
            }));
        } catch (error) {
            console.error("Error in Master API Call:", error);
        }
    };

    const fetchDetailsData = async () => {
        try {
            // if (!CROPID || !CDAID) {
            //     console.warn("CROPID or CDAID missing, skipping detail fetch");
            //     return;
            // }
            const payload = {
                cdaid: cdaid,
                cropid: CROPID,
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
                    cdaid: item.cdaid,
                    cropid: item.CROPID,
                    ScheduleDate: item.VISITINGDATE,
                    VehicleNo: item.VEHNO,
                    packer: item.PACKERNAME,
                    labour: item.WORKERNAME,
                    packingMaterial: item.SIZE,
                    crateCount: item.COUNT,
                    cropInvert: item.CROPINVERT,
                    Quantity: item.PQUNTITY,
                    Weight: item.WEIGHT,
                    isdeleted: item.isdeleted,
                }));
                setTableData(mappedProducts);
            } else {
                console.log("No details data found, might be new.");
            }
        } catch (error) {
            console.error("Error fetching detail data:", error);
        }
    };

    useEffect(() => {
        if (!CROPID) return;
        fetchMasterData();
    }, [CROPID]);

    useEffect(() => {
        if (!CROPID || !cdaid) return;
        fetchDetailsData();

    }, [CROPID, cbkid, cdaid]);
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
            size: "",
            cropInvert: "",
            Weight: "",
            Quantity: "",
            isdeleted: 0,
        });
        // const modal = document.getElementById("add-units-category");
        // if (modal) {
        //     modal.classList.remove("show");
        //     modal.style.display = "none";
        //     modal.setAttribute("aria-hidden", "true");

        //     document.querySelectorAll(".modal-backdrop").forEach(backdrop => {
        //         backdrop.remove();
        //     });
        //     document.body.classList.remove("modal-open");
        //     document.body.style.overflow = "";
        //     document.body.style.paddingRight = "";
        // }



        const modalElement = document.getElementById("add-units-category");
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

    };

    useEffect(() => {
        const Rate = parseFloat(formData.Rate) || 0;
        const weight = parseFloat(detailData.Weight) || 0;
        const cropInvert = Math.round(Rate * weight * 100) / 100;
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
        const printModal = document.getElementById('print-receipt1');
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
                                                                required
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
                                                            required
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
                                                                required
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
                                                    <div className="col-6 col-md-6 col-lg-1 mb-3">
                                                        <div className="mb-0 add-product">
                                                            <label htmlFor="Crop Invert" className="form-label required">Rate</label>
                                                            <input
                                                                ref={CrateCountRef}
                                                                className="form-control"
                                                                required
                                                                name="Rate"
                                                                value={formData.Rate}
                                                                onChange={(e) => setFormData({ ...formData, Rate: e.target.value })}
                                                                readOnly
                                                            />
                                                        </div>
                                                    </div>
                                                    <div className="col-6 col-md-6 col-lg-1 mb-3">
                                                        <div className="mb-0 add-product">
                                                            <label htmlFor="Crop Invert" className="form-label required" style={{ whiteSpace: "nowrap" }}>Weight(KG)</label>
                                                            <input
                                                                ref={CrateCountRef}
                                                                className="form-control"
                                                                name="Weight"
                                                                required
                                                                value={formData.Weight}
                                                                onChange={(e) => setFormData({ ...formData, Weight: e.target.value })}
                                                                readOnly
                                                            />
                                                        </div>
                                                    </div>
                                                    <div className="col-6 col-md-6 col-lg-1 mb-3">
                                                        <div className="mb-0 add-product">
                                                            <label htmlFor="Crop Invert" className="form-label required">Total Crate</label>
                                                            <input
                                                                ref={CrateCountRef}
                                                                className="form-control"
                                                                name="caret"
                                                                required
                                                                value={formData.caret}
                                                                onChange={(e) => setFormData({ ...formData, caret: e.target.value })}
                                                                readOnly
                                                            />
                                                        </div>
                                                    </div>
                                                    <div className="col-6 col-md-6 col-lg-1 mb-3">
                                                        <div className="mb-0 add-product">
                                                            <label htmlFor="Crop Invert" className="form-label required">Amount</label>
                                                            <input
                                                                ref={CrateCountRef}
                                                                className="form-control"
                                                                name="Amount"
                                                                required
                                                                value={formData.Amount}
                                                                onChange={(e) => setFormData({ ...formData, Amount: e.target.value })}
                                                                readOnly
                                                            />
                                                        </div>
                                                    </div>
                                                    <div className="col-lg-8 col-md-6 mb-2">
                                                        <label htmlFor="Narration" className="form-label">Narration</label>
                                                        <input
                                                            ref={RefNaration}
                                                            id="Narration"
                                                            className="form-control"
                                                            name="Narration"
                                                            onChange={handleChange}
                                                            value={formData.Narration}
                                                            placeholder="Enter Discription"
                                                            readOnly
                                                        />
                                                    </div>
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
                                                                                    <th className="text-center" style={{ height: "10px", width: "90px" }}>Date</th>
                                                                                    <th className="text-center" style={{ height: "10px", width: "120px" }}>Veh.no</th>
                                                                                    <th className="text-center" style={{ height: "10px" }}>Packer</th>
                                                                                    <th className="text-center" style={{ height: "10px" }}>Worker</th>
                                                                                    <th className="text-center" style={{ height: "10px" }}>Packing Size</th>
                                                                                    <th className="text-center" style={{ height: "10px", width: "90px" }}>Quantity</th>
                                                                                    {/* <th className="text-center" style={{ height: "10px", width: "90px" }}>Caret Count</th> */}
                                                                                    <th className="text-center" style={{ height: "10px", width: "90px" }}>Weight</th>
                                                                                    <th className="text-center" style={{ height: "10px", width: "90px" }}>Rate</th>
                                                                                    <th className="text-center" style={{ height: "10px", width: "90px" }}>View</th>
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

                                                                                                        {row.packingsize}
                                                                                                    </td>
                                                                                                    <td className="text-center">{row.Quantity}</td>
                                                                                                    {/* <td className="text-center">{row.crateCount}</td> */}
                                                                                                    <td className="text-center">{row.Weight}</td>
                                                                                                    <td className="text-center">{row.rate}</td>
                                                                                                    <td className="text-center">
                                                                                                        <button
                                                                                                            type="button"
                                                                                                            className="btn btn-sm btn-outline-success py-0 px-2"
                                                                                                            onClick={() => {
                                                                                                                const singleRowData = [{
                                                                                                                    cratecount: row.Quantity,
                                                                                                                    rate: row.rate,
                                                                                                                    weight: row.Weight
                                                                                                                }];
                                                                                                                const singleTotal = parseFloat(row.rate || 0) * parseFloat(row.Weight || 0);
                                                                                                                setBillViewMode({ tableData: singleRowData, totalAmount: singleTotal });

                                                                                                                const modalElement = document.getElementById('print-receipt1');
                                                                                                                if (modalElement) {
                                                                                                                    const modal = window.bootstrap.Modal.getOrCreateInstance(modalElement);
                                                                                                                    modal.show();

                                                                                                                    document.body.classList.add('force-landscape-view');
                                                                                                                    modalElement.addEventListener('hidden.bs.modal', function handler() {
                                                                                                                        document.body.classList.remove('force-landscape-view');
                                                                                                                        setBillViewMode(null);
                                                                                                                        modalElement.removeEventListener('hidden.bs.modal', handler);
                                                                                                                    }, { once: true });
                                                                                                                }
                                                                                                            }}
                                                                                                        >
                                                                                                            View Bill
                                                                                                        </button>
                                                                                                    </td>

                                                                                                </tr>
                                                                                            ))
                                                                                    ) : (
                                                                                        <tr>
                                                                                            <td colSpan="9" className="text-center">Data Not Availabel</td>
                                                                                        </tr>
                                                                                    )}
                                                                            </tbody>
                                                                        </table>
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
                                                                    {/* Add additional content here if needed 
                                                                </div>
                                                                <div style={{
                                                                    display: 'flex',
                                                                    flexDirection: isMobile ? 'column' : 'row',
                                                                    gap: '10px',
                                                                    width: isMobile ? '100%' : 'auto'
                                                                }}>
                                                                    <div style={{
                                                                        backgroundColor: "rgb(224 231 228)",
                                                                        fontSize: "15px",
                                                                        color: "black",
                                                                        border: "2px solid #343a40",
                                                                    }}>
                                                                        ₹ {totalAmount.toFixed(2)}(Total Amount)
                                                                    </div>
                                                                </div>
                                                            </div> */}
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
                                                                        Total Bill Amount:{" "}
                                                                        {totalAmount.toFixed(2)}
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>


                                                {/* //trandata */}
                                                <div className="border p-3 rounded shadow-sm mb-1 mt-2">
                                                    <div className="row align-items-center mb-3">
                                                        <div className="col-auto">
                                                            <h4 style={{ color: "rgb(255 255 255)", margin: 0 }}>Payment Summary</h4>
                                                        </div>
                                                        {/* <div className="col-md-4">
                                                            <div
                                                                className="p-1 rounded text-center fw-bold"
                                                                style={{
                                                                    backgroundColor: "rgb(224 231 228)",
                                                                    fontSize: "15px",
                                                                    color: "black",
                                                                    border: "2px solid #343a40",
                                                                }}
                                                            >
                                                                ₹ {totalAmount.toFixed(2)}(Total Amount)
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
                                                                                {/* <th className="text-center">Action</th> */}
                                                                            </tr>
                                                                        </thead>

                                                                        <tbody>
                                                                            {TranData.map((row) => (
                                                                                <tr key={row.traid}>
                                                                                    <td className="text-center">{row.trndt}</td>
                                                                                    <td className="text-center">{row.narr}</td>
                                                                                    <td className="text-center">{row.pamt}</td>
                                                                                    {/* <td className="text-center">
                                                                                        <Link
                                                                                            to="#"
                                                                                            onClick={() =>
                                                                                                setDetailData((prev) => prev.filter((d) => d.traid !== row.traid))
                                                                                            }
                                                                                            className="me-2 p-2"
                                                                                            style={{ color: "red" }}
                                                                                        >
                                                                                            <i className="feather-trash-2"></i>
                                                                                        </Link>
                                                                                    </td> */}
                                                                                </tr>
                                                                            ))}
                                                                        </tbody>
                                                                    </table>
                                                                </div>

                                                                {/* Highlighted Total Box */}
                                                                {/* <div
                                                                    className="mt-1 p-2 rounded text-center fw-bold"
                                                                    style={{
                                                                        backgroundColor: "rgb(224 231 228)",
                                                                        fontSize: "1rem",
                                                                        color: "black",
                                                                        border: "2px solid #343a40", // dark border
                                                                    }}
                                                                >
                                                                    Total Paid Amount:{" "}
                                                                    {TranData.reduce((sum, row) => sum + Number(row.pamt || 0), 0)}
                                                                </div> */}


                                                            </div>
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
                                                                Total Paid Amount:{" "}
                                                                {TranData.reduce((sum, row) => sum + Number(row.pamt || 0), 0)}
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                {/* trandata */}



                                                <div className="col-lg-12">
                                                    <div className="text-end">
                                                        <div className="row justify-content-end">
                                                            <div className="col-12 col-md-1 mb-2 order-2 order-md-1">
                                                                <button
                                                                    type="button"
                                                                    className="btn btn-submit w-100"
                                                                    to="#" data-bs-toggle="modal"
                                                                    data-bs-target="#print-receipt1"
                                                                    // onClick={e => {
                                                                    //     e.preventDefault();
                                                                    //     openModal(CROPID);
                                                                    // }}

                                                                    onClick={(e) => {
                                                                        e.preventDefault();
                                                                        setBillViewMode(null);
                                                                        openModal(CROPID);
                                                                        document.body.classList.add('force-landscape-view');

                                                                        // 3. Optional: Add an event listener to clean up the class when the modal closes
                                                                        const modalElement = document.getElementById('print-receipt1');
                                                                        if (modalElement) {
                                                                            // Ensure the cleanup happens ONLY after the modal is hidden
                                                                            modalElement.addEventListener('hidden.bs.modal', function handler() {
                                                                                document.body.classList.remove('force-landscape-view');
                                                                                modalElement.removeEventListener('hidden.bs.modal', handler);
                                                                            }, { once: true });
                                                                        }
                                                                    }}
                                                                >
                                                                    Print Bill
                                                                </button>
                                                            </div>

                                                            {/* Back Button - Second on mobile, first on desktop */}
                                                            <div className="col-12 col-md-1 mb-2 order-2 order-md-1">
                                                                <button
                                                                    type="button"
                                                                    className="btn btn-cancel w-100"
                                                                    // data-bs-dismiss="modal"
                                                                    onClick={showExitAlert}
                                                                    aria-label="Close"
                                                                >
                                                                    Back
                                                                </button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
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

            <>
                {/* <style>
                    {`
  
                        @page {
                          size: A5;
                          margin: 0;
                        }

                        @media print {
                          html, body {
                            margin: 0;
                            padding: 0;
                            height: 210mm; 
                            overflow: hidden;
                          }


                          body * {
                            visibility: hidden !important;
                          }


                          #print-receipt,
                          #print-receipt1,
                          #print-receipt *,
                          #print-receipt1 * {
                            visibility: visible !important;
                          }


                          #print-receipt {
                            position: absolute !important;
                            top: 0;
                            left: 0;
                            width: 148mm !important;
                            height: 210mm !important;
                            margin: 0 auto !important;
                            display: flex !important;
                            flex-direction: column !important;
                            justify-content: center !important; 
                            align-items: center !important;
                            overflow: hidden !important;
                          }


                          #print-receipt1 {
                            position: absolute !important;
                            top: 0;
                            left: 0;
                            width: 148mm !important;
                            height: 210mm !important;
                            margin: 0 auto !important;
                            display: flex !important;
                            flex-direction: column !important;
                            justify-content: center !important; 
                            align-items: center !important;
                            overflow: hidden !important;
                          }


                          .modal-backdrop {
                            display: none !important;
                          }


                          #print-receipt .modal-body,
                          #print-receipt1 .modal-body {
                            height: auto !important;
                            overflow: visible !important;
                            padding-top: 0 !important;
                            padding-bottom: 0 !important;
                          }

                        }
                        `}
                </style> */}

                <style>
                    {`
    @media only screen and (max-width: 768px) {
      body.force-landscape-view {
        overflow: hidden !important;
        position: fixed;
        width: 100%;
        height: 100%;
      }

      /* Modal dialog should fill screen */
      body.force-landscape-view #print-receipt .modal-dialog {
        width: 100vh !important;
        height: 100vw !important;
        margin: 0 auto !important;
        display: flex !important;
        align-items: center;
        justify-content: center;
        overflow: hidden !important;
      }

      /* Rotated modal content */
      body.force-landscape-view #print-receipt .modal-content {
        transform: rotate(90deg);
        transform-origin: center center;
        width: 100vh !important;
        min-width: 650px !important;  /* 👈 Important: give more width for headers */
        height: 100vw !important;
        overflow-x: auto !important;   /* 👈 Enable horizontal scroll if needed */
        overflow-y: auto !important;
            margin-right: 25vh;             
        background: white !important;
        border-radius: 0 !important;
        box-shadow: none !important;
        display: block;
        padding: 0;
      }

      /* Make sure tables inside modal don't collapse */
      body.force-landscape-view #print-receipt table {
        width: max-content !important;
        min-width: 100% !important;
      }
    }
  `}
                </style>

                <style>
                    {`
    @media only screen and (max-width: 768px) {
      body.force-landscape-view {
        overflow: hidden !important;
        position: fixed;
        width: 100%;
        height: 100%;
      }

      /* Modal dialog should fill screen */
      body.force-landscape-view #print-receipt1 .modal-dialog {
        width: 100vh !important;
        height: 100vw !important;
        margin: 0 auto !important;
        display: flex !important;
        align-items: center;
        justify-content: center;
        overflow: hidden !important;
      }

      /* Rotated modal content */
      body.force-landscape-view #print-receipt1 .modal-content {
        transform: rotate(90deg);
        transform-origin: center center;
        width: 100vh !important;
        min-width: 650px !important;  /* 👈 Important: give more width for headers */
        height: 100vw !important;
        overflow-x: auto !important;   /* 👈 Enable horizontal scroll if needed */
        overflow-y: auto !important;
            margin-right: 25vh;             
        background: white !important;
        border-radius: 0 !important;
        box-shadow: none !important;
        display: block;
        padding: 0;
      }

      /* Make sure tables inside modal don't collapse */
      body.force-landscape-view #print-receipt1 table {
        width: max-content !important;
        min-width: 100% !important;
      }
    }
  `}
                </style>


                <style>
                    {`
                        @page {
                            size: A5 portrait;
                            margin: 0;
                        }

                        @media print {
                            html, body {
                            margin: 0;
                            padding: 0;
                            width: 148mm;
                            height: 210mm;
                            overflow: hidden;
                            }

                            body * {
                            visibility: hidden !important;
                            }

                            #print-receipt,
                            #print-receipt1,
                            #print-receipt *,
                            #print-receipt1 * {
                            visibility: visible !important;
                            }

                            #print-receipt,
                            #print-receipt1 {
                            position: absolute !important;
                            top: 0;
                            left: 0;
                            width: 148mm !important;
                            height: 210mm !important;
                            margin: 0 !important;
                            padding: 0 !important;
                            overflow: hidden !important;
                            display: block !important;
                            box-sizing: border-box;
                            transform: scale(1); /* prevent auto shrink */
                            }

                            #print-receipt .modal-content,
                            #print-receipt1 .modal-content {
                            width: 100% !important;
                            height: 100% !important;
                            margin: 0 !important;
                            padding: 0 !important;
                            box-sizing: border-box;
                            border: none !important;
                            }

                            .modal-backdrop, .close {
                            display: none !important;
                            }
                        }
                        `}
                </style>
                <BillReceipt
                    modalId="print-receipt1"
                    billNo={BillNo}
                    date={Date123}
                    farmerName={farmeName}
                    mobileNo={MoblieNo}
                    vehNo={tabledata.length > 0 ? (tabledata[0].vehno || '') : ''}
                    tableData={billViewMode ? billViewMode.tableData : tabledata}
                    totalAmount={billViewMode ? billViewMode.totalAmount : totalAmount}
                />
            </>

        </>
    );
};

export default AddCropBill;





