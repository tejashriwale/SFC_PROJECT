import React, { useRef, useEffect, useState } from "react";
import { Link } from "react-router-dom";
import Select from "react-select";
import { all_routes } from "../../Router/all_routes";
import withReactContent from "sweetalert2-react-content";
import Swal from "sweetalert2";
// import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import { baseUrl, convertToISODate } from "../../core/json/custom";

import { ACSPLGUID } from "../../core/json/custom";
import { getUserData } from "../../Context/UserData";
import { useNavigate } from "react-router-dom";
import { useLocation } from 'react-router-dom';
import { OverlayTrigger, Tooltip } from "react-bootstrap";
import debounce from "lodash.debounce";
import {
    ArrowLeft,
    Info,
    Edit,
    Trash2,
} from "feather-icons-react/build/IconComponents";
import { Contact, Weight } from "lucide-react";
const AddDirectInward = ({ FAID, CROPID, onRefresh }) => {
    const route = all_routes;
    console.log("primary key ", FAID, CROPID)
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
    const QuantityRef = useRef(null);
    const WeightRef = useRef(null);
    const RateDetailRef = useRef(null);
    const GradeRef = useRef(null);
    const AddRef = useRef(null);
    // const WeightRef = useRef(null);
    const AmountRef = useRef(null);
    const [Transections, setTransections] = useState();
    const [isEditing, setIsEditing] = useState(false);
    const [cropType, setCropType] = useState([]);
    const [VDAID, setVDAID] = useState([]);
    const [suggestions, setSuggestions] = useState([]);


    const [cropId, setCropId] = useState(ACSPLGUID.getNew());
    const [cropIdDetail, setCropDetailId] = useState(ACSPLGUID.getNew());
    const [TransactionId, setTransctionId] = useState(ACSPLGUID.getNew());
    const [StockId, setStockId] = useState(ACSPLGUID.getNew());



    if (isAuthenticated == true) {
        console.log("user", userdetail);
        console.log("getUserData", getUserData);
    }
    console.log("AddCropInvert props", { CROPID });
    const MySwal = withReactContent(Swal);
    useEffect(() => {
        getTodayDate();
    }, [CROPID,]);
    const getTodayDate = () => {
        const today = userdetail?.APPDT ? new Date(userdetail.APPDT) : new Date();
        const formatted = today.toLocaleDateString("en-GB", {
            day: "2-digit",
            month: "short",
            year: "numeric",
        });
        setEndDate(formatted);
    };


    const [TableData, setTableData] = useState([]);
    const [rows, setRows] = useState([]);
    console.log(" dsfaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", rows)
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
        Rate: 0,
        ctype: "",
        Weight: 0,
        caret: "",
        Amount: 0,
        Shaid: "",
        Crdate: "",
    });


    const [detailData, setDetailData] = useState({
        packingMaterial: "",
        crateCount: "",
        Weight: "",
        Quantity: "",
        Grade: "",
        GradeName: "",
        cropInvert: "",
        PacMat: "",
        packingsize: "",
        Vdaid: "",
        labour: "",
        packer: "",
        VehicleNo: "",
        Amount: "",
        Rate: 0,
        shaid: "",
        shdaid: "",
        cdaid: "",
        cropid: "",
    });


    const GUID = ACSPLGUID.getNew();

    const RefOutWardStockWeight = useRef(null);
    const RefNaration = useRef(null);
    const Refvyavarmode = useRef(null)
    const SaveRef = useRef(null);
    const navigate = useNavigate();
    const ContactRef = useRef(null);
    const FarmerNameRef = useRef(null);
    const CropNameRef = useRef(null);
    const CropVarietyRef = useRef(null);
    const NarrationRef = useRef(null);
    const RateRef = useRef(null);


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
    // Add state to track if the modal-blocking history state is active
    const [isHistoryStatePushed, setIsHistoryStatePushed] = useState(false);

    useEffect(() => {
        const modal = document.getElementById("Direct-category");

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
    }, [navigate, route.DirectInward, MySwal]); // Keeping your original dependencies for context

    // ---

    // The key change should be in how you open the modal.
    // You need to push the history state *only* when you open the modal.

    // Example of modified onClick for 'Add Inward' button:
    /* <button
        className="btn btn-added"
        onClick={() => {
            const modal = document.getElementById("Direct-category");
    
            if (modal) {
                // Open modal logic...
                // ...
    
                // Pushes a temporary state to intercept the back action.
                window.history.pushState(null, "", window.location.pathname);
                setIsHistoryStatePushed(true); // Optional state update for tracking, but not strictly needed for the fix.
            }
        }}
    >
        ...
    </button>
    */

    // useEffect(() => {
    //     // Pushes a temporary state to intercept the back action.
    //     window.history.pushState(null, "", window.location.pathname);

    //     const handlePopState = () => {
    //         // Immediate re-push to block the browser from leaving the page.
    //         window.history.pushState(null, "", window.location.pathname);


    //         // 🌟 FIX 2: Use navigate() to force the navigation.
    //         // This is more reliable inside the asynchronous promise resolution.

    //         const modal = document.getElementById("Direct-category");
    //         if (modal) {
    //             modal.classList.remove("show");
    //             modal.style.display = "none";
    //             modal.setAttribute("aria-hidden", "true");
    //             const modalBackdrop = document.querySelector(".modal-backdrop");
    //             if (modalBackdrop) {
    //                 modalBackdrop.remove();
    //             }
    //             modal.removeAttribute("aria-modal");
    //             modal.removeAttribute("role");
    //             document.body.classList.remove("modal-open");
    //             document.body.style.overflow = "";
    //             document.body.style.paddingRight = "";
    //         }


    //     };

    //     window.addEventListener('popstate', handlePopState);

    //     return () => {
    //         window.removeEventListener('popstate', handlePopState);
    //     };
    // }, [navigate, route.MasterIndex, MySwal]); // Dependencies are still required!


    // useEffect(() => {
    //     ContactRef.current?.focus();
    // }, [CROPID]);

    useEffect(() => {
        const handleShortcut = (e) => {
            if (e.ctrlKey && (e.key === 'a' || e.key === 'A')) {
                e.preventDefault();
                // navigate(route.AddDirectInward);

                const modal = document.getElementById("Direct-category");

                if (modal) {
                    modal.classList.add("show");
                    modal.style.display = "block";
                    modal.setAttribute("aria-modal", "true");
                    modal.setAttribute("role", "dialog");
                    modal.removeAttribute("aria-hidden");
                    const backdrop = document.createElement("div");
                    backdrop.className = "modal-backdrop fade show";
                    document.body.appendChild(backdrop);
                    document.body.classList.add("modal-open");
                    document.body.style.overflow = "hidden";
                    document.body.style.paddingRight = "0px";
                }
            }
            if (e.ctrlKey && e.key === 'e') {
                e.preventDefault();
                // navigate(route.MasterIndex);
                showExitAlert();

            }
        };
        window.addEventListener('keydown', handleShortcut);
        return () => {
            window.removeEventListener('keydown', handleShortcut);
        };
    },);

    useEffect(() => {
        const fetchImplications = async () => {
            try {
                const headers = {
                    "Content-Type": "application/json",
                    Accept: "*/*",
                };
                const payload = {
                    companyid: userdetail?.companyID ? userdetail.companyID : "",
                    deptid: userdetail?.departmentID ? userdetail.departmentID : "",
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
                setCropType(implicationsDropdown);
            } catch (error) {
                console.error("Error fetching Farmer data:", error);
            }
        };
        fetchImplications();
    }, []);

    const fetchMasterData = async (CROPID) => {
        try {
            const payload1 = {
                cropid: CROPID,
                keyword: "%",
                companyid: userdetail?.companyID || "",
                deptid: userdetail?.departmentID || "",
            };

            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*"
            };

            const response1 = await axios.post(
                baseUrl.Url + "/backend/api/GET_CropDirectInvertMaster",
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
                CropName: apiData?.cropname || "",
                CropType: apiData?.ctype || "",
                CropArea: apiData?.carea || "",
                CropVariety: apiData?.cvariety || "",
                ExpectedHarvesting: apiData?.eproduction || "",
                Rate: apiData?.rate || 0,
                Weight: apiData?.weight || 0,
                caret: apiData?.caret || "",
                Amount: apiData?.amount || 0,
                Narration: apiData?.narr || "",
            }));
            setCropId(apiData?.cropid)
        } catch (error) {
            console.error("Error in Master API Call:", error);
        }

    };
    const fetchDetailsData = async (CROPID) => {
        try {
            const payload = {
                cropid: CROPID,
                companyid: userdetail?.companyID || "",
                deptid: userdetail?.departmentID || "",
            };
            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*"
            };
            const response = await axios.post(
                baseUrl.Url + "/backend/api/GET_CropDirectInvertDetail",
                payload,
                { headers }
            );
            if (response.status !== 200) throw new Error("Failed to fetch details data");
            console.log("Quotation Detail Data:", response.data);
            if (response.data.length > 0) {
                if (Array.isArray(response.data) && response.data.length > 0) {
                    const formattedRows = response.data.map(item => ({
                        shaid: item.shaid,
                        cropid: item.cropid,
                        cdaid: item.cdaid,
                        shdaid: item.shdaid || "",
                        staid: "",
                        ScheduleDate: item.shsdate || "",
                        VehicleNo: item.vehno || "",
                        packer: item.packer || "",
                        labour: item.worker || "",
                        packingMaterial: item.psize || "",
                        crateCount: item.cratecount || "",
                        cropInvert: item.cropinvert || "",
                        Quantity: item.pquntity || 0,
                        Weight: item.weight || 0,
                        Rate: item.rate || 0,
                        packingsize: item.packingsize || "",
                        isdeleted: 0,
                        Grade: item.cgrade || "",
                        Vdaid: item.vdaid || "",
                    }));

                    console.log("Formatted Rows", formattedRows);
                    setRows(formattedRows);


                } else {
                    setRows([]);  // No data case
                }
            }
        } catch (error) {
            console.error("Error fetching details data:", error);
        }
    };


    useEffect(() => {
        if (CROPID) {
            fetchMasterData(CROPID);
            fetchDetailsData(CROPID);
        }
    }, [CROPID]);


    useEffect(() => {
        const modal = document.getElementById('Direct-category');
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


    const getTodayDate1 = () => {
        const today = userdetail?.APPDT ? new Date(userdetail.APPDT) : new Date();
        const yyyy = today.getFullYear();
        const mm = String(today.getMonth() + 1).padStart(2, '0');
        const dd = String(today.getDate()).padStart(2, '0');
        return `${yyyy}-${mm}-${dd}`;
    };

    const [harvestingDate, setHarvestingDate] = useState(getTodayDate1());
    // useEffect(() => {
    //     if (StartDate && EndDate) {
    //         const start = new Date(StartDate);
    //         const end = new Date(EndDate);

    //         const diffTime = end.getTime() - start.getTime();
    //         const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));

    //         setFormData((prev) => ({
    //             ...prev,
    //             TotalDays: diffDays >= 0 ? diffDays : 0,
    //         }));
    //     }
    // }, [StartDate, EndDate]);



    const handleSuggestionSelect = (item) => {
        setFormData(prev => ({
            ...prev,
            PhoneNo: item.fcontactno,
            // Set other fields if needed:
            // FarmerName: item.name,
        }));

        fetchFarmerName(item.fcontactno)

        setSuggestions([]); // Clear suggestions after select
    };




    const fetchSuggestions = async (digitsOnly) => {
        console.log("000000000000000000000000000000")
        try {
            const payload = {
                fcontactno: "%",
                keyword: digitsOnly,
                companyid: userdetail?.companyID || "",
                deptid: userdetail?.departmentID || "",
            };
            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };

            const response = await axios.post(
                `${baseUrl.Url}/backend/api/GET_SuggestPhoneNumber/_Search`,
                payload,
                { headers }
            );

            // const response = await axios.post(
            //   "http://localhost:5200/api/GET_SuggestPhoneNumber",
            //   payload,
            //   {
            //     headers: {
            //       "Content-Type": "application/json",
            //     },
            //   }
            // );

            if (response.status === 200 && Array.isArray(response.data)) {
                setSuggestions(response.data); // Set the suggestions to state
                console.log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA", response.data)
            } else {
                setSuggestions([]);
            }
        } catch (error) {
            console.error("Failed to fetch suggestions:", error);
            setSuggestions([]);
        }
    };


    // Debounce the function to avoid too many API calls
    // const debouncedFetchSuggestions = debounce(fetchSuggestions, 300);




    const handleChange = (e) => {
        const { name, value } = e.target;
        // setFormData(prev => ({
        //     ...prev,
        //     [name]: value
        // }));

        // if (name === "PhoneNo" && value.length === 10) {
        //     fetchFarmerName(value);
        // }


        if (name === "PhoneNo") {
            setFormData(prev => ({
                ...prev,
                FarmerName: "",
            }));
            const digitsOnly = value.replace(/\D/g, ""); // Remove non-digit characters

            // Allow input only up to 10 digits
            if (digitsOnly.length > 10) return;

            // Update the state with cleaned number
            setFormData(prev => ({
                ...prev,
                [name]: digitsOnly
            }));

            if (digitsOnly.length >= 1) {
                console.log("wwwwwwwwwwwwwwwwwwwwwww")
                fetchSuggestions(digitsOnly);
            } else {
                // const keyword = "1111111111111";
                // digitsOnly = keyword;

                fetchSuggestions("111111111111111111")
            }

            // Validate once 10 digits are entered
            if (digitsOnly.length === 10) {
                const mobilePattern = /^[789][0-9]{9}$/;

                if (!mobilePattern.test(digitsOnly)) {
                    Swal.fire({
                        icon: "error",
                        title: "Invalid Mobile Number",
                        text: "Mobile number must be 10 digits and start with 7, 8, or 9.",
                        confirmButtonText: "OK",
                        allowOutsideClick: false,
                        allowEscapeKey: false,
                    }).then(() => {
                        setFormData(prev => ({
                            ...prev,
                            [name]: "",
                        }));
                    });
                    return;
                }

                // If valid, call API or fetch function
                fetchFarmerName(digitsOnly);
            }

            return; // Exit to avoid processing the rest
        }

        setFormData(prev => ({
            ...prev,
            [name]: value
        }));
    };

    const fetchtransectiondata = async (CROPID, FAID) => {
        try {
            const payload = {
                companyid: userdetail?.companyID || "",
                deptid: userdetail?.departmentID || "",
                refaid: CROPID || "",
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

            const transactions = response.data?.[0] || [];
            setTransections(transactions);
            return transactions;
        } catch (err) {
            console.error("Error fetching latest transactions:", err);
            return [];
        }
    };

    useEffect(() => {
        fetchtransectiondata(CROPID, FAID);
    }, [CROPID, FAID]);

    // Helper function to clear all input fields
    const clearInputFields = () => {
        setFormData({
            cropid: "", StartDate: "", FarmerName: "", faid: "", PhoneNo: "",
            CropName: "", CropArea: "", ExpectedHarvesting: "", CropVariety: "",
            Narration: "", cropInvert: "", Rate: 0, ctype: "", Weight: 0,
            caret: "", Amount: 0, Shaid: "", Crdate: "",
        });
        setDetailData({
            packingMaterial: "", crateCount: "", Weight: "", Quantity: "", Grade: "",
            GradeName: "", cropInvert: "", PacMat: "", packingsize: "", Vdaid: "",
            labour: "", packer: "", VehicleNo: "", Amount: "", Rate: 0, shaid: "",
            shdaid: "", cdaid: "",
        });
    };

    // Helper function to clear inputs and tables
    const resetFormAndTables = () => {
        clearInputFields();
        setRows([]);
        setTableData([]);
    };

    const filteredRows = rows.filter((row) => row.isdeleted == 0 || row.isdeleted == false);

    const totalWeight = filteredRows.reduce((sum, row) => sum + parseFloat(row.Weight || 0), 0);
    const totalAmount = filteredRows.reduce((sum, row) => sum + parseFloat(row.Amount || 0), 0);
    const totalCaret = filteredRows.reduce((sum, row) => sum + parseFloat(row.Quantity || 0), 0);


    const handleAddRow = async () => {
        const requiredFields = [
            { key: "PhoneNo", label: "Contact No." },
            { key: "FarmerName", label: "Farmer Name" },
            { key: "CropName", label: "Crop Name" },
        ];

        const missingFields = requiredFields.filter(
            (field) => !formData[field.key] || formData[field.key].toString().trim() === ""
        );

        if (missingFields.length > 0) {
            Swal.fire({
                icon: "warning",
                title: "Missing Information",
                html: `Please fill in the following required fields:`,
            });
            return;
        }

        if (
            !detailData.packingMaterial ||
            !detailData.Weight ||
            !detailData.Quantity ||
            !detailData.Grade ||
            !detailData.Rate
        ) {
            Swal.fire({
                icon: "warning",
                title: "Missing Information",
                text: "Please fill in all required fields (Packing Size, Inword Crate, Weight, Grade, and Rate) before adding.",
            });
            return;
        }

        const newRow = {
            ...detailData,
            cdaid: detailData.cdaid || ACSPLGUID.getNew(),  // unique ID
            isdeleted: 0,
        };

        setRows(prevRows => [...prevRows, newRow]);
        setTableData(prev => [...prev, newRow]);

        const transactions = await fetchtransectiondata(formData.cropid, formData.faid);
        console.log("latest transactions", transactions);

        let FFAID = ACSPLGUID.getNew();

        try {
            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };
            const farmercheck = {
                fcontactno: formData.PhoneNo,
                companyid: userdetail?.companyID ? userdetail.companyID : "",
            }
            const response = await axios.post(`${baseUrl.Url}/backend/api/GET_FARMERFROMCONTACT`, farmercheck, {
                headers,
            });
            if (response.status !== 200) throw new Error("Failed to fetch farmer name");
            const data = response.data;
            console.log(data, "data")
            if (data && data.length == 0) {
                const farmerpayload = {
                    faid: FFAID,
                    fname: formData?.FarmerName || "",
                    fcontactno: formData?.PhoneNo || "",
                    uaid: userdetail?.uaid ? userdetail.uaid : "",
                    date: harvestingDate,
                    uid: "",
                    fcmtoken: "",
                    refid: "",
                    fbirthdate: "",
                    fpanno: "",
                    faddharno: "",
                    fbankname: "",
                    facconame: "",
                    fbranchname: "",
                    faccountno: "",
                    fifsccode: "",
                    fpincode: "",
                    fstate: "",
                    fcity: "",
                    faddress: "",
                    fcroparea: formData.CropArea,
                    fcroptype: formData.CropName,
                    fstatus: "1",
                    fprofile: "",
                    companyid: userdetail?.companyID ? userdetail.companyID : "",
                    deptid: userdetail?.departmentID ? userdetail.departmentID : "",

                };
                console.log(farmerpayload, "farmerpayload")
                const farmerresponse1 = await axios.post(
                    baseUrl.Url + "/backend/api/SP_AddUpdFarmer",
                    farmerpayload,
                    { headers }
                );

                if (farmerresponse1.status !== 200) {
                    throw new Error("❌ Failed to save farmer data.");
                }
            } else {
                FFAID = response.data[0].faid;
            }

            // Calculate final totals including the newly added row
            const updatedRows = [...rows.filter(row => row.isdeleted === 0 || row.isdeleted === false), newRow];

            const finalTotalWeight = updatedRows.reduce((sum, row) => sum + parseFloat(row.Weight || 0), 0);
            const finalTotalAmount = updatedRows.reduce((sum, row) => sum + parseFloat(row.Amount || 0), 0);
            const finalTotalCaret = updatedRows.reduce((sum, row) => sum + parseFloat(row.Quantity || 0), 0);


            const nextTrnsr = (parseInt(transactions.trnsr) || 0) + 1;
            const newAmount = (parseFloat(detailData.Weight) || 0) * (parseFloat(detailData.Rate) || 0);
            const nextCbal = (parseFloat(transactions.cbal) || 0) + newAmount;
            let cropIdDetail = detailData.cropId ? detailData.cropId : ACSPLGUID.getNew();
            let cropid = cropId ? cropId : ACSPLGUID.getNew();
            const Invertpayload = {
                cropInvertDetail: [
                    {
                        cdaid: cropIdDetail,
                        cropid: cropid,
                        shsdate: harvestingDate,
                        vehno: detailData.VehicleNo,
                        packer: detailData.packer,
                        worker: detailData.labour,
                        psize: detailData.packingMaterial || "",
                        pquntity: detailData.Quantity,
                        cropinvert: detailData.Quantity || "",
                        cratecount: detailData.crateCount || "0",
                        companyid: userdetail?.companyID || "",
                        deptid: userdetail?.departmentID || "",
                        shaid: detailData.shaid,
                        vdaid: "",
                        cgrade: detailData.Grade || "",
                        shdaid: detailData.shdaid,
                        weight: detailData.Weight || "0",
                        rate: detailData.Rate || 0,
                    }
                ],
                cropInvertMaster: [
                    {
                        cropid: cropid,
                        crdate: harvestingDate,
                        faid: FFAID,
                        ctype: formData.CropType == undefined ? formData.CropName : formData.CropType,
                        carea: formData.CropArea,
                        contactno: formData.PhoneNo,
                        shaid: cropId ? cropId : ACSPLGUID.getNew(),
                        narr: formData.Narration || "",
                        eproduction: formData.ExpectedHarvesting,
                        cvariety: formData.CropVariety,
                        cropInvert: formData.Quantity,
                        weight: finalTotalWeight.toFixed(2),
                        caret: finalTotalCaret.toFixed(2),
                        amount: finalTotalAmount.toFixed(2),
                        companyid: userdetail?.companyID || "",
                        deptid: userdetail?.departmentID || "",
                        iscomplited: "1"
                    }
                ],
                allTransections: [
                    {
                        traid: cropIdDetail,
                        trnsr: nextTrnsr,
                        trnsid: "40",
                        trntid: "10",
                        trndt: harvestingDate,
                        cropid: formData.CropType == undefined ? formData.CropName : formData.CropType,
                        refaid: cropid,
                        maid: FFAID || "",
                        pamt: newAmount || 0,
                        cbal: nextCbal || 0,
                        advamt: transactions.advamt || 0,
                        asid: cropid,
                        isdeleted: false,
                        paymentdt: harvestingDate,
                        companyid: userdetail?.companyID || "",
                        deptid: userdetail?.departmentID || "",
                    }
                ],
                stockDetails: [
                    {
                        staid: cropIdDetail,
                        trndt: harvestingDate,
                        crop: formData.CropType == undefined ? formData.CropName : formData.CropType,
                        faid: formData.faid ? formData.faid : FFAID,
                        ciaid: cropid,
                        totcr: detailData.Quantity,
                        totwt: detailData.Weight,
                        trntid: "I",
                        cgrade: detailData.Grade || "",
                        isdeleted: false,
                        companyid: userdetail?.companyID || "",
                        deptid: userdetail?.departmentID || "",
                        reff: "",
                        rtotcr: detailData.Quantity
                    }
                ]
            };


            try {
                const response = await axios({
                    method: "POST",
                    url: baseUrl.Url + "/backend/api/DirectInvert",
                    data: JSON.stringify(Invertpayload),
                    headers: headers,
                });

                if (response.status === 200) {
                    console.log("✅ Save successful!");
                    Swal.fire({
                        icon: 'success',
                        title: 'Saved!',
                        text: 'Data saved successfully.',
                        timer: 1500,
                        showConfirmButton: false
                    });

                    // Refetch data from the server
                    await fetchMasterData(cropid, FFAID);
                    await fetchDetailsData(cropid, FFAID);
                    await fetchtransectiondata(cropid, FFAID);

                    // Clear detail form for the next entry
                    setDetailData({
                        packingMaterial: "", crateCount: "", Weight: "", Quantity: "", Grade: "",
                        GradeName: "", cropInvert: "", PacMat: "", packingsize: "", Vdaid: "",
                        labour: "", packer: "", VehicleNo: "", Amount: "", Rate: 0, shaid: "",
                        shdaid: "", cdaid: "",
                    });


                } else {
                    console.warn("⚠️ Unexpected response status:", response.status);
                    Swal.fire("Error", "Something went wrong. Please try again.", "error");
                }
            } catch (error) {
                console.error("❌ Error saving Direct Invert data:", error);
                Swal.fire("Error", "An error occurred while saving. Please check your inputs or try again later.", "error");
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

    const handleDelete = async (cropid, cdaid) => {
        console.log("HHHH FFFFFFF  111111", cropid, cdaid)
        Swal.fire({
            title: "Are you sure?",
            text: "You won’t be able to undo this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#d33",
            cancelButtonColor: "#3085d6",
            confirmButtonText: "Yes, delete it!",
            cancelButtonText: "Cancel",
        }).then(async (result) => {
            if (result.isConfirmed) {
                console.log("HHHH FFFFFFF ", cropid, cdaid)
                try {
                    const payload = {
                        cdaid: cdaid,
                        companyid: userdetail?.companyID || "",
                        deptid: userdetail?.departmentID || "",
                        // refaid: VDAID || ""
                        refaid: cropid || ""
                    };

                    const headers = {
                        "Content-Type": "application/json",
                        Accept: "*/*",
                    };

                    const response = await axios.post(
                        `${baseUrl.Url}/backend/api/SP_DeleteCropDirectInvertDetail`,
                        JSON.stringify(payload),
                        { headers }
                    );

                    if (response.status !== 200) throw new Error("Failed to delete data");

                    await Swal.fire({
                        title: response.data[0].responseCode === "ERROR"
                            ? "Deletion Not Allowed"
                            : "Deleted!",
                        text: response.data[0].responseCode === "ERROR"
                            ? "This record cannot be deleted because a subsequent transaction exists. Please delete the later transaction first, then try again."
                            : "The record has been successfully deleted.",
                        icon: response.data[0].responseCode === "ERROR" ? "error" : "success",
                        confirmButtonText: "OK",
                        customClass: {
                            confirmButton: response.data[0].responseCode === "ERROR"
                                ? "btn btn-danger"
                                : "btn btn-success",
                        },
                    });



                    try {
                        const payload = {
                            cropid: cropid,
                            companyid: userdetail?.companyID || "",
                            deptid: userdetail?.departmentID || "",
                        };
                        const headers = {
                            "Content-Type": "application/json",
                            Accept: "*/*"
                        };
                        const response = await axios.post(
                            baseUrl.Url + "/backend/api/GET_CropDirectInvertDetail",
                            payload,
                            { headers }
                        );
                        if (response.status !== 200) throw new Error("Failed to fetch details data");
                        console.log("Quotation Detail Data:", response.data);
                        const formattedRows = response.data.map(item => ({
                            shaid: item.shaid,
                            cropid: item.cropid,
                            cdaid: item.cdaid,
                            shdaid: item.shdaid || "",
                            staid: "",
                            ScheduleDate: item.shsdate || "",
                            VehicleNo: item.vehno || "",
                            packer: item.packer || "",
                            labour: item.worker || "",
                            packingMaterial: item.psize || "",
                            crateCount: item.cratecount || "",
                            cropInvert: item.cropinvert || "",
                            Quantity: item.pquntity || 0,
                            Weight: item.weight || 0,
                            Rate: item.rate || 0,
                            packingsize: item.packingsize || "",
                            isdeleted: 0,
                            Grade: item.cgrade || "",
                            Vdaid: item.vdaid || "",
                        }));

                        console.log("Formatted Rows", formattedRows);
                        setRows(formattedRows);




                    } catch (error) {
                        console.error("Error fetching details data:", error);
                    }

                    // Refresh schedule details

                } catch (error) {
                    console.error("Error deleting record:", error);
                }
            }
        });
    };

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
                const options = response.data.map(({ packingMaterial, maid, msize }) => ({
                    label: packingMaterial,
                    value: maid,
                    Number: msize
                    // msize: msize
                }));
                console.log("countcountcountcountcountcountcount 131231", options,)

                setpackingsize(options)
            } catch (error) {
                console.error("Error fetching Farmer data:", error);
            }
        };
        fetchpacking();
    }, []);

    const showExitAlert = () => {
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
                // Clear all inputs and tables on exit
                resetFormAndTables();

                if (onRefresh) {
                    onRefresh();
                }
                setSuggestions([]);

                const modalElement = document.getElementById("Direct-category");
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


    const fetchFarmerName = async (PhoneNo) => {
        // Don't clear farmer name if phone number is empty
        if (!PhoneNo || PhoneNo.trim() === "") {
            return;
        }

        try {
            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };
            const payload = {
                fcontactno: PhoneNo,
                companyid: userdetail?.companyID ? userdetail.companyID : "",
            };
            const response = await axios.post(`${baseUrl.Url}/backend/api/GET_FARMERFROMCONTACT`, payload, {
                headers,
            });
            if (response.status !== 200) throw new Error("Failed to fetch farmer name");
            const data = response.data;
            if (data && data.length > 0) {
                setFormData((prev) => ({
                    ...prev,
                    FarmerName: data[0].fname,
                    faid: data[0].faid,
                }));
            } else {
                // Only clear if we actually searched with a valid phone number
                setFormData((prev) => ({
                    ...prev,
                    FarmerName: "",
                }));
            }
        } catch (error) {
            console.error("Error fetching farmer name:", error);
            // Don't clear farmer name on error
        }
    };

    const handleEnterKey = (e, nextRef) => {
        if (e.key === 'Enter') {
            e.preventDefault();
            if (nextRef && nextRef.current) {
                nextRef.current.focus();
            }
        }
    };

    return (
        <>
            <div className="modal fade" id="Direct-category">
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
                                    <form>
                                        <div
                                            id="collapseOne"
                                            className="accordion-collapse collapse show"
                                            aria-labelledby="headingOne"
                                            data-bs-parent="#accordionExample"
                                        >
                                            <div className="accordion-body">


                                                <div className="row justify-content-center ">
                                                    <div className="col-6 col-md-6 col-lg-2 mb-3">
                                                        <div className="mb-0 add-product">
                                                            <label htmlFor="PhoneNo" className="form-label required">Contact No.</label>
                                                            <input
                                                                type="text"
                                                                id="PhoneNo"
                                                                className="form-control"
                                                                name="PhoneNo"
                                                                ref={ContactRef}
                                                                maxLength={10}
                                                                required
                                                                value={formData.PhoneNo}
                                                                onChange={handleChange}
                                                                onKeyDown={(e) => handleEnterKey(e, FarmerNameRef)}
                                                            />
                                                            {suggestions.length > 0 && (
                                                                <ul className="list-group position-fixed w-100 shadow-sm border-top-0 overflow-auto"

                                                                    style={{
                                                                        maxHeight: '150px', // this part still requires inline style for scroll limit
                                                                        zIndex: 1050,        // higher than default input fields
                                                                        maxWidth: '170px',
                                                                    }}
                                                                >
                                                                    {suggestions.map((item, index) => (
                                                                        <li
                                                                            key={index}
                                                                            className="list-group-item list-group-item-action "
                                                                            onClick={() => handleSuggestionSelect(item)}
                                                                            style={{ cursor: "pointer", border: '1px solid #f5f2f2ff' }}
                                                                        >
                                                                            {item.fcontactno}
                                                                        </li>
                                                                    ))}
                                                                </ul>
                                                            )}

                                                        </div>
                                                    </div>

                                                    <div className="col-6 col-md-6 col-lg-6 mb-2">
                                                        <label htmlFor="shopName" className="form-label required">Farmer Name</label>
                                                        <input
                                                            type="text"
                                                            className="form-control"
                                                            name="FarmerName"
                                                            required
                                                            value={formData.FarmerName || ''}
                                                            onChange={handleChange}
                                                            ref={FarmerNameRef}
                                                            onKeyDown={(e) => handleEnterKey(e, CropNameRef)}
                                                        />
                                                    </div>
                                                </div>




                                                <div className="row">
                                                    <div className="col-6 col-md-6 col-lg-2 mb-3">
                                                        <div className="mb-0 add-product">
                                                            <label htmlFor="StartDate" className="form-label">Harvesting Date</label>
                                                            <input
                                                                type="date"
                                                                className="form-control"
                                                                name="StartDate"
                                                                value={harvestingDate}
                                                                onChange={(e) => setHarvestingDate(e.target.value)}
                                                            />
                                                        </div>
                                                    </div>

                                                    <div className="col-6 col-md-6 col-lg-2 mb-3 ">
                                                        <div className="mb-0 add-product">
                                                            <label htmlFor="CropName" className="form-label required">Crop Name</label>
                                                            {/* <input
                                                                type="text"
                                                                className="form-control"
                                                                name="CropName"
                                                                required
                                                                value={formData.CropName || ''}
                                                                onChange={handleChange}
                                                            /> */}

                                                            <Select
                                                                placeholder="Select"
                                                                classNamePrefix="react-select"
                                                                options={cropType}
                                                                name="CropName"

                                                                openMenuOnFocus={true}
                                                                value={cropType.find(option => option.value === formData.CropName) || null}
                                                                // onChange={handleChange}
                                                                // onChange={(selectedOption) => {
                                                                //     const value = selectedOption ? selectedOption.value : "";
                                                                //     handleCropTypeChange(value, selectedOption); // ✅ let the function handle state
                                                                // }}
                                                                // ref={cropTypeRef}

                                                                onChange={(selectedOption) => {
                                                                    setFormData(prev => ({
                                                                        ...prev,
                                                                        CropName: selectedOption?.value || ""
                                                                    }));
                                                                    if (CropVarietyRef.current) {
                                                                        CropVarietyRef.current.focus();
                                                                    }
                                                                }}
                                                                ref={CropNameRef}
                                                                // onKeyDown={(e) => handleEnterKey(e, CropVarietyRef)}
                                                                title="Select Crop type"
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
                                                                ref={CropVarietyRef}
                                                                onKeyDown={(e) => handleEnterKey(e, NarrationRef)}

                                                            />
                                                        </div>
                                                    </div>

                                                    <div className="col-lg-6 col-md-6 mb-2">
                                                        <label htmlFor="Narration" className="form-label">Narration</label>
                                                        <input
                                                            id="Narration"
                                                            className="form-control"
                                                            name="Narration"
                                                            value={formData.Narration || ''}
                                                            onChange={handleChange} placeholder="Enter Description"
                                                            onKeyDown={(e) => handleKeyDown(e, PackingMaterialRef)}
                                                            ref={NarrationRef}
                                                        />
                                                    </div>


                                                </div>

                                                <div className="border p-3 rounded shadow-sm mb-1 mt-2">


                                                    <div className="row">
                                                        <div className="col-6 col-md-6 col-lg-2 mb-3">
                                                            <label className="form-label required">Packing Size</label>
                                                            <Select
                                                                ref={PackingMaterialRef}
                                                                openMenuOnFocus={true}
                                                                placeholder="Select Packing Size"
                                                                classNamePrefix="react-select"
                                                                required
                                                                // onKeyDown={(e) => handleEnterKey(e, QuantityRef)}
                                                                options={packingsize}
                                                                value={packingsize.find(option => option.value === detailData.packingMaterial) || ""}
                                                                // onChange={(selectedOption) => {
                                                                //     setDetailData(prev => ({ ...prev, packingMaterial: selectedOption?.value || "" }));
                                                                // }}

                                                                onChange={(selectedOption) => {
                                                                    const PacMat = parseFloat(selectedOption?.Number || 0); // convert to number
                                                                    const Quantity = parseFloat(detailData.Quantity || 0);      // get existing crateCount
                                                                    const weight = PacMat * Quantity;                    // calculate weight
                                                                    const Amount = (weight || 0) * (parseFloat(detailData.Rate) || 0)

                                                                    setDetailData(prev => ({
                                                                        ...prev,
                                                                        Amount: Amount,
                                                                        packingMaterial: selectedOption?.value,  // store as number
                                                                        Weight: weight || "",
                                                                        PacMat: PacMat || 0,
                                                                    }));
                                                                    if (QuantityRef.current) {
                                                                        QuantityRef.current.focus();
                                                                    }
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
                                                            <label htmlFor="Quantity" className="form-label required">Inword Crate</label>
                                                            <input
                                                                type="number"
                                                                ref={QuantityRef}
                                                                className="form-control"
                                                                name="Quantity"
                                                                value={detailData.Quantity}
                                                                onKeyDown={(e) => handleEnterKey(e, WeightRef)}
                                                                required
                                                                // onChange={(e) => setDetailData({ ...detailData, Quantity: e.target.value })}

                                                                onChange={(e) => {
                                                                    const Quantity = parseFloat(e.target.value || 0); // get user input
                                                                    const packingSize = parseFloat(detailData.PacMat || 0); // get selected packing size
                                                                    const weight = packingSize * Quantity; // calculate weight
                                                                    const Amount = weight * (parseFloat(detailData.Rate) || 0)

                                                                    setDetailData(prev => ({
                                                                        ...prev,
                                                                        Amount: Amount,
                                                                        Quantity: e.target.value, // keep as string for display
                                                                        Weight: isNaN(weight) ? "" : weight.toFixed(2), // optional: round weight
                                                                    }));
                                                                }}
                                                            />
                                                        </div>


                                                        <div className="col-6 col-md-6 col-lg-2 mb-3">
                                                            <div className="mb-0 add-product">
                                                                <label className="form-label required">Weight(KG)</label>
                                                                <input
                                                                    type="number"
                                                                    ref={WeightRef}
                                                                    className="form-control"
                                                                    name="Weight"
                                                                    required
                                                                    value={detailData.Weight}
                                                                    onKeyDown={(e) => handleEnterKey(e, RateDetailRef)}
                                                                    onChange={(e) => {
                                                                        const newWeight = e.target.value;
                                                                        const rate = parseFloat(detailData.Rate) || 0;
                                                                        const amount = (parseFloat(newWeight) || 0) * rate;
                                                                        setDetailData(prev => ({
                                                                            ...prev,
                                                                            Weight: newWeight,
                                                                            Amount: isNaN(amount) ? "" : amount.toFixed(2)
                                                                        }));
                                                                    }}
                                                                />
                                                            </div>
                                                        </div>

                                                        <div className="col-6 col-md-6 col-lg-2 mb-3">
                                                            <div className="mb-0 add-product">
                                                                <label className="form-label required">Rate</label>
                                                                <input
                                                                    type="number"
                                                                    ref={RateDetailRef}
                                                                    className="form-control"
                                                                    name="Rate"
                                                                    required
                                                                    value={detailData.Rate}
                                                                    onKeyDown={(e) => handleEnterKey(e, GradeRef)}
                                                                    onChange={(e) => {
                                                                        const newRate = parseFloat(e.target.value) || 0;
                                                                        const weight = parseFloat(detailData.Weight) || 0;
                                                                        const amount = weight * newRate;
                                                                        setDetailData(prev => ({
                                                                            ...prev,
                                                                            Rate: e.target.value,
                                                                            Amount: isNaN(amount) ? "" : amount.toFixed(2)
                                                                        }));
                                                                    }}
                                                                />
                                                            </div>
                                                        </div>

                                                        {/* <div className="col-6 col-md-6 col-lg-2 mb-3">
                                                            <div className="mb-0 add-product">
                                                                <label className="form-label required">Amount</label>
                                                                <input
                                                                    ref={CrateCountRef}
                                                                    className="form-control"
                                                                    name="Amount"
                                                                    required
                                                                    // value = {detailData.Quantity * }
                                                                    value={detailData.Amount}
                                                                    onChange={(e) => setDetailData({ ...detailData, Amount: e.target.value })}
                                                                />
                                                            </div>
                                                        </div> */}

                                                        <div className="col-6 col-md-6 col-lg-2 mb-3">
                                                            <div className="mb-0 add-product">
                                                                <label htmlFor="Crop Invert" className="form-label required">Grade</label>
                                                                <Select
                                                                    classNamePrefix="react-select"
                                                                    options={Grade}
                                                                    placeholder="Choose"
                                                                    name="Grade"
                                                                    ref={GradeRef}
                                                                    required
                                                                    // onKeyDown={(e) => handleEnterKey(e, AddRef)}
                                                                    value={Grade.find(option => option.value === detailData.Grade) || null}
                                                                    onChange={(selectedOption) => {
                                                                        setDetailData(prev => ({
                                                                            ...prev,
                                                                            Grade: selectedOption?.value || ""
                                                                        }));
                                                                        if (AddRef.current) {
                                                                            AddRef.current.focus();
                                                                        }
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
                                                                    ref={AddRef}
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
                                                                        <table className="table datanew table-bordered table-responsive-sm" style={{ width: "100%" }}>
                                                                            <thead className="thead-dark" style={{ top: 0, backgroundColor: "#343a40", color: "white", zIndex: 1000 }}>
                                                                                <tr>

                                                                                    <th className="text-center" >Packing Size</th>
                                                                                    {/* <th className="text-center" >Create Count</th> */}
                                                                                    <th className="text-center" >Inword Crate</th>
                                                                                    <th className="text-center" >Weight</th>
                                                                                    <th className="text-center" >Rate</th>
                                                                                    {/* <th className="text-center" >Amount</th> */}

                                                                                    <th className="text-center" >Grade</th>
                                                                                    <th className="text-center" >Action</th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody style={{ maxHeight: "325px", overflowY: "auto" }}>
                                                                                {
                                                                                    rows.filter((row) => row.isdeleted == 0 || row.isdeleted == false).length > 0 ? (
                                                                                        rows
                                                                                            .filter((row) => row.isdeleted == 0)
                                                                                            .map((row, index) => (
                                                                                                <tr key={row.cdaid || index}>

                                                                                                    <td className="text-center" style={{ wordBreak: "break-word", whiteSpace: "normal" }}>
                                                                                                        {(() => {
                                                                                                            const opt = packingsize.find(o => o.value === row.packingMaterial);
                                                                                                            return opt?.label || row.packingMaterial || "-";
                                                                                                        })()}
                                                                                                    </td>

                                                                                                    {/* <td className="text-center">{row.crateCount}</td> */}

                                                                                                    <td className="text-center">{row.Quantity}</td>
                                                                                                    <td className="text-center">{row.Weight}</td>
                                                                                                    <td className="text-center">{row.Rate || 0}</td>
                                                                                                    {/* <td className="text-center">{row.Amount}</td> */}

                                                                                                    <td className="text-center">
                                                                                                        {
                                                                                                            (() => {
                                                                                                                const opt = Grade.find(o => o.value.toString() === row.Grade?.toString());
                                                                                                                return opt?.label || row.Grade || "-";
                                                                                                            })()
                                                                                                        }
                                                                                                    </td>


                                                                                                    <td className="text-center">
                                                                                                        {/* <Link to="#" onClick={() => handleEdit(row.cdaid)} className="me-2 p-1" style={{ color: 'lightblue' }}>
                                                                                                            <Edit className="feather-edit" />
                                                                                                        </Link> */}
                                                                                                        <Link to="#" className="confirm-text p-2" onClick={(e) => { e.preventDefault(); handleDelete(row.cropid, row.cdaid); }}>
                                                                                                            <Trash2 className="feather-trash-2 text-danger" />
                                                                                                        </Link>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            ))
                                                                                    ) : (
                                                                                        <tr>
                                                                                            <td colSpan="6" className="text-center">No data available</td>
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
                                                                {/* <button
                                                                    type="submit"
                                                                    className="btn btn-submit w-100"
                                                                    ref={SaveRef}
                                                                    onClick={handleSubmit}
                                                                >
                                                                    Save
                                                                </button> */}
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

export default AddDirectInward;