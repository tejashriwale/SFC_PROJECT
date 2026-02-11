


import React, { useRef, useState, useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";

import Select from "react-select";
import "bootstrap/dist/css/bootstrap.min.css";
import {
    ArrowLeft,
    FilePlus
} from "feather-icons-react/build/IconComponents";
// import Swal from "sweetalert2";
// import withReactContent from "sweetalert2-react-content";
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


const AddVisitorMaster = ({ VAID, onRefresh }) => {

    console.log("VAID from Edit", VAID);

    ///////

    const [uploadMessage, setUploadMessage] = useState("");
    const [messageColor, setMessageColor] = useState("green");
    const [previewModalOpen, setPreviewModalOpen] = useState(false);
    const [filePreviews, setFilePreviews] = useState([]);
    // const [sessionGUID] = useState(ACSPLGUID.getNew());

    const fileInputRef = useRef(null);

    ///////

    const cropTypeRef = useRef(null);
    const cropvarietyRef = useRef(null);
    const cropareaRef = useRef(null);
    const expectedharvestingdateRef = useRef(null);
    const expectedproductionRef = useRef(null);
    const rateRef = useRef(null);
    const advancemoneyRef = useRef(null);

    const farmernameRef = useRef(null);
    const visitingdateRef = useRef(null);
    const phonenumberRef = useRef(null);
    const locationRef = useRef(null);


    const GUID = ACSPLGUID.getNew();
    const MySwal = withReactContent(Swal);
    const { userdetail } = getUserData();
    // const [farmer, setFarmer] = useState([]);
    // const [farmerOptions, setFarmerOptions] = useState([]); // only label, value for select

    const [cropType, setCropType] = useState([]);

    const [masterData, setMasterData] = useState({
        vaid: "",
        farmername: "",
        faid: "",
        visitingdate: "",
        phonenumber: "",
        location: "",
    });
    // For master input
    const handleMasterInputChange = (e) => {
        const { name, value } = e.target;

        setMasterData((prevData) => ({
            ...prevData,
            [name]: value,
        }));

        // function run when 10 digit typing complete will complete.
        if (name === "phonenumber" && value.length === 10) {
            fetchFarmerName(value); // call function when mobile number is 10 digits
        }

    };

    const [tableData, setTableData] = useState([]);
    const [formData, setFormData] = useState({
        cropType: "",
        cropvariety: "",
        croparea: "",
        expectedharvestingdate: "",
        expectedproduction: "",
        photoes: [],
        rate: "",
        advancemoney: "",
        isdeleted: 0,
    });

    // For Detail Inputes
    const handleDetailInputChange = (e) => {
        const { name, value } = e.target;

        setFormData((prev) => ({
            ...prev,
            [name]: value, // ✅ Always store as string
        }));
    };


    // images upload function //////////////////////////////////////////////////////////////////

    const handleFileChange = async (e) => {
        const selectedFiles = Array.from(e.target.files);
        if (!selectedFiles.length) return;

        const newFileNames = [];
        const newPreviews = [];
        const existingCount = formData.photoes.length; // already uploaded files

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

        // // ✅ Clear the input after upload
        // if (fileInputRef.current) {
        //     fileInputRef.current.value = "";
        // }
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
                setUploadMessage(""); // clear message after 2 seconds
            }, 1000);

        } catch (error) {
            console.error("❌ Upload Error:", error.message);
            setUploadMessage("फोटो अपलोड होताना त्रुटी आली.");
            setMessageColor("red");
        }
    };



    // images upload end  //////////////////////////////////////////////////////////////////////

    // To fetch and Set VisitorData By Edit
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
                        // farmername: apiData.farmername || "",
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
                            // photoes: item.photoes,
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

    //fetch farmer from contactnumber
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


    // //Farmer names
    // useEffect(() => {
    //     const fetchFarmer = async () => {
    //         try {
    //             const headers = {
    //                 "Content-Type": "application/json",
    //                 Accept: "*/*",
    //             };

    //             const payload = { companyid: "" };

    //             const response = await axios.post(
    //                 `${baseUrl.Url}/backend/api/GET_FarmerName`,
    //                 payload,
    //                 { headers }
    //             );

    //             if (response.status !== 200)
    //                 throw new Error("Failed to fetch Farmer data");

    //             console.log("Farmer Detail", response.data);

    //             // Store full farmer data for lookup (with phone)
    //             setFarmer(response.data);

    //             // Prepare options only for Select input
    //             const options = response.data.map(({ fname, faid }) => ({
    //                 label: fname,
    //                 value: faid,
    //             }));

    //             setFarmerOptions(options);  // <-- You need to create this new state

    //         } catch (error) {
    //             console.error("Error fetching Farmer data:", error);
    //         }
    //     };
    //     fetchFarmer();
    // }, []);


    // Fetch All Crops
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

                // setCrop_TYPE(implicationsDropdown);
                // if (formData.cropType != "") {
                //     setFormData(prev => ({
                //         ...prev,
                //         cropType: implicationsDropdown.find(item => item.value === formData.cropType)?.value || '',
                //     }));
                // }
            } catch (error) {
                console.error("Error fetching Farmer data:", error);
            }
        };

        fetchImplications();
    }, []);

    // Add row in details
    const addRecord = (e) => {
        e.preventDefault();

        // Validate each field and add error alert for empty fields or invalid patterns
        if (!formData.cropType) {
            cropTypeRef.current.focus();
            Swal.fire({
                icon: "error",
                title: "पीक प्रकार पडताळणी त्रुटी",
                text: "कृपया पीक प्रकार निवडा.",
                confirmButtonText: "ठीक आहे",
                allowOutsideClick: false,
                allowEscapeKey: false
            })
            return;
        }

        if (!formData.cropvariety || !/^[A-Za-z][A-Za-z\s]*$/.test(formData.cropvariety)) {
            cropvarietyRef.current.focus();

            Swal.fire({
                icon: "error",
                title: "पीक वान/जात पडताळणी त्रुटी",
                text: "कृपया पीक वान/जात प्रविष्ट करा. फक्त अक्षर प्रविष्ट करा सुरुवातीचा स्पेस काढा",
                confirmButtonText: "ठीक आहे",
                allowOutsideClick: false,
                allowEscapeKey: false
            })
            return;
        }

        // if (!/^[0-9]\d*$/.test(formData.croparea.trim() === "" ? (formData.croparea = "0") : formData.croparea.trim())) {

        if (!formData.croparea || !/^[1-9]\d*$/.test(formData.croparea)) {
            cropareaRef.current.focus();
            Swal.fire({
                icon: "error",
                title: "पीक क्षेत्र पडताळणी त्रुटी",
                text: "कृपया फक्त अंक प्रविष्ट करा आणि 0 पेक्षा मोठा असावा",
                confirmButtonText: "ठीक आहे",
                allowOutsideClick: false,
                allowEscapeKey: false
            });
            return;
        }

        if (!/^[0-9]\d*$/.test(formData.expectedproduction.trim() === "" ? (formData.expectedproduction = "0") : formData.expectedproduction.trim())) {
            expectedproductionRef.current.focus();
            Swal.fire({
                icon: "error",
                title: "अपेक्षित उत्पादन पडताळणी त्रुटी",
                text: "कृपया फक्त अंक प्रविष्ट करा.",
                confirmButtonText: "ठीक आहे",
                allowOutsideClick: false,
                allowEscapeKey: false
            });
            return;
        }
        if (!formData.photoes) {
            // ephotoesRef.current.focus();
            Swal.fire({
                icon: "error",
                title: "फोटो पडताळणी त्रुटी",
                text: "फोटो कॅप्चर करा कीव्हा अपलोड करा.",
                confirmButtonText: "ठीक आहे",
                allowOutsideClick: false,
                allowEscapeKey: false
            });
            return;
        }



        // if (!/^[0-9]\d*$/.test(formData.rate.trim() === "" ? (formData.rate = "0") : formData.rate.trim())) 
        if (formData.rate && !/^[0-9]+$/.test(String(formData.rate).trim())) {
            rateRef.current.focus();
            Swal.fire({
                icon: "error",
                title: "भाव पडताळणी त्रुटी",
                text: "कृपया फक्त अंक प्रविष्ट करा. अंकांमध्ये स्पेस नसावा.",
                confirmButtonText: "ठीक आहे",
                allowOutsideClick: false,
                allowEscapeKey: false
            });
            return;
        }


        // if (!/^[0-9]\d*$/.test(formData.advancemoney.trim() === "" ? (formData.advancemoney = "0") : formData.advancemoney.trim())) {
        if (formData.advancemoney && !/^[0-9]+$/.test(String(formData.advancemoney).trim())) {
            advancemoneyRef.current.focus();
            Swal.fire({
                icon: "error",
                title: "अडवांस पडताळणी त्रुटी",
                text: "कृपया फक्त अंक प्रविष्ट करा.  अंकान मध्ये स्पेस नसावा.",
                confirmButtonText: "ठीक आहे",
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
        // Add the current formData to tableData
        // setTableData((prevTableData) => [...prevTableData, formData]);

        // Reset formData to clear the form, including dropdowns
        setFormData({
            cropType: "",
            cropvariety: "",
            croparea: "",
            expectedharvestingdate: "",
            expectedproduction: "",
            photoes: [],
            rate: "",
            advancemoney: "",

        });

    };

    // on save button get triggred
    const handleSubmit = (e) => {
        e.preventDefault(e);
        console.log("masterdata before save11111111111111111111111111111111111111111111111", masterData);
        console.log("formData before save222222222222222222222222222222222222222222222", tableData);

        // // validation of detail table
        // const hasValidRowExist = tableData.some(row => row.isdeleted === 0);
        // if (!hasValidRowExist) {
        //     Swal.fire({
        //         icon: "error",
        //         title: "तपशील टेबल प्रमाणीकरण त्रुटी",
        //         text: "किमान एक तरी माहिती खालील टेबल मध्ये भरा"
        //     });
        //     return;
        // }
        showConfirmationAlert(e);
    };

    // Handle keyboard shortcuts with validation
    useEffect(() => {
        const handleShortcut = (e) => {
            if (e.ctrlKey && (e.key === 'e' || e.key === 'E')) {
                e.preventDefault();
                showExitAlert();
            }
            if (e.ctrlKey && (e.key === 's' || e.key === 'S')) {
                e.preventDefault();
                // validateinput();
            }
        };

        window.addEventListener('keydown', handleShortcut);

        return () => {
            window.removeEventListener('keydown', handleShortcut);
        };
    }, [masterData, handleSubmit]);

    // // validation on C+S only for master inputs
    // const validateinput = (e) => {
    //     const { farmername, location } = masterData;

    //     if (!farmername) {
    //         Swal.fire({
    //             icon: "error",
    //             title: "प्रमाणीकरण त्रुटी",
    //             text: "कृपया शेतकरी सिलेक्ट करा.",
    //             allowOutsideClick: false,
    //             allowEscapeKey: false
    //         }).then(() => {
    //             farmernameRef.current.focus();
    //         });
    //         return;
    //     }
    //     if (!location) {
    //         Swal.fire({
    //             icon: "error",
    //             title: "प्रमाणीकरण त्रुटी",
    //             text: "कृपया लोकेशन प्रविष्ट करा.",
    //             allowOutsideClick: false,
    //             allowEscapeKey: false
    //         }).then(() => {
    //             locationRef.current.focus();
    //         });
    //         return;
    //     }

    //     // तपशील टेबलमध्ये नोंदी आहेत का ते तपासा
    //     // validation of detail table
    //     // const hasValidRowExist = tableData.some(row => row.isdeleted === 0);
    //     // if (!hasValidRowExist) {
    //     //     Swal.fire({
    //     //         icon: "error",
    //     //         title: "तपशील टेबल प्रमाणीकरण त्रुटी",
    //     //         text: "कृपया सेव्ह करण्यापूर्वी किमान टेबल मध्ये एक माहिती  जोडा"
    //     //     });
    //     //     return;
    //     // }
    //     // if (tableData.length === 0) {
    //     //     Swal.fire({
    //     //         icon: "error",
    //     //         title: "तपशील टेबल प्रमाणीकरण त्रुटी",
    //     //         text: "कृपया सेव्ह करण्यापूर्वी किमान एक उत्पादन जोडा.",
    //     //         allowOutsideClick: false,
    //     //         allowEscapeKey: false
    //     //     });
    //     //     return;
    //     // }

    //     handleSubmit(event);
    // };


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
    ////////////////////////////////////////////////

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
                // farmername: masterData.farmername || "",
                // visitingdate: masterData.visitingdate || "",
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


            // Prepare payload for detail data
            const Payload2 = tableData.map((detail) => ({

                vdaid: detail.vdaid ? detail.vdaid : ACSPLGUID.getNew(),
                vaid: masterData.vaid || payload1.vaid,
                cropType: detail.cropType || "",
                cropvariety: detail.cropvariety || "",
                croparea: detail.croparea || "0",
                expectedharvestingdate: detail.expectedharvestingdate || "",
                expectedproduction: detail.expectedproduction || "0",
                // photoes: detail.photoes || "",
                photoes: Array.isArray(detail.photoes) ? detail.photoes.join(",") : detail.photoes || "",
                rate: detail.rate || 0,
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

            // ✅ Show success message after both API calls succeed
            Swal.fire({
                icon: "success",
                title: "यशस्वीपणे जतन केले",
                text: "डेटा यशस्वीरित्या सेव्ह झाला आहे.",
            });
            setMasterData({
                ...masterData,
                farmername: "",
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

    // Detail Edit Function
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

    // Detail Delete Function
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
                            ? { ...data, isdeleted: 1 } : data // Otherwise, keep the data unchanged
                    );

                    console.log("Updated Table Data:", updatedData);
                    return [...updatedData];  // Ensure state updates properly
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

    // show Exit Alert
    const showExitAlert = () => {
        MySwal.fire({
            title: "तुम्हाला खात्री आहे का?",
            text: "तुम्हाला मागे जायचं आहे का?",
            showCancelButton: true,
            confirmButtonColor: "#00ff00",
            confirmButtonText: "हो",
            cancelButtonColor: "#092C4C",
            cancelButtonText: "नाही",
        }).then((result) => {
            if (result.isConfirmed) {
                const modal = document.getElementById("AddVisitor");

                if (modal) {
                    // Hide modal
                    modal.classList.remove("show");
                    modal.style.display = "none";
                    modal.setAttribute("aria-hidden", "true");

                    // Remove backdrop if exists
                    const modalBackdrop = document.querySelector(".modal-backdrop");
                    if (modalBackdrop) {
                        modalBackdrop.remove();
                    }

                    // Clear modal-related attributes (important for fresh open)
                    modal.removeAttribute("aria-modal");
                    modal.removeAttribute("role");

                    // Clean up body classes and styles
                    document.body.classList.remove("modal-open");
                    document.body.style.overflow = "";
                    document.body.style.paddingRight = "";
                }

                setMasterData({
                    ...masterData,
                    farmername: "",
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
            }
        });
    };


    const getLiveLocation = () => {
        if (!navigator.geolocation) {
            alert("आपला ब्राउझर लोकेशन सेवा समर्थित नाही.");
            return;
        }

        navigator.geolocation.getCurrentPosition(
            async (position) => {
                const { latitude, longitude } = position.coords;

                try {
                    const response = await fetch(
                        `https://us1.locationiq.com/v1/reverse?key=pk.b47cd709bbf92892678f45bba3487b42&lat=${latitude}&lon=${longitude}&format=json`
                    );

                    const data = await response.json();
                    const location = data.display_name || `${latitude}, ${longitude}`;

                    setMasterData((prev) => ({
                        ...prev,
                        location,
                    }));
                } catch (err) {
                    alert("पत्ता मिळवताना त्रुटी.");
                    console.error("Geocoding error:", err);
                }
            },
            (error) => {
                alert("लोकेशन मिळवता आले नाही. कृपया परवानगी तपासा.");
                console.error("Geolocation Error:", error);
            },
            {
                enableHighAccuracy: true,
                timeout: 10000,
                maximumAge: 0,
            }
        );
    };
    useEffect(() => {
        getLiveLocation();
    }, []);







    const reverseGeocodeWithNominatim = async (lat, lng) => {
        try {
            console.log(`Fetching address for lat: ${lat}, lng: ${lng}`);

            const response = await fetch(
                `https://nominatim.openstreetmap.org/reverse?format=jsonv2&lat=${lat}&lon=${lng}`
            );

            console.log("Raw fetch response:", response);

            const data = await response.json();

            console.log("Nominatim response data:", data);

            if (data.address) {
                // Build readable address from returned parts
                const { road, neighbourhood, suburb, city, town, state, country } = data.address;
                const addressParts = [road, neighbourhood, suburb, city || town, state, country]
                    .filter(Boolean);
                return addressParts.join(', ');
            } else {
                console.warn("No address found in response");
                return "लोकेशन सापडले नाही";
            }
        } catch (err) {
            console.error("Reverse geocoding error:", err);
            return "त्रुटी आली";
        }
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
                                        <h4>विसीटर</h4>

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
                                            मागे
                                        </Link>

                                    </div>

                                </div>
                                <div className="modal-body custom-modal-body" style={{
                                    overflow: "hidden",
                                }}>
                                    {/* onSubmit={handleSubmit} */}
                                    <form onSubmit={handleSubmit} className="mt-0">

                                        <div className="row">
                                            <div className="col-lg-2 col-sm-6 col-12">
                                                <div >
                                                    <label className="form-label required">फोन नंबर</label>
                                                    <input
                                                        type="text"
                                                        className="form-control"
                                                        id="phonenumber"
                                                        name="phonenumber"
                                                        value={masterData.phonenumber}
                                                        onChange={handleMasterInputChange}
                                                        placeholder="फोन नंबर"

                                                        ref={phonenumberRef}
                                                    />
                                                </div>
                                            </div>

                                            <div className="col-lg-4 col-sm-6 col-12">
                                                <div >
                                                    <label className="form-label required">शेतकरी नाव</label>
                                                    <input
                                                        type="text"
                                                        className="form-control"
                                                        id="farmername"
                                                        name="farmername"
                                                        value={masterData.farmername}
                                                        onChange={handleMasterInputChange}
                                                        placeholder="शेतकरी नाव"

                                                        ref={farmernameRef}
                                                    />

                                                </div>
                                            </div>
                                        </div>

                                        {/* <div className="row">
                                            <div className="col-lg-4"></div>
                                            <div className="col-lg-4 col-sm-6 col-12">
                                                <label className="form-label required">शेतकरी नाव</label>
                                                <Select
                                                    classNamePrefix="react-select"
                                                    options={farmerOptions}
                                                    value={farmerOptions.find(option => option.value === masterData.farmername) || null}
                                                    onChange={(selectedOption) => {
                                                        const selectedFarmer = farmer.find(f => f.faid === selectedOption.value);
                                                        setMasterData(prev => ({
                                                            ...prev,
                                                            farmername: selectedOption ? selectedOption.value : '',
                                                            phonenumber: selectedFarmer ? selectedFarmer.fcontactno : '',
                                                        }));
                                                    }}
                                                    placeholder="निवडा"
                                                    openMenuOnFocus={true}
                                                    required
                                                    title="शेतकरी नाव निवडा"
                                                    ref={farmernameRef}
                                                />

                                            </div>
                                        </div> */}

                                        <div className="row">

                                            <div className="col-lg-2 col-sm-6 col-12">
                                                <div >
                                                    <label className="form-label required">विसीटिंग दिनांक  </label>
                                                    <input
                                                        type="date"
                                                        className="form-control"
                                                        id="visitingdate"
                                                        name="visitingdate"
                                                        // value={(masterData.visitingdate ? new Date.toISOString().split('T')[0])}
                                                        value={masterData.visitingdate ? masterData.visitingdate : new Date().toISOString().split('T')[0]}
                                                        onChange={handleMasterInputChange}
                                                        readOnly
                                                        ref={visitingdateRef}
                                                    />
                                                </div>
                                            </div>



                                            <div className="col-lg-10 col-sm-6 col-12">
                                                <label className="form-label required">लोकेशन</label>
                                                <div className="input-group">
                                                    <input
                                                        type="text"
                                                        className="form-control"
                                                        id="location"
                                                        name="location"
                                                        value={masterData.location}
                                                        onChange={handleMasterInputChange}

                                                        placeholder="लोकेशन प्रविष्ट करा"
                                                        required
                                                        title="लोकेशन निवडा "
                                                        ref={locationRef}

                                                    />

                                                    <button
                                                        type="button"
                                                        className="btn btn-outline-primary rounded-end d-flex align-items-center gap-1"
                                                        onClick={getLiveLocation}
                                                        title="सध्याचे लोकेशन मिळवा"
                                                    >
                                                        <i className="bi bi-geo-alt-fill"></i> लोकेशन
                                                    </button>

                                                    {/* <button
                                                        type="button"
                                                        className="btn btn-outline-secondary"
                                                        onClick={getLiveLocation}
                                                    >
                                                        📍
                                                    </button> */}
                                                </div>
                                            </div>




                                        </div>


                                        {/* start (Detail) */}
                                        <div className="border p-3 rounded shadow-sm mt-2 mb-2">
                                            {/* table */}
                                            <div className="col-lg-12">

                                                <div className="modal-body-table responsive-no-scroll" >
                                                    <div className="table-responsive">
                                                        <table className="table table-bordered table-striped mb-0">
                                                            <thead className="thead-dark bg-white" >
                                                                <tr>

                                                                    <th >पिक</th>
                                                                    {/* <th >पी.जात</th> */}
                                                                    <th >पी.क्षेत्र</th>
                                                                    <th >अ.दि</th>
                                                                    {/* <th >अपेक्षित उत्पादन</th> */}
                                                                    {/* <th >फोटो</th> */}
                                                                    <th >भाव</th>
                                                                    {/* <th >अडवांस रक्कम</th> */}
                                                                    <th >कृती </th>
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
                                                                                {/* <td >{data.cropvariety}</td> */}
                                                                                <td >{data.croparea}</td>
                                                                                <td >{data.expectedharvestingdate}</td>
                                                                                {/* <td >{data.expectedproduction}</td> */}
                                                                                {/* <th >{data.photoes}</th> */}
                                                                                <td >{data.rate}</td>
                                                                                {/* <td >{data.advancemoney}</td> */}

                                                                                <td >
                                                                                    <div >
                                                                                        {/* Left half - DELETE */}
                                                                                        <div
                                                                                            onClick={() => handleDelete(data.vdaid)}
                                                                                            className="btn btn-sm"
                                                                                        >
                                                                                            <Trash2 className="text-danger" />
                                                                                        </div>

                                                                                        {/* Right half - EDIT */}
                                                                                        <div
                                                                                            onClick={() => handleEdit(data.vdaid)}
                                                                                            className="btn btn-sm"
                                                                                        >
                                                                                            <Edit className="text-primary" />
                                                                                        </div>
                                                                                    </div>
                                                                                </td>


                                                                            </tr>
                                                                        ))
                                                                ) : (
                                                                    <tr>
                                                                        <td colSpan="8" className="text-center">रेकॉर्ड उपलब्ध नाही</td>
                                                                    </tr>
                                                                )
                                                                }
                                                            </tbody>


                                                        </table>
                                                    </div>
                                                </div>
                                            </div>

                                            {/* Detail forms inputs */}
                                            <div className="addservice-info mb-2" >

                                                <div className="row">

                                                    <div className="col-lg-4 col-sm-6 col-12">
                                                        <label className="form-label required">पिकाचा प्रकार</label>
                                                        <Select
                                                            placeholder="निवडा"
                                                            classNamePrefix="react-select"
                                                            options={cropType}
                                                            name="cropType"
                                                            openMenuOnFocus={true}
                                                            value={cropType.find(option => option.value === formData.cropType) || null}
                                                            onChange={(selectedOption) => {
                                                                setFormData(prevState => ({
                                                                    ...prevState,
                                                                    cropType: selectedOption ? selectedOption.value : '',
                                                                }));
                                                            }}
                                                            ref={cropTypeRef}
                                                            // required
                                                            title="कृपया पीक निवडा"
                                                        />
                                                    </div>


                                                    {/* <div className="col-lg-2 col-sm-6 col-12"> */}
                                                    <div className="col-6 col-md-4 col-lg-2 mb-3">
                                                        <div >
                                                            <label className="form-label required">पीक जात </label>
                                                            <input
                                                                type="text"
                                                                className="form-control"
                                                                id="cropvariety"
                                                                name="cropvariety"
                                                                value={formData.cropvariety}
                                                                onChange={handleDetailInputChange}
                                                                placeholder="पीक वान/जात प्रविष्ट करा"
                                                                ref={cropvarietyRef}
                                                            />
                                                        </div>
                                                    </div>

                                                    {/* <div className="col-lg-2 col-sm-6 col-12"> */}
                                                    <div className="col-6 col-md-4 col-lg-2 mb-3">
                                                        <div >
                                                            <label className="form-label required">पीक क्षेत्र(एकर)</label>
                                                            <input
                                                                type="text"
                                                                className="form-control"
                                                                id="croparea"
                                                                name="croparea"
                                                                value={formData.croparea}
                                                                onChange={handleDetailInputChange}
                                                                placeholder="कृपया पीक क्षेत्र प्रविष्ट करा"
                                                                ref={cropareaRef}
                                                            />
                                                        </div>
                                                    </div>

                                                    {/* <div className="col-lg-2  col-sm-6 col-12"> */}
                                                    <div className="col-6 col-md-4 col-lg-2 mb-3">
                                                        <div >
                                                            <label className="form-label">अ.कापणी दिनांक</label>
                                                            <input
                                                                type="date"
                                                                className="form-control"
                                                                id="expectedharvestingdate"
                                                                name="expectedharvestingdate"
                                                                value={formData.expectedharvestingdate}
                                                                onChange={handleDetailInputChange}
                                                                placeholder="अपेक्षितकापणी दिनांक निवडा"
                                                                ref={expectedharvestingdateRef}

                                                            // required
                                                            // pattern="^\d+(\.\d{1,2})?$"
                                                            // title="Only Digits are allowed. Field cannot be empty or just spaces."

                                                            />
                                                        </div>
                                                    </div>

                                                    {/* <div className="col-lg-2  col-sm-6 col-12"> */}
                                                    <div className="col-6 col-md-4 col-lg-2 mb-3">
                                                        <div >
                                                            <label className="form-label">अ.उत्पादन(कीलो)</label>
                                                            <input
                                                                type="text"
                                                                className="form-control"
                                                                id="expectedproduction"
                                                                name="expectedproduction"
                                                                value={formData.expectedproduction}
                                                                onChange={handleDetailInputChange}
                                                                placeholder="0"
                                                                ref={expectedproductionRef}

                                                            // required
                                                            // pattern="^\d+(\.\d{1,2})?$"
                                                            // title="Only Digits are allowed. Field cannot be empty or just spaces."

                                                            />
                                                        </div>
                                                    </div>
                                                </div>

                                                <div className="row">

                                                    <div className="col-lg-9 col-sm-6 col-12">


                                                        <div>
                                                            <div className="d-flex flex-wrap align-items-center">
                                                                <label className="form-label required">फोटो कॅप्चर करा</label>
                                                                {/* ✅ Show uploaded file names */}
                                                                <div className="mt-2 d-flex flex-wrap gap-2">
                                                                    {formData.photoes.length > 0 &&
                                                                        formData.photoes.map((file, index) => (
                                                                            // <div key={index} className="px-2 py-1 rounded bg-success text-white small">
                                                                            //     ✔ {file.split('_').pop()}
                                                                            // </div>
                                                                            <div
                                                                                key={index}
                                                                                className="px-3 py-1 rounded-pill bg-success text-black d-flex align-items-center shadow-sm"
                                                                                style={{
                                                                                    animation: "fadeIn 0.4s ease-in-out",
                                                                                    gap: "6px",
                                                                                    cursor: "pointer",
                                                                                }}
                                                                                onClick={handleViewUploadedFiles} // ⬅ Trigger preview modal
                                                                                title="पूर्वावलोकन पाहण्यासाठी क्लिक करा"
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
                                                                    onChange={handleFileChange}
                                                                    ref={fileInputRef}

                                                                />

                                                                {/* eye view
                                                                {formData.photoes.length > 0 && (
                                                                    <i
                                                                        className="fas fa-eye position-absolute text-primary"
                                                                        style={{
                                                                            right: '45px',
                                                                            top: '50%',
                                                                            transform: 'translateY(-50%)',
                                                                            cursor: 'pointer',
                                                                            fontSize: '18px',
                                                                        }}
                                                                        onClick={handleViewUploadedFiles}
                                                                        title="Uploaded file preview"
                                                                    ></i>
                                                                )} */}

                                                                {/* Camera Icon */}
                                                                <i
                                                                    className="fas fa-camera position-absolute "
                                                                    style={{
                                                                        right: '12px',
                                                                        top: '50%',
                                                                        transform: 'translateY(-50%)',
                                                                        cursor: 'pointer',
                                                                        fontSize: '18px',
                                                                        color: 'black'
                                                                    }}
                                                                    onClick={() => document.getElementById("cameraInput").click()}
                                                                    title="कॅमेरा उघडा"
                                                                ></i>
                                                            </div>



                                                            {/* ✅ Upload message display */}
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



                                                    {/* <div className="col-lg-1  col-sm-6 col-12"> */}
                                                    <div className="col-6 col-md-4 col-lg-1 mb-3">
                                                        <div >
                                                            <label className="form-label">भाव </label>
                                                            <input
                                                                type="text"
                                                                className="form-control"
                                                                id="rate"
                                                                name="rate"
                                                                value={formData.rate}
                                                                onChange={handleDetailInputChange}
                                                                placeholder="0"
                                                                ref={rateRef}
                                                            />
                                                        </div>
                                                    </div>

                                                    {/* <div className="col-lg-1  col-sm-6 col-12"> */}
                                                    <div className="col-6 col-md-4 col-lg-1 mb-3">

                                                        <div >
                                                            <label className="form-label">अडवांस</label>
                                                            <input
                                                                type="text"
                                                                className="form-control"
                                                                id="advancemoney"
                                                                name="advancemoney"
                                                                value={formData.advancemoney}
                                                                onChange={handleDetailInputChange}
                                                                placeholder="0"
                                                                ref={advancemoneyRef}

                                                            // required
                                                            // pattern="^\d+(\.\d{1,2})?$"
                                                            // title="Only Digits are allowed. Field cannot be empty or just spaces."

                                                            />
                                                        </div>
                                                    </div>


                                                    <div className="col-lg-1 col-sm-6 col-12">

                                                        <div className="mt-4">
                                                            <button

                                                                type="button"
                                                                // className="btn btn-primary"
                                                                className="btn btn-primary w-100"
                                                                onClick={addRecord}
                                                            >
                                                                जोडा
                                                            </button>
                                                        </div>

                                                    </div>

                                                </div>

                                            </div>

                                        </div>
                                        {/* End (Detail) */}

                                        <div className="col-lg-12">
                                            <div className="text-end">
                                                <div className="row justify-content-end">
                                                    {/* Save Button - First on mobile, second on desktop */}
                                                    <div className="col-12 col-md-1 mb-2 order-1 order-md-2">
                                                        <button
                                                            type="submit"
                                                            className="btn btn-submit w-100"
                                                        >
                                                            सेव्ह
                                                        </button>
                                                    </div>

                                                    {/* Back Button - Second on mobile, first on desktop */}
                                                    <div className="col-12 col-md-1 mb-2 order-2 order-md-1">
                                                        <button
                                                            type="button"
                                                            className="btn btn-cancel w-100"
                                                            onClick={showExitAlert}
                                                        >
                                                            मागे
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        {/* <div className="col-lg-12 d-flex justify-content-end mt-2">
                                            <Link className="btn btn-secondary me-2"
                                                onClick={showExitAlert}
                                            >
                                                मागे
                                            </Link>
                                            <button
                                                type="submit"
                                                className="btn btn-submit"
                                            >
                                                सेव्ह
                                            </button>
                                        </div> */}
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

export default AddVisitorMaster;

