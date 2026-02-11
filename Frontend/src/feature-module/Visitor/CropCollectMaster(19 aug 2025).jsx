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
import ExcelJS from "exceljs";
import { saveAs } from "file-saver";
import { jsPDF } from 'jspdf';
import autoTable from 'jspdf-autotable';
import AddCropCollect from "./AddCropCollect";
const CropCollectMaster = () => {
    const [selectedData, setSelectedData] = useState({ shaid: null, vdaid: null, faid: null });
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const data = useSelector((state) => state.toggle_header);
    const route = all_routes;
    const { isAuthenticated, userdetail } = getUserData();
    if (isAuthenticated == true) {
        console.log("user", userdetail);
        console.log("getUserData", getUserData);
    }
    const handleProceed = (shaid, vdaid, faid) => {
        setSelectedData({ shaid: shaid, vdaid: vdaid, faid: faid });
    };
    useEffect(() => {
        const handleShortcut = (e) => {
            if (e.ctrlKey && e.key === 'a') {
                e.preventDefault();
                navigate(route.AddCropInvert);
            }
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
    const [CropData, setCropData] = useState([]);
    useEffect(() => {
        const fetchCropData = async () => {
            try {
                const payload = {
                    "shaid": "%",
                    "keyword": "%",
                    date: userdetail.APPDT
                }
                const headers = {
                    "Content-Type": "application/json",
                    Accept: "*/*",
                };
                axios({
                    method: "POST",
                    url: baseUrl.Url + "/backend/api/GET_CropInvertMaster",
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
        fetchCropData();
    }, []);
    const [searchQuery, setSearchQuery] = useState("");
    const handleSearch = (event) => {
        setSearchQuery(event.target.value);
        try {
            const payload =
            {
                "shaid": "%",
                "keyword": event.target.value,
                "date": userdetail?.APPDT || ""
            }
            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };
            axios({
                method: "POST",
                url: baseUrl.Url + "/backend/api/GET_CropInvertMaster/_Search",
                data: JSON.stringify(payload),
                headers: headers,
            })
                .then((response) => {
                    if (response.status != 200) throw new Error("Failed to send  data"); console.log("response", response.data);
                    setCropData(response.data);
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
    const columns = [
        {
            title: "Farmer Name",
            dataIndex: "farmername",
        },
        {
            title: "Contact No",
            dataIndex: "contactno",
        },
        {
            title: "Crop Name",
            dataIndex: "cropname",
        },
        {
            title: "Crop Area",
            dataIndex: "carea",
        },
        {
            title: "Expected Harvesting Date",
            dataIndex: "shdate",
        },
        {
            title: "Rate(₹)",
            dataIndex: "rate",
            sorter: (a, b) => a.rate.length - b.rate.length,
        },
        {
            title: "Action",
            dataIndex: "Action",
            render: (_, record) => (
                <div className="action-table-data">
                    <div className="edit-delete-action">
                        <OverlayTrigger
                            placement="top"
                            overlay={renderproceeTooltip}
                        >
                            <Link className="me-2 p-2"
                                to="#"
                                onClick={() => handleProceed(record.shaid, record.vdaid, record.faid)}
                                data-bs-toggle="modal"
                                data-bs-target="#add-units-category"
                                style={{ color: 'green' }}>
                                <i data-feather="arrow-right-circle" className="feather-arrow-right-circle"></i>
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
            const worksheet = workbook.addWorksheet("Crop Collection (Report)");
            const headingRow = worksheet.addRow(["Crop Collection (Report)"]);
            headingRow.getCell(1).font = { bold: true, size: 16 };
            headingRow.getCell(1).alignment = { horizontal: "center", vertical: "middle" };
            worksheet.mergeCells("A1:F1");
            const headers = ["Farmer Name", "Contact No", "Crop Name", "Crop Area", "Expected Harvesting Date", "Rate(₹)"];
            const headerRow = worksheet.addRow(headers);
            headerRow.eachCell((cell) => {
                cell.font = { bold: true, color: { argb: "FFFFFF" } };
                cell.fill = { type: "pattern", pattern: "solid", fgColor: { argb: "808080" } };
                cell.alignment = { horizontal: "center", vertical: "middle" };
            });
            const columnWidths = [25, 25, 25, 25, 20, 20];
            columnWidths.forEach((width, index) => {
                worksheet.getColumn(index + 1).width = width;
            });
            CropData.forEach(({ farmername, contactno, cropname, carea, shdate, rate }) => {
                const row = worksheet.addRow([farmername, contactno, cropname, carea, shdate, rate]);
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
        const title = "Crop Collection";
        const titleWidth = doc.getTextWidth(title);
        const borderMargin = 10;
        doc.rect(borderMargin, borderMargin, pageWidth - borderMargin * 2, pageHeight - borderMargin * 2);
        doc.text(title, (pageWidth - titleWidth) / 2, 20);
        doc.setLineWidth(0.5);
        doc.line((pageWidth - titleWidth) / 2, 22, (pageWidth + titleWidth) / 2, 22);
        doc.setFontSize(13);
        doc.setFont("Helvetica", "normal");
        let yPosition = 15;
        const tableColumn = ["Farmer Name", "Contact No", "Crop Name", "Crop Area", "Expected Harvesting Date", "Rate"];
        const tableRows = CropData.map(item => [
            item.farmername,
            item.contactno,
            item.cropname,
            item.carea,
            item.shdate,
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
        doc.save("Crop Collection(Report).pdf");
    };
    return (
        <div className="page-wrapper">
            <div className="content">
                <div className="page-header">
                    <div className="add-item d-flex">
                        <div className="page-title">
                            <h3>Crop Collection Entry</h3>
                            <h6>Crop Management</h6>
                        </div>
                    </div>
                    <ul className="table-top-head">
                        <li>
                            <OverlayTrigger placement="top" overlay={renderTooltip}>
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
                                // onClick={OnReloadData}
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
                        <Link to={route.ShedulerIndex} className="btn btn-secondary">
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
                        <div className="table-responsive">
                            <Table columns={columns} dataSource={CropData} />
                        </div>
                    </div>
                </div>
                <Brand />
            </div>
            <AddCropCollect SHAID={selectedData.shaid} VDAID={selectedData.vdaid} FAID={selectedData.faid} />
        </div>
    );
};
export default CropCollectMaster;



