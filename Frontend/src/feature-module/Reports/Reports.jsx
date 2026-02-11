import React, { useState, useEffect } from "react";
import Breadcrumbs from "../../core/breadcrumbs";
// import Select from 'react-select';
import axios from 'axios';
import ExcelJS from "exceljs";
import { saveAs } from "file-saver";
// import { jsPDF } from 'jspdf';
// import autoTable from 'jspdf-autotable';
// import { defaultTableStyles } from "../style/fonts/pdfStyle";
import { baseUrl } from "../../core/json/custom";
import withReactContent from "sweetalert2-react-content";
import Swal from "sweetalert2";
import { useNavigate, Link } from 'react-router-dom';
import { ArrowLeft } from "feather-icons-react/build/IconComponents";
// import marathiFontBase64 from "../../style/fonts/NotoSansDevanagari";
// import { notoSansRegularBase64 } from "../style/fonts/marathiF";
import { getUserData } from '../../Context/UserData';
import { openVyapariPurchaseReportInNewTab, openPaidReportInNewTab, openReportInNewTab, openVyapariReportInNewTab, openDirectReportInNewTab, openFarmerReportInNewTab, openShetkariReportInNewTab, openVyapariVakkalInNewTab } from "./HTMLReport";
import { all_routes } from "../../Router/all_routes";
const Reports = () => {
    const { userdetail } = getUserData();
    console.log("user", userdetail);
    const route = all_routes;
    const [Auction, setAuction] = useState([]);
    const [Vyapari, setVyapari] = useState([]);
    const [VyapariPurchase, setVyapariPurchase] = useState([]);
    const [Director, setDirector] = useState([]);
    const [serviceData, setServiceData] = useState([]);
    const [vyapariData, setVyapariData] = useState([]);
    const [VyapariVakkal, setVyapariVakkal] = useState([]);
    const [Shetkari, setShetkari] = useState([]);
    const [Pattipaid, setPattipaid] = useState([]);
    const [Farmer, setFarmer] = useState([]);
    const [dates, setDates] = useState("");
    const MySwal = withReactContent(Swal);
    const [formData, setformData] = useState({
        product: "",
        payment: "",
        cate: "",
        Enddate: "",
        Startdate: "",
    });


    const { departmentname } = userdetail;
    // Convert accessPolicy to uppercase for normalization.
    const normalizedAccess =
        typeof departmentname === "string"
            ? departmentname.toUpperCase().split(",")
            : Array.isArray(departmentname)
                ? departmentname.map((a) => a.toUpperCase())
                : [];

    const hasAccess = (requiredAccess) => {
        if (!requiredAccess) return true;
        return normalizedAccess.includes(requiredAccess.toUpperCase());
    };

    const isValidDate = (date) => date && !isNaN(new Date(date).getTime());
    const AuctionReport = async () => {
        const { Startdate, Enddate } = dates;
        if (!isValidDate(Startdate) || !isValidDate(Enddate)) {
            return;
        }
        try {
            const payload =
            {
                "startdate": Startdate,
                "endtdate": Enddate,
                "companyid": "",
                "deptid": ""
            }

            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };
            const response = await axios.post(
                `${baseUrl.Url}/backend/api/GET_Report_Auction`,

                payload,
                { headers }
            );
            if (response.status !== 200)
                throw new Error("Failed to fetch vendor data");
            console.log("Auction Report", response.data)
            setAuction(response.data);
        } catch (error) {
            console.error("Error fetching AuctionReport data:", error);
        }
    };
    useEffect(() => {
        if (dates.Startdate && dates.Enddate) {
            AuctionReport();
        }
    }, [dates]);
    const downloadExcelAuction = async () => {
        const isValidDate = (date) => date && !isNaN(new Date(date).getTime());
        const today = new Date();
        const startDate = new Date(dates.Startdate);
        const endDate = new Date(dates.Enddate);
        if (!isValidDate(dates.Startdate) || !isValidDate(dates.Enddate)) {
            MySwal.fire({
                title: "योग्य तारीख निवडा",
                text: "कृपया सुरुवातीची व शेवटची तारीख योग्य प्रकारे निवडा.",
                icon: "error",
                confirmButtonColor: "#ff0000",
                confirmButtonText: "ठीक आहे",
            });
            return;
        }
        if (startDate > endDate) {
            MySwal.fire({
                title: "चूक तारीख",
                text: "सुरुवातीची तारीख शेवटच्या तारखेपेक्षा जास्त असू शकत नाही.",
                icon: "warning",
                confirmButtonColor: "#ff0000",
                confirmButtonText: "ठीक आहे",
            });
            return;
        }

        if (endDate > today) {
            MySwal.fire({
                title: "चूक तारीख",
                text: "शेवटची तारीख आजच्या तारखेपेक्षा जास्त असू शकत नाही.",
                icon: "warning",
                confirmButtonColor: "#ff0000",
                confirmButtonText: "ठीक आहे",
            });
            return;
        }
        try {
            const workbook = new ExcelJS.Workbook();
            const worksheet = workbook.addWorksheet("लिलाव अहवाल)");


            const headingRow = worksheet.addRow(["Auction (Report)"]);
            headingRow.getCell(1).font = { bold: true, size: 16 };
            headingRow.getCell(1).alignment = { horizontal: "center", vertical: "middle" };


            worksheet.mergeCells("A1:G1");


            const headers = ["टोकन नं.", "शेतकरी", "पिक", "मोबाईल नं.", "जाळी", "गाव", "वाहन नं."];
            const headerRow = worksheet.addRow(headers);

            headerRow.eachCell((cell) => {
                cell.font = { bold: true, color: { argb: "FFFFFF" } };
                cell.fill = { type: "pattern", pattern: "solid", fgColor: { argb: "808080" } };
                cell.alignment = { horizontal: "center", vertical: "middle" };
            });


            const columnWidths = [15, 15, 15, 15, 15, 15, 15, 20, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 20]; // Adjust widths as needed
            columnWidths.forEach((width, index) => {
                worksheet.getColumn(index + 1).width = width;
            });




            Auction.forEach(({ toknno, farmername, croplabel, mobileno,
                caretS_COUNT, village, vehno }) => {
                const row = worksheet.addRow([toknno, farmername, croplabel, mobileno,
                    caretS_COUNT, village, vehno]);
                row.eachCell((cell) => {
                    cell.alignment = { horizontal: "center", vertical: "middle" };
                });

            });


            const buffer = await workbook.xlsx.writeBuffer();
            const data = new Blob([buffer], { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });
            saveAs(data, "Auction(Report).xlsx");

        } catch (error) {
            console.error("Error generating the Excel file:", error);
        }
    };


    //farmer report
    const fetchFarmerReport = async () => {
        const { Startdate, Enddate } = dates;

        if (!isValidDate(Startdate) || !isValidDate(Enddate)) {
            return;
        }

        try {
            const payload = {
                startdate: Startdate,
                endtdate: Enddate,
                companyid: "",
                deptid: ""
            };

            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };

            const response = await axios.post(
                `${baseUrl.Url}/backend/api/GET_Report_farmer`,
                payload,
                { headers }
            );

            if (response.status !== 200) throw new Error("Failed to fetch farmer data");

            setFarmer(response.data);
        } catch (error) {
            console.error("Error fetching FarmerReport data:", error);
        }
    };

    useEffect(() => {
        if (dates.Startdate && dates.Enddate) {
            fetchFarmerReport();
        }
    }, [dates]);

    const handleDateChange = (event) => {
        const { name, value } = event.target;
        setDates((prevDates) => ({
            ...prevDates,
            [name]: value,
        }));
    };




    const downloadExcelfarmer = async () => {
        const today = new Date();
        const startDate = new Date(dates.Startdate);
        const endDate = new Date(dates.Enddate);

        if (!isValidDate(dates.Startdate) || !isValidDate(dates.Enddate)) {
            MySwal.fire({
                title: "योग्य तारीख निवडा",
                text: "कृपया सुरुवातीची व शेवटची तारीख योग्य प्रकारे निवडा.",
                icon: "error",
                confirmButtonColor: "#ff0000",
                confirmButtonText: "ठीक आहे",
            });
            return;
        }

        // Check if start date is after end date
        if (startDate > endDate) {
            MySwal.fire({
                title: "चूक तारीख",
                text: "सुरुवातीची तारीख शेवटच्या तारखेपेक्षा जास्त असू शकत नाही.",
                icon: "warning",
                confirmButtonColor: "#ff0000",
                confirmButtonText: "ठीक आहे",
            });
            return;
        }

        // Check if end date is in the future
        if (endDate > today) {
            MySwal.fire({
                title: "चूक तारीख",
                text: "शेवटची तारीख आजच्या तारखेपेक्षा जास्त असू शकत नाही.",
                icon: "warning",
                confirmButtonColor: "#ff0000",
                confirmButtonText: "ठीक आहे",
            });
            return;
        }

        try {
            const workbook = new ExcelJS.Workbook();
            const worksheet = workbook.addWorksheet("Farmer Report");

            const headingRow = worksheet.addRow(["Farmer (Report)"]);
            headingRow.getCell(1).font = { bold: true, size: 16 };
            headingRow.getCell(1).alignment = { horizontal: "center", vertical: "middle" };
            worksheet.mergeCells("A1:N1");

            // const headers = [
            //     "Farmer", "Vyapari", "Bill No", "Tarik", "Rate",
            //     "Jali", "Weight", "WeightMinMax", "Amount",
            //     "Total Carets", "Total Weight", "Total Amount", "Total Cost", "Remaining Amount"
            // ];
            const headers = [
                "शेतकरी", "बिल नं.", "तारीख", "दर", "जाळी", "वजन", "रक्कम", "केरेट्स", "एकूण वजन", "एकूण रक्कम", "खर्च", "शिल्लक रक्कम", "व्यापारी"
            ];
            const headerRow = worksheet.addRow(headers);

            headerRow.eachCell(cell => {
                cell.font = { bold: true, color: { argb: "FFFFFF" } };
                cell.fill = { type: "pattern", pattern: "solid", fgColor: { argb: "808080" } };
                cell.alignment = { horizontal: "center", vertical: "middle" };
            });

            headers.forEach((_, index) => {
                worksheet.getColumn(index + 1).width = 18;
            });

            Farmer.forEach(item => {
                const row = worksheet.addRow([
                    item.farmername, item.vyapariname, item.billno, item.date, item.rate,
                    item.jali, item.weight, item.weightminmax, item.amount,
                    item.totalcarets, item.totalweight, item.totalamount, item.totalcost, item.remainingamount
                ]);
                row.eachCell(cell => {
                    cell.alignment = { horizontal: "center", vertical: "middle" };
                });
            });

            const buffer = await workbook.xlsx.writeBuffer();
            const data = new Blob([buffer], {
                type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            });
            saveAs(data, "Farmer (Report).xlsx");

        } catch (error) {
            console.error("Error generating Excel file:", error);
        }
    };



    const handleReportClick = async () => {
        const { Startdate, Enddate } = dates;

        const today = new Date();
        const startDate = new Date(Startdate);
        const endDate = new Date(Enddate);

        // 🔐 Date validation checks
        if (!isValidDate(Startdate) || !isValidDate(Enddate)) {
            MySwal.fire({
                title: "योग्य तारीख निवडा",
                text: "कृपया सुरुवातीची व शेवटची तारीख योग्य प्रकारे निवडा.",
                icon: "error",
                confirmButtonColor: "#ff0000",
                confirmButtonText: "ठीक आहे",
            });
            return;
        }

        if (startDate > endDate) {
            MySwal.fire({
                title: "चूक तारीख",
                text: "सुरुवातीची तारीख शेवटच्या तारखेपेक्षा जास्त असू शकत नाही.",
                icon: "warning",
                confirmButtonColor: "#ff0000",
                confirmButtonText: "ठीक आहे",
            });
            return;
        }

        if (endDate > today) {
            MySwal.fire({
                title: "चूक तारीख",
                text: "शेवटची तारीख आजच्या तारखेपेक्षा जास्त असू शकत नाही.",
                icon: "warning",
                confirmButtonColor: "#ff0000",
                confirmButtonText: "ठीक आहे",
            });
            return;
        }

        try {
            const payload = {
                startdate: Startdate,
                endtdate: Enddate,
                companyid: userdetail?.companyid || userdetail?.companyID || "",
                deptid: userdetail?.departmentid || userdetail?.departmentID || "",
            };

            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };

            const response = await axios.post(
                `${baseUrl.Url}/backend/api/GET_Report_Auction`,
                payload,
                { headers }
            );

            if (response.status === 200) {
                const data = response.data || [];
                setAuction(data);
                openReportInNewTab(data, dates, departmentname);
            } else {
                MySwal.fire({
                    title: "डेटा मिळाला नाही",
                    icon: "error",
                });
            }
        } catch (error) {
            console.error("Error fetching AuctionReport:", error);
            MySwal.fire({
                title: "त्रुटी आली आहे",
                text: "डेटा मिळवताना काहीतरी चुकलं",
                icon: "error",
            });
        }
    };





    const VyapariReport = async () => {
        const { Startdate, Enddate } = dates;

        if (!isValidDate(Startdate) || !isValidDate(Enddate)) {
            return;
        }

        try {
            const payload = {
                startdate: Startdate,
                endtdate: Enddate,
                companyid: "",
                deptid: ""
            };

            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };


            const response = await axios.post(
                `${baseUrl.Url}/backend/api/GET_Report_Vyapari`,
                payload,
                { headers }
            );

            if (response.status !== 200) throw new Error("Failed to fetch farmer data");

            setVyapari(response.data);
        } catch (error) {
            console.error("Error fetching FarmerReport data:", error);
        }
    };

    useEffect(() => {
        if (dates.Startdate && dates.Enddate) {
            VyapariReport();
        }
    }, [dates]);



    const downloadExcelVyapari = async () => {
        const today = new Date();
        const startDate = new Date(dates.Startdate);
        const endDate = new Date(dates.Enddate);

        if (!isValidDate(dates.Startdate) || !isValidDate(dates.Enddate)) {
            MySwal.fire({
                title: "योग्य तारीख निवडा",
                text: "कृपया सुरुवातीची व शेवटची तारीख योग्य प्रकारे निवडा.",
                icon: "error",
                confirmButtonColor: "#ff0000",
                confirmButtonText: "ठीक आहे",
            });
            return;
        }

        // Check if start date is after end date
        if (startDate > endDate) {
            MySwal.fire({
                title: "चूक तारीख",
                text: "सुरुवातीची तारीख शेवटच्या तारखेपेक्षा जास्त असू शकत नाही.",
                icon: "warning",
                confirmButtonColor: "#ff0000",
                confirmButtonText: "ठीक आहे",
            });
            return;
        }

        // Check if end date is in the future
        if (endDate > today) {
            MySwal.fire({
                title: "चूक तारीख",
                text: "शेवटची तारीख आजच्या तारखेपेक्षा जास्त असू शकत नाही.",
                icon: "warning",
                confirmButtonColor: "#ff0000",
                confirmButtonText: "ठीक आहे",
            });
            return;
        }

        try {
            const workbook = new ExcelJS.Workbook();
            const worksheet = workbook.addWorksheet("व्यापारी Report");

            const headingRow = worksheet.addRow(["व्यापारी (Report)"]);
            headingRow.getCell(1).font = { bold: true, size: 16 };
            headingRow.getCell(1).alignment = { horizontal: "center", vertical: "middle" };
            worksheet.mergeCells("A1:K1");

            const headers = ["तारीख", "पिक", "जाळी", "प्रमाण", "मालाची किंमत", "बाजार शुल्क", "सु.फी", "एकूण शेतकरी", "दिवसभरातील जास्तीत जास्त दर", "दिवसभरातील किमान दर"
            ];
            const headerRow = worksheet.addRow(headers);

            headerRow.eachCell(cell => {
                cell.font = { bold: true, color: { argb: "FFFFFF" } };
                cell.fill = { type: "pattern", pattern: "solid", fgColor: { argb: "808080" } };
                cell.alignment = { horizontal: "center", vertical: "middle" };
            });

            headers.forEach((_, index) => {
                worksheet.getColumn(index + 1).width = 18;
            });

            Director.forEach(item => {
                const row = worksheet.addRow([
                    item.fname,
                    item.vname,
                    item.billno,
                    item.date,
                    item.croplabel,
                    item.totaljaali,
                    item.totalweight,
                    item.rate,
                    item.totalamount,
                    item.totalloss,
                    item.remainingamount,
                ]);
                row.eachCell(cell => {
                    cell.alignment = { horizontal: "center", vertical: "middle" };
                });
            });

            const buffer = await workbook.xlsx.writeBuffer();
            const data = new Blob([buffer], {
                type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            });
            saveAs(data, "व्यापारी (Report).xlsx");

        } catch (error) {
            console.error("Error generating Excel file:", error);
        }
    };



    useEffect(() => {
        if (dates.Startdate && dates.Enddate) {
            VyapariReport();
        }
    }, [dates]);





    const DirectorReport = async () => {
        const { Startdate, Enddate } = dates;

        if (!isValidDate(Startdate) || !isValidDate(Enddate)) {
            return;
        }

        try {
            const payload = {
                startdate: Startdate,
                endtdate: Enddate,
                companyid: "",
                deptid: ""
            };

            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };


            const response = await axios.post(
                `${baseUrl.Url}/backend/api/GET_Report_DayAuction`,
                payload,
                { headers }
            );

            if (response.status !== 200) throw new Error("Failed to fetch farmer data");

            setDirector(response.data);
        } catch (error) {
            console.error("Error fetching FarmerReport data:", error);
        }
    };

    useEffect(() => {
        if (dates.Startdate && dates.Enddate) {
            DirectorReport();
        }
    }, [dates]);

    const DirectorExcel = async () => {
        const today = new Date();
        const startDate = new Date(dates.Startdate);
        const endDate = new Date(dates.Enddate);

        if (!isValidDate(dates.Startdate) || !isValidDate(dates.Enddate)) {
            MySwal.fire({
                title: "योग्य तारीख निवडा",
                text: "कृपया सुरुवातीची व शेवटची तारीख योग्य प्रकारे निवडा.",
                icon: "error",
                confirmButtonColor: "#ff0000",
                confirmButtonText: "ठीक आहे",
            });
            return;
        }

        // Check if start date is after end date
        if (startDate > endDate) {
            MySwal.fire({
                title: "चूक तारीख",
                text: "सुरुवातीची तारीख शेवटच्या तारखेपेक्षा जास्त असू शकत नाही.",
                icon: "warning",
                confirmButtonColor: "#ff0000",
                confirmButtonText: "ठीक आहे",
            });
            return;
        }

        // Check if end date is in the future
        if (endDate > today) {
            MySwal.fire({
                title: "चूक तारीख",
                text: "शेवटची तारीख आजच्या तारखेपेक्षा जास्त असू शकत नाही.",
                icon: "warning",
                confirmButtonColor: "#ff0000",
                confirmButtonText: "ठीक आहे",
            });
            return;
        }

        try {
            const workbook = new ExcelJS.Workbook();
            const worksheet = workbook.addWorksheet("व्यापारी Report");

            const headingRow = worksheet.addRow(["व्यापारी (Report)"]);
            headingRow.getCell(1).font = { bold: true, size: 16 };
            headingRow.getCell(1).alignment = { horizontal: "center", vertical: "middle" };
            worksheet.mergeCells("A1:K1");

            const headers = ["तारीख", "पिक", "जाळी", "प्रमाण", "मालाची किंमत", "बाजार शुल्क", "सु.फी", "एकूण शेतकरी", "दिवसभरातील जास्तीत जास्त दर", "दिवसभरातील किमान दर"
            ];

            const headerRow = worksheet.addRow(headers);

            headerRow.eachCell(cell => {
                cell.font = { bold: true, color: { argb: "FFFFFF" } };
                cell.fill = { type: "pattern", pattern: "solid", fgColor: { argb: "808080" } };
                cell.alignment = { horizontal: "center", vertical: "middle" };
            });

            headers.forEach((_, index) => {
                worksheet.getColumn(index + 1).width = 18;
            });

            Director.forEach(item => {
                const row = worksheet.addRow([
                    item.date,
                    item.cropLabels,
                    item.totaL_JALI,
                    item.totaL_QUINTAL,
                    item.totaL_RATE,
                    item.markeT_FEE,
                    item.supeR_FEE,
                    item.totaL_FARMERS,
                    item.maX_RATE,
                    item.miN_RATE
                ]);
                row.eachCell(cell => {
                    cell.alignment = { horizontal: "center", vertical: "middle" };
                });
            });

            const buffer = await workbook.xlsx.writeBuffer();
            const data = new Blob([buffer], {
                type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            });
            saveAs(data, "director(Report).xlsx");

        } catch (error) {
            console.error("Error generating Excel file:", error);
        }
    };



    const fetchPaidReport = async () => {
        const { Startdate, Enddate } = dates;

        if (!isValidDate(Startdate) || !isValidDate(Enddate)) {
            return;
        }

        try {
            const payload = {
                startdate: Startdate,
                endtdate: Enddate,
                companyid: userdetail?.companyID || "",
                deptid: userdetail?.departmentID || "",
            };

            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };

            const response = await axios.post(
                `${baseUrl.Url}/backend/api/GET_PAIDReport`,
                payload,
                { headers }
            );

            if (response.status !== 200) throw new Error("Failed to fetch PattiPaidReport data");

            setPattipaid(response.data);
            console.log("PaidReport", response.data)
        } catch (error) {
            console.error("Error fetching PattiPaidReport data:", error);
        }
    };

    useEffect(() => {
        if (dates.Startdate && dates.Enddate, departmentname) {
            fetchPaidReport();
        }
    }, [dates]);




    const downloadExcelPaidReport = async () => {
        const today = new Date();
        const startDate = new Date(dates.Startdate);
        const endDate = new Date(dates.Enddate);

        if (!isValidDate(dates.Startdate) || !isValidDate(dates.Enddate)) {
            MySwal.fire({
                title: "योग्य तारीख निवडा",
                text: "कृपया सुरुवातीची व शेवटची तारीख योग्य प्रकारे निवडा.",
                icon: "error",
                confirmButtonColor: "#ff0000",
                confirmButtonText: "ठीक आहे",
            });
            return;
        }

        // Check if start date is after end date
        if (startDate > endDate) {
            MySwal.fire({
                title: "चूक तारीख",
                text: "सुरुवातीची तारीख शेवटच्या तारखेपेक्षा जास्त असू शकत नाही.",
                icon: "warning",
                confirmButtonColor: "#ff0000",
                confirmButtonText: "ठीक आहे",
            });
            return;
        }

        // Check if end date is in the future
        if (endDate > today) {
            MySwal.fire({
                title: "चूक तारीख",
                text: "शेवटची तारीख आजच्या तारखेपेक्षा जास्त असू शकत नाही.",
                icon: "warning",
                confirmButtonColor: "#ff0000",
                confirmButtonText: "ठीक आहे",
            });
            return;
        }

        try {
            const workbook = new ExcelJS.Workbook();
            const worksheet = workbook.addWorksheet("PattiPaidReport");

            const headingRow = worksheet.addRow(["व्यापारी (Report)"]);
            headingRow.getCell(1).font = { bold: true, size: 16 };
            headingRow.getCell(1).alignment = { horizontal: "center", vertical: "middle" };
            worksheet.mergeCells("A1:K1");

            const headers = ["तारीख", "बिल क्रमांक", "टोकन क्रमांक", "शेतकरी नाव", "गाव", "वाहन नाव", "एकूण खर्च", "रक्कम", "जमा रक्कम", "चेक रक्कम", "ऑनलाईन रक्कम", "एकूण जमा", "बाकी रक्कम"];
            const headerRow = worksheet.addRow(headers);

            headerRow.eachCell(cell => {
                cell.font = { bold: true, color: { argb: "FFFFFF" } };
                cell.fill = { type: "pattern", pattern: "solid", fgColor: { argb: "808080" } };
                cell.alignment = { horizontal: "center", vertical: "middle" };
            });

            headers.forEach((_, index) => {
                worksheet.getColumn(index + 1).width = 18;
            });

            Pattipaid.forEach(item => {
                const row = worksheet.addRow([
                    item.date,
                    item.billno,
                    item.btokanno,
                    item.fname,
                    item.villagename,
                    item.vehicleno,
                    item.totalcost,
                    item.remainingamount,
                    item.cashamount,
                    item.chequeamount,
                    item.onlineamount,
                    item.total,
                    item.bakirakkam,

                ]);
                row.eachCell(cell => {
                    cell.alignment = { horizontal: "center", vertical: "middle" };
                });
            });

            const buffer = await workbook.xlsx.writeBuffer();
            const data = new Blob([buffer], {
                type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            });
            saveAs(data, "व्यापारी (Report).xlsx");

        } catch (error) {
            console.error("Error generating Excel file:", error);
        }
    };



    const fetchVyapariPurchaseReport = async () => {
        try {
            const payload1 = {
                startdate: dates.Startdate,
                endtdate: dates.Enddate,
                companyid: userdetail?.companyID || "",
                deptid: userdetail?.departmentID || "",
            };

            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };

            const response1 = await axios.post(
                `${baseUrl.Url}/backend/api/GetVyapriDetailsReport`,
                payload1,
                { headers }
            );

            if (response1.status !== 200) throw new Error("Failed to fetch farmer data");

            const vyapariData = response1.data;
            setVyapariPurchase(vyapariData);

            // 👉 For each vbkid, fetch service data
            let allServiceData = [];

            for (const vyapari of vyapariData) {
                const vbkid = vyapari.vbkid || "%";

                const payload2 = {
                    vbkid,
                    startdate: dates.Startdate,
                    endtdate: dates.Enddate,
                    companyid: userdetail?.companyID || "",
                    deptid: userdetail?.departmentID || "",
                };

                const response2 = await axios.post(
                    `${baseUrl.Url}/backend/api/GET_VyapariPurchaseBillR`,
                    payload2,
                    { headers }
                );

                if (response2.status === 200 && Array.isArray(response2.data)) {
                    const filteredServiceData = response2.data.map(item => ({
                        nSERVICETYPETITLE: item.nSERVICETYPETITLE,
                        sevaValue: item.sevaValue,
                        vbkid: vyapari.vbkid, // 🔗 Attach vbkid
                    }));

                    allServiceData = [...allServiceData, ...filteredServiceData];
                }
            }

            setServiceData(allServiceData);

        } catch (error) {
            console.error("Error fetching data:", error);
        }
    };

    useEffect(() => {
        if (dates.Startdate && dates.Enddate, departmentname) {
            fetchVyapariPurchaseReport();
        }
    }, [dates]);

    const downloadExcelVyapariPurchase = async () => {
        const isValidDate = (date) => date && !isNaN(new Date(date).getTime());

        const today = new Date();
        const startDate = new Date(dates.Startdate);
        const endDate = new Date(dates.Enddate);

        if (!isValidDate(dates.Startdate) || !isValidDate(dates.Enddate)) {
            MySwal.fire({
                title: "योग्य तारीख निवडा",
                text: "कृपया सुरुवातीची व शेवटची तारीख योग्य प्रकारे निवडा.",
                icon: "error",
                confirmButtonColor: "#ff0000",
                confirmButtonText: "ठीक आहे",
            });
            return;
        }

        if (startDate > endDate) {
            MySwal.fire({
                title: "चूक तारीख",
                text: "सुरुवातीची तारीख शेवटच्या तारखेपेक्षा जास्त असू शकत नाही.",
                icon: "warning",
                confirmButtonColor: "#ff0000",
                confirmButtonText: "ठीक आहे",
            });
            return;
        }

        if (endDate > today) {
            MySwal.fire({
                title: "चूक तारीख",
                text: "शेवटची तारीख आजच्या तारखेपेक्षा जास्त असू शकत नाही.",
                icon: "warning",
                confirmButtonColor: "#ff0000",
                confirmButtonText: "ठीक आहे",
            });
            return;
        }

        try {
            const workbook = new ExcelJS.Workbook();
            const worksheet = workbook.addWorksheet("व्यापारी खरेदी अहवाल");

            // Title row
            const titleRow = worksheet.addRow(["व्यापारी खरेदी अहवाल"]);
            titleRow.font = { bold: true, size: 16 };
            titleRow.alignment = { horizontal: "center", vertical: "middle" };

            const purchaseColumns = ["तारीख", "बिल क्रमांक", "व्यापारी नाव", "डाग", "वजन", "रक्कम"];
            const distinctServiceTitles = [...new Set(serviceData.map(item => item.nSERVICETYPETITLE))];
            const mergedColumns = [...purchaseColumns, ...distinctServiceTitles, "एकूण", "नेटबिल"];

            worksheet.mergeCells(1, 1, 1, mergedColumns.length);

            // Header row
            const headerRow = worksheet.addRow(mergedColumns);
            headerRow.eachCell((cell) => {
                cell.font = { bold: true, color: { argb: "FFFFFF" } };
                cell.fill = { type: "pattern", pattern: "solid", fgColor: { argb: "808080" } };
                cell.alignment = { horizontal: "center", vertical: "middle" };
            });

            // Set column widths
            mergedColumns.forEach((_, i) => {
                worksheet.getColumn(i + 1).width = 15;
            });

            // Prepare rows
            const dataRows = VyapariPurchase.map(vyapari => {
                const purchaseRow = [
                    vyapari.date || "",
                    vyapari.billno || "",
                    vyapari.vyapariname || "",
                    vyapari.totaljaali || "",
                    vyapari.totalweight || "",
                    vyapari.totalamount || "",
                ];

                const serviceRow = distinctServiceTitles.map(title => {
                    const match = serviceData.find(s => s.vbkid === vyapari.vbkid && s.nSERVICETYPETITLE === title);
                    return match?.sevaValue?.toString() || "0";
                });

                const numericValues = [
                    ...[purchaseRow[3], purchaseRow[4], purchaseRow[5]].map(v => parseFloat(v) || 0),
                    ...serviceRow.map(v => parseFloat(v) || 0)
                ];
                const totalValue = numericValues.reduce((a, b) => a + b, 0);
                const serviceTotal = serviceRow.map(v => parseFloat(v) || 0).reduce((a, b) => a + b, 0);
                const netBill = totalValue - serviceTotal;

                return [...purchaseRow, ...serviceRow, totalValue.toFixed(2), netBill.toFixed(2)];
            });

            // Add data rows
            dataRows.forEach(rowData => {
                const row = worksheet.addRow(rowData);
                row.eachCell((cell) => {
                    cell.alignment = { horizontal: "center", vertical: "middle" };
                });
            });

            // Add Total row
            const totalRowData = [];

            for (let col = 0; col < mergedColumns.length; col++) {
                if (col === 0) {
                    totalRowData.push("Total");
                } else if (col === 1 || col === 2) {
                    // No total for "बिल क्रमांक" and "व्यापारी नाव"
                    totalRowData.push("");
                } else {
                    const sum = dataRows.reduce((acc, curr) => {
                        const val = parseFloat(curr[col]) || 0;
                        return acc + val;
                    }, 0);
                    totalRowData.push(sum.toFixed(2));
                }
            }

            const totalRow = worksheet.addRow(totalRowData);
            totalRow.eachCell(cell => {
                cell.font = { bold: true };
                cell.fill = { type: "pattern", pattern: "solid", fgColor: { argb: "D3D3D3" } };
                cell.alignment = { horizontal: "center", vertical: "middle" };
            });

            // Generate and download file
            const buffer = await workbook.xlsx.writeBuffer();
            const blob = new Blob([buffer], { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });
            saveAs(blob, "Vyapari_Purchase_Report.xlsx");

        } catch (error) {
            console.error("Excel तयार करताना त्रुटी:", error);
        }
    };



    const handleVyapariPurchaseReportClick = async () => {
        const { Startdate, Enddate } = dates;

        if (!isValidDate(Startdate) || !isValidDate(Enddate)) {
            MySwal.fire({
                title: "योग्य तारीख निवडा",
                icon: "warning",
            });
            return;
        }

        try {
            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };

            const payload1 = {
                startdate: Startdate,
                endtdate: Enddate,
                companyid: userdetail?.companyid || userdetail?.companyID || "",
                deptid: userdetail?.departmentid || userdetail?.departmentID || "",
            };

            // 🔹 Get main व्यापारी data
            const response1 = await axios.post(
                `${baseUrl.Url}/backend/api/GetVyapriDetailsReport`,
                payload1,
                { headers }
            );

            if (response1.status !== 200) {
                throw new Error("Failed to fetch व्यापारी data");
            }

            const vyapariData = response1.data || [];
            setVyapariPurchase(vyapariData); // optional, UI ला हवे असेल तर

            // 🔸 Get service data for each vbkid
            let allServiceData = [];

            for (const vyapari of vyapariData) {
                const vbkid = vyapari.vbkid || "%";

                const payload2 = {
                    vbkid,
                    startdate: Startdate,
                    endtdate: Enddate,
                    companyid: userdetail?.companyid || userdetail?.companyID || "",
                    deptid: userdetail?.departmentid || userdetail?.departmentID || "",
                };

                const response2 = await axios.post(
                    `${baseUrl.Url}/backend/api/GET_VyapariPurchaseBillR`,
                    payload2,
                    { headers }
                );

                if (response2.status === 200 && Array.isArray(response2.data)) {
                    const serviceList = response2.data.map(item => ({
                        nSERVICETYPETITLE: item.nSERVICETYPETITLE,
                        sevaValue: item.sevaValue,
                        vbkid: vbkid,
                    }));
                    allServiceData.push(...serviceList);
                }
            }

            setServiceData(allServiceData); // optional, जर UI मध्ये लागणार असेल

            // 🧾 Open report in new tab
            openVyapariPurchaseReportInNewTab(vyapariData, dates, allServiceData, departmentname);

        } catch (error) {
            console.error("Error fetching व्यापारी अहवाल:", error);
            MySwal.fire({
                title: "त्रुटी आली आहे",
                text: "डेटा मिळवताना काहीतरी चुकलं",
                icon: "error",
            });
        }
    };


    const handlePattiPaidReportClick = async () => {
        const { Startdate, Enddate } = dates;

        if (!isValidDate(Startdate) || !isValidDate(Enddate)) {
            MySwal.fire({
                title: "योग्य तारीख निवडा",
                text: "कृपया योग्य सुरुवात व शेवटची तारीख निवडा.",
                icon: "warning",
            });
            return;
        }

        try {
            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };

            const payload = {
                startdate: Startdate,
                endtdate: Enddate,
                companyid: userdetail?.companyid || userdetail?.companyID || "",
                deptid: userdetail?.departmentid || userdetail?.departmentID || "",
            };

            const response = await axios.post(
                `${baseUrl.Url}/backend/api/GET_PAIDReport`,
                payload,
                { headers }
            );

            if (response.status !== 200) throw new Error("Failed to fetch PattiPaidReport data");

            setPattipaid(response.data);
            console.log("PaidReport", response.data)
            // HTML रिपोर्ट नवीन टॅबमध्ये उघडणे
            openPaidReportInNewTab(response.data, dates, departmentname);

        } catch (error) {
            console.error("Error fetching PattiPaidReport:", error);
            MySwal.fire({
                title: "त्रुटी आली आहे",
                text: "पटी रिपोर्ट मिळवताना काहीतरी चुकलं.",
                icon: "error",
            });
        }
    };



    const handleVyapariReportClick = async () => {
        const { Startdate, Enddate } = dates;

        const today = new Date();
        const startDate = new Date(dates.Startdate);
        const endDate = new Date(dates.Enddate);

        if (!isValidDate(dates.Startdate) || !isValidDate(dates.Enddate)) {
            MySwal.fire({
                title: "योग्य तारीख निवडा",
                text: "कृपया सुरुवातीची व शेवटची तारीख योग्य प्रकारे निवडा.",
                icon: "error",
                confirmButtonColor: "#ff0000",
                confirmButtonText: "ठीक आहे",
            });
            return;
        }

        if (startDate > endDate) {
            MySwal.fire({
                title: "चूक तारीख",
                text: "सुरुवातीची तारीख शेवटच्या तारखेपेक्षा जास्त असू शकत नाही.",
                icon: "warning",
                confirmButtonColor: "#ff0000",
                confirmButtonText: "ठीक आहे",
            });
            return;
        }

        if (endDate > today) {
            MySwal.fire({
                title: "चूक तारीख",
                text: "शेवटची तारीख आजच्या तारखेपेक्षा जास्त असू शकत नाही.",
                icon: "warning",
                confirmButtonColor: "#ff0000",
                confirmButtonText: "ठीक आहे",
            });
            return;
        }


        if (!isValidDate(Startdate) || !isValidDate(Enddate)) {
            return;
        }
        try {
            const payload = {
                startdate: Startdate,
                endtdate: Enddate,
                companyid: "",
                deptid: ""
            };

            const response = await axios.post(
                `${baseUrl.Url}/backend/api/GET_Report_Vyapari`,
                payload,
                {
                    headers: {
                        "Content-Type": "application/json",
                        Accept: "*/*",
                    }
                }
            );

            if (response.status === 200) {
                const data = response.data || [];
                setVyapari(data);
                openVyapariReportInNewTab(data, departmentname, dates);
            } else {
                MySwal.fire({
                    title: "डेटा मिळाला नाही",
                    icon: "error",
                });
            }
        } catch (error) {
            console.error("Error fetching AuctionReport:", error);
            MySwal.fire({
                title: "त्रुटी आली आहे",
                text: "डेटा मिळवताना काहीतरी चुकलं",
                icon: "error",
            });
        }
    };


    const handleDirectReportClick = async () => {
        const { Startdate, Enddate } = dates;

        if (!isValidDate(Startdate) || !isValidDate(Enddate)) {
            MySwal.fire({
                title: "योग्य तारीख निवडा",
                icon: "warning",
            });
            return;
        }

        try {
            const payload = {
                startdate: Startdate,
                endtdate: Enddate,
                companyid: userdetail?.companyid || "",
                deptid: userdetail?.departmentid || ""
            };

            const response = await axios.post(
                `${baseUrl.Url}/backend/api/GET_Report_DayAuction`,
                payload,
                {
                    headers: {
                        "Content-Type": "application/json",
                        Accept: "*/*",
                    }
                }
            );

            if (response.status === 200) {
                const data = response.data || [];
                setDirector(data);
                openDirectReportInNewTab(data, dates, departmentname);
            } else {
                MySwal.fire({
                    title: "डेटा मिळाला नाही",
                    icon: "error",
                });
            }
        } catch (error) {
            console.error("Error fetching AuctionReport:", error);
            MySwal.fire({
                title: "त्रुटी आली आहे",
                text: "डेटा मिळवताना काहीतरी चुकलं",
                icon: "error",
            });
        }
    };

    const handleFarmerReportClick = async () => {
        const { Startdate, Enddate } = dates;

        const today = new Date();
        const startDate = new Date(Startdate);
        const endDate = new Date(Enddate);

        // 🔐 Date validation checks
        if (!isValidDate(Startdate) || !isValidDate(Enddate)) {
            MySwal.fire({
                title: "योग्य तारीख निवडा",
                text: "कृपया सुरुवातीची व शेवटची तारीख योग्य प्रकारे निवडा.",
                icon: "error",
                confirmButtonColor: "#ff0000",
                confirmButtonText: "ठीक आहे",
            });
            return;
        }

        if (startDate > endDate) {
            MySwal.fire({
                title: "चूक तारीख",
                text: "सुरुवातीची तारीख शेवटच्या तारखेपेक्षा जास्त असू शकत नाही.",
                icon: "warning",
                confirmButtonColor: "#ff0000",
                confirmButtonText: "ठीक आहे",
            });
            return;
        }

        if (endDate > today) {
            MySwal.fire({
                title: "चूक तारीख",
                text: "शेवटची तारीख आजच्या तारखेपेक्षा जास्त असू शकत नाही.",
                icon: "warning",
                confirmButtonColor: "#ff0000",
                confirmButtonText: "ठीक आहे",
            });
            return;
        }

        try {
            const payload = {
                startdate: Startdate,
                endtdate: Enddate,
                companyid: "",
                deptid: ""
            };

            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };

            const response = await axios.post(
                `${baseUrl.Url}/backend/api/GET_Report_farmer`,
                payload,
                { headers }
            );

            if (response.status === 200) {
                const data = response.data || [];
                setFarmer(data);
                openFarmerReportInNewTab(data, dates, departmentname);
            } else {
                MySwal.fire({
                    title: "डेटा मिळाला नाही",
                    icon: "error",
                });
            }
        } catch (error) {
            console.error("Error fetching AuctionReport:", error);
            MySwal.fire({
                title: "त्रुटी आली आहे",
                text: "डेटा मिळवताना काहीतरी चुकलं",
                icon: "error",
            });
        }
    };


    const downloadPDFShetkari = async () => {
        const { Startdate, Enddate } = dates;

        const today = new Date();
        const startDate = new Date(Startdate);
        const endDate = new Date(Enddate);

        // 🔐 Date validation checks
        if (!isValidDate(Startdate) || !isValidDate(Enddate)) {
            MySwal.fire({
                title: "योग्य तारीख निवडा",
                text: "कृपया सुरुवातीची व शेवटची तारीख योग्य प्रकारे निवडा.",
                icon: "error",
                confirmButtonColor: "#ff0000",
                confirmButtonText: "ठीक आहे",
            });
            return;
        }

        if (startDate > endDate) {
            MySwal.fire({
                title: "चूक तारीख",
                text: "सुरुवातीची तारीख शेवटच्या तारखेपेक्षा जास्त असू शकत नाही.",
                icon: "warning",
                confirmButtonColor: "#ff0000",
                confirmButtonText: "ठीक आहे",
            });
            return;
        }

        if (endDate > today) {
            MySwal.fire({
                title: "चूक तारीख",
                text: "शेवटची तारीख आजच्या तारखेपेक्षा जास्त असू शकत नाही.",
                icon: "warning",
                confirmButtonColor: "#ff0000",
                confirmButtonText: "ठीक आहे",
            });
            return;
        }


        try {
            const headers = { "Content-Type": "application/json", Accept: "*/*" };

            // 2️⃣ Fetch ALL farmers in the date range
            const farmerPayload = {
                startdate: Startdate,
                endtdate: Enddate,
                companyid: userdetail?.companyid || "",
                deptid: userdetail?.departmentid || ""
            };
            const farmerRes = await axios.post(
                `${baseUrl.Url}/backend/api/GET_Report_farmer`,
                farmerPayload,
                { headers }
            );

            if (farmerRes.status !== 200 || !Array.isArray(farmerRes.data)) {
                throw new Error("Farmer data fetch failed");
            }
            const farmerData = farmerRes.data;
            setShetkari(farmerData);

            // 3️⃣ For each farmer, fetch its services
            let allServiceData = [];
            for (const vyapari of farmerData) {
                if (!vyapari.baid) {
                    console.warn("Skipping missing baid:", vyapari);
                    continue;
                }

                const servicePayload = {
                    baid: vyapari.baid,
                    startdate: Startdate,
                    endtdate: Enddate,
                    companyid: userdetail?.companyID || "",
                    deptid: userdetail?.departmentID || "",
                };

                try {
                    const serviceRes = await axios.post(
                        `${baseUrl.Url}/backend/api/GET_Report_S`,
                        servicePayload,
                        { headers }
                    );
                    if (serviceRes.status === 200 && Array.isArray(serviceRes.data)) {
                        const filtered = serviceRes.data.map(item => ({
                            nSERVICETYPETITLE: item.nSERVICETYPETITLE,
                            sevaValue: item.sevaValue,
                            baid: vyapari.baid,
                            billno: vyapari.billno
                        }));
                        allServiceData = allServiceData.concat(filtered);
                    } else {
                        console.warn(`No services for baid ${vyapari.baid}`);
                    }
                } catch (err) {
                    console.error(`Failed service fetch for baid ${vyapari.baid}`, err);
                }
            }

            setServiceData(allServiceData);

            // 4️⃣ Only once both are ready, open the report
            openShetkariReportInNewTab(farmerData, dates, allServiceData, departmentname);

        } catch (err) {
            console.error("Error generating report:", err);
            MySwal.fire({
                title: "त्रुटी आली आहे",
                text: "डेटा मिळवताना काहीतरी चुकलं",
                icon: "error",
            });
        }
    };



    const fetchshetkariReport = async () => {
        try {
            // ⬅️ First API call: Vyapari Purchase Report
            const payload1 = {
                startdate: dates.Startdate,
                endtdate: dates.Enddate,
                companyid: "",
                deptid: "",
            };

            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };

            const response1 = await axios.post(
                `${baseUrl.Url}/backend/api/GET_Report_farmer`,
                payload1,
                { headers }
            );

            if (response1.status !== 200 || !Array.isArray(response1.data)) {
                throw new Error("Failed to fetch farmer data or invalid data");
            }

            const vyapariData = response1.data;
            setShetkari(vyapariData);

            // 👉 For each vbkid (baid), fetch service data
            let allServiceData = [];

            for (const vyapari of vyapariData) {
                if (!vyapari.baid) {
                    console.warn("Missing baid for record:", vyapari);
                    continue;
                }

                const payload2 = {
                    baid: vyapari.baid,
                    startdate: dates.Startdate,
                    endtdate: dates.Enddate,
                    companyid: userdetail?.companyID || "",
                    deptid: userdetail?.departmentID || "",
                };

                try {
                    const response2 = await axios.post(
                        `${baseUrl.Url}/backend/api/GET_Report_S`,
                        payload2,
                        { headers }
                    );

                    if (response2.status === 200 && Array.isArray(response2.data)) {
                        const filteredServiceData = response2.data.map(item => ({
                            nSERVICETYPETITLE: item.nSERVICETYPETITLE,
                            sevaValue: item.sevaValue,
                            baid: vyapari.baid,
                            billno: vyapari.billno, // used in PDF report matching
                        }));

                        allServiceData = [...allServiceData, ...filteredServiceData];
                        console.log("setServiceDatasetServiceDatasetServiceData", allServiceData)
                    } else {
                        console.warn(`Empty or invalid service data for baid ${vyapari.baid}`);
                    }
                } catch (error) {
                    console.error(`Error fetching service data for baid ${vyapari.baid}:`, error);
                }
            }

            console.log("📦 Final combined service data:", allServiceData);
            setServiceData(allServiceData);


        } catch (error) {
            console.error("❌ Error fetching Shetkari report:", error);
            MySwal.fire({
                title: "त्रुटी",
                text: "अहवाल मिळवण्यात अडचण आली. कृपया पुन्हा प्रयत्न करा.",
                icon: "error",
                confirmButtonText: "ठीक आहे"
            });
        }
    };




    useEffect(() => {
        if (dates.Startdate && dates.Enddate) {
            fetchshetkariReport();
        }
    }, [dates]);


    const downloadExcelShetkari = async () => {
        const isValidDate = (date) => date && !isNaN(new Date(date).getTime());

        const today = new Date();
        const startDate = new Date(dates.Startdate);
        const endDate = new Date(dates.Enddate);

        // तारीख तपासणी
        if (!isValidDate(dates.Startdate) || !isValidDate(dates.Enddate)) {
            MySwal.fire({
                title: "योग्य तारीख निवडा",
                text: "कृपया सुरुवातीची व शेवटची तारीख योग्य प्रकारे निवडा.",
                icon: "error",
                confirmButtonColor: "#ff0000",
                confirmButtonText: "ठीक आहे",
            });
            return;
        }

        if (startDate > endDate) {
            MySwal.fire({
                title: "चूक तारीख",
                text: "सुरुवातीची तारीख शेवटच्या तारखेपेक्षा जास्त असू शकत नाही.",
                icon: "warning",
                confirmButtonColor: "#ff0000",
                confirmButtonText: "ठीक आहे",
            });
            return;
        }

        if (endDate > today) {
            MySwal.fire({
                title: "चूक तारीख",
                text: "शेवटची तारीख आजच्या तारखेपेक्षा जास्त असू शकत नाही.",
                icon: "warning",
                confirmButtonColor: "#ff0000",
                confirmButtonText: "ठीक आहे",
            });
            return;
        }

        try {
            const workbook = new ExcelJS.Workbook();
            const worksheet = workbook.addWorksheet("शेतकरी सेवा अहवाल");

            // मुख्य शीर्षक
            const titleRow = worksheet.addRow(["शेतकरी सेवा अहवाल"]);
            titleRow.getCell(1).font = { bold: true, size: 16 };
            titleRow.getCell(1).alignment = { horizontal: "center", vertical: "middle" };
            worksheet.mergeCells("A1:K1"); // आता 8 कॉलम्स कारण Net Bill साठी एक नवीन कॉलम

            // मूळ कॉलम्स
            const baseHeaders = ["बिल क्रमांक", "दिनांक", "टोकन न", "शेतकरी", "गाव "];

            // सर्व्हिस कॉलम्स
            const serviceHeaders = [...new Set(serviceData.map(item => item.nSERVICETYPETITLE))];

            // Net Bill साठी नवीन कॉलम add करा
            const headers = [...baseHeaders, ...serviceHeaders, "एकुण", "निव्वळ रक्कम"];
            const headerRow = worksheet.addRow(headers);

            headerRow.eachCell((cell) => {
                cell.font = { bold: true, color: { argb: "FFFFFF" } };
                cell.fill = { type: "pattern", pattern: "solid", fgColor: { argb: "808080" } };
                cell.alignment = { horizontal: "center", vertical: "middle" };
            });


            const mergedRows = [];
            // कॉलमची योग्य रुंदी लावा
            headers.forEach((_, idx) => {
                worksheet.getColumn(idx + 1).width = 15;
            });

            // Data तयार करणे
            const purchaseRows = Shetkari.map(item => [
                item.billno ?? "",
                item.date ?? "",
                item.btokanno ?? "",
                item.farmername ?? "",
                item.villagename ?? ""
            ]);

            const groupedServiceValues = serviceHeaders.map(title =>
                serviceData
                    .filter(item => item.nSERVICETYPETITLE === title)
                    .map(item => item.sevaValue ? item.sevaValue.toString() : "")
            );

            const maxRows = Math.max(purchaseRows.length, ...groupedServiceValues.map(arr => arr.length));

            // Normalize purchase rows
            while (purchaseRows.length < maxRows) {
                purchaseRows.push(Array(baseHeaders.length).fill(""));
            }

            const normalizedServiceValues = serviceHeaders.map((_, i) => {
                const columnData = groupedServiceValues[i];
                while (columnData.length < maxRows) {
                    columnData.push("");
                }
                return columnData;
            });


            // Combine all rows
            for (let i = 0; i < maxRows; i++) {
                const purchaseRow = purchaseRows[i];
                const serviceRow = normalizedServiceValues.map(col => col[i]);

                // Total = डाग + वजन + रक्कम + सर्व service values
                const totalAmount = parseFloat(Shetkari[i]?.amount || 0);

                // सेवा रकमेची बेरीज
                const serviceSum = serviceRow
                    .map(val => parseFloat(val) || 0)
                    .reduce((a, b) => a + b, 0);

                // निव्वळ बिल = एकूण बिल - सेवा रक्कम
                const netBill = totalAmount - serviceSum;

                // अंतिम row तयार करा
                const finalRow = [
                    ...purchaseRow,
                    ...serviceRow.map(val => val === "" ? "" : val.toString()),
                    serviceSum.toFixed(2),
                    netBill.toFixed(2)
                ];

                mergedRows.push(finalRow);

                const row = worksheet.addRow(finalRow);

                row.eachCell((cell) => {
                    cell.alignment = { horizontal: "center", vertical: "middle" };
                });
            }


            // Excel तयार करून डाउनलोड करा
            const buffer = await workbook.xlsx.writeBuffer();
            const blob = new Blob([buffer], {
                type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            });
            saveAs(blob, "Vyapari_Purchase_Report.xlsx");

        } catch (error) {
            console.error("Excel तयार करताना त्रुटी:", error);
        }
    };
    const fetchVakkalReport = async () => {
        const { Startdate, Enddate } = dates;

        if (!isValidDate(Startdate) || !isValidDate(Enddate)) {
            return;
        }

        try {
            const payload = {
                startdate: Startdate,
                endtdate: Enddate,
                companyid: "",
                deptid: ""
            };

            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };

            const response = await axios.post(
                `${baseUrl.Url}/backend/api/GET_Report_VyapariVakkal
                `,
                payload,
                { headers }
            );

            if (response.status !== 200) throw new Error("Failed to fetch farmer data");

            setVyapariVakkal(response.data);
        } catch (error) {
            console.error("Error fetching FarmerReport data:", error);
        }
    };

    useEffect(() => {
        if (dates.Startdate && dates.Enddate) {
            fetchVakkalReport();
        }
    }, [dates]);

    const handleVakkalReportClick = async () => {
        const { Startdate, Enddate } = dates;

        const today = new Date();
        const startDate = new Date(Startdate);
        const endDate = new Date(Enddate);

        // 🔐 Date validation checks
        if (!isValidDate(Startdate) || !isValidDate(Enddate)) {
            MySwal.fire({
                title: "योग्य तारीख निवडा",
                text: "कृपया सुरुवातीची व शेवटची तारीख योग्य प्रकारे निवडा.",
                icon: "error",
                confirmButtonColor: "#ff0000",
                confirmButtonText: "ठीक आहे",
            });
            return;
        }

        if (startDate > endDate) {
            MySwal.fire({
                title: "चूक तारीख",
                text: "सुरुवातीची तारीख शेवटच्या तारखेपेक्षा जास्त असू शकत नाही.",
                icon: "warning",
                confirmButtonColor: "#ff0000",
                confirmButtonText: "ठीक आहे",
            });
            return;
        }

        if (endDate > today) {
            MySwal.fire({
                title: "चूक तारीख",
                text: "शेवटची तारीख आजच्या तारखेपेक्षा जास्त असू शकत नाही.",
                icon: "warning",
                confirmButtonColor: "#ff0000",
                confirmButtonText: "ठीक आहे",
            });
            return;
        }

        try {
            const payload = {
                startdate: Startdate,
                endtdate: Enddate,
                companyid: "",
                deptid: ""
            };

            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };

            const response = await axios.post(
                `${baseUrl.Url}/backend/api/GET_Report_VyapariVakkal`,
                payload,
                { headers }
            );

            if (response.status === 200) {
                const data = response.data || [];
                setFarmer(data);
                openVyapariVakkalInNewTab(data, dates, departmentname);
            } else {
                MySwal.fire({
                    title: "डेटा मिळाला नाही",
                    icon: "error",
                });
            }
        } catch (error) {
            console.error("Error fetching AuctionReport:", error);
            MySwal.fire({
                title: "त्रुटी आली आहे",
                text: "डेटा मिळवताना काहीतरी चुकलं",
                icon: "error",
            });
        }
    };

    const downloadExcelVyapariVakkal = async () => {
        const today = new Date();
        const startDate = new Date(dates.Startdate);
        const endDate = new Date(dates.Enddate);

        if (!isValidDate(dates.Startdate) || !isValidDate(dates.Enddate)) {
            MySwal.fire({
                title: "योग्य तारीख निवडा",
                text: "कृपया सुरुवातीची व शेवटची तारीख योग्य प्रकारे निवडा.",
                icon: "error",
                confirmButtonColor: "#ff0000",
                confirmButtonText: "ठीक आहे",
            });
            return;
        }

        // Check if start date is after end date
        if (startDate > endDate) {
            MySwal.fire({
                title: "चूक तारीख",
                text: "सुरुवातीची तारीख शेवटच्या तारखेपेक्षा जास्त असू शकत नाही.",
                icon: "warning",
                confirmButtonColor: "#ff0000",
                confirmButtonText: "ठीक आहे",
            });
            return;
        }

        // Check if end date is in the future
        if (endDate > today) {
            MySwal.fire({
                title: "चूक तारीख",
                text: "शेवटची तारीख आजच्या तारखेपेक्षा जास्त असू शकत नाही.",
                icon: "warning",
                confirmButtonColor: "#ff0000",
                confirmButtonText: "ठीक आहे",
            });
            return;
        }

        try {
            const workbook = new ExcelJS.Workbook();
            const worksheet = workbook.addWorksheet("व्यापारी वककल  Report");

            const headingRow = worksheet.addRow(["व्यापारी वककल (Report)"]);
            headingRow.getCell(1).font = { bold: true, size: 16 };
            headingRow.getCell(1).alignment = { horizontal: "center", vertical: "middle" };
            worksheet.mergeCells("A1:K1");

            const headers = ["दिनांक", "बिल क्रमांक", "शेतकरी", "नग", "वजन", "वजन कमी जास्त", "भाव", "व्यापारी "];
            const headerRow = worksheet.addRow(headers);

            headerRow.eachCell(cell => {
                cell.font = { bold: true, color: { argb: "FFFFFF" } };
                cell.fill = { type: "pattern", pattern: "solid", fgColor: { argb: "808080" } };
                cell.alignment = { horizontal: "center", vertical: "middle" };
            });

            headers.forEach((_, index) => {
                worksheet.getColumn(index + 1).width = 18;
            });

            VyapariVakkal.forEach(item => {
                const row = worksheet.addRow([


                    item.billno || "",
                    item.farmername || "",
                    item.jaali || "",
                    item.weightless || "",
                    item.weight,
                    item.rate || "",
                    item.vyapariname || "",
                    // item.jali || "",

                    // item.amount || "",


                    // item.totalweight || "",
                    // item.totalamount || "",
                    // item.totalcost || "",
                    // item.remainingamount || "",

                ]);
                row.eachCell(cell => {
                    cell.alignment = { horizontal: "center", vertical: "middle" };
                });
            });

            const buffer = await workbook.xlsx.writeBuffer();
            const data = new Blob([buffer], {
                type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            });
            saveAs(data, "व्यापारी वककल(Report).xlsx");

        } catch (error) {
            console.error("Error generating Excel file:", error);
        }
    };

    return (

        <div className="page-wrapper">
            <div className="content">
                <Breadcrumbs
                    maintitle="Sales Report"
                    subtitle=" Manage Your Sales Report"
                />
                <div className="page-header">
                    <div className="page-title">
                        <h3>माहिती पत्रके</h3>
                    </div>
                    <ul className="table-top-head">
                        <li>
                            <div className="page-btn">
                                <Link to={route.ReportIndex} className="btn btn-secondary">
                                    <ArrowLeft className="me-2" />
                                    मागे
                                </Link>
                            </div>
                        </li>
                    </ul>
                </div>
                <div className="">
                    {/* <div className="card table-list-card"> */}
                    <div className="card-body pt-3">
                        <div className="d-flex">
                            <div className="w-100 ">

                                <div className="row ">
                                    <div className="col-lg-4 col-md-6 col-sm-12">
                                        <div className="form-label">
                                            <label className="form-label">Start Date</label>
                                            <input type="date"
                                                className="form-control "
                                                name="Startdate"
                                                value={dates.Startdate}
                                                onChange={handleDateChange}

                                            />
                                        </div>
                                    </div>

                                    <div className="col-lg-2 col-md-6 col-sm-12">
                                        <div className="form-label">
                                            <label className="form-label">To</label>
                                        </div>
                                    </div>

                                    <div className="col-lg-4 col-md-6 col-sm-12">
                                        <div className="form-label">
                                            <label className="form-label">End Date</label>
                                            <input type="date"
                                                className="form-control "
                                                name="Enddate"
                                                value={dates.Enddate}
                                                onChange={handleDateChange}
                                            />
                                        </div>
                                    </div>
                                </div>

                            </div>


                            <div className="w-100 me-5">
                                <div className="row">


                                    <div className="col-lg-6 col-md-6 col-sm-12 pb-3">
                                        <div className="form-label d-flex align-items-center">
                                            <label className="me-2 ">
                                                <img alt="img" src="/assets/img/icons/pdf.svg" />
                                            </label>
                                            <button className="form-control  rbtcolor"
                                                onClick={handleFarmerReportClick}
                                                style={{ width: '230px', height: '35px' }}
                                            >
                                                Farmer Report
                                            </button>
                                        </div>
                                    </div>

                                    <div className="col-lg-6 col-md-6 col-sm-12 ">
                                        <div className="form-label d-flex align-items-center">
                                            <label className="me-2 ">
                                                <img alt="img" src="/assets/img/icons/excel.svg" />
                                            </label>
                                            <button className="form-control rbtcolor"
                                                onClick={downloadExcelfarmer}
                                                style={{ width: '230px', height: '35px' }}
                                            >
                                                farmer  Excel
                                            </button>
                                        </div>
                                    </div>
                                </div>


                                <div className="row">

                                    <div className="col-lg-6 col-md-6 col-sm-12 pb-3">
                                        <div className="form-label d-flex align-items-center">
                                            <label className="me-2 ">
                                                <img alt="img" src="/assets/img/icons/pdf.svg" />
                                            </label>
                                            <button className="form-control  rbtcolor"
                                                onClick={handleReportClick}
                                                style={{ width: '230px', height: '35px' }}
                                            >
                                                Auction Report
                                            </button>
                                        </div>
                                    </div>

                                    <div className="col-lg-6 col-md-6 col-sm-12 ">
                                        <div className="form-label d-flex align-items-center">
                                            <label className="me-2 ">
                                                <img alt="img" src="/assets/img/icons/excel.svg" />
                                            </label>
                                            <button className="form-control  rbtcolor"
                                                onClick={downloadExcelAuction}
                                                style={{ width: '230px', height: '35px' }}
                                            >
                                                Auction Excel
                                            </button>
                                        </div>
                                    </div>
                                </div>


                                <div className="row">

                                    <div className="col-lg-6 col-md-6 col-sm-12 pb-3">
                                        <div className="form-label d-flex align-items-center">
                                            <label className="me-2 ">
                                                <img alt="img" src="/assets/img/icons/pdf.svg" />
                                            </label>
                                            <button className="form-control  rbtcolor"
                                                onClick={handleVyapariReportClick}
                                                style={{ width: '230px', height: '35px' }}
                                            >
                                                Vyapari Report
                                            </button>
                                        </div>
                                    </div>

                                    <div className="col-lg-6 col-md-6 col-sm-12 pb-3">
                                        <div className="form-label d-flex align-items-center">
                                            <label className="me-2 ">
                                                <img alt="img" src="/assets/img/icons/excel.svg" />
                                            </label>
                                            <button className="form-control  rbtcolor"
                                                onClick={downloadExcelVyapari}
                                                style={{ width: '230px', height: '35px' }}
                                            >
                                                Vyapari Excel
                                            </button>
                                        </div>
                                    </div>
                                </div>


                                <div className="row">


                                    <div className="col-lg-6 col-md-6 col-sm-12 pb-3">
                                        <div className="form-label d-flex align-items-center">
                                            <label className="me-2 ">
                                                <img alt="img" src="/assets/img/icons/pdf.svg" />
                                            </label>
                                            <button className="form-control  rbtcolor"
                                                onClick={handleDirectReportClick}
                                                style={{ width: '230px', height: '35px' }}
                                            >
                                                Direct Report
                                            </button>
                                        </div>
                                    </div>
                                    <div className="col-lg-6 col-md-6 col-sm-12 pb-3">
                                        <div className="form-label d-flex align-items-center">
                                            <label className="me-2 ">
                                                <img alt="img" src="/assets/img/icons/excel.svg" />
                                            </label>
                                            <button className="form-control  rbtcolor"
                                                onClick={DirectorExcel}
                                                style={{ width: '230px', height: '35px' }}>
                                                Excel
                                            </button>
                                        </div>
                                    </div>
                                </div>

                                <div className="row">
                                    <div className="col-lg-6 col-md-6 col-sm-12 pb-3">
                                        <div className="form-label d-flex align-items-center">
                                            <label className="me-2">
                                                <img alt="pdf" src="/assets/img/icons/pdf.svg" />
                                            </label>
                                            <button
                                                className="form-control  rbtcolor"
                                                style={{ width: "230px", height: "35px" }}
                                                onClick={handlePattiPaidReportClick}
                                            >
                                                PattiPaid Report
                                            </button>
                                        </div>
                                    </div>
                                    <div className="col-lg-6 col-md-6 col-sm-12 pb-3">
                                        <div className="form-label d-flex align-items-center">
                                            <label className="me-2 ">
                                                <img alt="img" src="/assets/img/icons/excel.svg" />
                                            </label>
                                            <button className="form-control  rbtcolor"
                                                onClick={downloadExcelPaidReport}
                                                style={{ width: '230px', height: '35px' }}>
                                                PAID Report Excel
                                            </button>
                                        </div>
                                    </div>
                                </div>


                                <div className="row">

                                    <div className="col-lg-6 col-md-6 col-sm-12 pb-3">
                                        <div className="form-label d-flex align-items-center">
                                            <label className="me-2">
                                                <img alt="pdf" src="/assets/img/icons/pdf.svg" />
                                            </label>
                                            <button
                                                className="form-control  rbtcolor"
                                                style={{ width: "230px", height: "35px" }}
                                                onClick={handleVyapariPurchaseReportClick}
                                            >
                                                Vyapari Purchase Report
                                            </button>
                                        </div>
                                    </div>


                                    <div className="col-lg-6 col-md-6 col-sm-12 pb-3">
                                        <div className="form-label d-flex align-items-center">
                                            <label className="me-2 ">
                                                <img alt="img" src="/assets/img/icons/excel.svg" />
                                            </label>
                                            <button className="form-control  rbtcolor"
                                                onClick={downloadExcelVyapariPurchase}
                                                style={{ width: '230px', height: '35px' }}>
                                                VyapariPurchaseReportExcel
                                            </button>
                                        </div>
                                    </div>
                                </div>
                                <div className="row">
                                    <div className="col-lg-6 col-md-6 col-sm-12 ">
                                        <div className="form-label d-flex align-items-center">
                                            <label className="me-2 ">
                                                <img alt="img" src="/assets/img/icons/pdf.svg" />
                                            </label>
                                            <button className="form-control  rbtcolor"
                                                onClick={() => downloadPDFShetkari()}
                                                style={{ width: '230px', height: '35px' }}>
                                                Shetkari pdf
                                            </button>
                                        </div>
                                    </div>
                                    <div className="col-lg-6 col-md-6 col-sm-12 pb-3">
                                        <div className="form-label d-flex align-items-center">
                                            <label className="me-2 ">
                                                <img alt="img" src="/assets/img/icons/excel.svg" />
                                            </label>
                                            <button className="form-control  rbtcolor"
                                                onClick={downloadExcelShetkari}
                                                style={{ width: '230px', height: '35px' }}>
                                                Shetkari Excel
                                            </button>
                                        </div>
                                    </div>
                                </div>
                                <div className="row">
                                    <div className="col-lg-6 col-md-6 col-sm-12 pb-3">
                                        <div className="form-label d-flex align-items-center">
                                            <label className="me-2 ">
                                                <img alt="img" src="/assets/img/icons/pdf.svg" />
                                            </label>
                                            <button className="form-control  rbtcolor"
                                                onClick={handleVakkalReportClick}
                                                style={{ width: '230px', height: '35px' }}>
                                                Vyapari Vakkal  PDF
                                            </button>
                                        </div>
                                    </div>

                                    <div className="col-lg-6 col-md-6 col-sm-12 pb-3">
                                        <div className="form-label d-flex align-items-center">
                                            <label className="me-2 ">
                                                <img alt="img" src="/assets/img/icons/excel.svg" />
                                            </label>
                                            <button className="form-control  rbtcolor"
                                                onClick={downloadExcelVyapariVakkal}
                                                style={{ width: '230px', height: '35px' }}>
                                                Vyapari Vakkal  Excel
                                            </button>
                                        </div>

                                    </div>

                                </div>

                                <div className="row">
                                    <div className="col-lg-6 col-md-6 col-sm-12 pb-3">
                                        <div className="form-label d-flex align-items-center">
                                            <label className="me-2 ">
                                                <img alt="img" src="/assets/img/icons/pdf.svg" />
                                            </label>
                                            <Link to={route.FarmerLedger} className="form-control rbtcolor d-flex align-items-center justify-content-center"
                                                style={{ width: '230px', height: '35px', textDecoration: 'none' }}>
                                                Farmer Ledger Report
                                            </Link>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div >
    );
};

export default Reports;

