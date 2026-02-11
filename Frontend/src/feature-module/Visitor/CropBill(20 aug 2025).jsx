
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
import AddCropBill from "./AddCropBill";
const CropBill = () => {
    const [selectedData, setSelectedData] = useState({ cropid: null, });
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const data = useSelector((state) => state.toggle_header);
    const route = all_routes;
    const { isAuthenticated, userdetail } = getUserData();
    if (isAuthenticated == true) {
        console.log("user", userdetail);
        console.log("getUserData", getUserData);
    }
    const handleProceed = (cropid,) => {
        setSelectedData({ cropid: cropid, });
    };
    useEffect(() => {
        const handleShortcut = (e) => {
            if (e.ctrlKey && e.key === 'a') {
                e.preventDefault();
                navigate(route.AddItem);
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
    const [farmeName, setfarmeName] = useState([]);
    const [Date, setDate] = useState([]);
    const [MoblieNo, setMoblieNo] = useState([]);
    const [tabledata, settabledata] = useState([]);
    const [Crop, setCrop] = useState([]);
    useEffect(() => {
        if (selectedData.cropid) {
            fetchbill();
        }
    }, [selectedData.cropid]);
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
    const handlePrint = () => {
        const printContents = document.getElementById("printableArea1").innerHTML;
        const printWindow = window.open("", "", "height=800,width=1000");
        printWindow.document.write(`
    <html>
      <head>
        <title>Print Bill</title>
        <style>
          body { font-family: Arial, sans-serif; padding: 20px; }
          table { width: 100%; border-collapse: collapse; }
          th, td { padding: 8px; border: 1px solid #ccc; text-align: center; }
          th { background-color: #add8e6; }
          h3, h6 { margin: 10px 0; }
        </style>
      </head>
      <body>
        ${printContents}
      </body>
    </html>
  `);
        printWindow.document.close();
        printWindow.focus();
        printWindow.print();
        printWindow.close();
    };
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
    const renderproceeTooltip = (props) => (
        <Tooltip id="Edit-tooltip" {...props}>
            Proceed
        </Tooltip>
    );
    const openModal = (cropid) => {
        setSelectedData({ cropid })
        setTimeout(() => {
            setSelectedData(cropid);
        }, 10);
        console.log("setSelectedDatasetSelectedDatasetSelectedData", cropid)
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
            title: "Crop Area",
            dataIndex: "carea",
        },
        {
            title: "Rate(₹)",
            dataIndex: "rate",
            sorter: (a, b) => a.rate.length - b.rate.length,
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
                                onClick={() => handleProceed(record.cropid)}
                                data-bs-toggle="modal"
                                data-bs-target="#add-units-category"
                                style={{ color: 'green' }}>
                                <i data-feather="arrow-right-circle" className="feather-arrow-right-circle"></i>
                            </Link>
                        </OverlayTrigger>
                        <OverlayTrigger
                            placement="top"
                            overlay={<Tooltip id="delete-tooltip me-2">View</Tooltip>}
                        >
                            <Link
                                className="me-2 p-2"
                                to="#" data-bs-toggle="modal"
                                data-bs-target="#print-receipt1"
                                style={{ color: 'orange' }}
                                onClick={() => openModal(record.cropid)}
                            >
                                <i data-feather="eye" className="feather-eye"></i>
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
        const title = "Crop Bill ";
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
                        <Link to={route.CropBillIndex} className="btn btn-secondary">
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
                <div
                    className="modal fade modal-default"
                    id="print-receipt1"
                    data-bs-backdrop="false"
                    data-bs-keyboard="false"
                    aria-labelledby="print-receipt1"
                >
                    <div className="modal-dialog modal-dialog-centered" style={{ maxWidth: "400px" }}>
                        <div className="modal-content rounded-4 shadow border border-success" style={{ backgroundColor: "#f0f8ff" }}>
                            <div className="d-flex justify-content-end p-2">
                                <button
                                    type="button"
                                    className="close p-0"
                                    data-bs-dismiss="modal"
                                    aria-label="Close"
                                >
                                    <span aria-hidden="true">×</span>
                                </button>
                            </div>
                            <div className="modal-body" id="printableArea1" style={{ backgroundColor: "#f0f8ff" }}>
                                <div className="icon-head text-center mb-3">
                                    <h3 className="text-center" style={{ color: 'orange' }}>
                                        {userdetail?.departmentname}
                                    </h3>
                                </div>
                                <div className="tax-invoice" style={{ backgroundColor: "#f0f8ff" }}>
                                    <h6 className="text-center text-success">Farmer Bill</h6>
                                    <div className="row mb-3">
                                        <div className="col-sm-12 col-md-6">
                                            <div className="invoice-user-name">
                                                <span className="fw-bold " style={{ fontWeight: 'bold' }}>Farmer: </span>
                                                <span style={{ fontWeight: 'bold' }}>
                                                    {farmeName}
                                                </span>
                                            </div>
                                            <div className="invoice-user-name">
                                                <span className="fw-bold ">Mobile No: </span>
                                                <span style={{ fontWeight: 'bold' }}>
                                                    {MoblieNo}
                                                </span>
                                            </div>
                                        </div>
                                        <div className="col-sm-12 col-md-6">
                                            <div className="invoice-user-name">
                                                <span className="fw-bold ">Date: </span>
                                                <span style={{ fontWeight: 'bold' }}>
                                                    {Date}
                                                </span>
                                            </div>
                                            <div className="invoice-user-name">
                                                <span className="fw-bold ">Crop: </span>
                                                <span style={{ fontWeight: 'bold' }}>
                                                    {Crop}
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <table className="table-borderless w-100 table-fit">
                                    <thead style={{ backgroundColor: '#add8e6' }}>
                                        <tr>
                                            <th className="text-center">Sr.No</th>
                                            <th className="text-center">Crate </th>
                                            <th className="text-center">Rate </th>
                                            <th className="text-center">Weigth </th>
                                            <th className="text-end">Total </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {tabledata.length > 0 ? (
                                            tabledata.map((row, index) => (
                                                <tr key={index}>
                                                    <td className="text-center" style={{ color: '#333' }}><strong>{index + 1}</strong></td>
                                                    <td className="text-center" style={{ color: '#333' }}><strong>{row.cratecount}</strong></td>
                                                    <td className="text-center" style={{ color: '#333' }}><strong>{row.rate}</strong></td>
                                                    <td className="text-center" style={{ color: '#333' }}><strong>{row.weight}</strong></td>
                                                    <td className="text-end" style={{ color: '#333' }}>
                                                        <strong>{(parseFloat(row.rate || 0) * parseFloat(row.weight || 0)).toFixed(2)}</strong>
                                                    </td>
                                                </tr>
                                            ))
                                        ) : (
                                            <tr>
                                                <td colSpan="5" className="text-center">No Data Available</td>
                                            </tr>
                                        )}
                                    </tbody>
                                    <tfoot>
                                        <tr>
                                            <td colSpan="4" className="text-end"><strong>Total Amount:</strong></td>
                                            <td className="text-end" style={{ color: '#000' }}><strong>{totalAmount.toFixed(2)}</strong></td>
                                        </tr>
                                    </tfoot>
                                </table>
                                <div className="text-center invoice-bar mt-3">
                                    <Link to="#"
                                        className="btn btn-success"
                                        onClick={handlePrint}
                                    >
                                        Print Bill
                                    </Link>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <AddCropBill CROPID={selectedData.cropid} />
        </div>
    );
};

export default CropBill;



