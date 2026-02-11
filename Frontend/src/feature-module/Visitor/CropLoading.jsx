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
import AddCropLoading from "./AddCropLoading";
const CropLoading = () => {
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
            if (e.ctrlKey && e.key === 'a') {
                e.preventDefault();
                // navigate(route.AddCropLoading);
                const modal = document.getElementById("AddVehicleLoading");

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
            }
            // if (e.ctrlKey && e.key === 'e') {
            //     e.preventDefault();
            //     navigate(route.MasterIndex);
            // }
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
    const [selectedData, setSelectedData] = useState({ slvmid: null, slvdid: null });
    const onEditClick = (slvmid, slvdid) => {
        setSelectedData({
            slvmid: slvmid,
            slvdid: slvdid,
            refreshKey: Date.now(),
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
            title: "Action",
            dataIndex: "action",
            render: (_, record) => (
                <div className="action-table-data">
                    <div className="edit-delete-action">
                        <OverlayTrigger
                            placement="top"
                            overlay={<Tooltip id="edit-tooltip">Edit</Tooltip>}>
                            <a
                                className="me-2 p-2"
                                // onClick={() => { onEditClick(record.slvmid, record.slvdid) }}
                                // data-bs-toggle="modal"
                                // data-bs-target="#AddVehicleLoading"

                                onClick={(e) => {
                                    e.preventDefault();
                                    onEditClick(record.slvmid, record.slvdid);


                                    const modal = document.getElementById("AddVehicleLoading");

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
                            >  <Edit className="feather-edit" /></a>
                        </OverlayTrigger>
                        <OverlayTrigger placement="top" overlay={<Tooltip id="delete-tooltip">Delete</Tooltip>}>
                            <Link
                                className="confirm-text p-2"
                                to="#"
                                onClick={() => showConfirmationAlert(record.slvmid)}>
                                <Trash2 className="feather-trash-2" />
                            </Link>
                        </OverlayTrigger>
                    </div>
                </div>
            )
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
        setSelectedData({ slvmid: null, slvdid: null });
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
                            <h3>Crop Loading In Vehicle </h3>
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
                        <Link
                            to="#"
                            className="btn btn-added"
                            // data-bs-toggle="modal"
                            // data-bs-target="#AddVehicleLoading"

                            onClick={() => {

                                const modal = document.getElementById("AddVehicleLoading");

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
                            Crop Loading
                        </Link>
                    </div>
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
            <AddCropLoading SLVMID={selectedData.slvmid}
                refreshKey={selectedData1.refreshKey}
                onRefresh={refreshData}
                onSaved={fetchMasterList} />
        </div>
    );
};
export default CropLoading;
