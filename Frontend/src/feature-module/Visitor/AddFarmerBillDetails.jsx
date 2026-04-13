import React, { useState, useRef, useEffect } from "react";
import { Link, useLocation } from "react-router-dom";
import { all_routes } from "../../Router/all_routes";
import withReactContent from "sweetalert2-react-content";
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import { ACSPLGUID, baseUrl, convertToISODate } from "../../core/json/custom";
import Swal from "sweetalert2";
import Select from "react-select";
import { ArrowLeft } from "feather-icons-react/build/IconComponents";
import { getUserData } from "../../Context/UserData";
import { OverlayTrigger, Tooltip } from "react-bootstrap";
import AddCropCollect_1 from "./AddCropCollect_1";
import BillReceipt from "./BillReceipt";

function AddFarmerBillDetails() {
    const location = useLocation();
    const navigate = useNavigate();
    const { userdetail } = getUserData();
    const [showForm, setShowForm] = useState(false);
    const [Farmers, setFarmers] = useState([]);
    const [allFarmers, setAllFarmers] = useState([]);
    const [CropType, setCropType] = useState([]);
    const [filteredCropType, setFilteredCropType] = useState([]);
    const [CropData, setCropData] = useState([]);
    const [farmercrop, setfarmercrop] = useState([]);
    const [tabledata, settabledata] = useState([]);
    const [tabledata1, settabledata1] = useState([]);
    const [billno, setbillno] = useState([]);
    const [Fname, setFname] = useState([]);
    const [phone, setPhoneNumber] = useState([]);
    const [Datee, setDate] = useState([]);
    const [primary, setID] = useState("");
    const [mobileSearch, setMobileSearch] = useState("");
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
        paymentdate: "",
        status: false
    });
    const [defaultAppDate, setDefaultAppDate] = useState("");
    const [defaultPaymentDate, setDefaultPaymentDate] = useState("");
    useEffect(() => {
        // Use APPDT from userdetail if available, otherwise use current date
        const dateToUse = userdetail?.APPDT ? new Date(userdetail.APPDT) : new Date();

        const formattedDate = dateToUse.toLocaleDateString("en-GB", {
            day: "2-digit",
            month: "short",
            year: "numeric",
        });

        // Payment date = APPDT minus 1 day
        const paymentDateObj = new Date(dateToUse);
        paymentDateObj.setDate(paymentDateObj.getDate() - 1);
        const paymentIsoDate = paymentDateObj.toISOString().split('T')[0];

        // Format for HTML5 date input (YYYY-MM-DD)
        const isoDate = dateToUse.toISOString().split('T')[0];

        setDefaultAppDate(formattedDate);
        setDefaultPaymentDate(paymentIsoDate);

        setBillForm((prev) => ({ ...prev, appdt: formattedDate, paymentdate: paymentIsoDate }));
    }, [userdetail]);


    useEffect(() => {
        // Function to forcibly remove all modal remnants
        const cleanupModal = () => {
            // Remove the 'modal-open' class from the body
            document.body.classList.remove("modal-open");
            document.body.style.overflow = "";
            document.body.style.paddingRight = "";

            // Remove any leftover backdrop elements created by Bootstrap
            const backdrops = document.querySelectorAll(".modal-backdrop");
            backdrops.forEach(backdrop => backdrop.remove());

            // Also ensure the temporary landscape class is gone on navigation/unmount
            document.body.classList.remove('force-landscape-view');
        };

        // Cleanup runs when the component unmounts (i.e., when navigating away via back/forward)
        return () => {
            cleanupModal();
        };
    }, [location]);
    const [detailData, setDetailData] = useState([]);
    const route = all_routes;
    const farmerRef = useRef();
    const paidAmtRef = useRef(null);
    const cropRef = useRef();

    const [formData, setFormData] = useState({
        farmer: "",
        crop: ""
    });

    const MySwal = withReactContent(Swal);

    useEffect(() => {
        const modal = document.getElementById("BillDetails");
        if (modal) {
            const handleOpen = () => {
                if (farmerRef.current) farmerRef.current.focus();
            };
            modal.addEventListener("shown.bs.modal", handleOpen);
            return () => modal.removeEventListener("shown.bs.modal", handleOpen);
        }
    }, []);

    useEffect(() => {
        const fetchFarmer = async () => {
            try {
                const headers = { "Content-Type": "application/json", Accept: "*/*" };
                const payload = { companyid: userdetail?.companyID || "" };
                const response = await axios.post(
                    `${baseUrl.Url}/backend/api/GET_FarmerName`,
                    payload,
                    { headers }
                );
                const data = response.data.map(({ fname, faid, fcontactno }) => ({
                    label: fname,
                    value: faid,
                    phone: fcontactno || ""
                }));
                setFarmers(data);
                setAllFarmers(data);
            } catch (error) {
                console.error("Error fetching Farmer data:", error);
            }
        };
        fetchFarmer();
    }, [userdetail]);

    useEffect(() => {
        const fetchCrops = async () => {
            try {
                const headers = { "Content-Type": "application/json", Accept: "*/*" };
                const payload = {
                    companyid: userdetail?.companyID || "",
                    deptid: userdetail?.departmentID || "",
                };
                const response = await axios.post(
                    `${baseUrl.Url}/backend/api/GETItemDropdown`,
                    payload,
                    { headers }
                );
                const cropDropdown = response.data.map(({ itemaid, itemne }) => ({
                    label: itemne,
                    value: itemaid,
                }));
                setCropType(cropDropdown);
            } catch (error) {
                console.error("Error fetching crop data:", error);
            }
        };
        fetchCrops();
    }, [userdetail]);

    useEffect(() => {
        const filterCropsByFarmer = async () => {
            if (formData.farmer) {
                try {
                    const headers = { "Content-Type": "application/json", Accept: "*/*" };
                    const payload = {
                        maid: formData.farmer,
                        companyid: userdetail?.companyID || "",
                        deptid: userdetail?.departmentID || "",
                    };
                    const response = await axios.post(
                        `${baseUrl.Url}/backend/api/GET_FarmerCrops`,
                        payload,
                        { headers }
                    );
                    const farmerCrops = response.data.map(({ cropid, cropname }) => ({
                        label: cropname,
                        value: cropid,
                    }));
                    setFilteredCropType(farmerCrops.length > 0 ? farmerCrops : CropType);
                } catch (error) {
                    console.error("Error fetching farmer crops:", error);
                    setFilteredCropType(CropType);
                }
            } else {
                setFilteredCropType([]);
            }
        };
        filterCropsByFarmer();
    }, [formData.farmer, CropType, userdetail]);

    useEffect(() => {
        if (mobileSearch.trim() === "") {
            setFarmers(allFarmers);
        } else {
            const filtered = allFarmers.filter(farmer =>
                farmer.phone && farmer.phone.includes(mobileSearch)
            );
            setFarmers(filtered);

            // Auto-select farmer when exact 10-digit mobile match found
            if (mobileSearch.length === 10) {
                const exactMatch = allFarmers.find(farmer =>
                    farmer.phone === mobileSearch
                );
                if (exactMatch) {
                    setFormData(prev => ({
                        ...prev,
                        farmer: exactMatch.value,
                        crop: "",
                    }));
                }
            }
        }
    }, [mobileSearch, allFarmers]);

    useEffect(() => {
        if (formData.farmer && formData.crop) {
            const fetchCropDetails = async () => {
                try {
                    const payload = {
                        "maid": formData.farmer,
                        "cropid": formData.crop,
                        "companyid": userdetail?.companyID ? userdetail.companyID : "",
                        "deptid": userdetail?.departmentID ? userdetail.departmentID : "",
                    }
                    const headers = {
                        "Content-Type": "application/json",
                        Accept: "*/*",
                    };
                    axios({
                        method: "POST",
                        url: baseUrl.Url + "/backend/api/GET_FarmerCropBillData",
                        data: JSON.stringify(payload),
                        headers: headers,
                    })
                        .then((response) => {
                            if (response.status != 200) throw new Error("Failed to send otp");
                            console.log("response", response.data);
                            const DATA = response.data;

                            console.log("XXXXXXXXXXXXXXXXXXXXXX", DATA)
                            console.log("XXXXXXXXXXXXXXXXXXXXXX DATA.fname", DATA[0]?.pkid)
                            setCropData(DATA);

                            // settabledata(DATA)
                            setfarmercrop(DATA);
                            setID(DATA[0]?.pkid);
                            setbillno(DATA[0]?.billno)
                            setFname(DATA[0]?.fname)
                            setDate(DATA[0]?.trndt)

                        })
                } catch (error) {
                    console.error("Error fetching HSN data:", error);
                }
            };
            fetchCropDetails();
        }
    }, [formData.farmer, formData.crop, userdetail]);

    // useEffect(() => {
    //     // if (formData.farmer && formData.crop) {


    //     const fetchMasterData = async () => {
    //         // console.log("formData.crop formData.crop", formData.crop)
    //         // console.log("formData.farmer formData.farmer", formData.farmer)
    //         try {
    //             const payload1 = {
    //                 cropid: primary,
    //                 // keyword: "%",
    //                 companyid: userdetail?.companyID || "",
    //                 deptid: userdetail?.departmentID || "",
    //             };

    //             const headers = {
    //                 "Content-Type": "application/json",
    //                 Accept: "*/*"
    //             };

    //             const response1 = await axios.post(
    //                 baseUrl.Url + "/backend/api/GET_CropBillData",
    //                 payload1,
    //                 { headers }
    //             );

    //             // if (response1.status !== 200) throw new Error("Failed to fetch data");
    //             // console.log("response1.data response1.data response1.data", response1.data)
    //             // let apiData = response1.data;
    //             //  setID(DATA[0]?.pkid);
    //             // setbillno(apiData[0]?.billno)
    //             // setFname(apiData[0]?.farmername)
    //             // setDate(apiData[0]?.visitingdate)
    //             // setPhoneNumber(apiData[0]?.phonenumber)

    //             // settabledata(apiData)
    //             // settabledata(prev => [...prev, ...apiData]);


    //             if (response1.data.length > 0) {
    //                 const mappedProducts = response1.data.map((item) => ({
    //                     // vdaid: item.vdaid,
    //                     // cropType: item.croptype,
    //                     // cropvariety: item.cropvariety,
    //                     // croparea: item.croparea,
    //                     // expectedharvestingdate: convertToISODate(item.expectedharvestingdate),
    //                     // expectedproduction: item.expectedproduction,
    //                     // photoes: item.photoes ? item.photoes.split(",") : [],
    //                     // rate: item.rate,
    //                     // advancemoney: item.advancemoney,
    //                     // isdeleted: item.isdeleted,
    //                     cdaid: item.cdaid,
    //                     cropid: item.cropid,
    //                     ScheduleDate: item.visitingdate,
    //                     VehicleNo: item.vehno,
    //                     packer: item.packername,
    //                     labour: item.workername,
    //                     packingsize: item.psize,
    //                     Quantity: item.pquntity,
    //                     invert: item.cropinvert,
    //                     Weight: item.weight,
    //                     rate: item.rate,
    //                     isdeleted: item.isdeleted,


    //                 }));
    //                 settabledata(mappedProducts);
    //             }
    //             // settabledata(prev => ({
    //             //     ...prev,
    //             //     cropid: apiData?.cropid || "",
    //             //     faid: apiData?.faid || "",
    //             //     StartDate: apiData?.visitingdate || "",
    //             //     PhoneNo: apiData?.contactno || "",
    //             //     FarmerName: apiData?.farmername || "",
    //             //     faid: apiData?.faid || "",
    //             //     CropName: apiData?.cropname || "",
    //             //     CropName: apiData?.ctype || "",
    //             //     CropArea: apiData?.carea || "",
    //             //     CropVariety: apiData?.cvariety || "",
    //             //     ExpectedHarvesting: apiData?.eproduction || "",
    //             //     Rate: apiData?.rate || 0,
    //             //     Weight: apiData?.weight || 0,
    //             //     caret: apiData?.caret || "",
    //             //     Amount: apiData?.amount || 0,
    //             //     Narration: apiData?.narr || "",
    //             // }));
    //             console.log("BBBBBBBBBBBBBBBBBB", apiData)
    //             console.log("BBBBBBBBBBBBBBBBBB DATA.fname", apiData.contactno)
    //             // settabledata(apiData?.cropid)
    //         } catch (error) {
    //             console.error("Error in Master API Call:", error);
    //         }

    //     };
    //     // const fetchCropDetails = async () => {
    //     //     try {
    //     //         const payload = {

    //     //             "cropid": formData.crop,
    //     //             "companyid": userdetail?.companyID ? userdetail.companyID : "",
    //     //             "deptid": userdetail?.departmentID ? userdetail.departmentID : "",
    //     //         }
    //     //         const headers = {
    //     //             "Content-Type": "application/json",
    //     //             Accept: "*/*",
    //     //         };
    //     //         axios({
    //     //             method: "POST",
    //     //             url: baseUrl.Url + "/backend/api/GET_CropBill",
    //     //             data: JSON.stringify(payload),
    //     //             headers: headers,
    //     //         })
    //     //             .then((response) => {
    //     //                 if (response.status != 200) throw new Error("Failed to send otp");
    //     //                 console.log("response", response.data);
    //     //                 const apiData = response.data;
    //     //                 settabledata(prev => [...prev, apiData]);
    //     //                 // settabledata(prev => ({
    //     //                 //     ...prev,
    //     //                 //     cbkid: apiData.cbkid,
    //     //                 //     cropid: apiData.cropid,
    //     //                 //     cbkid: apiData.cbkid,
    //     //                 //     cdaid: apiData.cdaid,
    //     //                 //     StartDate: apiData.visitingdate,
    //     //                 //     PhoneNo: apiData.phonenumber,
    //     //                 //     FarmerName: apiData.farmername,
    //     //                 //     faid: apiData.faid || "",
    //     //                 //     CropName: apiData.cropname,
    //     //                 //     ctype: apiData.ctype,
    //     //                 //     CropArea: apiData.carea,
    //     //                 //     CropVariety: apiData.cvariety,
    //     //                 //     ExpectedHarvesting: apiData.eproduction,
    //     //                 //     Rate: apiData.rate,
    //     //                 //     Weight: apiData.weight,
    //     //                 //     caret: apiData.caret,
    //     //                 //     Amount: apiData.amount,
    //     //                 //     Narration: apiData.narr,
    //     //                 // }));

    //     //                 console.log("BBBBBBBBBBBBBBBBBB", apiData)
    //     //                 console.log("BBBBBBBBBBBBBBBBBB DATA.fname", apiData.phonenumber)
    //     //                 // setCropData(DATA);

    //     //                 // settabledata(DATA)
    //     //                 // setfarmercrop(DATA);
    //     //                 // setbillno(DATA[0]?.billno)
    //     //                 // setFname(DATA[0]?.fname)
    //     //                 // setDate(DATA[0]?.trndt)

    //     //             })
    //     //     } catch (error) {
    //     //         console.error("Error fetching HSN data:", error);
    //     //     }
    //     // };
    //     fetchMasterData();
    //     // }
    // }, [primary]);
    useEffect(() => {
        if (primary) {
            fetchbill();
        }
    }, [primary]);
    const fetchbill = async () => {
        try {
            const payload = {
                cropid: primary,
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
                setFname(data.farmername);
                setDate(data.date);
                setPhoneNumber(data.contactno);
                settabledata(response.data);
                setfarmercrop(data.ctype);
                setbillno(data.billno);
                console.log("response.data response.data response.data111", response.data)
            } else {
                console.warn("GET_CropInvertBill returned empty or invalid data:", response.data);
            }
        } catch (error) {
            console.error("Error fetching Bill data:", error);
        }
    };

    const [selectedData, setSelectedData] = useState({ cropid: null, cdaid: null });
    const openModal = (cropid, cdaid) => {
        setSelectedData({ cropid: cropid, cdaid: cdaid });
        console.log("View clicked for:", cropid, cdaid);
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
        if (!billForm.paidamt || billForm.paidamt == 0) {
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
            // const DATA = farmercrop.find(a => a.traid === billForm.traid);


            const billPayload1 = {
                maid: formData.farmer,
                cropid: formData.crop,
                companyid: userdetail?.companyID || "",
                deptid: userdetail?.departmentID || "",
            };

            const billResponse1 = await axios.post(
                `${baseUrl.Url}/backend/api/GET_FarmerCropBillData`,
                billPayload1,
                { headers: { "Content-Type": "application/json", Accept: "*/*" } }
            );
            const DATA1 = billResponse1.data;
            const DATA = DATA1
                ? DATA1.find(a => a.traid === billForm.traid)
                : null;
            const TRAID = ACSPLGUID.getNew();

            let statusPayload = {};
            let cropstatusPayload = {};
            let transectionPayloadList = [];
            let NewAdvAmt = 0;
            let NewPaidAmt = 0;
            let NTRNSR = 1;
            let NTRNSID = "10";
            let NCBAL = 0;
            let NREFAID = '';

            // Case 1: New Advance/Booking Payment (No existing transaction)
            if (!billForm.traid) {
                NewPaidAmt = parseFloat(billForm?.paidamt) || 0;
                NewAdvAmt = NewPaidAmt;
                NCBAL = 0;

                transectionPayloadList.push({
                    traid: TRAID,
                    trnsr: 1,
                    trnsid: "30",  // Booking/Advance payment
                    trntid: "10",
                    trndt: userdetail?.APPDT || "",
                    cropid: billForm?.cropid || formData.crop,
                    refaid: billForm?.refaid || "",
                    maid: billForm?.maid || formData.farmer,
                    pamt: parseFloat(NewPaidAmt) || 0,
                    cbal: parseFloat(NCBAL) || 0,
                    advamt: parseFloat(NewAdvAmt) || 0,
                    asid: TRAID,
                    isdeleted: false,
                    paymentdt: billForm?.paymentdate || userdetail?.APPDT || new Date().toISOString(),
                    companyid: userdetail?.companyID || "",
                    deptid: userdetail?.departmentID || "",
                });

                // No status update for initial advance payment
            }

            // Case 2: Existing Transaction - Payment Processing
            else {
                // Case 2a: Overpayment - Payment amount > Payable amount
                if ((parseFloat(billForm?.paidamt) || 0) > (parseFloat(DATA?.payableamt) || 0)) {
                    const PayableAmount = parseFloat(DATA?.payableamt) || 0;
                    const ExtraAdvance = (parseFloat(billForm?.paidamt) || 0) - PayableAmount;

                    NTRNSR = (parseFloat(billForm?.trnsr) || 0) + 1;
                    NREFAID = billForm?.refaid || "";

                    // Transaction 1: Payment for the full payable amount (TRNSID=20)
                    transectionPayloadList.push({
                        traid: ACSPLGUID.getNew(),
                        trnsr: NTRNSR,
                        trnsid: '20',  // Payment transaction
                        trntid: "10",
                        trndt: userdetail?.APPDT || "",
                        cropid: billForm?.cropid || formData.crop,
                        refaid: NREFAID,
                        maid: billForm?.maid || formData.farmer,
                        pamt: PayableAmount,
                        cbal: 0,  // Balance is zero after this payment
                        advamt: 0,
                        asid: billForm?.traid || TRAID,
                        isdeleted: false,
                        paymentdt: billForm?.paymentdate || userdetail?.APPDT || new Date().toISOString(),
                        companyid: userdetail?.companyID || "",
                        deptid: userdetail?.departmentID || "",
                    });

                    // Transaction 2: Extra advance (TRNSID=30)
                    transectionPayloadList.push({
                        traid: ACSPLGUID.getNew(),
                        trnsr: NTRNSR + 1,
                        trnsid: '30',  // Extra advance
                        trntid: "10",
                        trndt: userdetail?.APPDT || "",
                        cropid: billForm?.cropid || formData.crop,
                        refaid: NREFAID,
                        maid: billForm?.maid || formData.farmer,
                        pamt: ExtraAdvance,
                        cbal: 0,
                        advamt: ExtraAdvance,
                        asid: billForm?.traid || TRAID,
                        isdeleted: false,
                        paymentdt: billForm?.paymentdate || userdetail?.APPDT || new Date().toISOString(),
                        companyid: userdetail?.companyID || "",
                        deptid: userdetail?.departmentID || "",
                    });

                    // Bill is fully paid - update status
                    statusPayload = {
                        companyid: userdetail?.companyID || "",
                        deptid: userdetail?.departmentID || "",
                        maid: billForm?.maid || formData.farmer,
                        refaid: NREFAID,
                        cropid: billForm?.cropid || formData.crop,
                    };
                    cropstatusPayload = {
                        companyid: userdetail?.companyID || "",
                        deptid: userdetail?.departmentID || "",
                        refaid: NREFAID,
                    };

                    // Case 2b: Normal/Partial Payment - Payment amount <= Payable amount
                } else {
                    NewPaidAmt = parseFloat(billForm?.paidamt) || 0;
                    NTRNSR = (parseFloat(billForm?.trnsr) || 0) + 1;
                    NTRNSID = "20";  // Payment transaction
                    NCBAL = Math.max(0, (parseFloat(DATA?.payableamt) || 0) - NewPaidAmt);
                    NREFAID = billForm?.refaid || "";

                    // Calculate remaining advance amount (if any)
                    NewAdvAmt =
                        (parseFloat(DATA?.payableamt) || 0) - (parseFloat(DATA?.advamt) || 0) >= 0
                            ? 0
                            : parseFloat(DATA?.advamt) || 0;

                    // Payment transaction
                    transectionPayloadList.push({
                        traid: ACSPLGUID.getNew(),  // Always create new TRAID for payment
                        trnsr: NTRNSR,
                        trnsid: NTRNSID,  // TRNSID = 20 for payment
                        trntid: "10",
                        trndt: userdetail?.APPDT || "",
                        cropid: billForm?.cropid || formData.crop,
                        refaid: NREFAID,
                        maid: billForm?.maid || formData.farmer,
                        pamt: NewPaidAmt,
                        cbal: NCBAL,
                        advamt: NewAdvAmt,
                        asid: billForm?.traid || TRAID,
                        isdeleted: false,
                        paymentdt: billForm?.paymentdate || userdetail?.APPDT || new Date().toISOString(),
                        companyid: userdetail?.companyID || "",
                        deptid: userdetail?.departmentID || "",
                    });

                    // Update status only if bill is fully paid (CBAL = 0)
                    if (NCBAL === 0) {
                        statusPayload = {
                            companyid: userdetail?.companyID || "",
                            deptid: userdetail?.departmentID || "",
                            maid: billForm?.maid || formData.farmer,
                            refaid: NREFAID,
                            cropid: billForm?.cropid || formData.crop,
                        };
                        cropstatusPayload = {
                            companyid: userdetail?.companyID || "",
                            deptid: userdetail?.departmentID || "",
                            refaid: NREFAID,
                        };
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

            if (cropstatusPayload && Object.keys(cropstatusPayload).length > 0) {
                await axios.post(
                    `${baseUrl.Url}/backend/api/SP_UpdFCropBillStatus`,
                    cropstatusPayload,
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
                paymentdate: defaultPaymentDate,
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
                maid: formData.farmer || "",
                companyid: userdetail?.companyID || "",
                deptid: userdetail?.departmentID || "",
            };

            const tranResponse = await axios.post(
                `${baseUrl.Url}/backend/api/GET_FarmerPaymentSummary`,
                tranPayload,
                { headers: { "Content-Type": "application/json", Accept: "*/*" } }
            );
            setDetailData(tranResponse.data);

            const billPayload = {
                maid: formData.farmer,
                cropid: formData.crop,
                companyid: userdetail?.companyID || "",
                deptid: userdetail?.departmentID || "",
            };

            const billResponse = await axios.post(
                `${baseUrl.Url}/backend/api/GET_FarmerCropBillData`,
                billPayload,
                { headers: { "Content-Type": "application/json", Accept: "*/*" } }
            );

            if (billResponse.status !== 200) {
                throw new Error("Failed to fetch Farmer Bill Data");
            }

            setCropData(billResponse.data);
            setfarmercrop(billResponse.data);
            let ROW = billResponse.data.filter(row =>
                row.refaid === (billForm?.refaid || "") &&
                row.maid === (formData.farmer || "")
            );

            if (ROW && ROW.length > 0) {
                handleProceed(ROW[0]);
            }
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
            trnsid: "",
            appdt: defaultAppDate || "",
            cropid: row.cropid || "",
            refaid: row.refaid || "",
            maid: row.maid || "",
            Narr: row.narr || "",
            billamt: row.billamount || 0,
            paidamt: row.payableamt,
            cbal: row.cbal || 0,
            advanceamt: row.advanceamt || 0,
            extraadv: 0,
            paymentdate: defaultPaymentDate,
            status: false
        });
        setTimeout(() => {
            paidAmtRef.current?.focus();
        }, 100);
        try {
            const headers = { "Content-Type": "application/json", Accept: "*/*" };
            const payload = {
                refaid: row.refaid || "",
                maid: formData.farmer || "",
                companyid: userdetail?.companyID || "",
                deptid: userdetail?.departmentID || "",
            };
            const response = await axios.post(
                `${baseUrl.Url}/backend/api/GET_FarmerPaymentSummary`,
                payload,
                { headers }
            );
            setDetailData(response.data)

        } catch (error) {
            console.error("Error fetching crop data:", error);
        }

    };

    const handlePaymentDateChange = (traid, newDate) => {
        setDetailData(prevData =>
            prevData.map(row =>
                row.traid === traid ? { ...row, paymentdate: newDate } : row
            )
        );
    };

    const updatePaymentDate = async (traid, newDate) => {
        try {
            const headers = { "Content-Type": "application/json", Accept: "*/*" };
            const payload = {
                traid: traid,
                paymentdt: newDate,
                companyid: userdetail?.companyID || "",
                deptid: userdetail?.departmentID || "",
            };

            const response = await axios.post(
                `${baseUrl.Url}/backend/api/GET_FarmerPaymentSummary/UpdatePaymentDate`,
                payload,
                { headers }
            );

            if (response.data.success) {
                Swal.fire({
                    icon: "success",
                    title: "Updated",
                    text: "Payment date updated successfully!",
                    timer: 1500,
                    showConfirmButton: false,
                });
            } else {
                Swal.fire({
                    icon: "error",
                    title: "Error",
                    text: "Failed to update payment date!",
                });
            }
        } catch (error) {
            console.error("Error updating payment date:", error);
            Swal.fire({
                icon: "error",
                title: "Error",
                text: "An error occurred while updating payment date!",
            });
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
                    maid: formData.farmer,
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
                                maid: formData.farmer,
                                cropid: formData.crop,
                                companyid: userdetail?.companyID || "",
                                deptid: userdetail?.departmentID || "",
                            };

                            const billResponse = await axios.post(
                                `${baseUrl.Url}/backend/api/GET_FarmerCropBillData`,
                                billPayload,
                                { headers }
                            );

                            if (billResponse.status !== 200) {
                                throw new Error("Failed to fetch Farmer Bill Data");
                            }

                            setCropData(billResponse.data);
                            setfarmercrop(billResponse.data);

                            const tranPayload = {
                                refaid: refaid || "",
                                maid: formData.farmer || "",
                                companyid: userdetail?.companyID || "",
                                deptid: userdetail?.departmentID || "",
                            };

                            const tranResponse = await axios.post(
                                `${baseUrl.Url}/backend/api/GET_FarmerPaymentSummary`,
                                tranPayload,
                                { headers }
                            );
                            setDetailData(tranResponse.data);

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


    const totalAmount = tabledata.reduce((sum, row) => {
        const rate = parseFloat(row.rate || 0);
        const weight = parseFloat(row.weight || 0);
        return sum + (rate * weight);
    }, 0);



    return (
        <div>
            <div className="page-wrapper">
                <div className="content">
                    <div className="page-header" style={{ marginBottom: "0px" }}>
                        <div className="add-item d-flex">
                            <div className="page-title">
                                <h4>Farmer Bill Summary</h4>
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
                                        <div className="mb-3 col-lg-4 col-md-6 col-sm-12">
                                            <label className="form-label">Search by Mobile Number</label>
                                            <input
                                                type="text"
                                                className="form-control"
                                                placeholder="Enter mobile number"
                                                value={mobileSearch}
                                                onChange={(e) => setMobileSearch(e.target.value)}
                                            />
                                        </div>
                                        <div className="mb-3 col-lg-4 col-md-6 col-sm-12">
                                            <label className="form-label">Select Farmer</label>
                                            <Select
                                                placeholder="Select Farmer"
                                                classNamePrefix="react-select"
                                                options={Farmers}
                                                name="farmer"
                                                openMenuOnFocus
                                                value={Farmers.find(f => f.value === formData.farmer) || null}
                                                onChange={(opt) => {
                                                    setFormData((prev) => ({
                                                        ...prev,
                                                        farmer: opt ? opt.value : "",
                                                        crop: "",
                                                    }))
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
                                                    setDetailData([]);
                                                }}
                                                ref={farmerRef}
                                                menuPortalTarget={document.body}
                                                styles={{
                                                    menuPortal: (base) => ({ ...base, zIndex: 2000 }),
                                                    menu: (base) => ({ ...base, zIndex: 2000 })
                                                }}
                                            />
                                        </div>

                                        <div className="col-lg-4 col-md-6 col-sm-12 mb-3">
                                            <label className="form-label">Select Crop</label>
                                            <Select
                                                placeholder={formData.farmer ? "Select Crop" : "Select Farmer first"}
                                                classNamePrefix="react-select"
                                                options={formData.farmer ? filteredCropType : []}
                                                name="crop"
                                                openMenuOnFocus
                                                value={filteredCropType.find(c => c.value === formData.crop) || null}
                                                onChange={(opt) => {
                                                    setFormData((prev) => ({
                                                        ...prev,
                                                        crop: opt ? opt.value : "",
                                                    }))
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
                                                    setDetailData([]);
                                                }}
                                                ref={cropRef}
                                                menuPortalTarget={document.body}
                                                styles={{
                                                    menuPortal: (base) => ({ ...base, zIndex: 2000 }),
                                                    menu: (base) => ({ ...base, zIndex: 2000 })
                                                }}
                                            />
                                        </div>

                                    </div>
                                )}
                                {formData.farmer && formData.crop &&
                                    <div className="row mt-3">
                                        <h4>Farmer Bill Summary</h4>
                                        <div className="col-lg-12">
                                            <div className="modal-body-table">
                                                <div className="table-responsive">
                                                    <div>
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
                                                                    <th className="text-center" style={{ height: "10px", width: "90px" }}>Date</th>
                                                                    <th className="text-center" style={{ height: "10px", width: "90px" }}>Bill No</th>
                                                                    <th className="text-center" style={{ height: "10px", width: "100px" }}>Crop </th>
                                                                    <th className="text-center" style={{ height: "10px", width: "100px" }}>Total Bill </th>
                                                                    <th className="text-center" style={{ height: "10px", width: "100px" }}>Advance </th>
                                                                    <th className="text-center" style={{ height: "10px", width: "100px" }}>Paid </th>
                                                                    <th className="text-center" style={{ height: "10px", width: "100px" }}>Payable </th>
                                                                    <th className="text-center" style={{ height: "10px", width: "90px" }}>Action</th>
                                                                </tr>
                                                            </thead>

                                                            <tbody>
                                                                {CropData && CropData.length > 0 ? (
                                                                    CropData.map((row, index) => (
                                                                        <tr key={row.traid || index}>
                                                                            <td className="text-center" style={{ height: "10px", width: "90px" }} >{row.trndt ? row.trndt : "-"}</td>
                                                                            <td className="text-center" style={{ height: "10px", width: "90px", wordBreak: "break-word" }}>{row.billno || "-"}</td>
                                                                            <td className="text-center" style={{ height: "10px", width: "100px", wordBreak: "break-word" }}>{row.cropname || "-"}</td>
                                                                            <td className="text-center" style={{ height: "10px", width: "100px" }} >{row.billamount || "0"}</td>
                                                                            <td className="text-center" style={{ height: "10px", width: "100px" }} >{row.advanceamt || "0"}</td>
                                                                            <td className="text-center" style={{ height: "10px", width: "100px" }} >{row.paidamt || "0"}</td>
                                                                            <td className="text-center" style={{ height: "10px", width: "100px" }} >{Math.max(0, parseFloat(row.payableamt) || 0)}</td>
                                                                            <td className="text-center" style={{ height: "10px", width: "90px" }} >
                                                                                <Link
                                                                                    to="#"
                                                                                    // onClick={() => openModal(row.pkid, row.traid)}
                                                                                    onClick={() => {
                                                                                        // 1. Fetch data and open the modal
                                                                                        openModal(row.pkid, row.traid);

                                                                                        // 2. Add the temporary class to the body for styling
                                                                                        document.body.classList.add('force-landscape-view');

                                                                                        // 3. Optional: Add an event listener to clean up the class when the modal closes
                                                                                        const modalElement = document.getElementById('print-receipt2');
                                                                                        if (modalElement) {
                                                                                            // Ensure the cleanup happens ONLY after the modal is hidden
                                                                                            modalElement.addEventListener('hidden.bs.modal', function handler() {
                                                                                                document.body.classList.remove('force-landscape-view');
                                                                                                modalElement.removeEventListener('hidden.bs.modal', handler);
                                                                                            }, { once: true });
                                                                                        }
                                                                                    }}
                                                                                    data-bs-toggle="modal"
                                                                                    data-bs-target="#print-receipt2"
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
                                                                                    {/* <Link
                                                                                                                    className="me-2 p-2"
                                                                                                                    to="#" data-bs-toggle="modal"
                                                                                                                    data-bs-target="#print-receipt2"
                                                                                                                    style={{ color: 'orange' }}
                                                                                                                    onClick={() => openModal(record.cropid, record.cdaid)}>
                                                                                                                    <i data-feather="eye" className="feather-eye"></i>
                                                                                                                </Link> */}



                                                                                    <i className="feather-eye"></i>
                                                                                </Link>

                                                                                <Link
                                                                                    to="#"
                                                                                    onClick={() => handleProceed(row)}
                                                                                    className="me-2 p-2 rounded-circle d-inline-flex align-items-center justify-content-center"
                                                                                    style={{
                                                                                        backgroundColor: "#28a745",
                                                                                        color: "white",
                                                                                        fontSize: "18px",
                                                                                        width: "25px",
                                                                                        height: "25px",
                                                                                    }}
                                                                                    title="Proceed"
                                                                                >
                                                                                    <i className="feather-arrow-right"></i>
                                                                                </Link>
                                                                            </td>
                                                                        </tr>
                                                                    ))
                                                                ) : (
                                                                    <tr>
                                                                        <td colSpan="7" className="text-center">Data Not Available</td>
                                                                    </tr>
                                                                )}
                                                            </tbody>
                                                        </table>

                                                        {/* Scrollable body */}
                                                        <div style={{ maxHeight: "200px", overflowY: "auto" }}>
                                                            <table className="table datanew table-bordered table-responsive-sm" style={{ tableLayout: "fixed", width: "100%" }}>

                                                            </table>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                }
                                {formData.farmer && formData.crop && (
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
                                                                    <th className="text-center">Transaction Date</th>
                                                                    <th className="text-center">Payment Date</th>
                                                                    <th className="text-center">Narration</th>
                                                                    <th className="text-center">Receipt Amount</th>
                                                                    <th className="text-center">Action</th>
                                                                </tr>
                                                            </thead>

                                                            <tbody>
                                                                {detailData.map((row) => (
                                                                    <tr key={row.traid}>
                                                                        <td className="text-center">{row.transactiondate}</td>
                                                                        <td className="text-center">{row.paymentdate}</td>
                                                                        <td className="text-center">{row.narration}</td>
                                                                        <td className="text-center">{row.paymentamount}</td>
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
                                                        ₹ {detailData.reduce((sum, row) => sum + Number(row.paymentamount || 0), 0)}
                                                    </div>




                                                </div>
                                            </div>
                                        </div>


                                        <div className="row mt-4">
                                            <div className="col-lg-12">
                                                <form onSubmit={handleSubmit}>
                                                    <div className="row">
                                                        <div className="col-md-3 mb-3">
                                                            <label className="form-label">Transaction Date</label>
                                                            <input
                                                                type="text"
                                                                className="form-control"
                                                                value={billForm.appdt}
                                                                readOnly
                                                            />
                                                        </div>

                                                        <div className="col-md-3 mb-3">
                                                            <label className="form-label">Payment Date</label>
                                                            <input
                                                                type="date"
                                                                className="form-control"
                                                                name="paymentdate"
                                                                value={billForm.paymentdate}
                                                                onChange={(e) => setBillForm({ ...billForm, paymentdate: e.target.value })}
                                                            />
                                                        </div>

                                                        <div className="col-md-3 mb-3">
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
                                                        <div className="col-md-3 mb-3">
                                                            <label className="form-label">Receipt Amount</label>
                                                            <input
                                                                type="number"
                                                                className="form-control"
                                                                value={billForm.paidamt}
                                                                onChange={(e) => setBillForm({ ...billForm, paidamt: e.target.value })}
                                                                ref={paidAmtRef}
                                                            />
                                                        </div>

                                                        <div className="col-md-12 mb-3">
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
                                )}
                            </div>
                        </div>
                    </form>
                </div>
            </div>
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
      body.force-landscape-view #print-receipt2 .modal-dialog {
        width: 100vh !important;
        height: 100vw !important;
        margin: 0 auto !important;
        display: flex !important;
        align-items: center;
        justify-content: center;
        overflow: hidden !important;
      }

      /* Rotated modal content */
      body.force-landscape-view #print-receipt2 .modal-content {
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
      body.force-landscape-view #print-receipt2 table {
        width: max-content !important;
        min-width: 100% !important;
      }
    }
  `}
            </style>


            <style>
                {`
@media print {
    @page {
        size: A5 portrait;
        margin: 0;
    }

    html, body {
        margin: 0 !important;
        padding: 0 !important;
        width: 148mm !important;
        height: 210mm !important;
        overflow: hidden !important;
    }

    body * {
        visibility: hidden !important;
    }

    #print-receipt2, #print-receipt2 * {
        visibility: visible !important;
    }

    #print-receipt2 {
        position: absolute !important;
        left: 0 !important;
        top: 0 !important;
        margin: 0 auto !important;
        width: 148mm !important;   /* A5 width */
        height: 210mm !important;  /* A5 height */
        padding: 5mm !important;
        background: white !important;
        overflow: hidden !important;
        display: flex !important;
        flex-direction: column;
        align-items: center;
        justify-content: flex-start;
        box-sizing: border-box;
    }

    #print-receipt2 .modal-dialog {
        max-width: 100% !important;
        width: 100% !important;
        height: auto !important;
        margin: 0 !important;
    }

    #print-receipt2 .modal-content {
        border: none !important;
        box-shadow: none !important;
        height: auto !important;
    }

    #print-receipt2 .modal-body {
        padding: 0 !important;
        font-size: 12px !important;
        height: auto !important;
        overflow: hidden !important;
    }

    #print-receipt2 .close {
        display: none !important;
    }

    /* Table formatting */
    #print-receipt2 table {
        page-break-inside: avoid !important;
        width: 100% !important;
        border-collapse: collapse !important;
    }

    #print-receipt2 tr {
        page-break-inside: avoid !important;
        page-break-after: auto !important;
    }
}
`}
            </style>

            <BillReceipt
                modalId="print-receipt2"
                billNo={billno}
                date={Datee}
                farmerName={Fname}
                mobileNo={phone}
                vehNo=""
                tableData={tabledata}
                totalAmount={totalAmount}
            />

            <AddCropCollect_1 CROPID={selectedData.cropid} cdaid={selectedData.cdaid} />
        </div >

    );
}

export default AddFarmerBillDetails;
