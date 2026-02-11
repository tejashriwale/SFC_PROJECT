import React from 'react'

function AddRokadaVyapariBharana_1() {
    return (
        <div>
            {/* Add Purchase */}
            <div className="modal fade" id="add-bharna">
                <div className="modal-dialog  modal-dialog-centered modal-fullscreen">
                    <div className="modal-content mbgcolor">
                        <div className="mbgcolor page-wrapper-new p-0 " style={{ overflow: 'hidden', height: '100vh' }}>
                            <div className="content ms-4 me-4 mbgcolor">
                                <div className="modal-header border-0 custom-modal-header mbgcolor">
                                    <div className="page-title">
                                        <h4>रोकडा व्यापारी भरणा </h4>
                                    </div>
                                    <button type="button"
                                        className="btn btn-cancel me-2"
                                        // data-bs-dismiss="modal" 
                                        onClick={handleExit}
                                    >मागे
                                    </button>
                                </div>
                                <form onSubmit={handleSubmit}>

                                    <div className="row mb-3">
                                        <div className="col-md-3 mb-2 mx-auto">
                                            <label className="form-label required">लिलाव वपेरकर्ता</label>
                                            <Select
                                                placeholder="निवडा"
                                                classNamePrefix="react-select"
                                                options={farmer}
                                                openMenuOnFocus={true}
                                                styles={customStyles}
                                                name="User"
                                                ref={UserRef}
                                                value={farmer.find(option => option.value === formData.User) || null}
                                                onChange={(selectedOption) => {
                                                    setFormData(prevState => ({
                                                        ...prevState,
                                                        User: selectedOption ? selectedOption.value : '',
                                                    }));
                                                    fetchServiceCharge(selectedOption.value);
                                                    // for clear data
                                                    setCashInputs([]);
                                                    setOnlineInputs([]);
                                                    setTableData([]);
                                                    setFormData(prevState => ({
                                                        ...prevState,
                                                        Bank: '',
                                                        Totaldeposit: '',
                                                    }));

                                                    if (TotaldepositRef.current) {
                                                        TotaldepositRef.current.focus();
                                                    }
                                                }}
                                            />
                                        </div>
                                    </div>

                                    <div className="row mt-3">
                                        <div className="col-md-2 mb-2">
                                            <label className="form-label required">एकूण रक्कम </label>
                                            <input
                                                type="text"
                                                className="form-control"
                                                readOnly
                                                name="Totalamount"
                                                // ref={msgRef}
                                                value={totalAmount}
                                                onChange={handleInputChange}
                                            />
                                        </div>

                                        <div className="col-md-2 mb-2">
                                            <label className="form-label required">एकूण टोटल जमा </label>
                                            <input
                                                type="text"
                                                className="form-control"
                                                name="Totaldeposit"
                                                ref={TotaldepositRef}
                                                value={formData.Totaldeposit}
                                                onKeyDown={(e) => handleKeyDown(e, TransactionRef)}
                                                // value={grandTotal}
                                                onChange={handleInputChange}
                                            />
                                        </div>

                                        <div className="col-md-2 mb-2">
                                            <label className="form-label required">उर्वरित रक्कम</label>
                                            <input
                                                type="text"
                                                className="form-control"
                                                name="Reamaining"
                                                readOnly
                                                // ref={msgRef}
                                                value={remaningAmount}
                                                onChange={handleInputChange}
                                            />
                                        </div>

                                        {/* <div className="col-md-3 mb-2">
                                                      <label className="form-label required">व्यवहार प्रकार</label>
                                                      <Select
                                                          placeholder="निवडा"
                                                          classNamePrefix="react-select"
                                                          options={TransctionType}
                                                          openMenuOnFocus={true}
                                                          name="Transaction"
                                                          required
                                                          ref={TransactionRef}
                                                          value={TransctionType.find(option => option.value === formData.Transaction) || null}
                                                          onChange={(selectedOption) => {
                                                              setFormData(prevState => ({
                                                                  ...prevState,
                                                                  Transaction: selectedOption ? selectedOption.value : '',
                                                              }));
                                                              if (BankRef.current) {
                                                                  BankRef.current.focus();
                                                              }
                                                          }}
                                                      />
                                                  </div> */}

                                        <div className="col-md-6 mb-2">
                                            <label className="form-label required">बँक नाव </label>
                                            <Select
                                                placeholder="निवडा"
                                                classNamePrefix="react-select"
                                                options={BankName}
                                                openMenuOnFocus={true}
                                                styles={customStyles}
                                                name="Bank"
                                                required
                                                ref={BankRef}
                                                value={BankName.find(option => option.value === formData.Bank) || null}
                                                onChange={(selectedOption) => {
                                                    setFormData(prevState => ({
                                                        ...prevState,
                                                        Bank: selectedOption ? selectedOption.value : '',
                                                    }));
                                                    // if (AmountRef.current) {
                                                    //     AmountRef.current.focus();
                                                    // }
                                                }}
                                            />
                                        </div>

                                        <div className="col-md-12 mb-2">
                                            <label className="form-label">स्पष्टीकरण </label>
                                            <input
                                                type="text"
                                                className="form-control"
                                                name="Description"
                                                // ref={msgRef}
                                                value={formData.Description}
                                                onChange={handleInputChange}
                                            />
                                        </div>
                                    </div>
                                    <div className="border p-3 rounded shadow-sm mt-2">
                                        <div className="col-lg-12">
                                            <div className="modal-body-table overflow-auto max-vh-100" >
                                                <div className="table-responsive" style={{ height: "calc(40vh - 120px)" }}>
                                                    <table className="table table-bordered table-sm">
                                                        <thead className="thead-dark" style={{ position: 'sticky', top: 0, zIndex: 1, backgroundColor: '#343a40' }}>
                                                            <tr>
                                                                <th className="col-2" style={{ textAlign: 'center' }}>व्यापारी नावे </th>
                                                                <th className="col-1" style={{ textAlign: 'center' }}>दिनांक</th>
                                                                <th className="col-1" style={{ textAlign: 'center' }}>रक्कम  </th>
                                                                <th className="col-1" style={{ textAlign: 'center' }}>कॅश रक्कम</th>
                                                                <th className="col-1" style={{ textAlign: 'center' }}>ऑनलाइन रक्कम</th>
                                                                {/* <th className="col-1" style={{ textAlign: 'center' }}>क्रुती</th> */}
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            {tableData.map((row, index) => (
                                                                <tr key={index}>
                                                                    <td style={{ textAlign: 'center' }}>{row.vname}</td>
                                                                    <td style={{ textAlign: 'center' }}>{row.trndt}</td>
                                                                    <td style={{ textAlign: 'center' }}>{row.bpamt}</td>
                                                                    <td style={{ textAlign: 'center' }}>
                                                                        <input
                                                                            type="text"
                                                                            value={cashInputs[index] || row.cash || ""}
                                                                            readOnly

                                                                            className="form-control form-control-sm border-0 border-bottom border-warning text-center"
                                                                        />
                                                                    </td>
                                                                    <td style={{ textAlign: 'center' }}>
                                                                        <input
                                                                            type="text"
                                                                            value={onlineInputs[index] || ""}

                                                                            className="form-control form-control-sm border-0 border-bottom border-warning text-center"
                                                                        />
                                                                    </td>
                                                                </tr>
                                                            ))}
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>




                                    <div className="col-lg-12 mt-3">
                                        <div className="btn-addproduct ">
                                            <button type="button"
                                                className="btn btn-cancel me-2"
                                                // data-bs-dismiss="modal" 
                                                onClick={handleExit}
                                            >मागे
                                            </button>

                                            <button type="submit"
                                                className="btn btn-submit"
                                                onClick={() => {
                                                    if (totalCash > 0 && formData.Bank) {
                                                        setAmount(totalCash)
                                                        const modal = document.getElementById("Rokda");
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
                                                    }
                                                }}
                                            >सेव्ह
                                            </button>
                                        </div>
                                    </div>

                                </form>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <RokdaAddCash onRefresh={refreshData} TOTALAMOUNT={Amount} onClose={handleModalClose} />
        </div>

    )
}

export default AddRokadaVyapariBharana_1
