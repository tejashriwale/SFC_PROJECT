import React, { useEffect, useState } from 'react';
import ImageWithBasePath from '../../core/img/imagewithbasebath';
import { useNavigate, Link } from 'react-router-dom';
import { all_routes } from "../../Router/all_routes";
import { ArrowLeft } from "feather-icons-react/build/IconComponents";
import { getUserData } from '../../Context/UserData';

function ShedulerIndex() {
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
        // { title: isRestricted ? " Approval" : ' मंजुरी', image: 'assets/img/avatar/cropapprove.png', path: '/VistorApprovalMaster', access: 'PAPPROV' },
        { title: isRestricted ? " Scheduler" : ' वेळापत्रक', image: 'assets/img/avatar/slab.png', path: '/CropSchedulerMaster', access: 'SHEDULER' },
        { title: isRestricted ? "Purchase" : 'पिक संकलन', image: 'assets/img/avatar/crocollect.png', path: '/CropCollectMaster', access: 'CINVERD' },
        { title: isRestricted ? "Direct Purchase" : 'पिक संकलन', image: 'assets/img/avatar/crocollect.png', path: '/DirectInward', access: 'CINVERD' },
    ];

    const accessPolicy = userdetail?.accessPolicy || [];
    const normalizedAccess = Array.isArray(accessPolicy)
        ? accessPolicy.map(a => a.toUpperCase())
        : [];
    const visibleCards = cardsData.filter(card =>
        normalizedAccess.includes(card.access.toUpperCase())
    );
    return (
        <div className="page-wrapper pagehead">
            <div className="content">
                <div className="page-header">
                    <div className="page-title">
                        <h3>{isRestricted ? "scheduler  Index" : "scheduler  index"}</h3>
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
            </div>
        </div>
    );
}
export default ShedulerIndex;


