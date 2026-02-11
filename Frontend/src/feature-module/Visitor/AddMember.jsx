import React, { useRef, useState, useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";

import Select from "react-select";
import "bootstrap/dist/css/bootstrap.min.css";
import {
    ArrowLeft,
    FilePlus
} from "feather-icons-react/build/IconComponents";
import axios from 'axios';
import { all_routes } from "../../Router/all_routes"
import { Accounts, ACSPLGUID, baseUrl, convertToISODate } from "../../core/json/custom";
import {
    PlusCircle,
    Trash2,
    Edit
} from "feather-icons-react/build/IconComponents";
import { getUserData } from "../../Context/UserData";
import Swal from "sweetalert2";
import withReactContent from "sweetalert2-react-content";
import { FaMapMarkerAlt } from "react-icons/fa";
import { preventDefault } from "@fullcalendar/core/internal";
import { current } from "@reduxjs/toolkit";
const AddMember = ({ SRAID, onRefresh }) => {
    console.log("VAID from Edit", SRAID);

    const route = all_routes;
    const navigate = useNavigate();
    const SabhasadnameRef = useRef(null)
    const NameRef = useRef(null);
    const RelationRef = useRef(null);
    const AgeRef = useRef(null);
    const EducationRef = useRef(null);
    const BusinessRef = useRef(null);
    const SarveRef = useRef(null);
    const PlotRef = useRef(null);
    const LandareaRef = useRef(null);
    const IrrigatedtypeRef = useRef(null);
    const CropnameRef = useRef(null);
    const cropvarietyRef = useRef(null);
    const cropareaRef = useRef(null);
    const GrowingRef = useRef(null);
    const HarvestingRef = useRef(null);
    const ExpectedRef = useRef(null);
    const TypeRef = useRef(null);
    const FertilizersRef = useRef(null);
    const QuantityRef = useRef(null);
    const CoastRef = useRef(null);
    const FertilizertypeRef = useRef(null);
    const chemicalRef = useRef(null);
    const ChemicaltypeRef = useRef(null);
    const ChemicalquanitityRef = useRef(null);
    const ChemicalcoastRef = useRef(null);

    const GUID = ACSPLGUID.getNew();
    const MySwal = withReactContent(Swal);
    const { userdetail } = getUserData();
    const [cropType, setCropType] = useState([]);

    const [tableData1, setTableData1] = useState([]);
    const [tableData2, setTableData2] = useState([]);
    const [tableData3, setTableData3] = useState([]);
    const [tableData4, setTableData4] = useState([]);
    const [tableData5, setTableData5] = useState([]);

    const [sellingtype, setsellingtype] = useState([]);
    const [statetype, setstatetype] = useState([]);
    const [Crop, setCrop] = useState([]);
    const [Fertilizer, setFertilizer] = useState([]);
    const [Relations, setRelations] = useState([]);
    const [Bussiness, setBussiness] = useState([]);
    const [ChemicalType, setChemicalType] = useState([]);
    const [LandType, setLandType] = useState([]);
    const [uploadMessage, setUploadMessage] = useState("");
    const [uploadMessage1, setUploadMessage1] = useState("");
    const [previewModalOpen, setPreviewModalOpen] = useState(false);
    const [messageColor, setMessageColor] = useState("green");
    const [filePreviews, setFilePreviews] = useState([]);
    const [previewModalOpen1, setPreviewModalOpen1] = useState(false);
    const [messageColor1, setMessageColor1] = useState("green");
    const [filePreviews1, setFilePreviews1] = useState([]);

    const [masterData, setMasterData] = useState({
        Sabhasadname: "",
        phonenumber: "",
        Birthdate: "",
        Sabhasadage: "",
        VillageName: "",
        Pincode: "",
        City: "",
        State: "",
        Primary: "",
        Secondary: "",
        Totalland: "",
        Irrigated: "",
        Nonirrigated: "",
        Nominal: "",
        Age: "",
        Nominesname: "",
        NomineeCity: "",
        NomineeState: "",
        photoes: [],
        photoes1: [],

    });

    const [formData, setFormData] = useState({
        Name: "",
        Relation: "",
        Age: "",
        Education: "",
        Business: "",
        Sarve: "",
        Plot: "",
        Landarea: "",
        Irrigatedtype: "",
        Cropname: "",
        cropvariety: "",
        croparea: "",
        Growing: "",
        Harvesting: "",
        Expected: "",
        Type: "",
        Fertilizers: "",
        Quantity: "",
        Coast: "",
        Fertilizertype: "",
        chemical: "",
        Chemicaltype: "",
        Chemicalquanitity: "",
        Chemicalcoast: "",
        vdaid: "",
        Sraid: "",
        Dsfaid: "",
        Dsaid: "",
        Dscaid: "",
        Dsfid: "",
        Dscid: "",
        isdeleted: "",
        // State: "",
        // City: "",
    });




    useEffect(() => {
        // Pushes a temporary state to intercept the back action.
        window.history.pushState(null, "", window.location.pathname);

        const handlePopState = () => {
            // Immediate re-push to block the browser from leaving the page.
            window.history.pushState(null, "", window.location.pathname);


            // 🌟 FIX 2: Use navigate() to force the navigation.
            // This is more reliable inside the asynchronous promise resolution.
            const modal = document.getElementById("AddSabhsad");
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

        window.addEventListener('popstate', handlePopState);

        return () => {
            window.removeEventListener('popstate', handlePopState);
        };
    }, [navigate, route.Member, MySwal]); // Dependencies are still required!

    // ... component render


    //     useEffect(() => {
    //     const handlePopState = (event) => {
    //         event.preventDefault();
    //         // If modal is open, prevent going back
    //         const modal = document.getElementById('AddSabhsad');
    //         if (modal && modal.classList.contains('show')) {
    //             // Re-push the state to stay on page
    //             window.history.pushState(null, '', window.location.href);
    //         }
    //     };

    //     window.history.pushState(null, '', window.location.href);
    //     window.addEventListener('popstate', handlePopState);

    //     return () => {
    //         window.removeEventListener('popstate', handlePopState);
    //     };
    // }, []);



    // useEffect(() => {
    //     const modal = document.getElementById('AddSabhsad');
    //     const handleShown = () => {
    //         Cropref.current?.focus();
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


    const handleMasterInputChange = async (e) => {
        const { name, value } = e.target;
        // setMasterData((prevData) => ({
        //     ...prevData,
        //     [name]: value,
        // }));

        const namePattern = /^[A-Za-z][A-Za-z\s]*$/;
        const phonePattern = /^[6-9][0-9]{9}$/;
        const numberPattern = /^\d{1,3}$/;
        const pincodePattern = /^[1-9][0-9]{5}$/;
        const landPattern = /^\d*\.?\d{0,2}$/;


        if (name === "Sabhasadname" || name === "VillageName" || name === "Primary" || name === "Secondary" || name === "Nominal") {
            if (value === "" || namePattern.test(value)) {
                setMasterData((prev) => ({ ...prev, [name]: value }));
            } else {
                Swal.fire({
                    icon: "error",
                    title: "Invalid Name",
                    text: "Only letters and spaces are allowed.",
                    allowOutsideClick: false,
                    allowEscapeKey: false,
                });
            }

        } else if (name === "phonenumber") {
            const onlyDigits = value.replace(/\D/g, "");

            setMasterData((prev) => ({ ...prev, [name]: onlyDigits }));

            if (onlyDigits.length === 10 && !phonePattern.test(onlyDigits)) {
                Swal.fire({
                    icon: "error",
                    title: "Invalid Phone Number",
                    text: "Phone number must be 10 digits and start with 6, 7, 8, or 9.",
                    allowOutsideClick: false,
                    allowEscapeKey: false,
                }).then(() => {
                    setMasterData((prev) => ({ ...prev, [name]: "" }));
                });
            }

        } else if (name === "Sabhasadage" || name === "Age") {
            if (value === "" || numberPattern.test(value)) {
                setMasterData((prev) => ({ ...prev, [name]: value }));
            } else {
                Swal.fire({
                    icon: "error",
                    title: "Invalid Age",
                    text: "Age must be a number up to 3 digits.",
                    allowOutsideClick: false,
                    allowEscapeKey: false,
                });
            }

        } else if (name === "Pincode") {
            const onlyDigits = value.replace(/\D/g, "");

            // Always update state to keep showing user input
            setMasterData((prev) => ({
                ...prev,
                [name]: onlyDigits,
            }));

            // Only validate once 6 digits are entered
            if (onlyDigits.length === 6 && !pincodePattern.test(onlyDigits)) {
                Swal.fire({
                    icon: "error",
                    title: "Invalid Pincode",
                    text: "Enter a valid 6-digit pincode (should not start with 0).",
                    allowOutsideClick: false,
                    allowEscapeKey: false,
                }).then(() => {
                    // Clear input
                    setMasterData((prev) => ({
                        ...prev,
                        Pincode: "",
                    }));
                });
            }
        } else if (name === "Totalland" || name === "Irrigated" || name === "Nonirrigated") {
            if (value === "" || landPattern.test(value)) {
                setMasterData((prev) => ({ ...prev, [name]: value }));
            } else {
                Swal.fire({
                    icon: "error",
                    title: "Invalid Land Area",
                    text: "Enter a valid number like 2 or 2.5 (up to 2 decimal places).",
                    allowOutsideClick: false,
                    allowEscapeKey: false,
                });
            }

        } else {
            // For other fields (like dropdowns or date)
            setMasterData((prev) => ({ ...prev, [name]: value }));
        }

        if (name === "Pincode") {
            if (/^\d{6}$/.test(value)) {
                try {
                    const headers = {
                        "Content-Type": "application/json",
                        Accept: "*/*",
                    };
                    const payload = { spincode: value };
                    const response = await axios.post(
                        baseUrl.Url + "/backend/api/StatePincode",
                        JSON.stringify(payload),
                        { headers }
                    );

                    if (response.status === 200) {
                        const data = response.data;
                        const districtdata = data.map(({ sdistrict, said }) => ({
                            label: sdistrict,
                            value: said,
                        }));
                        setsellingtype(districtdata);
                        const Statedata = data.map(({ sstatename, said }) => ({
                            label: sstatename,
                            value: said,
                        }));
                        setstatetype(Statedata);
                        setMasterData((prevData) => ({
                            ...prevData,
                            State: data[0]?.said || "",
                            City: data[0]?.said || "",
                        }));

                        // if (response.status === 200) {
                        //     const data = response.data;
                        //     const districtdata = data.map(({ sdistrict, said }) => ({
                        //         label: sdistrict,
                        //         value: said,
                        //     }));
                        //     setsellingtype(districtdata);
                        //     const Statedata = data.map(({ sstatename, said }) => ({
                        //         label: sstatename,
                        //         value: said,
                        //     }));
                        //     setstatetype(Statedata);
                        //     setFormData((prevData) => ({
                        //         ...prevData,
                        //         State: data[0]?.said || "",
                        //         City: data[0]?.said || "",
                        //     }));
                    } else {
                        console.error("Failed to fetch district and state for the pincode");
                    }
                } catch (error) {
                    console.error("Error fetching district and state data:", error);
                }
            } else {
                console.log("Invalid pincode entered. It must be 6 digits.");
            }
        }

    };

    const handleDetailInputChange = (e) => {
        const { name, value } = e.target;
        setFormData((prev) => ({
            ...prev,
            [name]: value,
        }));
    };

    useEffect(() => {
        if (SabhasadnameRef.current) {
            SabhasadnameRef.current.focus();
        }
    }, [userdetail]);

    useEffect(() => {
        const fetchLocationData = async () => {
            try {
                const headers = {
                    "Content-Type": "application/json",
                    Accept: "*/*",
                };
                const payload = { spincode: "%" };
                const response = await axios({
                    method: "POST",
                    url: baseUrl.Url + "/backend/api/StatePincode",
                    data: JSON.stringify(payload),
                    headers: headers,
                });
                if (response.status === 200) {
                    const data = response.data;
                    const districtdata = data
                        .map(({ sdistrict, said }) => ({
                            label: sdistrict,
                            value: said,
                        }));
                    setsellingtype(districtdata)
                    const Statedata = data
                        .map(({
                            sstatename, said }) => ({
                                label: sstatename,
                                value: said,
                            }));
                    setstatetype(Statedata);
                } else {
                    console.error("Failed to fetch district and state for the pincode");
                }
            } catch (error) {
                console.error("Error fetching district and state data:", error);
            }
        }
        fetchLocationData();
    }, []);

    useEffect(() => {
        fetchCropType();
        fetchFertilizer();
        fetchRelations();
        fetchBussinessType();
        fetchChemicalType();
        fetchLandType();
    }, []);

    const fetchCropType = async () => {
        try {
            const response = await axios.get(
                baseUrl.Url + "/backend/api/Implications/CROPTYPE",
            );
            if (response.status !== 200) throw new Error("Failed to fetch implications data");
            const data = response.data;
            const implicationDropdown = data.map(({ iTitle, iValue }) => ({
                label: iTitle,
                value: iValue,
            }));
            setCrop(implicationDropdown);
        } catch (error) {
            console.error("Error fetching implications:", error);
        }
    };

    const fetchFertilizer = async () => {
        try {
            const response = await axios.get(
                baseUrl.Url + "/backend/api/Implications/FERTILIZER",
            );
            if (response.status !== 200) throw new Error("Failed to fetch implications data");
            const data = response.data;
            const implicationDropdown = data.map(({ iTitle, iValue }) => ({
                label: iTitle,
                value: iValue,
            }));
            setFertilizer(implicationDropdown);
        } catch (error) {
            console.error("Error fetching implications:", error);
        }
    };

    const fetchRelations = async () => {
        try {
            const response = await axios.get(
                baseUrl.Url + "/backend/api/Implications/RELATIONS",
            );
            if (response.status !== 200) throw new Error("Failed to fetch implications data");
            const data = response.data;
            const implicationDropdown = data.map(({ iTitle, iValue }) => ({
                label: iTitle,
                value: iValue,
            }));
            setRelations(implicationDropdown);
        } catch (error) {
            console.error("Error fetching implications:", error);
        }
    };

    const fetchBussinessType = async () => {
        try {
            const response = await axios.get(
                baseUrl.Url + "/backend/api/Implications/BUSINESS",
            );
            if (response.status !== 200) throw new Error("Failed to fetch implications data");
            const data = response.data;
            const implicationDropdown = data.map(({ iTitle, iValue }) => ({
                label: iTitle,
                value: iValue,
            }));
            setBussiness(implicationDropdown);
        } catch (error) {
            console.error("Error fetching implications:", error);
        }
    };

    const fetchChemicalType = async () => {
        try {
            const response = await axios.get(
                baseUrl.Url + "/backend/api/Implications/CHEMICAL",
            );
            if (response.status !== 200) throw new Error("Failed to fetch implications data");
            const data = response.data;
            const implicationDropdown = data.map(({ iTitle, iValue }) => ({
                label: iTitle,
                value: iValue,
            }));
            setChemicalType(implicationDropdown);
        } catch (error) {
            console.error("Error fetching implications:", error);
        }
    };

    const fetchLandType = async () => {
        try {
            const response = await axios.get(
                baseUrl.Url + "/backend/api/Implications/LANDTYPE",
            );
            if (response.status !== 200) throw new Error("Failed to fetch implications data");
            const data = response.data;
            const implicationDropdown = data.map(({ iTitle, iValue }) => ({
                label: iTitle,
                value: iValue,
            }));
            setLandType(implicationDropdown);
        } catch (error) {
            console.error("Error fetching implications:", error);
        }
    };

    useEffect(() => {
        if (SRAID != "") {
            const fetchVisitorData = async () => {
                try {
                    const payload = {
                        "companyid": userdetail?.companyID ? userdetail.companyID : "",
                        "deptid": userdetail?.departmentID ? userdetail.departmentID : "",
                        "sraid": SRAID,
                        "keyword": "%"
                    };
                    const headers = {
                        "Content-Type": "application/json",
                        Accept: "*/*"
                    };
                    const response = await axios.post(
                        baseUrl.Url + "/backend/api/GET_SabhasadMaster",
                        payload,
                        { headers }
                    );
                    if (response.status !== 200) throw new Error("Failed to fetch data");
                    let apiData = response.data[0];
                    setMasterData((prev) => ({
                        ...prev,
                        Sraid: apiData.sraid || "",
                        phonenumber: apiData.contactnumber || "",
                        Birthdate: convertToISODate(apiData.birthdate) || "",
                        Sabhasadage: apiData.age || "",
                        Sabhasadname: apiData.sabhasadname || "",
                        Irrigated: apiData.irrigatedland || "",
                        Age: apiData.nominalage || "",
                        Nominal: apiData.nominalname || "",
                        Nominesname: apiData.nominalrelation || "",
                        Nonirrigated: apiData.nonirrigatedland || "",
                        Pincode: apiData.pincode || "",
                        Primary: apiData.primarybusiness || "",
                        Secondary: apiData.secoundarybusines || "",
                        State: apiData.state || "",
                        Totalland: apiData.totalland || "",
                        VillageName: apiData.villagename || "",
                        City: apiData.city || "",
                        photoes1: apiData.form ? [apiData.form] : [],
                        photoes: apiData.photos ? [apiData.photos] : [],
                    }));

                    console.log("Get_VisitorData  Data:", masterData);

                    const payload1 = {
                        companyid: userdetail?.companyID ? userdetail.companyID : "",
                        deptid: userdetail?.departmentID ? userdetail.departmentID : "",
                        sraid: SRAID,
                        dsfaid: "%"
                    };

                    const detailResponse = await axios.post(
                        baseUrl.Url + "/backend/api/GET_SFamilyInformation", // Change to your actual API
                        payload1,
                        { headers }
                    );

                    if (detailResponse.status !== 200) throw new Error("Failed to fetch detail data");

                    const detailData = detailResponse.data;

                    const mappedDetailData = detailData.map((item) => ({
                        Dsfaid: item.dsfaid || "",
                        Name: item.name || "",
                        Relation: item.relation || "",
                        Age: item.age || "",
                        Education: item.education || "",
                        Business: item.business || "",
                        isdeleted: item.isdeleted || 0,
                        relationname: item.relationname || "",
                        businessname: item.businessname || "",
                    }));

                    setTableData1(mappedDetailData);
                    const payload2 = {
                        companyid: userdetail?.companyID ? userdetail.companyID : "",
                        deptid: userdetail?.departmentID ? userdetail.departmentID : "",
                        sraid: SRAID,
                        dsaid: "%"
                    };

                    const detailResponse2 = await axios.post(
                        baseUrl.Url + "/backend/api/GET_SAgriculturalInformation", // Change to your actual API
                        payload2,
                        { headers }
                    );

                    if (detailResponse.status !== 200) throw new Error("Failed to fetch detail data");

                    const detailData2 = detailResponse2.data;

                    const mappedDetailData2 = detailData2.map((item) => ({
                        Dsaid: item.dsaid || "",
                        Sarve: item.surveynumber || "",
                        Plot: item.clusternumber || "",
                        Landarea: item.landarea || "",
                        Irrigatedtype: item.landtype || "",
                        isdeleted: item.isdeleted || 0,
                        landname: item.landname || "",
                    }));

                    setTableData2(mappedDetailData2);
                    const payload3 = {
                        companyid: userdetail?.companyID ? userdetail.companyID : "",
                        deptid: userdetail?.departmentID ? userdetail.departmentID : "",
                        sraid: SRAID,
                        dscaid: "%"
                    };

                    const detailResponse3 = await axios.post(
                        baseUrl.Url + "/backend/api/GET_SCropInformation", // Change to your actual API
                        payload3,
                        { headers }
                    );

                    if (detailResponse.status !== 200) throw new Error("Failed to fetch detail data");

                    const detailData3 = detailResponse3.data;

                    const mappedDetailData3 = detailData3.map((item) => ({
                        Dscaid: item.dscaid || "",
                        Cropname: item.cropname || "",
                        cropvariety: item.cvariety || "",
                        croparea: item.area || "",
                        Growing: item.growingperiod || "",
                        Harvesting: item.hduration || "",
                        Expected: item.ecrop || "",
                        Type: item.ctype || "",
                        isdeleted: item.isdeleted || 0,
                        cname: item.cname || "",
                        ntype: item.ntype || "",

                    }));

                    setTableData3(mappedDetailData3);

                    const payload4 = {
                        companyid: userdetail?.companyID ? userdetail.companyID : "",
                        deptid: userdetail?.departmentID ? userdetail.departmentID : "",
                        sraid: SRAID,
                        Dsfid: "%"
                    };

                    const detailResponse4 = await axios.post(
                        baseUrl.Url + "/backend/api/GET_SFertilizersName", // Change to your actual API
                        payload4,
                        { headers }
                    );

                    if (detailResponse.status !== 200) throw new Error("Failed to fetch detail data");

                    const detailData4 = detailResponse4.data;

                    const mappedDetailData4 = detailData4.map((item) => ({
                        Dsfid: item.dsfid || "",
                        Fertilizers: item.fertilizersname || "",
                        Quantity: item.quantity || "",
                        Coast: item.cost || "",
                        Fertilizertype: item.ftype || "",
                        isdeleted: item.isdeleted || 0,
                        fname: item.fname || "",
                    }));

                    setTableData4(mappedDetailData4);
                    const payload5 = {
                        companyid: userdetail?.companyID ? userdetail.companyID : "",
                        deptid: userdetail?.departmentID ? userdetail.departmentID : "",
                        sraid: SRAID,
                        dscid: "%"
                    };

                    const detailResponse5 = await axios.post(
                        baseUrl.Url + "/backend/api/GET_SChemicalName", // Change to your actual API
                        payload5,
                        { headers }
                    );

                    if (detailResponse.status !== 200) throw new Error("Failed to fetch detail data");

                    const detailData5 = detailResponse5.data;

                    const mappedDetailData5 = detailData5.map((item) => ({
                        Dscid: item.dscid || "",
                        chemical: item.cname || "",
                        Chemicaltype: item.ctype || "",
                        Chemicalquanitity: item.quantity || "",
                        Chemicalcoast: item.coast || "",
                        isdeleted: item.isdeleted || 0,
                        chemicalname: item.chemicalname || 0,
                    }));

                    setTableData5(mappedDetailData5);


                } catch (error) {
                    console.error("Error in Get_VisitorData API Call:", error);
                }
            };
            fetchVisitorData();
        }
    }, [SRAID]);

    useEffect(() => {
        const fetchImplications = async () => {
            try {
                const headers = {
                    "Content-Type": "application/json",
                    Accept: "*/*",
                };
                const payload = {
                    "companyid": userdetail?.companyID ? userdetail.companyID : "",
                    "deptid": userdetail?.departmentID ? userdetail.departmentID : "",
                };
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


    const FarmilyInformation = (e) => {
        e.preventDefault();

        const { Name, Relation, Age, Education, Business } = formData;

        if ((!/^[A-Za-z][A-Za-z\s]*$/.test(Name)) || (!Name.trim())) {
            NameRef.current?.focus();
            Swal.fire({
                icon: "error",
                text: "Name should contain only letters and spaces.",
                confirmButtonText: "OK",
            });

            return;
        }

        if (!Relation.trim()) {
            RelationRef.current?.focus();
            Swal.fire({
                icon: "error",
                text: "Relation is required.",
                confirmButtonText: "OK",
            });
            return;
        }

        if (!/^\d{1,3}$/.test(Age) || (!String(Age).trim())) {
            AgeRef.current?.focus();
            Swal.fire({
                icon: "error",
                text: "Please enter a valid Age (number only).",
                confirmButtonText: "OK",
            });
            return;
        }

        if (!/^[A-Za-z\s]+$/.test(Education) || (!Education.trim())) {
            EducationRef.current?.focus();
            Swal.fire({
                icon: "error",
                text: "Education should contain only letters and spaces.",
                confirmButtonText: "OK",
            });
            return;
        }

        if (!Business.trim()) {
            BusinessRef.current?.focus();
            Swal.fire({
                icon: "error",
                text: "Business is required.",
                confirmButtonText: "OK",
            });
            return;
        }
        let updatedTableData;
        if (formData.Dsfaid) {
            updatedTableData = tableData1.map((item) =>
                item.Dsfaid === formData.Dsfaid ? { ...item, ...formData, isdeleted: 0 } : item
            );
        } else {
            updatedTableData = [...tableData1, { ...formData, Dsfaid: ACSPLGUID.getNew(), isdeleted: 0 }];
        }
        setTableData1(updatedTableData);
        setFormData({
            Name: "",
            Relation: "",
            Age: "",
            Education: "",
            Business: "",

        });
    };

    const Agriculturalinformation = (e) => {
        e.preventDefault();

        const { Sarve, Plot, Landarea, Irrigatedtype } = formData;

        if ((!/^[A-Za-z0-9-/]+$/.test(Sarve)) || (!String(Sarve).trim())) {
            SarveRef.current?.focus();
            Swal.fire({
                icon: "error",
                text: "Sarve should contain only Numbers.",
                confirmButtonText: "OK",
            });
            return;
        }

        if ((!/^[A-Za-z0-9-/]+$/.test(Plot)) || (!String(Plot).trim())) {
            PlotRef.current?.focus();
            Swal.fire({
                icon: "error",
                text: "Plot should contain only Numbers.",
                confirmButtonText: "OK",
            });
            return;
        }

        if (!/^\d+(\.\d{1,2})?$/.test(Landarea) || (!Landarea.trim())) {
            LandareaRef.current?.focus();
            Swal.fire({
                icon: "error",
                text: "Please enter a valid Land area (number only).",
                confirmButtonText: "OK",
            });
            return;
        }

        if (!Irrigatedtype.trim()) {
            IrrigatedtypeRef.current?.focus();
            Swal.fire({
                icon: "error",
                text: "Irrigated type is required.",
                confirmButtonText: "OK",
            });
            return;
        }

        let updatedTableData;
        if (formData.Dsaid) {
            updatedTableData = tableData2.map((item) =>
                item.Dsaid === formData.Dsaid ? { ...item, ...formData, isdeleted: 0 } : item
            );
        } else {
            updatedTableData = [...tableData2, { ...formData, Dsaid: ACSPLGUID.getNew(), isdeleted: 0 }];
        }
        setTableData2(updatedTableData);
        setFormData({
            Sarve: "",
            Plot: "",
            Landarea: "",
            Irrigatedtype: "",

        });
    };

    const YearOfCrop = (e) => {
        e.preventDefault();

        const { Cropname, cropvariety, croparea, Growing, Harvesting, Expected, Type } = formData;

        if (!Cropname.trim()) {
            CropnameRef.current?.focus();
            Swal.fire({
                icon: "error",
                text: "Crop name is required.",
                confirmButtonText: "OK",
            });
            return;
        }

        if ((!/^[A-Za-z\s]+$/.test(cropvariety)) || (!cropvariety.trim())) {
            cropvarietyRef.current?.focus();
            Swal.fire({
                icon: "error",
                text: "Crop variety should contain only letters and spaces.",
                confirmButtonText: "OK",
            });
            return;
        }

        if (!/^\d+(\.\d{1,2})?$/.test(croparea) || (!croparea.trim())) {
            cropareaRef.current?.focus();
            Swal.fire({
                icon: "error",
                text: "Please enter a valid crop area (number only).",
                confirmButtonText: "OK",
            });
            return;
        }

        if (!/^[A-Za-z0-9\s]+$/.test(Growing) || (!Growing.trim())) {
            GrowingRef.current?.focus();
            Swal.fire({
                icon: "error",
                text: "Please enter a valid Growing Letters and spaces (e.g, 3 months).",
                confirmButtonText: "OK",
            });
            return;
        }

        if (!/^[A-Za-z0-9\s\-]+$/.test(Harvesting) || (!Harvesting.trim())) {
            HarvestingRef.current?.focus();
            Swal.fire({
                icon: "error",
                text: "Please enter a valid Harvesting Letters and spaces (e.g, 3 months).",
                confirmButtonText: "OK",
            });
            return;
        }

        if (!/^\d+(\.\d{1,2})?$/.test(Expected) || (!Expected.trim())) {
            ExpectedRef.current?.focus();
            Swal.fire({
                icon: "error",
                text: "Please enter a valid Expected (number only).",
                confirmButtonText: "OK",
            });
            return;
        }

        if (!Type.trim()) {
            TypeRef.current?.focus();
            Swal.fire({
                icon: "error",
                text: "Crop Type is required.",
                confirmButtonText: "OK",
            });
            return;
        }

        let updatedTableData;
        if (formData.Dscaid) {
            updatedTableData = tableData3.map((item) =>
                item.Dscaid === formData.Dscaid ? { ...item, ...formData, isdeleted: 0 } : item
            );
        } else {
            updatedTableData = [...tableData3, { ...formData, Dscaid: ACSPLGUID.getNew(), isdeleted: 0 }];
        }
        setTableData3(updatedTableData);
        setFormData({
            Cropname: "",
            cropvariety: "",
            croparea: "",
            Growing: "",
            Harvesting: "",
            Expected: "",
            type: "",

        });
    };

    const addFertilizer = (e) => {
        e.preventDefault();

        const { Fertilizers, Quantity, Coast, Fertilizertype } = formData;

        if ((!/^[A-Za-z\s]+$/.test(Fertilizers)) || (!Fertilizers.trim())) {
            FertilizersRef.current?.focus();
            Swal.fire({
                icon: "error",
                text: "Fertilizers Name should contain only letters and spaces.",
                confirmButtonText: "OK",
            });
            return;
        }

        if ((!/^\d+$/.test(Quantity)) || (!String(Quantity).trim())) {
            QuantityRef.current?.focus();
            Swal.fire({
                icon: "error",
                text: "Quantity should contain only Numbers.",
                confirmButtonText: "OK",
            });
            return;
        }

        if ((!/^\d+$/.test(Coast)) || (!String(Coast).trim())) {
            CoastRef.current?.focus();
            Swal.fire({
                icon: "error",
                text: "Coast should contain only Numbers.",
                confirmButtonText: "OK",
            });
            return;
        }

        if (!Fertilizertype.trim()) {
            FertilizertypeRef.current?.focus();
            Swal.fire({
                icon: "error",
                text: "Fertilizer type is required.",
                confirmButtonText: "OK",
            });
            return;
        }

        let updatedTableData;
        if (formData.Dsfid) {
            updatedTableData = tableData4.map((item) =>
                item.Dsfid === formData.Dsfid ? { ...item, ...formData, isdeleted: 0 } : item
            );
        } else {
            updatedTableData = [...tableData4, { ...formData, Dsfid: ACSPLGUID.getNew(), isdeleted: 0 }];
        }
        setTableData4(updatedTableData);
        setFormData({
            Fertilizers: "",
            Quantity: "",
            Coast: "",
            Fertilizertype: "",

        });
    };
    const AddChemical = (e) => {
        e.preventDefault();

        const { chemical, Chemicaltype, Chemicalquanitity, Chemicalcoast } = formData;

        if ((!/^[A-Za-z\s]+$/.test(chemical)) || (!chemical.trim())) {
            chemicalRef.current?.focus();
            Swal.fire({
                icon: "error",
                text: "Chemical Name should contain only letters and spaces.",
                confirmButtonText: "OK",
            });
            return;
        }

        if (!Chemicaltype.trim()) {
            ChemicaltypeRef.current?.focus();
            Swal.fire({
                icon: "error",
                text: "Chemical type is required.",
                confirmButtonText: "OK",
            });
            return;
        }

        if ((!/^\d+(\.\d{1,2})?$/.test(Chemicalquanitity))) {
            ChemicalquanitityRef.current?.focus();
            Swal.fire({
                icon: "error",
                text: "Quanitity should contain only Numbers.",
                confirmButtonText: "OK",
            });
            return;
        }

        if ((!/^\d+(\.\d{1,2})?$/.test(Chemicalquanitity))) {
            ChemicalcoastRef.current?.focus();
            Swal.fire({
                icon: "error",
                text: "Quanitity should contain only Numbers.",
                confirmButtonText: "OK",
            });
            return;
        }


        let updatedTableData;
        if (formData.Dscid) {
            updatedTableData = tableData5.map((item) =>
                item.Dscid === formData.Dscid ? { ...item, ...formData, isdeleted: 0 } : item
            );
        } else {
            updatedTableData = [...tableData5, { ...formData, Dscid: ACSPLGUID.getNew(), isdeleted: 0 }];
        }
        setTableData5(updatedTableData);
        setFormData({
            chemical: "",
            Chemicaltype: "",
            Chemicalquanitity: "",
            Chemicalcoast: "",

        });
    };


    const handleEdit1 = (e, Dsfaid) => {
        e.preventDefault();
        console.log("Editing row with fdaid:", Dsfaid);
        const filteredProduct = tableData1.find((row) => row.Dsfaid === Dsfaid);
        if (filteredProduct) {
            setFormData({
                Dsfaid: filteredProduct.Dsfaid,
                Name: filteredProduct.Name,
                Relation: filteredProduct.Relation,
                Age: filteredProduct.Age,
                Education: filteredProduct.Education,
                Business: filteredProduct.Business,
                isdeleted: 0,
            });
        }
    };
    const handleDelete1 = (e, Dsfaid) => {
        e.preventDefault();
        Swal.fire({
            title: "Are you sure?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#d33",
            cancelButtonColor: "#3085d6",
            confirmButtonText: "Yes, delete it!",
            cancelButtonText: "Cancel",
        }).then((result) => {
            if (result.isConfirmed) {
                setTableData1((prevRows) => {
                    const updatedRows = prevRows.map((row) =>
                        row.Dsfaid === Dsfaid ? { ...row, isdeleted: 1 } : row
                    );
                    console.log("Updated Rows:", updatedRows);
                    return updatedRows;
                });
                Swal.fire({
                    icon: "success",
                    title: "Deleted!",
                    text: "Record has been deleted successfully.",
                    confirmButtonText: "OK",
                });
            }
        });
    };


    const handleEdit2 = (e, Dsaid) => {
        e.preventDefault();
        console.log("Editing row with fdaid:", Dsaid);
        const filteredProduct = tableData2.find((row) => row.Dsaid === Dsaid);
        if (filteredProduct) {
            setFormData({
                Dsaid: filteredProduct.Dsaid,
                Sarve: filteredProduct.Sarve,
                Plot: filteredProduct.Plot,
                Landarea: filteredProduct.Landarea,
                Irrigatedtype: filteredProduct.Irrigatedtype,
                isdeleted: 0,
            });
        }
    };
    const handleDelete2 = (e, Dsaid) => {
        e.preventDefault();
        Swal.fire({
            title: "Are you sure?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#d33",
            cancelButtonColor: "#3085d6",
            confirmButtonText: "Yes, delete it!",
            cancelButtonText: "Cancel",
        }).then((result) => {
            if (result.isConfirmed) {
                setTableData2((prevRows) => {
                    const updatedRows = prevRows.map((row) =>
                        row.Dsaid === Dsaid ? { ...row, isdeleted: 1 } : row
                    );
                    console.log("Updated Rows:", updatedRows);
                    return updatedRows;
                });
                Swal.fire({
                    icon: "success",
                    title: "Deleted!",
                    text: "Record has been deleted successfully.",
                    confirmButtonText: "OK",
                });
            }
        });
    };

    const handleEdit3 = (e, Dscaid) => {
        e.preventDefault();
        console.log("Editing row with fdaid:", Dscaid);
        const filteredProduct = tableData3.find((row) => row.Dscaid === Dscaid);
        if (filteredProduct) {
            setFormData({
                Dscaid: filteredProduct.Dscaid,
                Cropname: filteredProduct.Cropname,
                cropvariety: filteredProduct.cropvariety,
                croparea: filteredProduct.croparea,
                Growing: filteredProduct.Growing,
                Harvesting: filteredProduct.Harvesting,
                Expected: filteredProduct.Expected,
                Type: filteredProduct.Type,
                isdeleted: 0,
            });
        }
    };
    const handleDelete3 = (e, Dscaid) => {
        e.preventDefault();
        Swal.fire({
            title: "Are you sure?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#d33",
            cancelButtonColor: "#3085d6",
            confirmButtonText: "Yes, delete it!",
            cancelButtonText: "Cancel",
        }).then((result) => {
            if (result.isConfirmed) {
                setTableData3((prevRows) => {
                    const updatedRows = prevRows.map((row) =>
                        row.Dscaid === Dscaid ? { ...row, isdeleted: 1 } : row
                    );
                    console.log("Updated Rows:", updatedRows);
                    return updatedRows;
                });
                Swal.fire({
                    icon: "success",
                    title: "Deleted!",
                    text: "Record has been deleted successfully.",
                    confirmButtonText: "OK",
                });
            }
        });
    };


    const handleEdit4 = (e, Dsfid) => {
        e.preventDefault();
        console.log("Editing row with fdaid:", Dsfid);
        const filteredProduct = tableData4.find((row) => row.Dsfid === Dsfid);
        if (filteredProduct) {
            setFormData({
                Dsfid: filteredProduct.Dsfid,
                Fertilizers: filteredProduct.Fertilizers,
                Quantity: filteredProduct.Quantity,
                Coast: filteredProduct.Coast,
                Fertilizertype: filteredProduct.Fertilizertype,
                isdeleted: 0,
            });
        }
    };
    const handleDelete4 = (e, Dsfid) => {
        e.preventDefault();
        Swal.fire({
            title: "Are you sure?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#d33",
            cancelButtonColor: "#3085d6",
            confirmButtonText: "Yes, delete it!",
            cancelButtonText: "Cancel",
        }).then((result) => {
            if (result.isConfirmed) {
                setTableData4((prevRows) => {
                    const updatedRows = prevRows.map((row) =>
                        row.Dsfid === Dsfid ? { ...row, isdeleted: 1 } : row
                    );
                    console.log("Updated Rows:", updatedRows);
                    return updatedRows;
                });
                Swal.fire({
                    icon: "success",
                    title: "Deleted!",
                    text: "Record has been deleted successfully.",
                    confirmButtonText: "OK",
                });
            }
        });
    };

    const handleEdit5 = (e, Dscid) => {
        e.preventDefault();
        console.log("Editing row with fdaid:", Dscid);
        const filteredProduct = tableData5.find((row) => row.Dscid === Dscid);
        if (filteredProduct) {
            setFormData({
                Dscid: filteredProduct.Dscid,
                chemical: filteredProduct.chemical,
                Chemicaltype: filteredProduct.Chemicaltype,
                Chemicalquanitity: filteredProduct.Chemicalquanitity,
                Chemicalcoast: filteredProduct.Chemicalcoast,
                isdeleted: 0,
            });
        }
    };
    const handleDelete5 = (e, Dscid) => {
        e.preventDefault();
        Swal.fire({
            title: "Are you sure?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#d33",
            cancelButtonColor: "#3085d6",
            confirmButtonText: "Yes, delete it!",
            cancelButtonText: "Cancel",
        }).then((result) => {
            if (result.isConfirmed) {
                setTableData5((prevRows) => {
                    const updatedRows = prevRows.map((row) =>
                        row.Dscid === Dscid ? { ...row, isdeleted: 1 } : row
                    );
                    console.log("Updated Rows:", updatedRows);
                    return updatedRows;
                });
                Swal.fire({
                    icon: "success",
                    title: "Deleted!",
                    text: "Record has been deleted successfully.",
                    confirmButtonText: "OK",
                });
            }
        });
    };

    const handleSubmit = (e) => {
        e.preventDefault(e);

        if (!masterData.photoes || masterData.photoes.length === 0) {
            Swal.fire({
                icon: "error",
                title: "Photo Validation Error",
                text: "Please capture or upload a photo.",
                confirmButtonText: "OK",
                allowOutsideClick: false,
                allowEscapeKey: false
            });
            return;
        }

        if (!masterData.photoes1 || masterData.photoes1.length === 0) {
            Swal.fire({
                icon: "error",
                title: "Photo Validation Error",
                text: "Please capture or upload a Form.",
                confirmButtonText: "OK",
                allowOutsideClick: false,
                allowEscapeKey: false
            });
            return;
        }

        showConfirmationAlert(e);
    };

    const showConfirmationAlert = (event) => {
        MySwal.fire({
            title: "Are you sure?",
            text: "Do you want to save this information?",
            showCancelButton: true,
            confirmButtonColor: "#00ff00",
            confirmButtonText: "Save",
            cancelButtonColor: "#092C4C",
            cancelButtonText: "Cancel",
        })
            .then((result) => {
                if (result.isConfirmed) {
                    handlePayloadSubmition(event);
                }
            });
    };

    const handlePayloadSubmition = async () => {

        console.log("form Submit...", masterData)
        console.log("tableData", tableData1, tableData2, tableData3, tableData4, tableData5)
        console.log("formData", formData)


        const headers = {
            "Content-Type": "application/json",
            Accept: "*/*",
        };

        try {
            const payload = {
                "sraid": SRAID ? SRAID : GUID,
                "sabhasadname": masterData.Sabhasadname,
                "contactnumber": masterData.phonenumber,
                "birthdate": masterData.Birthdate || "",
                "age": masterData.Sabhasadage || 0,
                "villagename": masterData.VillageName,
                "pincode": masterData.Pincode,
                "city": masterData.City,
                "state": masterData.State,
                "primarybusiness": masterData.Primary,
                "secoundarybusines": masterData.Secondary,
                "totalland": masterData.Totalland,
                "irrigatedland": masterData.Irrigated,
                "nonirrigatedland": masterData.Nonirrigated,
                "nominalname": masterData.Nominal,
                "nominalage": masterData.Age,
                "nominalrelation": masterData.Nominesname,
                "photos": masterData.photoes?.toString(),
                "form": masterData.photoes1?.toString(),
                "companyid": userdetail?.companyID ? userdetail.companyID : "",
                "deptid": userdetail?.departmentID ? userdetail.departmentID : "",
            };

            console.log("jama payload", payload)



            const res = await axios.post(
                `${baseUrl.Url}/backend/api/SP_AddUpdSabhasadMaster`,
                JSON.stringify(payload),
                { headers }
            );

            const payload1 = tableData1.map(item => ({

                "dsfaid": item.Dsfaid ? item.Dsfaid : ACSPLGUID.getNew(),
                "sraid": SRAID ? SRAID : GUID,
                "name": item.Name,
                "relation": item.Relation,
                "age": item.Age,
                "education": item.Education,
                "business": item.Business,
                "companyid": userdetail?.companyID ? userdetail.companyID : "",
                "deptid": userdetail?.departmentID ? userdetail.departmentID : "",
                "isdeleted": item.isdeleted === 1 ? true : false,
            }));


            const res1 = await axios.post(
                `${baseUrl.Url}/backend/api/SP_AddUpdSFamilyInformation`,
                JSON.stringify(payload1),
                { headers }
            );



            const payload2 = tableData2.map(item => ({
                "dsaid": item.Dsaid ? item.Dsaid : ACSPLGUID.getNew(),
                "sraid": SRAID ? SRAID : GUID,
                "surveynumber": item.Sarve,
                "clusternumber": item.Plot,
                "landarea": item.Landarea,
                "landtype": item.Irrigatedtype,
                "companyid": userdetail?.companyID ? userdetail.companyID : "",
                "deptid": userdetail?.departmentID ? userdetail.departmentID : "",
                "isdeleted": item.isdeleted === 1 ? true : false,
            }));


            const res2 = await axios.post(
                `${baseUrl.Url}/backend/api/SP_AddUpdSAgriculturalInformation`,
                JSON.stringify(payload2),
                { headers }
            );


            const payload3 = tableData3.map(item => ({
                "dscaid": item.Dscaid ? item.Dscaid : ACSPLGUID.getNew(),
                "sraid": SRAID ? SRAID : GUID,
                "cropname": item.Cropname || "",
                "cvariety": item.cropvariety || "",
                "area": item.croparea || "",
                "growingperiod": item.Growing || "",
                "hduration": item.Harvesting || "",
                "ecrop": item.Expected || "",
                "ctype": item.Type || "",
                "companyid": userdetail?.companyID ? userdetail.companyID : "",
                "deptid": userdetail?.departmentID ? userdetail.departmentID : "",
                "isdeleted": item.isdeleted === 1 ? true : false,
            }));


            const res3 = await axios.post(
                `${baseUrl.Url}/backend/api/SP_AddUpdSCropInformation`,
                JSON.stringify(payload3),
                { headers }
            );


            const payload4 = tableData4.map(item => ({
                "dsfid": item.Dsfid ? item.Dsfid : ACSPLGUID.getNew(),
                "sraid": SRAID ? SRAID : GUID,
                "fertilizersname": item.Fertilizers || "",
                "quantity": item.Quantity || 0,
                "coast": item.Coast || 0,
                "ftype": item.Fertilizertype || "",
                "companyid": userdetail?.companyID ? userdetail.companyID : "",
                "deptid": userdetail?.departmentID ? userdetail.departmentID : "",
                "isdeleted": item.isdeleted === 1 ? true : false,
            }));


            const res4 = await axios.post(
                `${baseUrl.Url}/backend/api/SP_AddUpdSFertilizersName`,
                JSON.stringify(payload4),
                { headers }
            );


            const payload5 = tableData5.map(item => ({
                "dscid": item.Dscid ? item.Dscid : ACSPLGUID.getNew(),
                "sraid": SRAID ? SRAID : GUID,
                "cname": item.chemical || "",
                "ctype": item.Chemicaltype || "",
                "quantity": item.Chemicalquanitity || 0,
                "coast": item.Chemicalcoast || 0,
                "companyid": userdetail?.companyID ? userdetail.companyID : "",
                "deptid": userdetail?.departmentID ? userdetail.departmentID : "",
                "isdeleted": item.isdeleted === 1 ? true : false,
            }));


            const res5 = await axios.post(
                `${baseUrl.Url}/backend/api/SP_AddUpdSChemicalName`,
                JSON.stringify(payload5),
                { headers }
            );

            Swal.fire({
                icon: "success",
                title: "Saved!",
                text: "Data saved successfully.",
                confirmButtonText: "OK",
            })
            console.log("Sabhasad Entries Submitted");
        } catch (error) {
            console.error("Submission Error:", error);
            Swal.fire({
                icon: "error",
                title: "ओह... काहीतरी चुकीचे झाले",
                text: "डेटा जतन करताना काहीतरी चुकले.",
                confirmButtonText: "ठीक आहे",
            });
        }

        try {
            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };

            const payload = {
                contactnumber: masterData.phonenumber,
                companyid: userdetail?.companyID || "",
                deptid: userdetail?.departmentID || "",
            };

            const response = await axios.post(
                `${baseUrl.Url}/backend/api/GET_SContactNumberCheck`,
                payload,
                { headers }
            );

            if (response.status === 200) {
                const data = response.data;

                if (Array.isArray(data) && data.length > 0) {

                    return;
                }

                const user = {
                    uaid: GUID,
                    ucompanyid: userdetail?.companyID || "",
                    ubranchid: userdetail?.departmentID || "",
                    upacsid: "",
                    ustoreid: "",
                    uwarehouseid: "",
                    uroleid: "",
                    utitle: "",
                    uforename: masterData.Sabhasadname,
                    udescription: "",
                    uemailaddress: "",
                    ulogintype: "",
                    umobilenumber: masterData.phonenumber,
                    upassword: "",
                    ugroup: "",
                    uaccesspolicy: "",
                    uaccessdays: "",
                    uaccesstimestart: "",
                    uaccesstimeend: "",
                    uregistrationdate: "",
                    uexpirydate: "",
                    ureferencekey: "",
                    uloginkey: "",
                    ulastlogindatetime: "",
                    ulogincount: "",
                    ustatusid: "",
                    ugoogleclientid: "",
                    ufacebookclientid: "",
                    uapplicationkey: "",
                    uapplicationdate: "",
                    uapplicationhome: "",
                    ulanguageid: "",
                };

                const saveRes = await axios.post(
                    `${baseUrl.Url}/backend/api/SP_AddUpdUserMasters`,
                    JSON.stringify(user),
                    { headers }
                );

                Swal.fire({
                    icon: "success",
                    title: "User saved successfully!",
                    confirmButtonText: "OK",
                });
            }
        } catch (error) {
            console.error("Error during phone check or save:", error);
            Swal.fire({
                icon: "error",
                title: "Error",
                text: "Something went wrong while saving.",
                confirmButtonText: "OK",
            });
        }
    };

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
    }, [masterData, handleSubmit]);

    const validateinput = (e) => {

        const requiredFields = [
            "Sabhasadname",
            "phonenumber",
            "Birthdate",
            "Sabhasadage",
            "VillageName",
            "Pincode",
            "City",
            "State",
            "Primary",
            "Secondary",
            "Totalland",
            "Irrigated",
            "Nonirrigated",
            "Nominal",
            "Age",
            "Nominesname",
        ];

        for (let field of requiredFields) {
            if (!masterData[field] || masterData[field].toString().trim() === "") {
                Swal.fire({
                    icon: "error",
                    title: "Validation Error",
                    text: `Please enter ${field}.`,
                    confirmButtonText: "OK",
                    allowOutsideClick: false,
                    allowEscapeKey: false
                });
                return;
            }
        }

        if (!masterData.photoes || masterData.photoes.length === 0) {
            Swal.fire({
                icon: "error",
                title: "Photo Validation Error",
                text: "Please capture or upload a photo.",
                confirmButtonText: "OK",
                allowOutsideClick: false,
                allowEscapeKey: false
            });
            return;
        }

        if (!masterData.photoes1 || masterData.photoes1.length === 0) {
            Swal.fire({
                icon: "error",
                title: "Photo Validation Error",
                text: "Please capture or upload a Form.",
                confirmButtonText: "OK",
                allowOutsideClick: false,
                allowEscapeKey: false
            });
            return;
        }
        showConfirmationAlert(e);
    };

    const showExitAlert = () => {
        MySwal.fire({
            title: "Are you sure?",
            text: "Do you want to exit?",
            showCancelButton: true,
            confirmButtonColor: "#00ff00",
            confirmButtonText: "Yes",
            cancelButtonColor: "#092C4C",
            cancelButtonText: "No",
            allowOutsideClick: false,
            allowEscapeKey: false,
        }).then((result) => {
            if (result.isConfirmed) {
                if (onRefresh) {
                    onRefresh();
                }

                const modal = document.getElementById("AddSabhsad");
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




                setMasterData({
                    Sabhasadname: "",
                    phonenumber: "",
                    Birthdate: "",
                    Sabhasadage: "",
                    VillageName: "",
                    Pincode: "",
                    City: "",
                    State: "",
                    Primary: "",
                    Secondary: "",
                    Totalland: "",
                    Irrigated: "",
                    Nonirrigated: "",
                    Nominal: "",
                    Age: "",
                    Nominesname: "",
                    photoes: [],
                    photoes1: [],
                });
                setTableData1([]);
                setTableData2([]);
                setTableData3([]);
                setTableData4([]);
                setTableData5([]);

            }
        });
    };
    const handleFileChange = async (e) => {
        const selectedFiles = Array.from(e.target.files);
        if (!selectedFiles.length) return;
        const newFileNames = [];
        const newPreviews = [];
        const existingCount = masterData.photoes.length;
        for (let i = 0; i < selectedFiles.length; i++) {
            const file = selectedFiles[i];
            const ext = file.name.split('.').pop();
            const uniqueFileName = `${GUID}_img${existingCount + i + 1}.${ext}`;
            await uploadIProfile(file, uniqueFileName);
            newFileNames.push(uniqueFileName);
            const previewUrl = URL.createObjectURL(file);
            const fileType = file.type.startsWith("image/") ? "image" : "pdf";
            newPreviews.push({
                name: uniqueFileName,
                url: previewUrl,
                type: fileType,
            });
        }
        setMasterData((prev) => ({
            ...prev,
            photoes: Array.isArray(prev.photoes)
                ? [...prev.photoes, ...newFileNames]
                : newFileNames,
        }));
    };
    const handleViewUploadedFiles = () => {
        if (!masterData.photoes || masterData.photoes.length === 0) return;
        const previews = masterData.photoes.map((fileName) => {
            const url = `${baseUrl.Url}/Images/${fileName}`;
            const extension = fileName.split('.').pop().toLowerCase();
            const type = extension === 'pdf' ? 'pdf' : 'image';
            return {
                name: fileName,
                url,
                type,
            };
        });
        setFilePreviews(previews);
        setPreviewModalOpen(true);
    };
    const uploadIProfile = async (fileData, uniqueFileName) => {
        const formDataToSend = new FormData();
        formDataToSend.append("Files", fileData);
        formDataToSend.append("FileNames", uniqueFileName);
        formDataToSend.append("FileSizeInBytes", fileData.size);
        formDataToSend.append("FilePath", `/Images/${uniqueFileName}`);
        formDataToSend.append("FileDescription", "Profile Image");
        try {
            const response = await axios.post(
                `${baseUrl.Url}/backend/api/ProfileUpload/Upload`,
                formDataToSend,
                {
                    headers: {
                        "Content-Type": "multipart/form-data",
                    },
                }
            );
            console.log("✅ Upload Success:", response.data);
            setUploadMessage("Photo uploaded successfully!");
            setMessageColor("green");
            setTimeout(() => {
                setUploadMessage("");
            }, 1000);
        } catch (error) {
            console.error("❌ Upload Error:", error.message);
            setUploadMessage("Photo uploaded successfully!");
            setMessageColor("green");
            setTimeout(() => {
                setUploadMessage("");
            }, 1000);
        }
    };

    const handleFileChange1 = async (e) => {
        const selectedFiles = Array.from(e.target.files);
        if (!selectedFiles.length) return;
        const newFileNames = [];
        const newPreviews = [];
        const existingCount = masterData.photoes1.length;
        for (let i = 0; i < selectedFiles.length; i++) {
            const file = selectedFiles[i];
            const ext = file.name.split('.').pop();
            const uniqueFileName = `${GUID}_img${existingCount + i + 1}.${ext}`;
            await uploadIProfile1(file, uniqueFileName);
            newFileNames.push(uniqueFileName);
            const previewUrl = URL.createObjectURL(file);
            const fileType = file.type.startsWith("image/") ? "image" : "pdf";
            newPreviews.push({
                name: uniqueFileName,
                url: previewUrl,
                type: fileType,
            });
        }
        setMasterData((prev) => ({
            ...prev,
            photoes1: Array.isArray(prev.photoes1)
                ? [...prev.photoes1, ...newFileNames]
                : newFileNames,
        }));
    };
    const handleViewUploadedFiles1 = () => {
        if (!masterData.photoes1 || masterData.photoes1.length === 0) return;
        const previews = masterData.photoes1.map((fileName) => {
            const url = `${baseUrl.Url}/Images/${fileName}`;
            const extension = fileName.split('.').pop().toLowerCase();
            const type = extension === 'pdf' ? 'pdf' : 'image';
            return {
                name: fileName,
                url,
                type,
            };
        });
        setFilePreviews1(previews);
        setPreviewModalOpen1(true);
    };
    const uploadIProfile1 = async (fileData, uniqueFileName) => {
        const formDataToSend = new FormData();
        formDataToSend.append("Files", fileData);
        formDataToSend.append("FileNames", uniqueFileName);
        formDataToSend.append("FileSizeInBytes", fileData.size);
        formDataToSend.append("FilePath", `/Images/${uniqueFileName}`);
        formDataToSend.append("FileDescription", "Profile Image");
        try {
            const response = await axios.post(
                `${baseUrl.Url}/backend/api/ProfileUpload/Upload`,
                formDataToSend,
                {
                    headers: {
                        "Content-Type": "multipart/form-data",
                    },
                }
            );
            console.log("✅ Upload Success:", response.data);
            setUploadMessage1("Photo uploaded successfully!");
            setMessageColor1("green");
            setTimeout(() => {
                setUploadMessage1("");
            }, 1000);
        } catch (error) {
            console.error("❌ Upload Error:", error.message);
            setUploadMessage1("Photo uploaded successfully!");
            setMessageColor1("green");
            setTimeout(() => {
                setUploadMessage1("");
            }, 1000);
        }
    };

    return (
        <div>
            <div className="modal fade" id="AddSabhsad">
                <div className="modal-dialog modal-dialog-centered modal-fullscreen mbgcolor">
                    <div className="modal-content mbgcolor">
                        <div className="page-wrapper-new p-0">
                            <div className="content">
                                <div className="modal-header border-0 custom-modal-header">
                                    <div className="page-title">
                                        <h4>Sabhasad Registration Form</h4>
                                    </div>
                                    <div
                                        className="page-btn"
                                        style={{
                                            display: 'flex',
                                            alignItems: 'center',
                                            gap: '10px'
                                        }}
                                    >
                                        <Link
                                            className="btn btn-secondary"
                                            onClick={showExitAlert}
                                        >
                                            <ArrowLeft className="me-2" />
                                            Back
                                        </Link>
                                    </div>
                                </div>
                                <div className="modal-body custom-modal-body" style={{
                                    overflow: "hidden",
                                }}>
                                    <form onSubmit={handleSubmit} className="mt-0">
                                        <label className="form-label fw-bold">Personal Information :</label>
                                        <div className="row">
                                            <div className="col-lg-6 col-sm-6 col-12">
                                                <div >
                                                    <label className="form-label required">Sabhasad Name</label>
                                                    <input
                                                        type="text"
                                                        className="form-control"
                                                        name="Sabhasadname"
                                                        value={masterData.Sabhasadname}
                                                        onChange={handleMasterInputChange}
                                                        pattern="^[A-Za-z][A-Za-z\s]*$"
                                                        title="Only letters and spaces are allowed"
                                                        required
                                                        ref={SabhasadnameRef}

                                                    />
                                                </div>
                                            </div>

                                            <div className="col-lg-2 col-sm-6 col-12">
                                                <div >
                                                    <label className="form-label required">Contact Number</label>
                                                    <input
                                                        type="text"
                                                        className="form-control"
                                                        name="phonenumber"
                                                        maxLength={10}
                                                        value={masterData.phonenumber}
                                                        onChange={handleMasterInputChange}
                                                        pattern="^[6-9]\d{9}$"
                                                        title="Enter a valid 10-digit Indian phone number"
                                                        required
                                                    />
                                                </div>
                                            </div>
                                            <div className="col-lg-2 col-sm-6 col-12">
                                                <div >
                                                    <label className="form-label ">Birthdate(optional)</label>
                                                    <input
                                                        type="date"
                                                        className="form-control"
                                                        name="Birthdate"
                                                        value={masterData.Birthdate}
                                                        onChange={handleMasterInputChange}

                                                    />
                                                </div>
                                            </div>

                                            <div className="col-lg-2 col-sm-6 col-12">
                                                <div >
                                                    <label className="form-label ">Age(optional)</label>
                                                    <input
                                                        type="text"
                                                        className="form-control"
                                                        name="Sabhasadage"
                                                        value={masterData.Sabhasadage}
                                                        onChange={handleMasterInputChange}
                                                        pattern="^\d{1,3}$"
                                                        title="Enter a valid age (number only)"



                                                    />
                                                </div>
                                            </div>
                                        </div>

                                        <label className="form-label fw-bold">Full Address :</label>

                                        <div className="row">
                                            <div className="col-lg-3 col-sm-6 col-12">
                                                <div >
                                                    <label className="form-label required">Village Name</label>
                                                    <input
                                                        type="text"
                                                        className="form-control"
                                                        name="VillageName"
                                                        value={masterData.VillageName}
                                                        onChange={handleMasterInputChange}
                                                        pattern="^[A-Za-z\s]+$"
                                                        title="Only letters and spaces are allowed"
                                                        required

                                                    />
                                                </div>
                                            </div>

                                            <div className="col-lg-3 col-sm-6 col-12">
                                                <div >
                                                    <label className="form-label required">Pincode</label>
                                                    <input
                                                        type="text"
                                                        className="form-control"
                                                        name="Pincode"
                                                        value={masterData.Pincode}
                                                        maxLength={6}
                                                        onChange={handleMasterInputChange}
                                                        pattern="^[1-9][0-9]{5}$"
                                                        title="Enter a valid 6-digit Indian pincode (should not start with 0)"
                                                        required

                                                    />
                                                </div>
                                            </div>
                                            <div className="col-lg-3 col-sm-6 col-12">
                                                <div >
                                                    <label className="form-label required">City</label>
                                                    <Select
                                                        classNamePrefix="react-select"
                                                        name="City"
                                                        required
                                                        options={sellingtype}
                                                        value={sellingtype.find((option) => option.value === masterData.City) || null}
                                                        // onChange={(selectedOption) => handleMasterInputChange(selectedOption, 'City')}
                                                        onChange={(selectedOption) => {
                                                            setMasterData(prevState => ({
                                                                ...prevState,
                                                                City: selectedOption ? selectedOption.value : '',
                                                            }));
                                                        }}
                                                    />
                                                </div>
                                            </div>

                                            <div className="col-lg-3 col-sm-6 col-12">
                                                <div >
                                                    <label className="form-label required">State</label>
                                                    <Select
                                                        classNamePrefix="react-select"
                                                        name="State"
                                                        required
                                                        options={statetype}
                                                        value={statetype.find((option) => option.value === masterData.State) || null}
                                                        // onChange={(selectedOption) => handleMasterInputChange(selectedOption, 'State')}
                                                        onChange={(selectedOption) => {
                                                            setMasterData(prevState => ({
                                                                ...prevState,
                                                                State: selectedOption ? selectedOption.value : '',
                                                            }));
                                                        }}
                                                    />
                                                </div>
                                            </div>
                                        </div>

                                        <label className="form-label fw-bold">Business :</label>

                                        <div className="row">
                                            <div className="col-lg-6 col-sm-6 col-12">
                                                <div >
                                                    <label className="form-label required">Primary Business</label>
                                                    <input
                                                        type="text"
                                                        className="form-control"
                                                        name="Primary"
                                                        value={masterData.Primary}
                                                        onChange={handleMasterInputChange}
                                                        pattern="^[A-Za-z\s]+$"
                                                        title="Only letters and spaces are allowed"
                                                        required
                                                    />
                                                </div>
                                            </div>

                                            <div className="col-lg-6 col-sm-6 col-12">
                                                <div >
                                                    <label className="form-label required">Secondary Business</label>
                                                    <input
                                                        type="text"
                                                        className="form-control"
                                                        name="Secondary"
                                                        value={masterData.Secondary}
                                                        onChange={handleMasterInputChange}
                                                        pattern="^[A-Za-z\s]+$"
                                                        title="Only letters and spaces are allowed"
                                                        required
                                                    />
                                                </div>
                                            </div>

                                        </div>

                                        <div className="row">
                                            <div className="col-lg-4 col-sm-6 col-12">
                                                <div >
                                                    <label className="form-label required">Total Land Area (in acres)</label>
                                                    <input
                                                        type="text"
                                                        className="form-control"
                                                        name="Totalland"
                                                        value={masterData.Totalland}
                                                        onChange={handleMasterInputChange}
                                                        pattern="^\d+(\.\d{1,2})?$"
                                                        title="Enter a valid number (e.g., 5 or 5.25)"
                                                        required
                                                    />
                                                </div>
                                            </div>

                                            <div className="col-lg-4 col-sm-6 col-12">
                                                <div >
                                                    <label className="form-label required">Irrigated Land</label>
                                                    <input
                                                        type="text"
                                                        className="form-control"
                                                        name="Irrigated"
                                                        value={masterData.Irrigated}
                                                        onChange={handleMasterInputChange}
                                                        pattern="^\d+(\.\d{1,2})?$"
                                                        title="Enter a valid number (e.g., 3 or 3.5)"
                                                        required
                                                    />
                                                </div>
                                            </div>
                                            <div className="col-lg-4 col-sm-6 col-12">
                                                <div >
                                                    <label className="form-label required">Non-Irrigated Land</label>
                                                    <input
                                                        type="text"
                                                        className="form-control"
                                                        name="Nonirrigated"
                                                        value={masterData.Nonirrigated}
                                                        onChange={handleMasterInputChange}
                                                        pattern="^\d+(\.\d{1,2})?$"
                                                        title="Enter a valid number (e.g., 2 or 2.75)"
                                                        required
                                                    />
                                                </div>
                                            </div>
                                        </div>

                                        <div className="row">

                                            <div className="col-lg-4 col-sm-6 col-12">
                                                <div >
                                                    <label className="form-label required">Nominee Name</label>
                                                    <input
                                                        type="text"
                                                        className="form-control"
                                                        name="Nominal"
                                                        value={masterData.Nominal}
                                                        onChange={handleMasterInputChange}
                                                        pattern="^[A-Za-z\s]+$"
                                                        title="Only letters and spaces are allowed"
                                                        required

                                                    />
                                                </div>
                                            </div>

                                            <div className="col-lg-4 col-sm-6 col-12">
                                                <div >
                                                    <label className="form-label required">Age</label>
                                                    <input
                                                        type="text"
                                                        className="form-control"
                                                        name="Age"
                                                        value={masterData.Age}
                                                        onChange={handleMasterInputChange}
                                                        pattern="^\d{1,3}$"
                                                        title="Enter a valid age (number only)"
                                                        required

                                                    />
                                                </div>
                                            </div>

                                            <div className="col-lg-4 col-sm-6 col-12">
                                                <div >
                                                    <label className="form-label required">Relation with Nominee</label>
                                                    <Select
                                                        classNamePrefix="react-select"
                                                        name="Nominesname"
                                                        options={Relations}
                                                        required
                                                        value={Relations.find((option) => option.value === masterData.Nominesname) || null}
                                                        // onChange={(selectedOption) => handleMasterInputChange(selectedOption.value, 'Nominesname')}
                                                        onChange={(selectedOption) => {
                                                            setMasterData(prevState => ({
                                                                ...prevState,
                                                                Nominesname: selectedOption ? selectedOption.value : '',
                                                            }));
                                                        }}
                                                    />


                                                </div>
                                            </div>


                                        </div>

                                        <label className="pt-2 fw-bold">Family Information :</label>
                                        <div className="border p-3 rounded shadow-sm mt-2 mb-2">
                                            <div className="col-lg-12">
                                                <div className="modal-body-table responsive-no-scroll" >
                                                    <div className="table-responsive">
                                                        <table className="table table-bordered table-striped mb-0">
                                                            <thead className="thead-dark bg-white" >
                                                                <tr>
                                                                    <th style={{ width: "5%" }}>Sr. No.</th>
                                                                    <th style={{ width: "35%" }}>Name</th>
                                                                    <th style={{ width: "15%" }}>Relation</th>
                                                                    <th style={{ width: "5%" }}>Age</th>
                                                                    <th style={{ width: "15%" }}>Education</th>
                                                                    <th style={{ width: "15%" }}>Business</th>
                                                                    <th style={{ width: "5%" }}>Action</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                {tableData1.filter((data) => !data.isdeleted).length > 0 ? (
                                                                    tableData1
                                                                        .filter((data) => !data.isdeleted)
                                                                        .map((data, index) => (
                                                                            <tr key={data.Dsfaid || index}>
                                                                                <td >{index + 1}</td>
                                                                                <td >{data.Name}</td>
                                                                                {/* <td >{data.relationname}</td> */}
                                                                                <td >{Relations.find(option => option.value === data.Relation)?.label || "Not Found"}</td>
                                                                                <td >{data.Age}</td>
                                                                                <td >{data.Education}</td>
                                                                                {/* <td >{data.businessname}</td> */}
                                                                                <td >{Bussiness.find(option => option.value === data.Business)?.label || "Not Found"}</td>
                                                                                <td>
                                                                                    <Link
                                                                                        to="#"
                                                                                        onClick={(e) => handleEdit1(e, data.Dsfaid)}
                                                                                        className="me-2 p-1"
                                                                                        style={{ color: 'lightblue' }}
                                                                                    >
                                                                                        <Edit className="feather-edit" />
                                                                                    </Link>
                                                                                    <Link
                                                                                        className="confirm-text p-2"
                                                                                        to="#"
                                                                                        onClick={(e) => handleDelete1(e, data.Dsfaid)}
                                                                                    >
                                                                                        <Trash2 className="feather-trash-2 text-danger" />
                                                                                    </Link>
                                                                                </td>
                                                                            </tr>
                                                                        ))
                                                                ) : (
                                                                    <tr>
                                                                        <td colSpan="8" className="text-center">No Record Awailable</td>
                                                                    </tr>
                                                                )
                                                                }
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                            <div className="addservice-info mb-2" >
                                                <div className="row">
                                                    <div className="col-lg-4 col-sm-6 col-12">
                                                        <label className="form-label required">Name</label>
                                                        <input
                                                            type="text"
                                                            className="form-control"
                                                            name="Name"
                                                            value={formData.Name}
                                                            onChange={handleDetailInputChange}
                                                            pattern="^[A-Za-z\s]+$"
                                                            ref={NameRef}
                                                            required

                                                        />
                                                    </div>
                                                    <div className="col-6 col-md-4 col-lg-2 mb-3">
                                                        <div >
                                                            <label className="form-label required">Relation</label>
                                                            <Select
                                                                placeholder="Select"
                                                                classNamePrefix="react-select"
                                                                options={Relations}
                                                                name="Relation"
                                                                ref={RelationRef}
                                                                value={Relations.find(option => option.value === formData.Relation) || null}
                                                                onChange={(selectedOption) => {
                                                                    setFormData(prevState => ({
                                                                        ...prevState,
                                                                        Relation: selectedOption ? selectedOption.value : '',
                                                                    }));
                                                                }}
                                                                required
                                                            />
                                                        </div>
                                                    </div>
                                                    <div className="col-6 col-md-4 col-lg-1 mb-3">
                                                        <div >
                                                            <label className="form-label required">Age</label>
                                                            <input
                                                                type="text"
                                                                className="form-control"
                                                                name="Age"
                                                                value={formData.Age}
                                                                onChange={handleDetailInputChange}
                                                                pattern="^\d{1,3}$"
                                                                ref={AgeRef}
                                                                required

                                                            />
                                                        </div>
                                                    </div>
                                                    <div className="col-6 col-md-4 col-lg-2 mb-3">
                                                        <div >
                                                            <label className="form-label required">Education </label>
                                                            <input
                                                                type="text"
                                                                className="form-control"
                                                                name="Education"
                                                                value={formData.Education}
                                                                onChange={handleDetailInputChange}
                                                                pattern="^[A-Za-z\s]+$"
                                                                ref={EducationRef}
                                                                required

                                                            />
                                                        </div>
                                                    </div>
                                                    <div className="col-6 col-md-4 col-lg-2 mb-3">
                                                        <div >
                                                            <label className="form-label required">Business</label>
                                                            <Select
                                                                placeholder="Select"
                                                                classNamePrefix="react-select"
                                                                options={Bussiness}
                                                                name="Business"
                                                                value={Bussiness.find(option => option.value === formData.Business) || null}
                                                                onChange={(selectedOption) => {
                                                                    setFormData(prevState => ({
                                                                        ...prevState,
                                                                        Business: selectedOption ? selectedOption.value : '',
                                                                    }));
                                                                }}
                                                                pattern="^[A-Za-z\s]*$"
                                                                ref={BusinessRef}
                                                                required

                                                            />
                                                        </div>
                                                    </div>
                                                    <div className="col-lg-1 col-sm-6 col-12 ">
                                                        <div className="mt-4">
                                                            <button
                                                                type="button"
                                                                className="btn btn-primary w-100"
                                                                onClick={FarmilyInformation}
                                                            >
                                                                Add
                                                            </button>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>

                                        <label className="fw-bold">Agricultural Information :</label>
                                        <div className="border p-3 rounded shadow-sm mt-2 mb-2">
                                            <div className="col-lg-12">
                                                <div className="modal-body-table responsive-no-scroll" >
                                                    <div className="table-responsive">
                                                        <table className="table table-bordered table-striped mb-0">
                                                            <thead className="thead-dark bg-white" >
                                                                <tr>
                                                                    <th style={{ width: "10%" }}>Sr. No.</th>
                                                                    <th style={{ width: "20%" }}>Survey Number</th>
                                                                    <th style={{ width: "20%" }}>Cluster Number</th>
                                                                    <th style={{ width: "20%" }}>land area</th>
                                                                    <th style={{ width: "20%" }}>Irrigated / Non-Irrigated Land</th>
                                                                    <th style={{ width: "10%" }}>Action</th>

                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                {tableData2.filter((data) => !data.isdeleted).length > 0 ? (
                                                                    tableData2
                                                                        .filter((data) => !data.isdeleted)
                                                                        .map((data, index) => (
                                                                            <tr key={data.Dsaid || index}>
                                                                                <td >{index + 1}</td>
                                                                                <td >{data.Sarve}</td>
                                                                                <td >{data.Plot}</td>
                                                                                <td >{data.Landarea}</td>
                                                                                <td >{LandType.find(option => option.value === data.Irrigatedtype)?.label || "Not Found"}</td>
                                                                                {/* <td >{data.landname || data.Irrigatedtype}</td> */}
                                                                                <td>
                                                                                    <Link
                                                                                        to="#"
                                                                                        onClick={(e) => handleEdit2(e, data.Dsaid)}
                                                                                        className="me-2 p-1"
                                                                                        style={{ color: 'lightblue' }}
                                                                                    >
                                                                                        <Edit className="feather-edit" />
                                                                                    </Link>
                                                                                    <Link
                                                                                        className="confirm-text p-2"
                                                                                        to="#"
                                                                                        onClick={(e) => handleDelete2(e, data.Dsaid)}
                                                                                    >
                                                                                        <Trash2 className="feather-trash-2 text-danger" />
                                                                                    </Link>
                                                                                </td>
                                                                            </tr>
                                                                        ))
                                                                ) : (
                                                                    <tr>
                                                                        <td colSpan="8" className="text-center">No Record Awailable</td>
                                                                    </tr>
                                                                )
                                                                }
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                            <div className="addservice-info mb-2" >
                                                <div className="row">
                                                    <div className="col-lg-3 col-sm-4 col-12">
                                                        <label className="form-label required">Sarve Number</label>
                                                        <input
                                                            type="text"
                                                            className="form-control"
                                                            name="Sarve"
                                                            value={formData.Sarve}
                                                            onChange={handleDetailInputChange}
                                                            pattern="^[A-Za-z0-9\-\/]+$"
                                                            ref={SarveRef}
                                                            required

                                                        />
                                                    </div>
                                                    <div className="col-6 col-md-4 col-lg-2 mb-3">
                                                        <div >
                                                            <label className="form-label required">Plot Number</label>
                                                            <input
                                                                type="text"
                                                                className="form-control"
                                                                name="Plot"
                                                                value={formData.Plot}
                                                                onChange={handleDetailInputChange}
                                                                pattern="^[A-Za-z0-9\-\/]+$"
                                                                ref={PlotRef}
                                                                required
                                                            />
                                                        </div>
                                                    </div>
                                                    <div className="col-6 col-md-4 col-lg-3 mb-3">
                                                        <div >
                                                            <label className="form-label required">land area</label>
                                                            <input
                                                                type="text"
                                                                className="form-control"
                                                                name="Landarea"
                                                                value={formData.Landarea}
                                                                onChange={handleDetailInputChange}
                                                                pattern="^\d+(\.\d{1,2})?$"
                                                                ref={LandareaRef}
                                                                required

                                                            />
                                                        </div>
                                                    </div>

                                                    <div className="col-6 col-md-4 col-lg-3 mb-3">
                                                        <div >
                                                            <label className="form-label required">Irrigated / Non-Irrigated Land</label>
                                                            <Select
                                                                placeholder="Select"
                                                                classNamePrefix="react-select"
                                                                options={LandType}
                                                                name="Irrigatedtype"
                                                                value={LandType.find(option => option.value === formData.Irrigatedtype) || null}
                                                                onChange={(selectedOption) => {
                                                                    setFormData(prevState => ({
                                                                        ...prevState,
                                                                        Irrigatedtype: selectedOption ? selectedOption.value : '',
                                                                    }));
                                                                }}
                                                                ref={IrrigatedtypeRef}
                                                                required

                                                            />
                                                        </div>
                                                    </div>
                                                    <div className="col-lg-1 col-sm-6 col-12 ">
                                                        <div className="mt-4">
                                                            <button
                                                                type="button"
                                                                className="btn btn-primary w-100"
                                                                onClick={Agriculturalinformation}
                                                            >
                                                                Add
                                                            </button>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>


                                        <label className="fw-bold">Which crops are grown throughout the year? :</label>
                                        <div className="border p-3 rounded shadow-sm mt-2 mb-2">
                                            <div className="col-lg-12">
                                                <div className="modal-body-table responsive-no-scroll" >
                                                    <div className="table-responsive">
                                                        <table className="table table-bordered table-striped mb-0">
                                                            <thead className="thead-dark bg-white" >
                                                                <tr>
                                                                    <th style={{ width: "14%" }}>Crop Name</th>
                                                                    <th style={{ width: "14%" }}>Variety of Crop</th>
                                                                    <th style={{ width: "10%" }}>land area</th>
                                                                    <th style={{ width: "14%" }}>Growing Period</th>
                                                                    <th style={{ width: "14%" }}>Harvesting Period</th>
                                                                    <th style={{ width: "14%" }}>Expected Yield</th>
                                                                    <th style={{ width: "14%" }}>Type</th>
                                                                    <th style={{ width: "5%" }}>Action</th>

                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                {tableData3.filter((data) => !data.isdeleted).length > 0 ? (
                                                                    tableData3
                                                                        .filter((data) => !data.isdeleted)
                                                                        .map((data, index) => (
                                                                            <tr key={data.Dscaid || index}>
                                                                                {/* <td >{data.cname}</td> */}
                                                                                <td >{cropType.find(option => option.value === data.Cropname)?.label || "Not Found"}</td>
                                                                                <td >{data.cropvariety}</td>
                                                                                <td >{data.croparea}</td>
                                                                                <td >{data.Growing}</td>
                                                                                <td >{data.Harvesting}</td>
                                                                                <td >{data.Expected}</td>
                                                                                {/* <td >{data.Type}</td> */}
                                                                                <td >{Crop.find(option => option.value === data.Type)?.label || "Not Found"}</td>
                                                                                <td>
                                                                                    <Link
                                                                                        to="#"
                                                                                        onClick={(e) => handleEdit3(e, data.Dscaid)}
                                                                                        className="me-2 p-1"
                                                                                        style={{ color: 'lightblue' }}
                                                                                    >
                                                                                        <Edit className="feather-edit" />
                                                                                    </Link>
                                                                                    <Link
                                                                                        className="confirm-text p-2"
                                                                                        to="#"
                                                                                        onClick={(e) => handleDelete3(e, data.Dscaid)}
                                                                                    >
                                                                                        <Trash2 className="feather-trash-2 text-danger" />
                                                                                    </Link>
                                                                                </td>
                                                                            </tr>
                                                                        ))
                                                                ) : (
                                                                    <tr>
                                                                        <td colSpan="8" className="text-center">No Record Awailable</td>
                                                                    </tr>
                                                                )
                                                                }
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                            <div className="addservice-info mb-2" >
                                                <div className="row">
                                                    <div className="col-lg-2 col-sm-4 col-12">
                                                        <label className="form-label">Crop Name</label>
                                                        <Select
                                                            placeholder="Select"
                                                            classNamePrefix="react-select"
                                                            options={cropType}
                                                            name="Cropname"
                                                            openMenuOnFocus={true}

                                                            ref={CropnameRef}
                                                            value={cropType.find(option => option.value === formData.Cropname) || null}
                                                            onChange={(selectedOption) => {
                                                                setFormData(prevState => ({
                                                                    ...prevState,
                                                                    Cropname: selectedOption ? selectedOption.value : '',
                                                                }));
                                                            }}
                                                        />
                                                    </div>
                                                    <div className="col-6 col-md-4 col-lg-1 mb-3">
                                                        <div >
                                                            <label className="form-label ">Crop Variety</label>
                                                            <input
                                                                type="text"
                                                                className="form-control"
                                                                name="cropvariety"

                                                                value={formData.cropvariety}
                                                                onChange={handleDetailInputChange}
                                                                ref={cropvarietyRef}
                                                            />
                                                        </div>
                                                    </div>
                                                    <div className="col-6 col-md-4 col-lg-1 mb-3">
                                                        <div >
                                                            <label className="form-label ">land area</label>
                                                            <input
                                                                type="text"
                                                                className="form-control"
                                                                name="croparea"

                                                                value={formData.croparea}
                                                                onChange={handleDetailInputChange}
                                                                ref={cropareaRef}
                                                            />
                                                        </div>
                                                    </div>

                                                    <div className="col-6 col-md-4 col-lg-2 mb-3">
                                                        <div >
                                                            <label className="form-label ">Growing Period</label>
                                                            <input
                                                                type="text"
                                                                className="form-control"
                                                                name="Growing"

                                                                value={formData.Growing}
                                                                onChange={handleDetailInputChange}
                                                                ref={GrowingRef}

                                                            />
                                                        </div>
                                                    </div>

                                                    <div className="col-6 col-md-4 col-lg-2 mb-3">
                                                        <div >
                                                            <label className="form-label ">Harvesting Period</label>
                                                            <input
                                                                type="text"
                                                                className="form-control"
                                                                name="Harvesting"

                                                                value={formData.Harvesting}
                                                                onChange={handleDetailInputChange}
                                                                ref={HarvestingRef}
                                                            />
                                                        </div>
                                                    </div>

                                                    <div className="col-6 col-md-4 col-lg-1 mb-3">
                                                        <div >
                                                            <label className="form-label ">Expe. Yield</label>
                                                            <input
                                                                type="text"
                                                                className="form-control"
                                                                name="Expected"

                                                                value={formData.Expected}
                                                                onChange={handleDetailInputChange}
                                                                ref={ExpectedRef}

                                                            />
                                                        </div>
                                                    </div>

                                                    <div className="col-6 col-md-4 col-lg-2 mb-3">
                                                        <div >
                                                            <label className="form-label ">Type</label>
                                                            <Select
                                                                placeholder="Select"
                                                                classNamePrefix="react-select"
                                                                options={Crop}
                                                                name="Type"
                                                                openMenuOnFocus={true}
                                                                ref={TypeRef}

                                                                value={Crop.find(option => option.value === formData.Type) || null}
                                                                onChange={(selectedOption) => {
                                                                    setFormData(prevState => ({
                                                                        ...prevState,
                                                                        Type: selectedOption ? selectedOption.value : '',
                                                                    }));
                                                                }}
                                                            />
                                                        </div>
                                                    </div>
                                                    <div className="col-lg-1 col-sm-6 col-12 ">
                                                        <div className="mt-4">
                                                            <button
                                                                type="button"
                                                                className="btn btn-primary w-100"
                                                                onClick={YearOfCrop}
                                                            >
                                                                Add
                                                            </button>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>

                                        <label className="fw-bold">Which fertilizers are used throughout the year? :</label>
                                        <div className="border p-3 rounded shadow-sm mt-2 mb-2">
                                            <div className="col-lg-12">
                                                <div className="modal-body-table responsive-no-scroll" >
                                                    <div className="table-responsive">
                                                        <table className="table table-bordered table-striped mb-0">
                                                            <thead className="thead-dark bg-white" >
                                                                <tr>
                                                                    <th style={{ width: "25%" }}>Fertilizers Name</th>
                                                                    <th style={{ width: "20%" }}>Quantity </th>
                                                                    <th style={{ width: "20%" }}>Cost </th>
                                                                    <th style={{ width: "25%" }}>Type</th>
                                                                    <th style={{ width: "10%" }}>Action</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                {tableData4.filter((data) => !data.isdeleted).length > 0 ? (
                                                                    tableData4
                                                                        .filter((data) => !data.isdeleted)
                                                                        .map((data, index) => (
                                                                            <tr key={data.Dsfid || index}>
                                                                                <td >{data.Fertilizers}</td>
                                                                                <td >{data.Quantity}</td>
                                                                                <td >{data.Coast}</td>
                                                                                {/* <td >{data.fname}</td> */}
                                                                                <td >{Fertilizer.find(option => option.value === data.Fertilizertype)?.label || "Not Found"}</td>
                                                                                <td>
                                                                                    <Link
                                                                                        to="#"
                                                                                        onClick={(e) => handleEdit4(e, data.Dsfid)}
                                                                                        className="me-2 p-1"
                                                                                        style={{ color: 'lightblue' }}
                                                                                    >
                                                                                        <Edit className="feather-edit" />
                                                                                    </Link>
                                                                                    <Link
                                                                                        className="confirm-text p-2"
                                                                                        to="#"
                                                                                        onClick={(e) => handleDelete4(e, data.Dsfid)}
                                                                                    >
                                                                                        <Trash2 className="feather-trash-2 text-danger" />
                                                                                    </Link>
                                                                                </td>
                                                                            </tr>
                                                                        ))
                                                                ) : (
                                                                    <tr>
                                                                        <td colSpan="8" className="text-center">No Record Awailable</td>
                                                                    </tr>
                                                                )
                                                                }
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                            <div className="addservice-info mb-2" >
                                                <div className="row">
                                                    <div className="col-lg-3 col-sm-4 col-12">
                                                        <label className="form-label ">Fertilizers Name </label>
                                                        <input
                                                            type="text"
                                                            className="form-control"
                                                            name="Fertilizers"
                                                            value={formData.Fertilizers}
                                                            onChange={handleDetailInputChange}
                                                            ref={FertilizersRef}

                                                        />
                                                    </div>
                                                    <div className="col-6 col-md-4 col-lg-2 mb-3">
                                                        <div >
                                                            <label className="form-label ">Quantity </label>
                                                            <input
                                                                type="text"
                                                                className="form-control"
                                                                name="Quantity"
                                                                value={formData.Quantity}
                                                                onChange={handleDetailInputChange}
                                                                ref={QuantityRef}

                                                            />
                                                        </div>
                                                    </div>
                                                    <div className="col-6 col-md-4 col-lg-3 mb-3">
                                                        <div >
                                                            <label className="form-label ">Coast</label>
                                                            <input
                                                                type="text"
                                                                className="form-control"
                                                                name="Coast"
                                                                ref={CoastRef}
                                                                value={formData.Coast}
                                                                onChange={handleDetailInputChange}

                                                            />
                                                        </div>
                                                    </div>

                                                    <div className="col-6 col-md-4 col-lg-3 mb-3">
                                                        <div >
                                                            <label className="form-label ">Type</label>
                                                            <Select
                                                                classNamePrefix="react-select"
                                                                options={Fertilizer}
                                                                name="Fertilizertype"
                                                                ref={FertilizertypeRef}

                                                                value={Fertilizer.find(option => option.value === formData.Fertilizertype) || null}
                                                                onChange={(selectedOption) => {
                                                                    setFormData(prevState => ({
                                                                        ...prevState,
                                                                        Fertilizertype: selectedOption ? selectedOption.value : '',
                                                                    }));
                                                                }}
                                                            />

                                                        </div>
                                                    </div>




                                                    <div className="col-lg-1 col-sm-6 col-12 ">
                                                        <div className="mt-4">
                                                            <button
                                                                type="button"
                                                                className="btn btn-primary w-100"
                                                                onClick={addFertilizer}
                                                            >
                                                                Add
                                                            </button>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>

                                        <label className="fw-bold">Which chemical/ organic fertilizers, insecticides, herbicides, and fungicides are used throughout the year?:</label>
                                        <div className="border p-3 rounded shadow-sm mt-2 mb-2">
                                            <div className="col-lg-12">
                                                <div className="modal-body-table responsive-no-scroll" >
                                                    <div className="table-responsive">
                                                        <table className="table table-bordered table-striped mb-0">
                                                            <thead className="thead-dark bg-white" >
                                                                <tr>
                                                                    <th style={{ width: "8%" }}>Sr. No.</th>
                                                                    <th style={{ width: "25%" }}>Name </th>
                                                                    <th style={{ width: "25%" }}>Type </th>
                                                                    <th style={{ width: "17%" }}>Quantity</th>
                                                                    <th style={{ width: "17%" }}>Coast</th>
                                                                    <th style={{ width: "8%" }}>Action</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                {tableData5.filter((data) => !data.isdeleted).length > 0 ? (
                                                                    tableData5
                                                                        .filter((data) => !data.isdeleted)
                                                                        .map((data, index) => (
                                                                            <tr key={data.Dscid || index}>
                                                                                <td >{index + 1}</td>
                                                                                <td >{data.chemical}</td>
                                                                                {/* <td >{data.chemicalname}</td> */}
                                                                                <td >{ChemicalType.find(option => option.value === data.Chemicaltype)?.label || "Not Found"}</td>
                                                                                <td >{data.Chemicalquanitity}</td>
                                                                                <td >{data.Chemicalcoast}</td>
                                                                                <td>
                                                                                    <Link
                                                                                        to="#"
                                                                                        onClick={(e) => handleEdit5(e, data.Dscid)}
                                                                                        className="me-2 p-1"
                                                                                        style={{ color: 'lightblue' }}
                                                                                    >
                                                                                        <Edit className="feather-edit" />
                                                                                    </Link>
                                                                                    <Link
                                                                                        className="confirm-text p-2"
                                                                                        to="#"
                                                                                        onClick={(e) => handleDelete5(e, data.Dscid)}
                                                                                    >
                                                                                        <Trash2 className="feather-trash-2 text-danger" />
                                                                                    </Link>
                                                                                </td>

                                                                            </tr>
                                                                        ))
                                                                ) : (
                                                                    <tr>
                                                                        <td colSpan="8" className="text-center">No Record Awailable</td>
                                                                    </tr>
                                                                )
                                                                }
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                            <div className="addservice-info mb-2" >
                                                <div className="row">
                                                    <div className="col-lg-3 col-sm-4 col-12">
                                                        <label className="form-label ">Name</label>
                                                        <input
                                                            type="text"
                                                            className="form-control"
                                                            name="chemical"
                                                            value={formData.chemical}
                                                            onChange={handleDetailInputChange}
                                                            ref={chemicalRef}

                                                        />
                                                    </div>
                                                    <div className="col-6 col-md-4 col-lg-3 mb-3">
                                                        <div >
                                                            <label className="form-label ">Type</label>
                                                            <Select
                                                                classNamePrefix="react-select"
                                                                options={ChemicalType}
                                                                name="Chemicaltype"
                                                                value={ChemicalType.find(option => option.value === formData.Chemicaltype) || null}
                                                                onChange={(selectedOption) => {
                                                                    setFormData(prevState => ({
                                                                        ...prevState,
                                                                        Chemicaltype: selectedOption ? selectedOption.value : '',
                                                                    }));
                                                                }}
                                                                ref={ChemicaltypeRef}

                                                            />
                                                        </div>
                                                    </div>
                                                    <div className="col-6 col-md-4 col-lg-2 mb-3">
                                                        <div >
                                                            <label className="form-label ">Quantity</label>
                                                            <input
                                                                type="text"
                                                                className="form-control"
                                                                name="Chemicalquanitity"
                                                                value={formData.Chemicalquanitity}
                                                                onChange={handleDetailInputChange}
                                                                ref={ChemicalquanitityRef}

                                                            />
                                                        </div>
                                                    </div>

                                                    <div className="col-6 col-md-4 col-lg-3 mb-3">
                                                        <div >
                                                            <label className="form-label ">Coast</label>
                                                            <input
                                                                type="text"
                                                                className="form-control"
                                                                name="Chemicalcoast"
                                                                value={formData.Chemicalcoast}
                                                                onChange={handleDetailInputChange}
                                                                ref={ChemicalcoastRef}

                                                            />
                                                        </div>
                                                    </div>


                                                    <div className="col-lg-1 col-sm-6 col-12 ">
                                                        <div className="mt-4">
                                                            <button
                                                                type="button"
                                                                className="btn btn-primary w-100"
                                                                onClick={AddChemical}
                                                            >
                                                                Add
                                                            </button>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>



                                        <div className="row mb-3">
                                            <div className="col-lg-6 col-sm-6 col-12">
                                                <div>
                                                    <div className="d-flex flex-wrap align-items-center">
                                                        <label className="form-label required">Upload Photo </label>
                                                        <div className="mt-2 d-flex flex-wrap gap-2">
                                                            {masterData.photoes.length > 0 &&
                                                                masterData.photoes.map((file, index) => (
                                                                    <div
                                                                        key={index}
                                                                        className="px-3 py-1 rounded-pill bg-success text-black d-flex align-items-center shadow-sm"
                                                                        style={{
                                                                            animation: "fadeIn 0.4s ease-in-out",
                                                                            gap: "6px",
                                                                            cursor: "pointer",
                                                                        }}
                                                                        onClick={handleViewUploadedFiles} // ⬅ Trigger preview modal
                                                                        title="Click to preview"
                                                                    >
                                                                        <i className="fas fa-check-circle"></i>
                                                                        {file.split('_').pop()}
                                                                    </div>
                                                                ))}
                                                        </div>
                                                    </div>
                                                    <div className="position-relative">
                                                        <input
                                                            type="file"
                                                            accept="image/*,application/pdf"
                                                            capture="environment"
                                                            multiple
                                                            className="form-control pe-5"
                                                            id="photoes"
                                                            name="photoes"
                                                            required
                                                            onChange={handleFileChange}
                                                        // ref={fileInputRef}
                                                        />
                                                    </div>
                                                    {uploadMessage && (
                                                        <div className="mt-2" style={{ color: messageColor }}>
                                                            {uploadMessage}
                                                        </div>
                                                    )}
                                                    {previewModalOpen && filePreviews.length > 0 && (
                                                        <div
                                                            className="modal fade show"
                                                            tabIndex="-1"
                                                            style={{
                                                                display: 'block',
                                                                backdropFilter: 'blur(5px)',
                                                                backgroundColor: 'rgba(0,0,0,0.5)',
                                                            }}
                                                        >
                                                            <div className="modal-dialog modal-dialog-centered modal-lg">
                                                                <div className="modal-content shadow-lg rounded-3">
                                                                    <div className="modal-header bg-primary text-white border-0">
                                                                        <h5 className="modal-title">Uploaded File Preview</h5>
                                                                        <button
                                                                            type="button"
                                                                            className="btn-close text-white"
                                                                            onClick={() => setPreviewModalOpen(false)}
                                                                        ></button>
                                                                    </div>
                                                                    <div className="modal-body p-4">
                                                                        {filePreviews.map((preview, index) => (
                                                                            <div key={index} className="mb-4">
                                                                                <h6>{preview.name.split('_').pop()}</h6>
                                                                                {preview.type === "image" ? (
                                                                                    <img
                                                                                        src={preview.url}
                                                                                        alt={`Preview ${index}`}
                                                                                        className="img-fluid rounded-3 shadow-sm"
                                                                                    />
                                                                                ) : (
                                                                                    <iframe
                                                                                        src={preview.url}
                                                                                        title={`PDF Preview ${index}`}
                                                                                        width="100%"
                                                                                        height="500px"
                                                                                        style={{ border: 'none' }}
                                                                                    />
                                                                                )}
                                                                            </div>
                                                                        ))}
                                                                    </div>
                                                                    <div className="modal-footer border-0 bg-light">
                                                                        <button
                                                                            type="button"
                                                                            className="btn btn-secondary"
                                                                            onClick={() => setPreviewModalOpen(false)}
                                                                        >
                                                                            Close
                                                                        </button>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    )}
                                                </div>
                                            </div>

                                            <div className="col-lg-6 col-sm-6 col-12">
                                                <div>
                                                    <div className="d-flex flex-wrap align-items-center">
                                                        <label className="form-label required">Upload Form </label>
                                                        <div className="mt-2 d-flex flex-wrap gap-2">
                                                            {masterData.photoes1.length > 0 &&
                                                                masterData.photoes1.map((file, index) => (
                                                                    <div
                                                                        key={index}
                                                                        className="px-3 py-1 rounded-pill bg-success text-black d-flex align-items-center shadow-sm"
                                                                        style={{
                                                                            animation: "fadeIn 0.4s ease-in-out",
                                                                            gap: "6px",
                                                                            cursor: "pointer",
                                                                        }}
                                                                        onClick={handleViewUploadedFiles1} // ⬅ Trigger preview modal
                                                                        title="Click to preview"
                                                                    >
                                                                        <i className="fas fa-check-circle"></i>
                                                                        {file.split('_').pop()}
                                                                    </div>
                                                                ))}
                                                        </div>
                                                    </div>
                                                    <div className="position-relative">
                                                        <input
                                                            type="file"
                                                            accept="image/*,application/pdf"
                                                            capture="environment"
                                                            multiple
                                                            className="form-control pe-5"
                                                            id="photoes1"
                                                            name="photoes1"
                                                            required
                                                            onChange={handleFileChange1}
                                                        // ref={fileInputRef}
                                                        />
                                                    </div>
                                                    {uploadMessage1 && (
                                                        <div className="mt-2" style={{ color: messageColor1 }}>
                                                            {uploadMessage1}
                                                        </div>
                                                    )}
                                                    {previewModalOpen1 && filePreviews1.length > 0 && (
                                                        <div
                                                            className="modal fade show"
                                                            tabIndex="-1"
                                                            style={{
                                                                display: 'block',
                                                                backdropFilter: 'blur(5px)',
                                                                backgroundColor: 'rgba(0,0,0,0.5)',
                                                            }}
                                                        >
                                                            <div className="modal-dialog modal-dialog-centered modal-lg">
                                                                <div className="modal-content shadow-lg rounded-3">
                                                                    <div className="modal-header bg-primary text-white border-0">
                                                                        <h5 className="modal-title">Uploaded File Preview</h5>
                                                                        <button
                                                                            type="button"
                                                                            className="btn-close text-white"
                                                                            onClick={() => setPreviewModalOpen1(false)}
                                                                        ></button>
                                                                    </div>
                                                                    <div className="modal-body p-4">
                                                                        {filePreviews1.map((preview, index) => (
                                                                            <div key={index} className="mb-4">
                                                                                <h6>{preview.name.split('_').pop()}</h6>
                                                                                {preview.type === "image" ? (
                                                                                    <img
                                                                                        src={preview.url}
                                                                                        alt={`Preview ${index}`}
                                                                                        className="img-fluid rounded-3 shadow-sm"
                                                                                    />
                                                                                ) : (
                                                                                    <iframe
                                                                                        src={preview.url}
                                                                                        title={`PDF Preview ${index}`}
                                                                                        width="100%"
                                                                                        height="500px"
                                                                                        style={{ border: 'none' }}
                                                                                    />
                                                                                )}
                                                                            </div>
                                                                        ))}
                                                                    </div>
                                                                    <div className="modal-footer border-0 bg-light">
                                                                        <button
                                                                            type="button"
                                                                            className="btn btn-secondary"
                                                                            onClick={() => setPreviewModalOpen1(false)}
                                                                        >
                                                                            Close
                                                                        </button>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    )}
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
                                    </form>
                                </div>
                            </div>
                        </div>
                    </div>
                </div >
            </div >
        </div >
    );
};

export default AddMember;
