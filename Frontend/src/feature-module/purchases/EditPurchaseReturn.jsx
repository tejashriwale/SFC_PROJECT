
import React, { useState, useRef, useEffect } from "react";
import { Link } from "react-router-dom";
import { all_routes } from "../../Router/all_routes";
import withReactContent from "sweetalert2-react-content";
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import { ACSPLGUID, baseUrl, convertToISODate } from "../../core/json/custom";
import Swal from "sweetalert2";
import {
    ArrowLeft,
} from "feather-icons-react/build/IconComponents";
import { getUserData } from "../../Context/UserData";
import { preventDefault } from "@fullcalendar/core/internal";

function EditPurchaseReturn({ PRAID, onRefresh }) {
    const { userdetail } = getUserData();
    const GUID = ACSPLGUID.getNew()
    const [products, setProducts] = useState([]);

    const [formData, setFormData] = useState({
        PMODE: '',
        BillNumber: '',
        TransactionNumber: '',
        Consigner: '',
        Narration: '',
        Vendor: '',
        BillDate: '',
        TransactionDate: '',
        Vendorname: '',
        netAMT: 0,
        PBAID: '',
        REFF: '',
        PTYPE:'',
    });
    const route = all_routes;
    const BillRef = useRef();
    const VendorRef = useRef();
    const ConsignerRef = useRef();
    const navigate = useNavigate();
    useEffect(() => {
        const handleShortcut = (e) => {

            if (e.ctrlKey && e.key === 'e' || e.ctrlKey && e.key === 'E') {
                e.preventDefault();
                navigate(route.test);
            }
            if (e.ctrlKey && e.key === 's' || e.ctrlKey && e.key === 'S') {
                e.preventDefault();
                validateinput(e);
            }
        };
        window.addEventListener('keydown', handleShortcut);
        return () => {
            window.removeEventListener('keydown', handleShortcut);
        };
    }, [formData, navigate]);

    useEffect(() => {
        if (BillRef.current) {
            BillRef.current.focus();
        }
    }, []);

    useEffect(() => {
        if (PRAID) {

            const fetchmasters = async () => {
                try {
                    const payload = {
                        praid: PRAID,
                        companyid: userdetail?.companyID || "",
                        deptid: userdetail?.departmentID || "",
                    };
                    const headers = {
                        "Content-Type": "application/json",
                        Accept: "*/*",
                    };
                    const response = await axios.post(
                        `${baseUrl.Url}/backend/api/GET_PReturnDataByID`,
                        payload,
                        { headers }
                    );
                    if (response.status !== 200)
                        throw new Error("Failed to fetch vendor data");

                    console.log("quatation master", response.data)
                    const Data = response.data[0];
                    setFormData({
                        BillNumber: Data.pbno,
                        TransactionNumber: Data.potrnno,
                        Consigner: Data.prconsigner,
                        Narration: Data.prnarration,
                        Vendor: Data.pseller,
                        BillDate: convertToISODate(Data.pbdate),
                        TransactionDate: Data.prdate,
                        Vendorname:Data.vendorname,
                        Product: '',
                        UOM: '',
                        Quantity: '',
                        Total: '',
                        returnQuantity: '',
                        netAMT: 0,
                        PCAID: '',
                        PMODE: Data.paymentmode,
                        PTYPE: Data.paymenttype,
                        REFF: Data.pbreference,
                    })

                    if (response.data.length > 0) {
                        const mappedProducts = response.data.map((item) => ({
                            ProductName: item.productname,
                            Product: item.prdproduct,
                            UOM: item.prduom,
                            UOMtitle: item.uomtitle,
                            Quantity: item.prdquantity,
                            Total: item.prtdamt,
                            returnQuantity: item.prdquantityreturn,
                            prdigst: item.prdigst,
                            prdsgst: item.prdsgst,
                            prdcgst: item.prdcgst,
                            prdaid: item.prdaid,
                            batchno: item.batchno,
                            expirydate: convertToISODate(item.expirydate)
                        }));

                        setProducts(mappedProducts);
                    }
                } catch (error) {
                    console.error("Error fetching vendor data:", error);
                }
            };

            fetchmasters();
        }
    }, [PRAID]);


    const getTodayDate = () => {
        const today = new Date();
        const year = today.getFullYear();
        const month = String(today.getMonth() + 1).padStart(2, "0");
        const day = String(today.getDate()).padStart(2, "0");
        return `${year}-${month}-${day}`;
    };

    const [selectedDate, setSelectedDate] = useState(getTodayDate());

    const handleDateChange = (event) => {
        setSelectedDate(event.target.value);
        setFormData(prevData => ({
            ...prevData,
            TransactionDate: event.target.value,
        }));
    };
    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData(prevData => ({
            ...prevData,
            [name]: value,
        }));
    };

    const handleReturnQuantityChange = (index, event) => {
        const updatedProducts = [...products];

        const inputValue = parseFloat(event.target.value) || 0;
        const maxQuantity = parseFloat(updatedProducts[index].Quantity) || 0;

        if (inputValue > maxQuantity) {
            Swal.fire({
                icon: "warning",
                title: "Invalid!",
                text: "Return quantity cannot be greater than available quantity.",
                confirmButtonText: "OK",
            })
            updatedProducts[index].returnQuantity = 0;
            setProducts(updatedProducts);
            return; 
        }

        
        updatedProducts[index].returnQuantity = event.target.value;
        setProducts(updatedProducts);

    };


    const handleSubmit = (e) => {
        preventDefault(e);
        showConfirmationAlert(e);
    };

    const MySwal = withReactContent(Swal);

    const showConfirmationAlert = () => {
        MySwal.fire({
            title: "Are you sure?",
            text: "Do you want to save this data?",
            showCancelButton: true,
            confirmButtonColor: "#00ff00",
            confirmButtonText: "SAVE",
            cancelButtonColor: "#092C4C",
            cancelButtonText: "CANCEL",
        }).then((result) => {
            if (result.isConfirmed) {

                handleSave();
            }
        });
    };

    const handleSave = async () => {
        console.log('Products Details', products);
        console.log('FormData', formData);
        // handleSave();
        try {
            const payload = {
                "praid": PRAID ? PRAID : GUID,
                "pbno": formData.BillNumber,
                "pbdate": formData.BillDate,
                "potrnno": formData.TransactionNumber,
                "pseller": formData.Vendor,
                "prconsigner": formData.Consigner,
                "pbrefewence": formData.PBAID ? formData.PBAID : formData.REFF,
                "prnarration": formData.Narration,
                "prdate": formData.TransactionDate,
                "prnamt": formData.netAMT,
                "prgamt": 0,
                "companyid": userdetail?.companyID || "",
                "deptid": userdetail?.departmentID || "",
                "rtype": 1,
                "paymentmode": formData.PMODE,
            };
            console.log("Master payload ", payload);

            const headers = {
                "Content-Type": "application/json",
                Accept: "*/*",
            };


            const response1 = await axios.post(`${baseUrl.Url}/backend/api/AddUpdPReturnMaster`, payload, { headers });


            if (response1.status === 200) {
                const payload2 = products.map((product) => ({
                    "prdaid": product.prdaid ? product.prdaid : ACSPLGUID.getNew(),
                    "praid": PRAID ? PRAID : GUID,
                    "prdproduct": product.Product,
                    "prduom": product.UOM,
                    "prdquantity": product.Quantity,
                    "prdquantityreturn": product.returnQuantity.toString(),
                    "prtdamt": product.Total,
                    "prdigst": product.prdigst,
                    "prdsgst": product.prdsgst,
                    "prdcgst": product.prdcgst,
                    "companyid": userdetail?.companyID || "",
                    "deptid": userdetail?.departmentID || "",
                    "batchno": product.batchno || '',
                    "expirydate": product.expirydate || '',
                }));

                console.log("Detail payload2 ", payload2);


                const response2 = await axios.post(`${baseUrl.Url}/backend/api/AddUpdPReturnDetails`, payload2, { headers });


                if (response2.status === 200) {
                    Swal.fire({
                        icon: "success",
                        title: "Saved!",
                        text: "Data saved successfully.",
                        confirmButtonText: "OK",
                    })
                    // .then((result) => {
                    //     if (result.isConfirmed) {
                    //         const modal = document.getElementById("AddPurchaseReturn");
                    //         if (modal) {
                    //             modal.classList.remove("show");
                    //             modal.style.display = "none";
                    //             document.body.classList.remove("modal-open");
                    //             document.body.style.overflow = "auto";

                    //             const backdrop = document.querySelector(".modal-backdrop");
                    //             if (backdrop) {
                    //                 backdrop.remove();
                    //             }
                    //         }


                    //         setFormData({
                    //             PMODE: '',
                    //             BillNumber: '',
                    //             TransactionNumber: '',
                    //             Consigner: '',
                    //             Narration: '',
                    //             Vendor: '',
                    //             BillDate: '',
                    //             TransactionDate: '',
                    //             Vendorname: '',
                    //             netAMT: 0,
                    //             PBAID: '',
                    //             REFF: '',
                    //         });


                    //         setProducts([]);


                    //         const backdrop = document.querySelector(".modal-backdrop");
                    //         if (backdrop) {
                    //             backdrop.remove();
                    //         }
                    //     }
                    // });
                }
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


    const validateinput = (e) => {
        const { BillNumber, Vendor, Consigner } = formData;
        if (
            (!BillNumber || !/^\d{4,}$/.test(BillNumber)) ||
            (!Vendor || !/^[A-Za-z]+( [A-Za-z]+)*$/.test(Vendor)) ||
            (!Consigner || !/^[A-Za-z]+( [A-Za-z]+)*$/.test(Consigner))
        ) {
            Swal.fire({
                icon: "error",
                title: "Validation Error",
                text: "Please fill in all the required fields correctly.",
            }).then(() => {
                BillRef.current.focus();
                VendorRef.current.focus();
                ConsignerRef.current.focus();
            });
            return;
        }
        handleSubmit(e);
    };



    const showExitAlert = () => {
        MySwal.fire({
            title: "Are you sure?",
            text: "Do you want to Exit?",
            showCancelButton: true,
            confirmButtonColor: "#00ff00",
            confirmButtonText: "YES",
            cancelButtonColor: "#092C4C",
            cancelButtonText: "NO",
        }).then((result) => {
            if (result.isConfirmed) {

                
                
                const modal = document.getElementById("EditPurchaseReturn");
                if (modal) {
                    modal.classList.remove("show");
                    modal.style.display = "none";
                    modal.setAttribute("aria-hidden", "true");
                    document.querySelectorAll(".modal-backdrop").forEach((backdrop) => {
                        backdrop.remove();
                    });
                    document.body.classList.remove("modal-open");
                    document.body.style.overflow = "";
                    document.body.style.paddingRight = "";
                }

                setFormData({
                    PMODE: '',
                    BillNumber: '',
                    TransactionNumber: '',
                    Consigner: '',
                    Narration: '',
                    Vendor: '',
                    BillDate: '',
                    TransactionDate: '',
                    Vendorname: '',
                    netAMT: 0,
                    PBAID: '',
                    REFF: '',
                })

                setProducts();
                // setSearchQuery("");

                if (onRefresh) {
                    onRefresh(); // refreshReceiptData from Bills.jsx
                }
            }
        });
    };


    return (
        <div>
            <div
                className="modal fade"
                id="EditPurchaseReturn"
                tabIndex={-1}
                aria-labelledby="exampleModalFullscreenLabel"
                aria-hidden="true"
                style={{ display: "none" }}
            >
                <div className="modal-dialog modal-fullscreen mbgcolor">
                    <div className="modal-content mbgcolor">
                        <div className="modal-body mbgcolor">
                            <div className="modal-content mbgcolor">
                                <div className="page-wrapper-new p-0 mbgcolor">
                                    <div className="content mbgcolor ">
                                        <form onSubmit={handleSubmit}>
                                            <div className="modal-header border-0 custom-modal-header  ">
                                                <div className="page-title d-flex justify-content-between align-items-center w-75 ">
                                                    <div className="page-title ">
                                                        <h4>Purchase Bill Return :</h4>
                                                    </div>

                                                </div>

                                                <div className="page-btn">
                                                    <Link className="btn btn-secondary"
                                                        aria-label="Close"

                                                        onClick={showExitAlert}
                                                    >
                                                        <ArrowLeft className="me-2" />
                                                        Back to index
                                                    </Link>
                                                </div>
                                            </div>


                                            <div className="modal-body custom-modal-body">

                                                <div>

                                                    <div className="row">
                                                        <div className="col-lg-3 col-md-6 col-sm-12">
                                                            <div className="form-label">
                                                                <label className="form-label required">Transaction No</label>
                                                                <input
                                                                    type="text"
                                                                    className="form-control"
                                                                    value={formData.TransactionNumber || ""}
                                                                    name="TransactionNumber"
                                                                    onChange={handleChange}
                                                                />
                                                            </div>
                                                        </div>

                                                        <div className="col-lg-3 col-md-6 col-sm-12">
                                                            <div className="input-blocks">
                                                                <label className="form-label required">Transaction Date</label>
                                                                <div className="input-groupicon calender-input">

                                                                    <input
                                                                        type="date"
                                                                        className="form-control"
                                                                        value={selectedDate}
                                                                        placeholder="Choose Date"
                                                                        onChange={handleDateChange}
                                                                    />
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div className="col-lg-3 col-md-6 col-sm-12">
                                                            <div className="form-label">
                                                                <label className="form-label required">Bill No</label>
                                                                <input
                                                                    type="text"
                                                                    className="form-control"
                                                                    value={formData.BillNumber || ""}
                                                                    readOnly
                                                                    title="Bill only contains Numbers."
                                                                    onChange={handleChange}
                                                                />
                                                            </div>
                                                        </div>

                                                        <div className="col-lg-3 col-md-6 col-sm-12">
                                                            <div className="input-blocks">
                                                                <label className="form-label required">Bill Date</label>
                                                                <div className="input-groupicon calender-input">

                                                                    <input
                                                                        type="date"
                                                                        className="form-control"
                                                                        value={formData.BillDate}
                                                                        placeholder="Choose Date"
                                                                        onChange={(e) =>
                                                                            setFormData({
                                                                                ...formData,
                                                                                BillDate: e.target.value,
                                                                            })
                                                                        }

                                                                    />

                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div className="col-lg-5 col-md-6 col-sm-12">
                                                            <div className="add-product">
                                                                <label className="form-label required">Vendor</label>
                                                                <input
                                                                    type="text"
                                                                    className="form-control"
                                                                    ref={VendorRef}
                                                                    required
                                                                    value={formData.Vendorname || ""}
                                                                    name="Vendor"
                                                                    onChange={handleChange}
                                                                />
                                                            </div>
                                                        </div>

                                                        <div className="col-lg-4 col-md-6 col-sm-12">
                                                            <div className="form-label">
                                                                <label className="form-label required">Consigner</label>
                                                                <input
                                                                    type="text"
                                                                    className="form-control"
                                                                    ref={ConsignerRef}
                                                                    required
                                                                    value={formData.Consigner || ""}
                                                                    name="Consigner"
                                                                    onChange={handleChange}
                                                                />
                                                            </div>
                                                        </div>
                                                        <div className="col-lg-3 col-md-6 col-sm-12">
                                                            <div className="form-label">
                                                                <label className="form-label required">Payment Mode</label>
                                                                <input
                                                                    type="text"
                                                                    className="form-control"
                                                                    required
                                                                    value={formData.PTYPE || ""}
                                                                    name="PMODE"
                                                                    onChange={handleChange}
                                                                />
                                                            </div>
                                                        </div>

                                                        <div className="col-lg-12 col-sm-6 col-12">
                                                            <div className="mb-5">
                                                                <label className="form-label">Narration</label>
                                                                <textarea
                                                                    className="form-control mb-1"
                                                                    value={formData.Narration || ""}
                                                                    name="Narration"
                                                                    onChange={handleChange}
                                                                    rows={2}
                                                                />
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div className="row border border-1 p-3">
                                                        <h5>Purchase Bill Return Detail:</h5>
                                                        <div className="col-lg-12">
                                                            <div className="table-responsive" style={{ maxHeight: "200px", overflowY: "auto" }}>
                                                                <table className="table table-bordered" style={{ borderCollapse: "collapse" }}>
                                                                    <thead className="thead-dark"
                                                                        style={{
                                                                            position: "sticky",
                                                                            top: 0,
                                                                            zIndex: 2,
                                                                            backgroundColor: "#343a40",
                                                                            color: "white"
                                                                        }}
                                                                    >
                                                                        <tr>
                                                                            <th>Product</th>
                                                                            <th>Batch NO.</th>
                                                                            <th>Expiry Date</th>
                                                                            <th>UOM</th>
                                                                            <th>Quantity</th>
                                                                            <th>Total</th>
                                                                            <th>Return Quantity</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        {products && products.length > 0 ? (
                                                                            products.map((product, index) => (
                                                                                <tr key={index}>
                                                                                    <td>{product.ProductName}</td>
                                                                                    <td>{product.batchno}</td>
                                                                                    <td>{product.expirydate}</td>
                                                                                    <td>{product.UOMtitle}</td>
                                                                                    <td>{product.Quantity}</td>
                                                                                    <td>{product.Total}</td>
                                                                                    <td>
                                                                                        <input
                                                                                            type="number"
                                                                                            className="form-control form-control-sm"
                                                                                            name="returnQuantity"
                                                                                            required
                                                                                            min={0}
                                                                                            value={product.returnQuantity || 0}
                                                                                            onChange={(e) => handleReturnQuantityChange(index, e)}
                                                                                        />
                                                                                    </td>
                                                                                </tr>
                                                                            ))
                                                                        ) : (
                                                                            <tr>
                                                                                <td colSpan="7" className="text-center">
                                                                                    No Products Available
                                                                                </td>
                                                                            </tr>
                                                                        )}
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div className="col-lg-12">
                                                        <div className="modal-footer-btn">
                                                            <button
                                                                type="button"
                                                                className="btn btn-cancel me-2"

                                                                onClick={showExitAlert}
                                                            >
                                                                Cancel
                                                            </button>
                                                            <button className="btn btn-submit">
                                                                Save Purchase
                                                            </button>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </form>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default EditPurchaseReturn
