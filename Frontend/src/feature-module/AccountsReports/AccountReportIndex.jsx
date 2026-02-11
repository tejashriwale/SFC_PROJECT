import React, { useEffect, useRef, useState } from 'react';
import ImageWithBasePath from '../../core/img/imagewithbasebath';
import { useNavigate, Link } from 'react-router-dom';
import { all_routes } from "../../Router/all_routes";
import { getUserData } from '../../Context/UserData';
import axios from 'axios';
import { ArrowLeft } from "feather-icons-react/build/IconComponents";
import Swal from 'sweetalert2';
import withReactContent from 'sweetalert2-react-content';
import Select from "react-select";
function AccountReportIndex() {
    const MySwal = withReactContent(Swal);
    const route = all_routes;
    const navigate = useNavigate();
    const [isDesktop, setIsDesktop] = useState(false);
    const { userdetail } = getUserData();
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
    useEffect(() => {
        const handleResize = () => setIsDesktop(window.innerWidth >= 992);
        handleResize();
        window.addEventListener('resize', handleResize);
        return () => window.removeEventListener('resize', handleResize);
    }, []);
    const cardsData = [
        { title: 'रोज कीर्द', image: 'assets/img/avatar/reportS.png', path: '/DayBook' },
        { title: 'रोज विस्तृत कीर्द', image: 'assets/img/avatar/reportS.png', path: '/DayBook1' },
        { title: 'कॅश बूक', image: 'assets/img/avatar/reportS.png', path: 'Cash-Book' },
        { title: 'तेरीज  पत्रक', image: 'assets/img/avatar/reportS.png', path: 'trial-balance' },
        { title: 'नफातोटा पत्रक', image: 'assets/img/avatar/reportS.png', path: 'profit-loss' },
        { title: 'ताळेबंद', image: 'assets/img/avatar/reportS.png', path: 'balance-shit' },
        { title: 'सारांश खतावणी', image: 'assets/img/avatar/reportS.png', path: 'ledger' },
        { title: ' विस्तृत सारांश खतावणी', image: 'assets/img/avatar/reportS.png', path: 'ledger1' },
    ];
    useEffect(() => {
        const fetchledgerdropdown = async (type) => {
            try {
                const response = await axios.post(`${baseUrl.Url}/backend/api/GET_GenralAccount`, {
                    companyid: userdetail?.companyID || "",
                });
                const data = response.data;
                const Dropdown = data.map(({ accaid, acctm }) => ({
                    label: acctm,
                    value: accaid,
                }));
                setledger(Dropdown);
            } catch (error) {
                console.error("Error fetching DayBook data:", error);
            }
        };
        fetchledgerdropdown();
    }, [userdetail?.companyID]);
    const fetchDayBookData = async (type) => {

        try {
            const response = await axios.post(`${baseUrl.Url}/backend/api/GET_DayBookData`, {
                OrganizationID: userdetail?.companyID || "",
                DivisionID: userdetail?.departmentID || "",
                sdate: userdetail?.APPDT || "",
                edate: ""
            });
            const data = response.data;
            setDayBookData(data);

            if (type === 'summary') {
                openDayBookPrintWindow(data);
            } else if (type === 'detailed') {
                openDayBookDetailPrintWindow(data);
            } else if (type === 'cashbook') {
                openCashBookPrintWindow(data)
            }
        } catch (error) {
            console.error("Error fetching DayBook data:", error);
        }
    };
    const fetchTrailBalanceData = async () => {
        if (!startDate) {
            Swal.fire({
                icon: 'error',
                title: 'त्रुटी',
                text: 'कृपया सुरवात दिनांक निवडा.',
                allowOutsideClick: false,
                allowEscapeKey: false,
            }).then(() => {
                startDateRef.current?.focus();
            });
            return;
        }

        if (!endDate) {
            Swal.fire({
                icon: 'error',
                title: 'त्रुटी',
                text: 'कृपया शेवटचा दिनांक निवडा.',
                allowOutsideClick: false,
                allowEscapeKey: false,
            }).then(() => {
                endDateRef.current?.focus();
            });
            return;
        }

        try {
            const response = await axios.post(`${baseUrl.Url}/backend/api/GET_TrialBalanceData`, {
                organizationID: userdetail?.companyID || "",
                divisionID: userdetail?.departmentID || "",
                sdate: startDate,
                edate: endDate,
                level: "%"
            });

            const data = response.data;
            setTrialBData(data); // Keep if you use it elsewhere

            generateTrialBalanceReport(data);
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
    const openDayBookPrintWindow = (data) => {
        const cashData = data.filter(e => e.trnmode === 'C' || e.trnmode === 'T');

        const cashInHandDR = cashData.filter(e => e.acctm === 'Cash-in-Hand' && e.drcr === 'DR');
        const cashInHandCR = cashData.filter(e => e.acctm === 'Cash-in-Hand' && e.drcr === 'CR');

        const openingAmount = cashInHandDR.reduce((sum, e) => sum + e.amount, 0);
        const closingAmount = cashInHandCR.reduce((sum, e) => sum + e.amount, 0);

        const allDR = cashData.filter(e => e.drcr === 'DR' && e.acctm !== 'Cash-in-Hand');
        const allCR = cashData.filter(e => e.drcr === 'CR' && e.acctm !== 'Cash-in-Hand');

        allDR.unshift({
            _customLabel: 'आरंभीची रोख शिल्लक',
            amount: openingAmount,
            maingroup: '',
            references: [],
            trnmode: 'C'
        });

        allCR.push({
            _customLabel: 'अखेरची रोख शिल्लक',
            amount: closingAmount,
            maingroup: '',
            references: [],
            trnmode: 'C'
        });

        const groupData = (entries) => {
            const map = new Map();
            for (const e of entries) {
                const key = e._customLabel || e.maingroup || e.acctm;
                if (!map.has(key)) {
                    map.set(key, {
                        maingroup: e.maingroup,
                        _customLabel: e._customLabel,
                        amountC: 0,
                        amountT: 0,
                        references: []
                    });
                }
                const item = map.get(key);
                if (e.trnmode === 'C') {
                    item.amountC += Number(e.amount || 0);
                } else if (e.trnmode === 'T') {
                    item.amountT += Number(e.amount || 0);
                }

                if (e.acctm) {
                    item.references.push({
                        name: e.acctm,
                        amount: e.amount,
                        narration: e.narration || '',
                        trnmode: e.trnmode
                    });
                }
            }
            return Array.from(map.values());
        };

        const groupedDR = groupData(allDR);
        const groupedCR = groupData(allCR);

        let totalDRC = 0, totalDRT = 0, totalDR = 0;
        let totalCRC = 0, totalCRT = 0, totalCR = 0;
        const rows = [];

        const maxLength = Math.max(groupedDR.length, groupedCR.length);

        for (let i = 0; i < maxLength; i++) {
            const dr = groupedDR[i] || {};
            const cr = groupedCR[i] || {};

            const drHasValue = dr.amountC || dr.amountT || dr._customLabel;
            const crHasValue = cr.amountC || cr.amountT || cr._customLabel;

            if (drHasValue || crHasValue) {
                totalDRC += dr.amountC || 0;
                totalDRT += dr.amountT || 0;
                totalDR += (dr.amountC || 0) + (dr.amountT || 0);

                totalCRC += cr.amountC || 0;
                totalCRT += cr.amountT || 0;
                totalCR += (cr.amountC || 0) + (cr.amountT || 0);

                rows.push(`
                <tr>
                    <td>${drHasValue ? i + 1 : ''}</td>
                    <td class="left">${drHasValue ? (dr._customLabel || dr.maingroup) : ''}</td>
                    <td class="right">${dr.amountC || ''}</td>
                    <td class="right">${dr.amountT || ''}</td>
                    <td class="right">${drHasValue ? dr.amountC + dr.amountT : ''}</td>

                    <td>${crHasValue ? i + 1 : ''}</td>
                    <td class="left">${crHasValue ? (cr._customLabel || cr.maingroup) : ''}</td>
                    <td class="right">${cr.amountC || ''}</td>
                    <td class="right">${cr.amountT || ''}</td>
                    <td class="right">${crHasValue ? cr.amountC + cr.amountT : ''}</td>
                </tr>
            `);

                const maxRefs = Math.max(dr.references?.length || 0, cr.references?.length || 0);
                for (let j = 0; j < maxRefs; j++) {
                    const drRef = dr.references?.[j];
                    const crRef = cr.references?.[j];

                    if (drRef || crRef) {
                        rows.push(`
                        <tr class="sub-row">
                            <td></td>
                            <td class="left pink">${drRef?.name || ''}</td>
                            <td class="right pink">${drRef?.trnmode === 'C' ? drRef.amount : ''}</td>
                            <td class="right pink">${drRef?.trnmode === 'T' ? drRef.amount : ''}</td>
                            <td class="right pink"></td>

                            <td></td>
                            <td class="left pink">${crRef?.name || ''}</td>
                            <td class="right pink">${crRef?.trnmode === 'C' ? crRef.amount : ''}</td>
                            <td class="right pink">${crRef?.trnmode === 'T' ? crRef.amount : ''}</td>
                            <td class="right pink"></td>
                        </tr>
                    `);
                    }
                }
            }
        }

        rows.push(`
        <tr class="total-row">
            <th colspan="2" class="right">एकूण</th>
            <th class="right">${totalDRC}</th>
            <th class="right">${totalDRT}</th>
            <th class="right">${totalDR}</th>

            <th colspan="2" class="right">एकूण</th>
            <th class="right">${totalCRC}</th>
            <th class="right">${totalCRT}</th>
            <th class="right">${totalCR}</th>
        </tr>
    `);

        const htmlContent = `
        <html>
        <head>
            <meta charset="UTF-8" />
            <title>कीर्द</title>
            <style>
                body { 
                    font-family: 'Noto Sans Devanagari', sans-serif; 
                    margin: 20px; 
                }
                @media print { 
                    @page { size: A4 landscape; } 
                }
                .header { text-align: center; font-size: 22px; font-weight: bold; margin-bottom: 5px; }
                .sub-header { text-align: center; font-size: 18px; margin-bottom: 15px; }
                table { border-collapse: collapse; width: 100%; }
                th, td { border: 1px solid #000; padding: 6px; }
                th { background: #eee; text-align: center; }
                .left { text-align: left; }
                .right { text-align: right; }
                .pink { color: #d04a7f; font-size: 12px; font-style: italic; }
                .total-row th { background: #ddd; }
            </style>
        </head>
        <body>
            <div class="header">${userdetail?.departmentname || ''}</div>
            <div class="sub-header">दिनांक: ${userdetail.APPDT}</div>
            <div class="sub-header">दैनंदिन कीर्द</div>
            <div class="center sub-header">कालावधी: ${userdetail?.APPDT || ""} पासून ${userdetail?.APPDT || ""} पर्यंत</div>
            <table>
                <thead>
                    <tr><th colspan="5">जमा</th><th colspan="5">नावे</th></tr>
                    <tr>
                        <th>खाते क्र.</th><th>तपशील</th><th>रोख</th><th>लेखांतरण</th><th>एकूण</th>
                        <th>खाते क्र.</th><th>तपशील</th><th>रोख</th><th>लेखांतरण</th><th>एकूण</th>
                    </tr>
                </thead>
                <tbody>${rows.join('')}</tbody>
            </table>
        </body>
        </html>
    `;

        const blob = new Blob([htmlContent], { type: 'text/html' });
        const url = URL.createObjectURL(blob);
        window.open(url, '_blank');
    };
    const openDayBookDetailPrintWindow = (data) => {
        const allDR = data.filter(e => e.drcr === 'DR');
        const allCR = data.filter(e => e.drcr === 'CR');

        // Move "Cash-in-Hand" to end of CR
        // const cashInHand = allCR.find(e => e.acctm === 'Cash-in-Hand');
        // const filteredCR = allCR.filter(e => e.acctm !== 'Cash-in-Hand');
        // if (cashInHand) filteredCR.push(cashInHand);

        // Add Opening Balance to start of DR
        const openingBalance = allDR.find(e => e.accid === 'PK0034');
        const filteredDR = allDR.filter(e => e.accid !== 'PK0034');
        if (openingBalance) {
            openingBalance._customLabel = 'आरंभीची रोख शिल्लक';
            filteredDR.unshift(openingBalance);
        }

        const cashInHand = allCR.find(e => e.acctm === 'Cash-in-Hand');
        const filteredCR = allCR.filter(e => e.acctm !== 'Cash-in-Hand');
        if (cashInHand) {
            cashInHand._customLabel = 'अखेरची रोख शिल्लक';
            filteredCR.push(cashInHand);
        }

        // Group and Aggregate
        const groupAndAggregate = (entries) => {
            const map = new Map();

            for (const entry of entries) {
                const key = `${entry._customLabel || entry.acctm}-${entry.trnmode}`;
                if (!map.has(key)) {
                    map.set(key, {
                        acctm: entry.acctm,
                        trnmode: entry.trnmode,
                        _customLabel: entry._customLabel,
                        amount: 0,
                        references: [],
                    });
                }
                const item = map.get(key);
                item.amount += Number(entry.amount || 0);
                if (entry.referenceName) {
                    item.references.push({
                        name: entry.referenceName,
                        amount: Number(entry.amount || 0),
                        trnmode: entry.trnmode,
                        narration: entry.narration || ''
                    });
                }
            }

            return Array.from(map.values());
        };

        const groupedDR = groupAndAggregate(filteredDR);
        const groupedCR = groupAndAggregate(filteredCR);

        let totals = {
            dr_cash: 0, dr_transfer: 0, dr_total: 0,
            cr_cash: 0, cr_transfer: 0, cr_total: 0
        };

        const getCombinedRows = () => {
            let rows = '';
            const maxLength = Math.max(groupedDR.length, groupedCR.length);

            for (let i = 0; i < maxLength; i++) {
                const dr = groupedDR[i];
                const cr = groupedCR[i];

                const drCash = dr?.trnmode === 'C' ? dr.amount : 0;
                const drTransfer = dr?.trnmode === 'T' ? dr.amount : 0;
                const drAmount = dr?.amount || 0;

                const crCash = cr?.trnmode === 'C' ? cr.amount : 0;
                const crTransfer = cr?.trnmode === 'T' ? cr.amount : 0;
                const crAmount = cr?.amount || 0;

                if (dr) {
                    totals.dr_cash += drCash;
                    totals.dr_transfer += drTransfer;
                    totals.dr_total += drAmount;
                }

                if (cr) {
                    totals.cr_cash += crCash;
                    totals.cr_transfer += crTransfer;
                    totals.cr_total += crAmount;
                }

                rows += `
                    <tr class="tBold">
                        <td>${dr ? i + 1 : ''}</td>
                        <td class="left tBold">${dr ? (dr._customLabel || dr.acctm) : ''}</td>
                        <td class="right">${drCash || ''}</td>
                        <td class="right">${drTransfer || ''}</td>
                        <td class="right">${drAmount || ''}</td>
                        <td>${cr ? i + 1 : ''}</td>
                        <td class="left tBold">${cr ? (cr._customLabel || cr.acctm) : ''}</td>
                        <td class="right">${crCash || ''}</td>
                        <td class="right">${crTransfer || ''}</td>
                        <td class="right">${crAmount || ''}</td>
                    </tr>`;

                // Render sub-rows (references)
                const maxRefs = Math.max(dr?.references.length || 0, cr?.references.length || 0);
                for (let j = 0; j < maxRefs; j++) {
                    const drRef = dr?.references[j];
                    const crRef = cr?.references[j];

                    const drCashRef = drRef?.trnmode === 'C' ? drRef.amount : '';
                    const drTransferRef = drRef?.trnmode === 'T' ? drRef.amount : '';

                    const crCashRef = crRef?.trnmode === 'C' ? crRef.amount : '';
                    const crTransferRef = crRef?.trnmode === 'T' ? crRef.amount : '';

                    rows += `
                    <tr class="sub-row">
                        <td></td>
                        <td class="left sub-label">${drRef?.name || ''}<br>
                            <span class="narration-text">${drRef?.narration || ''}</span>
                        </td>
                        <td class="right sub-label">${drCashRef || ''}</td>
                        <td class="right sub-label">${drTransferRef || ''}</td>
                        <td class="right sub-label">${drRef?.amount || ''}</td>
                        <td></td>
                        <td class="left sub-label">${crRef?.name || ''}<br>
                            <span class="narration-text">${crRef?.narration || ''}</span>
                        </td>
                        <td class="right sub-label">${crCashRef || ''}</td>
                        <td class="right sub-label">${crTransferRef || ''}</td>
                        <td class="right sub-label">${crRef?.amount || ''}</td>
                    </tr>`;


                    //  if (drRef?.narration || crRef?.narration) {
                    // rows += `
                    //     <tr class="narration-row">
                    //         <td></td>
                    //         <td class="left narration-text">${drRef?.narration || ''}</td>
                    //         <td></td>
                    //         <td></td>
                    //         <td></td>
                    //         <td></td>
                    //         <td class="left narration-text">${crRef?.narration || ''}</td>
                    //         <td></td>
                    //         <td></td>
                    //         <td></td>
                    //     </tr>`;
                    // }
                }
            }

            return rows;
        };

        // Totals row
        const getTotalsRow = () => {
            return `
        <tr>
            <th colspan="2" class="right">एकूण</th>
            <th class="right">${totals.dr_cash}</th>
            <th class="right">${totals.dr_transfer}</th>
            <th class="right">${totals.dr_total}</th>
            <th colspan="2" class="right">एकूण</th>
            <th class="right">${totals.cr_cash}</th>
            <th class="right">${totals.cr_transfer}</th>
            <th class="right">${totals.cr_total}</th>
        </tr>`;

        };

        const htmlContent = `
            <html>
            <head>
                <meta charset="UTF-8">
                <title>विस्तृत कीर्द</title>
                <style>
                body { 
                    font-family: 'Noto Sans Devanagari',
                    sans-serif; 
                    margin: 20px; 
                }
                
                @media print { 
                    @page { size: A4 landscape; } 
                }
                .header { 
                    text-align: center; 
                    font-size: 22px; 
                    font-weight: bold; 
                    margin-bottom: 5px; 
                }
                .sub-header { 
                    text-align: center; 
                    font-size: 18px; 
                    margin-bottom: 10px; 
                }

               table {
                    border-collapse: collapse;
                    width: 100%;
                    margin-top: 10px;
                    border: 1px solid #66666647;
                }

                th, td {
                    border: 1px solid #66666647; /* Light black */
                    padding: 4px 8px;
                    font-size: 16px;
                    line-height: 1.5;
                    vertical-align: middle;
                }

                th {    
                    background-color:rgba(223, 223, 223, 0.93); 
                    text-align: center; 
                    font-weight: bold; 
                }

                .left { 
                    text-align: left; 
                }

                .right { 
                    text-align: right; 
                }
                    
                .sub-label { 
                    font-size: 14px; 
                    color: #a10000;
                    font-style: italic;
                }
                .sub-row td { 
                    padding: 3px; 
                }
                .tBold { 
                    font-weight: bold;
                }
                .narration-text {
                    font-size: 10px;          
                    font-style: italic;  
                    color:black;
                }

                .narration-row td {
                   
                }

            </style>

            </head>
            <body>
                <div class="header">${userdetail?.departmentname || 'दी जय मातादी फ्रूट कंपनी'}</div>
                <div class="sub-header">दिनांक: ${userdetail?.APPDT || ''}</div>
                <div class="sub-header">विस्तृत कीर्द</div>
                <div class="center sub-header">कालावधी: ${userdetail?.APPDT || ""} पासून ${userdetail?.APPDT || ""} पर्यंत</div>

                <table>
                    <thead>
                        <tr><th colspan="5">जमा</th><th colspan="5">नावे</th></tr>
                        <tr>
                            <th>खाते क्र.</th><th>तपशील</th><th>रोख</th><th>लेखांतरण</th><th>एकूण</th>
                            <th>खाते क्र.</th><th>तपशील</th><th>रोख</th><th>लेखांतरण</th><th>एकूण</th>
                        </tr>
                    </thead>
                    <tbody>
                        ${getCombinedRows()}
                        ${getTotalsRow()}
                    </tbody>
                </table>
            </body>
            </html>`;

        const blob = new Blob([htmlContent], { type: 'text/html' });
        const url = URL.createObjectURL(blob);
        window.open(url, '_blank');
    };
    const processTrialBalanceData = (data) => {
        const openingIndex = data.findIndex(item => item.dracc === "OPENING");
        const closingIndex = data.findIndex(item => item.cracc === "CLOSING");

        let opening = null;
        let closing = null;

        if (openingIndex !== -1) {
            opening = data.splice(openingIndex, 1)[0];
        }

        if (closingIndex !== -1) {
            closing = data.splice(closingIndex, 1)[0];
        }

        const drList = data.filter(item => item.dramt > 0);
        const crList = data.filter(item => item.cramt > 0);

        if (opening) drList.unshift(opening);
        if (closing) crList.unshift(closing);

        const maxLength = Math.max(drList.length, crList.length);
        const balancedData = [];

        for (let i = 0; i < maxLength; i++) {
            balancedData.push({
                dr: drList[i] || null,
                cr: crList[i] || null,
            });
        }

        return balancedData;
    };
    const generateTrialBalanceReport = (data) => {
        const rows = processTrialBalanceData(data);

        const totalDR = rows.reduce((sum, r) => sum + (r.dr?.dramt || 0), 0);
        const totalCR = rows.reduce((sum, r) => sum + (r.cr?.cramt || 0), 0);

        const htmlRows = rows.map(({ dr, cr }, index) => `
            <tr>
                <td >${dr ? dr.draccgid : ''}</td>
                <td >${dr ? dr.dracctm : ''}</td>
                <td class="text-right">${dr ? dr.dramt.toLocaleString('hi-IN') : ''}</td>

                <td>${cr ? cr.craccgid : ''}</td>
                <td>${cr ? cr.cracctm : ''}</td>
                <td class="text-right">${cr ? cr.cramt.toLocaleString('hi-IN') : ''}</td>
            </tr>
        `).join('');

        const htmlContent = `
            <html>
            <head>
                <meta charset="UTF-8">
                <title>तेरीज पत्रक</title>
                <style>
                    @page { size: A4 portrait; margin: 20mm; }
                    body { font-family: 'Noto Sans Devanagari', sans-serif; margin: 0 auto; padding: 20px; max-width: 210mm; }
                    .center { text-align: center; }
                    .title { font-size: 22px; font-weight: bold; }
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
                <div class="center title">${userdetail?.departmentname || 'दी जय मातादी फ्रूट कंपनी'}</div>
                <div class="center title">तेरीज पत्रक</div>
                <div class="center subtitle">कालावधी: ${formatDateToShortMonth(startDate)} पासून ${formatDateToShortMonth(endDate)} पर्यंत</div>

                <table>
                    <thead>
                        <tr>
                            <th colspan="3">जमा</th>
                            <th colspan="3">नावे</th>
                        </tr>
                    </thead>
                    <tbody>
                        ${htmlRows}
                    </tbody>
                    <tfoot>
                        <tr>
                            <td colspan="2" class="text-right bold">एकूण:</td>
                            <td class="text-right bold">${totalDR.toLocaleString('hi-IN')}</td>
                            <td colspan="2" class="text-right bold">एकूण:</td>
                            <td class="text-right bold">${totalCR.toLocaleString('hi-IN')}</td>
                        </tr>
                    </tfoot>
                </table>
            </body>
            </html>
            `;

        const blob = new Blob([htmlContent], { type: 'text/html' });
        const url = URL.createObjectURL(blob);
        window.open(url, '_blank');
    };
    const openCashBookPrintWindow = (data) => {
        const cashData = data.filter(e => e.trnmode === 'C');

        const cashInHandDR = cashData.filter(e => e.acctm === 'Cash-in-Hand' && e.drcr === 'DR');
        const cashInHandCR = cashData.filter(e => e.acctm === 'Cash-in-Hand' && e.drcr === 'CR');
        const openingAmount = cashInHandDR.reduce((sum, e) => sum + e.amount, 0);
        const closingAmount = cashInHandCR.reduce((sum, e) => sum + e.amount, 0);

        const allDR = cashData.filter(e => e.drcr === 'DR' && e.acctm !== 'Cash-in-Hand');
        const allCR = cashData.filter(e => e.drcr === 'CR' && e.acctm !== 'Cash-in-Hand');

        allDR.unshift({
            _customLabel: 'आरंभीची रोख शिल्लक',
            amount: openingAmount,
            maingroup: '',
            references: []
        });

        allCR.push({
            _customLabel: 'अखेरची रोख शिल्लक',
            amount: closingAmount,
            maingroup: '',
            references: []
        });

        const groupData = (entries) => {
            const map = new Map();
            for (const e of entries) {
                const key = e._customLabel || e.maingroup;
                if (!map.has(key)) {
                    map.set(key, {
                        maingroup: e.maingroup,
                        _customLabel: e._customLabel,
                        amount: 0,
                        references: [],
                    });
                }
                const item = map.get(key);
                item.amount += Number(e.amount || 0);
                if (e.acctm) {
                    item.references.push({
                        name: e.acctm,
                        amount: e.amount,
                        narration: e.narration || ''
                    });
                }
            }
            return Array.from(map.values());
        };

        const groupedDR = groupData(allDR);
        const groupedCR = groupData(allCR);

        const maxLength = Math.max(groupedDR.length, groupedCR.length);
        let totalDR = 0, totalCR = 0;
        const rows = [];

        for (let i = 0; i < maxLength; i++) {
            const dr = groupedDR[i];
            const cr = groupedCR[i];

            totalDR += dr?.amount || 0;
            totalCR += cr?.amount || 0;

            rows.push(`
            <tr>
                <td>${dr ? i + 1 : ''}</td>
                <td class="left">${dr ? (dr._customLabel || dr.maingroup) : ''}</td>
                <td class="right">${dr ? dr.amount : ''}</td>

                <td>${cr ? i + 1 : ''}</td>
                <td class="left">${cr ? (cr._customLabel || cr.maingroup) : ''}</td>
                <td class="right">${cr ? cr.amount : ''}</td>
            </tr>
            `);

            const maxRefs = Math.max(dr?.references.length || 0, cr?.references.length || 0);
            for (let j = 0; j < maxRefs; j++) {
                const drRef = dr?.references[j];
                const crRef = cr?.references[j];
                rows.push(`
                    <tr class="sub-row">
                    <td></td>
                    <td class="left pink">${drRef?.name || ''}</td>
                    <td class="right pink">${drRef?.amount || ''}</td>

                    <td></td>
                    <td class="left pink">${crRef?.name || ''}</td>
                    <td class="right pink">${crRef?.amount || ''}</td>
                    </tr>
                `);
            }
        }

        rows.push(`
            <tr class="total-row">
            <th colspan="2" class="right">एकूण</th>
            <th class="right">${totalDR}</th>
            <th colspan="2" class="right">एकूण</th>
            <th class="right">${totalCR}</th>
            </tr>
        `);

        const htmlContent = `
                <html>
                <head>
                    <meta charset="UTF-8" />
                    <title>कॅश बूक</title>
                    <style>
                    .header {
                            text-align: center;
                            font-size: 22px;
                            font-weight: bold;
                            margin-bottom: 5px;
                        }
                        .sub-header {
                            text-align: center;
                            font-size: 18px;
                            margin-bottom: 15px;
                    }
                    @page { size: A4 portrait; margin: 20mm; }
                    body { font-family: 'Noto Sans Devanagari', sans-serif; margin: 0 auto; padding: 20px; max-width: 210mm; }
                    table { border-collapse: collapse; width: 100%; }
                    th, td { border: 1px solid #000; padding: 6px; }
                    th { background: #eee; text-align: center; }
                    .left { text-align: left; }
                    .right { text-align: right; }
                    .pink { color: #d04a7f; font-size: 12px; font-style: italic; }
                    .total-row th { background: #ddd; }
                    </style>
                </head>
                <body>
                    <div class="header">${userdetail?.departmentname || ''}</div>
                    <div class="sub-header">दिनांक: ${userdetail.APPDT}</div>
                    <div class="sub-header">कॅश बूक</div>
                    <div class="center sub-header">कालावधी: ${userdetail?.APPDT || ""} पासून ${userdetail?.APPDT || ""} पर्यंत</div>
                    <table>
                    <thead>
                        <tr><th colspan="3">जमा</th><th colspan="3">नावे</th></tr>
                        <tr>
                        <th>खाते क्र.</th><th>तपशील</th><th>रोख</th>
                        <th>खाते क्र.</th><th>तपशील</th><th>रोख</th>
                        </tr>
                    </thead>
                    <tbody>${rows.join('')}</tbody>
                    </table>
                </body>
                </html>
            `;

        const blob = new Blob([htmlContent], { type: 'text/html' });
        const url = URL.createObjectURL(blob);
        window.open(url, '_blank');
    };
    const generateProfitLossReport = async () => {
        if (!startDate || !endDate) {
            Swal.fire({
                icon: "error",
                title: "त्रुटी",
                text: "कृपया सुरवात व शेवटचा दिनांक निवडा.",
                allowOutsideClick: false,
                allowEscapeKey: false,
            });
            return;
        }

        try {
            const response = await axios.post(`${baseUrl.Url}/backend/api/GET_ProfitLossData`, {
                organizationID: userdetail?.companyID || "",
                divisionID: userdetail?.departmentID || "",
                sdate: startDate,
                edate: endDate,
                level: "%"
            });

            const data = response.data;

            const income = data.filter(item => item.amt > 0);
            const expense = data.filter(item => item.amt < 0);

            const groupBySubgroup = (items) => {
                const grouped = {};
                for (let item of items) {
                    if (!grouped[item.subgroup]) grouped[item.subgroup] = [];
                    grouped[item.subgroup].push(item);
                }
                return grouped;
            };

            const incomeGroups = groupBySubgroup(income);
            const expenseGroups = groupBySubgroup(expense);

            const buildGroupRows = (groups) => {
                let rows = [];
                for (let [subgroup, entries] of Object.entries(groups)) {
                    rows.push({
                        isGroup: true,
                        subgroup,
                        amt: entries.reduce((sum, i) => sum + (Math.abs(i.amt) || 0), 0)
                    });
                    for (let entry of entries) {
                        rows.push({
                            isGroup: false,
                            name: entry.accountname,
                            amt: Math.abs(entry.amt) || 0
                        });
                    }
                }
                return rows;
            };

            const incomeRowsData = buildGroupRows(incomeGroups);
            const expenseRowsData = buildGroupRows(expenseGroups);
            const maxRowCount = Math.max(incomeRowsData.length, expenseRowsData.length);

            let rows = "";
            for (let i = 0; i < maxRowCount; i++) {
                const inc = incomeRowsData[i];
                const exp = expenseRowsData[i];

                const incAmt = inc?.amt !== undefined ? inc.amt.toLocaleString("hi-IN") : "";
                const expAmt = exp?.amt !== undefined ? exp.amt.toLocaleString("hi-IN") : "";

                rows += `
                    <tr${(inc?.isGroup || exp?.isGroup) ? '' : ' class="pink"'} >
                        <td>${inc ? (inc.isGroup ? `<strong>${inc.subgroup}</strong>` : inc.name) : ""}</td>
                        <td class="text-right">${inc ? incAmt : ""}</td>
                        <td>${exp ? (exp.isGroup ? `<strong>${exp.subgroup}</strong>` : exp.name) : ""}</td>
                        <td class="text-right">${exp ? expAmt : ""}</td>
                    </tr>
                `;

            }

            const incomeTotal = income.reduce((sum, item) => sum + item.amt, 0);
            const expenseTotal = expense.reduce((sum, item) => sum + Math.abs(item.amt), 0);
            const remaining = incomeTotal - expenseTotal;
            const maxTotal = Math.max(incomeTotal, expenseTotal);

            let profitLossRow = "";
            if (remaining > 0) {
                profitLossRow = `
                <tr>
                    <td></td>
                    <td></td>
                    <td class="bold">Current Profit</td>
                    <td class="text-right bold">${remaining.toLocaleString("hi-IN")}</td>
                </tr>
            `;
            } else if (remaining < 0) {
                profitLossRow = `
                <tr>
                    <td class="bold">Current Loss</td>
                    <td class="text-right bold">${Math.abs(remaining).toLocaleString("hi-IN")}</td>
                    <td></td>
                    <td></td>
                </tr>
            `;
            }

            const htmlContent = `
            <html>
            <head>
                <meta charset="UTF-8">
                <title>नफातोटा पत्रक</title>
                <style>
                    @page { size: A4 portrait; margin: 20mm; }
                    body { font-family: 'Noto Sans Devanagari', sans-serif; margin: 0 auto; padding: 20px; max-width: 210mm; }
                    .center { text-align: center; }
                    .title { font-size: 22px; font-weight: bold; }
                    .subtitle { font-size: 16px; margin-top: 4px; margin-bottom: 12px; }
                    table { width: 100%; font-size: 15px; border-collapse: collapse; }
                    th, td { padding: 6px 8px; border: 1px solid #ddd; vertical-align: top; }
                    th { background-color: rgba(223, 223, 223, 0.93); }
                    .text-right { text-align: right; }
                    .bold { font-weight: bold; }
                    tfoot td { border-top: 2px solid black; font-weight: bold; text-align: right; }
                    .pink { color: #d04a7f; font-size: 14px; font-style: italic; }
                </style>
            </head>
            <body>
                <div class="center title">${userdetail?.departmentname || 'दी जय मातादी फ्रूट कंपनी'}</div>
                <div class="center title">नफातोटा पत्रक</div>
                <div class="center subtitle">कालावधी: ${formatDateToShortMonth(startDate)} पासून ${formatDateToShortMonth(endDate)} पर्यंत</div>
                <table>
                    <thead>
                        <tr>
                            <th>उत्पन्न</th>
                            <th>रक्कम</th>
                            <th>खर्च</th>
                            <th>रक्कम</th>
                        </tr>
                    </thead>
                    <tbody>
                    ${rows}
                    ${profitLossRow}
                    </tbody>
                    <tfoot>
                    <tr>
                        <td class="text-right bold">एकूण</td>
                        <td class="text-right bold">${maxTotal.toLocaleString("hi-IN")}</td>
                        <td class="text-right bold">एकूण</td>
                        <td class="text-right bold">${maxTotal.toLocaleString("hi-IN")}</td>
                    </tr>
                    </tfoot>

                </table>
            </body>
            </html>
        `;

            const blob = new Blob([htmlContent], { type: "text/html" });
            const url = URL.createObjectURL(blob);
            window.open(url, "_blank");

        } catch (error) {
            console.error("Error fetching Profit & Loss data:", error);
            Swal.fire({
                icon: "error",
                title: "त्रुटी",
                text: "डेटा प्राप्त करण्यात अडचण आली.",
                allowOutsideClick: false,
                allowEscapeKey: false,
            });
        }
    };
    const generateLedgerReport = async () => {
        if (!startDate || !endDate || !ACCAID) {
            Swal.fire({
                icon: "error",
                title: "त्रुटी",
                text: !startDate ? "कृपया सुरवात दिनांक निवडा." : !endDate ? "कृपया शेवटचा दिनांक निवडा." : "कृपया खाते निवडा."
            });
            return;
        }

        try {
            const response = await axios.post(`${baseUrl.Url}/backend/api/GET_LedgerData`, {
                organizationID: userdetail?.companyID || "",
                divisionID: userdetail?.departmentID || "",
                sdate: startDate,
                edate: endDate,
                accaid: ACCAID
            });

            const data = response.data || [];
            if (data.length === 0) throw new Error("रिकामा डेटा");

            let rows = "";
            let totalJama = 0;
            let totalNave = 0;
            let balance = 0;

            // खात्याचा तपशील
            const accHead = data.find(item => item.genrallager !== "आरंभीची शिल्लक");
            const headerInfo = accHead?.genrallager || "";

            // आरंभीची शिल्लक
            const opening = data.find(item => item.genrallager === "आरंभीची शिल्लक");
            if (opening) {
                const openingDr = parseFloat(opening.dramt || 0);
                const openingCr = parseFloat(opening.cramt || 0);
                balance = openingDr - openingCr;

                rows += `
                <tr>
                    <td></td>
                    <td>आरंभीची शिल्लक</td>
                    <td class="text-right">-</td>
                    <td class="text-right">-</td>
                    <td class="text-right">${balance.toLocaleString("hi-IN")}</td>
                </tr>
            `;
            }

            // दिवसनिहाय गटबद्ध
            const groupedByDate = {};
            data.filter(item => item.genrallager !== "आरंभीची शिल्लक").forEach(item => {
                const dateKey = item.date.split("T")[0];
                if (!groupedByDate[dateKey]) {
                    groupedByDate[dateKey] = { jama: 0, nave: 0 };
                }

                if (item.drcr === "जमा") {
                    groupedByDate[dateKey].jama += parseFloat(item.dramt || 0);
                } else if (item.drcr === "नावे") {
                    groupedByDate[dateKey].nave += parseFloat(item.cramt || 0);
                }
            });

            // दिवसनिहाय एकाच ओळीत जमा आणि नावे टाकणे
            Object.keys(groupedByDate).sort().forEach(dateKey => {
                const entry = groupedByDate[dateKey];
                const formattedDate = formatDateToShortMonth(dateKey);

                // जमा असेल तर
                if (entry.jama > 0) {
                    totalJama += entry.jama;
                    balance += entry.jama;
                    rows += `
                    <tr>
                        <td>${formattedDate}</td>
                        <td>जमा</td>
                        <td class="text-right">${entry.jama.toLocaleString("hi-IN")}</td>
                        <td class="text-right">-</td>
                        <td class="text-right">${balance.toLocaleString("hi-IN")}</td>
                    </tr>
                `;
                }

                // नावे असेल तर
                if (entry.nave > 0) {
                    totalNave += entry.nave;
                    balance -= entry.nave;
                    rows += `
                    <tr>
                        <td>${formattedDate}</td>
                        <td>नावे</td>
                        <td class="text-right">-</td>
                        <td class="text-right">${entry.nave.toLocaleString("hi-IN")}</td>
                        <td class="text-right">${balance.toLocaleString("hi-IN")}</td>
                    </tr>
                `;
                }
            });

            const htmlContent = `
        <html>
        <head>
            <meta charset="UTF-8">
            <title>दैनिक विस्तृत सारांश खतावणी</title>
            <style>
                @page { size: A4 portrait; margin: 20mm; }
                body { font-family: 'Noto Sans Devanagari', sans-serif; margin: 0 auto; padding: 20px; max-width: 210mm; }
                .center { text-align: center; }
                .title { font-size: 18px; font-weight: bold; }
                .subtitle { font-size: 16px; margin: 4px 0; }
                .subinfo { font-size: 14px; margin: 0;  font-weight: bold;}
                hr { border-top: 2px solid #000; margin: 10px 0 12px 0; }
                table { width: 100%; border-collapse: collapse; font-size: 14px; margin-top: 10px; }
                th, td { padding: 6px 8px; border: 1px solid #000; vertical-align: top; }
                th { background-color: rgba(223, 223, 223, 0.93); }
                .text-right { text-align: right; }
                .bold { font-weight: bold; }
            </style>
        </head>
        <body>
            <p class="center subtitle" style="margin-bottom: 8px;">
            एकत्रित दैनिक सारांश खतावणी
            </p>

            <p class="center subinfo" style="margin-bottom: 8px;">
           ${headerInfo}
            </p>

            <p class="center subinfo" style="margin-bottom: 8px;">
            कालावधी: ${formatDateToShortMonth(startDate)} - ${formatDateToShortMonth(endDate)}
            </p>
            <hr />
            <table>
                <thead class="bold">
                    <tr>
                        <th rowspan="2">दिनांक</th>
                        <th rowspan="2">तपशील</th>
                        <th colspan="2">व्यवहार</th>
                        <th rowspan="2">शिल्लक बाकी</th>
                    </tr>
                    <tr>
                        <th>जमा</th>
                        <th>नावे</th>
                    </tr>
                </thead>
                <tbody>
                    ${rows}
                    <tr class="bold" style="border-top: 2px solid #000;">
                        <td colspan="2" style="text-align: right;">एकूण</td>
                        <td class="text-right">${totalJama.toLocaleString("hi-IN")}</td>
                        <td class="text-right">${totalNave.toLocaleString("hi-IN")}</td>
                        <td class="text-right">${balance.toLocaleString("hi-IN")}</td>
                    </tr>
                </tbody>
            </table>
        </body>
        </html>
        `;

            const blob = new Blob([htmlContent], { type: "text/html" });
            const url = URL.createObjectURL(blob);
            window.open(url, "_blank");

        } catch (error) {
            console.error("Error generating ledger report:", error);
            Swal.fire({
                icon: "error",
                title: "त्रुटी",
                text: "डेटा प्राप्त करण्यात अडचण आली.",
            });
        }
    };
    const generateLedgerReport1 = async () => {
        if (!startDate) {
            Swal.fire({ icon: "error", title: "त्रुटी", text: "कृपया सुरवात दिनांक निवडा." }).then(() => {
                startDateRef.current.focus();
            });
            return;
        }
        if (!endDate) {
            Swal.fire({ icon: "error", title: "त्रुटी", text: "कृपया शेवटचा दिनांक निवडा." }).then(() => {
                endDateRef.current.focus();
            });
            return;
        }
        if (!ACCAID) {
            Swal.fire({ icon: "error", title: "त्रुटी", text: "कृपया खाते निवडा." }).then(() => {
                ledgerref.current.focus();
            });
            return;
        }

        try {
            const response = await axios.post(`${baseUrl.Url}/backend/api/GET_LedgerData`, {
                organizationID: userdetail?.companyID || "",
                divisionID: userdetail?.departmentID || "",
                sdate: startDate,
                edate: endDate,
                accaid: ACCAID
            });

            const data = response.data || [];
            if (data.length === 0) throw new Error("रिकामा डेटा");

            let rows = "";
            let totalJama = 0;
            let totalNave = 0;
            let balance = 0;

            // खात्याचा तपशील (report header साठी)
            const accHead = data.find(item => item.genrallager !== "आरंभीची शिल्लक");
            const headerInfo = accHead?.genrallager || "";

            // आरंभीची शिल्लक
            const opening = data.find(item => item.genrallager === "आरंभीची शिल्लक");
            if (opening) {
                const openingDr = parseFloat(opening.dramt || 0);
                const openingCr = parseFloat(opening.cramt || 0);
                balance = openingDr - openingCr;

                rows += `
                <tr>
                   <td>${opening.genrallager === "आरंभीची शिल्लक" ? "" : formatDateToShortMonth(item.date)}</td>
                    <td>${opening.narr}</td>
                    <td class="text-right">-</td>
                    <td class="text-right">-</td>
                    <td class="text-right">${balance.toLocaleString("hi-IN")}</td>
                </tr>
            `;
            }

            // व्यवहार
            data.filter(item => item.genrallager !== "आरंभीची शिल्लक").forEach(item => {
                const jama = parseFloat(item.dramt || 0);
                const nave = parseFloat(item.cramt || 0);

                totalJama += jama;
                totalNave += nave;

                if (item.drcr === "जमा") balance += jama;
                else if (item.drcr === "नावे") balance -= nave;

                rows += `
                <tr>
                    <td>${formatDateToShortMonth(item.date)}</td>
                    <td>${item.narr}</td>
                    <td class="text-right">${jama.toLocaleString("hi-IN")}</td>
                    <td class="text-right">${nave.toLocaleString("hi-IN")}</td>
                    <td class="text-right">${balance.toLocaleString("hi-IN")}</td>
                </tr>
            `;
            });

            const htmlContent = `
        <html>
        <head>
            <meta charset="UTF-8">
            <title>दैनिक विस्तृत सारांश खतावणी</title>
            <style>
                @page { size: A4 portrait; margin: 20mm; }
                body { font-family: 'Noto Sans Devanagari', sans-serif; margin: 0 auto; padding: 20px; max-width: 210mm; }
                .center { text-align: center; }
                .title { font-size: 18px; font-weight: bold; }
                .subtitle { font-size: 16px; margin: 4px 0; }
                .subinfo { font-size: 14px; margin: 0;  font-weight: bold;}
                hr { border-top: 2px solid #000; margin: 10px 0 12px 0; }
                table { width: 100%; border-collapse: collapse; font-size: 14px; margin-top: 10px; }
                th, td { padding: 6px 8px; border: 1px solid #000; vertical-align: top; }
                th { background-color: rgba(223, 223, 223, 0.93); }
                .text-right { text-align: right; }
                .bold { font-weight: bold; }
            </style>
        </head>
        <body>
            <div class="center title" style="margin-bottom: 8px;">
            ${userdetail?.companyName || 'दी जय मातादी फ्रूट कंपनी'}
            </div>

            <p class="center subtitle" style="margin-bottom: 8px;">
            एकत्रित दैनिक विस्तृत सारांश खतावणी
            </p>

            <p class="center subinfo" style="margin-bottom: 8px;">
           ${headerInfo}
            </p>

            <p class="center subinfo" style="margin-bottom: 8px;">
            कालावधी: ${formatDateToShortMonth(startDate)} - ${formatDateToShortMonth(endDate)}
            </p>

            <hr />

            <table>
                <thead class="bold">
                    <tr>
                        <th rowspan="2">दिनांक</th>
                        <th rowspan="2">तपशील</th>
                        <th colspan="2">व्यवहार</th>
                        <th rowspan="2">शिल्लक बाकी</th>
                    </tr>
                    <tr>
                        <th>जमा</th>
                        <th>नावे</th>
                    </tr>
                </thead>

                <tbody>
                    ${rows}
                    <tr class="bold" style="border-top: 2px solid #000;">
                        <td colspan="2" style="text-align: right;">एकूण</td>
                        <td class="text-right">${totalJama.toLocaleString("hi-IN")}</td>
                        <td class="text-right">${totalNave.toLocaleString("hi-IN")}</td>
                        <td class="text-right">${balance.toLocaleString("hi-IN")}</td>
                    </tr>
                </tbody>
            </table>
        </body>
        </html>
        `;

            const blob = new Blob([htmlContent], { type: "text/html" });
            const url = URL.createObjectURL(blob);
            window.open(url, "_blank");

        } catch (error) {
            console.error("Error generating ledger report:", error);
            Swal.fire({
                icon: "error",
                title: "त्रुटी",
                text: "डेटा प्राप्त करण्यात अडचण आली.",
            });
        }
    };
    const generateBalanceShitReport = async () => {
        if (!startDate) {
            Swal.fire({ icon: "error", title: "त्रुटी", text: "कृपया सुरवात दिनांक निवडा." }).then(() => {
                startDateRef.current.focus();
            });
            return;
        }
        if (!endDate) {
            Swal.fire({ icon: "error", title: "त्रुटी", text: "कृपया शेवटचा दिनांक निवडा." }).then(() => {
                endDateRef.current.focus();
            });
            return;
        }

        try {
            const response = await axios.post(`${baseUrl.Url}/backend/api/GET_BalanceshitData`, {
                organizationID: userdetail?.companyID || "",
                divisionID: userdetail?.departmentID || "",
                sdate: startDate || "",
                edate: endDate || ""
            });

            const data = response.data || [];
            const formatAmt = amt => Math.abs(amt).toLocaleString("hi-IN");

            const groupData = (entries, type) => {
                const grouped = {};
                for (const item of entries.filter(e => e.crdr === type)) {
                    const mg = item.maingroup || "इतर";
                    const sg = item.subgroup || "इतर";
                    if (!grouped[mg]) grouped[mg] = {};
                    if (!grouped[mg][sg]) grouped[mg][sg] = [];
                    grouped[mg][sg].push(item);
                }
                return grouped;
            };

            const jamaGroups = groupData(data, "जमा");
            const naveGroups = groupData(data, "नावे");


            const buildRowsWithoutMainGroup = (grouped) => {
                const rows = [];
                for (const subMap of Object.values(grouped)) {
                    for (const [sg, items] of Object.entries(subMap)) {
                        let sgTotal = items.reduce((sum, item) => sum + parseFloat(item.amt || 0), 0);
                        rows.push({ type: "subgroup", name: sg, amt: sgTotal });
                        for (const item of items) {
                            rows.push({ type: "account", name: item.accountname, amt: parseFloat(item.amt || 0) });
                        }
                    }
                }
                return rows;
            };
            const leftRows = buildRowsWithoutMainGroup(jamaGroups);
            const rightRows = buildRowsWithoutMainGroup(naveGroups);
            const maxLength = Math.max(leftRows.length, rightRows.length);

            const totalLeft = leftRows
                .filter(item => item.type === "subgroup")
                .reduce((sum, item) => sum + parseFloat(item.amt || 0), 0);

            const totalRight = rightRows
                .filter(item => item.type === "subgroup")
                .reduce((sum, item) => sum + parseFloat(item.amt || 0), 0);
            // "अखेरची रोख शिल्लक" dynamic data मधून शोधून वर insert करा
            const finalCashBalance = data.find(
                d => d.accountname === "अखेरची रोख शिल्लक" && d.crdr === "नावे"
            );

            if (finalCashBalance) {
                const amt = parseFloat(finalCashBalance.amt || 0);

                // Remove any existing "अखेरची रोख शिल्लक" entries from rightRows
                const filteredRightRows = rightRows.filter(
                    row => row.name !== "अखेरची रोख शिल्लक"
                );

                // Now insert only once at top
                filteredRightRows.unshift(
                    // { type: "account", name: finalCashBalance.accountname, amt },
                    { type: "subgroup", name: finalCashBalance.accountname, amt }
                );

                // Assign back to rightRows
                rightRows.length = 0;
                rightRows.push(...filteredRightRows);
            }

            let balanceRows = "";

            for (let i = 0; i < maxLength; i++) {
                const left = leftRows[i];
                const right = rightRows[i];

                let leftHTML = "<td></td><td></td>", rightHTML = "<td></td><td></td>";

                if (left) {
                    if (left.type === "subgroup")
                        leftHTML = `<td class="subgroup-title">${left.name}</td><td class="amt subgroup-title">${left.amt}</td>`;
                    else if (left.type === "account")
                        leftHTML = `<td class="account">${left.name}</td><td class="amt account">${left.amt}</td>`;
                }

                if (right) {
                    if (right.type === "subgroup")
                        rightHTML = `<td class="subgroup-title">${right.name}</td><td class="amt subgroup-title">${right.amt}</td>`;
                    else if (right.type === "account")
                        rightHTML = `<td class="account">${right.name}</td><td class="amt account">${right.amt}</td>`;
                }

                balanceRows += `<tr>${leftHTML}${rightHTML}</tr>`;
            }

            balanceRows += `<tr class="total">
                <td class="amt"><b>एकूण</b></td>
                <td class="amt"><b>${formatAmt(totalLeft)}</b></td>
                <td class="amt"><b>एकूण</b></td>
                <td class="amt"><b>${formatAmt(totalRight)}</b></td>
            </tr>`;


            const html = `<!DOCTYPE html>
            <html lang="mr">
            <head>
            <meta charset="UTF-8">
            <title>ताळेबंद</title>
            <style>
                @page { size: A4 portrait; margin: 20mm; }
                body {
                    font-family: 'Noto Sans Devanagari', sans-serif;
                    margin: 0 auto;
                    padding: 20px;
                    max-width: 210mm;
                }
                h2, h3, p { text-align: center; margin: 4px 0; }
                table {
                    width: 100%;
                    border-collapse: collapse;
                    margin-top: 15px;
                }
                .center { text-align: center; }
                .title { font-size: 22px; font-weight: bold; }
                .subtitle { font-size: 16px; margin-top: 4px; margin-bottom: 12px; }
                thead th {
                    border: 1px solid #ccc; 
                    padding: 6px;
                    background: #e4e4e4;
                    text-align: center;
                    font-size: 15px;
                }
                tbody td {
                    border: 1px solid #ccc;
                    padding: 4px 8px;
                    font-size: 14px;
                    vertical-align: top;
                }
                .amt {
                    text-align: right;
                }
                .total {
                    font-weight: bold;
                }
                .subgroup-title {
                    font-weight: bold;
                    color: #000;
                    font-size: 14px;
                }
                .account {
                    padding-left: 20px;
                    font-style: italic;
                    color: #d04a7f;
                }
            </style>

            </head>
            <body>
            <div class="container">
                 <div class="center title">${userdetail?.departmentname || 'दी जय मातादी फ्रूट कंपनी'}</div>
                <div class="center title">ताळेबंद</div>
                <div class="center subtitle">कालावधी: ${formatDateToShortMonth(startDate)} पासून ${formatDateToShortMonth(endDate)} पर्यंत</div>

                <table>
                    <thead>
                        <tr>
                            <th>भांडवल व देणी</th>
                            <th class="amt">रक्कम</th>
                            <th>मालमत्ता व येणी</th>
                            <th class="amt">रक्कम</th>
                        </tr>
                    </thead>
                    <tbody>
                        ${balanceRows}
                    </tbody>
                </table>
            </div>
            </body>
            </html>`;

            const blob = new Blob([html], { type: "text/html" });
            const url = URL.createObjectURL(blob);
            window.open(url, "_blank");

        } catch (error) {
            console.error(error);
            Swal.fire({
                icon: "error",
                title: "त्रुटी",
                text: "डेटा मिळण्यात संयोग्य आली."
            });
        }
    };
    const handleCardClick = async (card) => {
        if (card.path === '/DayBook') {
            await fetchDayBookData('summary');
        } else if (card.path === '/DayBook1') {
            await fetchDayBookData('detailed');
        } else if (card.path === 'Cash-Book') {
            await fetchDayBookData('cashbook');
        } else if (card.path === 'trial-balance') {
            await fetchTrailBalanceData();
        } else if (card.path === 'profit-loss') {
            await generateProfitLossReport();
        } else if (card.path === 'ledger') {
            await generateLedgerReport();
        } else if (card.path === 'ledger1') {
            await generateLedgerReport1();
        } else if (card.path === 'balance-shit') {
            await generateBalanceShitReport();
        }
        else if (card.openInNewTab) {
            const reportUrl = window.location.origin + card.path;
            window.open(reportUrl, '_blank');
        } else if (card.path) {
            navigate(card.path, { state: { autoOpen: card.autoOpen || false } });
        }
    };
    return (
        <div className="page-wrapper pagehead">
            <div className="content">
                <div className="page-header">
                    <div className="page-title">
                        <h3>अकाऊंट पत्रके  सूची</h3>
                    </div>
                    <div className="page-btn">
                        <Link to={route.ReportIndex} className="btn btn-secondary">
                            <ArrowLeft className="me-2" />
                            मागे
                        </Link>
                    </div>
                </div>

                <div className='row'>
                    <div className="col-lg-2 col-md-2 col-sm-6 mb-2">
                        <label htmlFor="startDate" className="form-label">सुरवात दिनांक</label>
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
                        <strong>ते</strong>
                    </div>

                    <div className="col-lg-2 col-md-2 col-sm-6 mb-2">
                        <label htmlFor="endDate" className="form-label">शेवटचा दिनांक</label>
                        <input
                            type="date"
                            id="endDate"
                            ref={endDateRef}
                            className="form-control form-control-sm"
                            value={endDate}
                            onChange={(e) => setEndDate(e.target.value)}
                        />
                    </div>

                    {/* Account Select */}
                    <div className="col-lg-4 col-md-4 col-sm-6 mb-2">
                        <label className="form-label">खाते</label>
                        <Select
                            ref={ledgerref}
                            placeholder="Select Counter"
                            classNamePrefix="react-select"
                            options={ledger}
                            value={ledger.find((option) => option.value === ACCAID) || null}
                            onChange={(selectedOption) => {
                                setACCAID(selectedOption.value);
                            }}
                            openMenuOnFocus={true}
                        />
                    </div>
                </div>


                <div className="row mt-4">
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
                                <div
                                    className="card text-center"
                                    style={cardStyle}
                                    onMouseEnter={() => setHoverIndex(index)}
                                    onMouseLeave={() => setHoverIndex(null)}
                                    onClick={() => handleCardClick(card)}
                                >
                                    <div className="card-header border-bottom-0 pb-0"></div>
                                    <div className="card-body pt-1 d-flex flex-column align-items-center justify-content-center" style={{ height: '120px' }}>
                                        <span className="avatar avatar-xl avatar-rounded me-2 mb-2">
                                            <ImageWithBasePath src={card.image} alt={card.title} />
                                        </span>
                                        <div className="fw-semibold fs-16">{card.title}</div>
                                    </div>
                                </div>
                            </div>
                        );
                    })}
                </div>
            </div>
        </div>
    );
}
export default AccountReportIndex;
