import React, { useEffect, useState } from 'react';
import ImageWithBasePath from '../../core/img/imagewithbasebath';
import { useNavigate } from 'react-router-dom';
import { Link } from "react-router-dom";
import { all_routes } from "../../Router/all_routes";
import {
    ArrowLeft
} from "feather-icons-react/build/IconComponents";
function CompanyIndex() {
    const route = all_routes;
    const navigate = useNavigate();
    const [isDesktop, setIsDesktop] = useState(false);
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
        { title: 'कंपनी', image: 'assets/img/avatar/company.png', path: '/CompanyMaster' },
        { title: 'स्टोअर', image: 'assets/img/avatar/Store.png', path: '/StoreMaster' },
        { title: 'गोदाम', image: 'assets/img/avatar/warehouse.png', path: '/WareHousesMaster' },
        { title: 'रॅक', image: 'assets/img/avatar/Rack.png', path: '/RackMaster' },
        { title: 'काउंटर', image: 'assets/img/avatar/counter.png', path: '/Counter' },
    ];
    return (
        <div className="page-wrapper pagehead">
            <div className="content">
                <div className="page-header">
                    <div className="page-title">
                        <h3>कंपनी सूची</h3>
                    </div>
                    <ul className="table-top-head">
                    </ul>
                </div>
                <div className="row">
                    {cardsData.map((card, index) => (
                        <div
                            className="col-6 col-sm-6 col-md-4 col-lg-2 col-xxl-2 mb-3"
                            key={index}
                        >
                            <div className="card text-center" style={{ borderRadius: '20px' }}>
                                <div className="card-header border-bottom-0 pb-0"></div>
                                <div className="card-body pt-1" style={{ height: '120px' }} onClick={() => { navigate(card.path) }}>
                                    <span className="avatar avatar-xl avatar-rounded me-2 mb-2">
                                        <ImageWithBasePath src={card.image} alt={card.title} />
                                    </span>
                                    <div className="fw-semibold fs-16">{card.title}</div>
                                    <p className="mb-4 text-muted fs-11"></p>
                                </div>
                            </div>
                        </div>
                    ))}
                </div>
            </div>
        </div>
    );
}
export default CompanyIndex;
