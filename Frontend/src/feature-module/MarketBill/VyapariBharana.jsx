
import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useState } from "react";
import { Link } from "react-router-dom";
import ImageWithBasePath from "../../core/img/imagewithbasebath";
import Brand from "../../core/modals/inventory/brand";
// import withReactContent from "sweetalert2-react-content";
// import Swal from "sweetalert2";
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

// import BillPopup from "../../feature-module/FinanceAccounts/BillPopup";
// import AddVyaparislab from "../../feature-module/FinanceAccounts/AddVyaparislab"
// import withReactContent from "sweetalert2-react-content";


import { useNavigate } from "react-router-dom";
import { saveAs } from "file-saver";
import ExcelJS from "exceljs";
import { jsPDF } from "jspdf";
import autoTable from "jspdf-autotable";
import marathiFontBase64 from "../../style/fonts/NotoSansDevanagari";
import AddVyapariBharana from "./AddVyapariBharana";
// import A from "../../feature-module/Masters/AddVyapariJama";

import VyapariBharanaReceipt from "./VyapariBharanaReceipt.JSX";


const VyapariBharana = () => {
    const navigate = useNavigate();
    const dispatch = useDispatch();
    const data = useSelector((state) => state.toggle_header);
    const route = all_routes;

    // const [vyapariJamaMaster, setVyapariJamaMaster] = useState([]);
    const [RegulerVyapriBharana, setRegulerVyapriBharana] = useState([]);
    const [searchQuery, setSearchQuery] = useState("");
    const [refreshFlag, setRefreshFlag] = useState(false);// for Refresh
    const [selectedData, setSelectedData] = useState({ vdaid: null });





    // fetch RegulerVyapriBharana from tblVyaparidue table 
    useEffect(() => {

        const fetchRegulerVyapriBharana = async () => {
            try {
                const payload = {
                    "vdaid": "%",
                };
                const headers = {
                    "Content-Type": "application/json",
                    Accept: "*/*",
                };
                const response = await axios.post(
                    `${baseUrl.Url}/backend/api/GET_RegulerVyapriBharana`,
                    payload,
                    { headers }
                );
                if (response.status !== 200)
                    throw new Error("Failed to fetch ");
                console.log("Get_VyapariDueData", response.data)
                setRegulerVyapriBharana(response.data);
            } catch (error) {
                console.error("Error fetching GET_RegulerVyapriBharana data:", error);
            }
        };
        fetchRegulerVyapriBharana();

    }, []);

    // fetch by Search  VyapariSlabTMasetr Data
    const handleSearch = (event) => {
        setSearchQuery(event.target.value);
        try {
            const payload = {
                "vdaid": "%",
                "keyword": event.target.value,
            }
            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };

            axios({
                method: "POST",
                url: baseUrl.Url + "/backend/api/GET_RegulerVyapriBharana/_Search",
                data: JSON.stringify(payload),
                headers: headers,
            })

                .then((response) => {
                    if (response.status != 200) throw new Error("Failed to send response ");
                    console.log("response", response.data);
                    setRegulerVyapriBharana(response.data);
                })
        } catch (error) {
            console.error("Error while searching GET_RegulerVyapriBharana data:", error);
        }
    };



    // const MySwal = withReactContent(Swal);

    // const showConfirmationAlert = () => {
    //     MySwal.fire({
    //         title: "Are you sure?",
    //         text: "You won't be able to revert this!",
    //         showCancelButton: true,
    //         confirmButtonColor: "#00ff00",
    //         confirmButtonText: "Yes, delete it!",
    //         cancelButtonColor: "#ff0000",
    //         cancelButtonText: "Cancel",
    //     }).then((result) => {
    //         if (result.isConfirmed) {

    //             // OndeleteRequisition(vdaid);
    //         } else {
    //             MySwal.close();
    //         }
    //     });
    // };



    // for Refresh
    const refreshData = () => {
        setRefreshFlag(prev => !prev);
    };
    // for Refresh
    useEffect(() => {
        setSelectedData({ vdaid: null }); // 👈 Clear after refresh
    }, [refreshFlag]);

    const OnView = (vpaid) => {
        setSelectedData({ vdaid: vpaid });

    };



    //dowload excel function
    const exportToExcel = async () => {
        try {
            const workbook = new ExcelJS.Workbook();
            const worksheet = workbook.addWorksheet("व्यापारी भरणा रीपोर्ट ");
            const headingRow = worksheet.addRow(["व्यापारी भरणा रीपोर्ट "]);
            headingRow.getCell(1).font = { bold: true, size: 16 };
            headingRow.getCell(1).alignment = { horizontal: "center", vertical: "middle" };
            worksheet.mergeCells("A1:F1");
            const headers = ["व्यापारी नाव ", "शिल्लक रक्कम", "व्यवहार रक्कम ", "अदा वापसी", "व्यवहार मोड", "व्यवहार स्थिती"];
            const headerRow = worksheet.addRow(headers);
            headerRow.eachCell((cell) => {
                cell.font = { bold: true, color: { argb: "FFFFFF" } };
                cell.fill = { type: "pattern", pattern: "solid", fgColor: { argb: "808080" } };
                cell.alignment = { horizontal: "center", vertical: "middle" };
            });
            const columnWidths = [20, 20, 20, 20, 20, 20, 20]; // Adjust widths as needed
            columnWidths.forEach((width, index) => {
                worksheet.getColumn(index + 1).width = width;
            });
            RegulerVyapriBharana.forEach(({ vname, bpamt, tamt, vapasi, trntype, trntid }) => {
                const row = worksheet.addRow([vname, bpamt, tamt, vapasi, trntype, trntid]);
                row.eachCell((cell) => {
                    cell.alignment = { horizontal: "center", vertical: "middle" };
                });
            });
            const buffer = await workbook.xlsx.writeBuffer();
            const data = new Blob([buffer], { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });
            saveAs(data, "व्यापारी भरणा रीपोर्ट.xlsx");
            // console.log(marathiFontBase64);
        } catch (error) {
            console.error("Error generating the Excel file:", error);
        }
    };


    // dowload pdf function
    const generatePDF = (RegulerVyapriBharana) => {
        const doc = new jsPDF();
        const pageWidth = doc.internal.pageSize.getWidth();
        const cleanBase64 = marathiFontBase64.replace(/^data:font\/ttf;base64,/, "");
        doc.addFileToVFS("NotoSansDevanagari.ttf", cleanBase64);
        doc.addFont("NotoSansDevanagari.ttf", "NotoSansDevanagari", "normal");
        // Title: Marathi font
        doc.setFont("NotoSansDevanagari", "normal");
        doc.setFontSize(16);
        const title = "व्यापारी भरणा अहवाल";
        doc.text(title, (pageWidth - doc.getTextWidth(title)) / 2, 20);
        // Table headings in Marathi
        const tableColumn = [["व्यापारी नाव", "शिल्लक रक्कम", "व्यवहार रक्कम ", "अदा वापसी", "व्यवहार मोड", "व्यवहार स्थिती"]];
        // Table body data in English
        const tableRows = RegulerVyapriBharana.map((item) => {
            return [
                item.vname || "",
                item.bpamt || "",
                item.tamt || "",
                item.vapasi || "",
                item.trntype || "",
                item.trntid || ""
            ];
        });
        const sanitizedTableRows = tableRows.map(row =>
            row.map(cell => (cell ? cell.toString() : ""))
        );
        // Table generation
        autoTable(doc, {
            startY: 30,
            head: tableColumn,
            body: sanitizedTableRows,
            // Heading in Marathi
            headStyles: {
                font: "NotoSansDevanagari",
                fontSize: 14,
                fontStyle: "normal",
                fillColor: [0, 102, 204],
                textColor: 255,
            },
            // Body data in English
            styles: {
                font: "helvetica", // English font
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
    const renderViewTooltip = (props) => (
        <Tooltip id="View-tooltip" {...props}>
            View
        </Tooltip>
    );
    // const renderDeleteTooltip = (props) => (
    //     <Tooltip id="Edit-tooltip" {...props}>
    //         Delete
    //     </Tooltip>
    // );
    const renderActionTooltip = (props) => (
        <Tooltip id="action-tooltip" {...props}>
            Action
        </Tooltip>
    );




    const columns = [
        {

            title: (
                <div className="d-flex justify-content-center">
                    <OverlayTrigger placement="top" overlay={<Tooltip id="vyapari-tooltip">व्यापारी नाव</Tooltip>}>
                        <span style={{ cursor: "pointer" }}>व्यापारी नाव</span>
                    </OverlayTrigger>
                </div>
            ),
            dataIndex: "vname",
            // sorter: (a, b) => a.vname.length - b.vname.length,
            render: (text) => (
                <OverlayTrigger placement="top" overlay={<Tooltip id="vyapari-tooltip">{text}</Tooltip>}>
                    <div style={{ textAlign: "left" }}>{text}</div>
                </OverlayTrigger>
            ),
        },
        {
            title: (
                <div className="d-flex justify-content-center">
                    <OverlayTrigger placement="top" overlay={<Tooltip id="BPMT-tooltip">शिल्लक रक्कम</Tooltip>}>
                        <span style={{ cursor: "pointer" }}>शिल्लक रक्कम</span>
                    </OverlayTrigger>
                </div>
            ),
            dataIndex: "bpamt",
            // sorter: (a, b) => a.bpamt.length - b.BPMT.bpamt,
            render: (text) => (
                <OverlayTrigger placement="top" overlay={<Tooltip id="BPMT-tooltip">{text}</Tooltip>}>
                    <div style={{ textAlign: "left" }}>{text}</div>
                </OverlayTrigger>
            ),
        },
        {
            title: (
                <div className="d-flex justify-content-center">
                    <OverlayTrigger placement="top" overlay={<Tooltip id="TAMPT-tooltip">व्यवहार रक्कम</Tooltip>}>
                        <span style={{ cursor: "pointer" }}>व्यवहार रक्कम</span>
                    </OverlayTrigger>
                </div>
            ),
            dataIndex: "tamt",
            // sorter: (a, b) => a.TAMPT.length - b.TAMPT.length,
            render: (text) => (
                <OverlayTrigger placement="top" overlay={<Tooltip id="TAMPT-tooltip">{text}</Tooltip>}>
                    <div style={{ textAlign: "left" }}>{text}</div>
                </OverlayTrigger>
            ),
        },

        {
            title: (
                <div className="d-flex justify-content-center">
                    <OverlayTrigger placement="top" overlay={<Tooltip id="VAPSIAMOUNT-tooltip"> वापसी</Tooltip>}>
                        <span style={{ cursor: "pointer" }}> वापसी</span>
                    </OverlayTrigger>
                </div>
            ),
            dataIndex: "vapasi",
            align: "center",
            // sorter: (a, b) => a.vapasi.length - b.vapasi.length,
            render: (text) => (
                <OverlayTrigger placement="top" overlay={<Tooltip id="VAPSIAMOUNT-tooltip">{text}</Tooltip>}>
                    <div style={{ textAlign: "left" }}>{text}</div>
                </OverlayTrigger>
            ),
        },
        {
            title: (
                <div className="text-center">
                    <OverlayTrigger placement="top" overlay={<Tooltip>व्यवहार मोड</Tooltip>}>
                        <span style={{ cursor: "pointer" }}>व्यवहार मोड</span>
                    </OverlayTrigger>
                </div>
            ),
            dataIndex: "trntype",
            align: "center",
            render: (text) => {
                const label = { 0: "Online", 1: "Cash", 2: "Cheque" }[text] || "Unknown";
                return (
                    <OverlayTrigger placement="top" overlay={<Tooltip>{label}</Tooltip>}>
                        <span>{label}</span>
                    </OverlayTrigger>
                );
            },
        },
        {
            title: (
                <div className="d-flex justify-content-center">
                    <OverlayTrigger placement="top" overlay={<Tooltip id="TRNTID-tooltip">व्यवहार स्थिती </Tooltip>}>
                        <span style={{ cursor: "pointer" }}>व्यवहार स्थिती </span>
                    </OverlayTrigger>
                </div>
            ),
            dataIndex: "trntid",
            align: "center",
            render: (text) => {
                const map = {
                    30: ["Complete", "badge-success"],
                    20: ["Incomplete", "badge-warning"],
                    10: ["Pending ", "badge-info text-dark"],
                };
                const [label, color] = map[text] || ["Unknown", "badge-secondary"];
                return (
                    <OverlayTrigger placement="top" overlay={<Tooltip>{label}</Tooltip>}>
                        <span className={`badge ${color}`} style={{ cursor: "pointer" }}>{label}</span>
                    </OverlayTrigger>
                );
            },

        },

        {
            title: (
                <OverlayTrigger placement="top" overlay={renderActionTooltip}>
                    < div className="d-flex justify-content-center" >
                        कृती
                    </div >
                </OverlayTrigger>

            ),
            dataIndex: "action",
            render: (_, record) => (
                <div className="action-table-data">
                    <div className="edit-delete-action">
                        <OverlayTrigger
                            placement="top"
                            overlay={renderViewTooltip}
                        >
                            <Link
                                className="me-2 p-2"
                                to="#"
                                onClick={() => {
                                    OnView(record.vdaid); // your custom logic here
                                    const modal = document.getElementById("VyapariBharnaReceipt");
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
                                style={{ color: "gray" }}
                            >
                                <Eye className="feather-Eye" />
                            </Link>
                        </OverlayTrigger>

                    </div>
                </div >
            ),
        },
    ];


    // Empty data source
    // const dataSource = [];

    return (
        <div className="page-wrapper">
            <div className="content">
                <div className="page-header">
                    <div className="add-item d-flex">
                        <div className="page-title">
                            <h3>व्यापारी जमा   </h3>
                            <h6>म्यानेज व्यापारी जमा </h6>
                        </div>
                    </div>
                    <ul className="table-top-head">

                        <li>
                            <OverlayTrigger placement="top" overlay={renderTooltip}>
                                <Link
                                    to="#"
                                    onClick={(e) => {
                                        e.preventDefault();
                                        generatePDF(RegulerVyapriBharana);
                                    }}
                                >
                                    <ImageWithBasePath src="assets/img/icons/pdf.svg" alt="Download PDF" />
                                </Link>
                            </OverlayTrigger>
                        </li>



                        <li>
                            <OverlayTrigger placement="top" overlay={renderExcelTooltip}>
                                <Link data-bs-toggle="tooltip" data-bs-placement="top" onClick={exportToExcel}>
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
                                <Link data-bs-toggle="tooltip" data-bs-placement="top">
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
                                const modal = document.getElementById("AddVyapariJama");

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
                            व्यापारी जमा
                        </button>
                    </div>


                    <div className="page-btn">
                        <Link to={route.MarketBillIndex} className="btn btn-secondary">
                            <ArrowLeft className="me-2" />
                            मागे
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
                            <Table columns={columns} dataSource={RegulerVyapriBharana} />
                        </div>
                    </div>
                </div>

                <Brand />
            </div>

            {/* <AddVyaparislab
                VSAID={selectedData.vpaid}
                onRefresh={refreshData}// for refresh
            /> */}
            <AddVyapariBharana
                editVpaid={selectedData.vpaid}
                onRefresh={refreshData}// for refresh
            />

            <VyapariBharanaReceipt
                VDAID={selectedData.vdaid}
                onRefresh={refreshData}// for refresh
            />


        </div>
    );
};

export default VyapariBharana;
