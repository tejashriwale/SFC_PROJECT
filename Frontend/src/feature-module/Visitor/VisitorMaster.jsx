import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useState } from "react";
import { Link } from "react-router-dom";
import ImageWithBasePath from "../../core/img/imagewithbasebath";
import Brand from "../../core/modals/inventory/brand";
import withReactContent from "sweetalert2-react-content";
import Swal from "sweetalert2";
import { all_routes } from "../../Router/all_routes";
import { OverlayTrigger, Tooltip } from "react-bootstrap";
import Table from "../../core/pagination/datatable";
import { setToogleHeader } from "../../core/redux/action";
import { useEffect } from "react";
import axios from "axios";
import { baseUrl } from "../../core/json/custom";
import {
    ArrowLeft, ChevronUp, PlusCircle, RotateCcw, CheckCircle, Edit, Trash2, Eye
} from "feather-icons-react/build/IconComponents";
import { useNavigate } from "react-router-dom";
import { getUserData } from "../../Context/UserData";
import { saveAs } from "file-saver";
import ExcelJS from "exceljs";
import { jsPDF } from "jspdf";
import autoTable from "jspdf-autotable";
import marathiFontBase64 from "../../style/fonts/NotoSansDevanagari";
import AddVisitorMaster from "./AddVisitorMaster";
const VisitorMaster = () => {
    const MySwal = withReactContent(Swal);
    const navigate = useNavigate();
    const { userdetail } = getUserData();
    const dispatch = useDispatch();
    const data = useSelector((state) => state.toggle_header);
    const route = all_routes;
    const [visitorMaster, setVisitorMaster] = useState([]);
    const [searchQuery, setSearchQuery] = useState("");
    const [selectedData, setSelectedData] = useState({ vaid: null });
    const [refreshFlag, setRefreshFlag] = useState(false);
    useEffect(() => {
        const fetchVisitorMaster = async () => {
            try {
                const payload = {
                    "vaid": "%",
                    "keyword": "%",
                    "companyid": userdetail?.companyID ? userdetail.companyID : "",
                    "deptid": userdetail?.departmentID ? userdetail.departmentID : "",
                };
                const headers = {
                    "Content-Type": "application/json",
                    Accept: "*/*",
                };
                const response = await axios.post(
                    `${baseUrl.Url}/backend/api/GET_VisitorMaster`,
                    payload,
                    { headers }
                );
                if (response.status !== 200)
                    throw new Error("Failed to fetch ");
                console.log("GET_VisitorMaster", response.data)
                setVisitorMaster(response.data);
            } catch (error) {
                console.error("Error fetching GET_VisitorMaster data:", error);
            }
        };
        fetchVisitorMaster();
    }, []);
    const handleSearch = (event) => {
        setSearchQuery(event.target.value);
        try {
            const payload = {
                "vaid": "%",
                "keyword": event.target.value,
                "companyid": userdetail?.companyID ? userdetail.companyID : "",
                "deptid": userdetail?.departmentID ? userdetail.departmentID : "",
            }
            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };

            axios({
                method: "POST",
                url: baseUrl.Url + "/backend/api/GET_VisitorMaster/_Search",
                data: JSON.stringify(payload),
                headers: headers,
            })
                .then((response) => {
                    if (response.status != 200) throw new Error("Failed to send response ");
                    console.log("response", response.data);
                    setVisitorMaster(response.data);
                })
        } catch (error) {
            console.error("Error while searching Visitor Master data:", error);
        }
    };
    const refreshData = () => {
        const fetchVisitorMaster = async () => {
            try {
                const payload = {
                    "vaid": "%",
                    "keyword": "%",
                    "companyid": userdetail?.companyID ? userdetail.companyID : "",
                    "deptid": userdetail?.departmentID ? userdetail.departmentID : "",
                };
                const headers = {
                    "Content-Type": "application/json",
                    Accept: "*/*",
                };
                const response = await axios.post(
                    `${baseUrl.Url}/backend/api/GET_VisitorMaster`,
                    payload,
                    { headers }
                );
                if (response.status !== 200)
                    throw new Error("Failed to fetch ");
                console.log("GET_VisitorMaster", response.data)
                setVisitorMaster(response.data);
            } catch (error) {
                console.error("Error fetching GET_VisitorMaster data:", error);
            }
        };
        fetchVisitorMaster();
        setRefreshFlag(prev => !prev);
    };
    useEffect(() => {
        setSelectedData({ vaid: null });
    }, [refreshFlag]);
    const OnEdit = (vaid) => {
        setSelectedData({ vaid: vaid });
    };
    const OnDelete = async (vaid) => {
        console.log("on delete vdaid", vaid)
        try {
            const payload = {
                "vaid": vaid,
                "companyid": userdetail?.companyID ? userdetail.companyID : "",
                "deptid": userdetail?.departmentID ? userdetail.departmentID : "",
            };
            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };
            axios({
                method: "POST",
                url: baseUrl.Url + "/backend/api/SP_DeleteVisitorMaster",
                data: JSON.stringify(payload),
                headers: headers,
            })
                .then((response) => {
                    if (response.status != 200) throw new Error("Failed to Fetching Data");
                    MySwal.fire({
                        title: response.data[0].responseCode === "अपयश" ? "हटवणे अनुमत नाही" : "हटवले!",
                        text: response.data[0].responseCode === "FAILURE"
                            ? response.data[0].responseMessage
                            : response.data[0].responseMessage,
                        icon: response.data[0].responseCode === "FAILURE" ? "error" : "success",
                        confirmButtonText: "ठीक आहे",
                        customClass: {
                            confirmButton: response.data[0].responseCode === "FAILURE" ? "btn btn-danger" : "btn btn-success",
                        },
                    });
                    try {
                        const payload = {
                            "vaid": "%",
                            "keyword": "%",
                            "companyid": userdetail?.companyID ? userdetail.companyID : "",
                            "deptid": userdetail?.departmentID ? userdetail.departmentID : "",
                        };
                        const headers = {
                            "Content-Type": "application/json",
                            Accept: "*/*",
                        };
                        axios({
                            method: "POST",
                            url: baseUrl.Url + "/backend/api/GET_VisitorMaster",
                            data: JSON.stringify(payload),
                            headers: headers,
                        })
                            .then((response) => {
                                if (response.status != 200) throw new Error("Failed to Fetching Data");
                                const DATA = response.data;
                                setVisitorMaster(DATA);
                            })
                    } catch (error) {
                        console.error("Error fetching Access Right Data:", error);
                    }
                })
        } catch (error) {
            console.error("Error fetching Access Right Data:", error);
        }
    }
    const exportToExcel = async () => {
        try {
            const workbook = new ExcelJS.Workbook();
            const worksheet = workbook.addWorksheet("Visits Report");
            const headingRow = worksheet.addRow(["Visits Report"]);
            headingRow.getCell(1).font = { bold: true, size: 16 };
            headingRow.getCell(1).alignment = { horizontal: "center", vertical: "middle" };
            worksheet.mergeCells("A1:F1");
            const headers = ["Farmer Name ", "Visiting Date", "Mobile Number ", "Status"];
            const headerRow = worksheet.addRow(headers);
            headerRow.eachCell((cell) => {
                cell.font = { bold: true, color: { argb: "FFFFFF" } };
                cell.fill = { type: "pattern", pattern: "solid", fgColor: { argb: "808080" } };
                cell.alignment = { horizontal: "center", vertical: "middle" };
            });
            const columnWidths = [20, 20, 20, 20];
            columnWidths.forEach((width, index) => {
                worksheet.getColumn(index + 1).width = width;
            });
            visitorMaster.forEach(({ fname, visitingdate, phonenumber, iscomplited }) => {
                const row = worksheet.addRow([fname, visitingdate, phonenumber, iscomplited]);
                row.eachCell((cell) => {
                    cell.alignment = { horizontal: "center", vertical: "middle" };
                });
            });
            const buffer = await workbook.xlsx.writeBuffer();
            const data = new Blob([buffer], { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });
            saveAs(data, "Visits Report.xlsx");
        } catch (error) {
            console.error("Error generating the Excel file:", error);
        }
    };
    const generatePDF = (visitorMaster) => {
        const doc = new jsPDF();
        const pageWidth = doc.internal.pageSize.getWidth();
        const cleanBase64 = marathiFontBase64.replace(/^data:font\/ttf;base64,/, "");
        doc.addFileToVFS("NotoSansDevanagari.ttf", cleanBase64);
        doc.addFont("NotoSansDevanagari.ttf", "NotoSansDevanagari", "normal");
        doc.setFont("helvetica", "normal");
        doc.setFontSize(16);
        const title = "Visits Report";
        doc.text(title, (pageWidth - doc.getTextWidth(title)) / 2, 20);
        const tableColumn = ["Farmer Name", "Visiting Date", "Mobile Number", "Status"];
        const tableRows = visitorMaster.map((item) => {
            return [
                item.fname || "",
                item.visitingdate || "",
                item.phonenumber || "",
                item.iscomplited || "",
            ];
        });
        const sanitizedTableRows = tableRows.map(row =>
            row.map(cell => (cell ? cell.toString() : ""))
        );
        autoTable(doc, {
            startY: 30,
            head: [tableColumn],
            body: sanitizedTableRows,
            styles: {
                font: "helvetica",
                fontSize: 12,
            },
            alternateRowStyles: { fillColor: [240, 240, 240] },
        });
        window.open(doc.output("bloburl"), "_blank");
    };
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
        <Tooltip id="collapse-tooltip" {...props}>
            Collapse
        </Tooltip>
    );
    const renderEditTooltip = (props) => (
        <Tooltip id="Edit-tooltip" {...props}>
            Edit
        </Tooltip>
    );
    const renderDeleteTooltip = (props) => (
        <Tooltip id="Delete-tooltip" {...props}>
            Delete
        </Tooltip>
    );
    const renderActionTooltip = (props) => (
        <Tooltip id="action-tooltip" {...props}>
            Action
        </Tooltip>
    );
    const columns = [
        {
            title: (
                <OverlayTrigger placement="top" overlay={<Tooltip id="fname-tooltip">Farmer</Tooltip>}>
                    <div>Farmer</div>
                </OverlayTrigger>
            ),
            dataIndex: "fname",
            render: (text) => (
                <OverlayTrigger placement="top" overlay={<Tooltip id="fname-tooltip">{text}</Tooltip>}>
                    <div style={{ textAlign: "left" }}>{text}</div>
                </OverlayTrigger>
            ),
        },
        {
            title: (
                <div >
                    <OverlayTrigger placement="top" overlay={<Tooltip id="visitingdate-tooltip">V.Date</Tooltip>}>
                        <div>V.Date</div>
                    </OverlayTrigger>
                </div>
            ),
            dataIndex: "visitingdate",
            render: (text) => (
                <OverlayTrigger placement="top" overlay={<Tooltip id="visitingdate-tooltip">{text}</Tooltip>}>
                    <div style={{ textAlign: "left" }}>{text}</div>
                </OverlayTrigger>
            ),
        },
        {
            title: (
                <OverlayTrigger placement="top" overlay={<Tooltip id="phonenumber-tooltip">Mobile</Tooltip>}>
                    <div>Mobile</div>
                </OverlayTrigger>
            ),
            dataIndex: "phonenumber",
            render: (text) => (
                <OverlayTrigger placement="top" overlay={<Tooltip id="phonenumber-tooltip">{text}</Tooltip>}>
                    <div style={{ textAlign: "left" }}>{text}</div>
                </OverlayTrigger>
            ),
        },
        {
            title: (
                <OverlayTrigger placement="top" overlay={<Tooltip id="iscompleted-tooltip">Status</Tooltip>}>
                    <div>Status</div>
                </OverlayTrigger>
            ),
            dataIndex: "iscomplited",
            render: (text) => (
                <OverlayTrigger placement="top" overlay={<Tooltip id="iscompleted-tooltip">{text}</Tooltip>}>
                    <div style={{ textAlign: "left" }}>
                        {text === "1" ? (
                            <span className="btn bg-success">Completed</span>
                        ) : (
                            <span className="btn bg-warning">Pending</span>
                        )}
                    </div>
                </OverlayTrigger>
            ),
        },
        {
            title: (
                <OverlayTrigger placement="top" overlay={renderActionTooltip}>
                    < div >
                        Action
                    </div >
                </OverlayTrigger>
            ),
            dataIndex: "action",
            render: (_, record) => (
                <div className="action-table-data">
                    <div className="edit-delete-action">
                        <OverlayTrigger
                            placement="top"
                            overlay={renderEditTooltip}
                        >
                            <Link
                                className=""
                                to="#"
                                onClick={() => {
                                    OnEdit(record.vaid);
                                    const modal = document.getElementById("AddVisitor");
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
                            >
                                <Edit className="feather-Edit" />
                            </Link>
                        </OverlayTrigger>
                        <OverlayTrigger
                            placement="top"
                            overlay={renderDeleteTooltip}
                        >
                            <Link
                                className=""
                                to="#"
                                onClick={() => {
                                    OnDelete(record.vaid);
                                }}
                            >
                                <Trash2 className="feather-trash-2" />
                            </Link>
                        </OverlayTrigger>
                    </div>
                </div >
            ),
        },
    ];

    return (
        <div className="page-wrapper">
            <div className="content">
                <div className="page-header">
                    <div className="add-item d-flex">
                        <div className="page-title">
                            <h3>Agronomist's farm visit</h3>
                            <h6>Agronomist's farm visit</h6>
                        </div>
                    </div>
                    <ul className="table-top-head">
                        <li>
                            <OverlayTrigger placement="top" overlay={renderTooltip}>
                                <Link
                                    to="#"
                                    onClick={(e) => {
                                        e.preventDefault();
                                        generatePDF(visitorMaster);
                                    }}
                                >
                                    <ImageWithBasePath src="assets/img/icons/pdf.svg" alt="Download PDF" />
                                </Link>
                            </OverlayTrigger>
                        </li>

                        <li>
                            <OverlayTrigger placement="top" overlay={renderExcelTooltip}>
                                <Link data-bs-toggle="tooltip" data-bs-placement="top"
                                    onClick={exportToExcel}
                                >
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
                                <Link data-bs-toggle="tooltip"
                                    data-bs-placement="top"
                                    onClick={() => {
                                        refreshData();
                                    }}
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
                        <button
                            className="btn btn-added"
                            onClick={() => {
                                const modal = document.getElementById("AddVisitor");

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
                        >
                            <PlusCircle className="me-2" />
                            Add Visits
                        </button>
                    </div>

                    <div className="page-btn">
                        <Link to={route.VisitorIndex} className="btn btn-secondary">
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
                        <div className="table-responsive responsive-no-scroll" >
                            <Table columns={columns} dataSource={visitorMaster} />
                        </div>
                    </div>
                </div>
                <Brand />
            </div>
            <AddVisitorMaster
                VAID={selectedData.vaid}
                onRefresh={refreshData}
            />
        </div>
    );
};
export default VisitorMaster;

