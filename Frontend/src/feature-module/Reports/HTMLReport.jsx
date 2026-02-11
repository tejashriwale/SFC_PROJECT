

export const openVyapariPurchaseReportInNewTab = (VyapariPurchase, dates, serviceData, departmentname) => {
    // Distinct service titles
    const serviceTitles = [...new Set(serviceData.map(s => s.nSERVICETYPETITLE))];

    const tableRows = VyapariPurchase.map(vyapari => {
        const vbkid = vyapari.vbkid;

        const serviceCells = serviceTitles.map(title => {
            const match = serviceData.find(s => s.vbkid === vbkid && s.nSERVICETYPETITLE === title);
            return `<td>${match?.sevaValue || ""}</td>`;
        });

        // Calculate total & net bill
        const basicValues = [vyapari.totaljaali, vyapari.totalweight, vyapari.totalamount].map(v => parseFloat(v) || 0);
        const serviceValues = serviceTitles.map(title => {
            const match = serviceData.find(s => s.vbkid === vbkid && s.nSERVICETYPETITLE === title);
            return parseFloat(match?.sevaValue) || 0;
        });

        const total = [...basicValues, ...serviceValues].reduce((a, b) => a + b, 0).toFixed(2);
        const net = (parseFloat(total) - serviceValues.reduce((a, b) => a + b, 0)).toFixed(2);

        return `
      <tr>
        <td>${vyapari.date || ""}</td>
        <td>${vyapari.billno || ""}</td>
        <td>${vyapari.vyapariname || ""}</td>
        <td>${vyapari.totaljaali || ""}</td>
        <td>${vyapari.totalweight || ""}</td>
        <td>${vyapari.totalamount || ""}</td>
        ${serviceCells.join("")}
        <td>${total}</td>
        <td>${net}</td>
      </tr>
    `;
    });

    // ======== Calculate Total Row ========
    const totalRow = (() => {
        const totalCells = [];

        totalCells.push(`<td>Total</td>`); // तारीख column
        totalCells.push(`<td></td>`); // बिल क्रमांक
        totalCells.push(`<td></td>`); // व्यापारी नाव

        const getSum = key => VyapariPurchase.reduce((acc, v) => acc + (parseFloat(v[key]) || 0), 0).toFixed(2);

        totalCells.push(`<td>${getSum("totaljaali")}</td>`);
        totalCells.push(`<td>${getSum("totalweight")}</td>`);
        totalCells.push(`<td>${getSum("totalamount")}</td>`);

        // service totals
        serviceTitles.forEach(title => {
            const sum = serviceData
                .filter(s => s.nSERVICETYPETITLE === title)
                .reduce((acc, s) => acc + (parseFloat(s.sevaValue) || 0), 0)
                .toFixed(2);
            totalCells.push(`<td>${sum}</td>`);
        });

        // Total and NetBill
        const allTotal = VyapariPurchase.reduce((acc, vyapari) => {
            const vbkid = vyapari.vbkid;
            const basic = ["totaljaali", "totalweight", "totalamount"]
                .map(k => parseFloat(vyapari[k]) || 0)
                .reduce((a, b) => a + b, 0);
            const service = serviceTitles
                .map(title => {
                    const match = serviceData.find(s => s.vbkid === vbkid && s.nSERVICETYPETITLE === title);
                    return parseFloat(match?.sevaValue) || 0;
                })
                .reduce((a, b) => a + b, 0);
            return acc + basic + service;
        }, 0).toFixed(2);

        const allNet = VyapariPurchase.reduce((acc, vyapari) => {
            const basic = ["totaljaali", "totalweight", "totalamount"]
                .map(k => parseFloat(vyapari[k]) || 0)
                .reduce((a, b) => a + b, 0);
            return acc + basic;
        }, 0).toFixed(2);

        totalCells.push(`<td>${allTotal}</td>`);
        totalCells.push(`<td>${allNet}</td>`);

        return `<tr>${totalCells.join("")}</tr>`;
    })();


    // Table headers
    const headers = `
    <tr>
      <th>तारीख</th>
      <th>बिल क्रमांक</th>
      <th>व्यापारी नाव</th>
      <th>डाग</th>
      <th>वजन</th>
      <th>रक्कम</th>
      ${serviceTitles.map(title => `<th>${title}</th>`).join("")}
      <th>एकूण</th>
      <th>नेटबिल</th>
    </tr>
  `;

    const htmlContent = `
  <html>
    <head>
      <meta charset="UTF-8" />
      <title>व्यापारी खरेदी अहवाल</title>
      <link href="https://fonts.googleapis.com/css2?family=Noto+Sans+Devanagari&display=swap" rel="stylesheet" />
    <style>
        body {
          font-family: 'Noto Sans Devanagari', sans-serif;
            margin: 0;
            padding: 0;
            display: flex;
            ustify-content: center;
        }

        h2 {
            text-align: center;
            margin: 20px 0 5px 0;
        }
        .report-container {
            width: 90%;
            margin: 10mm auto 20mm auto;
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
            font-size: 13px;
            text-align: center;
            }

        th {
            background-color: #d1d1d1;
            font-weight: bold;
            }

        th:nth-child(1), td:nth-child(1) { min-width: 90px; }   /* तारीख */
        th:nth-child(3), td:nth-child(3) { min-width: 180px; }   /* व्यापारी नाव */


        th, td {
            border: 1.5px solid #444;
            padding: 8px;
            font-size: 10px;
            text-align: center;
            }

        .btn {
            padding: 10px 20px;
            background: #007bff;
            color: #fff;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }

      
        @media print {
            body {
                padding: 0px;
                margin: 5px;
                font-size: 12px;
            }

            .btn, .no-print {
                display: none;
            }

            table, th, td {
                border: 1px solid #000;
                page-break-inside: avoid;
            }

            th {
              
                -webkit-print-color-adjust: exact;
            }

            hr {
                border-color: #999;
                margin: 10px 0;
            }

            @page {
                size: A4 landscape;
                margin: 15mm;
            }
        }
    </style>


    </head>
    <body>
     <div class="report-container">
      <h2>व्यापारी खरेदी अहवाल</h2>

       <div class="report-header">
            <div style="flex: 1;"></div>
            <div class="report-title">${departmentname || "जय मातादी मार्केट कमिटी"}</div>
            <div class="report-dates">
            ${dates.Startdate || "-"} ते ${dates.Enddate || "-"}
            </div>
        </div>

      <hr />

      <table>
        <thead>${headers}</thead>
     
        <tbody>
            ${tableRows.join("")}
            ${totalRow}
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




export const openPaidReportInNewTab = (Pattipaid, dates, departmentname) => {
    const htmlContent = `
  <html>
    <head>
      <meta charset="UTF-8" />
      <title>पटी अहवाल</title>
      <link href="https://fonts.googleapis.com/css2?family=Noto+Sans+Devanagari&display=swap" rel="stylesheet" />
      <style>
        body {
          font-family: 'Noto Sans Devanagari', sans-serif;
          padding: 20px 100px;
        }
        .report-container {
        width: 90%;
        margin: 10mm auto 20mm auto;
        }
        h2 {
          text-align: center;
          margin-bottom: 10px;
        }
        .report-header {
          display: flex;
          justify-content: space-between;
          align-items: center;
          margin-bottom: 10px;
        }
        .report-title {
          text-align: center;
          font-size: 20px;
          font-weight: bold;
        }
        .report-dates {
        text-align: right;
        font-size: 14px;
        flex: 1;
        }
        hr {
          border: 1px solid #ccc;
          margin: 10px 0 20px 0;
        }
        table {
          width: 100%;
          border-collapse: collapse;
        }
       
        th:nth-child(1), td:nth-child(1) { min-width: 90px; }   /* तारीख */
        th:nth-child(4), td:nth-child(4) { min-width: 180px; }   /* व्यापारी नाव */


        th, td {
            border: 1.5px solid #444;
            padding: 8px;
            font-size: 10px;
            text-align: center;
            }
        th {
          background-color: #f0f0f0;
        }
        .btn {
          padding: 10px 20px;
          background: #007bff;
          color: #fff;
          border: none;
          border-radius: 5px;
          cursor: pointer;
        }
        @media print {
            body {
              margin: 0;
              padding: 5px;
            }
            .no-print {
              display: none;
            }
            table, th, td {
              border: 1px solid #000;
            }
            th {
              background-color: #ddd !important;
              -webkit-print-color-adjust: exact;
            }
            @page {
            size: A4 landscape;
            margin: 0;
           }
          }

      </style>
    </head>
    <body>
       <div class="report-container">
      <h2>पटी अहवाल</h2>

      <div class="report-header">
            <div style="flex: 1;"></div>
            <div class="report-title">${departmentname || "जय मातादी मार्केट कमिटी"}</div>
            <div class="report-dates">
            ${dates.Startdate || "-"} ते ${dates.Enddate || "-"}
            </div>
        </div>

      <hr />

      <table>
        <thead>
          <tr>
            <th>तारीख</th>
            <th>बिल क्रमांक</th>
            <th>टोकन क्रमांक</th>
            <th>शेतकरी नाव</th>
            <th>गाव</th>
            <th>वाहन नाव</th>
            <th>एकूण खर्च</th>
            <th>रक्कम</th>
            <th>जमा रक्कम</th>
            <th>चेक रक्कम</th>
            <th>ऑनलाईन रक्कम</th>
            <th>एकूण जमा</th>
            <th>बाकी रक्कम</th>
          </tr>
        </thead>
        <tbody>
          ${Pattipaid.map(item => `
            <tr>
              <td>${item.date || ""}</td>
              <td>${item.billno || ""}</td>
              <td>${item.btokanno || ""}</td>
              <td>${item.fname || ""}</td>
              <td>${item.villagename || ""}</td>
              <td>${item.vehicleno || ""}</td>
              <td>${item.totalcost || ""}</td>
              <td>${item.remainingamount || ""}</td>
              <td>${item.cashamount || ""}</td>
              <td>${item.chequeamount || ""}</td>
              <td>${item.onlineamount || ""}</td>
              <td>${item.total || ""}</td>
              <td>${item.bakirakkam || ""}</td>
            </tr>
          `).join("")}
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




export const openReportInNewTab = (Auction, dates, departmentname) => {
    const htmlContent = `
    <html>
        <head>
            <meta charset="UTF-8">
            <title>लिलाव अहवाल</title>
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
                    width: 90%;
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
                    font-size: 13px;
                    text-align: center;
                }

                th {
                    background-color: #d1d1d1;
                    font-weight: bold;
                }

                .btn {
                    padding: 10px 20px;
                    background: #007bff;
                    color: #fff;
                    border: none;
                    border-radius: 5px;
                    cursor: pointer;
                }

                @media print {
                    .btn {
                        display: none;
                    }

                    @page {
                        size: A4 landscape;
                        margin: 15mm;
                    }

                    body {
                        margin: 0;
                    }

                    .report-container {
                        width: 100%;
                        margin: 0 auto;
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
                <h2>लिलाव अहवाल</h2>

                <div class="report-header">
                    <div style="flex: 1;"></div>
                    <div class="report-title">${departmentname}</div>
                    <div class="report-dates">
                        ${dates.Startdate || "-"} ते ${dates.Enddate || "-"}
                    </div>
                </div>

                <hr />

                <table>
                    <thead>
                        <tr>
                            <th>टोकन नं.</th>
                            <th>शेतकरी</th>
                            <th>दिनांक</th>
                            <th>पिक</th>
                            <th>मोबाईल नं</th>
                            <th>जाळी</th>
                            <th>गाव</th>
                            <th>वाहन नं</th>
                        </tr>
                    </thead>
                    <tbody>
                        ${Auction.map((item) => `
                            <tr>
                                <td class="num">${item.toknno}</td>
                                <td class="name">${item.farmername}</td>
                                <td class="date">${item.date}</td>
                                <td class="name">${item.croplabel}</td>
                                <td class="num">${item.mobileno}</td>
                                <td class="num">${item.caretS_COUNT}</td>
                                <td class="name">${item.village}</td>
                                <td class="num">${item.vehno}</td>
                            </tr>
                        `).join("")}
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






export const openVyapariReportInNewTab = (Vyapari, departmentname, dates) => {
    const htmlContent = `
    <html>
        <head>
            <meta charset="UTF-8">
            <title>व्यापारी अहवाल</title>
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
                    width: 90%;
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
                    font-size: 13px;
                    text-align: center;
                }

                th {
                    background-color: #d1d1d1;
                    font-weight: bold;
                }

                .btn {
                    padding: 10px 20px;
                    background: #007bff;
                    color: #fff;
                    border: none;
                    border-radius: 5px;
                    cursor: pointer;
                }

                @media print {
                    .btn {
                        display: none;
                    }

                    @page {
                        size: A4 landscape;
                        margin: 15mm;
                    }

                    body {
                        margin: 0;
                    }

                    .report-container {
                        width: 100%;
                        margin: 0 auto;
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
                <h2>व्यापारी अहवाल</h2>

                <div class="report-header">
                    <div style="flex: 1;"></div>
                      <div class="report-title">${departmentname}</div>
                   <div class="report-dates">
                        ${dates.Startdate || "-"} ते ${dates.Enddate || "-"}
                    </div>
               
                </div>

                <hr />

                <table>
                    <thead>
                        <tr>
                            <th >शेतकरी</th>
                            <th >व्यापारी</th>
                            <th >बिल.नं.</th>
                            <th>दिनांक</th>
                            <th>पिक</th>
                            <th >जाळी</th>
                            <th >वजन</th>
                            <th >दर</th>
                            <th >रक्कम</th>
                            <th >कर</th>
                            <th >उर्वरित र.</th>
                        </tr>
                    </thead> 
                    <tbody>
                        ${Vyapari.map((item) => `
                            <tr>
                                <td>${item.lname || ''} ${item.fname || ''} ${item.fathername || ''}</td>
                                <td>${item.vname}</td>
                                <td>${item.billno}</td>
                                <td>${item.date || '-'}</td>
                                <td>${item.croplabel}</td>
                                <td>${Math.round(item.totaljaali)}</td>
                                <td>${item.totalweight ? Math.round(item.totalweight) : '-'}</td>
                                <td>${item.rate ? Math.round(item.rate) : '-'}</td>
                                <td>${item.totalamount ? Math.round(item.totalamount) : '-'}</td>
                                <td>${item.totalloss ? Math.round(item.totalloss) : '-'}</td>
                                <td>${item.remainingamount ? Math.round(item.remainingamount) : '-'}</td>
                            </tr>
                        `).join("")}
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

export const openDirectReportInNewTab = (Director, dates, departmentname) => {
    const htmlContent = `
    <html>
        <head>
            <meta charset="UTF-8">
            <title>मार्केटिंग संचालक, महाराष्ट्र राज्य अहवाल</title>
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
                    width: 90%;
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

                .btn {
                    padding: 10px 20px;
                    background: #007bff;
                    color: #fff;
                    border: none;
                    border-radius: 5px;
                    cursor: pointer;
                }

                @media print {
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

                    @page {
                        margin: 20mm;
                    }

                    .btn {
                        display: none;
                    }
                }
            </style>
        </head>
        <body>
            <div class="report-container">
                <h2>मार्केटिंग संचालक, महाराष्ट्र राज्य अहवाल</h2>

                <div class="report-header">
                    <div style="flex: 1;"></div>
                    <div class="report-title">${departmentname || "जय मातादी मार्केट कमिटी"}</div>
                    <div class="report-dates">
                        ${dates.Startdate || "-"} ते ${dates.Enddate || "-"}
                    </div>
                </div>

                <hr />

                <table>
                    <thead>
                        <tr>
                            <th>तारीख</th>
                            <th>पिक</th>
                            <th>जाळी</th>
                            <th>प्रमाण</th>
                            <th>मालाची किंमत</th>
                            <th>बाजार शुल्क</th>
                            <th>सु.फी</th>
                            <th>जास्तीत जास्त दर</th>
                            <th>किमान दर</th>
                        </tr>
                    </thead>
                    <tbody>
                        ${Director.map(item => `
                            <tr>
                                <td>${item.date || ""}</td>
                                <td>${item.cropLabels || ""}</td>
                                <td>${item.totaL_JALI || ""}</td>
                                <td>${item.totaL_QUINTAL || ""}</td>
                                <td>${item.totaL_RATE || ""}</td>
                                <td>${item.markeT_FEE || ""}</td>
                                <td>${item.supeR_FEE || ""}</td>
                                <td>${item.maX_RATE || ""}</td>
                                <td>${item.miN_RATE || ""}</td>
                            </tr>
                        `).join("")}
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




export const openFarmerReportInNewTab = (Farmer, dates, departmentname) => {
    const htmlContent = `
    <html>
        <head>
            <meta charset="UTF-8">
            <title>शेतकरी अहवाल</title>
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
                    width: 90%;
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
                    font-size: 13px;
                    text-align: center;
                }

                th {
                    background-color: #d1d1d1;
                    font-weight: bold;
                }

                .btn {
                    padding: 10px 20px;
                    background: #007bff;
                    color: #fff;
                    border: none;
                    border-radius: 5px;
                    cursor: pointer;
                }

                @media print {
                    .btn {
                        display: none;
                    }

                    @page {
                        size: A4 landscape;
                        margin: 15mm;
                    }

                    body {
                        margin: 0;
                    }

                    .report-container {
                        width: 100%;
                        margin: 0 auto;
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
                <h2>शेतकरी अहवाल</h2>

                <div class="report-header">
                    <div style="flex: 1;"></div>
                    <div class="report-title">${departmentname || "जय मातादी मार्केट कमिटी"}</div>
                    <div class="report-dates">
                        ${dates.Startdate || "-"} ते ${dates.Enddate || "-"}
                    </div>
                </div>

                <hr />

                <table>
                    <thead>
                        <tr>
                            <th>शेतकरी</th>
                            <th>बिल नं</th>
                            <th>तारीख</th>
                            <th>दर</th>
                            <th>जाळी</th>
                            <th>वजन</th>
                            <th>रक्कम</th>
                            <th>केरेट्स</th>
                            <th>एकूण वजन</th>
                            <th>एकूण रक्कम</th>
                            <th>खर्च</th>
                            <th>शिल्लक रक्कम</th>
                            <th>व्यापारी</th>
                        </tr>
                    </thead>
                    <tbody>
                        ${Farmer.map(item => `
                            <tr>
                                <td>${item.farmername || ""}</td>
                                <td>${item.billno || ""}</td>
                                <td>${item.date || ""}</td>
                                <td>${item.rate || ""}</td>
                                <td>${item.jali || ""}</td>
                                <td>${item.weight || ""}</td>
                                <td>${item.amount || ""}</td>
                                <td>${item.totalcarets || ""}</td>
                                <td>${item.totalweight || ""}</td>
                                <td>${item.totalamount || ""}</td>
                                <td>${item.totalcost || ""}</td>
                                <td>${item.remainingamount || ""}</td>
                                <td>${item.vyapariname || ""}</td>
                            </tr>
                        `).join("")}
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




export const openShetkariReportInNewTab = (Shetkari, dates, serviceData = [], departmentname) => {
    const distinctServiceTitles = [
        ...new Set(
            serviceData
                .map(item => item.nSERVICETYPETITLE)
                .filter(title => typeof title === "string" && title.trim() !== "")
        )
    ];

    const htmlContent = `
    <html>
        <head>
            <meta charset="UTF-8">
            <title>शेतकरी तपशील अहवाल</title>
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
                    width: 95%;
                    margin: 10mm auto 20mm auto;
                }

                h2 {
                    text-align: center;
                    margin: 20px 0 10px 0;
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
                    padding: 6px;
                    font-size: 13px;
                    text-align: center;
                }

                th {
                    background-color: #f0f0f0;
                    font-weight: bold;
                }

                .btn {
                    padding: 10px 20px;
                    background: #007bff;
                    color: #fff;
                    border: none;
                    border-radius: 5px;
                    cursor: pointer;
                }

                @media print {
                    .btn {
                        display: none;
                    }

                    @page {
                        size: A4 landscape;
                        margin: 15mm;
                    }

                    body {
                        margin: 0;
                    }

                    .report-container {
                        width: 100%;
                        margin: 0 auto;
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
                <h2>शेतकरी तपशील अहवाल</h2>

                <div class="report-header">
                    <div style="flex: 1;"></div>
                    <div class="report-title">${departmentname || "जय मातादी मार्केट कमिटी"}</div>
                    <div class="report-dates">
                        ${dates.Startdate || "-"} ते ${dates.Enddate || "-"}
                    </div>
                </div>

                <hr />

                <table>
                    <thead>
                        <tr>
                            <th>तारीख</th>
                            <th>टोकन नं.</th>
                            <th>शेतकरी</th>
                            <th>गाव</th>
                            <th>जाळी</th>
                            ${distinctServiceTitles.map(title => `<th>${title}</th>`).join("")}
                            <th>एकूण</th>
                            <th>निव्वळ रक्कम</th>
                        </tr>
                    </thead>
                    <tbody>
                        ${Shetkari.map(item => {
        const matchingServices = serviceData.filter(service =>
            service.baid === item.baid && service.billno === item.billno
        );

        const serviceValues = distinctServiceTitles.map(title => {
            const found = matchingServices.find(s => s.nSERVICETYPETITLE === title);
            return found ? parseFloat(found.sevaValue).toFixed(2) : "0.00";
        });

        const serviceTotal = serviceValues.reduce((sum, val) => sum + parseFloat(val), 0);
        const totalAmount = parseFloat(item.amount || 0);
        const netAmount = totalAmount - serviceTotal;

        return `
                                <tr>
                                    <td>${item.date || ""}</td>
                                    <td>${item.btokanno || ""}</td>
                                    <td>${item.farmername || ""}</td>
                                    <td>${item.villagename || ""}</td>
                                    <td>${item.jali || ""}</td>
                                    ${serviceValues.map(val => `<td>${val}</td>`).join("")}
                                    <td>${serviceTotal.toFixed(2)}</td>
                                    <td>${netAmount.toFixed(2)}</td>
                                </tr>
                            `;
    }).join("")}
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


export const openVyapariVakkalInNewTab = (VyapariVakkal, dates, departmentname) => {
    const htmlContent = `
    <html>
        <head>
            <meta charset="UTF-8">
            <title>व्यापारी वककल अहवाल</title>
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
                    width: 90%;
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

                .btn {
                    padding: 10px 20px;
                    background: #007bff;
                    color: #fff;
                    border: none;
                    border-radius: 5px;
                    cursor: pointer;
                }

                @media print {
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

                    @page {
                        margin: 20mm;
                    }

                    .btn {
                        display: none;
                    }
                }
            </style>
        </head>
        <body>
            <div class="report-container">
                <h2>व्यापारी वककल अहवाल</h2>

                <div class="report-header">
                    <div style="flex: 1;"></div>
                    <div class="report-title">${departmentname || "जय मातादी मार्केट कमिटी"}</div>
                    <div class="report-dates">
                        ${dates.Startdate || "-"} ते ${dates.Enddate || "-"}
                    </div>
                </div>

                <hr />

                <table>
                    <thead>
                        <tr>
                            <th>दिनांक</th>
                            <th>बिल क्रमांक</th>
                            <th>शेतकरी</th>
                            <th>नग</th>
                            <th>वजन</th>
                            <th>वजन कमी जास्त</th>
                            <th>भाव</th>
                            <th>व्यापारी</th>
                        
                        </tr>
                    </thead>
                    <tbody>
                        ${VyapariVakkal.map(item => `
                            <tr>
                                <td>${item.date || ""}</td>
                                <td>${item.billno || ""}</td>
                                <td>${item.farmername || ""}</td>
                                <td>${item.jaali || ""}</td>
                                <td>${item.weightless || ""}</td>
                                <td>${item.weight || ""}</td>
                                <td>${item.rate || ""}</td>
                                <td>${item.vyapariname || ""}</td>
                               
                            </tr>
                        `).join("")}
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



