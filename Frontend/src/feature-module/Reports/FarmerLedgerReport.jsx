export const openFarmerLedgerReportInNewTab = (ledgerData, filterInfo, companyName = 'SHREE SAI FRUITS COMPANY') => {
    // Calculate totals
    const totalDebit = ledgerData.reduce((sum, item) => sum + (parseFloat(item.debit || item.DEBIT) || 0), 0);
    const totalCredit = ledgerData.reduce((sum, item) => sum + (parseFloat(item.credit || item.CREDIT) || 0), 0);
    const finalBalance = totalDebit - totalCredit;

    const tableRows = ledgerData.map(item => `
        <tr>
            <td>${item.trndt || item.TRNDT || ''}</td>
            <td>${item.billno || item.BILLNO || ''}</td>
            <td>${item.fname || item.FNAME || ''}</td>
            <td>${item.transactiontype || item.TRANSACTIONTYPE || ''}</td>
            <td>${item.cropname || item.CROPNAME || ''}</td>
            <td>${item.cvariety || item.CVARIETY || ''}</td>
            <td>${item.narr || item.NARR || ''}</td>
            <td style="text-align: right;">${parseFloat(item.debit || item.DEBIT || 0).toFixed(2)}</td>
            <td style="text-align: right;">${parseFloat(item.credit || item.CREDIT || 0).toFixed(2)}</td>
            <td style="text-align: right;">${parseFloat(item.balance || item.BALANCE || 0).toFixed(2)}</td>
        </tr>
    `).join('');

    const totalRow = `
        <tr style="font-weight: bold; background-color: #e8e8e8;">
            <td colspan="7" style="text-align: right; font-size: 14px;">Total:</td>
            <td style="text-align: right;">${totalDebit.toFixed(2)}</td>
            <td style="text-align: right;">${totalCredit.toFixed(2)}</td>
            <td style="text-align: right;">${finalBalance.toFixed(2)}</td>
        </tr>
    `;

    const htmlContent = `
    <html>
        <head>
            <meta charset="UTF-8" />
            <title>Farmer Ledger Report</title>
            <style>
                @page {
                    size: landscape;
                    margin: 10mm;
                }

                body {
                    font-family: Arial, sans-serif;
                    margin: 0;
                    padding: 20px;
                    background-color: #fff;
                }

                .report-container {
                    width: 100%;
                    max-width: 1400px;
                    margin: 0 auto;
                }

                .company-name {
                    text-align: center;
                    font-size: 28px;
                    font-weight: bold;
                    margin-bottom: 10px;
                    text-transform: uppercase;
                }

                .report-title {
                    text-align: center;
                    font-size: 22px;
                    font-weight: bold;
                    margin-bottom: 15px;
                }

                .filter-info {
                    text-align: center;
                    font-size: 15px;
                    margin-bottom: 20px;
                    background: linear-gradient(135deg, #f0f7f0, #e8f4fd);
                    padding: 12px 20px;
                    border-radius: 8px;
                    border-left: 4px solid #1a7a1a;
                    font-weight: 600;
                    color: #333;
                    letter-spacing: 0.3px;
                }

                .filter-info .label {
                    color: #555;
                    font-weight: 500;
                }

                .filter-info .value {
                    color: #1a5c1a;
                    font-weight: 700;
                }

                .filter-info .separator {
                    color: #999;
                    margin: 0 8px;
                }

                table {
                    width: 100%;
                    border-collapse: collapse;
                    margin-top: 5px;
                    font-size: 13px;
                }

                th, td {
                    border: 1.5px solid #444;
                    padding: 10px 8px;
                    text-align: center;
                }

                th {
                    background-color: #d1d1d1;
                    font-weight: bold;
                    color: #000;
                }

                tr:hover {
                    background-color: #f5f5f5;
                }

                .btn-container {
                    text-align: center;
                    margin: 20px 0;
                }

                .btn {
                    padding: 12px 30px;
                    background: #4a90e2;
                    color: #fff;
                    border: none;
                    border-radius: 5px;
                    cursor: pointer;
                    font-size: 16px;
                    margin: 0 10px;
                }

                .btn:hover {
                    background: #357abd;
                }

                @media print {
                    @page {
                        size: landscape;
                        margin: 5mm;
                    }

                    body {
                        padding: 0;
                        margin: 0;
                        background-color: #fff;
                    }

                    .btn-container {
                        display: none;
                    }

                    .report-container {
                        width: 100%;
                        max-width: none;
                    }

                    table {
                        font-size: 11px;
                        page-break-inside: auto;
                    }

                    tr {
                        page-break-inside: avoid;
                        page-break-after: auto;
                    }

                    th, td {
                        padding: 6px 4px;
                    }

                    .company-name {
                        font-size: 24px;
                    }

                    .report-title {
                        font-size: 18px;
                    }
                }
            </style>
        </head>
        <body>
            <div class="report-container">
                <div class="company-name">${companyName}</div>
                <div class="report-title">Farmer Ledger Report</div>
                <div class="filter-info">
                    <span class="label">Farmer:</span> <span class="value">${filterInfo.farmerName || 'All Farmers'}</span>
                    <span class="separator">|</span>
                    <span class="label">Crop:</span> <span class="value">${filterInfo.cropName || 'All Crops'}</span>
                    <span class="separator">|</span>
                    <span class="label">Generated On:</span> <span class="value">${new Date().toLocaleString()}</span>
                </div>

                <table>
                    <thead>
                        <tr>
                            <th>Date</th>
                            <th>Bill No</th>
                            <th>Farmer Name</th>
                            <th>Transaction Type</th>
                            <th>Crop Name</th>
                            <th>Variety</th>
                            <th>Narration</th>
                            <th>Debit</th>
                            <th>Credit</th>
                            <th>Balance</th>
                        </tr>
                    </thead>
                    <tbody>
                        ${tableRows}
                        ${totalRow}
                    </tbody>
                </table>

                <div class="btn-container">
                    <button class="btn" onclick="window.print()">Print</button>
                    <button class="btn" style="background:#198754;" onclick="downloadPDF()">Download PDF</button>
                    <button class="btn" style="background:#dc3545;" onclick="window.close()">Close</button>
                </div>
            </div>
            <script src="https://cdnjs.cloudflare.com/ajax/libs/html2canvas/1.4.1/html2canvas.min.js"></script>
            <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/2.5.1/jspdf.umd.min.js"></script>
            <script>
                function downloadPDF() {
                    var btnContainer = document.querySelector('.btn-container');
                    btnContainer.style.display = 'none';
                    var el = document.querySelector('.report-container');
                    html2canvas(el, { scale: 2, useCORS: true, backgroundColor: '#fff' }).then(function(canvas) {
                        var imgData = canvas.toDataURL('image/png');
                        var pdf = new jspdf.jsPDF('l', 'mm', 'a4');
                        var pageWidth = pdf.internal.pageSize.getWidth();
                        var pageHeight = pdf.internal.pageSize.getHeight();
                        var imgWidth = pageWidth - 10;
                        var imgHeight = (canvas.height * imgWidth) / canvas.width;

                        var y = 5;
                        if (imgHeight <= pageHeight - 10) {
                            pdf.addImage(imgData, 'PNG', 5, y, imgWidth, imgHeight);
                        } else {
                            // Multi-page
                            var remainingHeight = canvas.height;
                            var position = 0;
                            var pageCanvas, pageCtx, pageImgData;
                            var sliceHeight = Math.floor(canvas.width * (pageHeight - 10) / imgWidth);
                            while (remainingHeight > 0) {
                                pageCanvas = document.createElement('canvas');
                                pageCanvas.width = canvas.width;
                                pageCanvas.height = Math.min(sliceHeight, remainingHeight);
                                pageCtx = pageCanvas.getContext('2d');
                                pageCtx.drawImage(canvas, 0, position, canvas.width, pageCanvas.height, 0, 0, canvas.width, pageCanvas.height);
                                pageImgData = pageCanvas.toDataURL('image/png');
                                var sliceImgHeight = (pageCanvas.height * imgWidth) / canvas.width;
                                if (position > 0) pdf.addPage();
                                pdf.addImage(pageImgData, 'PNG', 5, 5, imgWidth, sliceImgHeight);
                                position += sliceHeight;
                                remainingHeight -= sliceHeight;
                            }
                        }
                        var farmerName = '${(filterInfo.farmerName || 'All').replace(/'/g, "\\'")}';
                        var billNos = '${[...new Set(ledgerData.map(i => i.billno || i.BILLNO).filter(Boolean))].join('_')}';
                        var fileName = farmerName + '_Bill_' + billNos + '_Ledger.pdf';
                        pdf.save(fileName.replace(/[^a-zA-Z0-9_\\-]/g, '_'));
                        btnContainer.style.display = '';
                    }).catch(function() { btnContainer.style.display = ''; });
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
