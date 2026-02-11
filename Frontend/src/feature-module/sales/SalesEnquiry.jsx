
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
    ArrowLeft, ChevronUp, Edit, PlusCircle, RotateCcw, Trash2,
} from "feather-icons-react/build/IconComponents";
import AddSalesEnquiry from "../sales/AddSalesEnquiry";
import { useNavigate } from "react-router-dom";
// import AddPickupQuotation from "./AddPickupQuotation";
import { jsPDF } from 'jspdf';
import autoTable from 'jspdf-autotable';
import ExcelJS from "exceljs";
import { saveAs } from "file-saver";
import { getUserData } from "../../Context/UserData";
import ProceedRequisition from "./ProceedRequisition";
// import ProceedSalesPickUpQuatation from "./ProceedSalesPickUpQuatation";
function SalesEnquiry() {
    const { userdetail } = getUserData();
    const dispatch = useDispatch();
    const data = useSelector((state) => state.toggle_header);
    const route = all_routes;
    const [saleRequisition, setSaleRequisition] = useState([]);

    const navigate = useNavigate();

    useEffect(() => {
        const handleShortcut = (e) => {
            if (e.ctrlKey && (e.key === "a" || e.key === "A")) {
                e.preventDefault();
                navigate("/AddSalesEnquiry");
            }
            if (e.ctrlKey && (e.key === 'e' || e.key === 'E')) {
                e.preventDefault();
                navigate("/SalesIndex");
            }
        };

        window.addEventListener("keydown", handleShortcut);

        return () => {
            window.removeEventListener("keydown", handleShortcut);
        };
    }, [navigate]);

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
        <Tooltip id="view-tooltip" {...props}>
            Edit
        </Tooltip>
    );
    // const renderSendTooltip = (props) => (
    //     <Tooltip id="send-tooltip" {...props}>
    //         Send
    //     </Tooltip>
    // );
    const renderProceedTooltip = (props) => (
        <Tooltip id="Proceed-tooltip" {...props}>
            Proceed
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
    const renderEnquiryNumberTooltip = (props) => (
        <Tooltip id="action-tooltip" {...props}>
            EnquiryNumber
        </Tooltip>
    );
    const [searchQuery, setSearchQuery] = useState("");
    const handleSearch = (event) => {
        setSearchQuery(event.target.value);
        try {
            const payload = {
                "sraid": "%",
                "keyword": event.target.value,
                "companyid": userdetail?.companyID || "",
                "deptid": userdetail?.departmentID || "",
            }
            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };

            axios({
                method: "POST",
                url: baseUrl.Url + "/backend/api/GET_SRequisitionMaster/_Search",
                data: JSON.stringify(payload),
                headers: headers,
            })

                .then((response) => {
                    if (response.status != 200) throw new Error("Failed to send otp");
                    console.log("response", response.data);
                    setSaleRequisition(response.data);
                })
        } catch (error) {
            console.error("Error while searching Requisistion data:", error);
        }
    };

    useEffect(() => {
        const fetchSalesRequisition = async () => {
            try {
                const payload = {
                    sraid: "%",
                    keyword: "%",
                    companyid: userdetail?.companyID || "",
                    deptid: userdetail?.departmentID || "",
                };
                const headers = {
                    "Content-Type": "application/json",
                    Accept: "*/*",
                };
                const response = await axios.post(
                    `${baseUrl.Url}/backend/api/GET_SRequisitionMaster`,
                    payload,
                    { headers }
                );
                if (response.status !== 200)
                    throw new Error("Failed to fetch vendor data");
                console.log("quatation master", response.data)
                setSaleRequisition(response.data);
            } catch (error) {
                console.error("Error fetching vendor data:", error);
            }
        };

        fetchSalesRequisition();

    }, []);

    const [selectedData, setSelectedData] = useState({ sraid: null, vandorid: null, srno: null });
    const [refreshFlag, setRefreshFlag] = useState(false);// for Refresh

    // for Refresh
    const refreshData = () => {
        setSelectedData({ sraid: null, srno: null });

        const fetchSalesRequisition = async () => {
            try {
                const payload = {
                    sraid: "%",
                    keyword: "%",
                    companyid: userdetail?.companyID || "",
                    deptid: userdetail?.departmentID || "",
                };
                const headers = {
                    "Content-Type": "application/json",
                    Accept: "*/*",
                };
                const response = await axios.post(
                    `${baseUrl.Url}/backend/api/GET_SRequisitionMaster`,
                    payload,
                    { headers }
                );
                if (response.status !== 200)
                    throw new Error("Failed to fetch vendor data");
                console.log("quatation master", response.data)
                setSaleRequisition(response.data);
            } catch (error) {
                console.error("Error fetching vendor data:", error);
            }
        };
        fetchSalesRequisition();

        setRefreshFlag(prev => !prev);
    };

    // for Refresh
    useEffect(() => {
        setSelectedData({ sraid: null, srno: null }); // 👈 Clear after refresh
    }, [refreshFlag]);


    const openEditProceed = (sraid) => {
        setSelectedData({ sraid })
    }


    const openModalProceed = (srno) => {
        setSelectedData({ srno })
    }


    const handleProceedClick = async (e, srno) => {
        e.preventDefault(); // stop modal from opening unless allowed

        try {
            const payload = {
                SPRNO: srno,
                COMPANYID: userdetail?.companyID || "",
                DEPTID: userdetail?.departmentID || ""
            };

            const response = await axios.post(
                baseUrl.Url + "/backend/api/GET_QuatationSRNo",
                payload,
                { headers: { "Content-Type": "application/json" } }
            );

            // ✅ Check if array has at least one matching SPRNO
            const exists = Array.isArray(response.data) && response.data.length > 0;

            if (exists) {
                // ✅ Show alert and DO NOT open modal
                Swal.fire({
                    icon: "info",
                    title: "Already Saved",
                    text: "This data is already saved in Quotation Master. You Can not Save (Proceed) it Again.",
                    confirmButtonColor: "#3085d6"
                });
                return;
            }

            // ✅ If not found → proceed to open modal
            openModalProceed(srno);

            // (Optional) Only if modal doesn't open automatically:
            const modalEl = document.getElementById("ProceedRequisition");
            if (modalEl) {
                const modal = new window.bootstrap.Modal(modalEl);
                modal.show();
            }

        } catch (error) {
            console.error("Error checking SPRNO:", error);
            Swal.fire({
                icon: "error",
                title: "Error",
                text: "Something went wrong while checking if data exists in Quotation Master.",
            });
        }
    };

    const handleEditClick = async (e, srno, sraid) => {
        e.preventDefault(); // stop modal from opening unless allowed

        try {
            const payload = {
                SPRNO: srno,
                COMPANYID: userdetail?.companyID || "",
                DEPTID: userdetail?.departmentID || ""
            };

            const response = await axios.post(
                baseUrl.Url + "/backend/api/GET_QuatationSRNo",
                payload,
                { headers: { "Content-Type": "application/json" } }
            );

            // ✅ Check if array has at least one matching SPRNO
            const exists = Array.isArray(response.data) && response.data.length > 0;

            if (exists) {
                // ✅ Show alert and DO NOT open modal
                Swal.fire({
                    icon: "info",
                    title: "Already Saved",
                    text: "This data is already saved in Quotation Master. Now You Cannot Edit This Data",
                    confirmButtonColor: "#3085d6"
                });
                return;
            }

            // ✅ If not found → proceed to open modal
            openEditProceed(sraid);

            // (Optional) Only if modal doesn't open automatically:
            const modalEl = document.getElementById("AddSalesEnquiry");
            if (modalEl) {
                const modal = new window.bootstrap.Modal(modalEl);
                modal.show();
            }

        } catch (error) {
            console.error("Error checking SPRNO:", error);
            Swal.fire({
                icon: "error",
                title: "Error",
                text: "Something went wrong while checking if data exists in Quotation Master.",
            });
        }
    };

    const handleDelete = async (e, srno, sraid) => {
        e.preventDefault(); // stop modal from opening unless allowed

        try {
            const payload = {
                SPRNO: srno,
                COMPANYID: userdetail?.companyID || "",
                DEPTID: userdetail?.departmentID || ""
            };

            const response = await axios.post(
                baseUrl.Url + "/backend/api/GET_QuatationSRNo",
                payload,
                { headers: { "Content-Type": "application/json" } }
            );

            // ✅ Check if array has at least one matching SPRNO
            const exists = Array.isArray(response.data) && response.data.length > 0;

            if (exists) {
                // ✅ Show alert and DO NOT open modal
                Swal.fire({
                    icon: "info",
                    title: "Already Saved",
                    text: "This data is already saved in Quotation Master. You Can Not Delete It.",
                    confirmButtonColor: "#3085d6"
                });
                return;
            }

            // ✅ If not found → proceed to open modal
            showConfirmationAlert(sraid);

            // // (Optional) Only if modal doesn't open automatically:
            // const modalEl = document.getElementById("AddPickupQuotation");
            // if (modalEl) {
            //     const modal = new window.bootstrap.Modal(modalEl);
            //     modal.show();
            // }

        } catch (error) {
            console.error("Error checking SPRNO:", error);
            Swal.fire({
                icon: "error",
                title: "Error",
                text: "Something went wrong while checking if data exists in Quotation Master.",
            });
        }
    };


    const generatePDF = (saleRequisition) => {
        const doc = new jsPDF();
        const pageWidth = doc.internal.pageSize.getWidth();
        const pageHeight = doc.internal.pageSize.getHeight();
        const borderMargin = 10;

        doc.setFontSize(12);
        doc.setFont("Helvetica", "bold");
        const title = "Sale Enquiry Report";
        const titleWidth = doc.getTextWidth(title);

        // Function to draw a faint border on each page
        const drawBorder = () => {
            doc.setDrawColor(100, 100, 100);
            doc.rect(borderMargin, borderMargin, pageWidth - borderMargin * 2, pageHeight - borderMargin * 2);
        };
        drawBorder();

        doc.setDrawColor(0, 0, 0);
        doc.text(title, (pageWidth - titleWidth) / 2, 20);
        doc.setLineWidth(0.5);
        doc.line((pageWidth - titleWidth) / 2, 22, (pageWidth + titleWidth) / 2, 22);

        const tableColumn = ["Requisition Number", "Requisition Date", "Enquiry Number", "Expected Date", "Meeting Date"];
        const tableRows = saleRequisition.map((item) => [
            item.srno,
            item.srqdate,
            item.sreqno,
            item.srexpcdate,
            item.srmdate
        ]);

        autoTable(doc, {
            startY: 30,
            head: [tableColumn],
            body: tableRows,
            theme: 'grid',
            styles: { fontSize: 10, halign: "center", lineColor: [0, 0, 0], lineWidth: 0.20 },
            headStyles: { fillColor: [169, 169, 169], textColor: 0, fontStyle: "bold" },
            bodyStyles: { textColor: 0 },
            didDrawPage: () => {
                drawBorder();
            },
        });

        doc.save("Report.pdf");
    };


    const exportToExcel = async () => {
        try {
            const workbook = new ExcelJS.Workbook();
            const worksheet = workbook.addWorksheet("Sale Enquiry Report");

            // **Main Heading Row**
            const headingRow = worksheet.addRow(["Sale Enquiry Report"]);
            headingRow.getCell(1).font = { bold: true, size: 16 };
            headingRow.getCell(1).alignment = { horizontal: "center", vertical: "middle" };
            worksheet.mergeCells("A1:F1");

            // **Header Row**
            const headers = ["Requisition Number", "Requisition Date", "Enquiry Number", "Expected Date", "Meeting Date"];
            const headerRow = worksheet.addRow(headers);

            headerRow.eachCell((cell) => {
                cell.font = { bold: true, color: { argb: "FFFFFF" } };
                cell.fill = { type: "pattern", pattern: "solid", fgColor: { argb: "808080" } };
                cell.alignment = { horizontal: "center", vertical: "middle" };
            });

            // **Setting Column Widths**
            const columnWidths = [20, 20, 25, 30, 18, 18];
            columnWidths.forEach((width, index) => {
                worksheet.getColumn(index + 1).width = width;
            });


            saleRequisition.forEach(({ srno, srqdate, sreqno, srexpcdate, srmdate }) => {
                const row = worksheet.addRow([srno, srqdate, sreqno, srexpcdate, srmdate]);
                row.eachCell((cell) => {
                    cell.alignment = { horizontal: "center", vertical: "middle" };
                });
            });

            const buffer = await workbook.xlsx.writeBuffer();
            const data = new Blob([buffer], { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });
            saveAs(data, "EnquiryReport.xlsx");

        } catch (error) {
            console.error("Error generating the Excel file:", error);
        }
    };

    const columns = [
        {
            title: "Requisition Number",
            dataIndex: "srno",
            sorter: (a, b) => a.srno.length - b.srno.length,
            render: (text) => (
                <OverlayTrigger placement="top" overlay={<Tooltip id="requisitionnumber-tooltip">{text}</Tooltip>}>
                    <div>{text}</div>
                </OverlayTrigger>
            ),
        },
        {
            title: "Requisition Date",
            dataIndex: "srqdate",
            sorter: (a, b) => a.srqdate.length - b.srqdate.length,
            render: (text) => (
                <OverlayTrigger placement="top" overlay={<Tooltip id="requisitiondate-tooltip">{text}</Tooltip>}>
                    <div style={{ textAlign: "center" }}>{text}</div>
                </OverlayTrigger>
            ),
        },
        {
            title: (
                <OverlayTrigger placement="top" overlay={renderEnquiryNumberTooltip} >
                    <span>Enquiry Number</span>
                </OverlayTrigger >),
            dataIndex: "sreqno",
            render: (text) => (
                <OverlayTrigger placement="top" overlay={<Tooltip id="enquirynumber-tooltip">{text}</Tooltip>}>
                    <div style={{ textAlign: "right" }}>{text}</div>
                </OverlayTrigger>
            ),
        },
        {
            title: "Expected Date",
            dataIndex: "srexpcdate",
            sorter: (a, b) => a.srexpcdate.length - b.srexpcdate.length,
            render: (text) => (
                <OverlayTrigger placement="top" overlay={<Tooltip id="expecteddate-tooltip">{text}</Tooltip>}>
                    <div style={{ textAlign: "center" }}>{text}</div>
                </OverlayTrigger>
            ),
        },
        {
            title: "Meeting Date",
            dataIndex: "srmdate",
            sorter: (a, b) => a.srmdate.length - b.srmdate.length,
            render: (text) => (
                <OverlayTrigger placement="top" overlay={<Tooltip id="meetingdate-tooltip">{text}</Tooltip>}>
                    <div style={{ textAlign: "center" }}>{text}</div>
                </OverlayTrigger>
            ),
        },
        {
            title: (
                <OverlayTrigger placement="top" overlay={renderActionTooltip}>
                    < div className="d-flex justify-content-center" >
                        Action
                    </div >
                </OverlayTrigger>

            ),
            dataIndex: "action",
            render: (_, record) => (
                <div className="action-table-data">
                    <div className="edit-delete-action">

                        {/* Edit */}
                        <OverlayTrigger placement="top" overlay={renderEditTooltip}>
                            {/* <Link className="me-2 p-2"
                                to="#"
                                data-bs-toggle="modal"
                                data-bs-target="#AddSalesEnquiry"
                                onClick={() => openModal(record.sraid)}
                            >
                                <Edit className="feather-edit" />
                            </Link> */}
                            <Link
                                className="me-2 p-2"
                                to="#"
                                onClick={(e) => handleEditClick(e, record.srno, record.sraid)}
                                style={{ color: 'green' }}
                            >
                                <Edit className="feather-edit" />
                            </Link>
                        </OverlayTrigger>

                        {/* Send
                        <OverlayTrigger placement="top" overlay={renderSendTooltip}>
                            <Link className="me-2 p-2">
                                <Send size={14} className="feather-send" />
                            </Link>
                        </OverlayTrigger> */}

                        {/* proceds */}
                        <OverlayTrigger placement="top" overlay={renderProceedTooltip}>
                            {/* Proceed Button */}
                            {/* <Link
                                className="me-2 p-2"
                                to="#"
                                data-bs-toggle="modal"
                                onClick={() => openModalProceed(record.srno)}
                                data-bs-target="#AddPickupQuotation"
                               
                                style={{ color: 'green' }}>
                                <i data-feather="arrow-right-circle" className="feather-arrow-right-circle"></i>
                            </Link> */}
                            <Link
                                className="me-2 p-2"
                                to="#"
                                onClick={(e) => handleProceedClick(e, record.srno)}
                                style={{ color: 'green' }}
                            >
                                <i data-feather="arrow-right-circle" className="feather-arrow-right-circle"></i>
                            </Link>

                        </OverlayTrigger>

                        {/* Delete */}
                        <OverlayTrigger placement="top" overlay={renderDeleteTooltip}>
                            {/* <Link
                                className="confirm-text p-2 me-2"
                                to="#"
                                onClick={() => showConfirmationAlert(record.sraid)}
                            >
                                <Trash2 className="feather-trash-2" />
                            </Link> */}
                            <Link
                                className="confirm-text p-2 me-2"
                                to="#"
                                onClick={(e) => handleDelete(e, record.srno, record.sraid,)}

                            >
                                <Trash2 className="feather-trash-2" />
                            </Link>
                        </OverlayTrigger>


                    </div>
                </div>
            ),
        },
    ];

    const MySwal = withReactContent(Swal);
    const showConfirmationAlert = (sraid) => {
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
                OndeleteRequisition(sraid);
            } else {
                MySwal.close();
            }
        });
    };


    const OndeleteRequisition = async (sraid) => {
        try {
            const payload = {
                "sraid": sraid,
                "companyid": userdetail?.companyID || "",
                "deptid": userdetail?.departmentID || "",
            }
            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };

            axios({
                method: "POST",
                url: baseUrl.Url + "/backend/api/SP_DeleteSRequisition",
                data: JSON.stringify(payload),
                headers: headers,
            })
                .then((response) => {
                    if (response.status != 200) throw new Error("Failed to Fetching Data");
                    MySwal.fire({
                        title: response.data[0].responseCode === "FAILURE" ? "Deletion Not Allowed" : "Deleted!",
                        text: response.data[0].responseCode === "FAILURE"
                            ? response.data[0].responseMessage
                            : response.data[0].responseMessage,
                        icon: response.data[0].responseCode === "FAILURE" ? "error" : "success",
                        confirmButtonText: "OK",
                        customClass: {
                            confirmButton: response.data[0].responseCode === "FAILURE" ? "btn btn-danger" : "btn btn-success",
                        },
                    });
                    try {
                        const payload = {
                            "sraid": "%",
                            "keyword": "%",
                            "companyid": userdetail?.companyID || "",
                            "deptid": userdetail?.departmentID || "",
                        }
                        const headers = {
                            "Content-Type": "application/json",
                            Accept: "*/*",
                        };

                        axios({
                            method: "POST",
                            url: baseUrl.Url + "/backend/api/GET_SRequisitionMaster",
                            data: JSON.stringify(payload),
                            headers: headers,
                        })
                            .then((response) => {
                                if (response.status != 200) throw new Error("Failed to Fetching Data");
                                const DATA = response.data;
                                setSaleRequisition(DATA);
                            })

                    } catch (error) {
                        console.error("Error fetching Access Right Data:", error);
                    }
                })

        } catch (error) {
            console.error("Error fetching Access Right Data:", error);
        }

    }

    return (
        <div className="page-wrapper">
            <div className="content">
                <div className="page-header">
                    <div className="add-item d-flex">
                        <div className="page-title">
                            <h3>Sales Enquiry</h3>
                            <h6>Manage Sales Enquiry</h6>
                        </div>
                    </div>
                    <ul className="table-top-head">
                        <li>
                            <OverlayTrigger placement="top" overlay={renderTooltip}>
                                <Link onClick={() => generatePDF(saleRequisition)}>
                                    <ImageWithBasePath src="assets/img/icons/pdf.svg" alt="img" />
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
                        <Link
                            to="#"
                            className="btn btn-added"
                            data-bs-toggle="modal"
                            data-bs-target="#AddSalesEnquiry"
                        >
                            <PlusCircle className="me-2" />
                            Add New Sales Enquiry
                        </Link>
                    </div>

                    <div className="page-btn">
                        <Link to={route.SalesIndex} className="btn btn-secondary">
                            <ArrowLeft className="me-2" />
                            Back to Index
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
                                    placeholder="Search By Requisition Number OR Enquiry Number"
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
                            <Table columns={columns} dataSource={saleRequisition} />
                        </div>
                    </div>
                </div>

                <Brand />
            </div>
            <AddSalesEnquiry
                SRAID={selectedData.sraid}
                onRefresh={refreshData}// for refresh
            />
            {/* <AddPickupQuotation
                srno={selectedData.srno}
            /> */}

            <ProceedRequisition
                srno={selectedData.srno}
                onRefresh={refreshData}// for refresh
            />


        </div>
    );
}
export default SalesEnquiry
