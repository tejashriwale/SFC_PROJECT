


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
import { ACSPLGUID, baseUrl, convertToISODate } from "../../core/json/custom";

import {
    PlusCircle,
    Trash2,
    Edit
} from "feather-icons-react/build/IconComponents";
import { getUserData } from "../../Context/UserData";
import Swal from "sweetalert2";
import withReactContent from "sweetalert2-react-content";
import { FaMapMarkerAlt } from "react-icons/fa";


const AddVistorApproval = ({ VAID, onRefresh }) => {
    const [uploadMessage, setUploadMessage] = useState("");
    const [messageColor, setMessageColor] = useState("green");
    const [previewModalOpen, setPreviewModalOpen] = useState(false);
    const [filePreviews, setFilePreviews] = useState([]);
    const fileInputRef = useRef(null);
    const cropTypeRef = useRef(null);
    const cropvarietyRef = useRef(null);
    const cropareaRef = useRef(null);
    const expectedharvestingdateRef = useRef(null);
    const expectedproductionRef = useRef(null);
    const rateRef = useRef(null);
    const advancemoneyRef = useRef(null);
    const contactnumberRef = useRef(null);
    const farmernameRef = useRef(null);
    const visitingdateRef = useRef(null);
    const locationRef = useRef(null);
    const GUID = ACSPLGUID.getNew();
    const MySwal = withReactContent(Swal);
    const { userdetail } = getUserData();
    const [cropType, setCropType] = useState([]);
    const [masterData, setMasterData] = useState({
        vaid: "",
        farmername: "",
        faid: "",
        visitingdate: "",
        phonenumber: "",
        location: "",
    });
    const handleMasterInputChange = (e) => {
        const { name, value } = e.target;
        setMasterData((prevData) => ({
            ...prevData,
            [name]: value,
        }));
        if (name === "phonenumber" && value.length === 10) {
            fetchFarmerName(value);
        }
    };
    const [tableData, setTableData] = useState([]);
    const [formData, setFormData] = useState({
        cropType: "",
        cropvariety: "",
        croparea: "",
        expectedharvestingdate: "",
        expectedproduction: "0",
        photoes: [],
        rate: "0",
        advancemoney: "0",
        isdeleted: 0,
    });
    const handleDetailInputChange = (e) => {
        const { name, value } = e.target;

        setFormData((prev) => ({
            ...prev,
            [name]: value,
        }));
    };
    const handleFileChange = async (e) => {
        const selectedFiles = Array.from(e.target.files);
        if (!selectedFiles.length) return;
        const newFileNames = [];
        const newPreviews = [];
        const existingCount = formData.photoes.length;
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
        setFormData((prev) => ({
            ...prev,
            photoes: Array.isArray(prev.photoes)
                ? [...prev.photoes, ...newFileNames]
                : newFileNames,
        }));
        if (fileInputRef.current) {
            fileInputRef.current.value = "";
        }
    };

    const handleViewUploadedFiles = () => {
        if (!formData.photoes || formData.photoes.length === 0) return;
        const previews = formData.photoes.map((fileName) => {
            const url = `${baseUrl.Url}/Images/${fileName}`; // adjust path if needed
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
            setUploadMessage("फोटो यशस्वीरित्या अपलोड झाला!");
            setMessageColor("green");
            setTimeout(() => {
                setUploadMessage("");
            }, 1000);
        } catch (error) {
            console.error("❌ Upload Error:", error.message);
            setUploadMessage("फोटो अपलोड होताना त्रुटी आली.");
            setMessageColor("red");
        }
    };
    useEffect(() => {
        if (VAID != "") {
            const fetchVisitorData = async () => {
                try {
                    const payload1 = {
                        "companyid": userdetail?.companyID ? userdetail.companyID : "",
                        "deptid": userdetail?.departmentID ? userdetail.departmentID : "",
                        "vaid": VAID,
                        "keyword": "%"
                    };
                    const headers = {
                        "Content-Type": "application/json",
                        Accept: "*/*"
                    };
                    const response = await axios.post(
                        baseUrl.Url + "/backend/api/GET_VisitorData",
                        payload1,
                        { headers }
                    );
                    if (response.status !== 200) throw new Error("Failed to fetch data");
                    let apiData = response.data[0];
                    setMasterData((prev) => ({
                        ...prev,
                        vaid: apiData.vaid || "",
                        farmername: apiData.fname || "",
                        visitingdate: convertToISODate(apiData.visitingdate) || "",
                        phonenumber: apiData.phonenumber || "",
                        location: apiData.location || "",
                    }));
                    if (response.data.length > 0) {
                        const mappedProducts = response.data.map((item) => ({
                            vdaid: item.vdaid,
                            cropType: item.croptype,
                            cropvariety: item.cropvariety,
                            croparea: item.croparea,
                            expectedharvestingdate: convertToISODate(item.expectedharvestingdate),
                            expectedproduction: item.expectedproduction,
                            photoes: item.photoes ? item.photoes.split(",") : [],
                            rate: item.rate,
                            advancemoney: item.advancemoney,
                            isdeleted: item.isdeleted,
                        }));
                        console.log("Mapped Table Data on proceeedddddd:", mappedProducts);
                        console.log("Mapped Table Data on proceeedddddd:", mappedProducts);
                        setTableData(mappedProducts);
                        console.log('fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff', mappedProducts)
                    }
                    console.log("Get_VisitorData  Data:", masterData);
                } catch (error) {
                    console.error("Error in Get_VisitorData API Call:", error);
                }
            };
            fetchVisitorData();
        }
    }, [VAID]);

    const fetchFarmerName = async (phonenumber) => {
        try {
            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };
            const payload = {
                fcontactno: phonenumber
            };
            const response = await axios.post(`${baseUrl.Url}/backend/api/GET_FARMERFROMCONTACT`, payload, {
                headers,
            });
            if (response.status !== 200) throw new Error("Failed to fetch farmer name");
            const data = response.data;
            if (data && data.length > 0) {
                setMasterData((prev) => ({
                    ...prev,
                    farmername: data[0].fname,
                    faid: data[0].faid,
                }));
            } else {
                setMasterData((prev) => ({
                    ...prev,
                    farmername: "",
                }));
            }
        } catch (error) {
            console.error("Error fetching farmer name:", error);
            setMasterData((prev) => ({
                ...prev,
                farmername: "त्रुटी आली",
            }));
        }
    };
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
    const addRecord = (e) => {
        e.preventDefault();
        if (!formData.cropType) {
            cropTypeRef.current.focus();
            Swal.fire({
                icon: "error",
                title: "Crop Type Validation Error",
                text: "Please select a crop type.",
                confirmButtonText: "OK",
                allowOutsideClick: false,
                allowEscapeKey: false
            })
            return;
        }
        if (!formData.cropvariety || !/^[A-Za-z][A-Za-z\s]*$/.test(formData.cropvariety)) {
            cropvarietyRef.current.focus();
            Swal.fire({
                icon: "error",
                title: "Crop Variety Validation Error",
                text: " Please Enter The Crop Area. Only Charcter will Accept. Remove Space from Start",
                confirmButtonText: "OK",
                allowOutsideClick: false,
                allowEscapeKey: false
            })
            return;
        }
        if (!formData.croparea || !/^[1-9]\d*$/.test(formData.croparea)) {
            cropareaRef.current.focus();
            Swal.fire({
                icon: "error",
                title: "Crop Area Validation Error",
                text: "Please enter numbers only, and the value must be greater than 0",
                confirmButtonText: "OK",
                allowOutsideClick: false,
                allowEscapeKey: false
            });
            return;
        }
        if (!formData.expectedharvestingdate) {
            expectedharvestingdateRef.current.focus();
            Swal.fire({
                icon: "error",
                title: "E.H.Date Validation Error",
                text: "Please select a Expected Harvesting Date.",
                confirmButtonText: "OK",
                allowOutsideClick: false,
                allowEscapeKey: false
            })
            return;
        }
        if (!/^[0-9]\d*$/.test(formData.expectedproduction.trim() === "" ? (formData.expectedproduction = "0") : formData.expectedproduction.trim())) {
            expectedproductionRef.current.focus();
            Swal.fire({
                icon: "error",
                title: "Expected Production Validation Error",
                text: "Please enter numbers only",
                confirmButtonText: "OK",
                allowOutsideClick: false,
                allowEscapeKey: false
            });
            return;
        }
        if (!formData.photoes || formData.photoes.length === 0) {
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
        if (formData.rate && !/^[0-9]+$/.test(String(formData.rate).trim())) {
            rateRef.current.focus();
            Swal.fire({
                icon: "error",
                title: "Rate Validation Error",
                text: "Please enter numbers only. No spaces are allowed in the number.",
                confirmButtonText: "OK",
                allowOutsideClick: false,
                allowEscapeKey: false
            });
            return;
        }
        if (formData.advancemoney && !/^[0-9]+$/.test(String(formData.advancemoney).trim())) {
            advancemoneyRef.current.focus();
            Swal.fire({
                icon: "error",
                title: "Advance Amount Validation Error",
                text: "Please enter numbers only. No spaces are allowed in the number.",
                confirmButtonText: "OK",
                allowOutsideClick: false,
                allowEscapeKey: false
            });
            return;
        }
        let updatedTableData;
        if (formData.vdaid) {
            updatedTableData = tableData.map((item) =>
                item.vdaid === formData.vdaid ? { ...item, ...formData, isdeleted: 0 } : item
            );
        } else {
            updatedTableData = [...tableData, { ...formData, vdaid: ACSPLGUID.getNew(), isdeleted: 0 }];
        }
        setTableData(updatedTableData);
        setFormData({
            cropType: "",
            cropvariety: "",
            croparea: "",
            expectedharvestingdate: "",
            expectedproduction: "0",
            photoes: [],
            rate: "0",
            advancemoney: "0",
        });
    };
    const handleSubmit = (e) => {
        e.preventDefault(e);
        console.log("masterdata before save11111111111111111111111111111111111111111111111", masterData);
        console.log("formData before save222222222222222222222222222222222222222222222", tableData);
        showConfirmationAlert(e);
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
        if (!masterData.phonenumber || !/^[0-9]{10}$/.test(masterData.phonenumber.trim())) {
            Swal.fire({
                icon: "error",
                title: "Contact number Validation Error",
                text: "Enter The 10 Digits Only",
                allowOutsideClick: false,
                allowEscapeKey: false
            }).then(() => {
                contactnumberRef.current.focus();
            });
            return;
        }
        if (!masterData.farmername) {
            Swal.fire({
                icon: "error",
                title: "Farmer Name Validation Error",
                text: "Enter Farmer Name. Only Character will Accept",
                allowOutsideClick: false,
                allowEscapeKey: false
            }).then(() => {
                farmernameRef.current.focus();
            });
            return;
        }
        if (!masterData.location) {
            Swal.fire({
                icon: "error",
                title: "Location Validation Error",
                text: "Please Click On location Button.",
                allowOutsideClick: false,
                allowEscapeKey: false
            }).then(() => {
                locationRef.current.focus();
            });
            return;
        }
        handleSubmit(event);
    };
    const showConfirmationAlert = (event) => {
        MySwal.fire({
            title: "आपण खात्रीशीर आहात का?",
            text: "आपल्याला ही माहिती जतन करायची आहे का?",
            showCancelButton: true,
            confirmButtonColor: "#00ff00",
            confirmButtonText: "जतन करा",
            cancelButtonColor: "#092C4C",
            cancelButtonText: "रद्द करा",
        })
            .then((result) => {
                if (result.isConfirmed) {
                    handlePayloadSubmition(event);
                }
            });
    };
    const handlePayloadSubmition = async () => {
        console.log("table Data Submitted from  handlePayloadSubmition:", tableData);
        console.log("Master part Submitted from handlePayloadSubmition:", masterData);
        let FFAID = ACSPLGUID.getNew();
        try {
            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };
            const farmercheck = {
                fcontactno: masterData.phonenumber
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
                    fname: masterData?.farmername || "",
                    fcontactno: masterData?.phonenumber || "",
                    uaid: userdetail?.uaid ? userdetail.uaid : "",
                    date: userdetail.APPDT,
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
                    fcroparea: "",
                    fcroptype: "",
                    fstatus: "1",
                    fprofile: "",
                    companyid: "",
                    deptid: "",
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
            console.log(FFAID, "FFAIDFFAID");
            const payload1 = {
                vaid: masterData.vaid ? masterData.vaid : ACSPLGUID.getNew(),
                farmername: FFAID || "",
                visitingdate: masterData.visitingdate || new Date().toISOString().split('T')[0],
                phonenumber: masterData.phonenumber || "",
                location: masterData.location || "",
                uaid: userdetail?.uaid ? userdetail.uaid : "",
                companyid: userdetail?.companyID ? userdetail.companyID : "",
                deptid: userdetail?.departmentID ? userdetail.departmentID : "",
            };
            console.log("Payload1 after processing arrays:", payload1);
            await axios({
                method: "POST",
                url: baseUrl.Url + "/backend/api/SP_AddUpdVisitorMaster",
                data: JSON.stringify(payload1),
                headers: headers,
            });
            const Payload2 = tableData.map((detail) => ({
                vdaid: detail.vdaid ? detail.vdaid : ACSPLGUID.getNew(),
                vaid: payload1.vaid || masterData.vaid,
                cropType: detail.cropType || "",
                cropvariety: detail.cropvariety || "",
                croparea: detail.croparea || "0",
                expectedharvestingdate: detail.expectedharvestingdate || "",
                expectedproduction: detail.expectedproduction || "0",
                photoes: Array.isArray(detail.photoes) ? detail.photoes.join(",") : detail.photoes || "",
                rate: parseInt(detail.rate) || 0,
                advancemoney: parseInt(detail.advancemoney) || 0,
                isdeleted: detail.isdeleted === 1 || detail.isdeleted === true ? true : false,
                uaid: userdetail?.uaid ? userdetail.uaid : "",
                companyid: userdetail?.companyID ? userdetail.companyID : "",
                deptid: userdetail?.departmentID ? userdetail.departmentID : "",
            }));
            console.log("Payload2:", Payload2);
            await axios({
                method: "POST",
                url: baseUrl.Url + "/backend/api/SP_AddUpdVisitorDetail",
                data: JSON.stringify(Payload2),
                headers: headers,
            });
            Swal.fire({
                icon: "success",
                title: "Saved Successfully",
                text: "Data has been saved successfully.",
            }).then(() => {
                const modal = document.getElementById("AddVisitor");
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
                    ...masterData,
                    vaid: "",
                    farmername: "",
                    faid: "",
                    visitingdate: "",
                    phonenumber: "",
                    location: "",
                });
                setFormData({
                    ...formData,
                    cropType: "",
                    cropvariety: "",
                    croparea: "",
                    expectedharvestingdate: "",
                    expectedproduction: "",
                    photoes: "",
                    rate: "",
                    advancemoney: "",
                });
                setTableData([]);
                if (onRefresh) {
                    onRefresh();
                }
            });
        }
        catch (error) {
            console.error("Submission Error:", error);
            Swal.fire({
                icon: "error",
                title: "Error",
                text: "Failed to save data. Please try again.",
            });
        }
    };
    const handleEdit = (vdaid) => {
        const filteredProducts = tableData.filter((data) => data.vdaid == vdaid);
        setFormData({
            vdaid: filteredProducts[0].vdaid,
            cropType: filteredProducts[0].cropType,
            cropvariety: filteredProducts[0].cropvariety,
            croparea: filteredProducts[0].croparea,
            expectedharvestingdate: filteredProducts[0].expectedharvestingdate,
            expectedproduction: filteredProducts[0].expectedproduction,
            photoes: filteredProducts[0].photoes,
            rate: filteredProducts[0].rate,
            advancemoney: filteredProducts[0].advancemoney,
            isdeleted: 0,
        });
    }

    const handleDelete = (vdaid) => {
        Swal.fire({
            title: "तुम्हाला खात्री आहे का?",
            text: "तुम्ही हे बदलू शकणार नाही!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#d33",
            cancelButtonColor: "#3085d6",
            confirmButtonText: "हो, ते हटवा!",
            cancelButtonText: "रद्द करा",
        }).then((result) => {
            if (result.isConfirmed) {
                setTableData((prevData) => {
                    const updatedData = prevData.map((data) =>
                        data.vdaid === vdaid
                            ? { ...data, isdeleted: 1 } : data
                    );
                    console.log("Updated Table Data:", updatedData);
                    return [...updatedData];
                });
                Swal.fire({
                    icon: "success",
                    title: "हटवले!",
                    text: "रेकॉर्ड हटवले गेले आहे!",
                    confirmButtonText: "ठीक आहे",
                    allowOutsideClick: false,
                    allowEscapeKey: false
                });
            }
        });
    };
    const showExitAlert = () => {
        MySwal.fire({
            title: "Are you sure?",
            text: "Do you want to go back?",
            showCancelButton: true,
            confirmButtonColor: "#00ff00",
            confirmButtonText: "Yes",
            cancelButtonColor: "#092C4C",
            cancelButtonText: "No",
        }).then((result) => {
            if (result.isConfirmed) {
                const modal = document.getElementById("AddVisitor");
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
                    ...masterData,
                    vaid: "",
                    farmername: "",
                    faid: "",
                    visitingdate: "",
                    phonenumber: "",
                    location: "",
                });
                setFormData({
                    ...formData,
                    cropType: "",
                    cropvariety: "",
                    croparea: "",
                    expectedharvestingdate: "",
                    expectedproduction: "0",
                    photoes: "",
                    rate: "0",
                    advancemoney: "0",
                });
                setTableData([]);
                if (onRefresh) {
                    onRefresh();
                }
            }
        });
    };

    const takeConfirmation = () => {
        MySwal.fire({
            title: "Are you sure?",
            text: "Do you want to go back without saving the data?",
            showCancelButton: true,
            confirmButtonColor: "#00ff00",
            confirmButtonText: "Yes",
            cancelButtonColor: "#092C4C",
            cancelButtonText: "No",
        }).then((result) => {
            if (result.isConfirmed) {
                const modal = document.getElementById("AddVisitor");
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
                    ...masterData,
                    vaid: "",
                    farmername: "",
                    faid: "",
                    visitingdate: "",
                    phonenumber: "",
                    location: "",
                });
                setFormData({
                    ...formData,
                    cropType: "",
                    cropvariety: "",
                    croparea: "",
                    expectedharvestingdate: "",
                    expectedproduction: "0",
                    photoes: "",
                    rate: "0",
                    advancemoney: "0",
                });
                setTableData([]);
                if (onRefresh) {
                    onRefresh();
                }
            }
        });
    };
    const getLiveLocation = () => {
        if (!navigator.geolocation) {
            alert("Geolocation not supported.");
            return;
        }
        navigator.geolocation.getCurrentPosition(
            async (position) => {
                const { latitude, longitude } = position.coords;
                console.log("Coordinates:", latitude, longitude);
                const apiKey = "AIzaSyDGJJqkGy8Wp1FpnP24zrY1TMyUcJfYVYs"; // Your API Key
                const url = `https://maps.googleapis.com/maps/api/geocode/json?latlng=${latitude},${longitude}&key=${apiKey}`;
                try {
                    const res = await fetch(url);
                    const data = await res.json();
                    if (data.status === "OK" && data.results.length > 0) {
                        const address = data.results[0].formatted_address;
                        setMasterData((prev) => ({
                            ...prev,
                            location: address,
                        }));
                        console.log("Address set:", address);
                    } else {
                        console.warn("No results found", data);
                        alert("Could not retrieve a valid address.");
                    }
                } catch (err) {
                    console.error("Error fetching address:", err);
                    alert("Failed to convert coordinates to address.");
                }
            },
            (error) => {
                console.error("Location access denied or unavailable:", error);
                alert("Unable to get your location. Please allow GPS access.");
            },
            {
                enableHighAccuracy: true,
                timeout: 10000,
                maximumAge: 0,
            }
        );
    };
    const handleRateChange = (e, vdaid) => {
        const newValue = e.target.value;
        setTableData(prev =>
            prev.map(row =>
                row.vdaid === vdaid ? { ...row, rate: newValue } : row
            )
        );
    };
    const handleAdvanceChange = (e, vdaid) => {
        const newValue = e.target.value;
        setTableData(prev =>
            prev.map(row =>
                row.vdaid === vdaid ? { ...row, advancemoney: newValue } : row
            )
        );
    };

    return (
        <div>
            <div className="modal fade" id="AddVisitor">
                <div className="modal-dialog modal-dialog-centered modal-fullscreen mbgcolor">
                    <div className="modal-content mbgcolor">
                        <div className="page-wrapper-new p-0">
                            <div className="content">
                                <div className="modal-header border-0 custom-modal-header">
                                    <div className="page-title">
                                        <h4>Visitors Approval</h4>
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
                                            onClick={() => {
                                                console.log("tableData length:", tableData.length);
                                                if (tableData.length > 0) {
                                                    takeConfirmation();
                                                } else {
                                                    showExitAlert();
                                                }
                                            }}
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
                                        <div className="row">
                                            <div className="col-lg-2 col-sm-6 col-12">
                                                <div >
                                                    <label className="form-label required">Mobile</label>
                                                    <input
                                                        type="text"
                                                        className="form-control"
                                                        id="phonenumber"
                                                        name="phonenumber"
                                                        value={masterData.phonenumber}
                                                        onChange={handleMasterInputChange}
                                                        placeholder="Enter Mobile Number"
                                                        required
                                                        pattern="^\d{10}$"
                                                        title="Mobile Number Required"
                                                        ref={contactnumberRef}
                                                        autoFocus
                                                        readOnly
                                                    />
                                                </div>
                                            </div>
                                            <div className="col-lg-4 col-sm-6 col-12">
                                                <div >
                                                    <label className="form-label required">Farmer Name</label>
                                                    <input
                                                        type="text"
                                                        className="form-control"
                                                        id="farmername"
                                                        name="farmername"
                                                        value={masterData.farmername}
                                                        onChange={handleMasterInputChange}
                                                        placeholder="Enter Farmer Name"
                                                        ref={farmernameRef}
                                                        required
                                                        pattern="^[A-Za-z][A-Za-z\s]*$"
                                                        title="Please enter a valid name using only letters.Remove space From Start"
                                                        readOnly
                                                    />
                                                </div>
                                            </div>
                                        </div>
                                        <div className="row">
                                            <div className="col-lg-2 col-sm-6 col-12">
                                                <div >
                                                    <label className="form-label required">Visiting Date</label>
                                                    <input
                                                        type="date"
                                                        className="form-control"
                                                        id="visitingdate"
                                                        name="visitingdate"
                                                        value={masterData.visitingdate ? masterData.visitingdate : new Date().toISOString().split('T')[0]}
                                                        onChange={handleMasterInputChange}
                                                        readOnly
                                                        ref={visitingdateRef}
                                                    />
                                                </div>
                                            </div>
                                            <div className="col-lg-10 col-sm-6 col-12">
                                                <label className="form-label required">Location</label>
                                                <div className="input-group">
                                                    <input
                                                        type="text"
                                                        className="form-control"
                                                        id="location"
                                                        name="location"
                                                        value={masterData.location}
                                                        onChange={handleMasterInputChange}
                                                        placeholder="Enter Location"
                                                        required
                                                        title="Location  "
                                                        ref={locationRef}
                                                        readOnly
                                                    />
                                                    <button
                                                        type="button"
                                                        className="btn-secondary"
                                                        onClick={getLiveLocation}
                                                        title="Get Live Location"
                                                    >
                                                        Location
                                                        <FaMapMarkerAlt size={25} />
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                        <div className="border p-3 rounded shadow-sm mt-2 mb-2">
                                            <div className="col-lg-12">
                                                <div className="modal-body-table responsive-no-scroll" >
                                                    <div className="table-responsive">
                                                        <table className="table table-bordered table-striped mb-0">
                                                            <thead className="thead-dark bg-white" >
                                                                <tr>
                                                                    <th >Crop</th>
                                                                    <th >C.Area</th>
                                                                    <th >E.H.Date</th>
                                                                    <th >Rate</th>
                                                                    <th >Advance</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                {tableData.filter((data) => !data.isdeleted).length > 0 ? (
                                                                    tableData
                                                                        .filter((data) => !data.isdeleted)
                                                                        .map((data, index) => (
                                                                            <tr key={data.vdaid || index}>
                                                                                <td >
                                                                                    {cropType.find(option => option.value === data.cropType)?.label || "Not Found"}
                                                                                </td>
                                                                                <td >{data.croparea}</td>
                                                                                <td>
                                                                                    {data.expectedharvestingdate
                                                                                        ? new Date(data.expectedharvestingdate).toLocaleDateString('en-GB', {
                                                                                            day: '2-digit',
                                                                                            month: 'short',
                                                                                            year: 'numeric',
                                                                                        })
                                                                                        : ""}
                                                                                </td>
                                                                                <td className="col-1" >
                                                                                    <input
                                                                                        type="number"
                                                                                        name="rate"
                                                                                        className="form-control text-center"
                                                                                        value={data.rate}
                                                                                        onChange={(e) => handleRateChange(e, data.vdaid)}
                                                                                    />
                                                                                </td>
                                                                                <td className="col-1">
                                                                                    <input
                                                                                        type="number"
                                                                                        name="advancemoney"
                                                                                        className="form-control text-center"
                                                                                        value={data.advancemoney}
                                                                                        onChange={(e) => handleAdvanceChange(e, data.vdaid)}
                                                                                    />
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
                                                            onClick={() => {
                                                                console.log("tableData length:", tableData.length);
                                                                if (tableData.length > 0) {
                                                                    takeConfirmation();
                                                                } else {
                                                                    showExitAlert();
                                                                }
                                                            }}
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
export default AddVistorApproval;

