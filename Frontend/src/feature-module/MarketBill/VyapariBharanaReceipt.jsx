import React, { useState, useEffect } from 'react'
import { Link } from 'react-router-dom'
import ImageWithBasePath from '../../core/img/imagewithbasebath'
import { RefreshCcw, RotateCw, ShoppingCart, ArrowLeft, FilePlus } from 'feather-icons-react/build/IconComponents'
import { Check, CheckCircle, Edit, MoreVertical, Trash2, UserPlus } from 'react-feather'
import Select from 'react-select'
import PlusCircle from 'feather-icons-react/build/IconComponents/PlusCircle'
import MinusCircle from 'feather-icons-react/build/IconComponents/MinusCircle'
import { OverlayTrigger, Tooltip } from 'react-bootstrap'
import withReactContent from 'sweetalert2-react-content'
import Swal from 'sweetalert2'
import Slider from 'react-slick'

import 'slick-carousel/slick/slick.css';
import 'slick-carousel/slick/slick-theme.css';
import Table from "../../core/pagination/datatable";
import axios from 'axios';
import { baseUrl, ACSPLGUID } from "../../core/json/custom";

const VyapariBharanaReceipt = ({ vdaid }) => {

    const MySwal = withReactContent(Swal);
    const [tabledata, settabledata] = useState([]);


    useEffect(() => {

        const fetchRegulerVyapriBharana = async () => {
            try {
                const payload = {
                    "vdaid": "%",
                };
                const headers = {
                    "Content-Type": "application/json",
                    Accept: "*/*",
                };
                const response = await axios.post(
                    `${baseUrl.Url}/backend/api/GET_RegulerVyapriBharana`,
                    payload,
                    { headers }
                );
                if (response.status !== 200)
                    throw new Error("Failed to fetch ");
                console.log("Get_VyapariDueData", response.data)
                settabledata(response.data);
            } catch (error) {
                console.error("Error fetching GET_RegulerVyapriBharana data:", error);
            }
        };
        fetchRegulerVyapriBharana();

    }, []);

    const showExitAlert = () => {
        MySwal.fire({
            title: "तुम्हाला खात्री आहे का?",
            text: "तुम्हाला मागे जायचं आहे का?",
            showCancelButton: true,
            confirmButtonColor: "#00ff00",
            confirmButtonText: "हो",
            cancelButtonColor: "#092C4C",
            cancelButtonText: "नाही",
        }).then((result) => {
            if (result.isConfirmed) {
                // setMasterData({
                //     ...masterData,
                //     vyapari: "",
                //     txnmode: "",
                //     vyapariname: "",
                //     phonenumber: "",
                //     companyname: "",
                //     GSTnumber: "",
                //     ekunyenerakkam: "",
                //     bharlelirakkam: "",
                //     urvaritrakkam: "",
                //     description: "",
                //     chequedate: "",
                //     chequenumber: "",
                // });


                const modal = document.getElementById("VyapariBharnaReceipt");
                if (modal) {
                    modal.classList.remove("show");
                    modal.style.display = "none";
                    modal.setAttribute("aria-hidden", "true");

                    const modalBackdrop = document.querySelector(".modal-backdrop");
                    if (modalBackdrop) {
                        modalBackdrop.remove();
                    }

                    modal.removeAttribute("aria-modal");
                    modal.removeAttribute("role");

                    document.body.classList.remove("modal-open");
                    document.body.style.overflow = "";
                    document.body.style.paddingRight = "";
                }

                // if (onRefresh) {
                //     onRefresh();
                // }

            }
        });
    };




    return (
        <div>

            <div
                className="modal fade modal-default"
                id="VyapariBharnaReceipt"
            // aria-labelledby="print-receipt"
            >
                <div className="modal-dialog modal-dialog-centered">
                    <div className="modal-content">
                        <div className="d-flex justify-content-end">
                            <Link
                                className="btn btn-secondary"
                                onClick={showExitAlert}
                            >
                                <ArrowLeft className="me-2" />
                                मागे
                            </Link>
                            {/* <button
                                type="button"
                                className="close p-0"
                                data-bs-dismiss="modal"
                                aria-label="Close"
                            >
                                <span aria-hidden="true">×</span>
                            </button> */}
                        </div>
                        <div className="modal-body" id='printableArea'>
                            {/* <div className="icon-head text-center">
                                <Link to="#">
                                    <ImageWithBasePath
                                        src="assets/img/logo.png"
                                        width={100}
                                        height={30}
                                        alt="Receipt Logo"
                                    />
                                </Link>
                            </div> */}


                            <table className="table-borderless w-100 table-fit">
                                <thead>
                                    <tr>
                                        <th className="text-center">Sr</th>
                                        <th className="text-center">Farmer</th>
                                        {/* <th className="text-center">Crop</th> */}
                                        <th className="text-center">Weight</th>
                                        <th className="text-center">Rate</th>

                                        <th className="text-end">Amount</th>
                                    </tr>
                                </thead>
                                <tbody>

                                    {tabledata.length > 0 ? (
                                        tabledata.map((row, index) => (
                                            <tr key={index}>
                                                <td className="text-center">{index + 1}</td>
                                                <td className="text-center">{row.fname}</td>
                                                {/* <td className="text-center">{row.croplabel}</td> */}
                                                <td className="text-center">{row.weight}</td>
                                                <td className="text-center">{row.rate}</td>
                                                <td className="text-end">   {row.amount}</td>
                                            </tr>
                                        ))
                                    ) : (
                                        <tr>
                                            <td colSpan="5" className="text-center">No Data Available</td>
                                        </tr>
                                    )}


                                </tbody>
                            </table>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}
export default VyapariBharanaReceipt