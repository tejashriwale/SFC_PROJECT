import React, { useEffect, useState } from 'react';
import ImageWithBasePath from '../../core/img/imagewithbasebath';
import { useNavigate } from 'react-router-dom';
import { all_routes } from "../../Router/all_routes";
import { getUserData } from '../../Context/UserData';
import axios from 'axios';
import DayBook from './DayBook';

function VyapariDashboardIndex() {
  const [Vyapariname1, setVyapariname] = useState([]);
  const route = all_routes;
  const navigate = useNavigate();
  const [isDesktop, setIsDesktop] = useState(false);
  const { userdetail } = getUserData();

  // const [vyapari, setvyapari] = useState([]);

  const fetchCounter = async () => {
    try {
      const headers = {
        "Content-Type": "application/json",
        Accept: "*/*",
      };

      const payload = {
        companyid: "",
        deptid: "",
      };

      const response = await axios.post(
        `${baseUrl.Url}/backend/api/GET_VaypariName`,
        payload,
        { headers }
      );
      if (response.status !== 200)
        throw new Error("Failed to fetch vendor data");
      console.log("requisition setails", response.data)
      const data = response.data;
      const vyapariname = data.filter((a) => a.vpaid == userdetail?.uaid)[0].vfullnbame
      setVyapariname(vyapariname);
      console.log("vyapariname", vyapariname)
    } catch (error) {
      console.error("Error fetching vendor data:", error);
    }
  };




  useEffect(() => {
    const handleResize = () => {
      setIsDesktop(window.innerWidth >= 992);
    };

    handleResize();
    window.addEventListener('resize', handleResize);
    return () => {
      window.removeEventListener('resize', handleResize);
    };
  }, []);

  const cardsData = [
    { title: 'शेतकरी माल मान्यता', image: 'assets/img/avatar/auctionapprove.png', path: '/VApproveMaster' },
    { title: 'व्यापरी बिल', image: 'assets/img/avatar/billing.png', path: '/VyapriBillMaster' },
    {
      title: 'व्यवहार पत्रक',
      image: 'assets/img/avatar/billing.png',
      openInNewTab: true,
      path: '/VyapriTrnsectionReport',
    },
  ];
  const handleCardClick = async (card) => {
    if (card.path === '/VyapriTrnsectionReport') {
      try {
        const headers = {
          "Content-Type": "application/json",
          Accept: "*/*",
        };

        const payload = {
          companyid: "",
          deptid: "",
        };

        const response = await axios.post(
          `${baseUrl.Url}/backend/api/GET_VaypariName`,
          payload,
          { headers }
        );

        if (response.status !== 200)
          throw new Error("Failed to fetch vendor data");

        const data = response.data;
        const vyapariname = data.find(a => a.vpaid === "PK16CB07AFAFFB80662014")?.vfullnbame || "-";
        // const vyapariname = data.find(a => a.vpaid === userdetail?.uaid)?.vfullnbame || "-";
        setVyapariname(vyapariname);
        console.log("vyapariname", vyapariname);

        const vpaid = 'PK16CB07AFAFFB80662014';
        // const vpaid = userdetail?.uaid || "";
        const trndt = userdetail?.APPDT || '';
        const departmentname = userdetail?.departmentname || '';

        const [reportRes, vapasiRes] = await Promise.all([
          axios.post(`${baseUrl.Url}/backend/api/GET_VyapariTransectionReport`, { vpaid, trndt }, { headers }),
          axios.post(`${baseUrl.Url}/backend/api/GET_VyapariBharanaData`, { vpaid, trndate: trndt }, { headers })
        ]);

        const ReportData = reportRes.data;
        const VapasiData = vapasiRes.data;

        const totalvapasiAmount = Math.round(VapasiData.reduce((sum, item) => sum + parseFloat(item.vapasI_AMT || 0), 0));
        const totalReciveAmount = Math.round(ReportData.reduce((sum, item) => sum + parseFloat(item.reciveamount || 0), 0));
        const totalbillAmount = Math.round(ReportData.reduce((sum, item) => sum + parseFloat(item.billamount || 0), 0));
        const grandTotalpending = Math.round(totalbillAmount - totalReciveAmount);
        // const grandTotalpending = Math.round(ReportData.reduce((sum, item) => sum + parseFloat(item.total || 0), 0));
        const grandGrandTotal = Math.round(grandTotalpending - totalvapasiAmount);

        const formatDate = (dateStr) => {
          const dateObj = new Date(dateStr);
          return dateObj.toLocaleDateString('en-GB', {
            day: '2-digit',
            month: 'short',
            year: 'numeric'
          });
        };

        const rowsHtml = ReportData.map(item => `
        <tr>
          <td style="text-align: center;">${formatDate(item.trndt)}</td>
          <td style="text-align: center;">${item.vbillno}</td>
          <td style="text-align: left;">${item.perticulers}</td>
          <td style="text-align: left;">${item.transactionType}</td>
          <td style="text-align: right;">${item.billamount}</td>
          <td style="text-align: right;">${item.reciveamount}</td>
          <td style="text-align: right;">${item.total}</td>
        </tr>
      `).join("");

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

              .report-row {
                display: flex;
                justify-content: space-between;
                width: 100%;
              }

              .report-title-left {
                font-size: 16px;
                text-align: left;
                flex: 1;
              }

              .report-date-right {
                font-size: 14px;
                text-align: right;
                flex: 1;
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
                flex-direction: column;
                align-items: center;
                margin-bottom: 10px;
              }

              .report-title {
                font-size: 20px;
                // font-weight: bold;
                text-align: center;
              }

              .report-dates {
                font-size: 14px;
                text-align: center;
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
                font-weight: bold;
              }

              .no-border-top td {
                border-top: none !important;
              }

              .no-border td {
                border: none !important;
              }

              .bottom-line {
                height: 2px;
                background-color: #444;
                width: 100%;
                margin-top: 6px;
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
              <h2> व्यापारी खाते विवरण</h2>
              <div class="report-header">
                <div class="report-title">${departmentname}</div>
               
              </div>
              <div class="report-row">
                <div class="report-title-left">व्यापारी नाव : ${vyapariname}</div>
                <div class="report-date-right">दिनांक: ${userdetail?.APPDT || "-"}</div>
              </div>


              <hr />
              <table>
                <thead>
                  <tr>
                    <th>दिनांक</th>
                    <th>बिल क्रमांक</th>
                    <th>व्यवहार</th>
                    <th>प्रकार</th>
                    <th>बिल रक्कम</th>
                    <th>भरलेली रक्कम</th>
                    <th>शिल्लक</th>
                  </tr>
                </thead>
                <tbody>
                  ${rowsHtml}
                  <tr class="no-border">
                    <td colspan="7" style="padding: 0;">
                      <div class="bottom-line"></div>
                    </td>
                  </tr>
                  <tr class="summary-row no-border-top no-border">
                    <td colspan="3" style="border: none;"></td>
                    <td style="font-weight: normal;">एकूण:</td>
                    <td style="font-weight: bold; text-align: right;">${totalbillAmount.toFixed(2)}</td>
                    <td style="font-weight: bold; text-align: right;">${totalReciveAmount.toFixed(2)}</td>
                    <td style="font-weight: bold; text-align: right;">${grandTotalpending.toFixed(2)}</td>
                  </tr>
                 
                  <tr class="summary-row no-border">
                    <td colspan="4" style="border: none;"></td>
                    <td style="font-weight: normal;">सरासरी वापसी:</td>
                    <td></td>
                    <td style="text-align: right; font-weight: bold;">- ${totalvapasiAmount.toFixed(2)}</td>
                  </tr>
                  <tr class="summary-row">
                    <td colspan="4" style="border: none;"></td>
                    <td style="border-top: 2px solid black; border-left: none; border-right: none; border-bottom: none; font-weight: normal;">
                      व्यापारी देणे :
                    </td>
                    <td style="border-top: 2px solid black; border-left: none; border-right: none; border-bottom: none; font-weight: normal;"></td>
                    <td style="text-align: right; border-top: 2px solid black; border-left: none; border-right: none; border-bottom: none; font-weight: bold;">
                      ${grandGrandTotal.toFixed(2)}
                    </td>
                  </tr>
                </tbody>
              </table>
            </div>
          </body>
        </html>
      `;

        const blob = new Blob([htmlContent], { type: 'text/html' });
        const url = URL.createObjectURL(blob);
        window.open(url, '_blank');
      } catch (error) {
        console.error("Error generating report:", error);
      }
    } else if (card.openInNewTab) {
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
            <h3>व्यापरी  सूची</h3>
          </div>
        </div>

        <div className="row">
          {cardsData.map((card, index) => (
            <div
              className="col-6 col-sm-6 col-md-4 col-lg-2 col-xxl-2 mb-3"
              key={index}
            >
              <div className="card text-center" style={{ borderRadius: '20px' }}>
                <div className="card-header border-bottom-0 pb-0"></div>
                <div
                  className="card-body pt-1"
                  style={{ height: '120px', cursor: 'pointer' }}
                  onClick={() => handleCardClick(card)}
                >
                  <span className="avatar avatar-xl avatar-rounded me-2 mb-2">
                    <ImageWithBasePath src={card.image} alt={card.title} />
                  </span>
                  <div className="fw-semibold fs-16">{card.title}</div>
                </div>
              </div>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
}

export default VyapariDashboardIndex;
