import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useState, useRef } from "react";
import { Link } from "react-router-dom";
import ImageWithBasePath from "../../core/img/imagewithbasebath";
import Brand from "../../core/modals/inventory/brand";
import withReactContent from "sweetalert2-react-content";
import Swal from "sweetalert2";
import { all_routes } from "../../Router/all_routes";
import { OverlayTrigger, Tooltip } from "react-bootstrap";
import Table from "../../core/pagination/datatable";
import { setToogleHeader } from "../../core/redux/action";
import { useEffect } from "react";
import axios from "axios";
import { baseUrl, convertToISODate } from "../../core/json/custom";
import {
    ArrowLeft, ChevronUp, PlusCircle, RotateCcw, CheckCircle, Edit, Trash2, Eye
} from "feather-icons-react/build/IconComponents";
import { useNavigate } from "react-router-dom";
import { getUserData } from "../../Context/UserData";
import { saveAs } from "file-saver";
import ExcelJS from "exceljs";
import { jsPDF } from "jspdf";
import autoTable from "jspdf-autotable";
import marathiFontBase64 from "../../style/fonts/NotoSansDevanagari";
// import AddSabhasad from "./AddSabhasad";
import AddMember from "./AddMember";
// import AddVisitorMaster from "./AddVisitorMaster";
const Member = () => {
    const MySwal = withReactContent(Swal);
    const navigate = useNavigate();
    const { userdetail } = getUserData();
    const dispatch = useDispatch();
    const data = useSelector((state) => state.toggle_header);
    const route = all_routes;
    const [visitorMaster, setVisitorMaster] = useState([]);
    const [searchQuery, setSearchQuery] = useState("");
    const [selectedData, setSelectedData] = useState({ sraid: null });
    const [refreshFlag, setRefreshFlag] = useState(false);

    //  const MySwal = withReactContent(Swal);
    //     const route = all_routes;
    //     const navigate = useNavigate();
    const [isDesktop, setIsDesktop] = useState(false);
    // const { userdetail } = getUserData();
    const [dayBookData, setDayBookData] = useState([]);
    const [TrialBData, setTrialBData] = useState([]);
    const [startDate, setStartDate] = useState('');
    const [endDate, setEndDate] = useState('');
    const [hoverIndex, setHoverIndex] = useState(null);
    const startDateRef = useRef(null);
    const endDateRef = useRef(null);
    const ledgerref = useRef(null);
    const [ledger, setledger] = useState([]);
    const [ACCAID, setACCAID] = useState();

    const [formData, setFormData] = useState({
        sabhasadName: "",
        contactNumber: "",
        birthdate: "",
        age: "",
        village: "",
        tal: "",
        district: "",
        businessPrimary: "",
        businessSecondary: "",
        totalLand: "",
        irrigatedLand: "",
        nonIrrigatedLand: "",
        nominalName: "",
        nominalAge: "",
        nominalRelation: "",
        implicationTitle: "",
    });



    useEffect(() => {
        const handleResize = () => setIsDesktop(window.innerWidth >= 992);
        handleResize();
        window.addEventListener('resize', handleResize);
        return () => window.removeEventListener('resize', handleResize);
    }, []);
    const cardsData = [
        { title: 'Crop Report', image: 'assets/img/avatar/reportS.png', path: 'crop-report' },

    ];

    const fetchTrailBalanceData = async (sraid) => {
        // if (!startDate) {
        //     Swal.fire({
        //         icon: 'error',
        //         title: 'Error',
        //         text: 'Please select the start date.',
        //         allowOutsideClick: false,
        //         allowEscapeKey: false,
        //     }).then(() => {
        //         startDateRef.current?.focus();
        //     });
        //     return;
        // }

        // if (!endDate) {
        //     Swal.fire({
        //         icon: 'error',
        //         title: 'Error',
        //         text: 'Please select the end date.',
        //         allowOutsideClick: false,
        //         allowEscapeKey: false,
        //     }).then(() => {
        //         endDateRef.current?.focus();
        //     });
        //     return;
        // }

        try {
            const [res, res1, res2, res3, res4, response] = await Promise.all([
                axios.post(`${baseUrl.Url}/backend/api/GET_SFamilyInformation`, {
                    companyid: userdetail?.companyID || "",
                    deptid: userdetail?.departmentID || "",
                    dsfaid: "%",
                    sraid: sraid,
                }),
                axios.post(`${baseUrl.Url}/backend/api/GET_SAgriculturalInformation`, {
                    companyid: userdetail?.companyID || "",
                    deptid: userdetail?.departmentID || "",
                    dsaid: "%",
                    sraid: sraid,
                }),

                axios.post(`${baseUrl.Url}/backend/api/GET_SCropInformation`, {
                    companyid: userdetail?.companyID || "",
                    deptid: userdetail?.departmentID || "",
                    dscaid: "%",
                    sraid: sraid,
                }),

                axios.post(`${baseUrl.Url}/backend/api/GET_SFertilizersName`, {
                    companyid: userdetail?.companyID || "",
                    deptid: userdetail?.departmentID || "",
                    dsfid: "%",
                    sraid: sraid,
                }),

                axios.post(`${baseUrl.Url}/backend/api/GET_SChemicalName`, {
                    companyid: userdetail?.companyID || "",
                    deptid: userdetail?.departmentID || "",
                    dscid: "%",
                    sraid: sraid,
                }),

                axios.post(`${baseUrl.Url}/backend/api/GET_SabhasadMaster`, {
                    companyid: userdetail?.companyID || "",
                    deptid: userdetail?.departmentID || "",
                    sraid: sraid,
                    keyword: "%"
                })
            ]);

            const data = res.data;
            const data1 = res1.data;
            const data2 = res2.data;
            const data3 = res3.data;
            const data4 = res4.data;
            const master = response.data;

            setTrialBData(data1); // If needed for state

            generateTrialBalanceReport(data, data1, data2, data3, data4, master); // ✅ Pass both
        } catch (error) {
            console.error("Error fetching Trial Balance data:", error);
        }


    };


    function formatDateToShortMonth(dateStr) {
        const months = [
            "Jan", "Feb", "Mar", "Apr", "May", "Jun",
            "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"
        ];

        const [year, month, day] = dateStr.split("-");
        const monthName = months[parseInt(month, 10) - 1];
        const dayNumber = parseInt(day, 10); // Remove leading 0

        return `${dayNumber} ${monthName} ${year}`;
    }

    const processData = (arr) => {
        return arr.map(item => ({
            age: item.age || "",
            businessname: item.businessname || "",
            education: item.education || "",
            name: item.name || "",
            relationname: item.relationname || 0,

        }));
    };

    const processData1 = (arr) => {
        return arr.map(item => ({
            clusternumber: item.clusternumber || "",
            landname: item.landname || "",
            surveynumber: item.surveynumber || "",
            landarea: item.landarea || "",
        }));
    };

    const processData2 = (arr) => {
        return arr.map(item => ({
            cname: item.cname || "",
            cvariety: item.cvariety || "",
            ecrop: item.ecrop || "",
            growingperiod: item.growingperiod || "",
            hduration: item.hduration || "",
            ntype: item.ntype || "",
            area: item.area || "",
        }));
    };

    const processData3 = (arr) => {
        return arr.map(item => ({
            cost: item.cost || "",
            fertilizersname: item.fertilizersname || "",
            fname: item.fname || "",
            quantity: item.quantity || "",

        }));
    };

    const processData4 = (arr) => {
        return arr.map(item => ({
            chemicalname: item.chemicalname || "",
            cname: item.cname || "",
            coast: item.coast || "",
            quantity: item.quantity || "",

        }));
    };

    console.log("datadatadatadatadatadatadatadatadatadatadatadatadatadatadatadatadata", formData.sabhasadName)
    const generateTrialBalanceReport = (data, data1, data2, data3, data4, master) => {
        // const rows = processTrialBalanceData(data);
        const mapped = processData(data);
        const mapped1 = processData1(data1);
        const mapped2 = processData2(data2);
        const mapped3 = processData3(data3);
        const mapped4 = processData4(data4);

        const firstRow = master[0] || {};

        if (master && master.length > 0) {
            const firstRow = master[0];

            setFormData({
                sabhasadName: firstRow.sabhasadname || "",
                contactNumber: firstRow.contact || "",
                birthdate: firstRow.birthdate || "",
                age: firstRow.age || "",
                village: firstRow.village || "",
                tal: firstRow.taluka || "",
                district: firstRow.district || "",
                businessPrimary: firstRow.businessPrimary || "",
                businessSecondary: firstRow.businessSecondary || "",
                totalLand: firstRow.totalLand || "",
                irrigatedLand: firstRow.irrigatedLand || "",
                nonIrrigatedLand: firstRow.nonIrrigatedLand || "",
                nominalName: firstRow.nominalName || "",
                nominalAge: firstRow.nominalAge || "",
                nominalRelation: firstRow.nominalRelation || "",
                implicationTitle: firstRow.implicationTitle || ""
            });
        }




        // const totalDR = TrialBData.reduce((sum, r) => sum + (crtcnt || 0), 0);
        const totalDR = mapped.reduce((sum, r) => sum + (r.crtcnt || 0), 0);
        const totalCR = mapped.reduce((sum, r) => sum + (r.iwdamt || 0), 0);
        const totalSL = mapped.reduce((sum, r) => sum + (r.samt || 0), 0);
        const totalEX = mapped.reduce((sum, r) => sum + (r.examt || 0), 0);

        console.log("totalDR totalDR totalDR", totalDR)

        const htmlRows = mapped.map(({ name, businessname, age, education, relationname }, index) => `
                <tr>
                   <td class="text-right">${index + 1}</td>
                    <td class="text-left">${name}</td>
                    <td class="text-left">${relationname}</td>
                    <td class="text-center">${age}</td>
                    <td class="text-center">${education}</td>
                    <td class="text-center">${businessname}</td>
    
                </tr>
            `).join('');


        const htmlRows1 = mapped1.map(({ clusternumber, surveynumber, landarea, landname }, index) => `
                <tr>
                   <td class="text-right">${index + 1}</td>
                    <td class="text-center">${surveynumber}</td>
                    <td class="text-left">${clusternumber}</td>
                    <td class="text-left">${landarea}</td>
                    <td style="text-align: center;">${landname}</td>
    
                </tr>
            `).join('');


        const htmlRows2 = mapped2.map(({ cname, ntype, area, landname, cvariety, growingperiod, hduration, ecrop }, index) => `
                <tr>
                   <td class="text-right">${index + 1}</td>
                    <td class="text-center">${cname}</td>
                    <td class="text-left">${cvariety}</td>
                    <td class="text-left">${area}</td>
                    <td class="text-center">${growingperiod}</td>
                    <td class="text-center">${hduration}</td>
                    <td class="text-center">${ecrop}</td>
                    <td class="text-center">${ntype}</td>
    
                </tr>
            `).join('');

        const htmlRows3 = mapped3.map(({ fertilizersname, cost, quantity, fname }, index) => `
                <tr>
                   <td class="text-right">${index + 1}</td>
                    <td class="text-center">${fertilizersname}</td>
                    <td class="text-left">${quantity}</td>
                    <td class="text-left">${cost}</td>
                    <td class="text-center">${fname}</td>
                </tr>
            `).join('');

        const htmlRows4 = mapped4.map(({ cname, chemicalname, quantity, coast }, index) => `
                <tr>
                   <td class="text-right">${index + 1}</td>
                    <td class="text-center">${cname}</td>
                    <td class="text-left">${chemicalname}</td>
                    <td class="text-left">${quantity}</td>
                    <td class="text-center">${coast}</td>
                </tr>
            `).join('');

        const htmlContent = `
                <html>
                <head>
                    <meta charset="UTF-8">
    
                    <title>Crop Report</title>
                    <style>
                        @page { size: A4 Portait; margin: 5mm; }
                        body { font-family: 'Noto Sans Devanagari', sans-serif; margin: 0 auto; padding: 20px; max-width: 300mm; }
                        .center { text-align: center; }
                        .title { font-size: 22px; font-weight: bold; margin-top:3mm; }
                        .subtitle { font-size: 16px; margin-top: 4px; margin-bottom: 12px; }
                        table { width: 100%; font-size: 15px; border-collapse: collapse; }
                        th, td { padding: 6px 8px; border: 1px solid #ddd; vertical-align: top; }
                        th { background-color: rgba(223, 223, 223, 0.93); }
                        .text-right { text-align: right; }
                        .bold { font-weight: bold; }
                        tfoot td { border-top: 2px solid black; font-weight: bold; text-align: right; }
                    </style>
                </head>
                <body>
                    <div class="center title">${userdetail?.departmentname || 'श्री साई फ्रूट्स कंपनी'}</div>
                    <div class="center title">Crop Report</div>
                    <div class="center subtitle">Duration: <b>From</b> ${formatDateToShortMonth(startDate)} <b> to </b> ${formatDateToShortMonth(endDate)} </div>
    
                    <table>
                        <thead>
                            
                        <form>
                        
                        <div style="text-align: center; margin-bottom: 20px;">
                              <label class="bold">- - -  Personal Information - - -</label><br>
                        </div>
    
                        
                        <label class="bold">Sabhasadache Name : </label>
                        <input type="text" class="form-control" name="sabhasadName" value="${firstRow.sabhasadname}" style="border: none;border-bottom: 1px solid lightgray; outline: none; width: 580px;  margin-bottom: 20px;" />
                            
                        <label class="bold">Contact Number : </label>
                        <input type="text" class="form-control"  value=${firstRow.contactnumber} style="border: none; border-bottom: 1px solid lightgray; outline: none; width: 200px; margin-bottom: 20px;" /> <br>
                       
    
                        <label class="bold">Birthdate : </label>
                        <input type="text" value=${convertToISODate(firstRow.birthdate)} style="border: none; border-bottom: 1px solid lightgray; outline: none; width: 400px; margin-bottom: 20px;" />
    
                        <label class="bold">Age : </label>
                        <input type="text" value=${firstRow.age} style="border: none;  border-bottom: 1px solid lightgray; outline: none; width: 150px; margin-bottom: 20px;" /> <br>
    
                        <label class="bold"> Full Address : - Village : </label>
                        <input type="text" value=${firstRow.villagename} style="border: none; border-bottom: 1px solid lightgray;  outline: none; width: 300px; margin-bottom: 20px;" />
    
                        <label class="bold">Tal : </label>
                        <input type="text" value=${firstRow.birthdate} style="border: none; border-bottom: 1px solid lightgray; outline: none; width: 250px; margin-bottom: 20px;" /> 
    
                        <label class="bold">District : </label>
                        <input type="text" value=${firstRow.birthdate} style="border: none; border-bottom: 1px solid lightgray; outline: none; width: 280px; margin-bottom: 20px;" /><br>
    
                        <label class="bold"> Business : - Primary : </label>
                        <input type="text" value=${firstRow.primarybusiness} style="border: none; border-bottom: 1px solid lightgray; outline: none; width: 270px; margin-bottom: 20px;" />
    
                        <label class="bold">  Secondary : </label>
                        <input type="text" value=${firstRow.secoundarybusines} style="border: none; border-bottom: 1px solid lightgray; outline: none; width: 220px; margin-bottom: 20px;" /> <br>
    
    
                         <label class="bold">Total Agricultural Land : </label>
                        <input type="text" value=${firstRow.totalland} style="border: none; border-bottom: 1px solid lightgray; outline: none; width: 250px; margin-bottom: 20px;" />
    
                        <label class="bold">Irrigated Land : </label>
                        <input type="text" value=${firstRow.irrigatedland} style="border: none; border-bottom: 1px solid lightgray; outline: none; width: 200px; margin-bottom: 20px;" /> 
    
                        <label class="bold">Non Irrigated Land : </label>
                        <input type="text" value=${firstRow.nonirrigatedland} style="border: none; border-bottom: 1px solid lightgray; outline: none; width: 190px; margin-bottom: 20px;" /><br>
    
    
                        <label class="bold">Nominal Name : </label>
                        <input type="text" value=${firstRow.nominalname} style="border: none; border-bottom: 1px solid lightgray; outline: none; width: 320px; margin-bottom: 20px;" />
    
                        <label class="bold">Age : </label>
                        <input type="text" value=${firstRow.nominalage} style="border: none; border-bottom: 1px solid lightgray; outline: none; width: 240px; margin-bottom: 20px;" /> 
    
                        <label class="bold">Relation with Nominal : </label>
                        <input type="text" value=${firstRow.implicationTitle} style="border: none; border-bottom: 1px solid lightgray; outline: none; width: 190px; margin-bottom: 20px;" /><br>
    
                        </form>
    
                        <div style="text-align: center; margin-bottom: 20px;">
                              <label class="bold">- - - Family Information - - -</label><br>
                        </div>
    
                            <tr>
                                <th style="min-width: 30px;">Sr. No.</th>
                                <th style="min-width: 200px;">Name</th>
                                <th style="min-width: 80px;">Relation</th>
                                <th style="min-width: 80px;">Age</th>
                                <th style="min-width: 70px;">Education</th>
                                <th style="min-width: 50px;">Business </th>
                            </tr>
                        
                        </thead>
                        <tbody>
                            ${htmlRows}
                        </tbody>
                        
                    </table>
    
    
                    <table>
                         <thead>
                            <div style="text-align: center; margin-bottom: 20px; margin-top: 20px;">
                                <label class="bold">- - - Agricultural Information - - -</label><br>
                            </div>
                            <tr>
                                <th style="min-width: 30px;">Sr. No.</th>
                                <th style="min-width: 200px;">Survey Number</th>
                                <th style="min-width: 80px;">Cluster Number</th>
                                <th style="min-width: 80px;">land area</th>
                                <th style="min-width: 70px;">Irrigated / Non-Irrigated Land</th>
                               
                            </tr>
                        </thead>
                        <tbody>
                            ${htmlRows1}
                        </tbody>
                        
                    </table>
    
    
                    <table>
                         <thead>
                            <div style="text-align: center; margin-bottom: 20px; margin-top: 20px;">
                                <label class="bold">- - - Which crops are grown throughout the year? - - -</label><br>
                            </div>
                            <tr>
                                <th style="min-width: 30px;">Sr. No.</th>
                                <th style="min-width: 200px;">Crop Name</th>
                                <th style="min-width: 80px;">Variety of Crop</th>
                                <th style="min-width: 80px;">land area</th>
                                <th style="min-width: 70px;">Growing Period</th>
                                <th style="min-width: 70px;">Harvesting Period</th>
                                <th style="min-width: 70px;">Expected Yield</th>
                                <th style="min-width: 70px;">Type</th>
                               
                            </tr>
                        </thead>
                        <tbody>
                            ${htmlRows2}
                        </tbody>
                        
                    </table>
    
    
                    <table>
                         <thead>
                            <div style="text-align: center; margin-bottom: 20px; margin-top: 20px;">
                                <label class="bold">- - - Which fertilizers are used throughout the year? - - -</label><br>
                            </div>
                            <tr>
                                <th style="min-width: 30px;">Sr. No.</th>
                                <th style="min-width: 200px;">Fertilizers Name</th>
                                <th style="min-width: 80px;">Quantity</th>
                                <th style="min-width: 80px;">Cost</th>
                                <th style="min-width: 70px;">Type</th>
                               
                            </tr>
                        </thead>
                        <tbody>
                            ${htmlRows3}
                        </tbody>
                        
                    </table>
    
    
    
                    <table>
                         <thead>
                            <div style="text-align: center; margin-bottom: 20px; margin-top: 20px;">
                                <label class="bold">- - - Which chemical/ organic fertilizers, insecticides, herbicides, and fungicides are used throughout the year? - - -</label><br>
                            </div>
                            <tr>
                                <th style="min-width: 30px;">Sr. No.</th>
                                <th style="min-width: 200px;">Name</th>
                                <th style="min-width: 80px;">Type</th>
                                <th style="min-width: 80px;">Quantity</th>
                                <th style="min-width: 70px;">Coast</th>
                               
                            </tr>
                        </thead>
                        <tbody>
                            ${htmlRows4}
                        </tbody>
                        
                    </table>
    
                </body>
                </html>
                `;

        const blob = new Blob([htmlContent], { type: 'text/html' });
        const url = URL.createObjectURL(blob);
        window.open(url, '_blank');
    };


    const handleCardClick = async (card, sraid) => {

        if (card.path === 'crop-report') {
            await fetchTrailBalanceData(sraid);
        }
        else if (card.path) {
            navigate(card.path, { state: { autoOpen: card.autoOpen || false } });
        }
    };




















    useEffect(() => {
        const handleShortcut = (e) => {
            if (e.ctrlKey && (e.key === 'e' || e.key === 'E')) {
                e.preventDefault();
                // showExitAlert();
                navigate('/MasterIndex')

            }
            if (e.ctrlKey && (e.key === 'a' || e.key === 'A')) {
                e.preventDefault();
                // validateinput();
                const modal = document.getElementById("AddSabhsad");

                if (modal) {
                    modal.classList.add("show");
                    modal.style.display = "block";
                    modal.setAttribute("aria-modal", "true");
                    modal.setAttribute("role", "dialog");
                    modal.removeAttribute("aria-hidden");
                    const backdrop = document.createElement("div");
                    backdrop.className = "modal-backdrop fade show";
                    document.body.appendChild(backdrop);
                    document.body.classList.add("modal-open");
                    document.body.style.overflow = "hidden";
                    document.body.style.paddingRight = "0px";
                }
            }
        };
        window.addEventListener('keydown', handleShortcut);
        return () => {
            window.removeEventListener('keydown', handleShortcut);
        };
    }, []);


    useEffect(() => {
        const fetchVisitorMaster = async () => {
            try {
                const payload = {
                    "sraid": "%",
                    "keyword": "%",
                    "companyid": userdetail?.companyID ? userdetail.companyID : "",
                    "deptid": userdetail?.departmentID ? userdetail.departmentID : "",
                };
                const headers = {
                    "Content-Type": "application/json",
                    Accept: "*/*",
                };
                const response = await axios.post(
                    `${baseUrl.Url}/backend/api/GET_SabhasadMaster`,
                    payload,
                    { headers }
                );
                if (response.status !== 200)
                    throw new Error("Failed to fetch ");
                console.log("GET_VisitorMaster", response.data)
                setVisitorMaster(response.data);
            } catch (error) {
                console.error("Error fetching GET_VisitorMaster data:", error);
            }
        };
        fetchVisitorMaster();
    }, []);
    const handleSearch = (event) => {
        setSearchQuery(event.target.value);
        try {
            const payload = {
                "sraid": "%",
                "keyword": event.target.value,
                "companyid": userdetail?.companyID ? userdetail.companyID : "",
                "deptid": userdetail?.departmentID ? userdetail.departmentID : "",
            }
            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };

            axios({
                method: "POST",
                url: baseUrl.Url + "/backend/api/GET_SabhasadMaster/_Search",
                data: JSON.stringify(payload),
                headers: headers,
            })
                .then((response) => {
                    if (response.status != 200) throw new Error("Failed to send response ");
                    console.log("response", response.data);
                    setVisitorMaster(response.data);
                })
        } catch (error) {
            console.error("Error while searching Visitor Master data:", error);
        }
    };
    const refreshData = () => {
        setSelectedData({ sraid: null });
        const fetchVisitorMaster = async () => {
            try {
                const payload = {
                    "sraid": "%",
                    "keyword": "%",
                    "companyid": userdetail?.companyID ? userdetail.companyID : "",
                    "deptid": userdetail?.departmentID ? userdetail.departmentID : "",
                };
                const headers = {
                    "Content-Type": "application/json",
                    Accept: "*/*",
                };
                const response = await axios.post(
                    `${baseUrl.Url}/backend/api/GET_SabhasadMaster`,
                    payload,
                    { headers }
                );
                if (response.status !== 200)
                    throw new Error("Failed to fetch ");
                console.log("GET_VisitorMaster", response.data)
                setVisitorMaster(response.data);
            } catch (error) {
                console.error("Error fetching GET_VisitorMaster data:", error);
            }
        };
        fetchVisitorMaster();
        setRefreshFlag(prev => !prev);
    };
    useEffect(() => {
        setSelectedData({ sraid: null });
        console.log("HHHHHHHHHHHHHHHHHHH", selectedData.sraid)
    }, [refreshFlag]);
    const OnEdit = (sraid) => {
        setSelectedData({ sraid: sraid });
    };

    const ConfirmationAlert = (sraid) => {
        MySwal.fire({
            title: "Are you sure?",
            text: "You won’t be able to undo this!",
            showCancelButton: true,
            confirmButtonColor: "#00ff00",
            confirmButtonText: "Yes, delete it!",
            cancelButtonColor: "#ff0000",
            cancelButtonText: "Cancel",
            allowOutsideClick: false,
            allowEscapeKey: false,
        }).then((result) => {
            if (result.isConfirmed) {
                OnDelete(sraid);
            } else {
                MySwal.close();
            }
        });
    };


    const OnDelete = async (sraid) => {
        console.log("on delete vdaid", sraid)
        try {
            const payload = {
                "sraid": sraid,
                "companyid": userdetail?.companyID ? userdetail.companyID : "",
                "deptid": userdetail?.departmentID ? userdetail.departmentID : "",
            };
            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };
            axios({
                method: "POST",
                url: baseUrl.Url + "/backend/api/SP_DeleteSabhasadMaster",
                data: JSON.stringify(payload),
                headers: headers,
            })
                .then((response) => {
                    if (response.status != 200) throw new Error("Failed to Fetching Data");
                    MySwal.fire({
                        title: response.data[0].responseCode === "अपयश" ? "हटवणे अनुमत नाही" : "हटवले!",
                        text: response.data[0].responseCode === "FAILURE"
                            ? response.data[0].responseMessage
                            : response.data[0].responseMessage,
                        icon: response.data[0].responseCode === "FAILURE" ? "error" : "success",
                        confirmButtonText: "ठीक आहे",
                        customClass: {
                            confirmButton: response.data[0].responseCode === "FAILURE" ? "btn btn-danger" : "btn btn-success",
                        },
                    });
                    try {
                        const payload = {
                            "sraid": "%",
                            "keyword": "%",
                            "companyid": userdetail?.companyID ? userdetail.companyID : "",
                            "deptid": userdetail?.departmentID ? userdetail.departmentID : "",
                        };
                        const headers = {
                            "Content-Type": "application/json",
                            Accept: "*/*",
                        };
                        axios({
                            method: "POST",
                            url: baseUrl.Url + "/backend/api/GET_SabhasadMaster",
                            data: JSON.stringify(payload),
                            headers: headers,
                        })
                            .then((response) => {
                                if (response.status != 200) throw new Error("Failed to Fetching Data");
                                const DATA = response.data;
                                setVisitorMaster(DATA);
                            })
                    } catch (error) {
                        console.error("Error fetching Access Right Data:", error);
                    }
                })
        } catch (error) {
            console.error("Error fetching Access Right Data:", error);
        }
    }
    const exportToExcel = async () => {
        try {
            const workbook = new ExcelJS.Workbook();
            const worksheet = workbook.addWorksheet("Visits Report");
            const headingRow = worksheet.addRow(["Visits Report"]);
            headingRow.getCell(1).font = { bold: true, size: 16 };
            headingRow.getCell(1).alignment = { horizontal: "center", vertical: "middle" };
            worksheet.mergeCells("A1:F1");
            const headers = ["Farmer Name ", "Visiting Date", "Mobile Number ", "Status"];
            const headerRow = worksheet.addRow(headers);
            headerRow.eachCell((cell) => {
                cell.font = { bold: true, color: { argb: "FFFFFF" } };
                cell.fill = { type: "pattern", pattern: "solid", fgColor: { argb: "808080" } };
                cell.alignment = { horizontal: "center", vertical: "middle" };
            });
            const columnWidths = [20, 20, 20, 20];
            columnWidths.forEach((width, index) => {
                worksheet.getColumn(index + 1).width = width;
            });
            visitorMaster.forEach(({ fname, visitingdate, phonenumber, iscomplited }) => {
                const row = worksheet.addRow([fname, visitingdate, phonenumber, iscomplited]);
                row.eachCell((cell) => {
                    cell.alignment = { horizontal: "center", vertical: "middle" };
                });
            });
            const buffer = await workbook.xlsx.writeBuffer();
            const data = new Blob([buffer], { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });
            saveAs(data, "Visits Report.xlsx");
        } catch (error) {
            console.error("Error generating the Excel file:", error);
        }
    };
    const generatePDF = (visitorMaster) => {
        const doc = new jsPDF();
        const pageWidth = doc.internal.pageSize.getWidth();
        const cleanBase64 = marathiFontBase64.replace(/^data:font\/ttf;base64,/, "");
        doc.addFileToVFS("NotoSansDevanagari.ttf", cleanBase64);
        doc.addFont("NotoSansDevanagari.ttf", "NotoSansDevanagari", "normal");
        doc.setFont("helvetica", "normal");
        doc.setFontSize(16);
        const title = "Visits Report";
        doc.text(title, (pageWidth - doc.getTextWidth(title)) / 2, 20);
        const tableColumn = ["Farmer Name", "Visiting Date", "Mobile Number", "Status"];
        const tableRows = visitorMaster.map((item) => {
            return [
                item.fname || "",
                item.visitingdate || "",
                item.phonenumber || "",
                item.iscomplited || "",
            ];
        });
        const sanitizedTableRows = tableRows.map(row =>
            row.map(cell => (cell ? cell.toString() : ""))
        );
        autoTable(doc, {
            startY: 30,
            head: [tableColumn],
            body: sanitizedTableRows,
            styles: {
                font: "helvetica",
                fontSize: 12,
            },
            alternateRowStyles: { fillColor: [240, 240, 240] },
        });
        window.open(doc.output("bloburl"), "_blank");
    };
    const renderTooltip = (props) => (
        <Tooltip id="pdf-tooltip" {...props}>
            Pdf
        </Tooltip>
    );
    const renderExcelTooltip = (props) => (
        <Tooltip id="excel-tooltip" {...props}>
            Excel
        </Tooltip>
    );
    const renderPrinterTooltip = (props) => (
        <Tooltip id="printer-tooltip" {...props}>
            Printer
        </Tooltip>
    );
    const renderRefreshTooltip = (props) => (
        <Tooltip id="refresh-tooltip" {...props}>
            Refresh
        </Tooltip>
    );
    const renderCollapseTooltip = (props) => (
        <Tooltip id="collapse-tooltip" {...props}>
            Collapse
        </Tooltip>
    );
    const renderEditTooltip = (props) => (
        <Tooltip id="Edit-tooltip" {...props}>
            Edit
        </Tooltip>
    );
    const renderDeleteTooltip = (props) => (
        <Tooltip id="Delete-tooltip" {...props}>
            Delete
        </Tooltip>
    );
    const renderActionTooltip = (props) => (
        <Tooltip id="action-tooltip" {...props}>
            Action
        </Tooltip>
    );
    const columns = [
        {
            title: (
                <OverlayTrigger placement="top" overlay={<Tooltip id="fname-tooltip">Sabhasad Name</Tooltip>}>
                    <div>Sabhasad Name</div>
                </OverlayTrigger>
            ),
            dataIndex: "sabhasadname",
            render: (text) => (
                <OverlayTrigger placement="top" overlay={<Tooltip id="fname-tooltip">{text}</Tooltip>}>
                    <div style={{ textAlign: "left" }}>{text}</div>
                </OverlayTrigger>
            ),
        },
        {
            title: (
                <div >
                    <OverlayTrigger placement="top" overlay={<Tooltip id="visitingdate-tooltip">Mobile</Tooltip>}>
                        <div>Mobile</div>
                    </OverlayTrigger>
                </div>
            ),
            dataIndex: "contactnumber",
            render: (text) => (
                <OverlayTrigger placement="top" overlay={<Tooltip id="visitingdate-tooltip">{text}</Tooltip>}>
                    <div style={{ textAlign: "left" }}>{text}</div>
                </OverlayTrigger>
            ),
        },
        {
            title: (
                <OverlayTrigger placement="top" overlay={<Tooltip id="phonenumber-tooltip">Village Name</Tooltip>}>
                    <div>Village Name</div>
                </OverlayTrigger>
            ),
            dataIndex: "villagename",
            render: (text) => (
                <OverlayTrigger placement="top" overlay={<Tooltip id="phonenumber-tooltip">{text}</Tooltip>}>
                    <div style={{ textAlign: "left" }}>{text}</div>
                </OverlayTrigger>
            ),
        },
        // {
        //     title: (
        //         <OverlayTrigger placement="top" overlay={<Tooltip id="iscompleted-tooltip">Status</Tooltip>}>
        //             <div>Status</div>
        //         </OverlayTrigger>
        //     ),
        //     dataIndex: "iscomplited",
        //     render: (text) => (
        //         <OverlayTrigger placement="top" overlay={<Tooltip id="iscompleted-tooltip">{text}</Tooltip>}>
        //             <div style={{ textAlign: "left" }}>
        //                 {text === "1" ? (
        //                     <span className="btn bg-success">Completed</span>
        //                 ) : (
        //                     <span className="btn bg-warning">Pending</span>
        //                 )}
        //             </div>
        //         </OverlayTrigger>
        //     ),
        // },
        {
            title: (
                <OverlayTrigger placement="top" overlay={renderActionTooltip}>
                    < div >
                        Action
                    </div >
                </OverlayTrigger>
            ),
            dataIndex: "action",
            render: (_, record) => (
                <div className="action-table-data">
                    <div className="edit-delete-action">
                        <OverlayTrigger
                            placement="top"
                            overlay={renderEditTooltip}
                        >
                            <Link
                                className=""
                                to="#"
                                onClick={() => {
                                    OnEdit(record.sraid);
                                    const modal = document.getElementById("AddSabhsad");
                                    if (modal) {
                                        modal.classList.add("show");
                                        modal.style.display = "block";
                                        modal.setAttribute("aria-modal", "true");
                                        modal.setAttribute("role", "dialog");
                                        modal.removeAttribute("aria-hidden");
                                        const backdrop = document.createElement("div");
                                        backdrop.className = "modal-backdrop fade show";
                                        document.body.appendChild(backdrop);
                                        document.body.classList.add("modal-open");
                                        document.body.style.overflow = "hidden";
                                        document.body.style.paddingRight = "0px";
                                    }
                                }}
                            >
                                <Edit className="feather-Edit" />
                            </Link>
                        </OverlayTrigger>
                        <OverlayTrigger
                            placement="top"
                            overlay={renderDeleteTooltip}
                        >
                            <Link
                                className=""
                                to="#"
                                onClick={() => {
                                    ConfirmationAlert(record.sraid);
                                }}

                            >
                                <Trash2 className="feather-trash-2" />
                            </Link>
                        </OverlayTrigger>

                        {cardsData.map((card, index) => {
                            const isHovered = hoverIndex === index;
                            const cardStyle = {
                                transition: 'all 0.3s ease',
                                transform: isHovered ? 'scale(1.02)' : 'scale(1)',
                                boxShadow: isHovered ? '0 6px 16px rgba(0, 0, 0, 0.36)' : 'none',
                                borderRadius: '20px',
                                cursor: 'pointer',
                                height: '100%',
                            };

                            return (
                                <div
                                    className="col-6 col-sm-6 col-md-4 col-lg-2 col-xxl-2 mb-3"
                                    key={index}
                                >
                                    <Link
                                        className="mt-3"
                                        to="#"
                                        onClick={() => handleCardClick(card, record.sraid)}
                                    >
                                        <i data-feather="eye" className="feather-eye"></i>
                                    </Link>
                                </div>
                            );
                        })}

                        {/* <OverlayTrigger
                            placement="top"
                            overlay={renderDeleteTooltip}
                        >

                            <Link
                                className=""
                                to="#"
                                onClick={() => handleCardClick(card)}
                            >
                                <Trash2 className="feather-trash-2" />
                            </Link>
                        </OverlayTrigger> */}
                    </div>
                </div >
            ),
        },
    ];

    return (
        <div className="page-wrapper">






            {/* <div className="page-header">
                        <div className="page-title">
                            <h3>List of Statements</h3>
                        </div>
                        <div className="page-btn">
                            <Link to={route.ReportIndex} className="btn btn-secondary">
                                <ArrowLeft className="me-2" />
                                Back
                            </Link>
                        </div>
                    </div>

                    <div className='row'>
                        <div className="col-lg-2 col-md-2 col-sm-6 mb-2">
                            <label htmlFor="startDate" className="form-label">Start Date</label>
                            <input
                                type="date"
                                id="startDate"
                                ref={startDateRef}
                                className="form-control form-control-sm"
                                value={startDate}
                                onChange={(e) => setStartDate(e.target.value)}
                            />
                        </div>

                        <div className="col-auto mb-3 d-flex align-items-end">
                            <strong>to</strong>
                        </div>

                        <div className="col-lg-2 col-md-2 col-sm-6 mb-2">
                            <label htmlFor="endDate" className="form-label">End Date</label>
                            <input
                                type="date"
                                id="endDate"
                                ref={endDateRef}
                                className="form-control form-control-sm"
                                value={endDate}
                                onChange={(e) => setEndDate(e.target.value)}
                            />
                        </div>
                    </div> */}







            <div className="content">
                <div className="page-header">
                    <div className="add-item d-flex">
                        <div className="page-title">
                            <h3>Sabhasad Master</h3>
                            <h6>Manage Sabhasad</h6>
                        </div>
                    </div>
                    <ul className="table-top-head">
                        <li>
                            <OverlayTrigger placement="top" overlay={renderTooltip}>
                                <Link
                                    to="#"
                                    onClick={(e) => {
                                        e.preventDefault();
                                        generatePDF(visitorMaster);
                                    }}
                                >
                                    <ImageWithBasePath src="assets/img/icons/pdf.svg" alt="Download PDF" />
                                </Link>
                            </OverlayTrigger>
                        </li>

                        <li>
                            <OverlayTrigger placement="top" overlay={renderExcelTooltip}>
                                <Link data-bs-toggle="tooltip" data-bs-placement="top"
                                    onClick={exportToExcel}
                                >
                                    <ImageWithBasePath
                                        src="assets/img/icons/excel.svg"
                                        alt="img"
                                    />
                                </Link>
                            </OverlayTrigger>
                        </li>

                        <li>
                            <OverlayTrigger placement="top" overlay={renderPrinterTooltip}>
                                <Link data-bs-toggle="tooltip" data-bs-placement="top">
                                    <i data-feather="printer" className="feather-printer" />
                                </Link>
                            </OverlayTrigger>
                        </li>
                        <li>
                            <OverlayTrigger placement="top" overlay={renderRefreshTooltip}>
                                <Link data-bs-toggle="tooltip"
                                    data-bs-placement="top"
                                    onClick={() => {
                                        refreshData();
                                    }}
                                >
                                    <RotateCcw />
                                </Link>
                            </OverlayTrigger>
                        </li>
                        <li>
                            <OverlayTrigger placement="top" overlay={renderCollapseTooltip}>
                                <Link
                                    data-bs-toggle="tooltip"
                                    data-bs-placement="top"
                                    id="collapse-header"
                                    className={data ? "active" : ""}
                                    onClick={(e) => {
                                        e.preventDefault();
                                        dispatch(setToogleHeader(!data));
                                    }}
                                >
                                    <ChevronUp />
                                </Link>
                            </OverlayTrigger>
                        </li>
                    </ul>
                    <div className="page-btn">
                        <button
                            className="btn btn-added"
                            onClick={() => {
                                const modal = document.getElementById("AddSabhsad");

                                if (modal) {
                                    modal.classList.add("show");
                                    modal.style.display = "block";
                                    modal.setAttribute("aria-modal", "true");
                                    modal.setAttribute("role", "dialog");
                                    modal.removeAttribute("aria-hidden");
                                    const backdrop = document.createElement("div");
                                    backdrop.className = "modal-backdrop fade show";
                                    document.body.appendChild(backdrop);
                                    document.body.classList.add("modal-open");
                                    document.body.style.overflow = "hidden";
                                    document.body.style.paddingRight = "0px";
                                }
                            }}
                        >
                            <PlusCircle className="me-2" />
                            Add Member
                        </button>
                    </div>

                    <div className="page-btn">
                        <Link to={route.MasterIndex} className="btn btn-secondary">
                            <ArrowLeft className="me-2" />
                            Back
                        </Link>
                    </div>
                </div>

                <div className="search-container mb-3">
                    <div className="row">
                        <div className="col-lg-6 col-12 ms-auto">
                            <div className="input-group">
                                <input
                                    type="text"
                                    className="form-control"
                                    placeholder="Search"
                                    value={searchQuery}
                                    onChange={handleSearch}
                                />
                                <span className="input-group-text">
                                    <i className="fa fa-search"></i>
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
                <div className="card table-list-card">
                    <div className="card-body">
                        <div className="table-responsive responsive-no-scroll" >
                            <Table columns={columns} dataSource={visitorMaster} />
                        </div>
                    </div>
                </div>
                <Brand />
            </div>
            <AddMember SRAID={selectedData.sraid} onRefresh={refreshData} />
            {/* <AddVisitorMaster
                VAID={selectedData.vaid}
                onRefresh={refreshData}
            /> */}
        </div>

    );
};
export default Member;


