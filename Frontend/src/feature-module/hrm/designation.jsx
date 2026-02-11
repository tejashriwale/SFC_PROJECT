import React, { useState, useEffect } from 'react'
import { OverlayTrigger, Tooltip } from 'react-bootstrap';
import ImageWithBasePath from '../../core/img/imagewithbasebath';
import { Link, useNavigate } from 'react-router-dom';
import { ChevronUp, PlusCircle, RotateCcw, Users, Trash2, Edit, ArrowLeft } from 'feather-icons-react/build/IconComponents';
import { useDispatch, useSelector } from 'react-redux';
import withReactContent from 'sweetalert2-react-content';
import Swal from 'sweetalert2';
import AddDesignation from './AddDesignation';
import { setToogleHeader } from '../../core/redux/action';
import axios from 'axios';
import { baseUrl } from "../../core/json/custom";
import { all_routes } from "../../Router/all_routes";
import { getUserData } from '../../Context/UserData';
import { jsPDF } from 'jspdf';
import autoTable from 'jspdf-autotable';
import { saveAs } from "file-saver";
import ExcelJS from "exceljs";
import Table from "../../core/pagination/datatable";

const Designation = () => {
  const { userdetail } = getUserData();
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const route = all_routes;
  const data = useSelector((state) => state.toggle_header);
  const [designations, setDesignations] = useState([]);

  const [selectedData, setSelectedData] = useState({ daid: null });

  // ⬇️ fetch function
  const fetchDesignations = async () => {

    try {
      const payload = {
        "daid": "%",
        "companyid": userdetail?.companyID || "",
        "deptid": userdetail?.departmentID || "",
        keyword: "%"
      };

      const headers = {
        "Content-Type": "application/json",
        Accept: "*/*",
      };

      const response = await axios.post(
        baseUrl.Url + "/backend/api/GET_HRMDesignation",
        JSON.stringify(payload),
        { headers }
      );

      if (response.status !== 200) throw new Error("Failed to fetch data");

      setDesignations(response.data);
    } catch (error) {
      console.error("Error fetching data:", error);
    }
  };

  // ⬇️ call fetch once on mount
  useEffect(() => {
    fetchDesignations();
  }, []);


  const handleDelete = async (daid) => {
    try {
      const payload = {
        "daid": daid,
        "companyid": userdetail?.companyID || "",
        "deptid": userdetail?.departmentID || "",
      }
      const headers = {
        "Content-Type": "application/json",
        Accept: "*/*",
      };

      axios({
        method: "POST",
        url: baseUrl.Url + "/backend/api/SP_DeleteHRMDesignation",
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
          "daid": "%",
          "companyid": userdetail?.companyID || "",
          "deptid": userdetail?.departmentID || "",
          keyword: "%"
        };

        const headers = {
          "Content-Type": "application/json",
          Accept: "*/*",
        };

        axios({
          method: "POST",
          url: baseUrl.Url + "/backend/api/GET_HRMDesignation",
          data: JSON.stringify(payload),
          headers: headers,
        })
          .then((response) => {
            const DATA = response.data;
            setDesignations(DATA);
          });

      } catch (error) {
        console.error('get data Error:', error);
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
  const showConfirmationAlert = (daid) => {
    MySwal.fire({
      title: 'Are you sure?',
      text: 'You won\'t be able to revert this!',
      showCancelButton: true,
      confirmButtonColor: '#00ff00',
      confirmButtonText: 'Yes, delete it!',
      cancelButtonColor: '#ff0000',
      cancelButtonText: 'Cancel',
    }).then((result) => {
      if (result.isConfirmed) {
        handleDelete(daid)
      } else {
        MySwal.close();
      }
    });
  };
  const openModal = (daid) => {

    setSelectedData({ daid });
    console.log("daid daid daid", daid)

    // setModalShow(true);
  };

  const RefreshData = () => {

    setSelectedData({ daid: null })
    console.log("doneeeeeeeeeeeee")

  }

  useEffect(() => {
    const handleShortcut = (e) => {
      if (e.ctrlKey && e.key === 'a') {
        e.preventDefault();
        navigate(route.designation);
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

  const generatePDF = (DesignationList) => {
    const doc = new jsPDF();
    const pageWidth = doc.internal.pageSize.getWidth();
    const pageHeight = doc.internal.pageSize.getHeight();

    doc.setFontSize(12);
    doc.setFont("Helvetica", "bold");
    const title = "Sale challan Report";
    const titleWidth = doc.getTextWidth(title);

    const borderMargin = 10;
    doc.rect(borderMargin, borderMargin, pageWidth - borderMargin * 2, pageHeight - borderMargin * 2);

    doc.text(title, (pageWidth - titleWidth) / 2, 20);
    doc.setLineWidth(0.5);
    doc.line((pageWidth - titleWidth) / 2, 22, (pageWidth + titleWidth) / 2, 22);

    doc.setFontSize(13);
    doc.setFont("Helvetica", "normal");
    let yPosition = 15;

    const tableColumn = ["Designation", "Date", "Status"];
    const tableRows = DesignationList.map((item) => [
      item.dname,
      item.ddate,
      item.dstatus ? "Active" : "Inactive",
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
      const worksheet = workbook.addWorksheet("HRM Designation Report");

      const headingRow = worksheet.addRow(["HRM Designation Report"]);
      headingRow.getCell(1).font = { bold: true, size: 16 };
      headingRow.getCell(1).alignment = { horizontal: "center", vertical: "middle" };

      worksheet.mergeCells("A1:C1");

      const headers = ["Designation", "Date", "Status",];
      const headerRow = worksheet.addRow(headers);

      headerRow.eachCell((cell) => {
        cell.font = { bold: true, color: { argb: "FFFFFF" } };
        cell.fill = { type: "pattern", pattern: "solid", fgColor: { argb: "808080" } };
        cell.alignment = { horizontal: "center", vertical: "middle" };
      });

      const columnWidths = [15, 20, 25,]; // Adjust widths as needed
      columnWidths.forEach((width, index) => {
        worksheet.getColumn(index + 1).width = width;
      });

      designations.forEach(({ dname, ddate, dstatus, }) => {
        const row = worksheet.addRow([dname, ddate, dstatus,]);
        row.eachCell((cell) => {
          cell.alignment = { horizontal: "center", vertical: "middle" };
        });
      });

      const buffer = await workbook.xlsx.writeBuffer();
      const data = new Blob([buffer], { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });
      saveAs(data, "DesignationReport.xlsx");

    } catch (error) {
      console.error("Error generating the Excel file:", error);
    }
  };

  const [searchQuery, setSearchQuery] = useState("");

  const handleSearch = (e) => {
    setSearchQuery(e.target.value.toLowerCase());
  };

  const filteredData = designations.filter((item) =>
    item.dname.toLowerCase().includes(searchQuery)
  );

  const columns = [
    {
      title: (
        <OverlayTrigger placement="top" overlay={<Tooltip id="dname-tooltip">Designation</Tooltip>}>
          <div>Designation</div>
        </OverlayTrigger>
      ),
      dataIndex: "dname",
      key: "dname",
      render: (text) => (
        <OverlayTrigger placement="top" overlay={<Tooltip id="dname-tooltip">{text}</Tooltip>}>
          <div style={{ textAlign: "left" }}>{text}</div>
        </OverlayTrigger>
      ),
    },
    {
      title: (
        <OverlayTrigger placement="top" overlay={<Tooltip id="ddate-tooltip">Date</Tooltip>}>
          <div>Date</div>
        </OverlayTrigger>
      ),
      dataIndex: "ddate",
      key: "ddate",
      render: (text) => (
        <OverlayTrigger placement="top" overlay={<Tooltip id="ddate-tooltip">{text}</Tooltip>}>
          <div style={{ textAlign: "left" }}>{text}</div>
        </OverlayTrigger>
      ),
    },
    {
      title: (
        <OverlayTrigger placement="top" overlay={<Tooltip id="dstatus-tooltip">Status</Tooltip>}>
          <div>Status</div>
        </OverlayTrigger>
      ),
      dataIndex: "dstatus",
      key: "dstatus",
      render: (status) => (
        <span
          className={`badge ${status === 1 ? "badge-linesuccess" : status === 0 ? "badge-linedanger" : "badge-warning"
            }`}
        >
          <Link to="#">{status === 1 ? "Active" : status === 0 ? "Inactive" : "Unknown"}</Link>
        </span>
      ),
    },
    {
      title: (
        <OverlayTrigger placement="top" overlay={<Tooltip id="action-tooltip">Action</Tooltip>}>
          <div>Action</div>
        </OverlayTrigger>
      ),
      key: "action",
      render: (_, record) => (
        <div className="action-table-data">
          <div className="edit-delete-action">
            {/* Edit */}
            <OverlayTrigger placement="top" overlay={<Tooltip>Edit</Tooltip>}>
              <Link
                to="#"
                className="me-2 p-2"
                // data-bs-toggle="modal"
                // data-bs-target="#Adddesignation"
                // onClick={() => openModal(record.daid)}



                onClick={(e) => {
                  e.preventDefault();
                  openModal(record.daid);


                  const modal = document.getElementById("Adddesignation");

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

            <OverlayTrigger placement="top" overlay={<Tooltip>Delete</Tooltip>}>
              <Link
                to="#"
                className="p-2"
                onClick={() => showConfirmationAlert(record.daid)}
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
              <h4>Designation</h4>
              <h6>Manage your designation</h6>
            </div>
          </div>
          <ul className="table-top-head">
            <li>
              <OverlayTrigger placement="top" overlay={renderTooltip}>
                <Link onClick={() => generatePDF(designations)}>
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
              // data-bs-target="#Adddesignation"

              onClick={() => {

                const modal = document.getElementById("Adddesignation");

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
              Add New Designation
            </Link>
          </div>
          <div className="page-btn">
            <Link to={route.HrmIndex} className="btn btn-secondary">
              <ArrowLeft className="me-2" /> Back to Index
            </Link>
          </div>
        </div>
        {/* /product list */}
        <div className="card table-list-card">
          <div className="card-body">
            <div className="table-top table-top-new mb-3 d-flex justify-content-between align-items-center">
              <h6>
                <Users /> Total Employees
              </h6>
            </div>
            <div className="table-responsive responsive-no-scroll">
              <Table
                columns={columns}
                dataSource={filteredData}
                rowKey="daid"
                pagination={{ pageSize: 10 }}
              />
            </div>
          </div>
        </div>
        {/* /product list */}
      </div>
      <AddDesignation DAID={selectedData.daid} refreshList={fetchDesignations} onRefresh={RefreshData} />
    </div>

  )
}

export default Designation

