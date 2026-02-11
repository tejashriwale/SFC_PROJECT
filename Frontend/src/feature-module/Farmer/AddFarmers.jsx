import React, { useState, useEffect, useRef } from 'react';
import { Link } from "react-router-dom";
import { ChevronUp, ArrowLeft } from "feather-icons-react/build/IconComponents";
import { OverlayTrigger, Tooltip } from "react-bootstrap";
import { all_routes } from "../../Router/all_routes";
import {
    ChevronDown,
    Trash2,
    Edit,
    Info,
    LifeBuoy,
    List,
} from 'feather-icons-react/build/IconComponents';
import { useNavigate } from "react-router-dom";
import { useLocation } from 'react-router-dom';
import axios from 'axios';
import { ACSPLGUID, baseUrl } from "../../core/json/custom";
import withReactContent from "sweetalert2-react-content";
import Swal from "sweetalert2";
import Select from "react-select";
import { convertToISODate, formatDate } from "../../core/json/custom";
import { getUserData } from '../../Context/UserData';
const AddFarmers = () => {
    const route = all_routes;
    const { userdetail } = getUserData();
    console.log(userdetail, "userdetailuserdetail")
    const isRestricted = userdetail.LANGUAGE === "en";
    const location = useLocation();
    const navigate = useNavigate();
    const { FAID, ID } = location.state || {};
    const GUID = ACSPLGUID.getNew()
    const renderCollapseTooltip = (props) => (
        <Tooltip id="refresh-tooltip" {...props}>
            Collapse
        </Tooltip>
    );
    const [status, setstatus] = useState([]);
    const [relation, setrelation] = useState([]);
    const [error, setError] = useState('');
    const [formData, setFormData] = useState({
        FAID: '',
        FNAME: '',
        FCONTACTNO: '',
        FBIRTHDATE: '',
        FPANNO: '',
        FADDHARNO: '',
        FBANKNAME: '',
        FACCONAME: '',
        FBRANCHNAME: '',
        FACCOUNTNO: '',
        FIFSCCODE: '',
        FPINCODE: '',
        FSTATE: '',
        FCITY: '',
        FAddress: '',
        FCROPAREA: '',
        FCROPTYPE: '',
        FSTATUS: '',
    });
    const [rows, setRows] = useState([]);
    const [detailData, setDetailData] = useState({
        fdaid: '',
        FAID: '',
        // maid: FAID,
        FDNAME: '',
        FDBIRTHDATE: '',
        FDAADHARNO: '',
        FDRELATION: '',
        FDCONTACT: '',
        isdeleted: 0,

    });
    const FNAMERef = useRef(null);
    const FCONTACTNORef = useRef(null);
    const FBIRTHDATERef = useRef(null);
    const FPANNORef = useRef(null);
    const FADDHARNORef = useRef(null);
    const FBANKNAMERef = useRef(null);
    const FACCONAMERef = useRef(null);
    const FBRANCHNAMERef = useRef(null);
    const FACCOUNTNORef = useRef(null);
    const FIFSCCODERef = useRef(null);
    const FPINCODERef = useRef(null);
    const FSTATERef = useRef(null);
    const FCITYRef = useRef(null);
    const FAddressRef = useRef(null);
    const saveRef = useRef(null);
    const FCROPAREARef = useRef(null);
    const FDAADHARNORef = useRef(null);
    const FDCONTACTRef = useRef(null);
    const FDRELATIONRef = useRef(null);
    const FDBIRTHDATERef = useRef(null);
    const STATUSRef = useRef(null);
    const submitRef = useRef();
    const validateinput = (e) => {
        const { FNAME, FCONTACTNO, FBIRTHDATE, FBANKNAME, FBRANCHNAME, FACCOUNTNO, FIFSCCODE } = formData;
        const nameRegex = /^(?!\s)([A-Za-z0-9\u0900-\u097F\s]{3,50})(?<!\s)$/;
        if (!FNAME || !nameRegex.test(FNAME)) {
            Swal.fire({
                icon: "error",
                title: isRestricted ? "Validation Error" : "वैधता त्रुटी",
                text: isRestricted
                    ? "Full name must be valid (3 to 50 characters). No leading or trailing spaces allowed (Marathi/English letters and spaces allowed)."
                    : "पूर्ण नाव वैध असावे. 3 ते 50 अक्षरे. सुरुवातीला व शेवटी रिकामी जागा नसावी (मराठी/इंग्रजी अक्षरे व जागा).",
                allowOutsideClick: false,
                allowEscapeKey: false,
            }).then(() => {
                FNAMERef.current.focus();
            });
            return;
        }
        if (!FCONTACTNO || !/^([0-9\u0966-\u096F]{10})$/.test(FCONTACTNO)) {
            FCONTACTNORef.current.focus();
            Swal.fire({
                icon: "error",
                title: isRestricted ? "Validation Error" : "वैधता त्रुटी",
                text: isRestricted
                    ? "Mobile number must be a valid 10-digit number (0-9)."
                    : "मोबाईल नंबर 10 अंकी असावा (०-९ किंवा 0-9).",
                allowOutsideClick: false,
                allowEscapeKey: false,
            }).then(() => {
                FCONTACTNORef.current.focus();
            });
            return;
        }
        if (!FBIRTHDATE && userdetail.companyID != 'CM001' && userdetail.departmentID != "DP001") {
            Swal.fire({
                icon: "error",
                title: isRestricted ? "Validation Error" : "वैधता त्रुटी",
                text: isRestricted ? "Date of birth should be selected." : "जन्मतारीख निवडली जावी.",
                allowOutsideClick: false,
                allowEscapeKey: false,
            }).then(() => {
                FBIRTHDATERef.current.focus();
            });
            return;
        }
        // if (!FBANKNAME || !nameRegex.test(FBANKNAME)) {
        //     Swal.fire({
        //         icon: "error",
        //         title: isRestricted ? "Validation Error" : "वैधता त्रुटी",
        //         text: isRestricted
        //             ? "Bank name must be valid (3 to 50 characters). No leading or trailing spaces allowed. Marathi and English letters allowed."
        //             : "बँक नाव वैध असावे. 3 ते 50 अक्षरे, सुरुवातीला किंवा शेवटी स्पेस नसावी. मराठी व इंग्रजी अक्षरे व अंक चालतील.",
        //         allowOutsideClick: false,
        //         allowEscapeKey: false,
        //     }).then(() => {
        //         FBANKNAMERef.current.focus();
        //     });
        //     return;
        // }
        // if (!FBRANCHNAME || !nameRegex.test(FBRANCHNAME)) {
        //     Swal.fire({
        //         icon: "error",
        //         title: isRestricted ? "Validation Error" : "वैधता त्रुटी",
        //         text: isRestricted
        //             ? "Branch name must be valid (3 to 50 characters). No leading or trailing spaces allowed."
        //             : "शाखेचे नाव वैध असावे. 3 ते 50 अक्षरे. सुरुवातीला किंवा शेवटी रिकामी जागा नसावी. मराठी व इंग्रजी अक्षरे चालतील.",
        //         allowOutsideClick: false,
        //         allowEscapeKey: false,
        //     }).then(() => {
        //         FBRANCHNAMERef.current.focus();
        //     });
        //     return;
        // }
        // if (!FACCOUNTNO || !/^([0-9\u0966-\u096F]{10,20})$/.test(FACCOUNTNO)) {
        //     Swal.fire({
        //         icon: "error",
        //         title: isRestricted ? "Validation Error" : "वैधता त्रुटी",
        //         text: isRestricted
        //             ? "Account number must be valid, containing 10 to 20 digits (0-9)."
        //             : "खाते क्रमांक वैध असावा, ज्यामध्ये 10 ते 20 अंक (०-९ किंवा 0-9) असावेत.",
        //         allowOutsideClick: false,
        //         allowEscapeKey: false,
        //     }).then(() => {
        //         FACCOUNTNORef.current.focus();
        //     });
        //     return;
        // }
        // if (!FIFSCCODE || !/^[A-Z]{4}0[A-Z0-9]{6}$/.test(FIFSCCODE)) {
        //     Swal.fire({
        //         icon: "error",
        //         title: isRestricted ? "Validation Error" : "वैधता त्रुटी",
        //         text: isRestricted
        //             ? "IFSC code must be in the format: ABCD0123456 (only uppercase letters and digits allowed)."
        //             : "IFSC कोड खालील स्वरूपात असावा: ABCD0123456 (फक्त इंग्रजी अक्षरे आणि अंक).",
        //         allowOutsideClick: false,
        //         allowEscapeKey: false,
        //     }).then(() => {
        //         FIFSCCODERef.current.focus();
        //     });
        //     return;
        // }
        handleSubmit(e);
    };
    useEffect(() => {
        const handleShortcut = (e) => {
            if (e.ctrlKey && e.key === 'e' || e.ctrlKey && e.key === 'E') {
                e.preventDefault();
                showExitAlert();
            }
            if (e.ctrlKey && e.key === 's' || e.ctrlKey && e.key === 'S') {
                e.preventDefault();
                validateinput(e);
            }
        };
        window.addEventListener('keydown', handleShortcut);
        return () => {
            window.removeEventListener('keydown', handleShortcut);
        };
    }, [navigate, formData]);
    const handleSelectChange = (selectedOption, fieldName) => {
        setFormData({
            ...formData,
            [fieldName]: selectedOption ? selectedOption.value : '',
        });
    };
    const handleSubmit = (event) => {
        event.preventDefault();
        console.log('Form Data:', formData);
        showConfirmationAlert(event);
    };
    const fetchStatus = async () => {
        try {
            const response = await axios.get(
                baseUrl.Url + "/backend/api/Implications/STATUS",
            );
            if (response.status !== 200) throw new Error("Failed to fetch implications data");
            const data = response.data;
            const implicationDropdown = data.map(({ iTitle, iValue }) => ({
                label: iTitle,
                value: iValue,
            }));
            setstatus(implicationDropdown);
        } catch (error) {
            console.error("Error fetching implications:", error);
        }
    };
    const fetchRelations = async () => {
        try {
            const response = await axios.get(
                baseUrl.Url + "/backend/api/Implications/RETYPE",
            );
            if (response.status !== 200) throw new Error("Failed to fetch implications data");
            const data = response.data;
            const implicationDropdown = data.map(({ iTitle, iValue }) => ({
                label: iTitle,
                value: iValue,
            }));
            setrelation(implicationDropdown);
        } catch (error) {
            console.error("Error fetching implications:", error);
        }
    };
    useEffect(() => {
        fetchStatus();
        fetchRelations();
    }, []);
    const handleFormSubmission = async () => {
        if (FAID) {
            try {
                const headers = {
                    "Content-Type": "application/json",
                    Accept: "*/*",
                };
                const aadhaarCheckPayload = {
                    fcontactno: formData.FCONTACTNO,
                };
                const aadhaarCheckResponse = await axios.post(
                    baseUrl.Url + "/backend/api/SP_CheckMobileNumber",
                    aadhaarCheckPayload,
                    { headers }
                );
                let isFarmerExists = false;
                if (
                    aadhaarCheckResponse.status === 200 &&
                    aadhaarCheckResponse.data.length > 0 &&
                    aadhaarCheckResponse.data[0].isSuccessful === 1
                ) {

                    console.log("👤 Farmer already exists.");
                    isFarmerExists = true;
                    // Swal.fire({
                    //     icon: "error",
                    //     title: isRestricted ? "Error" : "त्रुटी",
                    //     text: isRestricted
                    //         ? "Farmer is already exist."
                    //         : "शेतकरी आधीपासून अस्तित्वात आहे.",
                    //     allowOutsideClick: false,
                    //     allowEscapeKey: false,
                    // });
                    // return;

                }
                if (isFarmerExists) {
                    const payload1 = {
                        faid: FAID ? FAID : GUID,
                        uid: "",
                        fcmtoken: "",
                        refid: "",
                        fname: formData?.FNAME,
                        fcontactno: formData?.FCONTACTNO,
                        fbirthdate: formData?.FBIRTHDATE,
                        fpanno: formData?.FPANNO,
                        faddharno: formData?.FADDHARNO,
                        fbankname: formData?.FBANKNAME,
                        facconame: formData.FACCONAME,
                        fbranchname: formData.FBRANCHNAME,
                        faccountno: formData.FACCOUNTNO,
                        fifsccode: formData.FIFSCCODE,
                        fpincode: formData.FPINCODE,
                        fstate: formData.FSTATE,
                        fcity: formData.FCITY,
                        faddress: formData.FAddress,
                        fcroparea: "",
                        fcroptype: "",
                        fstatus: formData.FSTATUS,
                        fprofile: "",
                        companyid: userdetail?.companyID ? userdetail.companyID : "",
                        deptid: userdetail?.departmentID || "",
                        uaid: userdetail?.uaid ? userdetail.uaid : "",
                        date: formatDate(userdetail.APPDT)
                    };
                    const response1 = await axios.post(
                        baseUrl.Url + "/backend/api/SP_AddUpdFarmer",
                        payload1,
                        { headers }
                    );

                    if (response1.status !== 200) {
                        throw new Error("❌ Failed to save farmer data.");
                    }
                }
                const payload2 = rows.map((data) => ({
                    fdaid: data.fdaid ? data.fdaid : ACSPLGUID.getNew(),
                    maid: FAID ? FAID : GUID,
                    fdname: data.FDNAME,
                    fdbirthdate: data.FDBIRTHDATE,
                    fdaadharno: data.FDAADHARNO,
                    fdrelation: data.FDRELATION,
                    fdcontact: data.FDCONTACT,
                    companyid: userdetail?.companyID || "",
                    deptid: userdetail?.departmentID || "",
                    isdeleted: data.isdeleted === 1 || data.isdeleted === true,
                }));
                const response2 = await axios.post(
                    baseUrl.Url + "/backend/api/SP_AddFamilyDetails",
                    payload2,
                    { headers }
                );
                if (response2.status !== 200) {
                    throw new Error("❌ Failed to save family details.");
                }
                const payload3 = rows.map((data) => ({
                    fdaid: ACSPLGUID.getNew(),
                    faid: FAID ? FAID : GUID,
                    fdname: data.FDNAME,
                    fdbirthdate: data.FDBIRTHDATE,
                    fdaadharno: data.FDAADHARNO,
                    companyid: userdetail?.companyID || "",
                    deptid: userdetail?.departmentID || "",
                }));
                const response3 = await axios.post(
                    baseUrl.Url + "/backend/api/SP_AddUpdFarmerFamily",
                    payload3,
                    { headers }
                );
                if (response3.status !== 200) {
                    throw new Error("❌ Failed to save extended family info.");
                }
                Swal.fire({
                    icon: "success",
                    title: isRestricted ? "Success!" : "यशस्वी!",
                    text: isRestricted
                        ? "Farmer data has been saved successfully."
                        : "शेतकऱ्याचा डेटा यशस्वीरीत्या जतन झाला.",
                    confirmButtonText: isRestricted ? "OK" : "ठीक आहे",
                    allowOutsideClick: false,
                    allowEscapeKey: false,
                }).then(() => {
                    setFormData({
                        FNAME: '',
                        FCONTACTNO: '',
                        FBIRTHDATE: '',
                        FPANNO: '',
                        FADDHARNO: '',
                        FBANKNAME: '',
                        FACCONAME: '',
                        FBRANCHNAME: '',
                        FACCOUNTNO: '',
                        FIFSCCODE: '',
                        FPINCODE: '',
                        FSTATE: '',
                        FCITY: '',
                        FAddress: '',
                        FCROPAREA: '',
                        FCROPTYPE: '',
                        FSTATUS: ''
                    });
                    setRows([]);
                    navigate(route.Farmers);
                });
            } catch (error) {
                console.error("❌ Submission Error:", error);
                Swal.fire({
                    icon: "error",
                    title: isRestricted ? "Error" : "त्रुटी",
                    text: isRestricted
                        ? "Failed to save data. Please try again."
                        : "डेटा जतन करण्यात अयशस्वी. कृपया पुन्हा प्रयत्न करा.",
                    allowOutsideClick: false,
                    allowEscapeKey: false,
                });
            }
        } else {

            try {
                const headers = {
                    "Content-Type": "application/json",
                    Accept: "*/*",
                };
                const aadhaarCheckPayload = {
                    fcontactno: formData.FCONTACTNO,
                };
                const aadhaarCheckResponse = await axios.post(
                    baseUrl.Url + "/backend/api/SP_CheckMobileNumber",
                    aadhaarCheckPayload,
                    { headers }
                );
                let isFarmerExists = false;
                if (
                    aadhaarCheckResponse.status === 200 &&
                    aadhaarCheckResponse.data.length > 0 &&
                    aadhaarCheckResponse.data[0].isSuccessful === 1
                ) {

                    console.log("👤 Farmer already exists.");
                    isFarmerExists = true;
                    Swal.fire({
                        icon: "error",
                        title: isRestricted ? "Error" : "त्रुटी",
                        text: isRestricted
                            ? "A farmer with this mobile number already exists."
                            : "शेतकरी आधीपासून अस्तित्वात आहे.",
                        allowOutsideClick: false,
                        allowEscapeKey: false,
                    });
                    return;

                }
                if (!isFarmerExists) {
                    const payload1 = {
                        faid: FAID ? FAID : GUID,
                        uid: "",
                        fcmtoken: "",
                        refid: "",
                        fname: formData?.FNAME,
                        fcontactno: formData?.FCONTACTNO,
                        fbirthdate: formData?.FBIRTHDATE,
                        fpanno: formData?.FPANNO,
                        faddharno: formData?.FADDHARNO,
                        fbankname: formData?.FBANKNAME,
                        facconame: formData.FACCONAME,
                        fbranchname: formData.FBRANCHNAME,
                        faccountno: formData.FACCOUNTNO,
                        fifsccode: formData.FIFSCCODE,
                        fpincode: formData.FPINCODE,
                        fstate: formData.FSTATE,
                        fcity: formData.FCITY,
                        faddress: formData.FAddress,
                        fcroparea: "",
                        fcroptype: "",
                        fstatus: formData.FSTATUS,
                        fprofile: "",
                        companyid: userdetail?.companyID ? userdetail.companyID : "",
                        deptid: userdetail?.departmentID || "",
                        uaid: userdetail?.uaid ? userdetail.uaid : "",
                        date: formatDate(userdetail.APPDT)
                    };
                    const response1 = await axios.post(
                        baseUrl.Url + "/backend/api/SP_AddUpdFarmer",
                        payload1,
                        { headers }
                    );

                    if (response1.status !== 200) {
                        throw new Error("❌ Failed to save farmer data.");
                    }
                }
                const payload2 = rows.map((data) => ({
                    fdaid: data.fdaid ? data.fdaid : ACSPLGUID.getNew(),
                    maid: FAID ? FAID : GUID,
                    fdname: data.FDNAME,
                    fdbirthdate: data.FDBIRTHDATE,
                    fdaadharno: data.FDAADHARNO,
                    fdrelation: data.FDRELATION,
                    fdcontact: data.FDCONTACT,
                    companyid: userdetail?.companyID || "",
                    deptid: userdetail?.departmentID || "",
                    isdeleted: data.isdeleted === 1 || data.isdeleted === true,
                }));
                const response2 = await axios.post(
                    baseUrl.Url + "/backend/api/SP_AddFamilyDetails",
                    payload2,
                    { headers }
                );
                if (response2.status !== 200) {
                    throw new Error("❌ Failed to save family details.");
                }
                const payload3 = rows.map((data) => ({
                    fdaid: ACSPLGUID.getNew(),
                    faid: FAID ? FAID : GUID,
                    fdname: data.FDNAME,
                    fdbirthdate: data.FDBIRTHDATE,
                    fdaadharno: data.FDAADHARNO,
                    companyid: userdetail?.companyID || "",
                    deptid: userdetail?.departmentID || "",
                }));
                const response3 = await axios.post(
                    baseUrl.Url + "/backend/api/SP_AddUpdFarmerFamily",
                    payload3,
                    { headers }
                );
                if (response3.status !== 200) {
                    throw new Error("❌ Failed to save extended family info.");
                }
                Swal.fire({
                    icon: "success",
                    title: isRestricted ? "Success!" : "यशस्वी!",
                    text: isRestricted
                        ? "Farmer data has been saved successfully."
                        : "शेतकऱ्याचा डेटा यशस्वीरीत्या जतन झाला.",
                    confirmButtonText: isRestricted ? "OK" : "ठीक आहे",
                    allowOutsideClick: false,
                    allowEscapeKey: false,
                }).then(() => {
                    setFormData({
                        FNAME: '',
                        FCONTACTNO: '',
                        FBIRTHDATE: '',
                        FPANNO: '',
                        FADDHARNO: '',
                        FBANKNAME: '',
                        FACCONAME: '',
                        FBRANCHNAME: '',
                        FACCOUNTNO: '',
                        FIFSCCODE: '',
                        FPINCODE: '',
                        FSTATE: '',
                        FCITY: '',
                        FAddress: '',
                        FCROPAREA: '',
                        FCROPTYPE: '',
                        FSTATUS: ''
                    });
                    setRows([]);
                    navigate(route.Farmers);
                });
            } catch (error) {
                console.error("❌ Submission Error:", error);
                Swal.fire({
                    icon: "error",
                    title: isRestricted ? "Error" : "त्रुटी",
                    text: isRestricted
                        ? "Failed to save data. Please try again."
                        : "डेटा जतन करण्यात अयशस्वी. कृपया पुन्हा प्रयत्न करा.",
                    allowOutsideClick: false,
                    allowEscapeKey: false,
                });
            }
        }
    };
    useEffect(() => {
        if (!FAID) return;
        const fetchMasterData = async () => {
            try {
                const payload1 = {
                    "faid": FAID,
                    "keyword": "%",
                    companyid: userdetail?.companyID || "",
                    deptid: userdetail?.departmentID || "",
                };
                const headers = {
                    "Content-Type": "application/json",
                    Accept: "*/*"
                };
                const response = await axios.post(
                    baseUrl.Url + "/backend/api/GET_GateFarmer",
                    payload1,
                    { headers }
                );
                if (response.status !== 200) throw new Error("Failed to fetch data");
                let apiData = response.data[0] || [];
                setFormData((prev) => ({
                    ...prev,
                    FAID: apiData.faid || "",
                    FNAME: apiData.fname || "",
                    FCONTACTNO: apiData.fcontactno || "",
                    FBIRTHDATE: convertToISODate(apiData.fbirthdate) || "",
                    FPANNO: apiData.fpanno || "",
                    FADDHARNO: apiData.faddharno || "",
                    FBANKNAME: apiData.fbankname || "",
                    FACCONAME: apiData.facconame || "",
                    FBRANCHNAME: apiData.fbranchname || "",
                    FACCOUNTNO: apiData.faccountno || "",
                    FIFSCCODE: apiData.fifsccode || "",
                    FPINCODE: apiData.fpincode || "",
                    FAddress: apiData.fAddress || "",
                    FCROPAREA: '',
                    FCROPTYPE: '',
                    FCITY: sellingtype.find((city) => city.value == apiData.fcity)?.value || "",
                    FSTATE: statetype.find((state) => state.value == apiData.fstate)?.value || "",
                    FSTATUS: apiData.fstatus || "",
                }));
                console.log(" Master Data:", apiData);
            } catch (error) {
                console.error("Error in Master API Call:", error);
            }
        };
        const fetchDetailsData = async () => {
            try {
                const payload = {
                    maid: FAID,
                    "companyid": userdetail?.companyID || "",
                    "deptid": userdetail?.departmentID || "",
                };
                const headers = {
                    "Content-Type": "application/json",
                    Accept: "*/*"
                };
                const response = await axios.post(
                    baseUrl.Url + "/backend/api/GET_FamilyDetailsById",
                    payload,
                    { headers }
                );
                if (response.status !== 200) throw new Error("Failed to fetch details data");
                console.log(" Detail Data:", response.data);
                if (response.data.length > 0) {
                    const mappedProducts = response.data.map((item) => ({
                        fdaid: item.fdaid,
                        FAID: item.maid,
                        FDNAME: item.fdname,
                        FDBIRTHDATE: item.fdbirthdate,
                        FDAADHARNO: item.fdaadharno,
                        FDCONTACT: item.fdcontact,
                        FDRELATION: item.fdrelation,
                        isdeleted: item.isdeleted,
                    }));
                    setRows(mappedProducts);
                }
            } catch (error) {
                console.error("Error fetching details data:", error);
            }
        };
        fetchMasterData();
        fetchDetailsData();
    }, [FAID]);
    const [sellingtype, setsellingtype] = useState([]);
    const [statetype, setstatetype] = useState([]);
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
    const handleChange = async (e) => {
        const { name, value } = e.target;
        // setFormData((prevData) => ({
        //     ...prevData,
        //     [name]: value,
        // }));
        const namePattern = /^[A-Za-z]+( [A-Za-z]+)*\s*$/;
        const aadhaarPattern = /^\d{12}$/;
        if (name === "FNAME") {
            if (value === "" || namePattern.test(value)) {
                setFormData((prev) => ({
                    ...prev,
                    [name]: value,
                }));
            } else {
                Swal.fire({
                    icon: "error",
                    title: isRestricted ? "Invalid Full Name" : "अवैध पूर्ण नाव",
                    text: isRestricted
                        ? "The full name should be valid, with no leading or trailing spaces."
                        : "पूर्ण नाव वैध असावे, ज्यात सुरुवातीला किंवा शेवटी रिकाम्या जागा नकोत.",
                    allowOutsideClick: false,
                    allowEscapeKey: false,
                });
            }
        }
        else if (name === "FADDHARNO") {
            const cleanedValue = value.replace(/\D/g, ""); // Only digits

            setFormData((prev) => ({
                ...prev,
                [name]: cleanedValue,
            }));

            // Show error if 12 digits are entered but not valid
            if (cleanedValue.length === 12 && !aadhaarPattern.test(cleanedValue)) {
                Swal.fire({
                    icon: "error",
                    title: isRestricted ? "Invalid Aadhaar Number" : "अवैध आधार क्रमांक",
                    text: isRestricted
                        ? "Enter a valid 12-digit Aadhaar number."
                        : "वैध 12 अंकी आधार क्रमांक प्रविष्ट करा.",
                    allowOutsideClick: false,
                    allowEscapeKey: false,
                }).then(() => {
                    setFormData((prev) => ({
                        ...prev,
                        [name]: "",
                    }));
                });
            }
        } else {
            // other fields logic
            setFormData((prev) => ({
                ...prev,
                [name]: value,
            }));
        }
        if (name === "FPINCODE") {
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
                        setFormData((prevData) => ({
                            ...prevData,
                            FSTATE: data[0]?.said || "",
                            FCITY: data[0]?.said || "",
                        }));
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
    const handleEnterKey = (e, nextRef) => {
        if (e.key === 'Enter') {
            e.preventDefault();
            if (nextRef && nextRef.current) {
                nextRef.current.focus();
            }
        }
    };
    const MySwal = withReactContent(Swal);
    const showConfirmationAlert = (event) => {
        MySwal.fire({
            title: isRestricted ? "Are you sure?" : "तुम्हाला खात्री आहे का?",
            text: isRestricted
                ? "Do you want to save this data?"
                : "तुम्हाला हा डेटा जतन करायचा आहे का?",
            showCancelButton: true,
            confirmButtonColor: "#00ff00",
            confirmButtonText: isRestricted ? "Save" : "जतन करा",
            cancelButtonColor: "#092C4C",
            cancelButtonText: isRestricted ? "Cancel" : "रद्द करा",
            allowOutsideClick: false,
            allowEscapeKey: false,
        }).then((result) => {
            if (result.isConfirmed) {
                handleFormSubmission(event);
            }
        });
    };
    const showExitAlert = () => {
        MySwal.fire({
            title: isRestricted ? "Are you sure?" : "तुम्हाला खात्री आहे का?",
            text: isRestricted
                ? "Do you want to exit?"
                : "तुम्ही बाहेर पडू इच्छिता का?",
            showCancelButton: true,
            confirmButtonColor: "#00ff00",
            confirmButtonText: isRestricted ? "Yes" : "होय",
            cancelButtonColor: "#092C4C",
            cancelButtonText: isRestricted ? "No" : "नाही",
        }).then((result) => {
            if (result.isConfirmed) {
                if (ID == '101') {
                    navigate(route.QuickFarmer)
                } else {
                    navigate(route.Farmers)
                }
            }
        });
    };
    const handleInputChange = (e) => {
        const { name, value } = e.target;
        if (name in formData) {
            setFormData({
                ...formData,
                [name]: value,
            });
        } else {
            setDetailData({
                ...detailData,
                [name]: value,
            });
        }
    };
    const handleAddDetail = () => {
        console.log(detailData);
        if (!detailData.FDNAME || !detailData.FDBIRTHDATE || !detailData.FDRELATION || !detailData.FDCONTACT) {
            Swal.fire({
                icon: "error",
                title: isRestricted ? "Error" : "चूक",
                text: isRestricted
                    ? "Please fill all the fields properly."
                    : "कृपया सर्व फील्ड नीट भरावेत.",
                allowOutsideClick: false,
                allowEscapeKey: false,
            });
            return;
        }
        if (detailData.fdaid) {
            const updatedRows = rows.map(row =>
                row.fdaid === detailData.fdaid
                    ? { ...row, ...detailData }
                    : row
            );
            setRows(updatedRows);
        } else {
            setRows([...rows, { ...detailData, fdaid: ACSPLGUID.getNew() }]);
        }
        console.log(rows, "deatil rows");
        setDetailData({
            fdaid: '',
            FDNAME: '',
            FDBIRTHDATE: '',
            FDRELATION: '',
            FDCONTACT: '',
            isdeleted: 0,
        });
    };
    const handleEdit = (fdaid) => {
        console.log("Editing row with fdaid:", fdaid);
        const filteredProduct = rows.find((row) => row.fdaid === fdaid);
        if (filteredProduct) {
            setDetailData({
                fdaid: filteredProduct.fdaid,
                FDNAME: filteredProduct.FDNAME,
                FDBIRTHDATE: convertToISODate(filteredProduct.FDBIRTHDATE),
                FDRELATION: filteredProduct.FDRELATION,
                FDAADHARNO: filteredProduct.FDAADHARNO,
                FDCONTACT: filteredProduct.FDCONTACT,
            });
        }
    };
    const handleDelete = (fdaid) => {
        Swal.fire({
            title: isRestricted ? "Are you sure?" : "तुम्हाला खात्री आहे का?",
            text: isRestricted
                ? "You won't be able to revert this!"
                : "तुम्ही हे बदलू शकणार नाही!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#d33",
            cancelButtonColor: "#3085d6",
            confirmButtonText: isRestricted ? "Yes, delete it!" : "हो, ते हटवा!",
            cancelButtonText: isRestricted ? "Cancel" : "रद्द करा",
        }).then((result) => {
            if (result.isConfirmed) {
                setRows((prevRows) => {
                    const updatedRows = prevRows.map((row) =>
                        row.fdaid === fdaid ? { ...row, isdeleted: 1 } : row
                    );
                    console.log("Updated Rows:", updatedRows);
                    return updatedRows;
                });
                Swal.fire({
                    icon: "success",
                    title: isRestricted ? "Deleted!" : "हटवले!",
                    text: isRestricted
                        ? "Record has been deleted successfully."
                        : "रेकॉर्ड यशस्वीपणे हटवला गेला आहे.",
                    confirmButtonText: isRestricted ? "OK" : "ठीक आहे",
                    allowOutsideClick: false,
                    allowEscapeKey: false,
                });
            }
        });
    };
    const handleAddharChange = (e) => {
        // setFormData({ ...formData, [e.target.name]: e.target.value });

        const { name, value } = e.target;
        const digitsOnly = value.replace(/\D/g, ""); // Remove non-digit characters

        // Limit to 10 digits
        if (digitsOnly.length <= 10) {
            setFormData({ ...formData, [name]: digitsOnly });
        }

        if (e.target.name == 'FCONTACTNO') {

            const mobilePattern = /^[789][0-9]{9}$/;

            if (digitsOnly.length === 10 && !mobilePattern.test(digitsOnly)) {
                Swal.fire({
                    icon: "error",
                    title: isRestricted ? "Invalid Mobile Number" : "अवैध मोबाईल नंबर",
                    text: isRestricted
                        ? "Mobile number must be 10 digits and start with 7, 8, or 9."
                        : "मोबाईल नंबर ७, ८ किंवा ९ पासून सुरू होऊन १० अंकी असावा.",
                    confirmButtonText: isRestricted ? "OK" : "ठीक आहे",
                    allowOutsideClick: false,
                    allowEscapeKey: false,
                }).then(() => {
                    setFormData((prev) => ({
                        ...prev,
                        [name]: "",
                    }));
                });
                return; // Stop here if local validation fails
            }

            if (digitsOnly.length === 10 && mobilePattern.test(digitsOnly)) {
                try {
                    const payload = {
                        "fcontactno": e.target.value,
                    };
                    const headers = {
                        "Content-Type": "application/json",
                        Accept: "*/*",
                    };
                    axios({
                        method: "POST",
                        url: baseUrl.Url + "/backend/api/SP_CheckMobileNumber",
                        data: JSON.stringify(payload),
                        headers: headers,
                    })
                        .then((response) => {
                            if (response.status === 200) {
                                console.log(response.data)
                                if (response.data[0].isSuccessful == 1) {
                                    Swal.fire({
                                        icon: "error",
                                        title: isRestricted ? "Error" : "त्रुटी",
                                        text: response.data[0].responseMessage,  // No translation since message comes from backend
                                        confirmButtonText: isRestricted ? "OK" : "ठीक आहे",
                                        allowOutsideClick: false,
                                        allowEscapeKey: false,
                                    }).then((result) => {
                                        if (result.isConfirmed) {
                                            console.log("data save succsess")
                                        }
                                    });
                                }
                            }
                        });
                } catch (error) {
                    console.error('get data Error:', error);
                }
            }
        }

    };
    const handleDetailAddharChange = (e) => {
        setDetailData({ ...detailData, [e.target.name]: e.target.value });
        if (e.target.name == 'FDAADHARNO') {
            try {
                const payload = {
                    "aadhaar": e.target.value,
                };
                const headers = {
                    "Content-Type": "application/json",
                    Accept: "*/*",
                };
                axios({
                    method: "POST",
                    url: baseUrl.Url + "/backend/api/SP_CheckAadhaardetail",
                    data: JSON.stringify(payload),
                    headers: headers,
                })
                    .then((response) => {
                        if (response.status === 200) {
                            console.log(response.data)
                            if (response.data[0].isSuccessful == 1) {
                                Swal.fire({
                                    icon: "error",
                                    title: isRestricted ? "Error" : "त्रुटी",
                                    text: response.data[0].responseMessage,
                                    confirmButtonText: isRestricted ? "OK" : "ठीक आहे",
                                    allowOutsideClick: false,
                                    allowEscapeKey: false,
                                }).then((result) => {
                                    if (result.isConfirmed) {
                                        console.log("data save succsess")
                                    }
                                });
                            }
                        }
                    });
            } catch (error) {
                console.error('get data Error:', error);
            }
        }
    };
    return (
        <div className="page-wrapper">
            <div className="content">
                <div className="page-header">
                    <div className="add-item d-flex">
                        <div className="page-title">
                            <h4>{isRestricted ? "Farmer Info" : "शेतकरी माहिती"}</h4>
                            {/* <h6>शेतकरी माहिती भरा</h6> */}
                        </div>
                    </div>
                    <ul className="table-top-head">
                        <li>
                            <OverlayTrigger placement="top" overlay={renderCollapseTooltip}>
                                <Link
                                    data-bs-toggle="tooltip"
                                    data-bs-placement="top"
                                    title="Collapse"
                                    id="collapse-header"
                                >
                                    <ChevronUp className="feather-chevron-up" />
                                </Link>
                            </OverlayTrigger>
                        </li>
                    </ul>
                    <div className="page-btn">
                        <Link onClick={showExitAlert} className="btn btn-secondary">
                            <ArrowLeft className="me-2" />
                            {isRestricted ? "back" : "मागे"}
                        </Link>
                    </div>
                </div>
                <form onSubmit={handleSubmit}>
                    <div className="card mbgcolor">
                        <div className="card-body  mbgcolor">
                            <div className="accordion-card-one accordion" id="accordionExample">
                                <div className="accordion-item mbgcolor">
                                    <div className="accordion-header" id="headingOne">
                                        <div className="" data-bs-toggle="collapse" data-bs-target="#collapseOne" aria-controls="collapseOne">
                                            <div className="addproduct-icon">
                                                <h5 >
                                                    <span> {isRestricted ? "Farmer Info" : "शेतकरी माहिती"}</span>
                                                </h5>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="" className="accordion-collapse collapse show p-0" aria-labelledby="headingOne" data-bs-parent="#accordionExample">
                                        <div className="accordion-body">
                                            <div className="row">
                                                <div className="col-lg-4 col-sm-6 col-12">
                                                    <div className=" ">
                                                        <label className='required form-label'> {isRestricted ? "Full Name" : "पूर्ण नाव"} </label>
                                                        <input
                                                            type="text"
                                                            className="form-control"
                                                            placeholder={isRestricted ? "Full Name" : "पूर्ण नाव"}
                                                            name="FNAME"
                                                            value={formData.FNAME}
                                                            onChange={handleChange}
                                                            pattern="^[A-Za-z]+( [A-Za-z]+)*\s*$"
                                                            title={isRestricted ? "The full name should be valid, with no leading spaces." : "पूर्ण नाव वैध असावे, ज्यात सुरुवातीला रिकाम्या जागा नकोत."}
                                                            ref={FNAMERef}
                                                            onKeyDown={(e) => handleEnterKey(e, FCONTACTNORef)}
                                                            required
                                                        />
                                                    </div>
                                                </div>
                                                <div className="col-lg-2 col-sm-4 col-6">
                                                    <div className=" ">
                                                        <label className='required form-label'>{isRestricted ? "Mobile No." : "मोबाईल नंबर"} </label>
                                                        <input
                                                            type="text"
                                                            className="form-control"
                                                            placeholder={isRestricted ? "Mobile No." : "मोबाईल नंबर"}
                                                            name="FCONTACTNO"
                                                            maxLength={10}
                                                            value={formData.FCONTACTNO}
                                                            // onChange={handleChange}
                                                            onChange={handleAddharChange}
                                                            pattern="^[0-9\u0966-\u096F]{10}$"
                                                            title={isRestricted ? "The mobile number must be a valid 10-digit number." : "मोबाईल नंबर वैध 10 अंकी नंबर असावा."}
                                                            ref={FCONTACTNORef}
                                                            onKeyDown={(e) => handleEnterKey(e, FADDHARNORef)}
                                                            required
                                                        />
                                                    </div>
                                                </div>
                                                {!isRestricted && <>
                                                    <div className="col-lg-2 col-sm-4 col-12">
                                                        <div className=" ">
                                                            <label className='required form-label'>{isRestricted ? "Birth Date" : "जन्मतारीख"}</label>
                                                            <input
                                                                type="date"
                                                                className="form-control"
                                                                name="FBIRTHDATE"
                                                                value={formData.FBIRTHDATE}
                                                                onChange={handleChange}
                                                                title={isRestricted ? "Please select the date of birth." : "जन्मतारीख निवडली जावी."}
                                                                ref={FBIRTHDATERef}
                                                                onKeyDown={(e) => handleEnterKey(e, FPANNORef)}
                                                                max={formatDate(userdetail.APPDT)}
                                                                required
                                                            />
                                                        </div>
                                                    </div>
                                                </>}
                                                {!isRestricted && <>
                                                    <div className="col-lg-2 col-sm-4 col-12">
                                                        <div>
                                                            <label className='form-label' >{isRestricted ? "Pan No." : "पॅन क्रमांक"}</label>
                                                            <input
                                                                type="text"
                                                                className="form-control"
                                                                placeholder={isRestricted ? "Pan No." : "पॅन क्रमांक"}
                                                                name="FPANNO"
                                                                value={formData.FPANNO}
                                                                onChange={handleChange}
                                                                pattern="^[A-Z]{5}[0-9]{4}[A-Z]{1}$" // PAN Number format: 5 letters, 4 digits, 1 letter
                                                                title={isRestricted ? "Enter PAN number in valid format (e.g. ABCDE1234F)." : "PAN नंबर योग्य फॉर्मॅटमध्ये असावा (उदा. ABCDE1234F)."}
                                                                ref={FPANNORef}
                                                                onKeyDown={(e) => handleEnterKey(e, FADDHARNORef)}
                                                            />
                                                        </div>
                                                    </div>
                                                </>}
                                                <div className="col-lg-2 col-sm-4 col-6">
                                                    <div className=" ">
                                                        <label className='form-label'>{isRestricted ? "Aadhaar No." : "आधार क्रमांक"}</label>
                                                        <input
                                                            type="text"
                                                            className="form-control"
                                                            placeholder={isRestricted ? "Aadhaar No." : "आधार क्रमांक"}
                                                            name="FADDHARNO"
                                                            value={formData.FADDHARNO}
                                                            onChange={handleChange}
                                                            maxLength={12}
                                                            // onChange={handleAddharChange}
                                                            pattern="^([0-9\u0966-\u096F]{12})$"
                                                            title={isRestricted ? "Enter a valid 12-digit Aadhaar number." : "आधार नंबर वैध 12 अंकी नंबर असावा."}
                                                            ref={FADDHARNORef}
                                                            onKeyDown={(e) => handleEnterKey(e, FACCONAMERef)}
                                                        />
                                                    </div>
                                                </div>
                                            </div>
                                            <div
                                                className="accordion-card-one accordion"
                                                id="accordionExample2"
                                            >
                                                <div className="accordion-item mbgcolor">
                                                    <div className="accordion-header" id="headingTwo">
                                                        <div
                                                            className=""
                                                            data-bs-toggle="collapse"
                                                            data-bs-target="#collapseTwo"
                                                            aria-controls="collapseTwo"
                                                        >
                                                            <div className="text-editor add-list">
                                                                <div className="addproduct-icon list icon">
                                                                    <h5>
                                                                        <hr />
                                                                        <span>{isRestricted ? "Bank Info" : "बँक तपशील"}</span>
                                                                    </h5>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div
                                                    >
                                                        <div className="row">
                                                            <div className="col-lg-4 col-sm-6 col-12">
                                                                <div className=" ">
                                                                    <label className='form-label'>{isRestricted ? "Account Holder's Name" : "खातेदार नाव"}</label>
                                                                    <input
                                                                        type="text"
                                                                        className="form-control"
                                                                        placeholder={isRestricted ? "Account Holder's Name" : "खातेदार नाव"}
                                                                        name="FACCONAME"
                                                                        value={formData.FACCONAME || formData.FNAME}
                                                                        onChange={handleChange}
                                                                        ref={FACCONAMERef}
                                                                        pattern="^[A-Za-z]+( [A-Za-z]+)*\s*$" // Allow letters, numbers, and spaces
                                                                        title={isRestricted ? "Enter a valid bank name without leading spaces." : "बँक नाव वैध असावे, ज्यात सुरुवातीला रिकाम्या जागा नकोत."}
                                                                        onKeyDown={(e) => handleEnterKey(e, FBANKNAMERef)}

                                                                    />
                                                                </div>
                                                            </div>
                                                            <div className="col-lg-2 col-sm-6 col-12">
                                                                <div className=" ">
                                                                    <label className=' form-label'>{isRestricted ? "Bank Name" : " बँकेचे नाव"}</label>
                                                                    <input
                                                                        type="text"
                                                                        className="form-control"
                                                                        placeholder={isRestricted ? "Bank Name" : " बँकेचे नाव"}
                                                                        name="FBANKNAME"
                                                                        value={formData.FBANKNAME}
                                                                        onChange={handleChange}
                                                                        ref={FBANKNAMERef}
                                                                        pattern="^(?!\s)([A-Za-z0-9\u0900-\u097F\s]{3,50})(?<!\s)$" // Allow letters, numbers, and spaces
                                                                        title={isRestricted ? "Enter a valid bank name " : "बँक नाव वैध असावे, ज्यात सुरुवातीला रिकाम्या जागा नकोत."}
                                                                        onKeyDown={(e) => handleEnterKey(e, FBRANCHNAMERef)}

                                                                    />
                                                                </div>
                                                            </div>
                                                            <div className="col-lg-2 col-sm-6 col-12">
                                                                <div className=" ">
                                                                    <label className=' form-label'>{isRestricted ? "Branch Name" : "शाखेचे नाव"}</label>
                                                                    <input
                                                                        type="text"
                                                                        className="form-control"
                                                                        placeholder={isRestricted ? "Branch Name" : "शाखेचे नाव"}
                                                                        name="FBRANCHNAME"
                                                                        value={formData.FBRANCHNAME}
                                                                        onChange={handleChange}
                                                                        ref={FBRANCHNAMERef}
                                                                        pattern="^(?!\s)([A-Za-z0-9\u0900-\u097F\s]{3,50})(?<!\s)$" // Allow letters, numbers, and spaces
                                                                        title={isRestricted ? "Enter a valid branch name" : "शाखेचे नाव वैध असावे, ज्यात सुरुवातीला रिकाम्या जागा नकोत."}
                                                                        onKeyDown={(e) => handleEnterKey(e, FACCOUNTNORef)}

                                                                    />
                                                                </div>
                                                            </div>
                                                            <div className="col-lg-2 col-sm-4 col-6">
                                                                <div className=" ">
                                                                    <label className=' form-label'>{isRestricted ? "Account No" : "खाते क्रमांक"}</label>
                                                                    <input
                                                                        type="text"
                                                                        className="form-control"
                                                                        placeholder={isRestricted ? "Account No" : "खाते क्रमांक"}
                                                                        name="FACCOUNTNO"
                                                                        value={formData.FACCOUNTNO}
                                                                        onChange={handleChange}
                                                                        ref={FACCOUNTNORef}
                                                                        pattern="^[0-9\u0966-\u096F]{9,18}$" // Account Number pattern with 10-20 digits
                                                                        title={isRestricted ? "Enter a valid account number with 10 to 20 digits." : "खाते नंबर वैध असावा, ज्यात 10-20 अंक असावेत."}
                                                                        onKeyDown={(e) => handleEnterKey(e, FIFSCCODERef)}

                                                                    />
                                                                </div>
                                                            </div>
                                                            <div className="col-lg-2 col-sm-4 col-6">
                                                                <div className=" ">
                                                                    <label className=' form-label'>{isRestricted ? "IFSC code" : "IFSC कोड"}</label>
                                                                    <input
                                                                        type="text"
                                                                        className="form-control"
                                                                        placeholder={isRestricted ? "IFSC code" : "IFSC कोड"}
                                                                        name="FIFSCCODE"
                                                                        value={formData.FIFSCCODE}
                                                                        onChange={handleChange}
                                                                        ref={FIFSCCODERef}
                                                                        pattern="^[A-Za-z]{4}0[A-Z0-9]{6}$" // Validate the IFSC code format
                                                                        title={isRestricted ? "Enter the IFSC code in the format: ABCD0123456." : "IFSC कोड खालील फॉर्मॅटमध्ये असावा: ABCD0123456."}
                                                                        onKeyDown={(e) => handleEnterKey(e, FPINCODERef)}

                                                                    />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div
                                                className="accordion-card-one accordion"
                                                id="accordionExample2"
                                            >
                                                <div className="accordion-item mbgcolor">
                                                    <div className="accordion-header" id="headingTwo">
                                                        <div
                                                            className=""
                                                            data-bs-toggle="collapse"
                                                            data-bs-target="#collapseTwo"
                                                            aria-controls="collapseTwo"
                                                        >
                                                            <div className="text-editor add-list">
                                                                <div className="addproduct-icon list icon">
                                                                    <h5>
                                                                        <hr />
                                                                        <span>{isRestricted ? "Address" : "राहिवासी पत्ता"} </span>
                                                                    </h5>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div
                                                    >
                                                        <div className="row">
                                                            <div className="col-lg-2 col-sm-4 col-12">
                                                                <div className=" ">
                                                                    <label className='form-label '> {isRestricted ? "Pincode" : "पिनकोड"} </label>
                                                                    <input
                                                                        type="text"
                                                                        className="form-control"
                                                                        placeholder={isRestricted ? "Pincode" : "पिनकोड"}
                                                                        name="FPINCODE"
                                                                        value={formData.FPINCODE}
                                                                        maxLength={6}
                                                                        onChange={handleChange}
                                                                        pattern="^([0-9\u0966-\u096F]{6})$"
                                                                        title={isRestricted ? "Enter a valid 6-digit PIN code." : "कृपया वैध 6-अंकी पिन कोड टाका"}
                                                                        ref={FPINCODERef}
                                                                        onKeyDown={(e) => handleEnterKey(e, FSTATERef)}

                                                                    />
                                                                </div>
                                                            </div>
                                                            <div className="col-lg-2 col-sm-4 col-6">
                                                                <div className=" ">
                                                                    <label className=' form-label'>{isRestricted ? "State" : "राज्य"} </label>
                                                                    <Select
                                                                        classNamePrefix="react-select"
                                                                        name="FSTATE"
                                                                        options={statetype}
                                                                        value={statetype.find((option) => option.value === formData.FSTATE) || null}
                                                                        onChange={(selectedOption) => handleSelectChange(selectedOption, 'FSTATE')}
                                                                        placeholder={isRestricted ? "Select" : "निवडा"}
                                                                        ref={FSTATERef}
                                                                        onKeyDown={(e) => handleEnterKey(e, FCITYRef)}

                                                                    />
                                                                </div>
                                                            </div>
                                                            <div className="col-lg-2 col-sm-4 col-6">
                                                                <div className=" ">
                                                                    <label className=' form-label'>{isRestricted ? "City" : "शहर"}  </label>
                                                                    <Select
                                                                        classNamePrefix="react-select"
                                                                        name="FCITY"
                                                                        options={sellingtype}
                                                                        value={sellingtype.find((option) => option.value === formData.FCITY) || null}
                                                                        onChange={(selectedOption) => handleSelectChange(selectedOption, 'FCITY')}
                                                                        placeholder={isRestricted ? "Select" : "निवडा"}
                                                                        ref={FCITYRef}
                                                                        onKeyDown={(e) => handleEnterKey(e, FAddressRef)}

                                                                    />
                                                                </div>
                                                            </div>
                                                            <div className=" col-sm-6 col-12">
                                                                <div className=" ">
                                                                    <label className='form-label'>{isRestricted ? "Address" : "पत्ता"} </label>
                                                                    <input
                                                                        type="text"
                                                                        className="form-control"
                                                                        placeholder={isRestricted ? "Address" : "पत्ता"}
                                                                        name="FAddress"
                                                                        value={formData.FAddress}
                                                                        onChange={handleChange}
                                                                        ref={FAddressRef}
                                                                        onKeyDown={(e) => handleEnterKey(e, saveRef)}
                                                                    />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            {!isRestricted && <>
                                                <div
                                                    className="accordion-card-one accordion"
                                                    id="accordionExample2"
                                                >
                                                    <div className="accordion-item mbgcolor">
                                                        <div className="accordion-header" id="headingTwo">
                                                            <div
                                                                className=""
                                                                data-bs-toggle="collapse"
                                                                data-bs-target="#collapseTwo"
                                                                aria-controls="collapseTwo"
                                                            >
                                                                <div className="text-editor add-list">
                                                                    <div className="addproduct-icon list icon">
                                                                        <h5>
                                                                            <hr />
                                                                            <span>कुटुंबाची माहिती</span>
                                                                        </h5>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div>
                                                            <div className="border p-3 rounded shadow-sm mb-0">
                                                                <div className="row mt-0">
                                                                    <div className="row">
                                                                        <div className="col-lg-3 col-md-6 col-sm-12">
                                                                            <div className=" ">
                                                                                <label className='form-label'>पूर्ण नाव</label>
                                                                                <input
                                                                                    type="text"
                                                                                    className="form-control"
                                                                                    name="FDNAME"
                                                                                    value={detailData.FDNAME}
                                                                                    onChange={handleInputChange}
                                                                                    ref={FNAMERef}
                                                                                    onKeyDown={(e) => handleEnterKey(e, FDBIRTHDATERef)}
                                                                                />
                                                                            </div>
                                                                        </div>
                                                                        <div className="col-lg-2 col-md-4 col-sm-12">
                                                                            <div className=" ">
                                                                                <label className='form-label'>जन्मदिनांक</label>
                                                                                <input
                                                                                    type="date"
                                                                                    className="form-control"
                                                                                    name="FDBIRTHDATE"
                                                                                    value={detailData.FDBIRTHDATE}
                                                                                    onChange={handleInputChange}
                                                                                    max={new Date().toISOString().split("T")[0]}
                                                                                    ref={FDBIRTHDATERef}
                                                                                    onKeyDown={(e) => handleEnterKey(e, FDRELATIONRef)}
                                                                                />
                                                                            </div>
                                                                        </div>
                                                                        <div className="col-lg-2 col-md-4 col-sm-12">
                                                                            <div className=" ">
                                                                                <label className='form-label'>नातेसंबंध</label>
                                                                                <Select
                                                                                    classNamePrefix="react-select"
                                                                                    name="FDRELATION"
                                                                                    options={relation}
                                                                                    placeholder="निवडा"
                                                                                    value={relation.find((option) => option.value === detailData.FDRELATION) || null}
                                                                                    onChange={(selectedOption) => {
                                                                                        setDetailData((prevData) => ({
                                                                                            ...prevData,
                                                                                            FDRELATION: selectedOption ? selectedOption.value : "",
                                                                                        }));
                                                                                        if (FDAADHARNORef.current) {
                                                                                            FDAADHARNORef.current.focus();
                                                                                        }
                                                                                    }}
                                                                                    ref={FDRELATIONRef}
                                                                                />
                                                                            </div>
                                                                        </div>
                                                                        <div className="col-lg-2 col-md-4 col-sm-12">
                                                                            <div className=" ">
                                                                                <label className='form-label' >आधार नंबर</label>
                                                                                <input
                                                                                    type="text"
                                                                                    className="form-control"
                                                                                    name="FDAADHARNO"
                                                                                    value={detailData.FDAADHARNO}
                                                                                    onChange={handleDetailAddharChange}
                                                                                    ref={FDAADHARNORef}
                                                                                    pattern="^([0-9\u0966-\u096F]{12})$"
                                                                                    onKeyDown={(e) => handleEnterKey(e, FDCONTACTRef)}
                                                                                />
                                                                            </div>
                                                                        </div>
                                                                        <div className="col-lg-2 col-md-4 col-sm-12">
                                                                            <div className=" ">
                                                                                <label className='form-label'>मोबाईल नंबर</label>
                                                                                <input
                                                                                    type="text"
                                                                                    className="form-control"
                                                                                    name="FDCONTACT"
                                                                                    value={detailData.FDCONTACT}
                                                                                    onChange={handleInputChange}
                                                                                    ref={FDCONTACTRef}
                                                                                    pattern="^[A-Z]{5}[0-9]{4}[A-Z]{1}$"
                                                                                    onKeyDown={(e) => handleEnterKey(e, submitRef)}
                                                                                />
                                                                            </div>
                                                                        </div>
                                                                        <div className="col-lg-1 col-md-4 col-sm-12 mt-4">
                                                                            <button
                                                                                type="button"
                                                                                className="btn btn-submit"
                                                                                onClick={handleAddDetail}
                                                                                ref={submitRef}
                                                                            >
                                                                                Add
                                                                            </button>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div className="row">
                                                                <div className="col-lg-12">
                                                                    <div className="modal-body-table">
                                                                        <div
                                                                            className="table-responsive"
                                                                            style={{
                                                                                height: 'calc(62vh - 120px)',
                                                                                overflowY: 'auto',
                                                                                overflowX: 'auto',
                                                                            }}
                                                                        >
                                                                            <table className="table table-bordered">
                                                                                <thead className="thead-dark">
                                                                                    <tr>
                                                                                        <th className="col-2" style={{ position: 'sticky', top: 0 }}>पूर्ण नाव</th>
                                                                                        <th className="col-1" style={{ position: 'sticky', top: 0 }}>जन्मदिनांक</th>
                                                                                        <th className="col-2" style={{ position: 'sticky', top: 0 }}>नातेसंबंध</th>
                                                                                        <th className="col-2" style={{ position: 'sticky', top: 0 }}>आधार नंबर</th>
                                                                                        <th className="col-2" style={{ position: 'sticky', top: 0 }}>मोबाईल नंबर</th>
                                                                                        <th className="col-1">Actions</th>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                                    {rows.filter((row) => row.isdeleted == 0 || row.isdeleted == false).length > 0 ? (
                                                                                        rows
                                                                                            .filter((row) => row.isdeleted == 0)
                                                                                            .map((row, index) => (
                                                                                                <tr key={row.fdaid || index}>
                                                                                                    <td>{row.FDNAME}</td>
                                                                                                    <td>{row.FDBIRTHDATE}</td>
                                                                                                    <td>{row.FDRELATION}</td>
                                                                                                    <td>{row.FDAADHARNO}</td>
                                                                                                    <td>{row.FDCONTACT}</td>
                                                                                                    <td>
                                                                                                        <Link
                                                                                                            to="#"
                                                                                                            onClick={() => handleEdit(row.fdaid)}
                                                                                                            className="me-2 p-1"
                                                                                                            style={{ color: 'lightblue' }}
                                                                                                        >
                                                                                                            <Edit className="feather-edit" />
                                                                                                        </Link>
                                                                                                        <Link
                                                                                                            className="confirm-text p-2"
                                                                                                            to="#"
                                                                                                            onClick={() => handleDelete(row.fdaid)}
                                                                                                        >
                                                                                                            <Trash2 className="feather-trash-2 text-danger" />
                                                                                                        </Link>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            ))
                                                                                    ) : (
                                                                                        <tr>

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
                                                </div>
                                            </>}
                                        </div>
                                        <div>
                                            <div className="mt-2 me-2 mb-2">
                                                <div className="row justify-content-end">
                                                    <div className="col-lg-6 col-sm-12 col-12">
                                                        <label className='form-label'>{isRestricted ? "Status" : "स्टेटस"}</label>
                                                        <div className=" ">
                                                            <Select
                                                                classNamePrefix="react-select"
                                                                name="FSTATUS"
                                                                options={status}
                                                                placeholder={isRestricted ? "Select" : "निवडा"}
                                                                ref={STATUSRef}
                                                                value={
                                                                    status.find((option) => option.value === formData.FSTATUS)
                                                                    || (status.length > 0 ? status[0] : null)
                                                                }
                                                                onChange={(selectedOption) => {
                                                                    setFormData((prevData) => ({
                                                                        ...prevData,
                                                                        FSTATUS: selectedOption ? selectedOption.value : "",
                                                                    }));
                                                                }}
                                                            />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div className="col-lg-12 mt -2">
                                            <div className="text-end">
                                                <div className="row justify-content-end">
                                                    {/* Save Button - First on mobile, second on desktop */}
                                                    <div className="col-12 col-md-1 mb-2 order-1 order-md-2">
                                                        <button
                                                            type="submit"
                                                            className="btn btn-submit w-100"
                                                            ref={saveRef}
                                                        >
                                                            {isRestricted ? "Save" : "सेव्ह"}
                                                        </button>
                                                    </div>
                                                    <div className="col-12 col-md-1 mb-2 order-2 order-md-1">
                                                        <button
                                                            type="button"
                                                            className="btn btn-cancel w-100"
                                                            onClick={showExitAlert}
                                                        >
                                                            {isRestricted ? "Back" : "मागे"}
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </form>
            </div >
        </div >
    );
};
export default AddFarmers;



