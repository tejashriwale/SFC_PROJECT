import React, { useState, useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";
import { all_routes } from "../../Router/all_routes";
import withReactContent from "sweetalert2-react-content";
import Swal from "sweetalert2";
import { ArrowLeft, ChevronDown, ChevronUp, Info } from "feather-icons-react/build/IconComponents";
import { useDispatch, useSelector } from "react-redux";
import { setToogleHeader } from "../../core/redux/action";
import { OverlayTrigger, Tooltip } from "react-bootstrap";
import { getUserData } from "../../Context/UserData";
import Select from "react-select";
import axios from 'axios';
import { MultiSelect } from "react-multi-select-component";
const Location = () => {
    const navigate = useNavigate();
    const route = all_routes;
    const dispatch = useDispatch();
    const data = useSelector((state) => state.toggle_header);
    const { userdetail } = getUserData();
    const isRestricted = userdetail.LANGUAGE === "en";

    const [states, setStates] = useState([]);
    const [districts, setDistricts] = useState([]);
    const [talukas, setTalukas] = useState([]);
    const [villages, setVillages] = useState([]);

    // const [formData, setFormData] = useState({
    //     state: "",
    //     district: "",
    //     taluka: "",
    //     village: "",
    // });

    // In your state, store arrays instead of single values
    const [formData, setFormData] = useState({
        state: "",
        district: [],
        taluka: [],
        village: "",
    });
    const renderCollapseTooltip = (props) => (
        <Tooltip id="refresh-tooltip" {...props}>
            Collapse
        </Tooltip>
    );

    // useEffect(() => {
    //     const fetchCropData = async () => {
    //         try {
    //             const headers = {
    //                 "Content-Type": "application/json",
    //                 Accept: "*/*",
    //             };

    //             // Only use the relative path now
    //             const response = await axios.post(
    //                 "/API/GetState",
    //                 {},
    //                 { headers }
    //             );

    //             if (response.status !== 200) {
    //                 throw new Error("Failed to fetch states");
    //             }

    //             console.log("response states", response.data);
    //             setState(response.data);
    //             setAllState(response.data);
    //         } catch (error) {
    //             console.error("Error fetching HSN data:", error);
    //         }
    //     };

    //     fetchCropData();
    // }, []);


    // const handleStateChange = async (selectedStateCode) => {
    //     setFormData((prev) => ({
    //         ...prev,
    //         state: selectedStateCode,
    //         district: "",
    //     }));

    //     if (!selectedStateCode) {
    //         setDistricts([]);
    //         return;
    //     }

    //     try {
    //         const headers = {
    //             "Content-Type": "application/json",
    //             Accept: "*/*",
    //         };

    //         const response = await axios.post(
    //             "/API/GetAllDistricts",
    //             { StateCode: selectedStateCode },
    //             { headers }
    //         );

    //         if (response.status !== 200) {
    //             throw new Error("Failed to fetch districts");
    //         }

    //         setDistricts(response.data);
    //     } catch (error) {
    //         console.error("Error fetching districts:", error);
    //     }
    // };


    // const locationData = {
    //     Maharashtra: {
    //         Pune: {
    //             Haveli: ["Wagholi", "Kharadi", "Yerwada"],
    //             Mulshi: ["Pirangut", "Paud", "Lavasa"],
    //         },
    //         Mumbai: {
    //             "South Mumbai": ["Colaba", "Fort", "Marine Lines"],
    //             Andheri: ["Andheri East", "Andheri West", "Versova"],
    //         },
    //     },
    //     Gujarat: {
    //         Ahmedabad: {
    //             Daskroi: ["Bopal", "Ghuma", "Sanand"],
    //             City: ["Paldi", "Navrangpura", "Maninagar"],
    //         },
    //         Surat: {
    //             Choriyasi: ["Vesu", "Dumas", "Adajan"],
    //             Olpad: ["Olpad Town", "Kansad", "Sayan"],
    //         },
    //     },
    // };

    // const [formData, setFormData] = useState({
    //     state: "",
    //     district: "",
    //     taluka: "",
    //     village: "",
    // });

    // const handleInputChange = (field, value) => {
    //     setFormData((prev) => ({
    //         ...prev,
    //         [field]: value,
    //     }));
    // };

    const MySwal = withReactContent(Swal);

    const handleSubmit = (e) => {
        e.preventDefault();
        showConfirmationAlert();
    };

    const handleFormSubmission = () => {
        Swal.fire({
            icon: "success",
            title: isRestricted ? "Saved!" : "जतन केले!",
            text: isRestricted ? "Location saved successfully." : "स्थान यशस्वीरित्या जतन झाले आहे.",
            confirmButtonText: isRestricted ? "OK" : "ठीक आहे",
            allowOutsideClick: false,
            allowEscapeKey: false
        });
        navigate(route.MasterIndex);
    };

    const showConfirmationAlert = () => {
        MySwal.fire({
            title: isRestricted ? "Are you sure?" : "तुम्हाला खात्री आहे का?",
            text: isRestricted ? "Do you want to save this location?" : "तुम्ही हे स्थान जतन करू इच्छिता का?",
            showCancelButton: true,
            confirmButtonColor: "#00ff00",
            confirmButtonText: isRestricted ? "Save" : "जतन करा",
            cancelButtonColor: "#092C4C",
            cancelButtonText: isRestricted ? "Cancel" : "रद्द करा",
            allowOutsideClick: false,
            allowEscapeKey: false
        }).then((result) => {
            if (result.isConfirmed) {
                handleFormSubmission();
            }
        });
    };

    const showExitAlert = () => {
        MySwal.fire({
            title: isRestricted ? "Are you sure?" : "तुम्हाला खात्री आहे का?",
            text: isRestricted ? "Do you want to exit?" : "तुम्ही बाहेर पडू इच्छिता का?",
            showCancelButton: true,
            confirmButtonColor: "#00ff00",
            confirmButtonText: isRestricted ? "Yes" : "होय",
            cancelButtonColor: "#092C4C",
            cancelButtonText: isRestricted ? "No" : "नाही",
            allowOutsideClick: false,
            allowEscapeKey: false
        }).then((result) => {
            if (result.isConfirmed) {
                navigate(route.MasterIndex); // change as needed
            }
        });
    };


    // Fetch states
    useEffect(() => {
        axios.post("/API/GetState", {})
            .then((res) => setStates(res.data))
            .catch((err) => console.error("Error fetching states", err));
    }, []);

    // Fetch districts on state change
    const handleStateChange = async (stateCode) => {
        setFormData({ state: stateCode, district: "", taluka: "" });
        try {
            const res = await axios.post("/API/GetAllDistricts", { statecode: stateCode });
            setDistricts(res.data);
            setTalukas([]); // reset talukas
        } catch (error) {
            console.error("Error fetching districts", error);
        }
    };

    // Fetch talukas on district change
    // const handleDistrictChange = async (districtCode) => {
    //     setFormData((prev) => ({ ...prev, district: districtCode, taluka: "" }));

    //     try {
    //         const res = await axios.post(
    //             `/API/GetTalukasOfDistrict?distcode=${districtCode}`, // query param
    //             {}, // empty body
    //             {
    //                 headers: { "Content-Type": "application/json" }
    //             }
    //         );

    //         console.log("Talukas Response:", res.data);

    //         if (Array.isArray(res.data)) {
    //             setTalukas(res.data);
    //         } else {
    //             console.error("Unexpected response format", res.data);
    //             setTalukas([]);
    //         }
    //     } catch (error) {
    //         console.error("Error fetching talukas", error);
    //         setTalukas([]);
    //     }
    // };

    // const handleTalukaChange = async (talukaCode) => {
    //     setFormData((prev) => ({ ...prev, taluka: talukaCode, village: "" }));
    //     setVillages([]);

    //     if (!formData.district || !talukaCode) return;

    //     try {
    //         const res = await axios.post(
    //             `/API/GetVillagesOfDistrictAndTaluka?distcode=${formData.district}&talukacode=${talukaCode}`,
    //             {},
    //             { headers: { "Content-Type": "application/json" } }
    //         );

    //         if (Array.isArray(res.data)) {
    //             setVillages(res.data);
    //         } else {
    //             console.error("Unexpected response format", res.data);
    //             setVillages([]);
    //         }
    //     } catch (error) {
    //         console.error("Error fetching villages", error);
    //         setVillages([]);
    //     }
    // };



    const handleDistrictChange = async (selectedOptions) => {
        const selectedDistricts = selectedOptions ? selectedOptions.map(opt => opt.value) : [];

        setFormData((prev) => ({
            ...prev,
            district: selectedDistricts,
            taluka: [],
            village: ""
        }));

        if (selectedDistricts.length === 0) {
            setTalukas([]);
            return;
        }

        try {
            let allTalukas = [];
            for (let distCode of selectedDistricts) {
                const res = await axios.post(
                    `/API/GetTalukasOfDistrict?distcode=${distCode}`,
                    {},
                    { headers: { "Content-Type": "application/json" } }
                );
                if (Array.isArray(res.data)) {
                    allTalukas = [...allTalukas, ...res.data];
                }
            }
            setTalukas(allTalukas);
        } catch (error) {
            console.error("Error fetching talukas", error);
            setTalukas([]);
        }
    };

    const handleTalukaChange = async (selectedOptions) => {
        const selectedTalukas = selectedOptions ? selectedOptions.map(opt => opt.value) : [];

        setFormData((prev) => ({
            ...prev,
            taluka: selectedTalukas,
            village: ""
        }));

        if (!formData.district.length || selectedTalukas.length === 0) {
            setVillages([]);
            return;
        }

        try {
            let allVillages = [];
            for (let distCode of formData.district) {
                for (let talCode of selectedTalukas) {
                    const res = await axios.post(
                        `/API/GetVillagesOfDistrictAndTaluka?distcode=${distCode}&talukacode=${talCode}`,
                        {},
                        { headers: { "Content-Type": "application/json" } }
                    );
                    if (Array.isArray(res.data)) {
                        allVillages = [...allVillages, ...res.data];
                    }
                }
            }
            setVillages(allVillages);
        } catch (error) {
            console.error("Error fetching villages", error);
            setVillages([]);
        }
    };


    const handleInputChange = (field, value) => {
        setFormData((prev) => ({
            ...prev,
            [field]: value,
        }));
    };


    return (
        <div className="page-wrapper">
            <div className="content">
                <div className="page-header">
                    <div className="add-item d-flex">
                        <div className="page-title">
                            <h4>{isRestricted ? "Select Location" : "स्थान निवडा"}</h4>
                            <h6>{isRestricted ? "Choose State, District, Taluka, and Village" : "राज्य, जिल्हा, तालुका आणि गाव निवडा"}</h6>
                        </div>
                    </div>
                    <ul className="table-top-head">
                        <li>
                            <div className="page-btn">
                                <Link to={route.MasterIndex} className="btn btn-secondary">
                                    <ArrowLeft className="me-2" />
                                    {isRestricted ? "Back" : "मागे"}
                                </Link>
                            </div>
                        </li>
                        <li>
                            <OverlayTrigger placement="top" overlay={renderCollapseTooltip}>
                                <Link
                                    id="collapse-header"
                                    className={data ? "active" : ""}
                                    onClick={() => {
                                        dispatch(setToogleHeader(!data));
                                    }}
                                >
                                    <ChevronUp className="feather-chevron-up" />
                                </Link>
                            </OverlayTrigger>
                        </li>
                    </ul>
                </div>

                <form onSubmit={handleSubmit}>
                    <div className="card mbgcolor">
                        <div className="card-body add-product mbgcolor pb-0">
                            <div className="accordion-card-one accordion" id="accordionExample">
                                <div className="accordion-item mbgcolor">
                                    <div className="accordion-header" id="headingOne">
                                        <div
                                            data-bs-toggle="collapse"
                                            data-bs-target="#collapseOne"
                                            aria-controls="collapseOne"
                                        >
                                            <div className="addproduct-icon">
                                                <h5>
                                                    <Info className="add-info" />
                                                    <span>
                                                        {isRestricted
                                                            ? "Select your location details step by step."
                                                            : "तुमचे स्थान तपशील पायरी-पायरीने निवडा."}
                                                    </span>
                                                </h5>
                                                <Link to="#">
                                                    <ChevronDown className="chevron-down-add" />
                                                </Link>
                                            </div>
                                        </div>
                                    </div>
                                    <div
                                        id="collapseOne"
                                        className="accordion-collapse collapse show"
                                        aria-labelledby="headingOne"
                                        data-bs-parent="#accordionExample"
                                    >
                                        <div className="accordion-body">
                                            <div className="row">
                                                {/* State */}
                                                <div className="col-lg-3 col-sm-6 col-12 mb-3">
                                                    <label className="form-label">{isRestricted ? "State" : "राज्य"}</label>
                                                    <Select
                                                        options={states.map((state) => ({
                                                            value: state.statecode,
                                                            label: state.statelocalname
                                                        }))}
                                                        value={states
                                                            .map((state) => ({
                                                                value: state.statecode,
                                                                label: state.statelocalname
                                                            }))
                                                            .find((opt) => opt.value === formData.state) || null}
                                                        onChange={(selected) => handleStateChange(selected?.value)}
                                                        placeholder="-- Select State --"
                                                        isClearable
                                                    />
                                                </div>

                                                {/* District */}
                                                <div className="col-lg-6 col-sm-6 col-12 mb-3">
                                                    <label className="form-label">{isRestricted ? "District" : "जिल्हा"}</label>
                                                    <MultiSelect
                                                        options={districts.map((dist) => ({
                                                            label: dist.districtlocalname,
                                                            value: dist.districtcode,
                                                        }))}
                                                        value={districts
                                                            .map((dist) => ({
                                                                label: dist.districtlocalname,
                                                                value: dist.districtcode,
                                                            }))
                                                            .filter((opt) => formData.district.includes(opt.value))}
                                                        onChange={(selected) => {
                                                            const selectedValues = selected.map((s) => s.value);
                                                            setFormData((prev) => ({
                                                                ...prev,
                                                                district: selectedValues,
                                                                taluka: [],
                                                                village: "",
                                                            }));
                                                            handleDistrictChange(selected);
                                                        }}
                                                        labelledBy={isRestricted ? "Select District(s)" : "जिल्हा निवडा"}
                                                        disableSearch={false}
                                                        hasSelectAll={true}
                                                        disabled={!states.length}
                                                        overrideStrings={{
                                                            selectSomeItems: isRestricted ? "Select District(s)" : "जिल्हा निवडा",
                                                            allItemsAreSelected: isRestricted ? "All Selected" : "सर्व निवडलेले",
                                                            selectAll: isRestricted ? "Select All" : "सर्व निवडा",
                                                            search: isRestricted ? "Search" : "शोधा",
                                                        }}
                                                        className="multi-select-black"
                                                    />
                                                </div>

                                                {/* Taluka */}
                                                <div className="col-lg-6 col-sm-6 col-12 mb-3">
                                                    <label className="form-label">{isRestricted ? "Taluka" : "तालुका"}</label>
                                                    <MultiSelect
                                                        options={talukas.map((tal) => ({
                                                            label: tal.subdistrictlocalname,
                                                            value: tal.subdistrictcode,
                                                        }))}
                                                        value={talukas
                                                            .map((tal) => ({
                                                                label: tal.subdistrictlocalname,
                                                                value: tal.subdistrictcode,
                                                            }))
                                                            .filter((opt) => formData.taluka.includes(opt.value))}
                                                        onChange={(selected) => {
                                                            const selectedValues = selected.map((s) => s.value);
                                                            setFormData((prev) => ({
                                                                ...prev,
                                                                taluka: selectedValues,
                                                                village: "",
                                                            }));
                                                            handleTalukaChange(selected);
                                                        }}
                                                        labelledBy={isRestricted ? "Select Taluka(s)" : "तालुका निवडा"}
                                                        disableSearch={false}
                                                        hasSelectAll={true}
                                                        disabled={!formData.district.length}
                                                        overrideStrings={{
                                                            selectSomeItems: isRestricted ? "Select Taluka(s)" : "तालुका निवडा",
                                                            allItemsAreSelected: isRestricted ? "All Selected" : "सर्व निवडलेले",
                                                            selectAll: isRestricted ? "Select All" : "सर्व निवडा",
                                                            search: isRestricted ? "Search" : "शोधा",
                                                        }}
                                                        className="multi-select-black"
                                                    />
                                                </div>

                                                {/* Village */}
                                                <div className="col-lg-4 col-sm-6 col-12 mb-3">
                                                    <label className="form-label">{isRestricted ? "Village" : "गाव"}</label>
                                                    <Select
                                                        options={villages.map((vill) => ({
                                                            value: vill.villagecode.trim(),
                                                            label: `${vill.villagelocalname} (${vill.villagelocalname})`
                                                        }))}
                                                        value={villages
                                                            .map((vill) => ({
                                                                value: vill.villagecode.trim(),
                                                                label: `${vill.villagelocalname} (${vill.villagelocalname})`
                                                            }))
                                                            .find((opt) => opt.value === formData.village) || null}
                                                        onChange={(selected) => handleInputChange("village", selected?.value)}
                                                        placeholder={isRestricted ? "-- Select Village --" : "-- गाव निवडा --"}
                                                        isDisabled={!formData.taluka}
                                                        isClearable
                                                    />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            {/* Buttons */}
                            <div className="col-lg-12">
                                <div className="btn-addproduct mb-4">
                                    <button type="button" onClick={showExitAlert} className="btn btn-cancel me-2">
                                        {isRestricted ? "Exit" : "बाहेर पडा"}
                                    </button>
                                    <button type="submit" className="btn btn-submit">
                                        {isRestricted ? "Save" : "जतन करा"}
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default Location;
