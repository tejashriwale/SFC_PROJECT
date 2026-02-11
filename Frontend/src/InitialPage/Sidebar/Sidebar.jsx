import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import * as FeatherIcons from "react-feather"; // Import all icons dynamically
import CollapsedSidebar from "./collapsedSidebar";
import { getUserData } from "../../Context/UserData";

const Sidebar = () => {
  const [activeSubmenu, setActiveSubmenu] = useState(null);
  const { userdetail } = getUserData();
  console.log(userdetail, "userdetailuserdetail")
  const isRestricted = userdetail?.LANGUAGE === "en" || "";
  useEffect(() => {
    const storedSubmenu = localStorage.getItem("activeSubmenu");
    if (storedSubmenu) {
      setActiveSubmenu(storedSubmenu);
    }
  }, []);

  const toggleSubmenu = (submenuName) => {
    const newActiveSubmenu = activeSubmenu === submenuName ? null : submenuName;
    setActiveSubmenu(newActiveSubmenu);
    localStorage.setItem("activeSubmenu", newActiveSubmenu || "");
  };

  const renderIcon = (iconName) => {
    const IconComponent = FeatherIcons[iconName]; // Get icon dynamically
    return IconComponent ? <IconComponent /> : null;
  };
  const accessPolicy = userdetail?.accessPolicy || "";
  // Convert accessPolicy to uppercase for normalization.
  const normalizedAccess =
    typeof accessPolicy === "string"
      ? accessPolicy.toUpperCase().split(",")
      : Array.isArray(accessPolicy)
        ? accessPolicy.map((a) => a.toUpperCase())
        : [];

  const hasAccess = (requiredAccess) => {
    if (!requiredAccess) return true;
    return normalizedAccess.includes(requiredAccess.toUpperCase());
  };

  const handleRestrictedClick = (e) => {
    e.preventDefault();
    setModalMessage("You do not have access rights to open this page.");
    setShowModal(true);
  };


  return (
    <div className="sidebar" style={{ backgroundColor: "#E5E1DA" }}>
      {/* Main Section */}
      {hasAccess("DASHBOARD") && ( // Check access for Main section
        <div className="submenu-items">
          <div className="submenu-item">
            <Link to="/admin-dashboard" onClick={() => toggleSubmenu("main")} className="menu-link">
              <div className="menu-text">
                {renderIcon("Home")} <span>मुख्य</span>
              </div>
              <div className="arrow">
                {activeSubmenu === "main" ? renderIcon("ChevronDown") : renderIcon("ChevronRight")}
              </div>
            </Link>
            {activeSubmenu === "main" && (
              <div className="submenu">
                {hasAccess("ADMINDASH") && <Link to="/admin-dashboard">{renderIcon("Grid")} प्रशासन डॅशबोर्ड</Link>}
                {hasAccess("SALEDASH") && <Link to="/sales-dashboard">{renderIcon("BarChart2")} विक्री डॅशबोर्ड</Link>}
                {/* {hasAccess("CHAT") && <Link to="/chat">{renderIcon("MessageCircle")} Chat</Link>} */}
              </div>
            )}
          </div>
        </div>
      )}

      {hasAccess("DAYEND") && ( // Check access for Masters
        <div className="submenu-items">
          <div className="submenu-item">
            <Link to="/DayEndIndex" onClick={() => toggleSubmenu("dayend")} className="menu-link">
              <div className="menu-text">
                {renderIcon("Database")} <span>दैनंदिन समाप्ती</span>
              </div>
              <div className="arrow">
                {activeSubmenu === "dayend" ? renderIcon("ChevronDown") : renderIcon("ChevronRight")}
              </div>
            </Link>
            {activeSubmenu === "dayend" && (
              <div className="submenu">
                {hasAccess("DAYEND") && <Link to="/DayEnd">{renderIcon("Tool")} दैनंदिन समाप्ती</Link>}

              </div>
            )}
          </div>
        </div>
      )}
      {/* 
      {hasAccess("VISITOR") && ( // Check access for Masters
        <div className="submenu-items">
          <div className="submenu-item">
            <Link to="/VisitorIndex" onClick={() => toggleSubmenu("dayend")} className="menu-link">
              <div className="menu-text">
                {renderIcon("Database")} <span>विझिटर</span>
              </div>
              <div className="arrow">
                {activeSubmenu === "dayend" ? renderIcon("ChevronDown") : renderIcon("ChevronRight")}
              </div>
            </Link>
            
          </div>
        </div>
      )} */}

      {/* {hasAccess("SHEDULER") && ( 
        <div className="submenu-items">
          <div className="submenu-item">
            <Link to="/ShedulerIndex" onClick={() => toggleSubmenu("dayend")} className="menu-link">
              <div className="menu-text">
                {renderIcon("Database")} <span>पिक संकलन वेळापत्रक </span>
              </div>
              <div className="arrow">
                {activeSubmenu === "dayend" ? renderIcon("ChevronDown") : renderIcon("ChevronRight")}
              </div>
            </Link>
           
          </div>
        </div>
      )} */}

      {/* Masters Section */}
      {hasAccess("MASTERS") && ( // Check access for Masters
        <div className="submenu-items">
          <div className="submenu-item">
            <Link to="/MasterIndex" onClick={() => toggleSubmenu("masters")} className="menu-link">
              <div className="menu-text">
                {renderIcon("Database")} <span>{isRestricted ? "Master" : "मास्टर्स"} </span>
              </div>
              <div className="arrow">
                {activeSubmenu === "masters" ? renderIcon("ChevronDown") : renderIcon("ChevronRight")}
              </div>
            </Link>
            {activeSubmenu === "masters" && (
              <div className="submenu">
                {hasAccess("SERVICES") && <Link to="/ServicesMaster">{renderIcon("Tool")} सेवा </Link>}
                {hasAccess("HSN") && <Link to="/HSNMaster">{renderIcon("FileText")}एच एस एन  </Link>}
                {hasAccess("STATECODE") && <Link to="/SateCodeMaster">{renderIcon("MapPin")}राज्य कोड</Link>}
                {hasAccess("PRODUCT") && <Link to="/ItemMaster">{renderIcon("Layers")} उत्पादन</Link>}
                {hasAccess("CATEGORY") && <Link to="/CategoryMaster">{renderIcon("Layers")} युनिट रूपांतरण</Link>}
                {hasAccess("CATEGORY") && <Link to="/CategoryMaster">{renderIcon("Layers")} वर्ग</Link>}
                {hasAccess("CATEGORY") && <Link to="/SubCategoryMaster">{renderIcon("Layers")}उपवर्ग</Link>}
                {hasAccess("CATEGORY") && <Link to="/WarrantyMaster">{renderIcon("Layers")} हमी</Link>}
                {hasAccess("CATEGORY") && <Link to="/CategoryMaster">{renderIcon("Layers")} सेवा शुल्क</Link>}
                {hasAccess("CATEGORY") && <Link to="/CategoryMaster">{renderIcon("Layers")} दुकाने</Link>}
                {hasAccess("FARMER") && <Link to="/Farmers">{renderIcon("Layers")} Farmer</Link>}
                {/* {hasAccess("EMP") && <Link to="/EmployeeMaster">{renderIcon("Layers")} Workers</Link>} */}
                {hasAccess("RMATERIAL") && <Link to="/CropPacking">{renderIcon("Layers")} Packing Material</Link>}
                {hasAccess("VISITOR") && <Link to="/VisitorMaster">{renderIcon("Layers")} Visitor</Link>}
                {hasAccess("PAPPROV") && <Link to="/VistorApprovalMaster">{renderIcon("Layers")} Approval</Link>}
                {hasAccess("SHEDULER") && <Link to="/CropSchedulerMaster">{renderIcon("Layers")} Sheduler</Link>}
                {hasAccess("CINVERD") && <Link to="/CropCollectMaster">{renderIcon("Layers")} Direct Purchase</Link>}
                {hasAccess("CBILL") && <Link to="/CropBill">{renderIcon("Layers")} Farmer Bill</Link>}
                {hasAccess("CBILL") && <Link to="/CropLoading">{renderIcon("Layers")} Crop Loading</Link>}
                {hasAccess("CBILL") && <Link to="/CropTransportInvoice">{renderIcon("Layers")} Transport Invoice</Link>}
                {hasAccess("MEMBER") && <Link to="/Member">{renderIcon("Layers")} Member</Link>}

              </div>
            )}
          </div>
        </div>
      )}


      {hasAccess("VISITOR") && (
        <div className="submenu-items">
          <div className="submenu-item">
            <Link to="/VisitorIndex" onClick={() => toggleSubmenu("VisitorManagement")} className="menu-link">
              <div className="menu-text">
                {renderIcon("UserCheck")} <span>{isRestricted ? "Agronomist" : "agronomist"}  </span>
              </div>
              <div className="arrow">
                {activeSubmenu === "VisitorManagement" ? renderIcon("ChevronDown") : renderIcon("ChevronRight")}
              </div>
            </Link>
            {activeSubmenu === "VisitorManagement" && (
              <div className="submenu">
                {hasAccess("PAPPROV") && <Link to="/Farmers">{renderIcon("Users")}{isRestricted ? "Farmer" : "शेतकरी"}  </Link>}
                {hasAccess("SHEDULER") && <Link to="/VisitorMaster">{renderIcon("Users")}{isRestricted ? "Agronomist" : "कृषीशास्त्रज्ञ"}  </Link>}
              </div>
            )}
          </div>
        </div>
      )}

      {hasAccess("SHEDULER") && (
        <div className="submenu-items">
          <div className="submenu-item">
            <Link to="/ShedulerIndex" onClick={() => toggleSubmenu("ShedulerManagement")} className="menu-link">
              <div className="menu-text">
                {renderIcon("UserCheck")} <span>{isRestricted ? "Scheduler " : "वेळापत्रक"}  </span>
              </div>
              <div className="arrow">
                {activeSubmenu === "ShedulerManagement" ? renderIcon("ChevronDown") : renderIcon("ChevronRight")}
              </div>
            </Link>
            {activeSubmenu === "ShedulerManagement" && (
              <div className="submenu">
                {/* {hasAccess("PAPPROV") && <Link to="/VistorApprovalMaster">{renderIcon("Users")}{isRestricted ? "Approval" : "Approval"}  </Link>} */}
                {hasAccess("SHEDULER") && <Link to="/CropSchedulerMaster">{renderIcon("Users")}{isRestricted ? "Scheduler " : "वेळापत्रक"}  </Link>}
                {hasAccess("CINVERD") && <Link to="/CropCollectMaster">{renderIcon("Lock")}{isRestricted ? "Purchase" : "पीक जमा"}  </Link>}
                {hasAccess("CINVERD") && <Link to="/DirectInward">{renderIcon("Lock")}{isRestricted ? "Direct Purchase" : "पीक जमा"}  </Link>}
              </div>
            )}
          </div>
        </div>
      )}

      {hasAccess("CBILL") && (
        <div className="submenu-items">
          <div className="submenu-item">
            <Link to="/CropBillIndex" onClick={() => toggleSubmenu("billManagement")} className="menu-link">
              <div className="menu-text">
                {renderIcon("UserCheck")} <span>{isRestricted ? "Billing" : "बिल"}  </span>
              </div>
              <div className="arrow">
                {activeSubmenu === "billManagement" ? renderIcon("ChevronDown") : renderIcon("ChevronRight")}
              </div>
            </Link>
            {activeSubmenu === "billManagement" && (
              <div className="submenu">
                {hasAccess("CBILL") && <Link to="/CropBill">{renderIcon("Users")}{isRestricted ? "Farmer Bill" : "शेतकरी बिल"}  </Link>}
                {hasAccess("CBILL") && <Link to="/CropLoading">{renderIcon("Users")}{isRestricted ? "Crop Loading" : "क्रॉप लोडिंग"}  </Link>}
                {hasAccess("CBILL") && <Link to="/CropTransportInvoice">{renderIcon("Users")}{isRestricted ? "Transport Invoice" : "वाहतूक चलन"}  </Link>}
              </div>
            )}
          </div>
        </div>
      )}

      {/* Masters Section */}
      {hasAccess("VOUCHER") && ( // Check access for Masters
        <div className="submenu-items">
          <div className="submenu-item">
            <Link to="/VoucherIndex" onClick={() => toggleSubmenu("VOUCHER")} className="menu-link">
              <div className="menu-text">
                {renderIcon("Database")} <span>व्हाउचर</span>
              </div>
              <div className="arrow">
                {activeSubmenu === "VOUCHER" ? renderIcon("ChevronDown") : renderIcon("ChevronRight")}
              </div>
            </Link>
            {activeSubmenu === "VOUCHER" && (
              <div className="submenu">
                {hasAccess("VOUCHERF") && <Link to="/Voucher">{renderIcon("Tool")} व्हाउचर </Link>}

              </div>
            )}
          </div>
        </div>
      )}

      {hasAccess("PRECOOLING") && ( // Check access for Masters
        <div className="submenu-items">
          <div className="submenu-item">
            <Link to="/CoolingIndex" onClick={() => toggleSubmenu("PRECOOLING")} className="menu-link">
              <div className="menu-text">
                {renderIcon("Database")} <span> कूलिंग</span>
              </div>
              <div className="arrow">
                {activeSubmenu === "PRECOOLING" ? renderIcon("ChevronDown") : renderIcon("ChevronRight")}
              </div>
            </Link>
            {activeSubmenu === "PRECOOLING" && (
              <div className="submenu">
                {hasAccess("PRECOOLING") && <Link to="/ColdStorage">{renderIcon("Tool")}     कूलिंग इनवर्ड/आउटवर्ड </Link>}

              </div>
            )}
          </div>
        </div>
      )}


      {/* Company Section */}
      {hasAccess("COMPANY") && ( // Check access for Company
        <div className="submenu-items">
          <div className="submenu-item">
            <Link to="/companyindex" onClick={() => toggleSubmenu("company")} className="menu-link">
              <div className="menu-text">
                {renderIcon("Briefcase")} <span> कंपनी</span>
              </div>
              <div className="arrow">
                {activeSubmenu === "company" ? renderIcon("ChevronDown") : renderIcon("ChevronRight")}
              </div>
            </Link>
            {activeSubmenu === "company" && (
              <div className="submenu">
                {hasAccess("COMPANY") && <Link to="/CompanyMaster">{renderIcon("Building")} कंपनी</Link>}
                {hasAccess("STORE") && <Link to="/StoreMaster">{renderIcon("ShoppingCart")} स्टोअर</Link>}
                {hasAccess("WAREHOUSE") && <Link to="/WareHousesMaster">{renderIcon("Home")}गोदाम</Link>}
                {hasAccess("RACK") && <Link to="/RackMaster">{renderIcon("Home")} रॅक</Link>}
                {hasAccess("COUNTER") && <Link to="/Counter">{renderIcon("Home")} काउंटर</Link>}
              </div>
            )}
          </div>
        </div>
      )}

      {/* Gate Entry */}
      {hasAccess("GATEENTRY") && (
        <div className="submenu-items">
          <div className="submenu-item">
            <Link to="/GateEntryIndex" onClick={() => toggleSubmenu("Gate")} className="menu-link">
              <div className="menu-text">
                {renderIcon("LogIn")} <span>प्रवेशद्वार</span>
              </div>
              <div className="arrow">
                {activeSubmenu === "Gate" ? renderIcon("ChevronDown") : renderIcon("ChevronRight")}
              </div>
            </Link>
            {activeSubmenu === "Gate" && (
              <div className="submenu">
                {hasAccess("GATEENTRY_VIEW") && <Link to="/GateEntry">{renderIcon("Key")} प्रवेशद्वार</Link>}
                {hasAccess("GATEENTRY_VIEW") && <Link to="/QuickFarmer">{renderIcon("Key")} शेतकरी नोंदणी </Link>}
                {hasAccess("GATEENTRY_VIEW") && <Link to="/QuickTransporter">{renderIcon("Key")} वाहन चालक नोंदणी </Link>}
              </div>
            )}
          </div>
        </div>
      )}
      {/* Auction*/}
      {hasAccess("AUCTION") && (
        <div className="submenu-items">
          <div className="submenu-item">
            <Link to="/AuctionIndex" onClick={() => toggleSubmenu("Auction")} className="menu-link">
              <div className="menu-text">
                {renderIcon("LogIn")} <span>लिलाव</span>
              </div>
              <div className="arrow">
                {activeSubmenu === "Auction" ? renderIcon("ChevronDown") : renderIcon("ChevronRight")}
              </div>
            </Link>
            {activeSubmenu === "Auction" && (
              <div className="submenu">
                {hasAccess("AUCTION_VIEW") && <Link to="/Auction">{renderIcon("Key")} लिलाव</Link>}
              </div>
            )}
          </div>
        </div>
      )}
      {/* Farmer Bill*/}
      {hasAccess("BILL") && (
        <div className="submenu-items">
          <div className="submenu-item">
            <Link to="/MarketBillIndex" onClick={() => toggleSubmenu("FarmerBill")} className="menu-link">
              <div className="menu-text">
                {renderIcon("LogIn")} <span>शेतकरी बिल</span>
              </div>
              <div className="arrow">
                {activeSubmenu === "FarmerBill" ? renderIcon("ChevronDown") : renderIcon("ChevronRight")}
              </div>
            </Link>
            {activeSubmenu === "FarmerBill" && (
              <div className="submenu">
                {hasAccess("FBILL") && <Link to="/FarmerBillMaster">{renderIcon("Key")} बिल</Link>}
              </div>
            )}
          </div>
        </div>
      )}

      {/* Ledger */}
      {hasAccess("LEDGER") && (
        <div className="submenu-items">
          <div className="submenu-item">
            <Link to="/LegerIndex" onClick={() => toggleSubmenu("ledger")} className="menu-link">
              <div className="menu-text">
                {renderIcon("BookOpen")} <span>लेजर</span>
              </div>
              <div className="arrow">
                {activeSubmenu === "ledger" ? renderIcon("ChevronDown") : renderIcon("ChevronRight")}
              </div>
            </Link>
            {activeSubmenu === "ledger" && (
              <div className="submenu">
                {hasAccess("LEDGER_VIEW") && <Link to="/Ledger">{renderIcon("FileText")} लेजर</Link>}
              </div>
            )}
          </div>
        </div>
      )}

      {/* Farmer */}
      {/* {hasAccess("FARMER") && (
        <div className="submenu-items">
          <div className="submenu-item">
            <Link to="/FarmerIndex" onClick={() => toggleSubmenu("Farmer")} className="menu-link">
              <div className="menu-text">
                {renderIcon("CloudRain")} <span>शेतकरी</span>
              </div>
              <div className="arrow">
                {activeSubmenu === "Farmer" ? renderIcon("ChevronDown") : renderIcon("ChevronRight")}
              </div>
            </Link>
            {activeSubmenu === "Farmer" && (
              <div className="submenu">
                {hasAccess("CROPPLOT") && <Link to="/CropPlot">{renderIcon("Feather")} क्रॉप प्लॉट</Link>}
                {hasAccess("FARMERSVIEW") && <Link to="/Farmers">{renderIcon("UserCheck")} शेतकरी नोंदणी</Link>}
              </div>
            )}
          </div>
        </div>
      )} */}

      {/* Transporter */}
      {hasAccess("TRANSPORTER") && (
        <div className="submenu-items">
          <div className="submenu-item">
            <Link to="/TranporterIndex" onClick={() => toggleSubmenu("Transporter")} className="menu-link">
              <div className="menu-text">
                {renderIcon("Truck")} <span>वाहतूक</span>
              </div>
              <div className="arrow">
                {activeSubmenu === "Transporter" ? renderIcon("ChevronDown") : renderIcon("ChevronRight")}
              </div>
            </Link>
            {activeSubmenu === "Transporter" && (
              <div className="submenu">
                {hasAccess("TRANSPORTER_VIEW") && <Link to="/Transporter">{renderIcon("Package")} वाहतूकदार</Link>}
              </div>
            )}
          </div>
        </div>
      )}

      {/* Vyapari */}
      {/* {hasAccess("VYAPARI") && (
        <div className="submenu-items">
          <div className="submenu-item">
            <Link to="/VyapariIndex" onClick={() => toggleSubmenu("Vyapari")} className="menu-link">
              <div className="menu-text">
                {renderIcon("ShoppingBag")} <span>व्यापारी </span>
              </div>
              <div className="arrow">
                {activeSubmenu === "Vyapari" ? renderIcon("ChevronDown") : renderIcon("ChevronRight")}
              </div>
            </Link>
            {activeSubmenu === "Vyapari" && (
              <div className="submenu">
                {hasAccess("VYAPARI_VIEW") && <Link to="/Vyapari">{renderIcon("User")} व्यापारी नोंदणी </Link>}
              </div>
            )}
          </div>
        </div>
      )} */}

      {/* Purchases Section */}
      {hasAccess("APPADMIN") && (
        <div className="submenu-items">
          <div className="submenu-item">
            <Link to="/AppAdminIndex" onClick={() => toggleSubmenu("AppAdmin")} className="menu-link">
              <div className="menu-text">
                {renderIcon("ShoppingCart")} <span>ॲप ॲडमिन</span>
              </div>
              <div className="arrow">
                {activeSubmenu === "AppAdmin" ? renderIcon("ChevronDown") : renderIcon("ChevronRight")}
              </div>
            </Link>
            {activeSubmenu === "AppAdmin" && (
              <div className="submenu">
                {hasAccess("NOTIFICATION") && <Link to="/AppNotification">{renderIcon("ClipboardList")} सूचना</Link>}

              </div>
            )}
          </div>
        </div>
      )}

      {/* Purchases Section */}
      {hasAccess("PURCHASES") && (
        <div className="submenu-items">
          <div className="submenu-item">
            <Link to="/purchaseindex" onClick={() => toggleSubmenu("purchases")} className="menu-link">
              <div className="menu-text">
                {renderIcon("ShoppingCart")} <span>खरेदी</span>
              </div>
              <div className="arrow">
                {activeSubmenu === "purchases" ? renderIcon("ChevronDown") : renderIcon("ChevronRight")}
              </div>
            </Link>
            {activeSubmenu === "purchases" && (
              <div className="submenu">
                {hasAccess("PREQUISITION") && <Link to="/RequisitionMaster">{renderIcon("ClipboardList")}खरेदी मागणी</Link>}
                {hasAccess("PQUOTATION") && <Link to="/QuatationMaster">{renderIcon("FileText")}खरेदी कोटेशन</Link>}
                {hasAccess("PORDER") && <Link to="/PurchaseOrderMaster">{renderIcon("FilePlus")}खरेदी ऑर्डर</Link>}
                {hasAccess("PCHALLAN") && <Link to="/ChallanIndex">{renderIcon("Truck")}खरेदी चलन</Link>}
                {hasAccess("PBILL") && <Link to="/PurchaseBill">{renderIcon("CreditCard")}खरेदी बिल</Link>}
                {hasAccess("PRETURN") && <Link to="/ReturnIndex">{renderIcon("CreditCard")}खरेदी परतावे</Link>}
              </div>
            )}
          </div>
        </div>
      )}

      {/* Sales Section */}
      {hasAccess("SALES") && (
        <div className="submenu-items">
          <div className="submenu-item">
            <Link to="/salesindex" onClick={() => toggleSubmenu("sales")} className="menu-link">
              <div className="menu-text">
                {renderIcon("TrendingUp")} <span>विक्री</span>
              </div>
              <div className="arrow">
                {activeSubmenu === "sales" ? renderIcon("ChevronDown") : renderIcon("ChevronRight")}
              </div>
            </Link>
            {activeSubmenu === "sales" && (
              <div className="submenu">
                {hasAccess("SENQUIRY") && <Link to="/SalesEnquiry">{renderIcon("Search")} विक्री चौकशी</Link>}
                {hasAccess("SQUOTATION") && <Link to="/invoice-report">{renderIcon("FileText")} विक्री कोटेशन </Link>}
                {hasAccess("SCHALLAN") && <Link to="/BillIndex">{renderIcon("FileInvoice")} विक्री चलन</Link>}
                {hasAccess("SBILL") && <Link to="/BillIndex">{renderIcon("FileInvoice")}विक्री बिल</Link>}
                {hasAccess("SRETURN") && <Link to="/BillIndex">{renderIcon("FileInvoice")}विक्री परतावे</Link>}

              </div>
            )}
          </div>
        </div>
      )}

      {/* People Section */}
      {hasAccess("PEOPLE") && (
        <div className="submenu-items">
          <div className="submenu-item">
            <Link to="/PeopleIndex" onClick={() => toggleSubmenu("people")} className="menu-link">
              <div className="menu-text">
                {renderIcon("Users")} <span>सभासद</span>
              </div>
              <div className="arrow">
                {activeSubmenu === "people" ? renderIcon("ChevronDown") : renderIcon("ChevronRight")}
              </div>
            </Link>
            {activeSubmenu === "people" && (
              <div className="submenu">
                {hasAccess("CUSTOMERS") && <Link to="/CustomerMaster">{renderIcon("UserCheck")} सभासद</Link>}
                {hasAccess("VENDORS") && <Link to="/VendorMaster">{renderIcon("Briefcase")} विक्रेते</Link>}
                {hasAccess("TRANSPORTERS") && <Link to="/TransporterMaster">{renderIcon("Truck")} वाहतूकदार</Link>}
              </div>
            )}
          </div>
        </div>
      )}

      {/* Finance & Accounts Section */}
      {hasAccess("ACCOUNTS") && (
        <div className="submenu-items">
          <div className="submenu-item">
            <Link to="/financeindex" onClick={() => toggleSubmenu("finance")} className="menu-link">
              <div className="menu-text">
                {renderIcon("DollarSign")} <span>अकाऊंट</span>
              </div>
              <div className="arrow">
                {activeSubmenu === "finance" ? renderIcon("ChevronDown") : renderIcon("ChevronRight")}
              </div>
            </Link>
            {activeSubmenu === "finance" && (
              <div className="submenu">
                {hasAccess("EXPENSES") && <Link to="/expense-list">{renderIcon("CreditCard")} खर्च</Link>}
                {hasAccess("EXPENSECATEGORIES") && <Link to="/expense-category">{renderIcon("Tag")} खर्चाचे विभाग</Link>}
              </div>
            )}
          </div>
        </div>
      )}

      {/* HRM Section */}
      {hasAccess("HRM") && (
        <div className="submenu-items">
          <div className="submenu-item">
            <Link to="/HrmIndex" onClick={() => toggleSubmenu("hrm")} className="menu-link">
              <div className="menu-text">
                {renderIcon("Briefcase")} <span>{isRestricted ? "HRM" : "एच आर एम"} </span>
              </div>
              <div className="arrow">
                {activeSubmenu === "hrm" ? renderIcon("ChevronDown") : renderIcon("ChevronRight")}
              </div>
            </Link>
            {activeSubmenu === "hrm" && (
              <div className="submenu">
                {hasAccess("EMPLOYEE") && <Link to="/EmployeeMaster">{renderIcon("Users")} {isRestricted ? "Workers" : "कर्मचारी"}</Link>}
                {hasAccess("DESIGNATION") && <Link to="/designation">{renderIcon("UserCheck")} {isRestricted ? "Designation" : "पदनाम"} </Link>}
                {hasAccess("SHIFTS") && <Link to="/shift">{renderIcon("DollarSign")} {isRestricted ? "Shift" : "शिफ्ट"}</Link>}
                {hasAccess("LEAVES") && <Link to="/attendance-employee">{renderIcon("Clock")} {isRestricted ? "Attendance" : "हजेरी"}</Link>}
                {hasAccess("PAYROLL") && <Link to="/PayrollMaster">{renderIcon("DollarSign")} {isRestricted ? "Payroll" : "पगार"}</Link>}
                {hasAccess("LEAVES") && <Link to="/leave-types">{renderIcon("DollarSign")} {isRestricted ? "Leaves" : "सुट्टी"}</Link>}
                {hasAccess("HOLIDAY") && <Link to="/holidays">{renderIcon("DollarSign")} {isRestricted ? "Holidays" : "सुट्ट्या"}</Link>}
              </div>
            )}
          </div>
        </div>
      )}

      {/* Reports Section */}
      {hasAccess("REPORTS") && (
        <div className="submenu-items">
          <div className="submenu-item">
            <Link to="/ReportIndex" onClick={() => toggleSubmenu("reports")} className="menu-link">
              <div className="menu-text">
                {renderIcon("FileText")} <span>माहिती पत्रके</span>
              </div>
              <div className="arrow">
                {activeSubmenu === "reports" ? renderIcon("ChevronDown") : renderIcon("ChevronRight")}
              </div>
            </Link>
            {activeSubmenu === "reports" && (
              <div className="submenu">
                {hasAccess("REPORTS") && <Link to="/Reports">{renderIcon("BarChart2")} माहिती पत्रके</Link>}
                {/* {hasAccess("PREPORT") && <Link to="/purchase-report">{renderIcon("PieChart")} खरेदी अहवाल</Link>} */}
              </div>
            )}
          </div>
        </div>
      )}
      {hasAccess("VAPDASH") && (
        <div className="submenu-items">
          <div className="submenu-item">
            <Link to="/VyapariDashboardIndex" onClick={() => toggleSubmenu("VyapariDashboard")} className="menu-link">
              <div className="menu-text">
                {renderIcon("FileText")} <span>व्यापारी डॅशबोर्ड </span>
              </div>
              <div className="arrow">
                {activeSubmenu === "VyapariDashboard" ? renderIcon("ChevronDown") : renderIcon("ChevronRight")}
              </div>
            </Link>
            {activeSubmenu === "VyapariDashboard" && (
              <div className="submenu">
                {/* {hasAccess("REPORTS") && <Link to="/Reports">{renderIcon("BarChart2")} माहिती पत्रके</Link>} */}
                {/* {hasAccess("PREPORT") && <Link to="/purchase-report">{renderIcon("PieChart")} खरेदी अहवाल</Link>} */}
              </div>
            )}
          </div>
        </div>
      )}

      {hasAccess("SFCREPORT") && (
        <div className="submenu-items">
          <div className="submenu-item">
            <Link to="/SfcReports" onClick={() => toggleSubmenu("SfcReports")} className="menu-link">
              <div className="menu-text">
                {renderIcon("FileText")} <span>Reports</span>
              </div>
              <div className="arrow">
                {activeSubmenu === "SfcReports" ? renderIcon("ChevronDown") : renderIcon("ChevronRight")}
              </div>
            </Link>
            {activeSubmenu === "SfcReports" && (
              <div className="submenu">
                {hasAccess("SFCREPORT") && <Link to="/SfcReports">{renderIcon("BarChart2")} Reports</Link>}
                {/* {hasAccess("PREPORT") && <Link to="/purchase-report">{renderIcon("PieChart")} खरेदी अहवाल</Link>} */}
              </div>
            )}
          </div>
        </div>
      )}


      {/* User Management Section */}
      {hasAccess("MANAGEUSER") && (
        <div className="submenu-items">
          <div className="submenu-item">
            <Link to="/UserIndex" onClick={() => toggleSubmenu("userManagement")} className="menu-link">
              <div className="menu-text">
                {renderIcon("UserCheck")} <span>{isRestricted ? "Users" : "वापरकर्ता"}  </span>
              </div>
              <div className="arrow">
                {activeSubmenu === "userManagement" ? renderIcon("ChevronDown") : renderIcon("ChevronRight")}
              </div>
            </Link>
            {activeSubmenu === "userManagement" && (
              <div className="submenu">
                {hasAccess("USERMANAGEMENT") && <Link to="/UserMaster">{renderIcon("Users")}{isRestricted ? "Users" : "वापरकर्ता"}  </Link>}
                {hasAccess("ACCESSRIGHTS") && <Link to="/AccessRight">{renderIcon("Lock")}{isRestricted ? "AccessRights" : "भूमिका आणि परवानग्या"}  </Link>}
              </div>
            )}
          </div>
        </div>
      )}
      <CollapsedSidebar />
    </div>
  );
};

export default Sidebar;
