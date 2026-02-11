
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
import AddDirectInward from "./AddDirectInward";
const DirectInward = () => {
    // const [selectedData, setSelectedData] = useState({ shaid: null, vdaid: null, faid: null });
    const [selectedData1, setSelectedData1] = useState({ cropid: null, faid: null });
    const [refreshFlag, setRefreshFlag] = useState(false);
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const data = useSelector((state) => state.toggle_header);
    const route = all_routes;
    const { isAuthenticated, userdetail } = getUserData();
    const MySwal = withReactContent(Swal);
    if (isAuthenticated == true) {
        console.log("user", userdetail);
        console.log("getUserData", getUserData);
    }
    // const handleProceed = (shaid, vdaid, faid) => {
    //     setSelectedData({ shaid: shaid, vdaid: vdaid, faid: faid });
    // };

    const OnEdit = (cropid, faid) => {
        setSelectedData1({ cropid: cropid, faid: faid });
        console.log("jfldjsfjfjl;skjfl;kdsjfl;kdsjjfkj", cropid)
    };

    const refreshData = () => {
        setSelectedData1({ cropid: null, faid: null })
        setRefreshFlag((prev) => !prev);
    }


    useEffect(() => {
        const handleShortcut = (e) => {
            if (e.ctrlKey && (e.key === 'a' || e.key === 'A')) {
                e.preventDefault();
                // navigate(route.AddDirectInward);

                const modal = document.getElementById("Direct-category");

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
    },);
    const [CropData, setCropData] = useState([]);
    useEffect(() => {
        const fetchCropData = async () => {
            try {
                const payload = {
                    "cropid": "%",
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
                    url: baseUrl.Url + "/backend/api/GET_CropDirectInvertMaster",
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
    }, [refreshFlag]);
    const [searchQuery, setSearchQuery] = useState("");
    const handleSearch = (event) => {
        setSearchQuery(event.target.value);
        try {
            const payload =
            {
                "cropid": "%",
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
                url: baseUrl.Url + "/backend/api/GET_CropDirectInvertMaster/_Search",
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


    const handleDelete = async (cropid) => {
        Swal.fire({
            title: "Are you sure?",
            text: "You won’t be able to undo this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#d33",
            cancelButtonColor: "#3085d6",
            confirmButtonText: "Yes, delete it!",
            cancelButtonText: "Cancel",
        }).then(async (result) => {
            if (result.isConfirmed) {
                OnDelete(cropid)
            }
        })

    }

    const OnDelete = async (cropid) => {
        try {
            const payload = {
                "cropid": cropid,
                "faid": "",
                "companyid": userdetail?.companyID || "",
                "deptid": userdetail?.departmentID || "",
            }
            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };
            axios({
                method: "POST",
                url: baseUrl.Url + "/backend/api/SP_DeleteCropInvert",
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
                            "cropid": "%",
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
                            url: baseUrl.Url + "/backend/api/GET_CropDirectInvertMaster",
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
                })
        } catch (error) {
            console.error("Error fetching data:", error);
        }
    }

    const renderproceeTooltip = (props) => (
        <Tooltip id="Edit-tooltip" {...props}>
            Proceed
        </Tooltip>
    );
    const renderEditTooltip = (props) => (
        <Tooltip id="Edit-tooltip" {...props}>
            Edit
        </Tooltip>
    );
    const columns = [
        {
            title: "date",
            dataIndex: "crdate"
        },
        {
            title: "Bill No",
            dataIndex: "billno",
        },
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
        // {
        //     title: "Crop Area",
        //     dataIndex: "carea",
        // },
        // {
        //     title: "Rate(₹)",
        //     dataIndex: "rate",
        //     sorter: (a, b) => a.rate.length - b.rate.length,
        // },
        {
            title: "Action",
            dataIndex: "Action",
            render: (_, record) => (
                <div className="action-table-data">
                    <div className="edit-delete-action">
                        {/* <OverlayTrigger
                            placement="top"
                            overlay={renderproceeTooltip}
                        >
                            <Link className="me-2 p-2"
                                to="#"
                                onClick={() => handleProceed(record.shaid, record.vdaid, record.faid)}
                                data-bs-toggle="modal"
                                data-bs-target="#Direct-category"
                                style={{ color: 'green' }}>
                                <i data-feather="arrow-right-circle" className="feather-arrow-right-circle"></i>
                            </Link>
                        </OverlayTrigger> */}
                        <OverlayTrigger
                            placement="top"
                            overlay={renderEditTooltip}
                        >
                            <Link
                                className=""
                                to="#"
                                onClick={() => {
                                    OnEdit(record.cropid, record.faid);
                                    const modal = document.getElementById("Direct-category");
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
                        {record.cropid && (
                            <OverlayTrigger
                                placement="top"
                                overlay={renderDeleteTooltip}
                            >
                                <Link
                                    to="#"
                                    onClick={() => {
                                        handleDelete(record.cropid);
                                    }}
                                >
                                    <Trash2 className="feather-trash-2" />
                                </Link>
                            </OverlayTrigger>
                        )}
                    </div>
                </div>
            ),
        },
    ];
    const dataSource = [];
    const renderDeleteTooltip = (props) => (
        <Tooltip id="Delete-tooltip" {...props}>
            Delete
        </Tooltip>
    );
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

    const handleBack = () => {
        const from = location.state?.from;

        if (from == "MasterIndex") {
            navigate("/MasterIndex");
        } else if (from == "ShedulerIndex") {
            navigate("/ShedulerIndex");
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

                        <button
                            className="btn btn-added"
                            onClick={() => {
                                const modal = document.getElementById("Direct-category");

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
                            Add Inward
                        </button>

                    </div>
                    <div className="page-btn">
                        {/* <Link to={route.ShedulerIndex} className="btn btn-secondary">
                            <ArrowLeft className="me-2" />
                            Back
                        </Link> */}
                        <button onClick={handleBack}
                            className="btn btn-secondary">
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
                                dataSource={CropData}
                                scroll={{ x: 'max-content' }} // 👈 Enables horizontal scroll on small screens
                                pagination={{ pageSize: 10 }} // Optional: adds pagination
                            />
                        </div>
                    </div>
                </div>

                <Brand />
            </div>
            {/* <AddCropCollect SHAID={selectedData.shaid} VDAID={selectedData.vdaid} FAID={selectedData.faid} /> */}
            <AddDirectInward CROPID={selectedData1.cropid} FAID={selectedData1.faid} onRefresh={refreshData} />
        </div>
    );
};
export default DirectInward;
