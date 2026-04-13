import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Link, useNavigate } from "react-router-dom";
import ImageWithBasePath from "../../core/img/imagewithbasebath";
import Brand from "../../core/modals/inventory/brand";
import withReactContent from "sweetalert2-react-content";
import Swal from "sweetalert2";
import { all_routes } from "../../Router/all_routes";
import { OverlayTrigger, Tooltip } from "react-bootstrap";
import Table from "../../core/pagination/datatable";
import { setToogleHeader } from "../../core/redux/action";
import axios from "axios";
import { baseUrl } from "../../core/json/custom";
import {
    ArrowLeft,
    ChevronUp,
    Edit,
    PlusCircle,
    RotateCcw,
    Trash2,
} from "feather-icons-react/build/IconComponents";
import { getUserData } from "../../Context/UserData";
import ExcelJS from "exceljs";
import { saveAs } from "file-saver";
import { jsPDF } from 'jspdf';
import autoTable from 'jspdf-autotable';
import CropTransportInvoiceProceed from "./CropTransportInvoiceProceed";
const CropTransportInvoice = () => {
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const data = useSelector((state) => state.toggle_header);
    const route = all_routes;
    const [ItemData, setItemData] = useState([]);
    const { isAuthenticated, userdetail } = getUserData();
    if (isAuthenticated == true) {
        console.log("user", userdetail);
        console.log("getUserData", getUserData);
    }
    useEffect(() => {
        const handleShortcut = (e) => {
            // if (e.ctrlKey && e.key === 'a') {
            //     e.preventDefault();
            //     navigate(route.AddItem);
            // }
            if (e.ctrlKey && e.key === 'e') {
                e.preventDefault();
                navigate(route.MasterIndex);
            }
        };
        window.addEventListener('keydown', handleShortcut);
        return () => {
            window.removeEventListener('keydown', handleShortcut);
        };
    }, [navigate]);
    const [MasterList, setMasterList] = useState([]);
    const fetchMasterList = async () => {
        try {
            const payload = {
                "slvmid": "%",
                "keyword": "%",
                "companyid": userdetail?.companyID ? userdetail.companyID : "",
                "deptid": userdetail?.departmentID ? userdetail.departmentID : ""
            }
            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };
            axios({
                method: "POST",
                url: baseUrl.Url + "/backend/api/GET_StockLoadingVehicleMaster",
                data: JSON.stringify(payload),
                headers: headers,
            })
                .then((response) => {
                    if (response.status != 200) throw new Error("Failed to send otp");
                    console.log("response", response.data);
                    const DATA = response.data;
                    setMasterList(DATA);
                })
        } catch (error) {
            console.error("Error fetching HSN data:", error);
        }
    };
    useEffect(() => {
        fetchMasterList();
    }, []);
    const OnReloadData = () => {
        try {
            const payload = {
                "pkid": "%"
                , "keyword": "%"
                , "companyid": userdetail?.companyID ? userdetail.companyID : "",
                "deptid": userdetail?.departmentID ? userdetail.departmentID : ""
            }
            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };
            axios({
                method: "POST",
                url: baseUrl.Url + "/backend/api/_GET_ProductMaster_/getByID",
                data: JSON.stringify(payload),
                headers: headers,
            })
                .then((response) => {
                    if (response.status != 200) throw new Error("Failed to send otp");
                    console.log("response", response.data);
                    const DATA = response.data;
                    setItemData(DATA);
                })
        } catch (error) {
            console.error("Error fetching HSN data:", error);
        }
    }
    const [selectedData, setSelectedData] = useState({ slvmid: null, slvdid: null, tradeR1: null, status: null });
    const onEditClick = (slvmid, slvdid, tradeR1, status) => {
        setSelectedData({
            slvmid: slvmid,
            slvdid: slvdid,
            tradeR1: tradeR1,
            refreshKey: Date.now(),
            status: status
        });
    };
    const [searchQuery, setSearchQuery] = useState("");
    const handleSearch = (event) => {
        setSearchQuery(event.target.value);
        try {
            const payload = {
                "slvmid": "%",
                "keyword": event.target.value
                , "companyid": userdetail?.companyID ? userdetail.companyID : "",
                "deptid": userdetail?.departmentID ? userdetail.departmentID : ""
            }
            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };
            axios({
                method: "POST",
                url: baseUrl.Url + "/backend/api/GET_StockLoadingVehicleMaster/_Search",
                data: JSON.stringify(payload),
                headers: headers,
            })
                .then((response) => {
                    if (response.status != 200) throw new Error("Failed to send  data"); console.log("response", response.data);
                    setMasterList(response.data);
                })
        } catch (error) {
            console.error("Error while searching Product data:", error);
        }
    };
    const renderproceeTooltip = (props) => (
        <Tooltip id="Edit-tooltip" {...props}>
            Proceed
        </Tooltip>
    );
    const renderReportTooltip = (props) => (
        <Tooltip id="Report-tooltip" {...props}>
            Customer Report
        </Tooltip>
    );

    const formatDateToShortMonth = (dateStr) => {
        const months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
        const [year, month, day] = dateStr.split("-");
        return `${parseInt(day, 10)} ${months[parseInt(month, 10) - 1]} ${year}`;
    };

    const openCustomerReport = async (record) => {
        try {
            const headers = { "Content-Type": "application/json", Accept: "*/*" };
            const response = await axios.post(`${baseUrl.Url}/backend/api/GET_CustomerLedger`, {
                companyid: userdetail?.companyID || "",
                deptid: userdetail?.departmentID || "",
                refaid: record.slvmid,
                maid: record.tradeR1,
                sdate: "1 Jan 2000",
                edate: "31 Dec 2099"
            }, { headers });

            const filteredRows = response.data;

            let totalDebit = 0;
            let totalCredit = 0;
            let totalAdvance = 0;
            let tableRows = '';
            const vehicleBillShown = {};

            filteredRows.forEach((r) => {
                const debit = parseFloat(r.totsamt) || 0;
                const credit = parseFloat(r.pamt) || 0;
                const advance = parseFloat(r.advamt) || 0;
                const balance = parseFloat(r.cbal) || 0;
                const vehicleKey = (r.vehicleno || '') + '_' + (r.ccompanyname || '');

                if (debit > 0 && !vehicleBillShown[vehicleKey]) {
                    vehicleBillShown[vehicleKey] = true;
                    totalDebit += debit;
                    tableRows += `
                    <tr>
                        <td>${r.trndt || ''}</td>
                        <td>${r.ccompanyname || ''}</td>
                        <td>${r.vehicleno || ''}</td>
                        <td>Bill Amount</td>
                        <td style="text-align: right;">${debit.toFixed(2)}</td>
                        <td style="text-align: right;">0.00</td>
                        <td style="text-align: right;">${(balance + credit + advance).toFixed(2)}</td>
                    </tr>`;
                }

                if (advance > 0) {
                    totalAdvance += advance;
                    tableRows += `
                    <tr>
                        <td>${r.trndt || ''}</td>
                        <td>${r.ccompanyname || ''}</td>
                        <td>${r.vehicleno || ''}</td>
                        <td>Advance</td>
                        <td style="text-align: right;">0.00</td>
                        <td style="text-align: right;">${advance.toFixed(2)}</td>
                        <td style="text-align: right;">${(balance + credit).toFixed(2)}</td>
                    </tr>`;
                }

                if (credit > 0) {
                    totalCredit += credit;
                    tableRows += `
                    <tr>
                        <td>${r.trndt || ''}</td>
                        <td>${r.ccompanyname || ''}</td>
                        <td>${r.vehicleno || ''}</td>
                        <td>Payment</td>
                        <td style="text-align: right;">0.00</td>
                        <td style="text-align: right;">${credit.toFixed(2)}</td>
                        <td style="text-align: right;">${balance.toFixed(2)}</td>
                    </tr>`;
                }
            });

            const finalBalance = totalDebit - totalCredit - totalAdvance;

            const totalRow = `
                <tr style="font-weight: bold; background-color: #e8e8e8;">
                    <td colspan="4" style="text-align: right; font-size: 14px;">Total:</td>
                    <td style="text-align: right;">${totalDebit.toFixed(2)}</td>
                    <td style="text-align: right;">${(totalCredit + totalAdvance).toFixed(2)}</td>
                    <td style="text-align: right;">${finalBalance.toFixed(2)}</td>
                </tr>
            `;

            const htmlContent = `
                <html>
                    <head>
                        <meta charset="UTF-8">
                        <title>Customer Ledger Report</title>
                        <style>
                            @page { size: landscape; margin: 10mm; }
                            body { font-family: Arial, sans-serif; margin: 0; padding: 20px; background-color: #fff; }
                            .report-container { width: 100%; max-width: 1400px; margin: 0 auto; }
                            .company-name { text-align: center; font-size: 28px; font-weight: bold; margin-bottom: 10px; text-transform: uppercase; }
                            .report-title { text-align: center; font-size: 22px; font-weight: bold; margin-bottom: 15px; }
                            .filter-info { text-align: center; font-size: 14px; margin-bottom: 20px; }
                            table { width: 100%; border-collapse: collapse; margin-top: 5px; font-size: 13px; }
                            th, td { border: 1.5px solid #444; padding: 10px 8px; text-align: center; }
                            th { background-color: #d1d1d1; font-weight: bold; color: #000; }
                            tr:hover { background-color: #f5f5f5; }
                            .btn-container { text-align: center; margin: 20px 0; }
                            .btn { padding: 12px 30px; background: #4a90e2; color: #fff; border: none; border-radius: 5px; cursor: pointer; font-size: 16px; margin: 0 10px; }
                            .btn:hover { background: #357abd; }
                            @media print { .btn-container { display: none; } }
                        </style>
                    </head>
                    <body>
                        <div class="report-container">
                            <div class="company-name">${userdetail?.departmentname || 'SHREE SAI FRUITS COMPANY'}</div>
                            <div class="report-title">Customer Ledger Report</div>
                            <div class="filter-info">
                                Customer: ${record.trader} | Vehicle: ${record.vehicleno} | Generated On: ${new Date().toLocaleString()}
                            </div>
                            <table>
                                <thead>
                                    <tr>
                                        <th>Date</th>
                                        <th>Customer Name</th>
                                        <th>Gadi No</th>
                                        <th>Transaction Type</th>
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
                                <button class="btn" onclick="window.close()">Close</button>
                            </div>
                        </div>
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
        } catch (error) {
            console.error("Error fetching customer ledger:", error);
            Swal.fire({ icon: "error", title: "Error", text: "Failed to fetch customer report." });
        }
    };
    const columns = [
        {
            title: "Date",
            dataIndex: "ldate",
            sorter: (a, b) => a.ldate.length - b.ldate.length,
        },
        {
            title: "Crop",
            dataIndex: "crop",
            sorter: (a, b) => a.crop.length - b.crop.length,
        },
        {
            title: "Customer Name",
            dataIndex: "trader",
            sorter: (a, b) => a.trader.length - b.trader.length,
        },
        {
            title: "Vehicle Number",
            dataIndex: "vehicleno",
            sorter: (a, b) => a.vehicleno.length - b.vehicleno.length,
        },
        {
            title: "Caret Count",
            dataIndex: "caretcount",
            sorter: (a, b) => a.caretcount.length - b.caretcount.length,
        },
        {
            title: "Status",
            dataIndex: "billstatus",
            sorter: (a, b) => a.billstatus.length - b.billstatus.length,
            render: (text) => {
                let color = text === "COMPLETED" ? "green" : "orange";
                return (
                    <button
                        style={{
                            backgroundColor: color,
                            color: "white",
                            border: "none",
                            borderRadius: "6px",
                            padding: "4px 10px",
                            fontSize: "12px",
                            cursor: "default"
                        }}
                    >
                        {text}
                    </button>
                );
            }
        },
        {
            title: "Action",
            dataIndex: "action",
            render: (_, record) => (
                <div className="action-table-data">
                    <div className="edit-delete-action">
                        <OverlayTrigger
                            placement="top"
                            overlay={renderproceeTooltip}
                        >
                            <Link className="me-2 p-2"
                                to="#"
                                // onClick={() => { onEditClick(record.slvmid, record.slvdid, record.tradeR1, record.billstatus) }}
                                // data-bs-toggle="modal"
                                // data-bs-target="#AddTransportProcced"

                                onClick={(e) => {
                                    e.preventDefault();
                                    onEditClick(record.slvmid, record.slvdid, record.tradeR1, record.billstatus);


                                    const modal = document.getElementById("AddTransportProcced");

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

                                style={{ color: 'green' }}>
                                <i data-feather="arrow-right-circle" className="feather-arrow-right-circle"></i>
                            </Link>
                        </OverlayTrigger>
                        <OverlayTrigger
                            placement="top"
                            overlay={renderReportTooltip}
                        >
                            <Link className="me-2 p-2"
                                to="#"
                                onClick={(e) => {
                                    e.preventDefault();
                                    openCustomerReport(record);
                                }}
                                style={{ color: '#4a90e2' }}>
                                <i data-feather="file-text" className="feather-file-text"></i>
                            </Link>
                        </OverlayTrigger>
                    </div>
                </div>
            ),
            sorter: (a, b) => a.createdby.length - b.createdby.length,
        },
    ];
    const MySwal = withReactContent(Swal);
    const handleDelete = async (slvmid) => {
        try {
            const payload = {
                "slvmid": slvmid
                , "companyid": userdetail?.companyID ? userdetail.companyID : "",
                "deptid": userdetail?.departmentID ? userdetail.departmentID : ""
            }
            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };
            axios({
                method: "POST",
                url: baseUrl.Url + "/backend/api/SP_DeleteStockLoadingVehicleMaster",
                data: JSON.stringify(payload),
                headers: headers,
            })
            Swal.fire({
                icon: "success",
                title: "Deleted!",
                text: "Your file has been deleted.",
                confirmButtonText: "OK",
            });
            try {
                const payload = {
                    "slvmid": "%",
                    "keyword": "%",
                    "companyid": userdetail?.companyID ? userdetail.companyID : "",
                    "deptid": userdetail?.departmentID ? userdetail.departmentID : ""
                }
                const headers = {
                    "Content-Type": "application/json",
                    Accept: "*/*",
                };
                axios({
                    method: "POST",
                    url: baseUrl.Url + "/backend/api/GET_StockLoadingVehicleMaster",
                    data: JSON.stringify(payload),
                    headers: headers,
                })
                    .then((response) => {
                        if (response.status != 200) throw new Error("Failed to send otp");
                        console.log("response", response.data);
                        const DATA = response.data;
                        setMasterList(DATA);
                    })
            } catch (error) {
                console.error("Error fetching HSN data:", error);
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
    const showConfirmationAlert = (slvmid) => {
        MySwal.fire({
            title: "Are you sure?",
            text: "You won't be able to revert this!",
            showCancelButton: true,
            confirmButtonColor: "#00ff00",
            confirmButtonText: "Yes, delete it!",
            cancelButtonColor: "#ff0000",
            cancelButtonText: "Cancel",
        }).then((result) => {
            if (result.isConfirmed) {
                handleDelete(slvmid)
            } else {
                MySwal.close();
            }
        });
    };
    const dataSource = [];
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
        <Tooltip id="refresh-tooltip" {...props}>
            Collapse
        </Tooltip>
    );
    const downloadExcel = async () => {
        try {
            const workbook = new ExcelJS.Workbook();
            const worksheet = workbook.addWorksheet("Product (Report)");
            const headingRow = worksheet.addRow(["Employee Type (Report)"]);
            headingRow.getCell(1).font = { bold: true, size: 16 };
            headingRow.getCell(1).alignment = { horizontal: "center", vertical: "middle" };
            worksheet.mergeCells("A1:E1");
            const headers = ["Product Name", "category", "Sub-Category", "Type of Sale", "Hsn Code"];
            const headerRow = worksheet.addRow(headers);
            headerRow.eachCell((cell) => {
                cell.font = { bold: true, color: { argb: "FFFFFF" } };
                cell.fill = { type: "pattern", pattern: "solid", fgColor: { argb: "808080" } };
                cell.alignment = { horizontal: "center", vertical: "middle" };
            });
            const columnWidths = [25, 25, 25, 25, 20];
            columnWidths.forEach((width, index) => {
                worksheet.getColumn(index + 1).width = width;
            });
            dataSource.forEach(({ pname, pcategory, psubcategory, ptypesofsales, phsn }) => {
                const row = worksheet.addRow([pname, pcategory, psubcategory, ptypesofsales, phsn]);
                row.eachCell((cell) => {
                    cell.alignment = { horizontal: "center", vertical: "middle" };
                });
            });
            const buffer = await workbook.xlsx.writeBuffer();
            const data = new Blob([buffer], { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });
            saveAs(data, "Product_(Report).xlsx");
        } catch (error) {
            console.error("Error generating the Excel file:", error);
        }
    };
    const downloadPDF = () => {
        const doc = new jsPDF();
        const pageWidth = doc.internal.pageSize.getWidth();
        const pageHeight = doc.internal.pageSize.getHeight();
        doc.setFontSize(12);
        doc.setFont("Helvetica", "bold");
        const title = "Product Report";
        const titleWidth = doc.getTextWidth(title);
        const borderMargin = 10;
        doc.rect(borderMargin, borderMargin, pageWidth - borderMargin * 2, pageHeight - borderMargin * 2);
        doc.text(title, (pageWidth - titleWidth) / 2, 20);
        doc.setLineWidth(0.5);
        doc.line((pageWidth - titleWidth) / 2, 22, (pageWidth + titleWidth) / 2, 22);
        doc.setFontSize(13);
        doc.setFont("Helvetica", "normal");
        let yPosition = 15;
        const tableColumn = ["Product Name", "category", "Sub-Category", "Type of Sale", "Hsn Code"];
        const tableRows = dataSource.map(item => [
            item.pname,
            item.pcategory,
            item.psubcategory,
            item.ptypesofsales,
            item.phsn
        ]);
        autoTable(doc, {
            startY: yPosition + 10,
            head: [tableColumn],
            body: tableRows,
            theme: 'grid',
            styles: { fontSize: 10, halign: "center", lineColor: [0, 0, 0], lineWidth: 0.20 },
            headStyles: { fillColor: [169, 169, 169], textColor: 0, fontStyle: "bold" },
            bodyStyles: { textColor: 0 },
        });
        doc.save("Product_(Report).pdf");
    };
    const [selectedData1, setSelectedData1] = useState({ slvmid: null, slvdid: null, refreshKey: null });
    const [refreshFlag, setRefreshFlag] = useState(false);
    const refreshData = () => {
        setRefreshFlag(prev => !prev);
        setSelectedData({ slvmid: null, slvdid: null, tradeR1: null, status: null })

    };

    useEffect(() => {
        setSelectedData1({ slvmid: null });
    }, [refreshFlag]);


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
    return (
        <div className="page-wrapper">
            <div className="content">
                <div className="page-header">
                    <div className="add-item d-flex">
                        <div className="page-title">
                            <h3>Crop Transport Vehicle Bill </h3>
                        </div>
                    </div>
                    <ul className="table-top-head">
                        <li>
                            <OverlayTrigger placement="top" overlay={renderTooltip} >
                                <Link onClick={downloadPDF}>
                                    <ImageWithBasePath src="assets/img/icons/pdf.svg" alt="img" />
                                </Link>
                            </OverlayTrigger>
                        </li>
                        <li>
                            <OverlayTrigger placement="top" overlay={renderExcelTooltip}>
                                <Link data-bs-toggle="tooltip" data-bs-placement="top" onClick={downloadExcel}>
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
                                <Link data-bs-toggle="tooltip" data-bs-placement="top" onClick={OnReloadData}>
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
                        {/* <Link to={route.CropBillIndex} className="btn btn-secondary">
                            <ArrowLeft className="me-2" />
                            Back to Index
                        </Link> */}
                        <button onClick={handleBack} className="btn btn-secondary">
                            <ArrowLeft className="me-2" />
                            Back
                        </button>
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
                <div
                    className="card table-list-card"
                    style={{
                        width: '100%',
                        backgroundColor: '#fff',
                        border: '1px solid #e0e0e0',
                        borderRadius: '8px',
                        boxShadow: '0 2px 4px rgba(0, 0, 0, 0.05)',
                    }}
                >
                    <div
                        className="card-body"
                        style={{
                            padding: '16px',
                        }}
                    >
                        <div
                            style={{
                                width: '100%',
                                overflowX: 'auto',
                                WebkitOverflowScrolling: 'touch',
                                backgroundColor: '#fff',
                                paddingBottom: '1rem',
                            }}
                        >
                            <Table
                                columns={columns}
                                dataSource={MasterList}
                                scroll={{ x: 'max-content' }} // 👈 Enables horizontal scrolling
                                pagination={{ pageSize: 10 }} // Optional, for cleaner UX
                                bordered
                            />
                        </div>
                    </div>
                </div>

                <Brand />
            </div>
            <CropTransportInvoiceProceed SLVMID={selectedData.slvmid} TREDER={selectedData.tradeR1}
                refreshKey={selectedData1.refreshKey}
                onRefresh={refreshData}
                STATUS={selectedData.status}
                onSaved={fetchMasterList} />
        </div>
    );
};
export default CropTransportInvoice;
