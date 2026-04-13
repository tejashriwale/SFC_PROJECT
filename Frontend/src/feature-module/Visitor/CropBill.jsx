
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
import { ArrowLeft, ChevronUp, Edit, PlusCircle, RotateCcw, Trash2, } from "feather-icons-react/build/IconComponents";
import { getUserData } from "../../Context/UserData";
import { convertToISODate } from "../../core/json/custom";
import ExcelJS from "exceljs";
import { saveAs } from "file-saver";
import { jsPDF } from 'jspdf';
import autoTable from 'jspdf-autotable';
import AddCropBill from "./AddCropBill";
import BillReceipt from "./BillReceipt";
import { openFarmerLedgerReportInNewTab } from "../Reports/FarmerLedgerReport";

const CropBill = () => {
    const [selectedData, setSelectedData] = useState({ cropid: null, cdaid: null });
    const [trandata, settrandata] = useState({ faid: null, vdaid: null });
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const data = useSelector((state) => state.toggle_header);
    const route = all_routes;
    const { isAuthenticated, userdetail } = getUserData();
    if (isAuthenticated == true) {
        console.log("user", userdetail);
        console.log("getUserData", getUserData);
    }
    const handleProceed = (cropid, faid, vdaid) => {
        setSelectedData({ cropid: cropid, cdaid: null });
        settrandata({ faid: faid, vdaid: vdaid });
    };

    const [farmeName, setfarmeName] = useState([]);
    const [Date, setDate] = useState([]);
    const [MoblieNo, setMoblieNo] = useState([]);
    const [tabledata, settabledata] = useState([]);
    const [Crop, setCrop] = useState([]);
    const [billno, setbillno] = useState([]);
    useEffect(() => {
        if (selectedData.cropid, selectedData.cdaid) {
            fetchbill();
        }
    }, [selectedData.cropid, selectedData.cdaid]);



    useEffect(() => {
        // Function to forcibly remove all modal remnants
        const cleanupModal = () => {
            // Remove the 'modal-open' class from the body
            document.body.classList.remove("modal-open");
            document.body.style.overflow = "";
            document.body.style.paddingRight = "";

            // Remove any leftover backdrop elements created by Bootstrap
            const backdrops = document.querySelectorAll(".modal-backdrop");
            backdrops.forEach(backdrop => backdrop.remove());

            // Also ensure the temporary landscape class is gone on navigation/unmount
            document.body.classList.remove('force-landscape-view');
        };

        // Cleanup runs when the component unmounts (i.e., when navigating away via back/forward)
        return () => {
            cleanupModal();
        };
    }, [location]);



    const fetchbill = async () => {
        try {
            const payload = {
                cropid: selectedData.cropid,
                companyid: userdetail?.companyID || "",
                deptid: userdetail?.departmentID || "",
            };
            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };
            const response = await axios.post(`${baseUrl.Url}/backend/api/GET_CropInvertBill`, payload, { headers });
            if (response.status !== 200) throw new Error("Failed to fetch vendor data");
            console.log("Farmer bill", response.data);
            if (Array.isArray(response.data) && response.data.length > 0) {
                const data = response.data[0];
                setfarmeName(data.farmername);
                setDate(data.date);
                setMoblieNo(data.contactno);
                settabledata(response.data);
                setCrop(data.ctype);
                setbillno(data.billno)
            } else {
                console.warn("GET_CropInvertBill returned empty or invalid data:", response.data);
            }
        } catch (error) {
            console.error("Error fetching Bill data:", error);
        }
    };
    const totalAmount = tabledata.reduce((sum, row) => {
        const rate = parseFloat(row.rate || 0);
        const weight = parseFloat(row.weight || 0);
        return sum + (rate * weight);
    }, 0);


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


    //     const handlePrint = () => {
    //         const printContents = document.getElementById("printableArea2").innerHTML;
    //         const printWindow = window.open("", "", "height=800,width=1000");
    //         printWindow.document.write(`
    //     <html>
    //       <head>
    //         <title>Print Bill</title>
    //         <style>
    //           body { font-family: Arial, sans-serif; padding: 20px; }
    //           table { width: 100%; border-collapse: collapse; }
    //           th, td { padding: 8px; border: 1px solid #ccc; text-align: center; }
    //           th { background-color: #add8e6; }
    //           h3, h6 { margin: 10px 0; }
    //         </style>
    //       </head>
    //       <body>
    //         ${printContents}
    //       </body>
    //     </html>
    //   `);
    //         printWindow.document.close();
    //         printWindow.focus();
    //         printWindow.print();
    //         printWindow.close();
    //     };
    const [CropData, setCropData] = useState([]);

    useEffect(() => {
        const fetchBillData = async () => {
            try {
                const payload = {
                    "cropid": "%",
                    "companyid": userdetail?.companyID ? userdetail.companyID : "",
                    "deptid": userdetail?.departmentID ? userdetail.departmentID : "",
                }
                const headers = {
                    "Content-Type": "application/json",
                    Accept: "*/*",
                };
                axios({
                    method: "POST",
                    url: baseUrl.Url + "/backend/api/GET_CropBill",
                    data: JSON.stringify(payload),
                    headers: headers,
                })
                    .then((response) => {
                        if (response.status != 200) throw new Error("Failed to send otp");
                        console.log("response", response.data);
                        const DATA = response.data;
                        setCropData(DATA);
                    })
            } catch (error) {
                console.error("Error fetching HSN data:", error);
            }
        };
        fetchBillData();
    }, []);

    const generateFarmerLedgerReport = async (faid, cropid, variety) => {
        try {
            // Show data for the specific farmer and crop
            const payload = {
                maid: faid || '%',
                cropid: cropid || '%',
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
                Swal.fire('No Data', 'No ledger data found for this farmer and crop.', 'info');
                return;
            }

            // Filter by variety if specified
            if (variety && variety.trim() !== '') {
                ledgerData = ledgerData.filter(item =>
                    (item.cvariety || item.CVARIETY || '') === variety
                );
            }

            if (ledgerData.length === 0) {
                Swal.fire('No Data', 'No ledger data found for this variety.', 'info');
                return;
            }

            // Get farmer and crop names from the data
            const farmerName = ledgerData.length > 0 ? (ledgerData[0].fname || ledgerData[0].FNAME) : 'Farmer';
            const cropName = ledgerData.length > 0 ? (ledgerData[0].cropname || ledgerData[0].CROPNAME) : 'Crop';
            const varietyName = variety || 'All Varieties';

            // Open report in new tab with specific farmer, crop, and variety
            openFarmerLedgerReportInNewTab(ledgerData, {
                farmerName: farmerName,
                cropName: `${cropName} - ${varietyName}`
            });

        } catch (error) {
            console.error("Error generating Farmer Ledger report:", error);
            Swal.fire('Error', 'Failed to generate report: ' + error.message, 'error');
        }
    };

    const renderproceeTooltip = (props) => (
        <Tooltip id="Edit-tooltip" {...props}>
            Proceed
        </Tooltip>
    );
    const openModal = (cropid, cdaid) => {
        setSelectedData({ cropid, cdaid })
        setTimeout(() => {
            setSelectedData(cropid, cdaid);
        }, 10);
        console.log("setSelectedDatasetSelectedDatasetSelectedData", cropid, cdaid)
    }
    const columns = [
        {
            title: "Farmer Name",
            dataIndex: "farmername",
        },
        {
            title: "Contact No.",
            dataIndex: "phonenumber",
        },
        {
            title: "Crop Variety",
            dataIndex: "cvariety",
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
            dataIndex: "Action",
            align: "center",
            render: (_, record) => (
                <div className="action-table-data">
                    <div className="edit-delete-action">
                        <OverlayTrigger
                            placement="top"
                            overlay={renderproceeTooltip}
                        >
                            <Link className="me-2 p-2"
                                to="#"
                                onClick={() => {
                                    handleProceed(record.cropid, record.faid, record.vdaid);
                                    const modal = document.getElementById("add-units-category");
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
                                // onClick={() => handleProceed(record.cropid, record.faid, record.vdaid)}
                                data-bs-toggle="modal"
                                // data-bs-target="#add-units-category"
                                style={{ color: 'green' }}>
                                <i data-feather="arrow-right-circle" className="feather-arrow-right-circle"></i>
                            </Link>
                        </OverlayTrigger>
                        <OverlayTrigger
                            placement="top"
                            overlay={<Tooltip id="delete-tooltip me-2">View</Tooltip>}>
                            <Link
                                className="me-2 p-2"
                                to="#" data-bs-toggle="modal"
                                data-bs-target="#print-receipt2"
                                style={{ color: 'orange' }}
                                // onClick={() => openModal(record.cropid, record.cdaid)}
                                onClick={() => {
                                    // 1. Fetch data and open the modal
                                    openModal(record.cropid, record.cdaid);

                                    // 2. Add the temporary class to the body for styling
                                    document.body.classList.add('force-landscape-view');

                                    // 3. Optional: Add an event listener to clean up the class when the modal closes
                                    const modalElement = document.getElementById('print-receipt2');
                                    if (modalElement) {
                                        // Ensure the cleanup happens ONLY after the modal is hidden
                                        modalElement.addEventListener('hidden.bs.modal', function handler() {
                                            document.body.classList.remove('force-landscape-view');
                                            modalElement.removeEventListener('hidden.bs.modal', handler);
                                        }, { once: true });
                                    }
                                }}



                            >
                                <i data-feather="eye" className="feather-eye"></i>
                            </Link>
                        </OverlayTrigger>
                        <OverlayTrigger
                            placement="top"
                            overlay={<Tooltip id="report-tooltip">Farmer Ledger</Tooltip>}>
                            <Link
                                className="me-2 p-2"
                                to="#"
                                style={{ color: 'blue' }}
                                onClick={() => generateFarmerLedgerReport(record.faid, record.cropid, record.cvariety)}
                            >
                                <i data-feather="file-text" className="feather-file-text"></i>
                            </Link>
                        </OverlayTrigger>
                    </div>
                </div>
            ),
        },
    ];

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
            const worksheet = workbook.addWorksheet("Crop Bill(Report)");
            const headingRow = worksheet.addRow(["Crop Bill(Report)"]);
            headingRow.getCell(1).font = { bold: true, size: 16 };
            headingRow.getCell(1).alignment = { horizontal: "center", vertical: "middle" };
            worksheet.mergeCells("A1:E1");
            const headers = ["Farmer Name", "Contact No.", "Crop Variety", "Crop Area", "Rate"];
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
            CropData.forEach(({ farmername, phonenumber, cvariety, carea, rate }) => {
                const row = worksheet.addRow([farmername, phonenumber, cvariety, carea, rate]);
                row.eachCell((cell) => {
                    cell.alignment = { horizontal: "center", vertical: "middle" };
                });
            });
            const buffer = await workbook.xlsx.writeBuffer();
            const data = new Blob([buffer], { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });
            saveAs(data, "Crop Bill(Report).xlsx");
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
        const title = "Crop Bill Report";
        const titleWidth = doc.getTextWidth(title);
        const borderMargin = 10;
        doc.rect(borderMargin, borderMargin, pageWidth - borderMargin * 2, pageHeight - borderMargin * 2);
        doc.text(title, (pageWidth - titleWidth) / 2, 20);
        doc.setLineWidth(0.5);
        doc.line((pageWidth - titleWidth) / 2, 22, (pageWidth + titleWidth) / 2, 22);
        doc.setFontSize(13);
        doc.setFont("Helvetica", "normal");
        let yPosition = 15;
        const tableColumn = ["Farmer Name", "Contact No.", "Crop Variety", "Crop Area", "Rate"];
        const tableRows = CropData.map(item => [
            item.farmername,
            item.phonenumber,
            item.cvariety,
            item.carea,
            item.rate
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
        doc.save("Crop Bill (Report).pdf");
    };


    const handleBack = () => {
        const from = location.state?.from;

        if (from == "MasterIndex") {
            navigate("/MasterIndex");
        } else if (from == "CropBillIndex") {
            navigate("/CropBillIndex");
        } else {
            navigate(-1); // fallback to last page
        }
    };




    // const handleBack = () => {
    //     const from = location.state?.from;

    //     if (from === "MasterIndex") {
    //         navigate("/MasterIndex");
    //     } else if (from === "CropBillIndex") {
    //         navigate("/CropBillIndex");
    //     } else if (from) {
    //         navigate(from); // manually go to the previous route
    //     } else {
    //         navigate("/"); // fallback to home or a default route
    //     }
    // };

    return (
        <div className="page-wrapper">
            <div className="content">
                <div className="page-header">
                    <div className="add-item d-flex">
                        <div className="page-title">
                            <h3>Crop Bill</h3>
                            <h6>Crop Bill Management</h6>
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
                                <Link data-bs-toggle="tooltip" data-bs-placement="top"
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
                    </div>
                    <div className="page-btn">
                        {/* <Link to={route.CropBillIndex} className="btn btn-secondary">
                            <ArrowLeft className="me-2" />
                            Back
                        </Link> */}
                        <button onClick={handleBack} className="btn btn-secondary">
                            <ArrowLeft className="me-2" />
                            Back
                        </button>
                    </div>
                </div>
                {/* <div className="col-lg-12">
                    <div className="text-end">
                        <div className="row justify-content-end">
                            <div className="col-12 col-md-1 mb-2 order-2 order-md-1">
                                <button
                                    type="button"
                                    className="btn btn-submit w-100"
                                    data-bs-toggle="modal"
                                    data-bs-target="#print-receipt2"
                                >
                                    Print Bill
                                </button>
                            </div>
                        </div>
                    </div>
                </div> */}
                <div className="search-container mb-3">
                    <div className="row">
                        <div className="col-lg-6 col-12 ms-auto">
                            <div className="d-flex align-items-center gap-2">

                                {/* Search Input */}
                                <input
                                    type="text"
                                    className="form-control"
                                    placeholder="Search"
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
                                dataSource={CropData.filter((item, index, self) =>
                                    // Show unique combinations of farmer + crop + variety
                                    index === self.findIndex((t) =>
                                        t.faid === item.faid &&
                                        t.cropid === item.cropid &&
                                        t.cvariety === item.cvariety
                                    )
                                )}
                                scroll={{ x: 'max-content' }} // 👈 Enables horizontal scroll on small screens
                                pagination={{ pageSize: 10 }} // Optional: adds pagination
                            />
                        </div>
                    </div>
                </div>

                <Brand />

                <style>
                    {`
    @media only screen and (max-width: 768px) {
      body.force-landscape-view {
        overflow: hidden !important;
        position: fixed;
        width: 100%;
        height: 100%;
      }

      /* Modal dialog should fill screen */
      body.force-landscape-view #print-receipt2 .modal-dialog {
        width: 100vh !important;
        height: 100vw !important;
        margin: 0 auto !important;
        display: flex !important;
        align-items: center;
        justify-content: center;
        overflow: hidden !important;
      }

      /* Rotated modal content */
      body.force-landscape-view #print-receipt2 .modal-content {
        transform: rotate(90deg);
        transform-origin: center center;
        width: 100vh !important;
        min-width: 650px !important;  /* 👈 Important: give more width for headers */
        height: 100vw !important;
        overflow-x: auto !important;   /* 👈 Enable horizontal scroll if needed */
        overflow-y: auto !important;
            margin-right: 25vh;             
        background: white !important;
        border-radius: 0 !important;
        box-shadow: none !important;
        display: block;
        padding: 0;
      }

      /* Make sure tables inside modal don't collapse */
      body.force-landscape-view #print-receipt2 table {
        width: max-content !important;
        min-width: 100% !important;
      }
    }
  `}
                </style>


                <style>
                    {`
                        @media print {
                            body * {
                                visibility: hidden!important;
                            }

                        #print-receipt2,
                        #print-receipt2 * {
                            visibility: visible !important;
                        }

                        /* Ensure the print container takes full page */
                        #print-receipt2 {
                            position: absolute !important;
                            left: 0 !important;
                            top: 0 !important;
                            margin: 0 auto !important;
                            width: 148mm !important;   /* A5 width */
                            height: 210mm !important;  /* A5 height */
                            padding: 10mm !important;  /* inner spacing */
                            background: white !important;
                            overflow: visible !important;
                        }

                        /* Make sure content fits inside A5 */
                        #print-receipt2 .modal-dialog {
                            max - width: 100% !important;
                            width: 100% !important;
                            height: 100% !important;
                            margin: 0 !important;
                        }

                        #print-receipt2 .modal-content {
                            border: none !important;
                            box-shadow: none !important;
                            height: 100% !important;
                        }

                        #print-receipt2 .modal-body {
                            padding: 0 !important;
                            font-size: 12px !important; /* Adjust font size if text overflows */
                            height: auto !important;
                            overflow: visible !important;
                        }

                        /* Remove modal close button on print */
                        #print-receipt2 .close {
                            display: none !important;
                        }

                        /* Table formatting */
                        #print-receipt2 table {
                            page -break-inside: auto;
                        }
                        #print-receipt2 tr {
                            page -break-inside: avoid;
                            page-break-after: auto;
                        }
                        }
                        
                    `}
                </style>
                <BillReceipt
                    modalId="print-receipt2"
                    billNo={billno}
                    date={Date}
                    farmerName={farmeName}
                    mobileNo={MoblieNo}
                    vehNo={tabledata.length > 0 ? (tabledata[0].vehno || '') : ''}
                    tableData={tabledata}
                    totalAmount={totalAmount}
                />
            </div>
            <AddCropBill CROPID={selectedData.cropid} cdaid={selectedData.cdaid} MAID={trandata.faid} VDAID={trandata.vdaid} />
        </div>


    );
};

export default CropBill;
