import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { all_routes } from "../../Router/all_routes";
import { getUserData } from '../../Context/UserData';
import axios from 'axios';

const baseUrl = {
  Url: "http://your-api-base-url.com" // replace with actual base URL
};

function sample_report() {
  const route = all_routes;
  const navigate = useNavigate();
  const { userdetail } = getUserData();
  const departmentname = userdetail?.departmentname || "";
  const dates = {
    Startdate: userdetail?.APPDT || ""
  };

  const [isDesktop, setIsDesktop] = useState(false);
  const [ReportData, setReportData] = useState([]);
  const [VapasiData, setVapasiData] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchData = async () => {
      if (userdetail?.uaid && userdetail?.APPDT) {
        try {
          setLoading(true);
          const payload = {
            vpaid: 'PK16CAFA9D95F454302024',
            trndt: userdetail?.APPDT || "",
            trndate: userdetail?.APPDT || ""
          };

          const headers = {
            "Content-Type": "application/json",
            Accept: "*/*"
          };

          const [reportRes, vapasiRes] = await Promise.all([
            axios.post(baseUrl.Url + "/backend/api/GET_VyapariTransectionReport", {
              vpaid: payload.vpaid,
              trndt: payload.trndt
            }, { headers }),
            axios.post(baseUrl.Url + "/backend/api/GET_VyapariBharanaData", {
              vpaid: payload.vpaid,
              trndate: payload.trndate
            }, { headers })
          ]);

          setReportData(reportRes.data);
          setVapasiData(vapasiRes.data);
        } catch (error) {
          console.error("Error fetching APIs:", error);
        } finally {
          setLoading(false);
        }
      }
    };

    fetchData();
  }, [userdetail?.uaid, userdetail?.APPDT]);

  useEffect(() => {
    const handleResize = () => {
      setIsDesktop(window.innerWidth >= 992);
    };

    handleResize();
    window.addEventListener('resize', handleResize);
    return () => window.removeEventListener('resize', handleResize);
  }, []);

  const formatDate = (dateStr) => {
    const dateObj = new Date(dateStr);
    return dateObj.toLocaleDateString('en-GB', {
      day: '2-digit',
      month: 'short',
      year: 'numeric'
    });
  };

  const openReportInNewTab = () => {
    const totalvapasiAmount = Math.round(
      VapasiData.reduce((sum, item) => sum + parseFloat(item.vapasI_AMT || 0), 0)
    );

    const totalReciveAmount = Math.round(
      ReportData.reduce((sum, item) => sum + parseFloat(item.reciveamount || 0), 0)
    );

    const totalbillAmount = Math.round(
      ReportData.reduce((sum, item) => sum + parseFloat(item.billamount || 0), 0)
    );

    const grandTotalReached = Math.round(totalbillAmount - totalvapasiAmount);

    const rowsHtml = ReportData.map(item => {
      const trndt = formatDate(item.trndt);
      return `
        <tr>
          <td class="num">${trndt}</td>
          <td class="name">${item.vbillno}</td>
          <td class="date">${item.perticulers}</td>
          <td class="name">${item.transactionType}</td>
          <td class="num">${item.reciveamount}</td>
          <td class="num">${item.billamount}</td>
        </tr>
      `;
    }).join("");

    const htmlContent = `
      <html>
        <head>
          <meta charset="UTF-8">
          <title>अहवाल</title>
          <link href="https://fonts.googleapis.com/css2?family=Noto+Sans+Devanagari&display=swap" rel="stylesheet">
          <style>
            body {
                font-family: 'Noto Sans Devanagari', sans-serif;
                margin: 0;
                padding: 0;
                display: flex;
                justify-content: center;
            }
            .report-container {
                width: 100%;
                max-width: 800px;
                margin: 10mm auto 20mm auto;
            }
            h2 {
              text-align: center;
              margin: 20px 0 5px 0;
            }
            .report-header {
              display: flex;
              justify-content: space-between;
              align-items: center;
              margin-bottom: 10px;
            }
            .report-title {
              font-size: 20px;
              font-weight: bold;
              text-align: center;
              flex: 1;
            }
            .report-dates {
              text-align: right;
              font-size: 14px;
              flex: 1;
            }
            hr {
              border: 1.5px solid #444;
              margin: 10px 0 15px 0;
            }
            table {
              width: 100%;
              border-collapse: collapse;
              page-break-inside: auto;
            }
            th, td {
              border: 1.5px solid #444;
              padding: 8px;
              font-size: 14px;
              text-align: center;
            }
            th {
              background-color: #d1d1d1;
              font-weight: bold;
            }
            .summary-row td {
              border: none !important;
              font-weight: bold;
            }
            @media print {
              @page {
                size: A4 portrait;
                margin: 20mm;
              }
              body {
                margin: 0;
              }
              .report-container {
                width: 100%;
                margin: 10mm 10mm 20mm 10mm;
              }
              thead {
                display: table-header-group;
              }
              tr {
                page-break-inside: avoid;
              }
            }
          </style>
        </head>
        <body>
          <div class="report-container">
            <h2>अहवाल</h2>
            <div class="report-header">
              <div style="flex: 1;"></div>
              <div class="report-title">${departmentname}</div>
              <div class="report-dates">दिनांक: ${dates.Startdate || "-"}</div>
            </div>
            <hr />
            <table>
              <thead>
                <tr>
                  <th>Date</th>
                  <th>Bill No</th>
                  <th>Particulars</th>
                  <th>Type</th>
                  <th>Amount Received</th>
                  <th>Bill Amount</th>
                </tr>
              </thead>
              <tbody>
                ${rowsHtml}
                <tr class="summary-row">
                  <td colspan="3"></td>
                  <td>Total:</td>
                  <td>${totalReciveAmount.toFixed(2)}</td>
                  <td>${totalbillAmount.toFixed(2)}</td>
                </tr>
                <tr class="summary-row">
                  <td colspan="4"></td>
                  <td>Vapasi Total:</td>
                  <td>${totalvapasiAmount.toFixed(2)}</td>
                </tr>
                <tr class="summary-row">
                  <td colspan="4"></td>
                  <td>Grand Total:</td>
                  <td>${grandTotalReached.toFixed(2)}</td>
                </tr>
              </tbody>
            </table>
          </div>
        </body>
      </html>
    `;

    const encodedHtml = new TextEncoder().encode(htmlContent);
    const blob = new Blob([encodedHtml], { type: "text/html;charset=UTF-8" });
    const url = URL.createObjectURL(blob);
    window.open(url, "_blank");
  };

  return (
    <div className="container mt-4">
      <h4>व्यापारी अहवाल</h4>
      <button
        className="btn btn-primary"
        disabled={loading}
        onClick={openReportInNewTab}
      >
        अहवाल उघडा (Open Report)
      </button>
    </div>
  );
}

export default sample_report;
