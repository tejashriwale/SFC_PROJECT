import React, { useState, useEffect } from "react";
import axios from 'axios';
import { baseUrl } from "../../core/json/custom";
import withReactContent from "sweetalert2-react-content";
import Swal from "sweetalert2";
import { Link } from "react-router-dom";
import { ArrowLeft } from "feather-icons-react/build/IconComponents";
import { getUserData } from '../../Context/UserData';
import { openFarmerLedgerReportInNewTab } from "./FarmerLedgerReport";
import Select from "react-select";

const FarmerLedger = () => {
    const { userdetail } = getUserData();
    const MySwal = withReactContent(Swal);
    const [formData, setFormData] = useState({
        Startdate: "",
        Enddate: "",
        selectedFarmer: null,
        selectedCrop: null
    });
    const [farmerOptions, setFarmerOptions] = useState([]);
    const [cropOptions, setCropOptions] = useState([]);

    // Fetch farmers list
    useEffect(() => {
        const fetchFarmers = async () => {
            try {
                const response = await axios.post(
                    `${baseUrl.Url}/backend/api/GET_Farmer`,
                    {
                        faid: "%",
                        companyid: userdetail?.companyID || "",
                        deptid: userdetail?.departmentID || ""
                    },
                    {
                        headers: {
                            "Content-Type": "application/json",
                            Accept: "*/*"
                        }
                    }
                );

                const options = response.data.map(farmer => ({
                    value: farmer.faid,
                    label: `${farmer.fname} - ${farmer.phonenum || ''}`
                }));
                setFarmerOptions([
                    { value: '%', label: 'All Farmers' },
                    ...options
                ]);
            } catch (error) {
                console.error("Error fetching farmers:", error);
            }
        };

        fetchFarmers();
    }, [userdetail]);

    // Fetch crops list
    useEffect(() => {
        const fetchCrops = async () => {
            try {
                const response = await axios.post(
                    `${baseUrl.Url}/backend/api/GET_Item`,
                    {
                        itemaid: "%",
                        companyid: userdetail?.companyID || "",
                        deptid: userdetail?.departmentID || ""
                    },
                    {
                        headers: {
                            "Content-Type": "application/json",
                            Accept: "*/*"
                        }
                    }
                );

                const options = response.data.map(item => ({
                    value: item.itemaid,
                    label: item.itemne
                }));
                setCropOptions([
                    { value: '%', label: 'All Crops' },
                    ...options
                ]);
            } catch (error) {
                console.error("Error fetching crops:", error);
            }
        };

        fetchCrops();
    }, [userdetail]);

    const isValidDate = (date) => date && !isNaN(new Date(date).getTime());

    const handleGenerateReport = async () => {
        const { Startdate, Enddate, selectedFarmer, selectedCrop } = formData;

        if (!isValidDate(Startdate) || !isValidDate(Enddate)) {
            MySwal.fire({
                title: "Invalid Date",
                text: "Please select valid start and end dates.",
                icon: "error",
                confirmButtonColor: "#ff0000",
                confirmButtonText: "OK",
            });
            return;
        }

        const startDate = new Date(Startdate);
        const endDate = new Date(Enddate);
        const today = new Date();

        if (startDate > endDate) {
            MySwal.fire({
                title: "Invalid Date Range",
                text: "Start date cannot be greater than end date.",
                icon: "warning",
                confirmButtonColor: "#ff0000",
                confirmButtonText: "OK",
            });
            return;
        }

        if (endDate > today) {
            MySwal.fire({
                title: "Invalid Date",
                text: "End date cannot be in the future.",
                icon: "warning",
                confirmButtonColor: "#ff0000",
                confirmButtonText: "OK",
            });
            return;
        }

        try {
            const payload = {
                maid: selectedFarmer?.value || '%',
                cropid: selectedCrop?.value || '%',
                companyid: userdetail?.companyID || '',
                deptid: userdetail?.departmentID || ''
            };

            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };

            const response = await axios.post(
                `${baseUrl.Url}/backend/api/GET_FarmerLedgerReport`,
                payload,
                { headers }
            );

            if (response.status !== 200) throw new Error("Failed to fetch farmer ledger data");

            const ledgerData = response.data;

            if (!ledgerData || ledgerData.length === 0) {
                MySwal.fire('No Data', 'No ledger data found for the selected filters.', 'info');
                return;
            }

            // Open report in new tab
            openFarmerLedgerReportInNewTab(ledgerData, {
                farmerName: selectedFarmer?.label || 'All Farmers',
                cropName: selectedCrop?.label || 'All Crops',
                startDate: Startdate,
                endDate: Enddate
            });

        } catch (error) {
            console.error("Error generating Farmer Ledger report:", error);
            MySwal.fire('Error', 'Failed to generate report: ' + error.message, 'error');
        }
    };

    return (
        <div className="page-wrapper">
            <div className="content">
                <div className="page-header">
                    <div className="add-item d-flex">
                        <div className="page-title">
                            <h3>Farmer Ledger Report</h3>
                            <h6>Generate transaction-wise farmer ledger report</h6>
                        </div>
                    </div>
                    <ul className="table-top-head">
                        <li>
                            <div className="page-btn">
                                <Link to="/Reports" className="btn btn-secondary">
                                    <ArrowLeft className="me-2" />
                                    Back
                                </Link>
                            </div>
                        </li>
                    </ul>
                </div>

                <div className="card">
                    <div className="card-body">
                        <div className="row">
                            <div className="col-lg-3 col-sm-6 col-12">
                                <div className="input-blocks">
                                    <label>Start Date</label>
                                    <input
                                        type="date"
                                        className="form-control"
                                        value={formData.Startdate}
                                        onChange={(e) => setFormData({ ...formData, Startdate: e.target.value })}
                                    />
                                </div>
                            </div>

                            <div className="col-lg-3 col-sm-6 col-12">
                                <div className="input-blocks">
                                    <label>End Date</label>
                                    <input
                                        type="date"
                                        className="form-control"
                                        value={formData.Enddate}
                                        onChange={(e) => setFormData({ ...formData, Enddate: e.target.value })}
                                    />
                                </div>
                            </div>

                            <div className="col-lg-3 col-sm-6 col-12">
                                <div className="input-blocks">
                                    <label>Select Farmer</label>
                                    <Select
                                        options={farmerOptions}
                                        value={formData.selectedFarmer}
                                        onChange={(selected) => setFormData({ ...formData, selectedFarmer: selected })}
                                        placeholder="Select Farmer..."
                                        isClearable
                                    />
                                </div>
                            </div>

                            <div className="col-lg-3 col-sm-6 col-12">
                                <div className="input-blocks">
                                    <label>Select Crop</label>
                                    <Select
                                        options={cropOptions}
                                        value={formData.selectedCrop}
                                        onChange={(selected) => setFormData({ ...formData, selectedCrop: selected })}
                                        placeholder="Select Crop..."
                                        isClearable
                                    />
                                </div>
                            </div>
                        </div>

                        <div className="row">
                            <div className="col-lg-12">
                                <div className="input-blocks text-center">
                                    <button
                                        className="btn btn-primary"
                                        onClick={handleGenerateReport}
                                        style={{ width: '250px', height: '45px', fontSize: '16px' }}
                                    >
                                        Generate Farmer Ledger Report
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default FarmerLedger;
