import React, { useState, useEffect, useRef } from "react";
import { Link } from "react-router-dom";
import Select from "react-select";
import axios from "axios";
import { ArrowLeft } from "react-feather";
import { ACSPLGUID, baseUrl } from "../../core/json/custom";
import Swal from "sweetalert2";
import { all_routes } from "../../Router/all_routes";
import withReactContent from "sweetalert2-react-content";
import { useNavigate } from 'react-router-dom';
import { getUserData } from "../../Context/UserData";

function TransportBillDetails() {
    const { userdetail } = getUserData();
    const [Customers, setCustomers] = useState([]);
    const [BillData, setBillData] = useState([]);
    const [detailData, setDetailData] = useState([]);
    const [BillDetails, setBillDetails] = useState([]);
    const [formData, setFormData] = useState({ Customer: "" });
    const CustomerRef = useRef(null);
    const [showForm, setShowForm] = useState(false);
    const MySwal = withReactContent(Swal);
    const navigate = useNavigate();
    const route = all_routes;
    const paidAmtRef = useRef(null);
    const [billForm, setBillForm] = useState({
        trdaid: "",
        traid: "",
        trnsr: "",
        trnsid: "",
        appdt: "",
        cropid: "",
        refaid: "",
        maid: "",
        billamt: 0,
        paidamt: 0,
        cbal: 0,
        advanceamt: 0,
        extraadv: 0,
        Narr: "",
        status: false
    });
    const [defaultAppDate, setDefaultAppDate] = useState("");
    useEffect(() => {
        const dateToUse = userdetail?.APPDT ? new Date(userdetail.APPDT) : new Date();

        const formattedDate = dateToUse.toLocaleDateString("en-GB", {
            day: "2-digit",
            month: "short",
            year: "numeric",
        });

        setDefaultAppDate(formattedDate);

        setBillForm((prev) => ({ ...prev, appdt: formattedDate }));
    }, [userdetail]);

    useEffect(() => {
        const fetchCustomers = async () => {
            try {
                const headers = {
                    "Content-Type": "application/json",
                    Accept: "*/*",
                };
                const payload = {
                    ctype: "3",
                    companyid: userdetail?.companyID || "",
                    deptid: userdetail?.departmentID || "",
                };

                const response = await axios.post(
                    `${baseUrl.Url}/backend/api/GET_TraderNameDropdown`,
                    payload,
                    { headers }
                );

                if (response.status !== 200) {
                    throw new Error("Failed to fetch customer data");
                }

                const data = response.data;
                const customerOptions = data.map(({ ccompanyname, caid }) => ({
                    label: ccompanyname,
                    value: caid,
                }));

                setCustomers(customerOptions);
            } catch (error) {
                console.error("Error fetching customer data:", error);
            }
        };

        fetchCustomers();
    }, [userdetail]);

    const fetchMasterList = async (customerId) => {
        try {
            const payload = {
                maid: customerId,
                companyid: userdetail?.companyID || "",
                deptid: userdetail?.departmentID || "",
            };

            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };

            const response = await axios.post(
                `${baseUrl.Url}/backend/api/GET_CustomerTransportBillData`,
                JSON.stringify(payload),
                { headers }
            );

            if (response.status !== 200) {
                throw new Error("Failed to fetch bill data");
            }

            console.log("response", response.data);
            setBillData(response.data);
            setBillDetails(response.data);
        } catch (error) {
            console.error("Error fetching bill data:", error);
        }
    };

    const handleCustomerChange = (selectedOption) => {
        setBillData([])
        setDetailData([]);
        setBillDetails([]);
        setBillForm({
            trdaid: "",
            traid: "",
            trnsr: "",
            trnsid: "",
            appdt: defaultAppDate,
            cropid: "",
            refaid: "",
            maid: "",
            billamt: 0,
            paidamt: 0,
            cbal: 0,
            advanceamt: 0,
            extraadv: 0,
            status: false,
        });
        const selectedCustomer = selectedOption ? selectedOption.value : "";

        setFormData((prev) => ({
            ...prev,
            Customer: selectedCustomer,
        }));
        fetchMasterList(selectedCustomer);
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
            allowOutsideClick: false,
            backdrop: true
        }).then((result) => {
            if (result.isConfirmed) {
                navigate(route.CropBillIndex);
            }
        });
    };
    const handleSubmit = (e) => {
        e.preventDefault();
        showConfirmationAlert(e);
    };

    const showConfirmationAlert = (e) => {
        if (!billForm.paidamt || billForm.paidamt <= 0) {
            Swal.fire({
                icon: "warning",
                title: "Invalid Amount",
                text: "Please enter a valid bill amount greater than 0.",
                confirmButtonText: "OK",
            }).then(() => {
                if (paidAmtRef && paidAmtRef.current) {
                    paidAmtRef.current.focus();
                }
            });
            return;
        }

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

    const handleFormSubmission = async () => {

        try {
            const DATA = BillDetails.find(a => a.traid === billForm.traid);
            const TRAID = ACSPLGUID.getNew();

            let statusPayload = {};
            let transportstatusPayload = {};
            let transectionPayloadList = [];
            let NewAdvAmt = 0;
            let NewPaidAmt = 0;
            let NTRNSR = 1;
            let NTRNSID = "10";
            let NCBAL = 0;
            let NREFAID = '';

            if ((parseFloat(billForm?.paidamt) || 0) < 0) {
                NewPaidAmt = parseFloat(billForm?.paidamt) || 0;
                NewAdvAmt = NewPaidAmt;
                NCBAL = 0;
                const paidAmount = Math.abs(parseFloat(billForm?.paidamt) || 0);
                transectionPayloadList.push({
                    traid: TRAID,
                    trnsr: 1,
                    trnsid: "10",
                    trntid: "20",
                    trndt: userdetail?.APPDT || "",
                    cropid: billForm?.cropid || formData.crop,
                    refaid: "",
                    maid: billForm?.maid || formData.farmer,
                    pamt: paidAmount || 0,
                    cbal: 0,
                    advamt: paidAmount || 0,
                    asid: TRAID,
                    isdeleted: false,
                    companyid: userdetail?.companyID || "",
                    deptid: userdetail?.departmentID || "",
                    PAYMENTDT: userdetail?.APPDT || new Date().toISOString()
                });
                statusPayload = {
                    companyid: userdetail?.companyID || "",
                    deptid: userdetail?.departmentID || "",
                    maid: billForm?.maid || formData.farmer,
                    refaid: billForm?.refaid || "",
                    cropid: billForm?.cropid || formData.crop,
                };
                transportstatusPayload = {
                    companyid: userdetail?.companyID || "",
                    deptid: userdetail?.departmentID || "",
                    refaid: billForm?.refaid || "",
                }
            } if (!billForm.traid) {
                NewPaidAmt = parseFloat(billForm?.paidamt) || 0;
                NewAdvAmt = NewPaidAmt;
                NCBAL = 0;

                transectionPayloadList.push({
                    traid: TRAID,
                    trnsr: 1,
                    trnsid: "10",
                    trntid: "20",
                    trndt: userdetail?.APPDT || "",
                    cropid: "",
                    refaid: "",
                    maid: formData.Customer,
                    pamt: parseFloat(NewPaidAmt) || 0,
                    cbal: parseFloat(NCBAL) || 0,
                    advamt: parseFloat(NewAdvAmt) || 0,
                    asid: TRAID,
                    isdeleted: false,
                    companyid: userdetail?.companyID || "",
                    deptid: userdetail?.departmentID || "",
                    PAYMENTDT: userdetail?.APPDT || new Date().toISOString()
                });
            }

            else {
                if ((parseFloat(billForm?.paidamt) || 0) > (parseFloat(DATA?.pendingrecive) || 0)) {
                    NewAdvAmt = (parseFloat(billForm?.paidamt) || 0) - (parseFloat(DATA?.pendingrecive) || 0);
                    NewPaidAmt = NewAdvAmt;
                    NTRNSR = 1;
                    NTRNSID = "10";
                    NCBAL = 0;
                    NREFAID = '';
                    transectionPayloadList.push({
                        traid: billForm?.trdaid || TRAID,
                        trnsr: NTRNSR,
                        trnsid: NTRNSID,
                        trntid: "20",
                        trndt: userdetail?.APPDT || "",
                        cropid: billForm?.cropid || "",
                        refaid: NREFAID,
                        maid: billForm?.maid || formData.Customer,
                        pamt: parseFloat(NewPaidAmt) || 0,
                        cbal: parseFloat(NCBAL) || 0,
                        advamt: parseFloat(NewAdvAmt) || 0,
                        asid: billForm?.traid || TRAID,
                        isdeleted: false,
                        companyid: userdetail?.companyID || "",
                        deptid: userdetail?.departmentID || "",
                        PAYMENTDT: userdetail?.APPDT || new Date().toISOString()
                    });
                    transectionPayloadList.push({
                        traid: ACSPLGUID.getNew(),
                        trnsr: (parseFloat(billForm?.trnsr) || 0) + 1,
                        trnsid: '20',
                        trntid: "20",
                        trndt: userdetail?.APPDT || "",
                        cropid: billForm?.cropid || "",
                        refaid: billForm?.refaid || "",
                        maid: billForm?.maid || formData.Customer,
                        pamt: parseFloat(DATA?.pendingrecive) || 0,
                        cbal: 0,
                        advamt: 0,
                        asid: billForm?.traid || TRAID,
                        isdeleted: false,
                        companyid: userdetail?.companyID || "",
                        deptid: userdetail?.departmentID || "",
                        PAYMENTDT: userdetail?.APPDT || new Date().toISOString()
                    });

                    statusPayload = {
                        companyid: userdetail?.companyID || "",
                        deptid: userdetail?.departmentID || "",
                        maid: billForm?.maid || formData.Customer,
                        refaid: billForm?.refaid || "",
                        cropid: billForm?.cropid || "",
                    };
                    transportstatusPayload = {
                        companyid: userdetail?.companyID || "",
                        deptid: userdetail?.departmentID || "",
                        refaid: billForm?.refaid || "",
                    }

                } else {
                    NewAdvAmt =
                        (parseFloat(DATA?.pendingrecive) || 0) - (parseFloat(DATA?.advanceamt) || 0) >= 0
                            ? 0
                            : parseFloat(DATA?.advanceamt) || 0;

                    NewPaidAmt = parseFloat(billForm?.paidamt) || 0;
                    NTRNSR = (parseFloat(billForm?.trnsr) || 0) + 1;
                    NTRNSID = "20";
                    NCBAL = Math.abs(
                        (parseFloat(DATA?.pendingrecive) || 0) - (parseFloat(billForm?.paidamt) || 0)
                    );
                    NREFAID = billForm?.refaid || "";
                    transectionPayloadList.push({
                        traid: billForm?.trdaid || TRAID,
                        trnsr: NTRNSR,
                        trnsid: NTRNSID,
                        trntid: "20",
                        trndt: userdetail?.APPDT || "",
                        cropid: billForm?.cropid || "",
                        refaid: NREFAID,
                        maid: billForm?.maid || formData.Customer,
                        pamt: parseFloat(NewPaidAmt) || 0,
                        cbal: parseFloat(NCBAL) || 0,
                        advamt: parseFloat(NewAdvAmt) || 0,
                        asid: billForm?.traid || TRAID,
                        isdeleted: false,
                        companyid: userdetail?.companyID || "",
                        deptid: userdetail?.departmentID || "",
                        PAYMENTDT: userdetail?.APPDT || new Date().toISOString()
                    });
                    if (parseFloat(NCBAL) == 0) {
                        statusPayload = {
                            companyid: userdetail?.companyID || "",
                            deptid: userdetail?.departmentID || "",
                            maid: billForm?.maid || formData.Customer,
                            refaid: billForm?.refaid || "",
                            cropid: billForm?.cropid || "",
                        };
                        transportstatusPayload = {
                            companyid: userdetail?.companyID || "",
                            deptid: userdetail?.departmentID || "",
                            refaid: billForm?.refaid || "",
                        }
                    }

                }
            }
            console.log(transectionPayloadList, "transectionPayloadList");
            console.log(statusPayload, "statusPayload");

            // return;
            if (transectionPayloadList.length > 0) {
                await axios.post(
                    `${baseUrl.Url}/backend/api/SP_AddUpdAllTransections`,
                    transectionPayloadList,
                    { headers: { "Content-Type": "application/json" } }
                );
            }

            if (statusPayload && Object.keys(statusPayload).length > 0) {
                await axios.post(
                    `${baseUrl.Url}/backend/api/SP_UpdFarmerBillStatus`,
                    statusPayload,
                    { headers: { "Content-Type": "application/json" } }
                );
            }

            if (transportstatusPayload && Object.keys(transportstatusPayload).length > 0) {
                await axios.post(
                    `${baseUrl.Url}/backend/api/SP_UpdateTransportBillStatus`,
                    transportstatusPayload,
                    { headers: { "Content-Type": "application/json" } }
                );
            }


            setBillForm({
                trdaid: "",
                traid: "",
                trnsr: "",
                trnsid: "",
                appdt: defaultAppDate,
                cropid: "",
                refaid: "",
                maid: "",
                billamt: 0,
                paidamt: 0,
                cbal: 0,
                advanceamt: 0,
                extraadv: 0,
                status: false,
            });
            setBillForm({
                trdaid: "",
                traid: "",
                trnsr: "",
                trnsid: "",
                appdt: defaultAppDate,
                cropid: "",
                refaid: "",
                maid: "",
                billamt: 0,
                paidamt: 0,
                cbal: 0,
                advanceamt: 0,
                extraadv: 0,
                status: false,
            });

            Swal.fire({
                icon: "success",
                title: "Success",
                text: "Transaction saved successfully!",
                timer: 1500,
                showConfirmButton: false,
            });

            const tranPayload = {
                refaid: billForm?.refaid || "",
                maid: formData.Customer || "",
                companyid: userdetail?.companyID || "",
                deptid: userdetail?.departmentID || "",
            };

            const tranResponse = await axios.post(
                `${baseUrl.Url}/backend/api/GET_CustomerTranData`,
                tranPayload,
                { headers: { "Content-Type": "application/json", Accept: "*/*" } }
            );
            setDetailData(tranResponse.data);

            const billPayload = {
                maid: formData.Customer,
                companyid: userdetail?.companyID || "",
                deptid: userdetail?.departmentID || "",
            };

            const billResponse = await axios.post(
                `${baseUrl.Url}/backend/api/GET_CustomerTransportBillData`,
                billPayload,
                { headers: { "Content-Type": "application/json", Accept: "*/*" } }
            );

            if (billResponse.status !== 200) {
                throw new Error("Failed to fetch Farmer Bill Data");
            }

            setBillData(billResponse.data);
            setBillDetails(billResponse.data);
            let ROW = billResponse.data.filter(row =>
                row.refaid === (billForm?.refaid || "") &&
                row.maid === (formData.Customer || "")
            );

            handleProceed(ROW[0]);

        } catch (err) {
            console.error("Save failed:", err);
            Swal.fire({
                icon: "error",
                title: "Error",
                text: "Failed to save row details!",
            });
        }
    };

    const handleProceed = async (row) => {
        setBillForm({
            traid: row.traid || "",
            trdaid: "",
            trnsr: row.trnsr || 0,
            Narr: row.narr || "",
            trnsid: "",
            appdt: row.trndt || "",
            cropid: row.cropid || "",
            refaid: row.refaid || "",
            maid: row.maid || "",
            billamt: row.billamount || 0,
            paidamt: row.pendingrecive,
            cbal: row.cbal || 0,
            advanceamt: row.advanceamt || 0,
            extraadv: 0,

            status: false
        });
        setTimeout(() => {
            paidAmtRef.current?.focus();
        }, 100);
        try {
            const headers = { "Content-Type": "application/json", Accept: "*/*" };
            const payload = {
                refaid: row.refaid || "",
                maid: formData.Customer || "",
                companyid: userdetail?.companyID || "",
                deptid: userdetail?.departmentID || "",
            };
            const response = await axios.post(
                `${baseUrl.Url}/backend/api/GET_CustomerTranData`,
                payload,
                { headers }
            );
            setDetailData(response.data)

        } catch (error) {
            console.error("Error fetching crop data:", error);
        }

    };

    const handleDeleteRow = async (traid, refaid) => {
        if (!traid) return;

        try {
            Swal.fire({
                title: "⚠️ Are you sure?",
                text: "This record will be permanently deleted.",
                icon: "warning",
                showCancelButton: true,
                confirmButtonColor: "#d33",
                cancelButtonColor: "#3085d6",
                confirmButtonText: "Yes, delete it!",
                cancelButtonText: "Cancel",
            }).then(async (result) => {
                if (!result.isConfirmed) return;

                const payload = {
                    companyid: userdetail?.companyID || "",
                    deptid: userdetail?.departmentID || "",
                    traid: traid,
                    refaid: refaid,
                    maid: formData.Customer,
                };

                const headers = {
                    "Content-Type": "application/json",
                    Accept: "*/*",
                };

                const response = await axios.post(
                    `${baseUrl.Url}/backend/api/SP_DeleteFarmerTran`,
                    payload,
                    { headers }
                );

                if (response.status === 200) {
                    const result = response.data[0];

                    if (result.responseCode == "SUCCESS") {
                        Swal.fire({
                            title: "✅ Deleted!",
                            text: result.responseMessage,
                            icon: "success",
                            confirmButtonText: "OK",
                        });

                        try {

                            const billPayload = {
                                refaid: billForm?.refaid || "",
                                maid: formData.Customer || "",
                                companyid: userdetail?.companyID || "",
                                deptid: userdetail?.departmentID || "",
                            };

                            const billResponse = await axios.post(
                                `${baseUrl.Url}/backend/api/GET_CustomerTranData`,
                                billPayload,
                                { headers }
                            );

                            if (billResponse.status !== 200) {
                                throw new Error("Failed to fetch Farmer Bill Data");
                            }

                            setDetailData(billResponse.data);

                            const tranPayload = {
                                maid: formData.Customer,
                                companyid: userdetail?.companyID || "",
                                deptid: userdetail?.departmentID || "",
                            };

                            const tranResponse = await axios.post(
                                `${baseUrl.Url}/backend/api/GET_CustomerTransportBillData`,
                                tranPayload,
                                { headers }
                            );
                            setBillData(tranResponse.data);
                            setBillDetails(tranResponse.data);

                        } catch (err) {
                            console.error("Error refreshing farmer data:", err);
                            Swal.fire("❌ Error", "Failed to refresh farmer data.", "error");
                        }
                    } else {
                        Swal.fire({
                            title: "⚠️ Cannot Delete",
                            text: result.responseMessage,
                            icon: "error",
                            confirmButtonText: "OK",
                        });
                    }
                }
            });
        } catch (err) {
            console.error("Error deleting row:", err);
            Swal.fire({
                title: "❌ Error",
                text: "Something went wrong while deleting the record.",
                icon: "error",
                confirmButtonText: "OK",
            });
        }
    };


    const handleBack = () => {
        const from = location.state?.from;

        if (from === "MasterIndex") {
            navigate("/MasterIndex");
        } else if (from === "CropBillIndex") {
            navigate("/CropBillIndex");
        } else {
            navigate(-1); // fallback to last page
        }
    };
    return (
        <div>
            <div className="page-wrapper">
                <div className="content">
                    <div className="page-header" style={{ marginBottom: "0px" }}>
                        <div className="add-item d-flex">
                            <div className="page-title">
                                <h4>Transport Bill Summary</h4>
                            </div>
                        </div>
                        <div className="page-btn">
                            {/* <Link onClick={showExitAlert} className="btn btn-secondary">
                                <ArrowLeft className="me-2" />
                                Back
                            </Link> */}

                            <button onClick={handleBack} className="btn btn-secondary">
                                <ArrowLeft className="me-2" />
                                Back
                            </button>
                        </div>
                    </div>

                    <form>
                        <div className="card mbgcolor">
                            <div className="card-body add-product mbgcolor">
                                {!showForm && (
                                    <div className="row justify-content-center m-1">
                                        <div className="col-lg-3 col-md-6 col-sm-12 mb-3">
                                            <label className="form-label">Select Customer</label>
                                            <Select
                                                placeholder="Select Customer"
                                                classNamePrefix="react-select"
                                                options={Customers}
                                                name="Customer"
                                                openMenuOnFocus
                                                value={
                                                    Customers.find(
                                                        (c) => c.value === formData.Customer
                                                    ) || null
                                                }
                                                onChange={handleCustomerChange}
                                                ref={CustomerRef}
                                                menuPortalTarget={document.body}
                                                styles={{
                                                    menuPortal: (base) => ({ ...base, zIndex: 2000 }),
                                                    menu: (base) => ({ ...base, zIndex: 2000 }),
                                                }}
                                            />
                                        </div>
                                    </div>
                                )}

                                <div className="col-lg-12">
                                    <div className="modal-body-table">
                                        <div className="table-responsive">
                                            <div>
                                                <div style={{ maxHeight: "200px", overflowY: "auto" }}>
                                                    <table
                                                        className="table datanew table-bordered table-responsive-sm"
                                                        style={{ tableLayout: "fixed", width: "100%" }}
                                                    >
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
                                                                <th
                                                                    className="text-center"
                                                                    style={{
                                                                        width: "100px",
                                                                        fontSize: "12px",
                                                                        padding: "4px 6px",
                                                                        whiteSpace: "nowrap",
                                                                        verticalAlign: "middle",
                                                                    }}
                                                                >
                                                                    Date
                                                                </th>
                                                                <th
                                                                    className="text-center"
                                                                    style={{
                                                                        width: "90px",
                                                                        fontSize: "12px",
                                                                        padding: "4px 6px",
                                                                        whiteSpace: "nowrap",
                                                                        verticalAlign: "middle",
                                                                    }}
                                                                >
                                                                    Crop
                                                                </th>
                                                                <th
                                                                    className="text-center"
                                                                    style={{
                                                                        width: "120px",
                                                                        fontSize: "12px",
                                                                        padding: "4px 6px",
                                                                        whiteSpace: "nowrap",
                                                                        verticalAlign: "middle",
                                                                    }}
                                                                >
                                                                    Customer Name
                                                                </th>
                                                                <th
                                                                    className="text-center"
                                                                    style={{
                                                                        width: "120px",
                                                                        fontSize: "12px",
                                                                        padding: "4px 6px",
                                                                        whiteSpace: "nowrap",
                                                                        verticalAlign: "middle",
                                                                    }}
                                                                >
                                                                    Vehicle Number
                                                                </th>
                                                                <th
                                                                    className="text-center"
                                                                    style={{
                                                                        width: "100px",
                                                                        fontSize: "12px",
                                                                        padding: "4px 6px",
                                                                        whiteSpace: "nowrap",
                                                                        verticalAlign: "middle",
                                                                    }}
                                                                >
                                                                    Total Bill
                                                                </th>
                                                                <th
                                                                    className="text-center"
                                                                    style={{
                                                                        width: "120px",
                                                                        fontSize: "12px",
                                                                        padding: "4px 6px",
                                                                        whiteSpace: "nowrap",
                                                                        verticalAlign: "middle",
                                                                    }}
                                                                >
                                                                    Advance
                                                                </th>
                                                                <th
                                                                    className="text-center"
                                                                    style={{
                                                                        width: "120px",
                                                                        fontSize: "12px",
                                                                        padding: "4px 6px",
                                                                        whiteSpace: "nowrap",
                                                                        verticalAlign: "middle",
                                                                    }}
                                                                >
                                                                    Received
                                                                </th>
                                                                <th
                                                                    className="text-center"
                                                                    style={{
                                                                        width: "120px",
                                                                        fontSize: "12px",
                                                                        padding: "4px 6px",
                                                                        whiteSpace: "nowrap",
                                                                        verticalAlign: "middle",
                                                                    }}
                                                                >
                                                                    Pending
                                                                </th>
                                                                <th
                                                                    className="text-center"
                                                                    style={{
                                                                        width: "90px",
                                                                        fontSize: "12px",
                                                                        padding: "4px 6px",
                                                                        whiteSpace: "nowrap",
                                                                        verticalAlign: "middle",
                                                                    }}
                                                                >
                                                                    Action
                                                                </th>
                                                            </tr>
                                                        </thead>


                                                        <tbody>
                                                            {BillData && BillData.length > 0 ? (
                                                                BillData.map((row, index) => (
                                                                    <tr key={row.traid || index}>
                                                                        <td className="text-center" style={{
                                                                            width: "120px",
                                                                            whiteSpace: "nowrap",
                                                                            overflow: "hidden",
                                                                            textOverflow: "ellipsis",
                                                                        }}>
                                                                            {row.trndt || "-"}
                                                                        </td>
                                                                        <td className="text-center" style={{
                                                                            width: "120px",
                                                                            whiteSpace: "nowrap",
                                                                            overflow: "hidden",
                                                                            textOverflow: "ellipsis",
                                                                        }}>
                                                                            {row.cropname || "-"}
                                                                        </td>
                                                                        <td className="text-center" style={{
                                                                            width: "120px",
                                                                            whiteSpace: "nowrap",
                                                                            overflow: "hidden",
                                                                            textOverflow: "ellipsis",
                                                                        }}>
                                                                            {row.customername || "-"}
                                                                        </td>
                                                                        <td className="text-center" style={{
                                                                            width: "120px",
                                                                            whiteSpace: "nowrap",
                                                                            overflow: "hidden",
                                                                            textOverflow: "ellipsis",
                                                                        }}>
                                                                            {row.vehchno || "-"}
                                                                        </td>
                                                                        <td className="text-center" style={{
                                                                            width: "120px",
                                                                            whiteSpace: "nowrap",
                                                                            overflow: "hidden",
                                                                            textOverflow: "ellipsis",
                                                                        }}>
                                                                            {row.billamount || "0"}
                                                                        </td>
                                                                        <td className="text-center" style={{
                                                                            width: "120px",
                                                                            whiteSpace: "nowrap",
                                                                            overflow: "hidden",
                                                                            textOverflow: "ellipsis",
                                                                        }}>
                                                                            {row.advanceamt || "0"}
                                                                        </td>
                                                                        <td className="text-center" style={{
                                                                            width: "120px",
                                                                            whiteSpace: "nowrap",
                                                                            overflow: "hidden",
                                                                            textOverflow: "ellipsis",
                                                                        }}>
                                                                            {row.resivedamt || "0"}
                                                                        </td>
                                                                        <td className="text-center" style={{
                                                                            width: "120px",
                                                                            whiteSpace: "nowrap",
                                                                            overflow: "hidden",
                                                                            textOverflow: "ellipsis",
                                                                        }}>
                                                                            {row.pendingrecive || "0"}
                                                                        </td>
                                                                        <td className="text-center" style={{ width: "90px" }}>
                                                                            <Link
                                                                                to="#"
                                                                                onClick={() => openModal(row.pkid, row.traid)}
                                                                                data-bs-toggle="modal"
                                                                                data-bs-target="#add-units-category"
                                                                                className="me-2 p-2 rounded-circle d-inline-flex align-items-center justify-content-center"
                                                                                style={{
                                                                                    backgroundColor: "#f39c12",
                                                                                    color: "white",
                                                                                    fontSize: "18px",
                                                                                    width: "25px",
                                                                                    height: "25px",
                                                                                }}
                                                                                title="View Details"
                                                                            >
                                                                                <i className="feather-eye"></i>
                                                                            </Link>

                                                                            <Link
                                                                                to="#"
                                                                                className="me-2 p-2 rounded-circle d-inline-flex align-items-center justify-content-center"
                                                                                style={{
                                                                                    backgroundColor: "#28a745",
                                                                                    color: "white",
                                                                                    fontSize: "18px",
                                                                                    width: "25px",
                                                                                    height: "25px",
                                                                                }}
                                                                                title="Proceed"
                                                                                onClick={() => handleProceed(row)}
                                                                            >
                                                                                <i className="feather-arrow-right"></i>
                                                                            </Link>
                                                                        </td>
                                                                    </tr>
                                                                ))
                                                            ) : (
                                                                <tr>
                                                                    <td colSpan="9" className="text-center">
                                                                        Data Not Available
                                                                    </td>
                                                                </tr>
                                                            )}
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div className="border p-3 rounded shadow-sm mb-1 mt-2">
                                    <div className="row align-items-center mb-3">
                                        <div className="col-auto">
                                            <h4 style={{ color: "rgb(255 255 255)", margin: 0 }}>Payment Summary</h4>
                                        </div>
                                        <div className="col-md-4">
                                            <div
                                                className="p-1 rounded text-center fw-bold"
                                                style={{
                                                    backgroundColor: "rgb(224 231 228)",
                                                    fontSize: "15px",
                                                    color: "black",
                                                    border: "2px solid #343a40",
                                                }}
                                            >
                                                Total Bill Amount : ₹ {billForm.billamt}
                                            </div>
                                        </div>


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
                                                                <th className="text-center">Action</th>
                                                            </tr>
                                                        </thead>

                                                        <tbody>
                                                            {detailData.map((row) => (
                                                                <tr key={row.traid}>
                                                                    <td className="text-center">{row.trndt}</td>
                                                                    <td className="text-center">{row.narr}</td>
                                                                    <td className="text-center">{row.pamt}</td>
                                                                    <td className="text-center">
                                                                        <Link
                                                                            to="#"
                                                                            onClick={() => handleDeleteRow(row.traid, row.refaid)}
                                                                            className="me-2 p-2"
                                                                            style={{ color: "red" }}
                                                                        >
                                                                            <i className="feather-trash-2"></i>
                                                                        </Link>
                                                                    </td>

                                                                </tr>
                                                            ))}
                                                        </tbody>
                                                    </table>
                                                </div>

                                                {/* Highlighted Total Box */}
                                                <div
                                                    className="mt-1 p-2 rounded text-center fw-bold"
                                                    style={{
                                                        backgroundColor: "rgb(224 231 228)",
                                                        fontSize: "1rem",
                                                        color: "black",
                                                        border: "2px solid #343a40", // dark border
                                                    }}
                                                >
                                                    Total Paid Amount : {" "}
                                                    ₹ {detailData.reduce((sum, row) => sum + Number(row.pamt || 0), 0)}
                                                </div>


                                            </div>
                                        </div>
                                    </div>

                                    <div className="row mt-4">
                                        <div className="col-lg-12">
                                            <form onSubmit={handleSubmit}>
                                                <div className="row">
                                                    <div className="col-md-2 mb-3">
                                                        <label className="form-label"> Date</label>
                                                        <input
                                                            type="text"
                                                            className="form-control"
                                                            value={billForm.appdt}
                                                            readOnly
                                                        />
                                                    </div>

                                                    <div className="col-md-2 mb-3">
                                                        <label className="form-label">Bill Amount</label>
                                                        <input
                                                            type="number"
                                                            className="form-control"
                                                            value={billForm.billamt}
                                                            onChange={(e) => setBillForm({ ...billForm, billamt: e.target.value })}
                                                            readOnly
                                                        />
                                                    </div>
                                                    {/* <div className="col-md-2 mb-3">
                                                        <label className="form-label">Booking Amount</label>
                                                        <input
                                                            type="number"
                                                            className="form-control"
                                                            value={billForm.advanceamt}
                                                            onChange={(e) => setBillForm({ ...billForm, advanceamt: e.target.value })}
                                                            readOnly
                                                        />
                                                    </div> */}
                                                    <div className="col-md-2 mb-3">
                                                        <label className="form-label">Receipt Amount</label>
                                                        <input
                                                            type="number"
                                                            className="form-control"
                                                            value={billForm.paidamt}
                                                            onChange={(e) => setBillForm({ ...billForm, paidamt: e.target.value })}
                                                            ref={paidAmtRef}
                                                        />
                                                    </div>

                                                    <div className="col-md-6 mb-3">
                                                        <label className="form-label">Narration</label>
                                                        <input
                                                            type="text"
                                                            className="form-control"
                                                            value={billForm.Narr}
                                                            onChange={(e) => setBillForm({ ...billForm, Narr: e.target.value })}
                                                            ref={paidAmtRef}
                                                        />
                                                    </div>
                                                    {/* <div className="col-md-2 mb-3">
                                                        <label className="form-label">Additional Advance</label>
                                                        <input
                                                            type="number"
                                                            className="form-control"
                                                            value={billForm.extraadv}
                                                            onChange={(e) => setBillForm({ ...billForm, extraadv: e.target.value })}
                                                        />
                                                    </div> */}

                                                    <div className="col-lg-12 mt-3">
                                                        <div className="text-end">
                                                            <div className="row justify-content-end">
                                                                <div className="col-12 col-md-1 mb-2 order-1 order-md-2">
                                                                    <button
                                                                        type="submit"
                                                                        className="btn btn-primary w-100"
                                                                        onClick={handleSubmit}
                                                                    >
                                                                        Save
                                                                    </button>
                                                                </div>

                                                                <div className="col-12 col-md-1 mb-2 order-2 order-md-1">
                                                                    <button
                                                                        type="button"
                                                                        className="btn btn-secondary w-100"
                                                                        aria-label="Close"
                                                                        onClick={showExitAlert}
                                                                    >
                                                                        Exit
                                                                    </button>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </form>
                                        </div>
                                    </div>

                                </div>
                            </div>

                        </div>

                    </form>
                </div>
            </div>
        </div>
    );
}

export default TransportBillDetails;
