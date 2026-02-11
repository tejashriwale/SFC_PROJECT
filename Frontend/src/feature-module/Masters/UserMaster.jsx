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
import axios from 'axios';
import { getUserData } from "../../Context/UserData";
import {
  ArrowLeft,
  ChevronUp,
  Edit,
  PlusCircle,
  RotateCcw,
  Trash2,
} from "feather-icons-react/build/IconComponents";
import { baseUrl } from "../../core/json/custom"
import ExcelJS from "exceljs";
import marathiFontBase64 from "../../style/fonts/NotoSansDevanagari"
import { saveAs } from 'file-saver';
import { jsPDF } from 'jspdf';
import autoTable from 'jspdf-autotable';

const UserMaster = () => {
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const [userData, setUserData] = useState([]);
  // const [loading, setLoading] = useState(true);
  const data = useSelector((state) => state.toggle_header);
  const route = all_routes;
  const onEditClick = (uaid) => {
    console.log("dsfsafsadfdsasafuaid", uaid)
    navigate(route.AddUserMaster, { state: { UAID: uaid } });
  };
  const { userdetail } = getUserData();
  const isRestricted = userdetail.LANGUAGE === "en";
  useEffect(() => {
    const handleShortcut = (e) => {
      if (e.ctrlUAID && e.UAID === 'a') {
        e.preventDefault();
        navigate(route.AddUser);
      }
      if (e.ctrlUAID && e.UAID === 'e') {
        e.preventDefault();
        navigate(route.MasterIndex);
      }
    };

    window.addEventListener('UAIDdown', handleShortcut);

    return () => {
      window.removeEventListener('UAIDdown', handleShortcut);
    };
  }, [navigate]);

  const columns = [
    {
      title: isRestricted ? "User Name" : "वापरकर्त्याचे नाव",
      dataIndex: "utitle",
    },
    {
      title: isRestricted ? "Email" : "ईमेल",
      dataIndex: "uemailaddress",
    },
    {
      title: isRestricted ? "Phone Number" : "फोन नंबर",
      dataIndex: "umobilenumber",
    },
    {
      title: isRestricted ? "Registration Date" : "दिनांक",
      dataIndex: "uregistrationdate",
    },
    {
      title: isRestricted ? "Status" : "स्थिती",
      dataIndex: "ustatusid",
      width: "3%",
      render: (status) => {
        const Isactive = status == 1;
        const badgeClass = Isactive
          ? "bg-danger text-white"
          : "bg-success text-white";
        const label = Isactive
          ? (isRestricted ? "Inactive" : "निष्क्रिय")
          : (isRestricted ? "Active" : "सक्रिय");

        return (
          <OverlayTrigger
            placement="top"
            overlay={<Tooltip id={`tooltip-${label}`}>{label}</Tooltip>}
          >
            <span
              className={`badge ${badgeClass} d-flex justify-content-center`}
              style={{ padding: "7px 12px", fontSize: "0.875rem" }}
            >
              {label}
            </span>
          </OverlayTrigger>
        );
      },
    },
    {
      title: isRestricted ? "Action" : "क्रिया",
      dataIndex: "action",
      render: (_, record) =>

        <div className="action-table-data">
          <div className="edit-delete-action">
            <OverlayTrigger
              placement="top"
              overlay={<Tooltip id="edit-tooltip">{isRestricted ? "Edit" : "संपादन करा"}</Tooltip>}
            >
              <a className="me-2 p-2" onClick={() => onEditClick(record.uaid)}>
                <Edit className="feather-edit" />
              </a>
            </OverlayTrigger>
          </div>
        </div>

    },
  ];


  const MySwal = withReactContent(Swal);

  // const showConfirmationAlert = (UAID) => {
  //   MySwal.fire({
  //     title: "Are you sure?",
  //     text: "You won't be able to revert this!",
  //     showCancelButton: true,
  //     confirmButtonColor: "#00ff00",
  //     confirmButtonText: "Yes, delete it!",
  //     cancelButtonColor: "#ff0000",
  //     cancelButtonText: "Cancel",
  //   }).then((result) => {
  //     if (result.isConfirmed) {
  //       OndeleteUser(UAID);
  //     } else {
  //       MySwal.close();
  //     }
  //   });
  // };

  useEffect(() => {
    const fetchUserData = async () => {

      try {
        const payload = {
          "uaid": "%",
          "keyword": "%"
          , "companyid": "CM001",
          // "deptid": userdetail?.departmentID ? userdetail.departmentID : "",
        }
        const headers = {
          "Content-Type": "application/json",
          Accept: "*/*",
        };

        axios({
          method: "POST",
          url: baseUrl.Url + "/backend/api/_GET_UserMasters_",
          data: JSON.stringify(payload),
          headers: headers,
        })
          .then((response) => {
            if (response.status != 200) throw new Error("Failed to send otp");
            console.log("response", response.data);
            const DATA = response.data;
            setUserData(DATA);
          })

      } catch (error) {
        console.error("Error fetching User Master data:", error);
      }

    };

    fetchUserData();
  }, []);

  const OnReloadData = () => {
    try {
      const payload = {
        "uaid": "%",
        "keyword": "%"
        , "companyid": "CM001",
        // "deptid": userdetail?.departmentID ? userdetail.departmentID : "",
      }
      const headers = {
        "Content-Type": "application/json",
        Accept: "*/*",
      };

      axios({
        method: "POST",
        url: baseUrl.Url + "/backend/api/_GET_UserMasters_",
        data: JSON.stringify(payload),
        headers: headers,
      })
        .then((response) => {
          if (response.status != 200) throw new Error("Failed to send otp");
          console.log("response", response.data);
          const DATA = response.data;
          setUserData(DATA);
        })

    } catch (error) {
      console.error("Error fetching User Master data:", error);
    }
  }

  // const dataSource = userData.map((user) => ({
  //   UAID: user.uaid,
  //   empname: user.empname,
  //   uemailaddress: user.uemailaddress,
  //   umobilenumber: user.umobilenumber,
  //   dname: user.dname,
  //   ustatusid: user.ustatusid,
  //   action: user,
  // }));
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

  // const OndeleteUser = async (UAID) => {
  //   try {
  //     const payload = { uaid: UAID };
  //     const headers = {
  //       "Content-Type": "application/json",
  //       Accept: "*/*",
  //     };

  //     const response = await axios.post(
  //       baseUrl.Url + "/backend/api/SP_DeleteUserMasters",
  //       JSON.stringify(payload),
  //       { headers }
  //     );

  //     if (response.status !== 200) throw new Error("Failed to Delete Data");

  //     const result = response.data[0];

  //     await MySwal.fire({
  //       title: result.responseCode === "FAILURE" ? "Deletion Not Allowed" : "Deleted!",
  //       text: result.responseMessage,
  //       icon: result.responseCode === "FAILURE" ? "error" : "success",
  //       confirmButtonText: "OK",
  //       customClass: {
  //         confirmButton: result.responseCode === "FAILURE" ? "btn btn-danger" : "btn btn-success",
  //       },
  //     });

  //     if (result.responseCode !== "FAILURE") {
  //       setUserData((prevState) =>
  //         prevState.filter((userData) => String(userData.uaid) !== String(UAID))
  //       );
  //     }

  //   } catch (error) {
  //     console.error("Error deleting account:", error);
  //   }
  // };




  const [searchQuery, setSearchQuery] = useState("");

  const handleSearch = (event) => {
    setSearchQuery(event.target.value);
    try {
      const payload =

      {
        "uaid": "%",
        "keyword": event.target.value
        , "companyid": "CM001",
      }
      const headers = {
        "Content-Type": "application/json",
        Accept: "*/*",
      };

      axios({
        method: "POST",
        url: baseUrl.Url + "/backend/api/_GET_UserMasters_/_Search",
        data: JSON.stringify(payload),
        headers: headers,
      })

        .then((response) => {
          if (response.status != 200) throw new Error("Failed to send otp");
          console.log("response", response.data);
          setUserData(response.data);
        })
    } catch (error) {
      console.error("Error while searching WareHouse data:", error);
    }
  };

  const generatePDF = (userData) => {
    const doc = new jsPDF();
    const pageWidth = doc.internal.pageSize.getWidth();

    const cleanBase64 = marathiFontBase64.replace(/^data:font\/ttf;base64,/, "");

    doc.addFileToVFS("NotoSansDevanagari.ttf", cleanBase64);
    doc.addFont("NotoSansDevanagari.ttf", "NotoSansDevanagari", "normal");
    doc.setFont("NotoSansDevanagari", "normal");
    doc.setFontSize(16);

    console.log(doc.getFontList());

    const title = " वापरकर्ता अहवाल";
    doc.text(title, (pageWidth - doc.getTextWidth(title)) / 2, 20);

    const tableColumn = [["वापरकर्त्याचे नाव", "ईमेल", "फोन नंबर", "भूमिका"]];

    const tableRows = userData.map((item) => [
      item.empname,
      item.uemailaddress,
      item.umobilenumber,
      item.dname
    ]);

    autoTable(doc, {
      startY: 30,
      head: tableColumn,
      body: tableRows,
      styles: { font: "NotoSansDevanagari", fontStyle: "normal", fontSize: 12 },
      headStyles: { fontStyle: "normal", fillColor: [0, 102, 204], textColor: 255, fontSize: 14 },
      alternateRowStyles: { fillColor: [240, 240, 240] },
      columnStyles: {
        0: {
          font: "normal", // Marathi font for "नाव"
          fontSize: 12
        },
        1: {
          font: "normal", // Marathi font for "तारीख"
          fontSize: 12
        },
        2: {
          font: "normal", // Marathi font for "तक्रारीचे शीर्षक"
          fontSize: 12
        }
      }
    });

    window.open(doc.output("bloburl"), "_blank");
  };

  const exportToExcel = async () => {
    try {
      const workbook = new ExcelJS.Workbook();
      const worksheet = workbook.addWorksheet("Acoounts Report");


      const headingRow = worksheet.addRow(["Acoounts Report"]);
      headingRow.getCell(1).font = { bold: true, size: 16 };
      headingRow.getCell(1).alignment = { horizontal: "center", vertical: "middle" };


      worksheet.mergeCells("A1:E1");


      const headers = ["वापरकर्त्याचे नाव", "ईमेल", "फोन नंबर", "वापरकर्ता भूमिका"];
      const headerRow = worksheet.addRow(headers);

      headerRow.eachCell((cell) => {
        cell.font = { bold: true, color: { argb: "FFFFFF" } };
        cell.fill = { type: "pattern", pattern: "solid", fgColor: { argb: "808080" } };
        cell.alignment = { horizontal: "center", vertical: "middle" };
      });

      const columnWidths = [40, 40, 40, 40];
      columnWidths.forEach((width, index) => {
        worksheet.getColumn(index + 1).width = width;
      });

      userData.forEach(({ empname, uemailaddress, umobilenumber, dname }) => {
        const row = worksheet.addRow([empname, uemailaddress, umobilenumber, dname]);
        row.eachCell((cell) => {
          cell.alignment = { horizontal: "center", vertical: "middle" };
        });
      });

      const buffer = await workbook.xlsx.writeBuffer();
      const data = new Blob([buffer], { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });
      saveAs(data, "Acoounts.xlsx");

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
              <h3>{isRestricted ? "User List" : "वापरकर्त्यांची यादी"}</h3>
              <h6>{isRestricted ? "User Master Record" : "वापरकर्ता मुख्य नोंद"}</h6>

            </div>
          </div>
          <ul className="table-top-head">
            <li>
              <OverlayTrigger placement="top" overlay={renderTooltip}>
                <Link onClick={() => generatePDF(userData)}>
                  <ImageWithBasePath src="assets/img/icons/pdf.svg" alt="Download PDF" />
                </Link>
              </OverlayTrigger>
            </li>
            <li>
              <OverlayTrigger placement="top" overlay={renderExcelTooltip}>
                <Link onClick={() => exportToExcel(userData)}>
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
            <Link to={route.AddUserMaster} className="btn btn-added">
              <PlusCircle className="me-2 iconsize" />{isRestricted ? "New User" : "नवीन वापरकर्ता"}
            </Link>
          </div>
          <div className="page-btn">
            <Link to={route.UserIndex} className="btn btn-secondary">
              <ArrowLeft className="me-2" />
              {isRestricted ? "Back" : "मागे"}
            </Link>
          </div>
        </div>
        <div className="search-container mb-2 mt-2">
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
            border: '1px solid #ddd',
            borderRadius: '8px',
            boxShadow: '0 2px 8px rgba(0,0,0,0.05)',
            marginBottom: '20px',
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
                dataSource={userData}
                scroll={{ x: 'max-content' }} // 👈 Enables horizontal scroll on narrow screens
                pagination={{ pageSize: 10 }} // Optional for better UX
                bordered // Optional: adds borders to table
              />
            </div>
          </div>
        </div>

        <Brand />
      </div>
    </div>
  );
};

export default UserMaster;
