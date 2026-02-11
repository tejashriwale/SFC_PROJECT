import React, { useRef, useState, useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";

import Select from "react-select";
import "bootstrap/dist/css/bootstrap.min.css";
import {
    ArrowLeft,
    FilePlus
} from "feather-icons-react/build/IconComponents";
import Swal from "sweetalert2";
import withReactContent from "sweetalert2-react-content";
import axios from 'axios';
import { all_routes } from "../../Router/all_routes"
import { Accounts, ACSPLGUID, baseUrl, convertToISODate, formatToDateTimeLocal, formatDate1 } from "../../core/json/custom";

import {
    PlusCircle,
    Trash2,
    Edit
} from "feather-icons-react/build/IconComponents";
import { getUserData } from "../../Context/UserData"
import AddCashVyaparibharana from "./AddCashVyaparibharana";

const AddVyapariBharana = ({ editVpaid, onRefresh }) => {

    console.log(editVpaid, "VPAID from edit page")

    const MySwal = withReactContent(Swal);
    const { userdetail } = getUserData();
    const navigate = useNavigate();
    const route = all_routes;
    const GUID = ACSPLGUID.getNew();
    const vyapariRef = useRef(null);
    const txnmodeRef = useRef(null);
    const bharlelirakkamRef = useRef(null);
    const descriptionRef = useRef(null);
    const chequedateRef = useRef(null);
    const chequenumberRef = useRef(null);
    const khateRef = useRef(null);
    const [tableData, setTableData] = useState([]);
    const [txnmodeall, settxnmodeall] = useState([]);
    const [vyapari, setvyapari] = useState([]);
    const [BankName, setBankName] = useState([]);
    const [vyapariDueData, setVyapariDueData] = useState([]);

    const [masterData, setMasterData] = useState({
        vyapari: "",
        txnmode: "",
        khate: "",
        chequedate: "",
        chequenumber: "",
        vyapariname: "",
        phonenumber: "",
        companyname: "",
        GSTnumber: "",
        ekunyenerakkam: "",
        bharlelirakkam: "",
        urvaritrakkam: "",
        description: "",
        Date: convertToISODate(userdetail.APPDT),

    });

    const handleMasterInputChange = (e) => {
        const { name, value } = e.target;

        const updatedData = {
            ...masterData,
            [name]: value
        };

        const liveEkunyenerakkam = tableData.reduce((sum, row) => {
            const roundedTotal = Math.round(parseFloat(row.total) || 0);
            return sum + roundedTotal;
        }, 0);
        const bharlelirakkam = parseFloat(updatedData.bharlelirakkam) || 0;

        // Step 3: Validation
        if (bharlelirakkam > liveEkunyenerakkam) {
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: `भरलेली रक्कम (${bharlelirakkam}) ही एकूण येणे रक्कम (${liveEkunyenerakkam}) पेक्षा मोठी नसावी.`,
                confirmButtonText: 'OK',
            });
            return;
        }
        if (name === "ekunyenerakkam" || name === "bharlelirakkam") {
            updatedData.urvaritrakkam = (liveEkunyenerakkam - bharlelirakkam);
        }
        setMasterData(updatedData);
    };

    useEffect(() => {
        const RegulerVaypariName = async () => {
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
                    `${baseUrl.Url}/backend/api/GET_RegulerVaypariName`,
                    payload,
                    { headers }
                );
                if (response.status !== 200)
                    throw new Error("Failed to fetch GET_RegulerVaypariName data");

                const data = response.data;
                console.log("GET_RegulerVaypariName", data)
                const conuterData = data
                    .map(({ vname, vpaid }) => ({
                        label: vname,
                        value: vpaid
                    }));

                setvyapari(conuterData);
            } catch (error) {
                console.error("Error fetching GET_RegulerVaypariName:", error);
            }
        };
        RegulerVaypariName();
    }, []);

    useEffect(() => {
        const fetchBankName = async () => {
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
                    `${baseUrl.Url}/backend/api/GET_BankName`,
                    payload,
                    { headers }
                );
                if (response.status !== 200)
                    throw new Error("Failed to fetch vendor data");
                console.log("requisition setails", response.data)
                const data = response.data;
                const conuterData = data
                    .map(({ bankname, bankid }) => ({
                        label: bankname,
                        value: bankid
                    }));

                setBankName(conuterData);
            } catch (error) {
                console.error("Error fetching vendor data:", error);
            }
        };
        fetchBankName();
    }, []);

    // when i do edit from Vyapari bharana componant
    useEffect(() => {
        if (editVpaid) {
            handleSelect(editVpaid);
        }
    }, [editVpaid]);

    const handleSelect = async (VPAID) => {
        if (VPAID) {
            try {
                const payload = {

                    companyid: userdetail?.companyID ? userdetail.companyID : "",
                    deptid: userdetail?.departmentID ? userdetail.departmentID : "",
                    vshortname: VPAID,
                };
                const headers = {
                    'Content-Type': 'application/json',
                    Accept: '*/*',
                };
                const response = await axios.post(
                    `${baseUrl.Url}/backend/api/GET_VyaparijamaByVyapariDroDown`,
                    payload,
                    { headers }
                );
                if (response.status !== 200) throw new Error("Failed to fetch data");
                const apiData = response.data[0];

                setMasterData((prev) => ({
                    ...prev,

                    "vpaid": apiData?.vpaid || "",
                    "vyapariname": apiData?.vshortname || "",
                    "phonenumber": apiData?.vmoblie || "",
                    "companyname": apiData?.vcompname || "",
                    "GSTnumber": apiData?.vgstin || "",


                }));
                await fetchVyapariBharnaData(apiData.vpaid);
                console.log("GET_VyaparijamaByVyapariDroDown  Data:", masterData);
            } catch (error) {
                console.error('Error fetching GET_VyaparijamaByVyapariDroDown data:', error);
            }
        }
    };

    const fetchVyapariBharnaData = async (vpaid,) => {
        try {
            const payload = {
                "vpaid": vpaid,
                // "TRNDATE": new Date().toISOString().split('T')[0],
                "TRNDATE": userdetail.APPDT,
            }

            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };
            const response = await axios.post(
                `${baseUrl.Url}/backend/api/GET_VyapariBharanaData`,

                payload,
                { headers }
            );
            if (response.status !== 200)
                throw new Error("Failed to fetch fetchVyapariBharnaData data");
            console.log("fetchVyapariBharnaData", response.data)
            setTableData(response.data);

        } catch (error) {
            console.error("Error fetching fetchVyapariBharnaData data:", error);
        }
    };

    console.log(vyapariDueData, "vyapariDueData check here++++++++++++++++++");
    useEffect(() => {

        const fetchVyapariDueData = async () => {
            try {
                const payload = {
                    "vdaid": "%",
                };
                const headers = {
                    "Content-Type": "application/json",
                    Accept: "*/*",
                };
                const response = await axios.post(
                    `${baseUrl.Url}/backend/api/GET_VYAPARIDUEDATA`,
                    payload,
                    { headers }
                );
                if (response.status !== 200)
                    throw new Error("Failed to fetch ");
                console.log("Get_VyapariDueData", response.data)
                setVyapariDueData(response.data);
            } catch (error) {
                console.error("Error fetching Get_VyapariDueData data:", error);
            }
        };
        fetchVyapariDueData();

    }, []);

    useEffect(() => {
        const totalAAMT = tableData.reduce((sum, row) => {
            const roundedTotal = Math.round(parseFloat(row.total) || 0);
            return sum + roundedTotal;
        }, 0);

        setMasterData((prev) => ({
            ...prev,
            ekunyenerakkam: totalAAMT
        }));
    }, [tableData]);

    useEffect(() => {
        const TRANSTYPE = async () => {
            try {
                const response = await axios.get(
                    baseUrl.Url + "/backend/api/Implications/TRANSTYPE"
                );

                if (response.status !== 200) throw new Error("Failed to fetch implications data");

                const data = response.data;
                const implicationsDropdown = data.map(({ iTitle, iValue }) => ({
                    label: iTitle,
                    value: iValue,
                }));

                settxnmodeall(implicationsDropdown);
            } catch (error) {
                console.error("Error fetching implications transaction mode:", error);
            }
        };

        TRANSTYPE();
    }, []);


    const handleSubmit = (e) => {
        e.preventDefault(e);

        if (masterData.txnmode === "1") {
            const modal = document.getElementById("Cash");
            if (modal) {
                modal.classList.add("show");
                modal.style.display = "block";
                modal.setAttribute("aria-modal", "true");
                modal.setAttribute("role", "dialog");
                modal.removeAttribute("aria-hidden");

                // Prevent duplicate backdrops
                if (!document.querySelector('.modal-backdrop')) {
                    const backdrop = document.createElement("div");
                    backdrop.className = "modal-backdrop fade show";
                    document.body.appendChild(backdrop);
                }
                document.body.classList.add("modal-open");
                document.body.style.overflow = "hidden";
                document.body.style.paddingRight = "0px";
            }
        }
        else {
            showConfirmationAlert(e);
        }
        console.log("Master part Submitted:", masterData);
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

    // const handlePayloadSubmition = async () => {

    //     console.log("Master part Submitted from tableData:", tableData);
    //     try {
    //         const Payload1 = tableData
    //             .map((detail, index) => {
    //                 let tempRemaining = Math.round(parseFloat(masterData.bharlelirakkam || 0));
    //                 let useAmount = 0;
    //                 for (let i = 0; i <= index; i++) {
    //                     const rowTotal = Math.round(parseFloat(tableData[i].total || 0));
    //                     const amt = Math.min(tempRemaining, rowTotal);
    //                     if (i === index) useAmount = amt;
    //                     tempRemaining -= amt;
    //                 }

    //                 if (useAmount <= 0) return null; // ✅ skip row if useAmount is 0 or negative

    //                 let remaining = Math.round(parseFloat(masterData.bharlelirakkam || 0));
    //                 let totalTillRow = 0;
    //                 for (let i = 0; i <= index; i++) {
    //                     totalTillRow += Math.round(parseFloat(tableData[i].total || 0));
    //                 }

    //                 let covered = 0;
    //                 if (remaining >= totalTillRow) {
    //                     covered = Math.round(detail.vapasI_AMT || 0);
    //                 }

    //                 const AMT = Math.round(detail.vapasI_AMT) + Math.round(detail.total);
    //                 const AMT1 = Math.round(useAmount) + Math.round(covered);
    //                 const BPAMT = Math.round(AMT - AMT1) || 0;

    //                 let TRNTYPE = '';
    //                 if (masterData.txnmode === '0') TRNTYPE = '2';
    //                 if (masterData.txnmode === '1') TRNTYPE = '0';
    //                 if (masterData.txnmode === '2') TRNTYPE = '1';

    //                 return {
    //                     vdaid: ACSPLGUID.getNew(),
    //                     trndt: userdetail.APPDT,
    //                     tamt: Math.round(useAmount) || 0,
    //                     bpamt: Math.round(BPAMT),
    //                     fduedt: detail.fduedt || "",
    //                     sduedt: detail.sduedt || "",
    //                     vpaid: detail.vpaid || "",
    //                     tid: detail.tid || "",
    //                     stid: detail.stid || "",
    //                     trntid: Math.round(BPAMT) === 0 ? "30" : "20",
    //                     isclose: Math.round(BPAMT) === 0,
    //                     trnsr: "",
    //                     refkey: "",
    //                     uaid: userdetail?.uaid || "",
    //                     isdeleted: detail.IsDeleted === 1 || detail.IsDeleted === true,
    //                     vreff: "",
    //                     reff: detail.vdaid || "",
    //                     vtype: "0",
    //                     vapasi: covered || 0,
    //                     trntype: TRNTYPE || "",
    //                     grpid: detail?.grpid || ""
    //                 };
    //             })
    //             .filter(p => p !== null);

    //         const PayloadVAPASI1 = tableData
    //             .map((detail, index) => {
    //                 let tempRemaining = Math.round(parseFloat(masterData.bharlelirakkam || 0));
    //                 let useAmount = 0;
    //                 for (let i = 0; i <= index; i++) {
    //                     const rowTotal = Math.round(parseFloat(tableData[i].total || 0));
    //                     const amt = Math.min(tempRemaining, rowTotal);
    //                     if (i === index) useAmount = amt;
    //                     tempRemaining -= amt;
    //                 }


    //                 let remaining = Math.round(parseFloat(masterData.bharlelirakkam || 0));
    //                 let totalTillRow = 0;
    //                 for (let i = 0; i <= index; i++) {
    //                     totalTillRow += Math.round(parseFloat(tableData[i].total || 0));
    //                 }

    //                 let covered = 0;
    //                 if (remaining >= totalTillRow) {
    //                     covered = Math.round(detail.vapasI_AMT || 0);
    //                 }

    //                 const AMT = Math.round(detail.vapasI_AMT) + Math.round(detail.total);
    //                 const AMT1 = Math.round(useAmount) + Math.round(covered);
    //                 const BPAMT = Math.round(AMT - AMT1) || 0;

    //                 let TRNTYPE = '';
    //                 if (masterData.txnmode === '0') TRNTYPE = '2';
    //                 if (masterData.txnmode === '1') TRNTYPE = '0';
    //                 if (masterData.txnmode === '2') TRNTYPE = '1';

    //                 if (useAmount <= 0) return null; // ✅ skip row if useAmount is 0 or negative
    //                 if (covered <= 0) return null; // ✅ skip row if useAmount is 0 or negative




    //                 return {
    //                     vdaid: ACSPLGUID.getNew(),
    //                     trndt: userdetail.APPDT,
    //                     tamt: Math.round(covered) || 0,
    //                     bpamt: Math.round(BPAMT),
    //                     fduedt: detail.fduedt || "",
    //                     sduedt: detail.sduedt || "",
    //                     vpaid: detail.vpaid || "",
    //                     tid: detail.tid || "",
    //                     stid: detail.stid || "",
    //                     trntid: "40",
    //                     isclose: Math.round(BPAMT) === 0,
    //                     trnsr: "",
    //                     refkey: "",
    //                     uaid: userdetail?.uaid || "",
    //                     isdeleted: detail.IsDeleted === 1 || detail.IsDeleted === true,
    //                     vreff: "",
    //                     reff: detail.vdaid || "",
    //                     vtype: "0",
    //                     vapasi: covered || 0,
    //                     trntype: TRNTYPE || "",
    //                     grpid: detail?.grpid || ""
    //                 };
    //             })
    //             .filter(p => p !== null);



    //         //payload2
    //         const dueUpdatePayload = Payload1.filter(p => p.bpamt === 0).map(p => ({
    //             vdaid: p.grpid || ""
    //         }));

    //         console.log(Payload1, "Payload1")
    //         console.log(PayloadVAPASI1, "PayloadVAPASI1")
    //         // console.log(Payload1,"Payload1")
    //         //payload3
    //         const voucherAID = ACSPLGUID.getNew();

    //         const Payload3 = Payload1.map(data => ({
    //             voucherAID: ACSPLGUID.getNew(),
    //             uaid: userdetail?.uaid || "",
    //             organizationID: userdetail?.companyID || "",
    //             divisionID: userdetail?.departmentID || "",
    //             voucherTID: "CR",
    //             voucherDate: userdetail.APPDT,
    //             yapariacc: Accounts.VYAPARIACC || "",
    //             cashacc: Accounts.CASHACC || "",
    //             checkacc: Accounts.CHECKACC || "",
    //             onlineacc: masterData.khate || "",
    //             voucherAmount: data.tamt,
    //             referenceTID: "",
    //             referenceKey: masterData.vyapari,
    //             narration: masterData.description,
    //             grpkey: voucherAID,
    //             oid: "30",
    //             trntype: data.trntype || "",
    //             vctype: "20"

    //         }));

    //         const Payload4 = Payload1
    //             .filter(data => data.vapasi > 0)
    //             .map(data => ({
    //                 voucherAID: ACSPLGUID.getNew(),
    //                 uaid: userdetail?.uaid || "",
    //                 organizationID: userdetail?.companyID || "",
    //                 divisionID: userdetail?.departmentID || "",
    //                 voucherTID: "CR",
    //                 voucherDate: userdetail.APPDT,
    //                 yapariacc: Accounts.VYAPARIACC || "",
    //                 cashacc: Accounts.CASHACC || "",
    //                 checkacc: Accounts.CHECKACC || "",
    //                 onlineacc: masterData.khate || "",
    //                 voucherAmount: data.vapasi,
    //                 referenceTID: "",
    //                 referenceKey: masterData.vyapari,
    //                 narration: masterData.description,
    //                 grpkey: voucherAID,
    //                 oid: "30",
    //                 trntype: data.trntype || "",
    //                 vctype: "20"

    //             }));

    //         const headers = {
    //             "Content-Type": "application/json",
    //             Accept: "*/*",
    //         };

    //         // console.log(dueUpdatePayload, "dueUpdatePayload");
    //         console.log(Payload1, "Payload1");
    //         console.log(PayloadVAPASI1, "PayloadVAPASI1");
    //         console.log(Payload3, "Payload3 TRANSFER VOUCHER");
    //         console.log(Payload4, "Payload4 VAPASI VOUCHER");


    //         // const res1 = await axios.post(baseUrl.Url + "/backend/api/SP_AddUpdVyapariJama", Payload1, { headers });
    //         // const resvapasi = await axios.post(baseUrl.Url + "/backend/api/SP_AddUpdVyapariJama", PayloadVAPASI1, { headers });

    //         // let res2;
    //         // if (dueUpdatePayload.length > 0) {
    //         //     res2 = await axios.post(baseUrl.Url + "/backend/api/SP_UpdateVyapariDue", dueUpdatePayload, { headers });
    //         // }

    //         // if (res1.status === 200) {
    //         //     await axios.post(baseUrl.Url + "/backend/api/SP_TransectionVoucher", Payload3, { headers });
    //         // }

    //         // if (resvapasi.status === 200) {
    //         //     await axios.post(baseUrl.Url + "/backend/api/SP_VapasiVoucher", Payload4, { headers });
    //         // }

    //         if (res1.status === 200 && (!res2 || res2.status === 200)) {
    //             Swal.fire({
    //                 icon: "success",
    //                 title: "जतन झाले!",
    //                 text: "माहिती यशस्वीरित्या जतन करण्यात आली.",
    //                 confirmButtonText: "ठीक आहे",

    //             }).then(() => {
    //                 setMasterData({
    //                     ...masterData,
    //                     vyapari: "",
    //                     txnmode: "",
    //                     khate: "",
    //                     chequedate: "",
    //                     chequenumber: "",
    //                     vyapariname: "",
    //                     phonenumber: "",
    //                     companyname: "",
    //                     GSTnumber: "",
    //                     ekunyenerakkam: "",
    //                     bharlelirakkam: "",
    //                     urvaritrakkam: "",
    //                 });
    //                 setTableData([]);
    //             });
    //         }

    //     } catch (error) {
    //         console.error("Submission Error:", error);
    //         Swal.fire({
    //             icon: "error",
    //             title: "Error",
    //             text: "Failed to save data. Please try again.",
    //         });
    //     }
    // };


    const handlePayloadSubmition = async () => {
        console.log("Master part Submitted from tableData:", tableData);

        try {
            const Payload1 = tableData
                .map((detail, index) => {
                    let tempRemaining = Math.round(parseFloat(masterData.bharlelirakkam || 0));
                    let useAmount = 0;
                    for (let i = 0; i <= index; i++) {
                        const rowTotal = Math.round(parseFloat(tableData[i].total || 0));
                        const amt = Math.min(tempRemaining, rowTotal);
                        if (i === index) useAmount = amt;
                        tempRemaining -= amt;
                    }

                    if (useAmount <= 0) return null;

                    let remaining = Math.round(parseFloat(masterData.bharlelirakkam || 0));
                    let totalTillRow = 0;
                    for (let i = 0; i <= index; i++) {
                        totalTillRow += Math.round(parseFloat(tableData[i].total || 0));
                    }

                    let covered = 0;
                    if (remaining >= totalTillRow) {
                        covered = Math.round(detail.vapasI_AMT || 0);
                    }

                    const AMT = Math.round(detail.vapasI_AMT) + Math.round(detail.total);
                    const AMT1 = Math.round(useAmount) + Math.round(covered);
                    const BPAMT = Math.round(AMT - AMT1) || 0;

                    let TRNTYPE = '';
                    if (masterData.txnmode === '0') TRNTYPE = '2';
                    if (masterData.txnmode === '1') TRNTYPE = '0';
                    if (masterData.txnmode === '2') TRNTYPE = '1';

                    return {
                        vdaid: ACSPLGUID.getNew(),
                        trndt: formatDate1(userdetail.APPDT),
                        tamt: Math.round(useAmount),
                        bpamt: Math.round(BPAMT),
                        fduedt: detail.fduedt || "",
                        sduedt: detail.sduedt || "",
                        vpaid: detail.vpaid || "",
                        tid: detail.tid || "",
                        stid: detail.stid || "",
                        trntid: Math.round(BPAMT) === 0 ? "30" : "20",
                        isclose: Math.round(BPAMT) === 0,
                        trnsr: "",
                        refkey: detail?.refkey || "",
                        uaid: userdetail?.uaid || "",
                        isdeleted: detail.IsDeleted === 1 || detail.IsDeleted === true,
                        vreff: "",
                        reff: detail.vdaid || "",
                        vtype: "0",
                        vapasi: covered || 0,
                        trntype: TRNTYPE || "",
                        grpid: detail?.grpid || "",
                        disbdt: Math.round(BPAMT) === 0 ? userdetail?.APPDT || "" : detail?.trndt || "",
                    };
                })
                .filter(p => p !== null);

            const PayloadVAPASI1 = tableData
                .map((detail, index) => {
                    let tempRemaining = Math.round(parseFloat(masterData.bharlelirakkam || 0));
                    let useAmount = 0;
                    for (let i = 0; i <= index; i++) {
                        const rowTotal = Math.round(parseFloat(tableData[i].total || 0));
                        const amt = Math.min(tempRemaining, rowTotal);
                        if (i === index) useAmount = amt;
                        tempRemaining -= amt;
                    }

                    let remaining = Math.round(parseFloat(masterData.bharlelirakkam || 0));
                    let totalTillRow = 0;
                    for (let i = 0; i <= index; i++) {
                        totalTillRow += Math.round(parseFloat(tableData[i].total || 0));
                    }

                    let covered = 0;
                    if (remaining >= totalTillRow) {
                        covered = Math.round(detail.vapasI_AMT || 0);
                    }

                    const AMT = Math.round(detail.vapasI_AMT) + Math.round(detail.total);
                    const AMT1 = Math.round(useAmount) + Math.round(covered);
                    const BPAMT = Math.round(AMT - AMT1) || 0;

                    let TRNTYPE = '';
                    if (masterData.txnmode === '0') TRNTYPE = '2';
                    if (masterData.txnmode === '1') TRNTYPE = '0';
                    if (masterData.txnmode === '2') TRNTYPE = '1';

                    if (useAmount <= 0 || covered <= 0) return null;

                    return {
                        vdaid: ACSPLGUID.getNew(),
                        trndt: userdetail.APPDT,
                        tamt: Math.round(covered),
                        bpamt: Math.round(BPAMT),
                        fduedt: detail.fduedt || "",
                        sduedt: detail.sduedt || "",
                        vpaid: detail.vpaid || "",
                        tid: detail.tid || "",
                        stid: detail.stid || "",
                        trntid: "40",
                        isclose: Math.round(BPAMT) === 0,
                        trnsr: "",
                        refkey: detail?.refkey || "",
                        uaid: userdetail?.uaid || "",
                        isdeleted: detail.IsDeleted === 1 || detail.IsDeleted === true,
                        vreff: "",
                        reff: detail.vdaid || "",
                        vtype: "0",
                        vapasi: covered,
                        trntype: TRNTYPE || "",
                        grpid: detail?.grpid || "",
                        disbdt: Math.round(BPAMT) === 0 ? userdetail?.APPDT || "" : detail?.trndt || ""
                    };
                })
                .filter(p => p !== null);

            const dueUpdatePayload = Payload1.filter(p => p.bpamt === 0).map(p => ({
                vdaid: p.grpid || ""
            }));

            const voucherAID = ACSPLGUID.getNew();

            const Payload3 = Payload1.map(data => ({
                voucherAID: ACSPLGUID.getNew(),
                uaid: userdetail?.uaid || "",
                organizationID: userdetail?.companyID || "",
                divisionID: userdetail?.departmentID || "",
                voucherTID: "CR",
                voucherDate: userdetail.APPDT,
                yapariacc: Accounts.VYAPARIACC || "",
                cashacc: Accounts.CASHACC || "",
                checkacc: Accounts.CHECKACC || "",
                onlineacc: masterData.khate || "",
                voucherAmount: data.tamt,
                referenceTID: "",
                referenceKey: masterData.vyapari,
                narration: masterData.description,
                grpkey: voucherAID,
                oid: "30",
                trntype: data.trntype || "",
                vctype: "20"
            }));

            const Payload4 = Payload1
                .filter(data => data.vapasi > 0)
                .map(data => ({
                    voucherAID: ACSPLGUID.getNew(),
                    uaid: userdetail?.uaid || "",
                    organizationID: userdetail?.companyID || "",
                    divisionID: userdetail?.departmentID || "",
                    voucherTID: "CR",
                    voucherDate: userdetail.APPDT,
                    yapariacc: Accounts.VYAPARIACC || "",
                    vpasiacc: Accounts.VAPASIACC || "",
                    voucherAmount: data.vapasi,
                    referenceTID: "",
                    referenceKey: masterData.vyapari,
                    narration: masterData.description,
                    grpkey: voucherAID,
                    oid: "30",
                }));

            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*"
            };

            console.log(Payload1, "Payload1");
            console.log(PayloadVAPASI1, "PayloadVAPASI1");
            console.log(Payload3, "Payload3 TRANSFER VOUCHER");
            console.log(Payload4, "Payload4 VAPASI VOUCHER");

            // Uncomment these lines to activate submission

            const res1 = await axios.post(baseUrl.Url + "/backend/api/SP_AddUpdVyapariJama", Payload1, { headers });
            const resvapasi = await axios.post(baseUrl.Url + "/backend/api/SP_AddUpdVyapariJama", PayloadVAPASI1, { headers });

            let res2;
            if (dueUpdatePayload.length > 0) {
                res2 = await axios.post(baseUrl.Url + "/backend/api/SP_UpdateVyapariDue", dueUpdatePayload, { headers });
            }

            if (res1.status === 200) {
                await axios.post(baseUrl.Url + "/backend/api/SP_TransectionVoucher", Payload3, { headers });
            }

            if (resvapasi.status === 200) {
                await axios.post(baseUrl.Url + "/backend/api/SP_VapasiVoucher", Payload4, { headers });
            }

            if (res1.status === 200 && (!res2 || res2.status === 200)) {

                Swal.fire({
                    icon: "success",
                    title: "जतन झाले!",
                    text: "माहिती यशस्वीरित्या जतन करण्यात आली.",
                    confirmButtonText: "ठीक आहे",
                }).then(() => {
                    setMasterData({
                        ...masterData,
                        vyapari: "",
                        txnmode: "",
                        khate: "",
                        chequedate: "",
                        chequenumber: "",
                        vyapariname: "",
                        phonenumber: "",
                        companyname: "",
                        GSTnumber: "",
                        ekunyenerakkam: "",
                        bharlelirakkam: "",
                        urvaritrakkam: "",
                    });
                    setTableData([]);
                });

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
                setMasterData({
                    ...masterData,
                    vyapari: "",
                    txnmode: "",
                    vyapariname: "",
                    phonenumber: "",
                    companyname: "",
                    GSTnumber: "",
                    ekunyenerakkam: "",
                    bharlelirakkam: "",
                    urvaritrakkam: "",
                    description: "",
                    chequedate: "",
                    chequenumber: "",
                });
                setTableData([]);

                const modal = document.getElementById("AddVyapariJama");
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

                if (onRefresh) {
                    onRefresh();
                }

            }
        });
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


    // validation by C+S 
    const validateinput = (e) => {

        if (!masterData.vyapari) {
            Swal.fire({
                icon: "error",
                title: "प्रमाणीकरण त्रुटी",
                text: "कृपया व्यापारी निवडा.",
            }).then(() => {
                vyapariRef.current.focus();
            });
            return;
        }

        if (!masterData.txnmode) {
            Swal.fire({
                icon: "error",
                title: "प्रमाणीकरण त्रुटी",
                text: "कृपया व्यवहार प्रकार निवडा.",
            }).then(() => {
                txnmodeRef.current.focus();
            });
            return;
        }

        if (!masterData.bharlelirakkam || !/^\d+(\.\d{1,2})?$/.test(masterData.bharlelirakkam)) {
            Swal.fire({
                icon: "error",
                title: "प्रमाणीकरण त्रुटी",
                text: "कृपया भरलेली रक्कम योग्य प्रकारे भरा.",
            }).then(() => {
                bharlelirakkamRef.current.focus();
            });
            return;
        }

        if (!masterData.description || !/^(?!\s*$).+/.test(masterData.description)) {
            Swal.fire({
                icon: "error",
                title: "प्रमाणीकरण त्रुटी",
                text: "कृपया संदर्भ लिहा.",
            }).then(() => {
                descriptionRef.current.focus();
            });
            return;
        }
        console.log(masterData.txnmode, "transcation mode $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");

        if (Number(masterData.txnmode) === 2) {
            if (!masterData.chequedate) {
                Swal.fire({
                    icon: "error",
                    title: "प्रमाणीकरण त्रुटी",
                    text: "कृपया चेक तारीख निवडा",
                }).then(() => {
                    chequedateRef.current.focus();
                });
                return;
            }
            if (!masterData.chequenumber || !/^\d{6}$/.test(masterData.chequenumber)) {
                Swal.fire({
                    icon: "error",
                    title: "प्रमाणीकरण त्रुटी",
                    text: "कृपया ६ अंकी बरोबरचा चेक क्रमांक लिहा",
                }).then(() => {
                    chequenumberRef.current.focus();
                });
                return;
            }

        }

        if (Number(masterData.txnmode) === 0) {
            if (!masterData.khate) {
                Swal.fire({
                    icon: "error",
                    title: "प्रमाणीकरण त्रुटी",
                    text: "कृपया खाते निवडा",
                }).then(() => {
                    khateRef.current.focus();
                });
                return;
            }
            // if (!masterData.chequenumber || !/^\d{6}$/.test(masterData.chequenumber)) {
            //     Swal.fire({
            //         icon: "error",
            //         title: "प्रमाणीकरण त्रुटी",
            //         text: "please enter Corect ChequeNumber .",
            //     }).then(() => {
            //         chequenumberRef.current.focus();
            //     });
            //     return;
            // }

        }
        handleSubmit(event);
    };

    const handleModalClose = (result) => {
        handlePayloadSubmition();
        console.log("Modal resultModal resultModal resultModal resultModal result:", result);
    };
    const handleVapasiChange = (e, index) => {
        const updatedValue = Math.round(parseFloat(e.target.value)) || 0; // keep 2 decimal places
        const updatedData = [...tableData];
        updatedData[index].vapasI_AMT = updatedValue;

        const bpamt = Math.round(updatedData[index].bpamt || 0);
        updatedData[index].total = bpamt - updatedValue;
        setTableData(updatedData);
        handleMasterInputChange(e);
    };
    return (
        <div>
            <div className="modal fade" id="AddVyapariJama">
                <div className="modal-dialog modal-dialog-centered modal-fullscreen mbgcolor">
                    <div className="modal-content mbgcolor">
                        <div className="page-wrapper-new p-0">
                            <div className="content">
                                <div className="modal-header border-0 custom-modal-header">
                                    <div className="page-title">
                                        <h4>व्यापारी जमा </h4>

                                    </div>
                                    <div
                                        className="page-btn"
                                        style={{
                                            display: 'flex',
                                            alignItems: 'center',
                                            gap: '10px'
                                        }}
                                    >
                                        <h3>({userdetail?.APPDT})</h3>
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
                                    <form onSubmit={handleSubmit} className="mt-0">
                                        <div className="row">
                                            <div className="col-lg-3"></div>
                                            <div className="col-lg-5 col-sm-6 col-12">

                                                <div className="mb-2">
                                                    <label className='required'>व्यापारी</label>
                                                    <Select
                                                        placeholder="निवडा"
                                                        classNamePrefix="react-select"
                                                        options={vyapari}
                                                        value={vyapari.find(option => option.value === masterData.vyapari) || null}
                                                        openMenuOnFocus={true}
                                                        onChange={(selectedOption) => {
                                                            setMasterData(prevState => ({
                                                                ...prevState,
                                                                vyapari: selectedOption ? selectedOption.value : '',
                                                            }));
                                                            handleSelect(selectedOption.value);
                                                        }}
                                                        styles={{
                                                            menu: (provided) => ({
                                                                ...provided,
                                                                zIndex: 9999,
                                                                position: 'absolute',
                                                            }),
                                                        }}
                                                        required
                                                        title="Please Select vyapari "
                                                        ref={vyapariRef}
                                                        autoFocus
                                                    />
                                                </div>
                                            </div>
                                        </div>
                                        <div className="row">
                                            <div className="col-lg-4 col-sm-6 col-12">
                                                <div >
                                                    <label className='required'>व्यवहार मोड</label>
                                                    <Select
                                                        placeholder="निवडा"
                                                        classNamePrefix="react-select"
                                                        options={txnmodeall}
                                                        value={txnmodeall.find(option => option.value == masterData.txnmode) || null}
                                                        autoFocus
                                                        openMenuOnFocus={true}
                                                        onChange={(selectedOption) => {
                                                            setMasterData(prevState => ({
                                                                ...prevState,
                                                                txnmode: selectedOption ? selectedOption.value : '',
                                                            }));
                                                        }}
                                                        styles={{
                                                            menu: (provided) => ({
                                                                ...provided,
                                                                zIndex: 9999,
                                                                position: 'absolute',
                                                            }),
                                                        }}
                                                        required
                                                        title="Please Select txnmode "
                                                    />
                                                </div>
                                            </div>
                                            {masterData.txnmode !== '' && masterData.txnmode == 0 && (
                                                <>
                                                    <div className="col-lg-4 col-sm-6 col-12">
                                                        <label className="form-label required">खाते </label>
                                                        <Select
                                                            placeholder="निवडा"
                                                            classNamePrefix="react-select"
                                                            options={BankName}
                                                            name="khate"
                                                            openMenuOnFocus={true}
                                                            value={BankName.find(option => option.value === masterData.khate) || null}
                                                            onChange={(selectedOption) => {
                                                                setMasterData(prevState => ({
                                                                    ...prevState,
                                                                    khate: selectedOption ? selectedOption.value : '',
                                                                }));
                                                            }}
                                                            ref={khateRef}
                                                            required
                                                            title="Please Select khate "
                                                        />
                                                    </div>
                                                </>
                                            )}
                                            {masterData.txnmode == 2 && (
                                                <>
                                                    <div className="col-lg-2 col-sm-6 col-12">
                                                        <div >
                                                            <label className="form-label required">चेक तारीक</label>
                                                            <input
                                                                type="Date"
                                                                className="form-control"
                                                                id="chequedate"
                                                                name="chequedate"
                                                                value={masterData.chequedate}
                                                                onChange={handleMasterInputChange}
                                                                title="Please select Date."
                                                                required
                                                            />
                                                        </div>
                                                    </div>
                                                    <div className="col-lg-2 col-sm-6 col-12">
                                                        <div >
                                                            <label className="form-label required">चेक नंबर</label>
                                                            <input
                                                                type="text"
                                                                className="form-control"
                                                                id="chequenumber"
                                                                name="chequenumber"
                                                                placeholder="चेक नंबर"
                                                                value={masterData.chequenumber}
                                                                onChange={handleMasterInputChange}
                                                                pattern="^\d{6}$"
                                                                title="Only Digits. Field cannot be empty or just spaces."
                                                                required
                                                            />
                                                        </div>
                                                    </div>
                                                </>
                                            )}
                                        </div>
                                        <div className="row mb-2 mt-2" >
                                            <div className="col-lg-4 col-sm-6 col-12">
                                                <div>
                                                    <label className="form-label required">व्यापारी नाव</label>
                                                    <input
                                                        type="text"
                                                        className="form-control"
                                                        id="vyapariname"
                                                        name="vyapariname"
                                                        value={masterData.vyapariname}
                                                        onChange={handleMasterInputChange}
                                                        readOnly
                                                        placeholder="व्यापारी नाव"
                                                    />
                                                </div>
                                            </div>

                                            <div className="col-lg-2 col-sm-6 col-12">
                                                <div >
                                                    <label className="form-label required">फोन नंबर </label>
                                                    <input
                                                        type="text"
                                                        className="form-control"
                                                        id="phonenumber"
                                                        name="phonenumber"
                                                        value={masterData.phonenumber}
                                                        onChange={handleMasterInputChange}
                                                        readOnly
                                                    />
                                                </div>
                                            </div>
                                            <div className="col-lg-2 col-sm-6 col-12">
                                                <div >
                                                    <label className="form-label required">GSTN क्रमांक </label>
                                                    <input
                                                        type="text"
                                                        className="form-control"
                                                        id="GSTnumber"
                                                        name="GSTnumber"
                                                        value={masterData.GSTnumber}
                                                        onChange={handleMasterInputChange}
                                                        readOnly
                                                    />
                                                </div>
                                            </div>
                                            <div className="col-lg-4 col-sm-6 col-12">
                                                <div >
                                                    <label className="form-label required">कंपनी नाव</label>
                                                    <input
                                                        type="text"
                                                        className="form-control"
                                                        id="companyname"
                                                        name="companyname"
                                                        value={masterData.companyname}
                                                        onChange={handleMasterInputChange}
                                                        readOnly
                                                    />
                                                </div>
                                            </div>
                                        </div>
                                        <div className="row">
                                            <div className="col-lg-2 col-sm-6 col-12">
                                                <div>
                                                    <label className="form-label required">एकूण येणे रक्कम</label>
                                                    <input
                                                        type="text"
                                                        className="form-control"
                                                        id="ekunyenerakkam"
                                                        name="ekunyenerakkam"
                                                        value={masterData.ekunyenerakkam}
                                                        onChange={handleMasterInputChange}
                                                        readOnly
                                                    />
                                                </div>
                                            </div>
                                            <div className="col-lg-2 col-sm-6 col-12">
                                                <div >
                                                    <label className="form-label required">भरलेली रक्कम </label>
                                                    <input
                                                        type="text"
                                                        className="form-control"
                                                        id="bharlelirakkam"
                                                        name="bharlelirakkam"
                                                        value={masterData.bharlelirakkam}
                                                        onChange={handleMasterInputChange}
                                                        required
                                                        pattern="^\d+(\.\d{1,2})?$"
                                                        title="Only Digits are allowed. Field cannot be empty or just spaces."

                                                    />
                                                </div>
                                            </div>
                                            <div className="col-lg-2 col-sm-6 col-12">
                                                <div >
                                                    <label className="form-label required">उर्वरित रक्कम </label>
                                                    <input
                                                        type="text"
                                                        className="form-control"
                                                        id="urvaritrakkam"
                                                        name="urvaritrakkam"
                                                        value={masterData.urvaritrakkam}
                                                        onChange={handleMasterInputChange}
                                                        readOnly
                                                        required
                                                    />
                                                </div>
                                            </div>
                                            <div className="col-lg-6 col-sm-6 col-12">
                                                <div >
                                                    <label className="form-label required">संदर्भ </label>
                                                    <textarea
                                                        type="text"
                                                        className="form-control"
                                                        id="description"
                                                        name="description"
                                                        value={masterData.description}
                                                        onChange={handleMasterInputChange}
                                                        pattern="^(?!\s*$).+"
                                                        title="Only letters and spaces are allowed. Field cannot be empty or just spaces."
                                                        required
                                                        placeholder="संदर्भ"
                                                    />
                                                </div>
                                            </div>
                                        </div>

                                        <div className="border p-3 rounded shadow-sm mt-2">
                                            <div className="row ">
                                                <div className="col-lg-12">
                                                    <div className="modal-body-table">
                                                        <div className="table-responsive">
                                                            <div >
                                                                <div className="modal-body-table overflow-auto max-vh-100" >
                                                                    <div className="table-responsive" style={{ height: "calc(40vh - 120px)" }}>
                                                                        <table className="table datanew table-bordered" style={{ tableLayout: "fixed", width: "100%" }}>
                                                                            <thead className="thead-dark" style={{ position: "sticky", top: 0, backgroundColor: "#343a40", color: "white", zIndex: 1000 }}>
                                                                                <tr>
                                                                                    <th className="text-center">तारीक</th>
                                                                                    <th className="text-center">बिल रक्कम </th>
                                                                                    <th className="text-center">अंदाजे वापसी</th>
                                                                                    <th className="text-center">येणे बाकी</th>
                                                                                    <th className="text-center">भरणा रक्कम</th>
                                                                                    <th className="text-center">अदा वापसी</th>
                                                                                    <th className="text-center">शिल्लक</th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                {tableData.length > 0 ? (
                                                                                    tableData.map((row, index) => {


                                                                                        return (
                                                                                            <tr key={index}>
                                                                                                <td className="text-center">{row.slaB_RANGE}</td>
                                                                                                <td className="text-center">{Math.round(row.totalTAMT)}</td>
                                                                                                <td className="text-center">
                                                                                                    {/* {Math.round(row.vapasI_AMT)} */}
                                                                                                    <input
                                                                                                        type="text"
                                                                                                        className="form-control text-center"
                                                                                                        value={Math.round(row.vapasI_AMT) || 0}
                                                                                                        onChange={(e) => handleVapasiChange(e, index)}
                                                                                                    />
                                                                                                </td>

                                                                                                <td className="text-center">{Math.round(row.total)}</td>
                                                                                                <td className="text-center">
                                                                                                    <>
                                                                                                        {(() => {
                                                                                                            let remaining = Math.round(parseFloat(masterData.bharlelirakkam || 0));
                                                                                                            for (let i = 0; i <= index; i++) {
                                                                                                                const rowTotal = Math.round(parseFloat(tableData[i].total || 0));
                                                                                                                const useAmount = Math.min(remaining, rowTotal);
                                                                                                                if (i === index) return useAmount;
                                                                                                                remaining -= useAmount;
                                                                                                            }
                                                                                                            return 0;
                                                                                                        })()}

                                                                                                    </>
                                                                                                </td>
                                                                                                <td className="text-center">
                                                                                                    {(() => {
                                                                                                        let remaining = Math.round(parseFloat(masterData.bharlelirakkam || 0));
                                                                                                        let totalTillRow = 0;
                                                                                                        let covered = false;

                                                                                                        for (let i = 0; i <= index; i++) {
                                                                                                            const rowTotal = Math.round(parseFloat(tableData[i].total || 0));
                                                                                                            totalTillRow += rowTotal;
                                                                                                        }

                                                                                                        // If total amount paid so far is >= total till current row
                                                                                                        if (remaining >= totalTillRow) {
                                                                                                            covered = true;
                                                                                                        }

                                                                                                        return covered ? Math.round(row.vapasI_AMT || 0) : 0;
                                                                                                    })()}
                                                                                                </td>
                                                                                                <td className="text-center">
                                                                                                    {(() => {
                                                                                                        let remaining = Math.round(parseFloat(masterData.bharlelirakkam || 0));
                                                                                                        let useAmount = 0;

                                                                                                        // Calculate useAmount up to this row
                                                                                                        for (let i = 0; i <= index; i++) {
                                                                                                            const rowTotal = Math.round(parseFloat(tableData[i].total || 0));
                                                                                                            const currentUse = Math.min(remaining, rowTotal);
                                                                                                            if (i === index) useAmount = currentUse;
                                                                                                            remaining -= currentUse;
                                                                                                        }

                                                                                                        // Calculate covered
                                                                                                        let covered = 0;
                                                                                                        let totalTillRow = 0;
                                                                                                        for (let i = 0; i <= index; i++) {
                                                                                                            totalTillRow += Math.round(parseFloat(tableData[i].total || 0));
                                                                                                        }
                                                                                                        if (Math.round(parseFloat(masterData.bharlelirakkam || 0)) >= totalTillRow) {
                                                                                                            covered = Math.round(parseFloat(row.vapasI_AMT || 0));
                                                                                                        }

                                                                                                        const vapasI_AMT = Math.round(parseFloat(row.vapasI_AMT || 0));
                                                                                                        const total = Math.round(parseFloat(row.total || 0));

                                                                                                        const shillak = (vapasI_AMT + total) - (useAmount + covered);
                                                                                                        return shillak;
                                                                                                    })()}
                                                                                                </td>

                                                                                            </tr>
                                                                                        );
                                                                                    })
                                                                                ) : (
                                                                                    <tr>
                                                                                        <td colSpan="6" className="text-center">No Data Available</td>
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
                                        </div>
                                        <div className="col-lg-12 d-flex justify-content-end mt-2">
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
                                        </div>
                                    </form>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <AddCashVyaparibharana onClose={handleModalClose}
                BHARALELIRAKKAM={masterData.bharlelirakkam} />
        </div >

    );
};

export default AddVyapariBharana;






