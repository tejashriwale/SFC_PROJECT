
import React, { useRef, useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { ArrowLeft } from "feather-icons-react/build/IconComponents";
import { OverlayTrigger, Tooltip } from "react-bootstrap";
import axios from 'axios';
import Table from "../../core/pagination/datatable";
import withReactContent from "sweetalert2-react-content";
import Swal from "sweetalert2";
import { getUserData } from "../../Context/UserData";
import { ACSPLGUID, baseUrl } from "../../core/json/custom";
import {
    ChevronUp,
    Edit,
    PlusCircle,
    RotateCcw,
    Trash2,
} from "feather-icons-react/build/IconComponents";
function PendingAuction({ onClose }) {
    const { userdetail } = getUserData();
    const [auctionshed, setauctionshed] = useState([]);
    const fetchAuctionShed = async () => {
        const payload = {
            "apkid": "%",
            "keyword": "%",
            companyid: userdetail?.companyID ? userdetail.companyID : "",
            deptid: userdetail?.departmentID ? userdetail.departmentID : "",
            "date": userdetail.APPDT,
            "userid": userdetail.uaid
        };
        const headers = {
            "Content-Type": "application/json",
            Accept: "*/*",
        };
        try {
            const response = await axios.post(
                `${baseUrl.Url}/backend/api/GET_AUCTIONSHED`,
                JSON.stringify(payload),
                { headers }
            );
            if (response.status !== 200) {
                throw new Error("Failed to fetch data");
            }
            console.log(response.data, "response.data auction")
            setauctionshed(response.data.filter(user => user.iscompleted === false));
        } catch (error) {
            console.error("Error fetching auction shed data:", error);
        }
    };
    useEffect(() => {
        fetchAuctionShed();
        const intervalId = setInterval(() => {
            fetchAuctionShed();
        }, 5000);
        return () => clearInterval(intervalId);
    }, []);
    const renderActionTooltip = (props) => (
        <Tooltip id="action-tooltip" {...props}>
            Action
        </Tooltip>
    );
    const MySwal = withReactContent(Swal);
    const showConfirmationAlert = (apkid, token) => {
        if (userdetail.ROLEID !== 'admin') {
            Swal.fire({
                icon: 'warning',
                title: 'कृपया लक्ष द्या!',
                text: 'ही प्रवेश अनुमती फक्त अडमिनकडे आहे.',
                confirmButtonText: 'ठीक आहे',
                allowOutsideClick: false,
                allowEscapeKey: false,
            });
            return;
        }
        MySwal.fire({
            title: "तुम्हाला खात्री आहे का?",
            text: "हे आपण पूर्ववत करू शकणार नाही!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#00ff00",
            confirmButtonText: "होय, हटवा!",
            cancelButtonColor: "#ff0000",
            cancelButtonText: "रद्द करा",
            allowOutsideClick: false,
            allowEscapeKey: false,
        }).then((result) => {
            if (result.isConfirmed) {
                OndeleteQuatation(apkid, token)
            } else {
                MySwal.close();
            }
        });
    };
    const OndeleteQuatation = async (apkid, token) => {
        try {
            const payload = {
                apkid: apkid,
                companyid: userdetail?.companyID ? userdetail.companyID : "",
                deptid: userdetail?.departmentID ? userdetail.departmentID : "",
                token: token,
                date: userdetail.APPDT,
            };
            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };
            const response = await axios.post(
                `${baseUrl.Url}/backend/api/SP_DeleteAuctionMasters`,
                JSON.stringify(payload),
                { headers }
            );
            if (response.status !== 200) {
                throw new Error("Failed to delete auction shed");
            }
            MySwal.fire({
                title: response.data[0].responseCode === "FAILURE" ? "Deletion Not Allowed" : "Deleted!",
                text: response.data[0].responseMessage,
                icon: response.data[0].responseCode === "FAILURE" ? "error" : "success",
                confirmButtonText: "OK",
                customClass: {
                    confirmButton: response.data[0].responseCode === "FAILURE" ? "btn btn-danger" : "btn btn-success",
                },
            });
            const refreshPayload = {
                apkid: "%",
                keyword: "%",
                companyid: userdetail?.companyID || "",
                deptid: userdetail?.departmentID || "",
                date: userdetail.APPDT,
                "userid": userdetail?.uaid || "",
            };
            const refreshResponse = await axios.post(
                `${baseUrl.Url}/backend/api/GET_AUCTIONSHED`,
                JSON.stringify(refreshPayload),
                { headers }
            );
            if (refreshResponse.status !== 200) {
                throw new Error("Failed to fetch auction shed data");
            }
            console.log("Quotation master", refreshResponse.data);
            setauctionshed(response.data.filter(user => user.iscompleted === false));
        } catch (error) {
            console.error("Error processing deletion:", error);
            MySwal.fire({
                title: "Error",
                text: error.message || "An unexpected error occurred.",
                icon: "error",
                confirmButtonText: "OK",
            });
        }
    };
    const columns = [
        {
            title: (
                <div className="d-flex justify-content-center">
                    <OverlayTrigger
                        placement="top"
                        overlay={<Tooltip id="Member_Name-tooltip" style={{ textAlign: "center" }}>टोकन क्रमांक</Tooltip>}
                    >
                        <span>टोकन क्रमांक</span>
                    </OverlayTrigger>
                </div>
            ),
            dataIndex: "token",
            render: (text) => <div style={{ textAlign: "center" }}>{text}</div>,
        },
        {
            title: (
                <div className="d-flex justify-content-center">
                    <OverlayTrigger placement="top" overlay={<Tooltip id="Member_Cantact-tooltip" style={{ textAlign: "center" }}>बिल क्रमांक</Tooltip>}>
                        <span>बिल क्रमांक</span>
                    </OverlayTrigger>
                </div>
            ),
            dataIndex: "billno",
            render: (text) => <div style={{ textAlign: "center" }}>{text}</div>,
        },
        {
            title: (
                <div className="d-flex justify-content-center">
                    <OverlayTrigger placement="top" overlay={<Tooltip id="Member_Cantact-tooltip">दिनांक</Tooltip>}>
                        <span>दिनांक</span>
                    </OverlayTrigger>
                </div>
            ),
            dataIndex: "date",
            render: (text) => <div style={{ textAlign: "center" }}>{text}</div>,
        },
        {
            title: (
                <div className="d-flex justify-content-center">
                    <OverlayTrigger placement="top" overlay={<Tooltip id="Member_Email-tooltip">शेतकऱ्याचे नाव</Tooltip>}>
                        <span>शेतकऱ्याचे नाव</span>
                    </OverlayTrigger>
                </div>
            ),
            dataIndex: "fname",
            render: (text) => <div style={{ textAlign: "center" }}>{text}</div>,
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
            render: (_, record) => (
                <div className="action-table-data">
                    <div className="edit-delete-action">
                        <Link
                            className="confirm-text p-2 me-2"
                            to="#"
                            onClick={() => showConfirmationAlert(record.apkid, record.token)}
                        >
                            <Trash2 className="feather-trash-2" />
                        </Link>
                    </div>
                </div>
            ),
        },
    ];
    return (
        <div className="modal fade" id="add-units-PendingAuction">
            <div className="modal-dialog modal-dialog-centered modal-fullscreen">
                <div className="modal-content">
                    {/* Modal Header */}
                    <div className="d-flex justify-content-between align-items-center p-3 border-bottom">
                        <h5 className="mb-0">Todays Pending Auction </h5>
                        <button
                            type="button"
                            className="btn btn-secondary d-flex align-items-center"
                            data-bs-dismiss="modal"
                            aria-label="Close"
                            onClick={onClose}
                        >
                            <ArrowLeft className="me-2" />
                            मागे
                        </button>
                    </div>
                    <div className="card table-list-card">
                        <div className="card-body p-2">
                            <div className="table-responsive responsive-no-scroll">
                                <Table
                                    columns={columns}
                                    dataSource={auctionshed}
                                    pagination={false}
                                    scroll={false}
                                />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}
export default PendingAuction;

