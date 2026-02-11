import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Link, useNavigate } from "react-router-dom";
import ImageWithBasePath from "../../core/img/imagewithbasebath";
import Brand from "../../core/modals/inventory/brand";
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
import ScheduleProceed from "./ScheduleProceed";
import withReactContent from "sweetalert2-react-content";
import Swal from "sweetalert2";
const CropSchedulerMaster = () => {
    const [searchQuery, setSearchQuery] = useState("");
    const [ScheduleData, setScheduleData] = useState([]);
    const [refreshFlag, setRefreshFlag] = useState(false);
    const [selectedData, setSelectedData] = useState({ vaid: null, shaid: null, ID: null });
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const data = useSelector((state) => state.toggle_header);
    const route = all_routes;
    const { isAuthenticated, userdetail } = getUserData();
    if (isAuthenticated == true) {
        console.log("user", userdetail);
        console.log("getUserData", getUserData);
    }
    const handleProceed = (vaid, shaid, ID) => {
        setSelectedData({ vaid: vaid, shaid: shaid, ID: ID });
    };
    const MySwal = withReactContent(Swal);
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
    useEffect(() => {
        const fetchRackData = async () => {
            try {
                const payload = {
                    "vaid": "%",
                    "companyid": userdetail?.companyID || "",
                    "deptid": userdetail?.departmentID || "",
                    "keyword": "%",
                    "date": userdetail?.APPDT || ""
                }
                const headers = {
                    "Content-Type": "application/json",
                    Accept: "*/*",
                };
                axios({
                    method: "POST",
                    url: baseUrl.Url + "/backend/api/GET_ShedulerMasterData",
                    data: JSON.stringify(payload),
                    headers: headers,
                })
                    .then((response) => {
                        if (response.status != 200) throw new Error("Failed to send otp");
                        console.log("response", response.data);
                        const DATA = response.data;
                        setScheduleData(DATA);
                    })
            } catch (error) {
                console.error("Error fetching HSN data:", error);
            }
        };
        fetchRackData();
    }, []);

    const handleSearch = (event) => {
        setSearchQuery(event.target.value);
        try {
            const payload =
            {
                "vaid": "%",
                "companyid": userdetail?.companyID || "",
                "deptid": userdetail?.departmentID || "",
                "keyword": event.target.value,
                "date": userdetail?.APPDT || ""
            }
            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };
            axios({
                method: "POST",
                url: baseUrl.Url + "/backend/api/GET_ShedulerMasterData/_Search",
                data: JSON.stringify(payload),
                headers: headers,
            })
                .then((response) => {
                    if (response.status != 200) throw new Error("Failed to send  data"); console.log("response", response.data);
                    setScheduleData(response.data);
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
    const refreshData = () => {
        setSelectedData({ vaid: null, shaid: null, ID: null });

    };


    const OnDelete = async (shaid) => {
        try {
            const payload = {
                "shaid": shaid,
                "companyid": userdetail?.companyID || "",
                "deptid": userdetail?.departmentID || "",
            }
            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };
            axios({
                method: "POST",
                url: baseUrl.Url + "/backend/api/SP_DeleteShedulerMaster",
                data: JSON.stringify(payload),
                headers: headers,
            })
                .then((response) => {
                    if (response.status != 200) throw new Error("Failed to Fetching Data");
                    MySwal.fire({
                        title: response.data[0].responseCode === "ERROR" ? "Not Deleted" : "Deleted!",
                        text: response.data[0].responseCode === "ERROR"
                            ? response.data[0].responseMessage
                            : response.data[0].responseMessage,
                        icon: response.data[0].responseCode === "ERROR" ? "ERROR" : "success",
                        confirmButtonText: "Ok",
                        customClass: {
                            confirmButton: response.data[0].responseCode === "ERROR" ? "btn btn-danger" : "btn btn-success",
                        },
                    });
                    try {
                        const payload = {
                            "vaid": "%",
                            "companyid": userdetail?.companyID || "",
                            "deptid": userdetail?.departmentID || "",
                            "keyword": "%",
                            "date": userdetail?.APPDT || ""
                        }
                        const headers = {
                            "Content-Type": "application/json",
                            Accept: "*/*",
                        };
                        axios({
                            method: "POST",
                            url: baseUrl.Url + "/backend/api/GET_ShedulerMasterData",
                            data: JSON.stringify(payload),
                            headers: headers,
                        })
                            .then((response) => {
                                if (response.status != 200) throw new Error("Failed to send otp");
                                console.log("response", response.data);
                                const DATA = response.data;
                                setScheduleData(DATA);
                            })
                    } catch (error) {
                        console.error("Error fetching HSN data:", error);
                    }
                })
        } catch (error) {
            console.error("Error fetching data:", error);
        }
    }

    const columns = [
        {
            title: "Farmer",
            dataIndex: "farmername",
            sorter: (a, b) => a.farmername.length - b.farmername.length,
        },
        {
            title: "Crop",
            dataIndex: "cropname",
            sorter: (a, b) => a.cropname.length - b.cropname.length,
        },
        {
            title: "Area",
            dataIndex: "area",
            sorter: (a, b) => a.area.length - b.area.length,
        },
        {
            title: "Date",
            dataIndex: "ehdate",
            sorter: (a, b) => a.ehdate.length - b.ehdate.length,
        },
        {
            title: "Rate",
            dataIndex: "rate",
            sorter: (a, b) => a.rate.length - b.rate.length,
        },
        {
            title: (
                <OverlayTrigger
                    placement="top"
                    overlay={<Tooltip id="types-tooltip" style={{ textAlign: "center" }}>Status</Tooltip>}
                >
                    <div className="text-center w-100" style={{ textAlign: "center" }}>Status</div>
                </OverlayTrigger>
            ),
            dataIndex: "iscompleted",
            render: (text) => {
                const isCompleted = text === "1";
                return (
                    <OverlayTrigger
                        placement="top"
                        overlay={
                            <Tooltip id={`tooltip-${text}`}>
                                {isCompleted ? "Complete" : "Pending"}
                            </Tooltip>
                        }
                    >
                        <div style={{ textAlign: "center" }}>
                            <button
                                className={`btn btn-sm ${isCompleted ? "btn-success" : "btn-warning"} text-dark`}
                                style={{ color: "#000" }} // ensures text is black
                                disabled
                            >
                                {isCompleted ? "Completed" : "Pending"}
                            </button>
                        </div>
                    </OverlayTrigger>
                );
            },
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
                            <Link
                                className="me-2 p-2"
                                to="#"
                                // onClick={() => handleProceed(record.vdaid, record.shaid, record.vaid)}
                                onClick={() => {
                                    handleProceed(record.vdaid, record.shaid, record.vaid);
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
                                // data-bs-toggle="modal"
                                // data-bs-target="#add-units-category"
                                style={{ color: 'green' }}
                            >
                                <i
                                    data-feather="arrow-right-circle"
                                    className="feather-arrow-right-circle"
                                ></i>
                            </Link>
                        </OverlayTrigger>

                        <OverlayTrigger
                            placement="top"
                            overlay={renderDeleteTooltip}
                        >
                            <Link
                                to="#"
                                onClick={() => record.shaid && OnDelete(record.shaid)}
                                style={{
                                    visibility: record.shaid ? "visible" : "hidden",
                                }}
                            >
                                <Trash2 className="feather-trash-2" />
                            </Link>
                        </OverlayTrigger>
                    </div>

                </div>
            ),
            sorter: (a, b) => a.createdby.length - b.createdby.length,
        },
    ];
    const dataSource = [];
    const renderTooltip = (props) => (
        <Tooltip id="pdf-tooltip" {...props}>
            Pdf
        </Tooltip>
    );
    const renderDeleteTooltip = (props) => (
        <Tooltip id="Delete-tooltip" {...props}>
            Delete
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
    return (
        <div className="page-wrapper">
            <div className="content">
                <div className="page-header">
                    <div className="add-item d-flex">
                        <div className="page-title">
                            <h3>Crop Schedule</h3>
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
                                <Link data-bs-toggle="tooltip" data-bs-placement="top" >
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
                    <div className="card-body p-2">
                        <div style={{ overflowX: 'auto', WebkitOverflowScrolling: 'touch' }}>
                            <Table
                                columns={columns}
                                dataSource={ScheduleData}
                                pagination={false}
                                scroll={{ x: 800 }}
                                style={{ minWidth: 800 }}
                            />
                        </div>
                    </div>
                </div>
                <Brand />
            </div>
            <ScheduleProceed
                VAID={selectedData.vaid}
                SHAID={selectedData.shaid}
                ID={selectedData.ID}
                onRefresh={refreshData}
            />
        </div>
    );
};
export default CropSchedulerMaster;

