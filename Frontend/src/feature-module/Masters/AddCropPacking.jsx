import React, { useState, useEffect, useRef } from 'react';
import withReactContent from "sweetalert2-react-content";
import Swal from "sweetalert2";
import { ACSPLGUID, baseUrl, convertToISODate } from "../../core/json/custom";
import axios from "axios";
import { all_routes } from '../../Router/all_routes';
import { useNavigate } from 'react-router-dom';
import { getUserData } from '../../Context/UserData';
import { Type } from 'feather-icons-react/build/IconComponents';
import Select from "react-select";
const AddCropPacking = ({ MAID, onSuccess, onRefresh }) => {
    const route = all_routes;
    const { userdetail } = getUserData();
    const GUID = ACSPLGUID.getNew();
    const MySwal = withReactContent(Swal);
    const navigate = useNavigate();
    const TYPERef = useRef(null);
    const UOMRef = useRef(null);
    const SIZERef = useRef(null);
    const COUNTRef = useRef(null);

    const [formData, setFormData] = useState({
        MAID: "",
        Type: "",
        UOM: "",
        Size: "",
        Count: ""
    });


    useEffect(() => {
        // Pushes a temporary state to intercept the back action.
        window.history.pushState(null, "", window.location.pathname);

        const handlePopState = () => {
            // Immediate re-push to block the browser from leaving the page.
            window.history.pushState(null, "", window.location.pathname);


            // 🌟 FIX 2: Use navigate() to force the navigation.
            // This is more reliable inside the asynchronous promise resolution.
            const modal = document.getElementById("Addmaterial");
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


    useEffect(() => {
        const fetchData = async () => {
            if (MAID) {
                try {
                    const payload = {
                        maid: MAID,
                        keyword: "%",
                        companyid: userdetail?.companyID || "",
                        deptid: userdetail?.departmentID || "",
                    };
                    const headers = {
                        "Content-Type": "application/json",
                        Accept: "*/*",
                    };

                    const response = await axios({
                        method: "POST",
                        url: baseUrl.Url + "/backend/api/GET_PackingMaterial",
                        data: JSON.stringify(payload),
                        headers: headers,
                    });

                    if (response.status !== 200) {
                        throw new Error("Failed to Fetch Material Data");
                    }

                    let apiData = response.data[0];

                    setFormData((prev) => ({
                        ...prev,
                        MAID: apiData.maid,
                        Type: apiData.mtype,
                        UOM: apiData.uom,
                        Size: apiData.msize,
                        Count: apiData.count,

                    }));

                } catch (error) {
                    console.error("Error fetching Access Right Data:", error);
                }
            }
        };

        fetchData();
    }, [MAID]);


    const checkFormValidity = (e) => {
        e.preventDefault();

        const { Type, UOM, Size, Count } = formData;

        if (!Type || Type.trim() === "") {
            TYPERef.current.focus();
            Swal.fire({
                icon: "error",
                title: "Error",
                text: "Please enter a type",
            });
            return false;
        }

        if (!UOM || UOM.toString().trim() === "") {
            UOMRef.current.focus();
            Swal.fire({
                icon: "error",
                title: "Error",
                text: "Please select a unit of measurement",
            });
            return false;
        }

        if (!Size || isNaN(Size) || Number(Size) <= 0) {
            SIZERef.current.focus();
            Swal.fire({
                icon: "error",
                title: "Error",
                text: "Please enter a valid quantity",
            });
            return false;
        }

        // if (!Count || isNaN(Count) || Number(Count) <= 0) {
        //     COUNTRef.current.focus();
        //     Swal.fire({
        //         icon: "error",
        //         title: "Error",
        //         text: "Please enter a valid number",
        //     });
        //     return false;
        // }
        handleSubmit(e);
    };


    const handleSubmit = (e) => {
        e.preventDefault();
        const form = e.target.closest("form");
        if (!form.checkValidity()) {
            form.reportValidity();
            return;
        }
        showConfirmationAlert();
    };
    const showConfirmationAlert = () => {
        MySwal.fire({
            title: 'Are you sure?',
            text: 'Do you want to save this data?',
            showCancelButton: true,
            confirmButtonColor: '#00ff00',
            confirmButtonText: 'Save',
            cancelButtonColor: '#092C4C',
            cancelButtonText: 'Cancel',
            allowOutsideClick: false,
            allowEscapeKey: false,

        }).then((result) => {
            if (result.isConfirmed) {
                handleFormSubmission();
            }
        });
    };

    const handleFormSubmission = async () => {
        try {
            const payload = {
                "maid": MAID ? MAID : GUID,
                "mtype": formData.Type,
                "uom": formData.UOM,
                "msize": formData.Size,
                "count": formData.Count,
                "companyid": userdetail?.companyID || "",
                "deptid": userdetail?.departmentID || "",

            };

            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };

            const response = await axios({
                method: "POST",
                url: baseUrl.Url + "/backend/api/SP_AddUpdPackingMaterial",
                data: JSON.stringify(payload),
                headers: headers,
            });

            console.log("Response Received:", response.data);
            Swal.fire({
                icon: "success",
                title: "Saved!",
                text: "Information has been saved successfully.",
                confirmButtonText: "OK",
                allowOutsideClick: false,
                allowEscapeKey: false,
            }).then(() => {
                const modalElement = document.getElementById("Addmaterial");
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
                if (onSuccess) onSuccess();
            });

            setFormData({
                Type: "",
                UOM: "",
                Size: "",
                Count: ""
            });
        } catch (error) {
            console.error("Submission Error:", error);
            Swal.fire({
                icon: "error",
                title: "Error",
                text: "Failed to save the information. Please try again.",
                allowOutsideClick: false,
                allowEscapeKey: false,
            });
        }
    };

    const handleInputChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });

    };


    useEffect(() => {
        const handleShortcut = (e) => {
            if (e.ctrlKey && e.key === "s" || e.ctrlKey && e.key === 'S') {
                e.preventDefault();
                checkFormValidity(e);

            }

            if (e.ctrlKey && e.key === "e") {
                e.preventDefault();
                showExitAlert();
            }
        };

        window.addEventListener("keydown", handleShortcut);

        return () => {
            window.removeEventListener("keydown", handleShortcut);
        };
    }, [formData, navigate, route.designation, handleSubmit]);


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
                // navigate("/CropPacking");
                // const modal = document.getElementById("Addmaterial");
                // if (modal) {
                //     modal.classList.remove("show");
                //     modal.style.display = "none";
                //     modal.setAttribute("aria-hidden", "true");

                //     document.querySelectorAll(".modal-backdrop").forEach((backdrop) => {
                //         backdrop.remove();
                //     });

                //     document.body.classList.remove("modal-open");
                //     document.body.style.overflow = "";
                //     document.body.style.paddingRight = "";
                // }


                const modalElement = document.getElementById("Addmaterial");
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

                setFormData({
                    MAID: "",
                    Type: "",
                    UOM: "",
                    Size: "",
                    Count: ""
                });

            }
        });
    };


    const [UOM, setUOM] = useState([]);
    useEffect(() => {

        const fetchPakingOptions = async () => {
            try {
                const response = await axios.get(baseUrl.Url + "/backend/api/Implications/SOLID");

                if (response.status !== 200) throw new Error("Failed to fetch brand data");

                const data = response.data;

                const dropdownOptions = data.map(({ iTitle, iValue }) => ({
                    label: iTitle,
                    value: iValue,
                }));

                setUOM(dropdownOptions);
            } catch (error) {
                console.error("Error fetching brand options:", error);
            }
        };

        fetchPakingOptions();

    }, []);

    const handleSelectChange = (selectedOption, actionMeta) => {
        setFormData(prev => ({
            ...prev,
            [actionMeta.name]: selectedOption ? selectedOption.value : ""
        }));
    };

    return (
        <div>
            <div className="modal fade" id="Addmaterial">
                <div className="modal-dialog modal-dialog-centered custom-modal-two">
                    <div className="modal-content">
                        <div className="page-wrapper-new p-0">
                            <div className="content">
                                <div className="modal-header border-0 custom-modal-header">
                                    <div className="page-title">
                                        <h4>Add Packing Material</h4>
                                    </div>
                                    <button
                                        type="button"
                                        className="close"
                                        aria-label="Close"
                                        onClick={showExitAlert}
                                    >
                                        <span aria-hidden="true">×</span>
                                    </button>
                                </div>
                                <div className="modal-body custom-modal-body">
                                    <form onSubmit={handleSubmit}>
                                        <div className="row">
                                            <div className="col-lg-12 col-sm-6 col-12">
                                                <div className="mb-3">
                                                    <label className="form-label required">Select Type</label>
                                                    <input
                                                        ref={TYPERef}
                                                        type="text"
                                                        className="form-control"
                                                        name="Type"
                                                        value={formData.Type}
                                                        onChange={handleInputChange}
                                                        required
                                                    />
                                                </div>
                                            </div>
                                        </div>
                                        <div className="row">
                                            <div className="col-6 mb-3 ">
                                                <label className="form-label required">Unit of Measurement</label>
                                                <Select
                                                    ref={UOMRef}
                                                    classNamePrefix="react-select"
                                                    options={UOM}
                                                    placeholder="Choose"
                                                    name="UOM"
                                                    value={UOM.find(option => option.value.toString() === formData.UOM.toString()) || null}
                                                    onChange={(selectedOption) => handleSelectChange(selectedOption, { name: "UOM" })}
                                                    required
                                                />
                                            </div>
                                            <div className="col-6 mb-3">
                                                <label className="form-label required">Packing Size</label>
                                                <input
                                                    ref={SIZERef}
                                                    type="text"
                                                    className="form-control"
                                                    name="Size"
                                                    required
                                                    value={formData.Size}
                                                    onChange={handleInputChange}
                                                />
                                            </div>
                                        </div>

                                        <div className='row'>
                                            <div className="col-lg-12 col-sm-6 col-12">
                                                <div className="mb-3">
                                                    <label className="form-label">Number of Crates</label>
                                                    <input
                                                        ref={COUNTRef}
                                                        type="text"
                                                        className="form-control"
                                                        name="Count"
                                                        value={formData.Count}
                                                        onChange={handleInputChange}
                                                    />
                                                </div>
                                            </div>
                                        </div>

                                        <div className="modal-footer-btn">
                                            <button
                                                type="button"
                                                className="btn btn-cancel me-2"
                                                aria-label="Close"
                                                onClick={showExitAlert}
                                            >
                                                Cancel
                                            </button>

                                            <button
                                                type="submit"
                                                className="btn btn-submit"
                                            >
                                                Save
                                            </button>
                                        </div>
                                    </form>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    )
}

export default AddCropPacking
