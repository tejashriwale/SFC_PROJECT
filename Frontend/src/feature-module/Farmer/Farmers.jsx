
import React, { useState, useEffect } from 'react';
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
import axios from 'axios';
import { baseUrl } from "../../core/json/custom";
import {
    ArrowLeft,
    ChevronUp,
    Edit,
    PlusCircle,
    RotateCcw,
    Trash2,
} from "feather-icons-react/build/IconComponents";
import { jsPDF } from 'jspdf';
import autoTable from 'jspdf-autotable';
import { saveAs } from 'file-saver';
import ExcelJS from "exceljs";
import marathiFontBase64 from "../../style/fonts/NotoSansDevanagari";
import { useLocation } from 'react-router-dom';
import { getUserData } from '../../Context/UserData';
const Farmers = () => {
    const { search } = useLocation();
    const query = new URLSearchParams(search);
    const id = query.get('id');
    console.log('Query ID:', id);
    const dispatch = useDispatch();
    const data = useSelector((state) => state.toggle_header);
    const route = all_routes;
    const navigate = useNavigate();
    const [searchQuery, setSearchQuery] = useState("");
    const { userdetail } = getUserData();
    console.log(userdetail, "userdetailuserdetail")
    const isRestricted = userdetail.LANGUAGE === "en";
    const onEditClick = (faid) => {
        navigate(route.AddFarmers, { state: { FAID: faid } });
    };
    const handleSearch = (event) => {
        setSearchQuery(event.target.value);
        try {
            const payload = {
                "faid": "%",
                "keyword": event.target.value,
                companyid: userdetail?.companyID || "",
                deptid: userdetail?.departmentID || "",
            }
            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };

            axios({
                method: "POST",
                url: baseUrl.Url + "/backend/api/GET_GateFarmer/_Search",
                data: JSON.stringify(payload),
                headers: headers,
            })
                .then((response) => {
                    if (response.status != 200) throw new Error("Failed to send otp");
                    console.log("response", response.data);
                    setFarmer(response.data);
                })
        } catch (error) {
            console.error("Error while searching Gate Entry data:", error);
        }
    };
    const columns = isRestricted
        ? [
            {
                title: "Name",
                dataIndex: "fname",
                width: 100,
            },
            {
                title: "Mobile",
                dataIndex: "fcontactno",
                width: 100
            },
            {
                title: (
                    <div className="d-flex justify-content-center">
                        <OverlayTrigger placement="top" overlay={<Tooltip id="action-tooltip">Action</Tooltip>}>
                            <span>Action</span>
                        </OverlayTrigger>
                    </div>
                ),
                dataIndex: "action",
                width: 100,
                render: (_, record) => (
                    <div className="action-table-data">
                        <div className="edit-delete-action">
                            <OverlayTrigger placement="top" overlay={<Tooltip id="edit-tooltip">Edit</Tooltip>}>
                                <a className="me-2 p-2" onClick={() => { onEditClick(record.faid) }}>
                                    <Edit className="feather-edit" />
                                </a>
                            </OverlayTrigger>
                            <OverlayTrigger placement="top" overlay={<Tooltip id="delete-tooltip">Delete</Tooltip>}>
                                <Link className="confirm-text p-2" to="#" onClick={() => showConfirmationAlert(record.faid)}>
                                    <Trash2 className="feather-trash-2" />
                                </Link>
                            </OverlayTrigger>
                        </div>
                    </div>
                ),
            },
        ]
        : [
            {
                title: "नाव",
                dataIndex: "fname",
                width: 150,
                ellipsis: true,
            },
            {
                title: "आधार नंबर",
                dataIndex: "faddharno",
                width: 180,
                ellipsis: true,
            },
            {
                title: "पॅन नंबर",
                dataIndex: "fpanno",
                width: 150,
                ellipsis: true,
            },
            {
                title: "मोबाईल नंबर",
                dataIndex: "fcontactno",
                width: 150,
                ellipsis: true,
            },
            {
                title: "पिनकोड",
                dataIndex: "fpincode",
                width: 120,
                ellipsis: true,
            },
            {
                title: (
                    <div className="d-flex justify-content-center">
                        <OverlayTrigger placement="top" overlay={<Tooltip id="action-tooltip">कृती</Tooltip>}>
                            <span>कृती</span>
                        </OverlayTrigger>
                    </div>
                ),
                dataIndex: "action",
                width: 100,
                render: (_, record) => (
                    <div className="action-table-data">
                        <div className="edit-delete-action">
                            <OverlayTrigger placement="top" overlay={<Tooltip id="edit-tooltip">Edit</Tooltip>}>
                                <a className="me-2 p-2" onClick={() => { onEditClick(record.faid) }}>
                                    <Edit className="feather-edit" />
                                </a>
                            </OverlayTrigger>
                            <OverlayTrigger placement="top" overlay={<Tooltip id="delete-tooltip">Delete</Tooltip>}>
                                <Link className="confirm-text p-2" to="#" onClick={() => showConfirmationAlert(record.faid)}>
                                    <Trash2 className="feather-trash-2" />
                                </Link>
                            </OverlayTrigger>
                        </div>
                    </div>
                ),
            },
        ];
    const MySwal = withReactContent(Swal);
    const showConfirmationAlert = (faid) => {
        MySwal.fire({
            title: isRestricted ? "Are you sure?" : "तुम्हाला खात्री आहे का?",
            text: isRestricted
                ? "This action cannot be undone!"
                : "हे कृती उलटवता येणार नाही!",
            showCancelButton: true,
            confirmButtonColor: "#00ff00",
            confirmButtonText: isRestricted ? "Yes, delete!" : "हो, हटवा!",
            cancelButtonColor: "#ff0000",
            cancelButtonText: isRestricted ? "Cancel" : "रद्द करा",
            allowOutsideClick: false,
            allowEscapeKey: false,

        }).then((result) => {
            if (result.isConfirmed) {
                // MySwal.fire({
                //   title: "Deleted!",
                //   text: "Your file has been deleted.",
                //   className: "btn btn-success",
                //   confirmButtonText: "OK",
                //   customClass: {
                //     confirmButton: "btn btn-success",
                //   },
                // });
                OndeleteFarmer(faid);
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
    useEffect(() => {
        const handleShortcut = (e) => {
            if (e.ctrlKey && e.key === 'a' || e.key === 'A') {
                e.preventDefault();
                navigate(route.AddFarmers);
            }
            if (e.ctrlKey && e.key === 'e') {
                e.preventDefault();
                navigate(route.Farmers);
            }
        };
        window.addEventListener('keydown', handleShortcut);
        return () => {
            window.removeEventListener('keydown', handleShortcut);
        };
    }, [navigate]);

    const [farmer, setFarmer] = useState([]);
    useEffect(() => {
        try {
            const payload = {
                "keyword": "%",
                "faid": "%",
                companyid: userdetail?.companyID || "",
                deptid: userdetail?.departmentID || "",
            }
            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };

            axios({
                method: "POST",
                url: baseUrl.Url + "/backend/api/GET_GateFarmer",
                data: JSON.stringify(payload),
                headers: headers,
            })
                .then((response) => {
                    if (response.status != 200) throw new Error("Failed to Fetching Data");
                    const DATA = response.data;
                    setFarmer(DATA);
                })
        } catch (error) {
            console.error("Error fetching Access Right Data:", error);
        }
    }, []);

    const OndeleteFarmer = async (faid) => {
        try {
            const payload = {
                "faid": faid,

                "companyid": userdetail?.companyID ? userdetail.companyID : "",
                "deptid": userdetail?.departmentID || "",
            }
            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };

            axios({
                method: "POST",
                url: baseUrl.Url + "/backend/api/SP_DeleteSFarmer",
                data: JSON.stringify(payload),
                headers: headers,
            })
                .then((response) => {
                    if (response.status != 200) throw new Error("Failed to Fetching Data");
                    MySwal.fire({
                        title: response.data[0].responseCode === "FAILURE"
                            ? (isRestricted ? "Deletion Not Allowed" : "हटवणे परवानगी नाही")
                            : (isRestricted ? "Deleted!" : "हटवले!"),
                        text: response.data[0].responseMessage,  // dynamic, keeping as-is
                        icon: response.data[0].responseCode === "FAILURE" ? "error" : "success",
                        confirmButtonText: isRestricted ? "OK" : "ठीक आहे",
                        customClass: {
                            confirmButton: response.data[0].responseCode === "FAILURE"
                                ? "btn btn-danger"
                                : "btn btn-success"
                        },
                    });
                    try {
                        const payload = {
                            "keyword": "%",
                            "faid": "%",
                            companyid: userdetail?.companyID || "",
                            deptid: userdetail?.departmentID || "",
                        }
                        const headers = {
                            "Content-Type": "application/json",
                            Accept: "*/*",
                        };

                        axios({
                            method: "POST",
                            url: baseUrl.Url + "/backend/api/GET_GateFarmer",
                            data: JSON.stringify(payload),
                            headers: headers,
                        })
                            .then((response) => {
                                if (response.status != 200) throw new Error("Failed to Fetching Data");
                                const DATA = response.data;
                                setFarmer(DATA);
                            })

                    } catch (error) {
                        console.error("Error fetching Access Right Data:", error);
                    }
                })

        } catch (error) {
            console.error("Error fetching Access Right Data:", error);
        }
    }

    const generatePDF = (farmer) => {
        const doc = new jsPDF();
        const pageWidth = doc.internal.pageSize.getWidth();

        const cleanBase64 = marathiFontBase64.replace(/^data:font\/ttf;base64,/, "");
        doc.addFileToVFS("NotoSansDevanagari.ttf", cleanBase64);
        doc.addFont("NotoSansDevanagari.ttf", "NotoSansDevanagari", "normal");

        const title = isRestricted ? "Farmer Information Report" : "शेतकरी माहिती अहवाल";

        doc.setFont("NotoSansDevanagari", "normal");
        doc.setFontSize(16);
        doc.text(title, (pageWidth - doc.getTextWidth(title)) / 2, 20);

        const tableColumn = [
            isRestricted
                ? ["Name", "Aadhaar No", "PAN No", "Mobile No", "Pincode"]
                : ["नाव", "आधार नंबर", "पॅन नंबर", "मोबाईल नंबर", "पिनकोड"]
        ];

        const tableRows = farmer.map((item) => [
            item.fname || "",
            item.faddharno || "",
            item.fpanno || "",
            item.fcontactno || "",
            item.fpincode || "",
        ]);

        const sanitizedTableRows = tableRows.map(row => row.map(cell => (cell ? cell.toString() : "")));

        autoTable(doc, {
            startY: 30,
            head: tableColumn,
            body: sanitizedTableRows,
            headStyles: {
                font: "NotoSansDevanagari",
                fontSize: 14,
                fontStyle: "normal",
                fillColor: [0, 102, 204],
                textColor: 255,
            },
            styles: {
                font: "helvetica",
                fontSize: 12,
            },
            alternateRowStyles: { fillColor: [240, 240, 240] },
        });
        window.open(doc.output("bloburl"), "_blank");
    };

    const exportFarmerDataToExcel = async () => {
        try {
            const workbook = new ExcelJS.Workbook();
            const worksheetTitle = isRestricted ? "Farmer Information Report" : "शेतकरी माहिती अहवाल";
            const worksheet = workbook.addWorksheet(worksheetTitle);

            const headingRow = worksheet.addRow([worksheetTitle]);
            headingRow.getCell(1).font = { bold: true, size: 16 };
            headingRow.getCell(1).alignment = { horizontal: "center", vertical: "middle" };
            worksheet.mergeCells("A1:E1");

            const headers = isRestricted
                ? ["Name", "Aadhaar No", "PAN No", "Mobile No", "Pincode"]
                : ["नाव", "आधार नंबर", "पॅन नंबर", "मोबाईल नंबर", "पिनकोड"];

            const headerRow = worksheet.addRow(headers);
            headerRow.eachCell((cell) => {
                cell.font = { bold: true, color: { argb: "FFFFFF" } };
                cell.fill = { type: "pattern", pattern: "solid", fgColor: { argb: "808080" } };
                cell.alignment = { horizontal: "center", vertical: "middle" };
            });

            const columnWidths = [20, 20, 20, 20, 20];
            columnWidths.forEach((width, index) => {
                worksheet.getColumn(index + 1).width = width;
            });

            farmer.forEach(({ fname, faddharno, fpanno, fcontactno, fpincode }) => {
                const row = worksheet.addRow([fname, faddharno, fpanno, fcontactno, fpincode]);
                row.eachCell((cell) => {
                    cell.alignment = { horizontal: "center", vertical: "middle" };
                });
            });

            const buffer = await workbook.xlsx.writeBuffer();
            const data = new Blob([buffer], { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });
            saveAs(data, "Farmer_Report.xlsx");

        } catch (error) {
            console.error("Error generating the Excel file:", error);
        }
    };

    return (
        <div className="page-wrapper">
            <div className="content">
                <div className="page-header">
                    <div className="add-item d-flex">
                        <div className="page-title">
                            <h3>{isRestricted ? "Farmer Info" : "शेतकरी माहिती"} </h3>
                        </div>
                    </div>
                    <ul className="table-top-head">
                        <li>
                            <OverlayTrigger placement="top" overlay={renderTooltip}>
                                <Link onClick={() => generatePDF(farmer)}>
                                    <ImageWithBasePath src="assets/img/icons/pdf.svg" alt="img" />
                                </Link>
                            </OverlayTrigger>
                        </li>
                        <li>
                            <OverlayTrigger placement="top" overlay={renderExcelTooltip}  >
                                <Link data-bs-toggle="tooltip" data-bs-placement="top" onClick={() => exportFarmerDataToExcel(farmer)} >
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
                        <Link to={route.AddFarmers} className="btn btn-added">
                            <PlusCircle className="me-2 iconsize" />
                            {isRestricted ? "Add" : "नवीन"}
                        </Link>
                    </div>
                    <div className="page-btn">
                        <Link to={
                            userdetail.accessPolicy.some(p => p.toUpperCase() === 'MASTERS')
                                ? route.MasterIndex
                                : route.VisitorIndex
                        } className="btn btn-secondary">
                            <ArrowLeft className="me-2" />
                            {isRestricted ? "Back" : "मागे"}
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
                                    style={{ height: '35px', padding: '5px' }}
                                />
                                {searchQuery && (
                                    <span className="input-group-text" style={{ cursor: 'pointer', height: '35px', padding: '5px' }}
                                        onClick={() => setSearchQuery('')}>
                                        <i className="fa fa-times"></i>
                                    </span>
                                )}
                                <span className="input-group-text" style={{ height: '35px', padding: '5px' }}>
                                    <i className="fa fa-search"></i>
                                </span>
                            </div>
                        </div>
                    </div>
                </div>

                <div className="card table-list-card">
                    <div className="card-body p-2">
                        <div className="table-responsive responsive-no-scroll">
                            <Table
                                columns={columns}
                                dataSource={farmer}
                                pagination={false}
                                scroll={false}
                            />
                        </div>
                    </div>
                </div>
                <Brand />
            </div>
        </div>
    );
};

export default Farmers;

