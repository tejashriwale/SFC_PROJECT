import React, { useEffect, useState } from 'react';
import ImageWithBasePath from '../../core/img/imagewithbasebath';
import { useNavigate, Link } from 'react-router-dom';
import { all_routes } from "../../Router/all_routes";
import { ArrowLeft } from "feather-icons-react/build/IconComponents";
import { getUserData } from '../../Context/UserData';

function Test() {
    const route = all_routes;
    const navigate = useNavigate();
    const [isDesktop, setIsDesktop] = useState(false);
    const { userdetail } = getUserData();
    console.log(userdetail, "userdetailuserdetail")
    const isRestricted = userdetail.LANGUAGE === "en";
    useEffect(() => {
        const handleResize = () => {
            setIsDesktop(window.innerWidth >= 992);
        };
        handleResize();
        window.addEventListener('resize', handleResize);
        return () => {
            window.removeEventListener('resize', handleResize);
        };
    }, []);

    const cardsData = [
        { title: isRestricted ? "Customer" : 'विक्रेता नोंदणी', image: 'assets/img/avatar/vendor.png', path: '/VendorMaster', access: 'VENDOR' },
        { title: isRestricted ? "Location" : 'Location', image: 'assets/img/avatar/location.png', path: '/Location', access: 'STATECODE' },
        { title: isRestricted ? "Farmer" : "शेतकरी", image: 'assets/img/avatar/farmer.png', path: '/Farmers', access: 'FARMER' },
        { title: isRestricted ? "Member" : "Member", image: 'assets/img/avatar/member.png', path: '/Member', access: 'MEMBER' },
        { title: isRestricted ? "Packing Material" : 'पॅकिंग साहित्य', image: 'assets/img/avatar/rowmaterial.png', path: '/CropPacking', access: 'RMATERIAL' },
        { title: 'क्रॉप प्लॉट', image: 'assets/img/avatar/plot.png', path: '/CropPlot', access: 'CROPMASTER' },
        { title: 'व्यापारी नोंदणी ', image: 'assets/img/avatar/register.png', path: '/Vyapari', access: 'CROPMASTER' },
        { title: 'व्यापारी लिमिट नोंदणी', image: 'assets/img/avatar/limit.png', path: '/Vyaparilimit', access: 'CROPMASTER' },
        { title: 'सेवा', image: 'assets/img/avatar/Services.png', path: '/ServicesMaster', access: 'SERVICE' },
        { title: 'एच एस एन ', image: 'assets/img/avatar/hsn.png', path: '/HSNMaster', access: 'HSN' },
        { title: 'उत्पादन', image: 'assets/img/avatar/product.png', path: '/ItemMaster', access: 'PRODUCT' },
        { title: 'राज्य कोड', image: 'assets/img/avatar/state.png', path: '/SateCodeMaster', access: 'STATECODE' },
        { title: 'युनिट रूपांतरण', image: 'assets/img/avatar/unit.png', path: '/UnitConversionMaster', access: 'UNIT CONVERSION' },
        { title: 'वर्ग', image: 'assets/img/avatar/categories.png', path: '/CategoryMaster', access: 'CATEGORY' },
        { title: 'उपवर्ग ', image: 'assets/img/avatar/subcategory.png', path: '/SubCategoryMaster', access: 'SUBCATEGORY' },
        { title: 'हमी', image: 'assets/img/avatar/warranty.png', path: '/WarrantyMaster', access: 'WARRANTY' },
        { title: 'सेवा शुल्क', image: 'assets/img/avatar/service.png', path: '/ServiceCharges', access: 'SERVCHARGES' },
        { title: 'दुकाने', image: 'assets/img/avatar/shop.png', path: '/Shop', access: 'SHOP' },
        { title: isRestricted ? "Add Crop" : 'पीक नोंदणी', image: 'assets/img/avatar/plant.png', path: '/CropMaster', access: 'ITEM' },
        { title: 'स्थायी सूचना', image: 'assets/img/avatar/instruction.png', path: '/SatandingInstruction', access: 'CROPMASTER' },
        { title: 'जाळी नोंदणी', image: 'assets/img/avatar/shopping-basket.png', path: '/Carets', access: 'CROPMASTER' },
        { title: 'प्रीकूलिंग ', image: 'assets/img/avatar/temp.png', path: '/PreCooling ', access: 'CROPMASTER' },
        { title: 'शॉप अलॉट ', image: 'assets/img/avatar/shop.png', path: '/GalaAlotMaster ', access: 'CROPMASTER' },
        { title: 'वापारी स्लॅब मास्टर', image: 'assets/img/avatar/slab.png', path: '/YapariSlabMaster ', access: 'CROPMASTER' },
        { title: 'वापासी स्लॅब तपशील', image: 'assets/img/avatar/vapsi.png', path: '/VVapasiSlabDetail ', access: 'CROPMASTER' },
        // { title: isRestricted ? "agronomist" : 'विझिटर', image: 'assets/img/avatar/visitors.png', path: '/VisitorMaster', access: 'VISITOR' },
        { title: isRestricted ? " Approval" : ' मंजुरी', image: 'assets/img/avatar/cropapprove.png', path: '/VistorApprovalMaster', access: 'PAPPROV' },
        // { title: isRestricted ? " Sheduler" : ' वेळापत्रक', image: 'assets/img/avatar/cropshedule.png', path: '/CropSchedulerMaster', access: 'SHEDULER' },
        // { title: isRestricted ? "Inward" : 'पिक संकलन', image: 'assets/img/avatar/crocollect.png', path: '/CropCollectMaster', access: 'CINVERD' },
        // { title: isRestricted ? "Farmer Bill" : 'शेतकरी बिल', image: 'assets/img/avatar/cropbill.png', path: '/CropBill', access: 'CBILL' },
        // { title: isRestricted ? "Crop Loading" : 'Crop Loading', image: 'assets/img/avatar/loading.png', path: '/CropLoading', access: 'CBILL' },
        // { title: isRestricted ? "Transport Invoice" : 'Transport Invoice', image: 'assets/img/avatar/tinvoice.png', path: '/CropTransportInvoice', access: 'CBILL' },
    ];




    const cardsData1 = [

        { title: isRestricted ? "Direct Purchase" : 'Direct Purchase', image: 'assets/img/avatar/crocollect.png', path: '/DirectInward', access: 'CBILL' },
        { title: isRestricted ? "Farmer Payment " : 'Farmer Payment ', image: 'assets/img/avatar/farmerinvoice.png', path: '/AddFarmerBillDetails', access: 'CBILL' },
        { title: isRestricted ? "Farmer Bill and Summary" : 'Farmer Bill and Summary', image: 'assets/img/avatar/cropbill.png', path: '/CropBill', access: 'CBILL' },
        { title: isRestricted ? "Transport Loading" : 'Transport Loading', image: 'assets/img/avatar/loading.png', path: '/CropLoading', access: 'CBILL' },
        { title: isRestricted ? "Customer Sales Entry and Summary" : 'Customer Bill and Summary', image: 'assets/img/avatar/tinvoice.png', path: '/CropTransportInvoice', access: 'CBILL' },
        { title: isRestricted ? "Customer Bill" : 'Customer Bill', image: 'assets/img/avatar/transortbill.png', path: '/TransportBillDetails', access: 'CBILL' },

    ];

    const reportsData = [
        { title: isRestricted ? "Reports" : 'माहिती पत्रके', image: 'assets/img/avatar/reportS.png', path: '/SfcReports', access: 'SFCREPORT' },
    ];

    const accessPolicy = userdetail?.accessPolicy || [];
    const normalizedAccess = Array.isArray(accessPolicy)
        ? accessPolicy.map(a => a.toUpperCase())
        : [];
    const visibleCards = cardsData.filter(card =>
        normalizedAccess.includes(card.access.toUpperCase())
    );

    const visibleCards1 = cardsData1.filter(card =>
        normalizedAccess.includes(card.access.toUpperCase())
    );

    const visibleReports = reportsData.filter(card =>
        normalizedAccess.includes(card.access.toUpperCase())
    );

    return (
        <div className="page-wrapper pagehead">
            <div className="content">
                <div className="page-header">
                    <div className="page-title">
                        <h3>{isRestricted ? "Master Index" : "मास्टर्स सूची"}</h3>
                    </div>
                </div>

                <div className="row">
                    {visibleCards.length === 0 ? (
                        <div className="col-12 text-center mt-4">
                            <p>{isRestricted ? "You do not have access to any module." : "आपल्याकडे कोणत्याही मॉड्यूलसाठी प्रवेश नाही."}</p>
                        </div>
                    ) : (
                        visibleCards.map((card, index) => (
                            <div
                                className="col-6 col-sm-6 col-md-4 col-lg-2 col-xxl-2 mb-3"
                                key={index}
                            >
                                <div
                                    className="card text-center"
                                    style={{
                                        borderRadius: '20px',
                                        cursor: 'pointer'
                                    }}
                                    onClick={() => navigate(card.path)}>
                                    <div className="card-header border-bottom-0 pb-0"></div>
                                    <div className="card-body pt-1" style={{ height: '120px' }}>
                                        <span className="avatar avatar-xl avatar-rounded me-2 mb-2">
                                            <ImageWithBasePath src={card.image} alt={card.title} />
                                        </span>
                                        <div className="fw-semibold fs-16">{card.title}</div>
                                    </div>
                                </div>
                            </div>
                        ))
                    )}
                </div>



                <div className="page-header">
                    <div className="page-title">
                        <h3>{isRestricted ? "Transaction" : "व्यवहार"}</h3>
                    </div>
                </div>


                <div className="row">
                    {visibleCards1.length === 0 ? (
                        <div className="col-12 text-center mt-4">
                            <p>{isRestricted ? "You do not have access to any module." : "आपल्याकडे कोणत्याही मॉड्यूलसाठी प्रवेश नाही."}</p>
                        </div>
                    ) : (
                        visibleCards1.map((card, index) => (
                            <div
                                className="col-6 col-sm-6 col-md-4 col-lg-2 col-xxl-2 mb-3"
                                key={index}
                            >
                                <div
                                    className="card text-center"
                                    style={{
                                        borderRadius: '20px',
                                        cursor: 'pointer'
                                    }}
                                    onClick={() => navigate(card.path)}>
                                    <div className="card-header border-bottom-0 pb-0"></div>
                                    <div className="card-body pt-1" style={{ height: '120px' }}>
                                        <span className="avatar avatar-xl avatar-rounded me-2 mb-2">
                                            <ImageWithBasePath src={card.image} alt={card.title} />
                                        </span>
                                        <div className="fw-semibold fs-16">{card.title}</div>
                                    </div>
                                </div>
                            </div>
                        ))
                    )}
                </div>


                <div className="page-header">
                    <div className="page-title">
                        <h3>{isRestricted ? "Reports" : "माहिती पत्रके"}</h3>
                    </div>
                </div>

                <div className="row">
                    {visibleReports.length === 0 ? (
                        <div className="col-12 text-center mt-4">
                            <p>{isRestricted ? "You do not have access to any report." : "आपल्याकडे कोणत्याही अहवालासाठी प्रवेश नाही."}</p>
                        </div>
                    ) : (
                        visibleReports.map((card, index) => (
                            <div
                                className="col-6 col-sm-6 col-md-4 col-lg-2 col-xxl-2 mb-3"
                                key={index}
                            >
                                <div
                                    className="card text-center"
                                    style={{
                                        borderRadius: '20px',
                                        cursor: 'pointer'
                                    }}
                                    onClick={() => navigate(card.path)}>
                                    <div className="card-header border-bottom-0 pb-0"></div>
                                    <div className="card-body pt-1" style={{ height: '120px' }}>
                                        <span className="avatar avatar-xl avatar-rounded me-2 mb-2">
                                            <ImageWithBasePath src={card.image} alt={card.title} />
                                        </span>
                                        <div className="fw-semibold fs-16">{card.title}</div>
                                    </div>
                                </div>
                            </div>
                        ))
                    )}
                </div>


            </div>
        </div>
    );
}
export default Test;
