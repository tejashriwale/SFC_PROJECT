
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
import { baseUrl } from '../../core/json/custom';
import { openFarmerLedgerReportInNewTab } from '../Reports/FarmerLedgerReport';
function ReportsInd() {
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
    const customerRef = useRef(null);
    const ledgerref = useRef(null);
    const [ledger, setledger] = useState([]);
    const [ACCAID, setACCAID] = useState();
    const [Customers, setCustomers] = useState([]);
    const [formData, setFormData] = useState({ Customer: "", CustomerName: "", SaleCustomerName: "", SaleCustomer: "" });
    const [TotalPurchasedata, setTotalPurchaseData] = useState([]);
    const [showCustomerModal, setShowCustomerModal] = useState(false);
    const [showCustomerModal1, setShowCustomerModal1] = useState(false);
    const [farmerOptions, setFarmerOptions] = useState([]);
    const [cropOptions, setCropOptions] = useState([]);
    const [selectedFarmer, setSelectedFarmer] = useState(null);
    const [selectedCrop, setSelectedCrop] = useState(null);
    useEffect(() => {
        const handleResize = () => setIsDesktop(window.innerWidth >= 992);
        handleResize();
        window.addEventListener('resize', handleResize);
        return () => window.removeEventListener('resize', handleResize);
    }, []);

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

    // Fetch farmers list for Farmer Ledger
    useEffect(() => {
        const fetchFarmers = async () => {
            try {
                const response = await axios.post(
                    `${baseUrl.Url}/backend/api/GET_FarmerName`,
                    {
                        companyid: userdetail?.companyID || "",
                    },
                    {
                        headers: {
                            "Content-Type": "application/json",
                            Accept: "*/*"
                        }
                    }
                );

                console.log("Farmers API Response:", response.data);

                if (response.data && Array.isArray(response.data)) {
                    const options = response.data.map(farmer => ({
                        value: farmer.faid,
                        label: farmer.fname
                    }));
                    setFarmerOptions([
                        { value: '%', label: 'All Farmers' },
                        ...options
                    ]);
                    console.log("Farmer options set:", options.length);
                }
            } catch (error) {
                console.error("Error fetching farmers:", error);
            }
        };

        if (userdetail?.companyID) {
            fetchFarmers();
        }
    }, [userdetail]);

    // Fetch crops list for Farmer Ledger
    useEffect(() => {
        const fetchCrops = async () => {
            try {
                const response = await axios.post(
                    `${baseUrl.Url}/backend/api/GETItemDropdown`,
                    {
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

                console.log("Crops API Response:", response.data);

                if (response.data && Array.isArray(response.data)) {
                    const options = response.data.map(item => ({
                        value: item.itemaid,
                        label: item.itemne
                    }));
                    setCropOptions([
                        { value: '%', label: 'All Crops' },
                        ...options
                    ]);
                    console.log("Crop options set:", options.length);
                }
            } catch (error) {
                console.error("Error fetching crops:", error);
            }
        };

        if (userdetail?.companyID) {
            fetchCrops();
        }
    }, [userdetail]);

    const handleCustomerChange = async (selectedOption) => {

        const selectedCustomer = selectedOption ? selectedOption.value : "";
        const selectedCustomer1 = selectedOption ? selectedOption.label : "";

        console.log("llllllllllllllllllllllllllllllllllllllll", selectedCustomer1)
        setFormData((prev) => ({
            ...prev,
            Customer: selectedCustomer,
            CustomerName: selectedCustomer1,

        }));


        // if (card.path === 'sales-updated') {

        //     await fetchSalesupdatedData();
        //     console.log("Ssssssssssssssssssssssssssssss")
        // } else if (card.path === 'customer') {
        //     await fetchCustomerData(selectedCustomer);
        //     console.log("Cccccccccccccccccccccccccccccccccccccccccccc")

        // }





        await fetchCustomerData(selectedCustomer, selectedCustomer1);
        // await fetchSalesupdatedData();
        // fetchCustomerData(selectedCustomer);
    };

    const handleCustomerChange1 = async (selectedOption) => {

        const selectedCustomer = selectedOption ? selectedOption.value : "";
        const selectedCustomer1 = selectedOption ? selectedOption.label : "";

        console.log("llllllllllllllllllllllllllllllllllllllll", selectedCustomer1)
        setFormData((prev) => ({
            ...prev,
            SaleCustomer: selectedCustomer,
            SaleCustomerName: selectedCustomer1,

        }));


        await fetchSalesupdatedData(selectedCustomer, selectedCustomer1);

    };

    const cardsData = [
        { title: 'Crop Transport Iist', image: 'assets/img/avatar/reportS.png', path: 'trial-balance' },
        { title: 'Total Purchase', image: 'assets/img/avatar/reportS.png', path: 'totalpurchase' },
        // { title: 'Sales', image: 'assets/img/avatar/reportS.png', path: 'sales' },
        { title: 'Profit Loss', image: 'assets/img/avatar/reportS.png', path: 'profit-loss' },
        { title: 'Customer Ledger', image: 'assets/img/avatar/reportS.png', path: 'customer' },
        { title: 'Sales report', image: 'assets/img/avatar/reportS.png', path: 'sales-updated' },
        { title: 'Farmer Ledger', image: 'assets/img/avatar/reportS.png', path: route.FarmerLedger },
    ];

    const fetchTrailBalanceData = async () => {
        if (!startDate) {
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'Please select the start date.',
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
                title: 'Error',
                text: 'Please select the end date.',
                allowOutsideClick: false,
                allowEscapeKey: false,
            }).then(() => {
                endDateRef.current?.focus();
            });
            return;
        }

        try {
            const response = await axios.post(`${baseUrl.Url}/backend/api/GET_CropTransportInvoice`, {
                companyid: userdetail?.companyID || "",
                deptid: userdetail?.departmentID || "",
                startDate: startDate,
                endDate: endDate,
                // level: "%",
                ctaid: "%",
            });

            const data = response.data;
            setTrialBData(data); // Keep if you use it elsewhere

            generateTrialBalanceReport(data);
        } catch (error) {
            console.error("Error fetching Trial Balance data:", error);
        }
    };

    const fetchTotalPurchaseData = async () => {
        if (!startDate) {
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'Please select the start date.',
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
                title: 'Error',
                text: 'Please select the end date.',
                allowOutsideClick: false,
                allowEscapeKey: false,
            }).then(() => {
                endDateRef.current?.focus();
            });
            return;
        }

        try {
            const response = await axios.post(`${baseUrl.Url}/backend/api/GET_TotalPurchaseReport`, {
                companyid: userdetail?.companyID || "",
                deptid: userdetail?.departmentID || "",
                startdate: startDate,
                enddate: endDate,
                cropid: "%",
                // level: "%"
            });

            const data = response.data;
            // setTrialBData(data); // Keep if you use it elsewhere
            setTotalPurchaseData(data);

            generateTotalPurchaseReport(data);
        } catch (error) {
            console.error("Error fetching Total Purchase data:", error);
        }
    };

    const fetchSalesData = async () => {
        if (!startDate) {
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'Please select a start date.',
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
                title: 'Error',
                text: 'Please select a end date',
                allowOutsideClick: false,
                allowEscapeKey: false,
            }).then(() => {
                endDateRef.current?.focus();
            });
            return;
        }

        try {
            const response = await axios.post(`${baseUrl.Url}/backend/api/GET_VechicleProfitReport`, { //GET_Report_Sales
                // ctaid: "%",
                // companyid: userdetail?.companyID || "",
                // deptid: userdetail?.departmentID || "",
                // startdate: startDate,
                // enddate: endDate,

                companyid: userdetail?.companyID || "",
                deptid: userdetail?.departmentID || "",
                edate: formatToShortDate(endDate),
                sdate: formatToShortDate(startDate),

                // level: "%"
            });

            const data = response.data;
            // setTrialBData(data); // Keep if you use it elsewhere
            // setTotalPurchaseData(data);

            generateSalesReport(data);
        } catch (error) {
            console.error("Error fetching Sales data:", error);
        }
    };

    const fetchSalesupdatedData = async (selectedCustomer, selectedCustomer1) => {
        setShowCustomerModal1(false);
        if (!startDate) {
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'Please select a start date.',
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
                title: 'Error',
                text: 'Please select a end date',
                allowOutsideClick: false,
                allowEscapeKey: false,
            }).then(() => {
                endDateRef.current?.focus();
            });
            return;
        }

        try {
            const response = await axios.post(`${baseUrl.Url}/backend/api/GET_SalesReport`, { //GET_Report_Sales
                // ctaid: "%",
                // companyid: userdetail?.companyID || "",
                // deptid: userdetail?.departmentID || "",
                // startdate: startDate,
                // enddate: endDate,
                maid: selectedCustomer,
                slvmid: "%",
                companyid: userdetail?.companyID || "",
                deptid: userdetail?.departmentID || "",
                edate: formatToShortDate(endDate),
                sdate: formatToShortDate(startDate),

                // level: "%"
            });

            const data = response.data;
            // setTrialBData(data); // Keep if you use it elsewhere
            // setTotalPurchaseData(data);

            generateSalesupdatedReport(data, selectedCustomer1);
        } catch (error) {
            console.error("Error fetching Sales data:", error);
        }
    };

    function formatToShortDate(dateStr) {
        const date = new Date(dateStr);
        return `${date.getDate()} ${date.toLocaleString('default', { month: 'short' }).toLowerCase()} ${date.getFullYear()}`;
    }

    // Example usage:
    const formattedDate = formatToShortDate("2025-9-01");
    console.log(formattedDate); // "1 sep 2025"


    const fetchProfitLossData = async () => {
        if (!startDate) {
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'Please select a start date.',
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
                title: 'Error',
                text: 'Please select a end date',
                allowOutsideClick: false,
                allowEscapeKey: false,
            }).then(() => {
                endDateRef.current?.focus();
            });
            return;
        }

        try {
            const response = await axios.post(`${baseUrl.Url}/backend/api/GET_VechicleProfitReport`, {
                // ctaid: "%",
                companyid: userdetail?.companyID || "",
                deptid: userdetail?.departmentID || "",
                edate: formatToShortDate(endDate),
                sdate: formatToShortDate(startDate),
                // level: "%"
            });

            const data = response.data;
            console.log(data, "1111111111111111111111111111111111111111111")
            setTrialBData(data); // Keep if you use it elsewhere
            setTotalPurchaseData(data);

            generateProfitLossReport(data);
        } catch (error) {
            console.error("Error fetching Profit Loss data:", error);
        }
    };

    const fetchCustomerData = async (selectedCustomer, selectedCustomer1) => {
        setShowCustomerModal(false);
        // if (!startDate) {
        //     Swal.fire({
        //         icon: 'error',
        //         title: 'Error',
        //         text: 'Please select a start date.',
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
        //         text: 'Please select a end date',
        //         allowOutsideClick: false,
        //         allowEscapeKey: false,
        //     }).then(() => {
        //         endDateRef.current?.focus();
        //     });
        //     return;
        // }


        // if (!formData.Customer) {
        //     Swal.fire({
        //         icon: 'error',
        //         title: 'Error',
        //         text: 'Please select a Customer',
        //         allowOutsideClick: false,
        //         allowEscapeKey: false,
        //     }).then(() => {
        //         customerRef.current?.focus();
        //     });
        //     return;
        // }

        try {
            const response = await axios.post(`${baseUrl.Url}/backend/api/GET_CustomerLedger`, {
                companyid: userdetail?.companyID || "",
                deptid: userdetail?.departmentID || "",
                refaid: "%",
                maid: selectedCustomer,
                edate: formatToShortDate(endDate),
                sdate: formatToShortDate(startDate)
            });

            const data = response.data;
            console.log(data, "CustomerLedger data");

            // Fetch detail data for each unique refaid to get crop, quantity, weight info
            const uniqueRefaids = [...new Set(data.map(r => r.refaid).filter(Boolean))];
            const detailMap = {};
            const headers = { "Content-Type": "application/json", Accept: "*/*" };

            await Promise.all(uniqueRefaids.map(async (refaid) => {
                try {
                    const detailRes = await axios.post(
                        `${baseUrl.Url}/backend/api/GET_StockLoadingVehicleDetail`,
                        {
                            svvmid: refaid,
                            companyid: userdetail?.companyID || "",
                            deptid: userdetail?.departmentID || ""
                        },
                        { headers }
                    );
                    if (detailRes.data && detailRes.data.length > 0) {
                        const details = detailRes.data;
                        const cropNames = [...new Set(details.map(d => d.cropname).filter(Boolean))].join(', ');
                        const totalCrt = details.reduce((s, d) => s + (parseFloat(d.count) || 0), 0);
                        const totalCharges = details.reduce((s, d) => s + (parseFloat(d.charges) || 0), 0);
                        // Quantity/Kg = sum of totalamount for each detail (totalamount = count * rate = weight in value)
                        // Weight per SKU = if totalamount and rate available, qtykg = totalamount/rate per row, then weight = qtykg/count
                        let totalQtyKg = 0;
                        details.forEach(d => {
                            const cnt = parseFloat(d.count) || 0;
                            const rate = parseFloat(d.rate) || 0;
                            const totAmt = parseFloat(d.totalamount) || 0;
                            if (rate > 0) {
                                totalQtyKg += totAmt / rate;
                            } else {
                                totalQtyKg += cnt;
                            }
                        });
                        const weightPerSku = totalCrt > 0 ? (totalQtyKg / totalCrt) : 0;
                        detailMap[refaid] = {
                            cropname: cropNames,
                            wtpersku: weightPerSku,
                            qtycrt: totalCrt,
                            qtykg: totalQtyKg,
                            pcharges: totalCharges
                        };
                    }
                } catch (err) {
                    console.error("Error fetching detail for refaid:", refaid, err);
                }
            }));

            // Merge detail data into ledger rows
            const enrichedData = data.map(r => {
                const detail = detailMap[r.refaid] || {};
                return {
                    ...r,
                    cropname: r.cropname || detail.cropname || '',
                    wtpersku: r.wtpersku || detail.wtpersku || 0,
                    qtycrt: r.qtycrt || detail.qtycrt || 0,
                    qtykg: r.qtykg || detail.qtykg || 0,
                    pcharges: r.pcharges || detail.pcharges || 0,
                };
            });

            generateCustomerReport(enrichedData, selectedCustomer1);
        } catch (error) {
            console.error("Error fetching Customer Ledger data:", error);
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
            date: item.date || "",
            crop: item.crop || "",
            vehchno: item.vehchno || "",
            cgrade: item.cgrade || "",
            crtcnt: item.crtcnt || 0,
            iwdamt: item.iwdamt || 0,
            examt: item.examt || 0,
            itemne: item.itemne || "",
            fname: item.fname || "",
            gradename: item.implicationTitle || "",
            samt: item.samt || 0, // if profit or credit amount exists
        }));
    };

    const generateTrialBalanceReport = (data) => {
        // const rows = processTrialBalanceData(data);
        const mapped = processData(data);

        console.log("datadatadatadatadatadatadatadatadatadatadatadatadatadatadatadatadata", data)

        // const totalDR = TrialBData.reduce((sum, r) => sum + (crtcnt || 0), 0);
        const totalDR = mapped.reduce((sum, r) => sum + (r.crtcnt || 0), 0);
        const totalCR = mapped.reduce((sum, r) => sum + (r.iwdamt || 0), 0);
        const totalSL = mapped.reduce((sum, r) => sum + (r.samt || 0), 0);
        const totalEX = mapped.reduce((sum, r) => sum + (r.examt || 0), 0);
        const totalTentative = mapped.reduce((sum, r) => sum + (r.tentativeprofit || 0), 0);


        console.log("totalDR totalDR totalDR", totalDR)

        const htmlRows = mapped.map(({ date, crop, vehchno, cgrade, crtcnt, samt, iwdamt, examt, gradename, itemne, fname }) => `
            <tr>
               
                <td class="text-center">${date}</td>
                <td class="text-left">${fname}</td>
                <td class="text-left">${itemne}</td>
                <td class="text-center">${vehchno}</td>
                <td class="text-center">${gradename}</td>
                <td class="text-right">${crtcnt}</td>
                <td class="text-right">${examt}</td>
                <td class="text-right">${iwdamt}</td>
             <!--    <td class="text-right">${samt}</td>
                <td class="text-right">${totalTentative}</td>-->
            </tr>
        `).join('');

        const htmlContent = `
            <html>
            <head>
                <meta charset="UTF-8">
                <title>Crop Transport Iist</title>
                <style>
                    @page { size: A4 landscape; margin: 5mm; }
                    body { font-family: 'Noto Sans Devanagari', sans-serif; margin: 0 auto; padding: 20px; max-width: 300mm; }
                    .center { text-align: center; }
                    .title { font-size: 22px; font-weight: bold; margin-top:3mm; }
                    .subtitle { font-size: 16px; margin-top: 4px; margin-bottom: 12px; }
                    table { width: 100%; font-size: 15px; border-collapse: collapse; }
                    th, td { padding: 6px 8px; border: 1px solid #ddd; vertical-align: top; }
                    thead th { background-color: rgba(223, 223, 223, 0.93); }
                    .text-right { text-align: right; }
                    .bold { font-weight: bold; }
                    tfoot td { font-weight: bold; text-align: right; }
                </style>
            </head>
            <body>
                <div class="center title">${userdetail?.departmentname || 'श्री साई फ्रूट्स कंपनी'}</div>
                <div class="center title">Crop Transport Iist</div>
                <div class="center subtitle">Duration: ${formatDateToShortMonth(startDate)} <b> to </b> ${formatDateToShortMonth(endDate)} </div>

                <table>
                    <thead>
                        <tr>
                            <th style="min-width: 90px;">Date</th>
                            <th style="min-width: 200px;">Farmer Name</th>
                            <th style="min-width: 80px;">Crop Name</th>
                            <th style="min-width: 80px;">Vehical Number</th>
                            <th style="min-width: 70px;">Grade</th>
                            <th style="min-width: 50px;">Total Crate</th>
                            <th style="min-width: 50px;">Total Expense</th>
                            <th style="min-width: 50px;">Total Inward</th>
                           
                        </tr>
                    </thead>
                    <tbody>
                        ${htmlRows}
                    </tbody>
                    <tfoot>
                        <tr>
                            <th colspan="5" class="text-right bold">Total:</th>
                            <th class="text-right bold">${totalDR.toLocaleString('hi-IN')}</th>
                            <th class="text-right bold">${totalEX.toLocaleString('hi-IN')}</th>
                            <th class="text-right bold">${totalCR.toLocaleString('hi-IN')}</th>
                           
                        </tr>
                    </tfoot>
                </table>
                <div class="btn-container" style="text-align:center;margin:20px 0;">
                    <button onclick="window.print()" style="padding:10px 28px;background:#3b82f6;color:#fff;border:none;border-radius:5px;cursor:pointer;font-size:14px;font-weight:600;margin:0 8px;">Print</button>
                    <button onclick="downloadPDF('CropTransportList')" style="padding:10px 28px;background:#198754;color:#fff;border:none;border-radius:5px;cursor:pointer;font-size:14px;font-weight:600;margin:0 8px;">Download PDF</button>
                    <button onclick="window.close()" style="padding:10px 28px;background:#ef4444;color:#fff;border:none;border-radius:5px;cursor:pointer;font-size:14px;font-weight:600;margin:0 8px;">Close</button>
                </div>
                <script src="https://cdnjs.cloudflare.com/ajax/libs/html2canvas/1.4.1/html2canvas.min.js"></script>
                <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/2.5.1/jspdf.umd.min.js"></script>
                <script>
                function downloadPDF(name) {
                    var btns = document.querySelector('.btn-container'); btns.style.display='none';
                    html2canvas(document.body, {scale:2, useCORS:true, backgroundColor:'#fff'}).then(function(canvas) {
                        var pdf = new jspdf.jsPDF('l','mm','a4');
                        var pw = pdf.internal.pageSize.getWidth()-10, ph = pdf.internal.pageSize.getHeight()-10;
                        var iw = pw, ih = canvas.height*iw/canvas.width;
                        if(ih<=ph){pdf.addImage(canvas.toDataURL('image/png'),'PNG',5,5,iw,ih);}
                        else{var rem=canvas.height,pos=0,sl=Math.floor(canvas.width*ph/iw);
                        while(rem>0){var pc=document.createElement('canvas');pc.width=canvas.width;pc.height=Math.min(sl,rem);
                        pc.getContext('2d').drawImage(canvas,0,pos,canvas.width,pc.height,0,0,canvas.width,pc.height);
                        if(pos>0)pdf.addPage();pdf.addImage(pc.toDataURL('image/png'),'PNG',5,5,iw,pc.height*iw/canvas.width);
                        pos+=sl;rem-=sl;}}
                        pdf.save(name+'_Report.pdf'); btns.style.display='';
                    }).catch(function(){btns.style.display='';});
                }
                </script>
            </body>
            </html>
            `;

        const blob = new Blob([htmlContent], { type: 'text/html' });
        const url = URL.createObjectURL(blob);
        window.open(url, '_blank');
    };


    const generateTotalPurchaseReport = (data) => {
        // Use original API data directly
        const rows = data;

        const totalInvartcaret = rows.reduce((sum, r) => sum + Number(r.cropinvert), 0);
        const totalWeight = rows.reduce((sum, r) => sum + (r.weight), 0);
        const totalRate = rows.reduce((sum, r) => sum + (r.rate), 0);
        const totalAmount = rows.reduce((sum, r) => sum + ((r.weight || 0) * (r.rate || 0)), 0);

        const htmlRows = rows.map((r) => `
            <tr>
                <td class="text-center">${r.date || ''}</td>
                <td class="text-left">${r.fname || ''}</td>
                <td class="text-center">${r.vehno || ''}</td>               
                <td>${r.cropname || ''}</td>
                <td>${r.implicationTitle || ''}</td>
                <td class="text-right">${r.cropinvert || ''}</td>
                <td class="text-right">${r.weight || ''}</td>
                <td class="text-right">${r.rate || ''}</td>
                <td class="text-right">${r.amount || ''}</td>
            </tr>
        `).join('');

        const htmlContent = `
            <html>
                <head>
                    <meta charset="UTF-8">
                    <title>Total Purchase Report</title>
                    <style>
                        @page { size: A4 portrait; margin: 5mm; }
                        body { font-family: 'Noto Sans Devanagari', sans-serif; margin: 0 auto; padding: 20px; max-width: 300mm; }
                        .center { text-align: center; }
                        .title { font-size: 22px; font-weight: bold; margin-top:3mm; }
                        .subtitle { font-size: 16px; margin-top: 6px; margin-bottom: 12px; }
                        table { width: 100%; font-size: 15px; border-collapse: collapse; }
                        th, td { padding: 6px 8px; border: 1px solid #ddd; vertical-align: top; }
                        th { background-color: rgba(223, 223, 223, 0.93);  }
                        .text-right { text-align: right; }
                        .bold { font-weight: bold; }
                        tfoot td { font-weight: bold; text-align: right;  }
                    </style>
                </head>
                <body>
                    <div class="center title">${userdetail?.departmentname || 'दी जय मातादी फ्रूट कंपनी'}</div>
                    <div class="center title">Total Purchase Report</div>
                    <div class="center subtitle">Duration: ${formatDateToShortMonth(startDate)} <b>To</b> ${formatDateToShortMonth(endDate)} </div>

                    <table>
                        <thead>
                            <tr>
                                <th style="min-width: 90px;">Date</th>                           
                                <th style="min-width: 200px;">Farmer Name</th>
                                <th style="min-width: 80px;">Vehicle No</th>                                
                                <th style="min-width: 80px;">Crop</th>                           
                                <th style="min-width: 70px;">Grade</th>
                                <th style="min-width: 70px;">Inward Crate</th>
                                <th style="min-width: 70px;">Weight</th>
                                <th style="min-width: 70px;">Rate</th>
                                <th style="min-width: 70px;">Amount</th>
                            </tr>
                        </thead>
                        <tbody> 
                            ${htmlRows}
                        </tbody>
                       <tfoot style="border: 1px solid black">
                       <tr>
                        <td colspan="5" class="text-right-bold">Total:</td>
                        <td class="text-right bold">${totalInvartcaret.toLocaleString('hi-IN')}</td>
                        <td class="text-right bold">${totalWeight.toLocaleString('hi-IN')}</td>
                        <td class="text-right bold">${totalRate.toLocaleString('hi-IN')}</td>
                        <td class="text-right bold">${totalAmount.toLocaleString('hi-IN')}</td>
                      </tr>
                      </tfoot>

                    </table>
                <div class="btn-container" style="text-align:center;margin:20px 0;">
                    <button onclick="window.print()" style="padding:10px 28px;background:#3b82f6;color:#fff;border:none;border-radius:5px;cursor:pointer;font-size:14px;font-weight:600;margin:0 8px;">Print</button>
                    <button onclick="downloadPDF('TotalPurchase')" style="padding:10px 28px;background:#198754;color:#fff;border:none;border-radius:5px;cursor:pointer;font-size:14px;font-weight:600;margin:0 8px;">Download PDF</button>
                    <button onclick="window.close()" style="padding:10px 28px;background:#ef4444;color:#fff;border:none;border-radius:5px;cursor:pointer;font-size:14px;font-weight:600;margin:0 8px;">Close</button>
                </div>
                <script src="https://cdnjs.cloudflare.com/ajax/libs/html2canvas/1.4.1/html2canvas.min.js"></script>
                <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/2.5.1/jspdf.umd.min.js"></script>
                <script>
                function downloadPDF(name) {
                    var btns = document.querySelector('.btn-container'); btns.style.display='none';
                    html2canvas(document.body, {scale:2, useCORS:true, backgroundColor:'#fff'}).then(function(canvas) {
                        var pdf = new jspdf.jsPDF('l','mm','a4');
                        var pw = pdf.internal.pageSize.getWidth()-10, ph = pdf.internal.pageSize.getHeight()-10;
                        var iw = pw, ih = canvas.height*iw/canvas.width;
                        if(ih<=ph){pdf.addImage(canvas.toDataURL('image/png'),'PNG',5,5,iw,ih);}
                        else{var rem=canvas.height,pos=0,sl=Math.floor(canvas.width*ph/iw);
                        while(rem>0){var pc=document.createElement('canvas');pc.width=canvas.width;pc.height=Math.min(sl,rem);
                        pc.getContext('2d').drawImage(canvas,0,pos,canvas.width,pc.height,0,0,canvas.width,pc.height);
                        if(pos>0)pdf.addPage();pdf.addImage(pc.toDataURL('image/png'),'PNG',5,5,iw,pc.height*iw/canvas.width);
                        pos+=sl;rem-=sl;}}
                        pdf.save(name+'_Report.pdf'); btns.style.display='';
                    }).catch(function(){btns.style.display='';});
                }
                </script>
                </body>
            </html>
        `;

        const blob = new Blob([htmlContent], { type: 'text/html' });
        const url = URL.createObjectURL(blob);
        window.open(url, '_blank');
    };

    const generateSalesReport = (data) => {
        // Use original API data directly
        const rows = data;
        const total_totcaretcount = rows.reduce((sum, r) => sum + Number(r.crtcnt), 0);
        const total_totsamt = rows.reduce((sum, r) => sum + Number(r.totsamt), 0);
        // const totalWeight = rows.reduce((sum, r) => sum + (r.weight), 0);
        // const totalRate = rows.reduce((sum, r) => sum + (r.rate), 0);
        // const totalAmount = rows.reduce((sum, r) => sum + ((r.weight || 0) * (r.rate || 0)), 0);

        const htmlRows = rows.map((r) => `
            <tr>
                <td class="text-center">${r.sysdt || ''}</td>
                <td class="text-left">${r.customername || ''}</td>
                <td class="text-center">${r.vehchno || ''}</td>               
                <td>${r.cropname || ''}</td>
                <td class="text-right">${r.crtcnt || ''}</td>
                <td class="text-right">${r.totsamt || ''}</td>
               
            </tr>
        `).join('');

        const htmlContent = `
            <html>
                <head>
                    <meta charset="UTF-8">
                    <title>Sales Report</title>
                    <style>
                        @page { size: A4 portrait; margin: 5mm; }
                        body { font-family: 'Noto Sans Devanagari', sans-serif; margin: 0 auto; padding: 20px; max-width: 300mm; }
                        .center { text-align: center; }
                        .title { font-size: 22px; font-weight: bold; margin-top:3mm; }
                        .subtitle { font-size: 16px; margin-top: 6px; margin-bottom: 12px; }
                        table { width: 100%; font-size: 15px; border-collapse: collapse; }
                        th, td { padding: 6px 8px; border: 1px solid #ddd; vertical-align: top; }
                        th { background-color: rgba(223, 223, 223, 0.93);  }
                        .text-right { text-align: right; }
                        .bold { font-weight: bold; }
                        tfoot td { font-weight: bold; text-align: right;  }
                    </style>
                </head>
                <body>
                    <div class="center title">${userdetail?.departmentname || 'दी जय मातादी फ्रूट कंपनी'}</div>
                    <div class="center title">Sales Report</div>
                    <div class="center subtitle">Duration: ${formatDateToShortMonth(startDate)} <b>To</b> ${formatDateToShortMonth(endDate)} </div>

                    <table>
                        <thead>
                            <tr>
                                <th style="min-width: 90px;">Date</th>                           
                                <th style="min-width: 200px;">Customer Name</th>
                                <th style="min-width: 80px;">Vehicle No</th>                                
                                <th style="min-width: 80px;">Crop</th>                           
                                <th style="min-width: 70px;">Crate Count</th>
                                <th style="min-width: 70px;">Sales value</th>
                                
                            </tr>
                        </thead>
                        <tbody> 
                            ${htmlRows}
                        </tbody>
                       <tfoot style="border: 1px solid black">
                            <tr>
                                <td colspan="4" class="text-right-bold">Total:</td>
                                  <td class="text-right bold">${total_totcaretcount.toLocaleString('hi-IN')}</td>  
                                <td class="text-right bold">${total_totsamt.toLocaleString('hi-IN')}</td>                    
                            </tr>
                        </tfoot>

                    </table>
                <div class="btn-container" style="text-align:center;margin:20px 0;">
                    <button onclick="window.print()" style="padding:10px 28px;background:#3b82f6;color:#fff;border:none;border-radius:5px;cursor:pointer;font-size:14px;font-weight:600;margin:0 8px;">Print</button>
                    <button onclick="downloadPDF('SalesReport')" style="padding:10px 28px;background:#198754;color:#fff;border:none;border-radius:5px;cursor:pointer;font-size:14px;font-weight:600;margin:0 8px;">Download PDF</button>
                    <button onclick="window.close()" style="padding:10px 28px;background:#ef4444;color:#fff;border:none;border-radius:5px;cursor:pointer;font-size:14px;font-weight:600;margin:0 8px;">Close</button>
                </div>
                <script src="https://cdnjs.cloudflare.com/ajax/libs/html2canvas/1.4.1/html2canvas.min.js"></script>
                <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/2.5.1/jspdf.umd.min.js"></script>
                <script>
                function downloadPDF(name) {
                    var btns = document.querySelector('.btn-container'); btns.style.display='none';
                    html2canvas(document.body, {scale:2, useCORS:true, backgroundColor:'#fff'}).then(function(canvas) {
                        var pdf = new jspdf.jsPDF('l','mm','a4');
                        var pw = pdf.internal.pageSize.getWidth()-10, ph = pdf.internal.pageSize.getHeight()-10;
                        var iw = pw, ih = canvas.height*iw/canvas.width;
                        if(ih<=ph){pdf.addImage(canvas.toDataURL('image/png'),'PNG',5,5,iw,ih);}
                        else{var rem=canvas.height,pos=0,sl=Math.floor(canvas.width*ph/iw);
                        while(rem>0){var pc=document.createElement('canvas');pc.width=canvas.width;pc.height=Math.min(sl,rem);
                        pc.getContext('2d').drawImage(canvas,0,pos,canvas.width,pc.height,0,0,canvas.width,pc.height);
                        if(pos>0)pdf.addPage();pdf.addImage(pc.toDataURL('image/png'),'PNG',5,5,iw,pc.height*iw/canvas.width);
                        pos+=sl;rem-=sl;}}
                        pdf.save(name+'_Report.pdf'); btns.style.display='';
                    }).catch(function(){btns.style.display='';});
                }
                </script>
                </body>
            </html>
        `;

        const blob = new Blob([htmlContent], { type: 'text/html' });
        const url = URL.createObjectURL(blob);
        window.open(url, '_blank');
    };

    const generateSalesupdatedReport = (data, selectedCustomer1) => {
        // Use original API data directly
        const rows = data;
        const total_totcaretcount1 = rows.reduce((sum, r) => sum + Number(r.caretcount), 0);
        const total_totsamt1 = rows.reduce((sum, r) => sum + Number(r.totsamt), 0);
        // const totalWeight = rows.reduce((sum, r) => sum + (r.weight), 0);
        // const totalRate = rows.reduce((sum, r) => sum + (r.rate), 0);
        // const totalAmount = rows.reduce((sum, r) => sum + ((r.weight || 0) * (r.rate || 0)), 0);

        const htmlRows = rows.map((r) => `
            <tr>
                <td class="text-center">${r.sysdt || ''}</td>
                
                <td class="text-center">${r.vehicleno || ''}</td>               
                <td>${r.itemne || ''}</td>
                <td class="text-right">${r.caretcount || ''}</td>
                <td class="text-right">${r.totsamt || ''}</td>
               
            </tr>
        `).join('');

        const htmlContent = `
            <html>
                <head>
                    <meta charset="UTF-8">
                    <title>Sales Report</title>
                    <style>
                        @page { size: A4 portrait; margin: 5mm; }
                        body { font-family: 'Noto Sans Devanagari', sans-serif; margin: 0 auto; padding: 20px; max-width: 300mm; }
                        .center { text-align: center; }
                        .title { font-size: 22px; font-weight: bold; margin-top:3mm; }
                        .subtitle { font-size: 16px; margin-top: 6px; margin-bottom: 12px; }
                        table { width: 100%; font-size: 15px; border-collapse: collapse; }
                        th, td { padding: 6px 8px; border: 1px solid #ddd; vertical-align: top; }
                        th { background-color: rgba(223, 223, 223, 0.93);  }
                        .text-right { text-align: right; }
                        .bold { font-weight: bold; }
                        tfoot td { font-weight: bold; text-align: right;  }
                    </style>
                </head>
                <body>
                    <div class="center title">${userdetail?.departmentname || 'दी जय मातादी फ्रूट कंपनी'}</div>
                    <div class="center title">Sales Report</div>
                    <div class="center subtitle">Duration: ${formatDateToShortMonth(startDate)} <b>To</b> ${formatDateToShortMonth(endDate)} </div>
                    <div class="text-left" style="margin-bottom: 10px;"><b>Customer Name : </b>${selectedCustomer1}</div>
                    <table>
                        <thead>
                            <tr>
                                <th style="min-width: 90px;">Date</th>                           
                                
                                <th style="min-width: 80px;">Vehicle No</th>                                
                                <th style="min-width: 80px;">Crop</th>                           
                                <th style="min-width: 70px;">Crate Count</th>
                                <th style="min-width: 70px;">Sales value</th>
                                
                            </tr>
                        </thead>
                        <tbody> 
                            ${htmlRows}
                        </tbody>
                       <tfoot style="border: 1px solid black">
                            <tr>
                                <td colspan="3" class="text-right-bold">Total:</td>
                                  <td class="text-right bold">${total_totcaretcount1.toLocaleString('hi-IN')}</td>  
                                <td class="text-right bold">${total_totsamt1.toLocaleString('hi-IN')}</td>
                            </tr>
                        </tfoot>

                    </table>
                <div class="btn-container" style="text-align:center;margin:20px 0;">
                    <button onclick="window.print()" style="padding:10px 28px;background:#3b82f6;color:#fff;border:none;border-radius:5px;cursor:pointer;font-size:14px;font-weight:600;margin:0 8px;">Print</button>
                    <button onclick="downloadPDF('SalesUpdated')" style="padding:10px 28px;background:#198754;color:#fff;border:none;border-radius:5px;cursor:pointer;font-size:14px;font-weight:600;margin:0 8px;">Download PDF</button>
                    <button onclick="window.close()" style="padding:10px 28px;background:#ef4444;color:#fff;border:none;border-radius:5px;cursor:pointer;font-size:14px;font-weight:600;margin:0 8px;">Close</button>
                </div>
                <script src="https://cdnjs.cloudflare.com/ajax/libs/html2canvas/1.4.1/html2canvas.min.js"></script>
                <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/2.5.1/jspdf.umd.min.js"></script>
                <script>
                function downloadPDF(name) {
                    var btns = document.querySelector('.btn-container'); btns.style.display='none';
                    html2canvas(document.body, {scale:2, useCORS:true, backgroundColor:'#fff'}).then(function(canvas) {
                        var pdf = new jspdf.jsPDF('l','mm','a4');
                        var pw = pdf.internal.pageSize.getWidth()-10, ph = pdf.internal.pageSize.getHeight()-10;
                        var iw = pw, ih = canvas.height*iw/canvas.width;
                        if(ih<=ph){pdf.addImage(canvas.toDataURL('image/png'),'PNG',5,5,iw,ih);}
                        else{var rem=canvas.height,pos=0,sl=Math.floor(canvas.width*ph/iw);
                        while(rem>0){var pc=document.createElement('canvas');pc.width=canvas.width;pc.height=Math.min(sl,rem);
                        pc.getContext('2d').drawImage(canvas,0,pos,canvas.width,pc.height,0,0,canvas.width,pc.height);
                        if(pos>0)pdf.addPage();pdf.addImage(pc.toDataURL('image/png'),'PNG',5,5,iw,pc.height*iw/canvas.width);
                        pos+=sl;rem-=sl;}}
                        pdf.save(name+'_Report.pdf'); btns.style.display='';
                    }).catch(function(){btns.style.display='';});
                }
                </script>
                </body>
            </html>
        `;

        const blob = new Blob([htmlContent], { type: 'text/html' });
        const url = URL.createObjectURL(blob);
        window.open(url, '_blank');
    };

    const generateProfitLossReport = (data) => {
        // Use original API data directly
        const rows = data;

        // Calculate totals
        const total_totcaretcount = rows.reduce((sum, r) => sum + Number(r.crtcnt), 0);
        const total_totexpence = rows.reduce((sum, r) => sum + Number(r.totex), 0);
        const total_purchase_amount = rows.reduce((sum, r) => sum + (r.totiwdamt), 0);
        const total_svamount = rows.reduce((sum, r) => sum + (r.totsamt), 0);
        // Calculate total profit dynamically from all rows
        const total_profit = rows.reduce((sum, r) => sum + ((r.totsamt || 0) - ((r.totex || 0) + (r.totiwdamt || 0))), 0);


        const htmlRows = rows.map((r) => `
           
            <tr>
                <td class="text-center">${r.sysdt || ''}</td> 
                <td class="text-left">${r.cropname || ''}</td>
                <td class="text-center">${r.customername || ''}</td>  
                <td class="text-center">${r.vehchno || ''}</td>             
                <td class="text-right">${r.crtcnt || ''}</td>
                <td class="text-right">${r.totex || ''}</td>  
                <td class="text-right">${r.totiwdamt || ''}</td> 
                <td class="text-right">${r.totsamt || ''}</td>  
                <td class="text-right">${Math.round(r.totsamt - (r.totiwdamt + r.totex)) || ''}</td>                
            </tr>
        `).join('');

        const htmlContent = `
            <html>
                <head>
                    <meta charset="UTF-8">
                    <title>Profit Loss Report</title>   
                    <style>
                        @page { size: A4 landscape; margin: 5mm; }
                        body { font-family: 'Noto Sans Devanagari', sans-serif; margin: 0 auto; padding: 20px; max-width: 300mm; }
                        .center { text-align: center; }
                        .title { font-size: 22px; font-weight: bold; margin-top:3mm; }
                        .subtitle { font-size: 16px; margin-top: 6px; margin-bottom: 12px; }
                        table { width: 100%; font-size: 15px; border-collapse: collapse; }
                        th, td { padding: 6px 8px; border: 1px solid #ddd; vertical-align: top; }
                        th { background-color: rgba(223, 223, 223, 0.93);  }
                        .text-right { text-align: right; }
                        .bold { font-weight: bold; }
                        tfoot td { font-weight: bold; text-align: right;  }
                    </style>
                </head>
                <body>
                    <div class="center title">${userdetail?.departmentname || 'दी जय मातादी फ्रूट कंपनी'}</div>
                    <div class="center title">Profit Loss Report</div>
                    <div class="center subtitle">Duration: ${formatDateToShortMonth(startDate)} <b>To</b> ${formatDateToShortMonth(endDate)} </div>

                    <table>
                        <thead>
                            <tr>
                                <th style="min-width: 90px;">Date</th>     
                                <th style="min-width: 80px;">Crop</th>                          
                                <th style="min-width: 200px;">Customer Name</th>
                                <th style="min-width: 80px;">Vehicle No</th>                                
                                <th style="min-width: 70px;">Total Crate</th>                     
                                <th style="min-width: 70px;">Total Expence</th>
                                <th style="min-width: 70px;">Total Purchase Amount</th>
                                <th style="min-width: 70px;">sales value</th>
                                <th style="min-width: 70px;">Profit</th>

                                
                            </tr>
                        </thead>
                        <tbody> 
                            ${htmlRows}
                        </tbody>
                       <tfoot style="border: 1px solid black">
                            <tr>
                                <td colspan="4" class="text-right-bold">Total:</td>
                                <td class="text-right bold">${total_totcaretcount.toLocaleString('hi-IN')}</td>  
                                <td class="text-right bold">${total_totexpence.toLocaleString('hi-IN')}</td> 
                                <td class="text-right bold">${total_purchase_amount.toLocaleString('hi-IN')}</td> 
                                <td class="text-right bold">${total_svamount.toLocaleString('hi-IN')}</td> 
                                <td class="text-right bold">${total_profit.toLocaleString('hi-IN')}</td>

                            </tr>
                        </tfoot>

                    </table>
                <div class="btn-container" style="text-align:center;margin:20px 0;">
                    <button onclick="window.print()" style="padding:10px 28px;background:#3b82f6;color:#fff;border:none;border-radius:5px;cursor:pointer;font-size:14px;font-weight:600;margin:0 8px;">Print</button>
                    <button onclick="downloadPDF('ProfitLoss')" style="padding:10px 28px;background:#198754;color:#fff;border:none;border-radius:5px;cursor:pointer;font-size:14px;font-weight:600;margin:0 8px;">Download PDF</button>
                    <button onclick="window.close()" style="padding:10px 28px;background:#ef4444;color:#fff;border:none;border-radius:5px;cursor:pointer;font-size:14px;font-weight:600;margin:0 8px;">Close</button>
                </div>
                <script src="https://cdnjs.cloudflare.com/ajax/libs/html2canvas/1.4.1/html2canvas.min.js"></script>
                <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/2.5.1/jspdf.umd.min.js"></script>
                <script>
                function downloadPDF(name) {
                    var btns = document.querySelector('.btn-container'); btns.style.display='none';
                    html2canvas(document.body, {scale:2, useCORS:true, backgroundColor:'#fff'}).then(function(canvas) {
                        var pdf = new jspdf.jsPDF('l','mm','a4');
                        var pw = pdf.internal.pageSize.getWidth()-10, ph = pdf.internal.pageSize.getHeight()-10;
                        var iw = pw, ih = canvas.height*iw/canvas.width;
                        if(ih<=ph){pdf.addImage(canvas.toDataURL('image/png'),'PNG',5,5,iw,ih);}
                        else{var rem=canvas.height,pos=0,sl=Math.floor(canvas.width*ph/iw);
                        while(rem>0){var pc=document.createElement('canvas');pc.width=canvas.width;pc.height=Math.min(sl,rem);
                        pc.getContext('2d').drawImage(canvas,0,pos,canvas.width,pc.height,0,0,canvas.width,pc.height);
                        if(pos>0)pdf.addPage();pdf.addImage(pc.toDataURL('image/png'),'PNG',5,5,iw,pc.height*iw/canvas.width);
                        pos+=sl;rem-=sl;}}
                        pdf.save(name+'_Report.pdf'); btns.style.display='';
                    }).catch(function(){btns.style.display='';});
                }
                </script>
                </body>
            </html>
        `;

        const blob = new Blob([htmlContent], { type: 'text/html' });
        const url = URL.createObjectURL(blob);
        window.open(url, '_blank');
    };

    const generateCustomerReport = (data, selectedCustomer1) => {
        const rows = data;
        const fmtNum = (v) => {
            const n = parseFloat(v) || 0;
            return n === 0 ? '' : n.toLocaleString('en-IN', { minimumFractionDigits: 1, maximumFractionDigits: 1 });
        };

        // Build a map: for each refaid, find the payment date (trndt of the payment row)
        const releaseDateMap = {};
        rows.forEach(r => {
            const pmt = parseFloat(r.pamt) || 0;
            if (pmt > 0 && r.refaid) {
                releaseDateMap[r.refaid] = r.trndt || '';
            }
        });

        let totalPurchase = 0;
        let totalPayment = 0;
        let totalPCharges = 0;
        const billShown = {};
        let tableRows = '';

        rows.forEach((r) => {
            const purchase = parseFloat(r.totsamt) || 0;
            const payment = parseFloat(r.pamt) || 0;
            const advance = parseFloat(r.advamt) || 0;
            const pcharges = parseFloat(r.pcharges) || 0;
            const refKey = r.refaid || '';
            const isBillRow = purchase > 0 && !billShown[refKey];

            if (isBillRow) {
                billShown[refKey] = true;
                totalPurchase += purchase;
                totalPCharges += pcharges;
            }
            totalPayment += payment + advance;

            // Show bill row with all detail columns
            if (isBillRow) {
                const releaseDate = releaseDateMap[refKey] || r.releasedate || '';
                tableRows += `
                <tr>
                    <td>${r.trndt || ''}</td>
                    <td>${r.vehicleno || ''}</td>
                    <td class="name">${r.ccompanyname || ''}</td>
                    <td>${r.cropname || ''}</td>
                    <td class="num">${fmtNum(r.wtpersku)}</td>
                    <td class="num">${fmtNum(r.qtycrt)}</td>
                    <td class="num">${fmtNum(r.qtykg)}</td>
                    <td class="num">${fmtNum(purchase)}</td>
                    <td class="num">${fmtNum(payment + advance)}</td>
                    <td>${releaseDate}</td>
                </tr>`;
            } else if (payment > 0 || advance > 0) {
                // Payment/Advance row - show only date, name, payment, release date
                tableRows += `
                <tr>
                    <td>${r.trndt || ''}</td>
                    <td>${r.vehicleno || ''}</td>
                    <td class="name">${r.ccompanyname || ''}</td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td class="num">${fmtNum(payment + advance)}</td>
                    <td>${r.trndt || ''}</td>
                </tr>`;
            }
        });

        const payable = totalPurchase - totalPayment;

        const htmlContent = `
            <html>
                <head>
                    <meta charset="UTF-8">
                    <title>Customer Ledger Report</title>
                    <style>
                        @page { size: A4 landscape; margin: 8mm; }
                        * { box-sizing: border-box; margin: 0; padding: 0; }
                        body {
                            font-family: 'Segoe UI', Arial, sans-serif;
                            padding: 15px;
                            background: #fff;
                            color: #222;
                        }
                        .report-container { width: 100%; max-width: 1400px; margin: 0 auto; }
                        .header {
                            text-align: center;
                            border-bottom: 3px double #333;
                            padding-bottom: 10px;
                            margin-bottom: 12px;
                        }
                        .company-name {
                            font-size: 26px;
                            font-weight: 800;
                            text-transform: uppercase;
                            letter-spacing: 1.5px;
                        }
                        .report-title {
                            font-size: 18px;
                            font-weight: 700;
                            margin-top: 4px;
                            color: #444;
                        }
                        .filter-bar {
                            display: flex;
                            justify-content: space-between;
                            background: #f4f6f8;
                            border: 1px solid #d0d5dd;
                            border-radius: 6px;
                            padding: 8px 16px;
                            margin-bottom: 12px;
                            font-size: 13px;
                        }
                        .filter-bar .label { color: #666; font-weight: 500; }
                        .filter-bar .value { color: #1a1a1a; font-weight: 700; margin-left: 4px; }

                        table { width: 100%; border-collapse: collapse; font-size: 12px; }
                        thead th {
                            background: #2c3e50;
                            color: #fff;
                            font-weight: 700;
                            padding: 8px 6px;
                            border: 1px solid #1a252f;
                            text-align: center;
                            white-space: nowrap;
                            font-size: 11.5px;
                            text-transform: uppercase;
                            letter-spacing: 0.3px;
                        }
                        tbody td {
                            padding: 6px;
                            border: 1px solid #ccc;
                            text-align: center;
                            vertical-align: middle;
                        }
                        tbody tr:nth-child(even) { background: #f9fafb; }
                        tbody tr:hover { background: #eef3f8; }
                        .name { text-align: left; font-weight: 500; }
                        .num { text-align: right; font-family: 'Consolas', 'Courier New', monospace; }

                        .summary-box {
                            margin-top: 16px;
                            border: 2px solid #2c3e50;
                            border-radius: 8px;
                            overflow: hidden;
                            width: 380px;
                            margin-left: auto;
                        }
                        .summary-box .s-title {
                            background: #2c3e50;
                            color: #fff;
                            padding: 8px 16px;
                            font-weight: 700;
                            font-size: 14px;
                            text-align: center;
                        }
                        .summary-box table { margin: 0; font-size: 14px; }
                        .summary-box table td {
                            padding: 8px 16px;
                            border: none;
                            border-bottom: 1px solid #e5e7eb;
                        }
                        .summary-box .s-label { text-align: left; font-weight: 600; color: #333; }
                        .summary-box .s-value { text-align: right; font-weight: 700; font-family: 'Consolas', monospace; font-size: 14px; }
                        .summary-box .s-purchase .s-value { color: #b91c1c; }
                        .summary-box .s-payment .s-value { color: #15803d; }
                        .summary-box .s-payable {
                            background: #eff6ff;
                        }
                        .summary-box .s-payable .s-label { font-size: 15px; color: #1e40af; }
                        .summary-box .s-payable .s-value { font-size: 16px; color: #1e40af; font-weight: 800; }

                        .btn-container { text-align: center; margin: 20px 0; }
                        .btn {
                            padding: 10px 28px;
                            color: #fff;
                            border: none;
                            border-radius: 5px;
                            cursor: pointer;
                            font-size: 14px;
                            font-weight: 600;
                            margin: 0 8px;
                        }
                        .btn-print { background: #3b82f6; }
                        .btn-print:hover { background: #2563eb; }
                        .btn-close { background: #ef4444; }
                        .btn-close:hover { background: #dc2626; }

                        @media print {
                            @page { size: A4 landscape; margin: 5mm; }
                            body { padding: 0; }
                            .btn-container { display: none; }
                            .report-container { max-width: none; }
                            table { font-size: 10.5px; page-break-inside: auto; }
                            tr { page-break-inside: avoid; }
                            thead th { padding: 5px 4px; font-size: 9.5px; }
                            tbody td { padding: 4px; }
                            .company-name { font-size: 22px; }
                            .report-title { font-size: 16px; }
                            .summary-box { page-break-inside: avoid; }
                        }
                    </style>
                </head>
                <body>
                    <div class="report-container">
                        <div class="header">
                            <div class="company-name">${userdetail?.departmentname || 'SHREE SAI FRUITS COMPANY'}</div>
                            <div class="report-title">Customer Ledger Report</div>
                        </div>
                        <div class="filter-bar">
                            <div>
                                <span class="label">Customer:</span><span class="value">${selectedCustomer1 || 'All Customers'}</span>
                            </div>
                            <div>
                                <span class="label">Duration:</span><span class="value">${formatDateToShortMonth(startDate)} To ${formatDateToShortMonth(endDate)}</span>
                            </div>
                            <div>
                                <span class="label">Generated:</span><span class="value">${new Date().toLocaleDateString('en-IN', { day: '2-digit', month: 'short', year: 'numeric' })}</span>
                            </div>
                        </div>

                        <table>
                            <thead>
                                <tr>
                                    <th>Date</th>
                                    <th>Vehicle No</th>
                                    <th>Farmer Name</th>
                                    <th>Variety</th>
                                    <th>Weight</th>
                                    <th>Quantity/Crt</th>
                                    <th>Quantity/Kg</th>
                                    <th>Purchase Amount</th>
                                    <th>Payment</th>
                                    <th>Release Date</th>
                                </tr>
                            </thead>
                            <tbody>
                                ${tableRows}
                            </tbody>
                        </table>

                        <div class="summary-box">
                            <div class="s-title">Summary</div>
                            <table>
                                <tr class="s-purchase">
                                    <td class="s-label">Purchase</td>
                                    <td class="s-value">${totalPurchase.toLocaleString('en-IN', { minimumFractionDigits: 1, maximumFractionDigits: 1 })}</td>
                                </tr>
                                <tr class="s-payment">
                                    <td class="s-label">Payment Release</td>
                                    <td class="s-value">${totalPayment.toLocaleString('en-IN', { minimumFractionDigits: 1, maximumFractionDigits: 1 })}</td>
                                </tr>
                                <tr class="s-payable">
                                    <td class="s-label">Payable</td>
                                    <td class="s-value">${payable.toLocaleString('en-IN', { minimumFractionDigits: 1, maximumFractionDigits: 1 })}</td>
                                </tr>
                            </table>
                        </div>

                        <div class="btn-container">
                            <button class="btn btn-print" onclick="window.print()">Print</button>
                            <button class="btn" style="background:#198754;" onclick="downloadPDF('CustomerLedger')">Download PDF</button>
                            <button class="btn btn-close" onclick="window.close()">Close</button>
                        </div>
                    </div>
                    <script src="https://cdnjs.cloudflare.com/ajax/libs/html2canvas/1.4.1/html2canvas.min.js"></script>
                    <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/2.5.1/jspdf.umd.min.js"></script>
                    <script>
                    function downloadPDF(name) {
                        var btns = document.querySelector('.btn-container'); btns.style.display='none';
                        html2canvas(document.querySelector('.report-container'), {scale:2, useCORS:true, backgroundColor:'#fff'}).then(function(canvas) {
                            var pdf = new jspdf.jsPDF('l','mm','a4');
                            var pw = pdf.internal.pageSize.getWidth()-10, ph = pdf.internal.pageSize.getHeight()-10;
                            var iw = pw, ih = canvas.height*iw/canvas.width;
                            if(ih<=ph){pdf.addImage(canvas.toDataURL('image/png'),'PNG',5,5,iw,ih);}
                            else{var rem=canvas.height,pos=0,sl=Math.floor(canvas.width*ph/iw);
                            while(rem>0){var pc=document.createElement('canvas');pc.width=canvas.width;pc.height=Math.min(sl,rem);
                            pc.getContext('2d').drawImage(canvas,0,pos,canvas.width,pc.height,0,0,canvas.width,pc.height);
                            if(pos>0)pdf.addPage();pdf.addImage(pc.toDataURL('image/png'),'PNG',5,5,iw,pc.height*iw/canvas.width);
                            pos+=sl;rem-=sl;}}
                            pdf.save(name+'_Report.pdf'); btns.style.display='';
                        }).catch(function(){btns.style.display='';});
                    }
                    </script>
                </body>
            </html>
        `;

        const newWindow = window.open('', '_blank');
        if (newWindow) {
            newWindow.document.write(htmlContent);
            newWindow.document.close();
        } else {
            alert('Please allow popups for this site to view the report.');
        }
    };

    const fetchFarmerLedgerReport = async () => {
        if (!startDate) {
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'Please select a start date.',
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
                title: 'Error',
                text: 'Please select an end date.',
                allowOutsideClick: false,
                allowEscapeKey: false,
            }).then(() => {
                endDateRef.current?.focus();
            });
            return;
        }

        const startDateObj = new Date(startDate);
        const endDateObj = new Date(endDate);
        const today = new Date();

        if (startDateObj > endDateObj) {
            MySwal.fire({
                title: "Invalid Date Range",
                text: "Start date cannot be greater than end date.",
                icon: "warning",
                confirmButtonColor: "#ff0000",
                confirmButtonText: "OK",
            });
            return;
        }

        if (endDateObj > today) {
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

            let ledgerData = response.data;

            if (!ledgerData || ledgerData.length === 0) {
                MySwal.fire('No Data', 'No ledger data found for the selected filters.', 'info');
                return;
            }

            // Sort by bill number descending (recent first), then by transaction serial ascending
            ledgerData = ledgerData.sort((a, b) => {
                // First by bill number descending (recent/higher numbers first)
                const billA = (a.billno || a.BILLNO || '').toString();
                const billB = (b.billno || b.BILLNO || '').toString();

                // Try numeric comparison first
                const numBillA = parseFloat(billA);
                const numBillB = parseFloat(billB);

                if (!isNaN(numBillA) && !isNaN(numBillB)) {
                    // Both are numbers - compare numerically (descending)
                    if (numBillB !== numBillA) {
                        return numBillB - numBillA;
                    }
                } else {
                    // String comparison (descending)
                    const billCompare = billB.localeCompare(billA);
                    if (billCompare !== 0) {
                        return billCompare;
                    }
                }

                // Then by transaction serial ascending (within same bill)
                const serialA = parseInt(a.trnsr || a.TRNSR || 0);
                const serialB = parseInt(b.trnsr || b.TRNSR || 0);
                return serialA - serialB;
            });

            // Open report in new tab
            openFarmerLedgerReportInNewTab(ledgerData, {
                farmerName: selectedFarmer?.label || 'All Farmers',
                cropName: selectedCrop?.label || 'All Crops',
                startDate: startDate,
                endDate: endDate
            });

        } catch (error) {
            console.error("Error generating Farmer Ledger report:", error);
            MySwal.fire('Error', 'Failed to generate report: ' + error.message, 'error');
        }
    };

    const handleCardClick = async (card) => {

        if (card.path === 'trial-balance') {
            await fetchTrailBalanceData();
        } else if (card.path === 'totalpurchase') {
            await fetchTotalPurchaseData();
        } else if (card.path === 'sales') {
            await fetchSalesData();
        } else if (card.path === 'sales-updated') {
            if (!startDate) {
                Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    text: 'Please select a start date.',
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
                    title: 'Error',
                    text: 'Please select a end date',
                    allowOutsideClick: false,
                    allowEscapeKey: false,
                }).then(() => {
                    endDateRef.current?.focus();
                });
                return;
            }

            setShowCustomerModal1(true);

            // await fetchSalesupdatedData();
        } else if (card.path === 'profit-loss') {
            await fetchProfitLossData();
        } else if (card.path === 'customer') {

            if (!startDate) {
                Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    text: 'Please select a start date.',
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
                    title: 'Error',
                    text: 'Please select a end date',
                    allowOutsideClick: false,
                    allowEscapeKey: false,
                }).then(() => {
                    endDateRef.current?.focus();
                });
                return;
            }

            setShowCustomerModal(true);


            // await fetchCustomerData();
        } else if (card.path === route.FarmerLedger) {
            await fetchFarmerLedgerReport();
        }
        else if (card.path) {
            navigate(card.path, { state: { autoOpen: card.autoOpen || false } });
        }
    };
    return (
        <div className="page-wrapper pagehead">
            <div className="content">
                <div className="page-header">
                    <div className="page-title">
                        <h3>List of Statements</h3>
                    </div>
                    <div className="page-btn">
                        <Link to="/MasterIndex" className="btn btn-secondary">
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

                    <div className="col-lg-3 col-md-3 col-sm-6 mb-2">
                        <label className="form-label">Select Farmer (Optional)</label>
                        <Select
                            options={farmerOptions}
                            value={selectedFarmer}
                            onChange={(selected) => setSelectedFarmer(selected)}
                            placeholder="All Farmers"
                            isClearable
                            classNamePrefix="react-select"
                            className="react-select-container"
                            menuPortalTarget={document.body}
                            styles={{
                                menuPortal: (base) => ({ ...base, zIndex: 2000 }),
                                menu: (base) => ({ ...base, zIndex: 2000 }),
                                control: (base) => ({
                                    ...base,
                                    minHeight: '38px',
                                }),
                            }}
                        />
                    </div>

                    <div className="col-lg-3 col-md-3 col-sm-6 mb-2">
                        <label className="form-label">Select Crop (Optional)</label>
                        <Select
                            options={cropOptions}
                            value={selectedCrop}
                            onChange={(selected) => setSelectedCrop(selected)}
                            placeholder="All Crops"
                            isClearable
                            classNamePrefix="react-select"
                            className="react-select-container"
                            menuPortalTarget={document.body}
                            styles={{
                                menuPortal: (base) => ({ ...base, zIndex: 2000 }),
                                menu: (base) => ({ ...base, zIndex: 2000 }),
                                control: (base) => ({
                                    ...base,
                                    minHeight: '38px',
                                }),
                            }}
                        />
                    </div>

                    {/* <div className="col-lg-3 col-md-2 col-sm-6 mb-2">
                        <label className="form-label">Select Customer</label>
                        <Select
                            placeholder="Select Customer"
                            classNamePrefix="react-select"
                            options={Customers}
                            name="Customer"
                            // openMenuOnFocus
                            value={
                                Customers.find(
                                    (c) => c.value === formData.Customer
                                ) || null
                            }
                            onChange={handleCustomerChange}
                            ref={customerRef}
                            menuPortalTarget={document.body}
                            styles={{
                                menuPortal: (base) => ({ ...base, zIndex: 2000 }),
                                menu: (base) => ({ ...base, zIndex: 2000 }),
                            }}
                        />
                    </div> */}
                </div>

                <div className='row'>
                    <div className="col-12">
                        <small className="text-muted">
                            <em>* Farmer and Crop filters are optional and apply only to Farmer Ledger report</em>
                        </small>
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


            {showCustomerModal && (
                <div className="modal fade show" style={{
                    display: 'block',
                    position: 'fixed',
                    top: 0,
                    left: 0,
                    width: '100%',
                    height: '100%',
                    backgroundColor: 'rgba(29, 28, 28, 0.3)', // semi-transparent dark overlay
                    backdropFilter: 'blur(1px)', // 🔹 adds blur effect to the background
                    WebkitBackdropFilter: 'blur(6px)', // Safari support
                    zIndex: 1050, // ensures it’s above other elements
                }} tabIndex="-1">
                    <div className="modal-dialog modal-sm modal-dialog-centered">
                        <div className="modal-content" style={{
                            backgroundColor: '#e0e0d7ff', // light gray
                            color: '#333',              // darker text for contrast
                            borderRadius: '8px',        // softer corners (optional)
                            border: 'none',             // remove Bootstrap border (optional)
                        }}>
                            <div className="modal-header">
                                <h5 className="modal-title">Select Customer</h5>
                                <button
                                    type="button"
                                    className="btn-close"
                                    onClick={() => setShowCustomerModal(false)}
                                ></button>
                            </div>

                            <div className="modal-body">
                                <div className="col-lg-12 col-md-12 col-sm-12 mb-2">
                                    <label className="form-label">Select Customer</label>
                                    <Select
                                        placeholder="Select Customer"
                                        classNamePrefix="react-select"
                                        options={Customers}
                                        name="Customer"
                                        value={
                                            Customers.find(
                                                (c) => c.value === formData.Customer
                                            ) || null
                                        }
                                        onChange={handleCustomerChange}
                                        ref={customerRef}
                                        menuPortalTarget={document.body}
                                        styles={{
                                            menuPortal: (base) => ({
                                                ...base,
                                                zIndex: 2000,
                                            }),
                                            menu: (base) => ({
                                                ...base,
                                                zIndex: 2000,
                                            }),
                                        }}
                                    />
                                </div>
                            </div>


                            <div className="col-lg-12 pe-3">
                                <div className="btn-addproduct mb-4">
                                    <button
                                        type="button"
                                        onClick={() => setShowCustomerModal(false)}
                                        className="btn btn-cancel me-2">
                                        Back
                                    </button>
                                    <button className="btn btn-submit"
                                        onClick={async () => {
                                            await fetchCustomerData(formData.Customer); // or pass selected value here
                                            // setShowCustomerModal(false);
                                        }}>
                                        Confirm
                                    </button>
                                </div>
                            </div>


                        </div>
                    </div>
                </div>
            )}

            {showCustomerModal1 && (
                <div className="modal fade show" style={{
                    display: 'block',
                    position: 'fixed',
                    top: 0,
                    left: 0,
                    width: '100%',
                    height: '100%',
                    backgroundColor: 'rgba(29, 28, 28, 0.3)', // semi-transparent dark overlay
                    backdropFilter: 'blur(1px)', // 🔹 adds blur effect to the background
                    WebkitBackdropFilter: 'blur(6px)', // Safari support
                    zIndex: 1050, // ensures it’s above other elements
                }} tabIndex="-1">
                    <div className="modal-dialog modal-sm modal-dialog-centered">
                        <div className="modal-content" style={{
                            backgroundColor: '#e0e0d7ff', // light gray
                            color: '#333',              // darker text for contrast
                            borderRadius: '8px',        // softer corners (optional)
                            border: 'none',             // remove Bootstrap border (optional)
                        }}>
                            <div className="modal-header">
                                <h5 className="modal-title">Select Customer</h5>
                                <button
                                    type="button"
                                    className="btn-close"
                                    onClick={() => setShowCustomerModal1(false)}
                                ></button>
                            </div>

                            <div className="modal-body">
                                <div className="col-lg-12 col-md-12 col-sm-12 mb-2">
                                    <label className="form-label">Select Customer</label>
                                    <Select
                                        placeholder="Select Customer"
                                        classNamePrefix="react-select"
                                        options={Customers}
                                        name="Customer"
                                        value={
                                            Customers.find(
                                                (c) => c.value === formData.SaleCustomer
                                            ) || null
                                        }
                                        onChange={handleCustomerChange1}
                                        ref={customerRef}
                                        menuPortalTarget={document.body}
                                        styles={{
                                            menuPortal: (base) => ({
                                                ...base,
                                                zIndex: 2000,
                                            }),
                                            menu: (base) => ({
                                                ...base,
                                                zIndex: 2000,
                                            }),
                                        }}
                                    />
                                </div>
                            </div>


                            <div className="col-lg-12 pe-3">
                                <div className="btn-addproduct mb-4">
                                    <button
                                        type="button"
                                        onClick={() => setShowCustomerModal1(false)}
                                        className="btn btn-cancel me-2">
                                        Back
                                    </button>
                                    <button className="btn btn-submit"
                                        onClick={async () => {
                                            await fetchSalesupdatedData(formData.SaleCustomer); // or pass selected value here
                                            // setShowCustomerModal(false);
                                        }}>
                                        Confirm
                                    </button>
                                </div>
                            </div>


                        </div>
                    </div>
                </div>
            )}

        </div>
    );
}
export default ReportsInd;

