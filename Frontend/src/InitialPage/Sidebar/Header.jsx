import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import FeatherIcon from "feather-icons-react";
import ImageWithBasePath from "../../core/img/imagewithbasebath";
import { Settings, User } from "react-feather";
import { all_routes } from "../../Router/all_routes";
// import "../../style/scss/layout/_header"
import { getUserData, updateUserData, clearUserData } from "../../Context/UserData";
import { useNavigate } from "react-router-dom";
import axios from "axios";
import { baseUrl } from "../../core/json/custom";
import Swal from "sweetalert2";
import withReactContent from "sweetalert2-react-content";
import { convertToCustomDate } from "../../feature-module/pages/login/signin";
import { FiLogOut } from 'react-icons/fi';
import { useGoogleTranslate } from './useGoogleTranslate';
import { WiDayLightning } from "weather-icons-react";
import { Dropdown } from "react-bootstrap";


const Header = () => {
  const isTranslateLoaded = useGoogleTranslate();
  const { userdetail } = getUserData();
  const route = all_routes;
  const [toggle, SetToggle] = useState(false);
  const [isFullscreen, setIsFullscreen] = useState(false);
  const isElementVisible = (element) => {
    return element.offsetWidth > 0 || element.offsetHeight > 0;
  };
  const isRestricted = userdetail?.LANGUAGE === "en" || "";
  const navigate = useNavigate();
  const [selectedLanguage, setSelectedLanguage] = useState(isRestricted ? 'en' : 'mr');
  const MySwal = withReactContent(Swal);

  // Convert APPDT "16 Apr 2026" to "YYYY-MM-DD" for date input
  const appDateToInputFormat = (appdt) => {
    if (!appdt) return '';
    const d = new Date(appdt);
    if (isNaN(d)) return '';
    const year = d.getFullYear();
    const month = String(d.getMonth() + 1).padStart(2, '0');
    const day = String(d.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`;
  };

  const [headerDate, setHeaderDate] = useState(appDateToInputFormat(userdetail?.APPDT));

  const handleHeaderDateChange = async (e) => {
    const newDateValue = e.target.value; // YYYY-MM-DD
    if (!newDateValue) return;

    const today = new Date().toISOString().split("T")[0];
    if (newDateValue > today) return;
    if (newDateValue === headerDate) return;

    const confirmResult = await MySwal.fire({
      icon: "question",
      title: isRestricted ? "Change Date?" : "तारीख बदलायची आहे का?",
      text: isRestricted
        ? `Do you want to change the application date to ${newDateValue}?`
        : `तुम्हाला अॅप्लिकेशन तारीख ${newDateValue} मध्ये बदलायची आहे का?`,
      showCancelButton: true,
      confirmButtonColor: "#3e5f68",
      cancelButtonColor: "#d33",
      confirmButtonText: isRestricted ? "Yes, Change" : "होय, बदला",
      cancelButtonText: isRestricted ? "No" : "नाही",
      allowOutsideClick: false,
      allowEscapeKey: false,
    });

    if (!confirmResult.isConfirmed) {
      e.target.value = headerDate;
      return;
    }

    const inputDate = new Date(newDateValue);
    const formattedDate = inputDate.toLocaleDateString("en-GB", {
      year: "numeric",
      month: "long",
      day: "numeric"
    });

    try {
      const payload = {
        date: formattedDate,
        companyid: userdetail?.companyID,
        branchid: userdetail?.departmentID
      };
      const headers = {
        "Content-Type": "application/json",
        Accept: "*/*",
      };
      const response = await axios.post(
        baseUrl.Url + "/backend/api/SP_UpdateApplicationDate",
        payload,
        { headers }
      );
      if (response.status === 200 && response.data[0]?.isSuccessful == 1) {
        const newAPPDT = convertToCustomDate(newDateValue, 0);
        const userData = getUserData();
        updateUserData({
          ...userData,
          userdetail: { ...userData.userdetail, APPDT: newAPPDT }
        });
        setHeaderDate(newDateValue);
        MySwal.fire({
          icon: "success",
          title: isRestricted ? "Success" : "यशस्वी",
          text: isRestricted ? "Application date updated for all users." : "अॅप्लिकेशन तारीख सर्व वापरकर्त्यांसाठी अपडेट केली.",
          confirmButtonText: "OK",
        });
      } else {
        MySwal.fire({
          icon: "warning",
          title: isRestricted ? "Attention" : "कृपया लक्ष द्या!",
          text: response.data[0]?.responseMessage || (isRestricted ? "Failed to update date." : "तारीख अपडेट करता आली नाही."),
          confirmButtonText: "OK",
        });
      }
    } catch (error) {
      console.error("Date update error:", error);
      MySwal.fire({
        icon: "error",
        title: isRestricted ? "Error" : "त्रुटी",
        text: isRestricted ? "Failed to update application date." : "अॅप्लिकेशन तारीख अपडेट करण्यात अयशस्वी.",
        confirmButtonText: "OK",
      });
    }
  };

  const waitForGoogleTranslateCombo = () => {
    return new Promise((resolve) => {
      const checkExist = setInterval(() => {
        const selectField = document.querySelector('.goog-te-combo');
        if (selectField) {
          clearInterval(checkExist);
          resolve(selectField);
        }
      }, 100);
    });
  };

  const handleLogout_1 = async () => {
    try {

      if (userdetail?.uaid) {
        await axios.post(`${baseUrl.Url}/backend/api/SP_UpadateUserLogin`, {
          uaid: userdetail.uaid,
          islogin: false,
        }, {
          headers: {
            "Content-Type": "application/json",
            Accept: "*/*"
          }
        });
      }
    } catch (error) {
      console.error("❌ Logout failed:", error);
    } finally {
      // always allow the user to leave, even if the server call failed
      clearUserData();
      navigate("/signin");
    }

  }

  const waitForComboAndChangeLanguage = async (lang) => {
    setSelectedLanguage(lang);
    if (!isTranslateLoaded) {
      console.warn("Google Translate not yet ready");
      return;
    }

    const maxWait = 5000; // max wait 5 seconds
    const interval = 100;
    let waited = 0;

    const intervalId = setInterval(() => {
      const selectField = document.querySelector('.goog-te-combo');
      if (selectField) {
        clearInterval(intervalId);
        selectField.value = lang;
        selectField.dispatchEvent(new Event('change'));
      }

      waited += interval;
      if (waited >= maxWait) {
        clearInterval(intervalId);
        console.error("❌ Failed to find Google Translate dropdown.");
      }
    }, interval);
  };

  function waitForIframe(callback, timeout = 5000) {
    const interval = 100;
    let waited = 0;

    const check = setInterval(() => {
      const frame = document.querySelector("iframe.goog-te-menu-frame");
      if (frame) {
        clearInterval(check);
        callback(frame);
      } else {
        waited += interval;
        if (waited >= timeout) {
          clearInterval(check);
          console.error("❌ Google Translate iframe not found.");
        }
      }
    }, interval);
  }

  const handleLanguageChange = (lang) => {
    setSelectedLanguage(lang);

    waitForIframe((frame) => {
      const innerDoc = frame.contentDocument || frame.contentWindow.document;
      const langLinks = innerDoc.querySelectorAll("a.goog-te-menu2-item");

      langLinks.forEach(link => {
        if (lang === 'en' && link.innerText.includes('English')) {
          link.click();
        }
        if (lang === 'mr' && link.innerText.includes('Marathi')) {
          link.click();
        }
      });
    });
  };

  const getLanguageText = () => {
    switch (selectedLanguage) {
      case 'mr': return 'मराठी';
      case 'en':
      default: return 'English';
    }
  };

  useEffect(() => {
    const handleMouseover = (e) => {
      e.stopPropagation();

      const body = document.body;
      const toggleBtn = document.getElementById("toggle_btn");

      if (
        body.classList.contains("mini-sidebar") &&
        isElementVisible(toggleBtn)
      ) {
        e.preventDefault();
      }
    };

    document.addEventListener("mouseover", handleMouseover);

    return () => {
      document.removeEventListener("mouseover", handleMouseover);
    };
  }, []);
  const handleLogout = () => {
    if (!userdetail) {
      navigate("/no-access");
      return;
    }

    const userAccess = userdetail.accessPolicy.find((policy) =>
      [
        "DASHBOARD",
        "SALEDASH",
        "AUCTION",
        "PURCHASES",
        "LEDGER",
        "HRM",
        "VYAPARI",
        "GATEENTRY",
        "BILL",
        "VAPDASH",
        "PRECOOLING",
        "MASTERS",
        "DAYEND",
        "VISITOR",
        "SHEDULER",
        "CBILL",
        "SFCREPORT"
      ].includes(policy)
    );

    switch (userAccess) {
      case "DASHBOARD":
        navigate("/admin-dashboard");
        break;
      case "SALEDASH":
        navigate("/sales-dashboard");
        break;
      case "AUCTION":
        navigate(route.AuctionIndex);
        break;
      case "PURCHASES":
        navigate(route.PurchaseIndex);
        break;
      case "LEDGER":
        navigate(route.LedgerIndex);
        break;
      case "HRM":
        navigate(route.HrmIndex);
        break;
      case "VYAPARI":
        navigate(route.VyapariIndex);
        break;
      case "GATEENTRY":
        navigate("/GateEntryIndex");
        break;
      case "BILL":
        navigate(route.MarketBillIndex);
        break;
      case "VAPDASH":
        navigate(route.VyapariDashboardIndex);
        break;
      case "PRECOOLING":
        navigate(route.CoolingIndex);
        break;
      case "MASTERS":
        navigate(route.MasterIndex);
        break;
      case "SHEDULER":
        navigate(route.ShedulerIndex);
        break;
      case "VISITOR":
        navigate(route.VisitorIndex);
        break;
      case "CBILL":
        navigate(route.CropBillIndex);
        break;
      case "DAYEND":
        navigate(route.DayEndIndex);
        break;
      case "SFCREPORT":
        navigate(route.SfcReportIndex);
        break;
      default:
        navigate("/no-access");
        break;
    }
  };


  useEffect(() => {
    const handleMouseover = (e) => {
      e.stopPropagation();

      const body = document.body;
      const toggleBtn = document.getElementById("toggle_btn");

      if (
        body.classList.contains("mini-sidebar") &&
        isElementVisible(toggleBtn)
      ) {
        e.preventDefault();
      }
    };

    document.addEventListener("mouseover", handleMouseover);

    return () => {
      document.removeEventListener("mouseover", handleMouseover);
    };
  }, []);
  useEffect(() => {
    const handleFullscreenChange = () => {
      setIsFullscreen(
        document.fullscreenElement ||
        document.mozFullScreenElement ||
        document.webkitFullscreenElement ||
        document.msFullscreenElement
      );
    };

    document.addEventListener("fullscreenchange", handleFullscreenChange);
    document.addEventListener("mozfullscreenchange", handleFullscreenChange);
    document.addEventListener("webkitfullscreenchange", handleFullscreenChange);
    document.addEventListener("msfullscreenchange", handleFullscreenChange);

    return () => {
      document.removeEventListener("fullscreenchange", handleFullscreenChange);
      document.removeEventListener(
        "mozfullscreenchange",
        handleFullscreenChange
      );
      document.removeEventListener(
        "webkitfullscreenchange",
        handleFullscreenChange
      );
      document.removeEventListener(
        "msfullscreenchange",
        handleFullscreenChange
      );
    };
  }, []);
  const handlesidebar = () => {
    document.body.classList.toggle("mini-sidebar");
    SetToggle((current) => !current);
  };
  const expandMenu = () => {
    document.body.classList.remove("expand-menu");
  };
  const expandMenuOpen = () => {
    document.body.classList.add("expand-menu");
  };
  // const sidebarOverlay = () => {
  //   document?.querySelector(".main-wrapper")?.classList?.toggle("slide-nav");
  //   document?.querySelector(".sidebar-overlay")?.classList?.toggle("opened");
  //   document?.querySelector("html")?.classList?.toggle("menu-opened");
  // };
  const sidebarOverlay = () => {
    const mainWrapper = document.querySelector(".main-wrapper");
    const overlay = document.querySelector(".sidebar-overlay");
    const html = document.querySelector("html");

    mainWrapper?.classList?.toggle("slide-nav");
    overlay?.classList?.toggle("opened");
    html?.classList?.toggle("menu-opened");

    // Push dummy state to history when sidebar opens (mobile only)
    if (window.innerWidth < 768 && mainWrapper?.classList.contains("slide-nav")) {
      history.pushState(null, "", location.href);
    }
  };

  const closeSidebar = () => {
    const mainWrapper = document.querySelector(".main-wrapper");
    const overlay = document.querySelector(".sidebar-overlay");
    const html = document.querySelector("html");

    mainWrapper?.classList?.remove("slide-nav");
    overlay?.classList?.remove("opened");
    html?.classList?.remove("menu-opened");
  };


  useEffect(() => {
    const submenuItems = document.querySelectorAll(".submenu-item");
    const overlay = document.querySelector(".sidebar-overlay");

    const handleMobileClose = () => {
      if (window.innerWidth < 768) {
        closeSidebar();
      }
    };

    submenuItems.forEach((item) => {
      item.addEventListener("click", handleMobileClose);
    });

    overlay?.addEventListener("click", handleMobileClose);

    // Handle browser back button
    const handlePopState = () => {
      const isSidebarOpen = document.querySelector(".main-wrapper")?.classList.contains("slide-nav");

      if (isSidebarOpen) {
        closeSidebar();

      }
    };
    window.addEventListener("popstate", handlePopState);

    return () => {
      window.removeEventListener("popstate", handlePopState);
    };
  }, [closeSidebar]);

  let pathname = location.pathname;

  const exclusionArray = [
    "/reactjs/template/dream-pos/index-three",
    "/reactjs/template/dream-pos/index-one",
  ];
  if (exclusionArray.indexOf(window.location.pathname) >= 0) {
    return "";
  }

  const toggleFullscreen = (elem) => {
    elem = elem || document.documentElement;
    if (
      !document.fullscreenElement &&
      !document.mozFullScreenElement &&
      !document.webkitFullscreenElement &&
      !document.msFullscreenElement
    ) {
      if (elem.requestFullscreen) {
        elem.requestFullscreen();
      } else if (elem.msRequestFullscreen) {
        elem.msRequestFullscreen();
      } else if (elem.mozRequestFullScreen) {
        elem.mozRequestFullScreen();
      } else if (elem.webkitRequestFullscreen) {
        elem.webkitRequestFullscreen(Element.ALLOW_KEYBOARD_INPUT);
      }
    } else {
      if (document.exitFullscreen) {
        document.exitFullscreen();
      } else if (document.msExitFullscreen) {
        document.msExitFullscreen();
      } else if (document.mozCancelFullScreen) {
        document.mozCancelFullScreen();
      } else if (document.webkitExitFullscreen) {
        document.webkitExitFullscreen();
      }
    }
  };
  function waitForTranslateCombo(callback) {
    const interval = setInterval(() => {
      const selectField = document.querySelector('.goog-te-combo');
      if (selectField) {
        clearInterval(interval);
        callback(selectField);
      }
    }, 100); // check every 100ms
  }

  const doTranslation = (langText) => {
    const frame = document.querySelector("iframe.goog-te-menu-frame");
    if (!frame) {
      console.error("Google Translate iframe not loaded yet.");
      return;
    }
    const innerDoc = frame.contentDocument || frame.contentWindow.document;
    const langLinks = innerDoc.querySelectorAll("a.goog-te-menu2-item");
    for (let i = 0; i < langLinks.length; i++) {
      if (langLinks[i].innerText.includes(langText)) {
        langLinks[i].click();
        break;
      }
    }
  };

  return (
    <>
      <div className="header">
        {/* <div id="google_translate_element"></div> */}


        {/* Logo */}
        <div
          className={`header-left ${toggle ? "" : "active"}`}
          onMouseLeave={expandMenu}
          onMouseOver={expandMenuOpen}
        >
          <div
            className="logo logo-normal d-flex align-items-center justify-content-center"
            onClick={handleLogout}
            style={{ padding: '8px' }}
          >
            <ImageWithBasePath
              src="assets/img/sfclogo.png"
              alt="SFC Logo"
              style={{ width: '60px', height: 'auto', objectFit: 'contain' }}
            />
          </div>

          <div className="logo logo-white" onClick={handleLogout} >
            <ImageWithBasePath src="assets/img/logo-white.png" alt="img" />
          </div>
          <div className="logo-small" onClick={handleLogout}>
            <ImageWithBasePath src="assets/img/logo-small.png" alt="img" />
          </div>
          <Link
            id="toggle_btn"
            to="#"
            style={{
              display:
                pathname.includes("tasks") || pathname.includes("pos")
                  ? "none"
                  : pathname.includes("compose")
                    ? "none"
                    : "",
            }}
            onClick={handlesidebar}
          >
            <FeatherIcon icon="chevrons-left" className="feather-16" />
          </Link>
        </div>
        {/* /Logo */}
        <Link
          id="mobile_btn"
          className="mobile_btn"
          to="#"
          onClick={sidebarOverlay}
        >
          <span className="bar-icon">
            <span />
            <span />
            <span />
          </span>
        </Link>
        {/* Header Menu */}
        <ul className="nav user-menu mt-2">
          {/* Search */}
          <li className="nav-item nav-searchinputs">
            <div
              className="top-nav-search"
              style={{
                backgroundColor: '#e3eef2', // lighter shade matchingrgb(88, 129, 143)2
                padding: '6px 12px',
                borderRadius: '8px',
                color: '#2c3e50', // dark text for contrast
                fontWeight: '500',
                fontSize: '1.2rem',
              }}
            >
              <b style={{ color: '#3e5f68', display: 'flex', alignItems: 'center', gap: '6px' }}>
                {isRestricted ? 'Date' : 'दिनांक'} :
                <input
                  type="date"
                  value={headerDate}
                  min={headerDate}
                  max={new Date().toISOString().split("T")[0]}
                  onChange={handleHeaderDateChange}
                  style={{
                    border: '1px solid #3e5f68',
                    borderRadius: '4px',
                    padding: '2px 6px',
                    fontSize: '1rem',
                    color: '#3e5f68',
                    backgroundColor: 'transparent',
                    cursor: 'pointer',
                    outline: 'none',
                  }}
                />
              </b>
            </div>
          </li>
          <li
            className="nav-item"
            style={{
              position: 'absolute',
              left: '50%',
              transform: 'translateX(-60%)',
              fontSize: '1.8rem',
              fontWeight: '300',
              fontFamily: "'Times New Roman', Georgia, Garamond, serif", // <-- Added font family
              letterSpacing: '1px',
              userSelect: 'none',
              cursor: 'default',
              color: 'white',
              textShadow: `
                  0.5px 0.5px 0 #fff,      
                  -0.5px -0.5px 0 white  
              `,
              whiteSpace: 'nowrap',
              padding: '5px 45px',
              borderRadius: '8px',
            }}
          >
            {userdetail?.departmentname}
          </li>

          {/* /Select Store */}

          {/* Flag */}
          <li className="nav-item dropdown has-arrow flag-nav">
            <Link
              data-bs-toggle="dropdown"
              to="#"
              role="button"
              className="nav-link dropdown-toggle"
            >
              <span style={{ fontWeight: 'bold', marginRight: '5px' }}>
                {getLanguageText()}
              </span>
              <ImageWithBasePath src="assets/img/flags/in.png" alt="Language" height={16} />
            </Link>

            <div className="dropdown-menu dropdown-menu-right p-2 shadow" style={{ minWidth: '160px', borderRadius: '10px' }}>
              <button
                onClick={() => {
                  handleLanguageChange('en');
                }}
                className={`btn btn-sm w-100 mb-2 ${selectedLanguage === 'en' ? 'btn-success' : 'btn-outline-success'}`}
              >
                English
              </button>
              <button
                onClick={() => {
                  handleLanguageChange('mr');
                }}
                className={`btn btn-sm w-100 ${selectedLanguage === 'mr' ? 'btn-primary' : 'btn-outline-primary'}`}
              >
                मराठी
              </button>


            </div>
          </li>




          {/* /Flag */}
          {/* /Flag */}
          {/* <li className="nav-item nav-item-box">
            <Link
              to="#"
              id="btnFullscreen"
              onClick={() => toggleFullscreen()}
              className={isFullscreen ? "Exit Fullscreen" : "Go Fullscreen"}
            >
              {/* <i data-feather="maximize" /> 
              <FeatherIcon icon="maximize" />
            </Link>
          </li> */}
          {/* <li className="nav-item nav-item-box">
            <Link to="/email">
              <FeatherIcon icon="mail" />
              <span className="badge rounded-pill">1</span>
            </Link>
          </li> */}
          {/* <li className="nav-item dropdown nav-item-box">
            <Link
              to="#"
              className="dropdown-toggle nav-link"
              data-bs-toggle="dropdown"
            >
              {/* <i data-feather="bell" /> 
              <FeatherIcon icon="bell" />
              <span className="badge rounded-pill">2</span>
            </Link>
            <div className="dropdown-menu notifications">
              <div className="topnav-dropdown-header">
                <span className="notification-title">Notifications</span>
                <Link to="#" className="clear-noti">
                  {" "}
                  Clear All{" "}
                </Link>
              </div>
              <div className="noti-content">
                <ul className="notification-list">
                  <li className="notification-message active">
                    <Link to="/activities">
                      <div className="media d-flex">
                        <span className="avatar flex-shrink-0">
                          <ImageWithBasePath
                            alt="img"
                            src="assets/img/profiles/avatar-02.jpg"
                          />
                        </span>
                        <div className="media-body flex-grow-1">
                          <p className="noti-details">
                            <span className="noti-title">John Doe</span> added
                            new task{" "}
                            <span className="noti-title">
                              Patient appointment booking
                            </span>
                          </p>
                          <p className="noti-time">
                            <span className="notification-time">
                              4 mins ago
                            </span>
                          </p>
                        </div>
                      </div>
                    </Link>
                  </li>
                  <li className="notification-message">
                    <Link to="/activities">
                      <div className="media d-flex">
                        <span className="avatar flex-shrink-0">
                          <ImageWithBasePath
                            alt="img"
                            src="assets/img/profiles/avatar-03.jpg"
                          />
                        </span>
                        <div className="media-body flex-grow-1">
                          <p className="noti-details">
                            <span className="noti-title">Tarah Shropshire</span>{" "}
                            changed the task name{" "}
                            <span className="noti-title">
                              Appointment booking with payment gateway
                            </span>
                          </p>
                          <p className="noti-time">
                            <span className="notification-time">
                              6 mins ago
                            </span>
                          </p>
                        </div>
                      </div>
                    </Link>
                  </li>
                  <li className="notification-message">
                    <Link to="/activities">
                      <div className="media d-flex">
                        <span className="avatar flex-shrink-0">
                          <ImageWithBasePath
                            alt="img"
                            src="assets/img/profiles/avatar-06.jpg"
                          />
                        </span>
                        <div className="media-body flex-grow-1">
                          <p className="noti-details">
                            <span className="noti-title">Misty Tison</span>{" "}
                            added{" "}
                            <span className="noti-title">Domenic Houston</span>{" "}
                            and <span className="noti-title">Claire Mapes</span>{" "}
                            to project{" "}
                            <span className="noti-title">
                              Doctor available module
                            </span>
                          </p>
                          <p className="noti-time">
                            <span className="notification-time">
                              8 mins ago
                            </span>
                          </p>
                        </div>
                      </div>
                    </Link>
                  </li>
                  <li className="notification-message">
                    <Link to="/activities">
                      <div className="media d-flex">
                        <span className="avatar flex-shrink-0">
                          <ImageWithBasePath
                            alt="img"
                            src="assets/img/profiles/avatar-17.jpg"
                          />
                        </span>
                        <div className="media-body flex-grow-1">
                          <p className="noti-details">
                            <span className="noti-title">Rolland Webber</span>{" "}
                            completed task{" "}
                            <span className="noti-title">
                              Patient and Doctor video conferencing
                            </span>
                          </p>
                          <p className="noti-time">
                            <span className="notification-time">
                              12 mins ago
                            </span>
                          </p>
                        </div>
                      </div>
                    </Link>
                  </li>
                  <li className="notification-message">
                    <Link to="/activities">
                      <div className="media d-flex">
                        <span className="avatar flex-shrink-0">
                          <ImageWithBasePath
                            alt="img"
                            src="assets/img/profiles/avatar-13.jpg"
                          />
                        </span>
                        <div className="media-body flex-grow-1">
                          <p className="noti-details">
                            <span className="noti-title">Bernardo Galaviz</span>{" "}
                            added new task{" "}
                            <span className="noti-title">
                              Private chat module
                            </span>
                          </p>
                          <p className="noti-time">
                            <span className="notification-time">
                              2 days ago
                            </span>
                          </p>
                        </div>
                      </div>
                    </Link>
                  </li>
                </ul>
              </div>
              <div className="topnav-dropdown-footer">
                <Link to="/activities">View all Notifications</Link>
              </div>
            </div>
          </li> */}
          {/* /Notifications */}
          {/* <li className="nav-item nav-item-box">
            <Link to="/general-settings">
              <FeatherIcon icon="settings" />
            </Link>
          </li> */}
          <li className="nav-item main-drop">
            <Dropdown align="end">
              <Dropdown.Toggle as="a" className="nav-link userset">
                <span className="user-info">
                  <span className="user-letter">
                    <ImageWithBasePath
                      src="assets/img/profiles/avator1.jpg"
                      alt="img"
                      className="img-fluid"
                    />
                  </span>
                  <span className="user-detail">
                    <span className="user-name">{userdetail?.username || "UserName"}</span>
                    <span className="user-role">Super Admin</span>
                  </span>
                </span>
              </Dropdown.Toggle>

              <Dropdown.Menu className="menu-drop-user p-2 shadow">
                <Dropdown.Item as={Link} to={route.profile}>
                  <User className="me-2" /> My Profile
                </Dropdown.Item>
                <Dropdown.Item as={Link} to={route.generalsettings}>
                  <Settings className="me-2" /> Settings
                </Dropdown.Item>
                <Dropdown.Divider />
                <Dropdown.Item onClick={handleLogout_1}>
                  <ImageWithBasePath
                    src="assets/img/icons/log-out.svg"
                    alt="img"
                    className="me-2"
                  />
                  Logout
                </Dropdown.Item>
              </Dropdown.Menu>
            </Dropdown>
          </li>

        </ul>
        {/* /Header Menu */}
        {/* Mobile Menu */}
        <div className="dropdown mobile-user-menu">
          <div onClick={handleLogout} className="logout-button" title="Logout">
            <FiLogOut size={24} />
          </div>
          <Link
            to="#"
            className="nav-link dropdown-toggle"
            data-bs-toggle="dropdown"
            aria-expanded="false"
          >
            <i className="fa fa-ellipsis-v" />
          </Link>
          <div className="dropdown-menu dropdown-menu-right">
            <Link className="dropdown-item" to="profile">
              My Profile
            </Link>
            <Link className="dropdown-item" to="generalsettings">
              Settings
            </Link>
            <Link className="dropdown-item" to="signin">
              Logout
            </Link>
          </div>
        </div>
        {/* /Mobile Menu */}
      </div>
    </>
  );
};

export default Header;
