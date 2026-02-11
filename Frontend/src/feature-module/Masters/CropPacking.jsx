import React, { useState, useEffect } from 'react'
import { OverlayTrigger, Tooltip } from 'react-bootstrap';
import ImageWithBasePath from '../../core/img/imagewithbasebath';
import { Link, useNavigate } from 'react-router-dom';
import { ChevronUp, PlusCircle, RotateCcw, Users, Trash2, Edit, ArrowLeft } from 'feather-icons-react/build/IconComponents';
import { useDispatch, useSelector } from 'react-redux';
import withReactContent from 'sweetalert2-react-content';
import Swal from 'sweetalert2';
import { setToogleHeader } from '../../core/redux/action';
import axios from 'axios';
import { baseUrl } from "../../core/json/custom";
import { all_routes } from "../../Router/all_routes";
import { getUserData } from '../../Context/UserData';
import { jsPDF } from 'jspdf';
import autoTable from 'jspdf-autotable';
import Table from "../../core/pagination/datatable";
import { saveAs } from "file-saver";
import ExcelJS from "exceljs";
import AddCropPacking from './AddCropPacking';
const CropPacking = () => {
    const { userdetail } = getUserData();
    const navigate = useNavigate();
    const dispatch = useDispatch();
    const route = all_routes;
    const data = useSelector((state) => state.toggle_header);
    const [Material, setMaterial] = useState([]);


    const [selectedData, setSelectedData] = useState({ maid: null });
    // useEffect(() => {
    //   try {
    //     const payload = {
    //       "maid": "%",
    //       "keyword": "%",
    //       "companyid": userdetail?.companyID || "",
    //       "deptid": userdetail?.departmentID || "",
    //     }

    //     const headers = {
    //       "Content-Type": "application/json",
    //       Accept: "*/*",
    //     };

    //     axios({
    //       method: "POST",
    //       url: baseUrl.Url + "/backend/api/GET_PackingMaterial",
    //       data: JSON.stringify(payload),
    //       headers: headers,
    //     })
    //       .then((response) => {
    //         if (response.status != 200) throw new Error("Failed to Fetching Data");
    //         const DATA = response.data;
    //         setMaterial(DATA);
    //       })

    //   } catch (error) {
    //     console.error("Error fetching Access Right Data:", error);
    //   }

    // }, []);

    const fetchMaterialData = () => {
        const payload = {
            "maid": "%",
            "keyword": "%",
            "companyid": userdetail?.companyID || "",
            "deptid": userdetail?.departmentID || "",
        };

        const headers = {
            "Content-Type": "application/json",
            Accept: "*/*",
        };

        axios.post(baseUrl.Url + "/backend/api/GET_PackingMaterial", payload, { headers })
            .then((response) => {
                if (response.status !== 200) throw new Error("Failed to fetch data");
                setMaterial(response.data);
            })
            .catch((error) => {
                console.error("Error fetching data:", error);
            });
    };

    useEffect(() => {
        fetchMaterialData();
    }, []);

    const handleDelete = async (maid) => {
        try {
            const payload = {
                "maid": maid,
                "companyid": userdetail?.companyID || "",
                "deptid": userdetail?.departmentID || "",
            }
            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };

            axios({
                method: "POST",
                url: baseUrl.Url + "/backend/api/SP_DeletePackingMaterial",
                data: JSON.stringify(payload),
                headers: headers,
            })

            Swal.fire({
                icon: "success",
                title: "Deleted!",
                text: "Your record has been successfully deleted.",
                confirmButtonText: "OK",
            });

            try {

                const payload = {
                    "maid": "%",
                    "keyword": "%",
                    "companyid": userdetail?.companyID || "",
                    "deptid": userdetail?.departmentID || "",
                };

                const headers = {
                    "Content-Type": "application/json",
                    Accept: "*/*",
                };

                axios({
                    method: "POST",
                    url: baseUrl.Url + "/backend/api/GET_PackingMaterial",
                    data: JSON.stringify(payload),
                    headers: headers,
                })
                    .then((response) => {
                        const DATA = response.data;
                        setMaterial(DATA);
                    });

            } catch (error) {
                console.error('get data Error:', error);
            }

        } catch (error) {
            console.error("Submission Error:", error);
            Swal.fire({
                icon: "error",
                title: "Error",
                text: "Failed to save the information. Please try again.",
                confirmButtonText: "OK"
            });
        }
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
        <Tooltip id="refresh-tooltip" {...props}>
            Collapse
        </Tooltip>
    );

    const MySwal = withReactContent(Swal);
    const showConfirmationAlert = (maid) => {
        MySwal.fire({
            title: 'Are you sure?',
            text: 'This action cannot be undone!',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#00ff00',
            confirmButtonText: 'Yes, delete it!',
            cancelButtonColor: '#ff0000',
            cancelButtonText: 'Cancel'
        }).then((result) => {
            if (result.isConfirmed) {
                handleDelete(maid)
            } else {
                MySwal.close();
            }
        });
    };
    const openModal = (maid) => {
        setSelectedData({ maid });
        // setModalShow(true);
    };

    const RefreshData = () => {

        setSelectedData({ maid: null })
        console.log("doneeeeeeeeeeeee")

    }

    useEffect(() => {
        const handleShortcut = (e) => {
            if (e.ctrlKey && e.key === 'a') {
                e.preventDefault();
                // navigate(route.designation);
                const modal = document.getElementById("Addmaterial");

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

    const generatePDF = (MaterialList) => {
        const doc = new jsPDF();
        const pageWidth = doc.internal.pageSize.getWidth();
        const pageHeight = doc.internal.pageSize.getHeight();

        doc.setFontSize(12);
        doc.setFont("Helvetica", "bold");
        const title = "Paking Material Report";
        const titleWidth = doc.getTextWidth(title);

        const borderMargin = 10;
        doc.rect(borderMargin, borderMargin, pageWidth - borderMargin * 2, pageHeight - borderMargin * 2);

        doc.text(title, (pageWidth - titleWidth) / 2, 20);
        doc.setLineWidth(0.5);
        doc.line((pageWidth - titleWidth) / 2, 22, (pageWidth + titleWidth) / 2, 22);

        doc.setFontSize(13);
        doc.setFont("Helvetica", "normal");
        let yPosition = 15;

        const tableColumn = ["Type", "Count", "Quantity"];
        const tableRows = MaterialList.map((item) => [
            item.mtype,
            item.count,
            item.msize,
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
        doc.save("Report.pdf");
    };

    const exportToExcel = async () => {
        try {
            const workbook = new ExcelJS.Workbook();
            const worksheet = workbook.addWorksheet("Paking Material Report");

            const headingRow = worksheet.addRow(["Paking Material Report"]);
            headingRow.getCell(1).font = { bold: true, size: 16 };
            headingRow.getCell(1).alignment = { horizontal: "center", vertical: "middle" };

            worksheet.mergeCells("A1:C1");

            const headers = ["Type", "Count", "Quantity"];
            const headerRow = worksheet.addRow(headers);

            headerRow.eachCell((cell) => {
                cell.font = { bold: true, color: { argb: "FFFFFF" } };
                cell.fill = { type: "pattern", pattern: "solid", fgColor: { argb: "808080" } };
                cell.alignment = { horizontal: "center", vertical: "middle" };
            });

            const columnWidths = [15, 20, 25,];
            columnWidths.forEach((width, index) => {
                worksheet.getColumn(index + 1).width = width;
            });

            Material.forEach(({ mtype, count, msize, }) => {
                const row = worksheet.addRow([mtype, count, msize,]);
                row.eachCell((cell) => {
                    cell.alignment = { horizontal: "center", vertical: "middle" };
                });
            });

            const buffer = await workbook.xlsx.writeBuffer();
            const data = new Blob([buffer], { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });
            saveAs(data, "Packing Material Report.xlsx");

        } catch (error) {
            console.error("Error generating the Excel file:", error);
        }
    };
    const columns = [
        {
            title: "Type",
            dataIndex: "mtype",
            // sorter: (a, b) => a.categoryname.length - b.categoryname.length,
        },


        // {
        //   title: "मापन एकक",
        //   dataIndex: "uom",
        //   render: (text) => (
        //     <span className="badge badge-linesuccess">
        //       <Link to="#"> {text}</Link>
        //     </span>
        //   ),
        //   sorter: (a, b) => a.cstatus.length - b.cstatus.length,
        // },
        {
            title: "Packing Size",
            dataIndex: "msize",
            // render: (text) => (
            //   <span className="badge badge-linesuccess">
            //     <Link to="#"> {text}</Link>
            //   </span>
            // ),
            // sorter: (a, b) => a.count.length - b.count.length,
        },
        // {
        //   title: (
        //     <OverlayTrigger
        //       placement="top"
        //       overlay={<Tooltip id="types-status">प्रमाण</Tooltip>}
        //     >
        //       <div>Status</div>
        //     </OverlayTrigger>
        //   ),
        //   dataIndex: "cstatus",
        //   render: (text) => (
        //     <span className={`badge ${text == true
        //       ? "badge-linesuccess" : text == false
        //         ? "badge-linedanger" : "badge-warning"}`}>
        //       <Link to="#"> {text == true ? "Active" : text == false
        //         ? "Inactive" : "Unknown"}
        //       </Link>
        //     </span>

        //   ),
        //   sorter: (a, b) => a.cstatus.length - b.cstatus.length,
        //   width: 200,
        // },

        // {
        //     title: "UOM",
        //     dataIndex: "uom",
        //     // sorter: (a, b) => a.Quntity.length - b.Quntity.length,
        // },
        {


            title: (
                <OverlayTrigger placement="top" overlay={<Tooltip >Action</Tooltip>}>
                    <div className="text-center">Action</div>
                </OverlayTrigger>
            ),

            dataIndex: "actions",
            key: "actions",
            render: (_, record) => (
                <div className="action-table-data">
                    <div className="edit-delete-action">
                        <OverlayTrigger
                            placement="top"
                            overlay={<Tooltip id="edit-tooltip">Edit</Tooltip>}

                        >
                            <Link className="me-2 p-2"
                                to="#"
                                // data-bs-toggle="modal"
                                // data-bs-target="#Addmaterial"
                                // onClick={() => openModal(record.maid)}

                                onClick={(e) => {
                                    e.preventDefault();
                                    openModal(record.maid);


                                    const modal = document.getElementById("Addmaterial");

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
                                <Edit className="feather-edit" />
                            </Link>

                        </OverlayTrigger>
                        <OverlayTrigger
                            placement="top"
                            overlay={<Tooltip id="delete-tooltip">Delete</Tooltip>}
                        >
                            <Link
                                className="confirm-text me-2 p-2"
                                to="#"
                                onClick={() => showConfirmationAlert(record.maid)}

                            >
                                <Trash2 className="feather-trash-2" />
                            </Link>
                        </OverlayTrigger>

                    </div>
                </div>
            ),
        },
    ];
    return (
        <div className="page-wrapper">
            <div className="content">
                <div className="page-header">
                    <div className="add-item d-flex">
                        <div className="page-title">
                            <h4>Add Material</h4>
                            <h6>Manage the materials</h6>
                        </div>
                    </div>
                    <ul className="table-top-head">
                        <li>
                            <OverlayTrigger placement="top" overlay={renderTooltip}>
                                <Link onClick={() => generatePDF(Material)}>
                                    <ImageWithBasePath src="assets/img/icons/pdf.svg" alt="img" />
                                </Link>
                            </OverlayTrigger>
                        </li>
                        <li>
                            <OverlayTrigger placement="top" overlay={renderExcelTooltip}>
                                <Link data-bs-toggle="tooltip" data-bs-placement="top" onClick={exportToExcel}>
                                    <ImageWithBasePath src="assets/img/icons/excel.svg" alt="img" />
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
                                    onClick={() => { dispatch(setToogleHeader(!data)) }}
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
                            // data-bs-target="#Addmaterial"


                            onClick={() => {

                                const modal = document.getElementById("Addmaterial");

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
                            Add
                        </Link>
                    </div>
                    <div className="page-btn">
                        <Link to={route.MasterIndex} className="btn btn-secondary">
                            <ArrowLeft className="me-2" /> Back
                        </Link>
                    </div>
                </div>
                {/* /product list */}
                {/* <div className="card table-list-card">
          <div className="card-body pb-0">
            <div className="table-top table-top-new">
              <div className="search-set mb-0">
                <div className="total-employees">
                  <h6>
                    <Users />
                    Total Employees
                    <span>21</span>
                  </h6>
                </div>
                <div className="search-input">
                  <Link to="#" className="btn btn-searchset">
                    <i data-feather="search" className="feather-search" />
                  </Link>
                  <input type="search" className="form-control" />
                </div>
              </div>
            </div>

            <div className="table-responsive">
              <table className="table  datanew">
                <thead>
                  <tr>
                    <th>Add Material </th>
                    <th>Date</th>
                    <th>Created On</th>
                    <th>Total Members</th>
                    <th>Status</th>
                    <th className="no-sort">Action</th>
                  </tr>
                </thead>
                <tbody>
                  {designations.map((item, index) => (
                    <tr key={index}>
                      <td>{item.dname}</td>
                      <td>{item.ddate}</td>
                      <td>{item.createdOn}</td>
                      <td>{item.totalMembers}</td>
                      <td>
                        <span className={`badge ${item.dstatus === 1 ? "badge-linesuccess" : item.dstatus === 0 ? "badge-linedanger"
                          : "badge-warning"
                          }`}>
                          <Link to="#"> {item.dstatus == 1 ? "Active" : item.dstatus == 0 ? "Inactive" : "Unknown"}</Link>
                        </span>
                      </td>
                      <td className="action-table-data">
                        <div className="edit-delete-action">

                          <OverlayTrigger
                            placement="top"
                            overlay={<Tooltip id="edit-tooltip">Edit</Tooltip>}
                          >
                            <Link className="me-2 p-2"
                              to="#"
                              data-bs-toggle="modal"
                              data-bs-target="#Adddematerial"
                              onClick={() => openModal(item.daid)}
                            >
                              <Edit className="feather-edit" />
                            </Link>
                          </OverlayTrigger>

                          <Link className="confirm-text p-2" to="#">
                            <i
                              data-feather="trash-2"
                              className="feather-trash-2"
                              onClick={() => showConfirmationAlert(item.daid)}
                            ></i>
                            <Trash2 className="feather-trash-2" />
                          </Link>
                        </div>
                      </td>
                    </tr>
                  ))}
                </tbody>

              </table>

            </div>
          </div>
        </div> */}
                {/* <div className="card table-list-card">

                    <div className="card-body">
                        <div className="table-responsive">
                            <Table columns={columns} dataSource={Material} />
                        </div>
                    </div>
                </div> */}
                <div className="card table-list-card">
                    <div className="card-body p-2">
                        <div style={{ overflowX: 'auto', WebkitOverflowScrolling: 'touch' }}>
                            <Table
                                columns={columns}
                                dataSource={Material}
                                pagination={false}
                                scroll={{ x: 800 }}
                                style={{ minWidth: 800 }}
                            />
                        </div>
                    </div>
                </div>
                {/* /product list */}
            </div>
            <AddCropPacking MAID={selectedData.maid} onSuccess={fetchMaterialData} onRefresh={RefreshData} />
        </div>

    )
}

export default CropPacking

