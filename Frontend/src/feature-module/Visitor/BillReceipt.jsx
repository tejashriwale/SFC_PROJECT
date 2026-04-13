import React, { useState } from "react";
import { Printer, Download } from "lucide-react";
import { jsPDF } from 'jspdf';
import html2canvas from 'html2canvas';
import { getUserData } from "../../Context/UserData";

// Number to words (Indian system)
const numToWordsHelper = (n) => {
    const ones = ['', 'One', 'Two', 'Three', 'Four', 'Five', 'Six', 'Seven', 'Eight', 'Nine', 'Ten',
        'Eleven', 'Twelve', 'Thirteen', 'Fourteen', 'Fifteen', 'Sixteen', 'Seventeen', 'Eighteen', 'Nineteen'];
    const tens = ['', '', 'Twenty', 'Thirty', 'Forty', 'Fifty', 'Sixty', 'Seventy', 'Eighty', 'Ninety'];
    if (n === 0) return 'Zero';
    let str = '';
    if (Math.floor(n / 10000000) > 0) { str += numToWordsHelper(Math.floor(n / 10000000)) + ' Crore '; n %= 10000000; }
    if (Math.floor(n / 100000) > 0) { str += numToWordsHelper(Math.floor(n / 100000)) + ' Lakh '; n %= 100000; }
    if (Math.floor(n / 1000) > 0) { str += numToWordsHelper(Math.floor(n / 1000)) + ' Thousand '; n %= 1000; }
    if (Math.floor(n / 100) > 0) { str += numToWordsHelper(Math.floor(n / 100)) + ' Hundred '; n %= 100; }
    if (n > 0) { if (n < 20) { str += ones[n]; } else { str += tens[Math.floor(n / 10)]; if (n % 10 > 0) str += ' ' + ones[n % 10]; } }
    return str.trim();
};

const getAmountInWords = (amount) => {
    const rupees = Math.floor(amount);
    const paise = Math.round((amount - rupees) * 100);
    let words = 'Rupees ' + numToWordsHelper(rupees);
    if (paise > 0) words += ' and ' + numToWordsHelper(paise) + ' Paise';
    words += ' Only';
    return words;
};

const BillReceipt = ({ modalId, billNo, date, farmerName, mobileNo, vehNo, tableData, totalAmount }) => {
    const { userdetail } = getUserData();
    const companyName = 'SHRAM JEEVAN AGRO FARMS PRODUCER COMPANY LTD.';
    const deptName = 'Pathare, Tal. Sinnar, Dist. Nashik - 422104. Email : swapnildawange8@gmail.com';
    const printAreaId = `bill-print-${modalId}`;
    const [loading, setLoading] = useState(false);

    const captureCanvas = async () => {
        const el = document.getElementById(printAreaId);
        if (!el) return null;
        return await html2canvas(el, { scale: 2, useCORS: true, backgroundColor: '#fff' });
    };

    const handlePrint = () => {
        // Open window FIRST (sync, not blocked by browser)
        const printWindow = window.open('', '_blank');
        if (!printWindow) { alert('Please allow popups for this site.'); return; }
        printWindow.document.write('<html><body><p>Loading bill...</p></body></html>');

        setLoading(true);
        captureCanvas().then(canvas => {
            if (!canvas) { printWindow.close(); setLoading(false); return; }
            const imgData = canvas.toDataURL('image/png');

            printWindow.document.open();
            printWindow.document.write(`<html><head><title>Bill - ${billNo}</title>
            <style>
                * { -webkit-print-color-adjust: exact !important; print-color-adjust: exact !important; }
                body { margin: 0; padding: 0; display: flex; justify-content: center; align-items: flex-start; }
                img { width: 100%; height: auto; }
                @media print { @page { size: A5 portrait; margin: 2mm; } }
            </style>
            </head><body><img src="${imgData}" /></body></html>`);
            printWindow.document.close();
            printWindow.focus();
            setTimeout(() => { printWindow.print(); printWindow.close(); setLoading(false); }, 500);
        }).catch(() => { printWindow.close(); setLoading(false); });
    };

    const handleDownloadPDF = async () => {
        setLoading(true);
        try {
            const canvas = await captureCanvas();
            if (!canvas) return;
            const imgData = canvas.toDataURL('image/png');
            const doc = new jsPDF('p', 'mm', 'a5');
            const pageWidth = doc.internal.pageSize.getWidth();
            const pageHeight = doc.internal.pageSize.getHeight();
            const imgWidth = pageWidth - 6;
            const imgHeight = (canvas.height * imgWidth) / canvas.width;

            doc.addImage(imgData, 'PNG', 3, 3, imgWidth, Math.min(imgHeight, pageHeight - 6));
            doc.save(`Bill_${billNo || 'receipt'}.pdf`);
        } finally {
            setLoading(false);
        }
    };

    const handleClose = () => {
        const modalEl = document.getElementById(modalId);
        if (!modalEl) return;
        const bsModal = window.bootstrap?.Modal?.getInstance(modalEl);
        if (bsModal) {
            bsModal.hide();
        }
        // Always cleanup after a short delay
        setTimeout(() => {
            modalEl.classList.remove('show');
            modalEl.style.display = 'none';
            modalEl.setAttribute('aria-hidden', 'true');
            modalEl.removeAttribute('aria-modal');
            modalEl.removeAttribute('role');
            document.body.classList.remove('modal-open', 'force-landscape-view');
            document.body.style.overflow = '';
            document.body.style.paddingRight = '';
            document.querySelectorAll('.modal-backdrop').forEach(b => b.remove());
        }, 200);
    };

    const minRows = 8;
    const emptyRowCount = Math.max(0, minRows - tableData.length);

    return (
        <div className="modal fade" id={modalId} tabIndex="-1" aria-hidden="true"
            data-bs-backdrop="static" data-bs-keyboard="false">
            <div className="modal-dialog modal-dialog-centered" style={{ maxWidth: '600px', margin: 'auto' }}>
                <div className="modal-content" style={{ border: 'none', borderRadius: '0', boxShadow: '0 8px 32px rgba(0,0,0,0.18)' }}>

                    {/* Action Buttons Bar */}
                    <div style={{
                        display: 'flex', justifyContent: 'flex-end', alignItems: 'center',
                        gap: '10px', padding: '10px 14px',
                        background: '#f8f9fa', borderBottom: '1px solid #dee2e6',
                        position: 'relative', zIndex: 10
                    }}>
                        <button type="button" onClick={handlePrint} disabled={loading} title="Print"
                            style={{
                                borderRadius: '6px', padding: '6px 18px', fontSize: '14px', fontWeight: '600',
                                background: loading ? '#6c757d' : '#0d6efd', color: '#fff', border: 'none',
                                cursor: loading ? 'wait' : 'pointer', display: 'flex', alignItems: 'center', gap: '6px',
                                boxShadow: '0 2px 6px rgba(13,110,253,0.3)'
                            }}>
                            <Printer size={16} /> {loading ? 'Wait...' : 'Print'}
                        </button>
                        <button type="button" onClick={handleDownloadPDF} disabled={loading} title="Download PDF"
                            style={{
                                borderRadius: '6px', padding: '6px 18px', fontSize: '14px', fontWeight: '600',
                                background: loading ? '#6c757d' : '#198754', color: '#fff', border: 'none',
                                cursor: loading ? 'wait' : 'pointer', display: 'flex', alignItems: 'center', gap: '6px',
                                boxShadow: '0 2px 6px rgba(25,135,84,0.3)'
                            }}>
                            <Download size={16} /> {loading ? 'Wait...' : 'PDF'}
                        </button>
                        <button type="button" aria-label="Close" onClick={handleClose}
                            style={{
                                borderRadius: '6px', padding: '6px 14px', fontSize: '18px', fontWeight: 'bold',
                                background: '#dc3545', color: '#fff', border: 'none',
                                cursor: 'pointer', lineHeight: '1',
                                boxShadow: '0 2px 6px rgba(220,53,69,0.3)'
                            }}>
                            ✕
                        </button>
                    </div>

                    {/* Bill Content */}
                    <div className="modal-body" style={{ padding: '0', overflow: 'hidden', fontFamily: "'Arial', sans-serif" }}>
                        <div id={printAreaId} style={{
                            padding: '20px 28px 16px',
                            background: '#fff',
                            position: 'relative'
                        }}>

                            {/* Company Header */}
                            <div style={{ textAlign: 'center', marginBottom: '4px' }}>
                                <div style={{
                                    fontSize: '17px', fontWeight: 'bold', fontStyle: 'italic',
                                    color: '#1a5c1a', textTransform: 'uppercase', lineHeight: '1.3',
                                    textDecoration: 'underline', textUnderlineOffset: '3px',
                                    letterSpacing: '0.3px', whiteSpace: 'nowrap'
                                }}>
                                    {companyName}
                                </div>
                                <div style={{
                                    fontSize: '11px', color: '#333', marginTop: '4px',
                                    letterSpacing: '0.2px'
                                }}>
                                    {deptName}
                                </div>
                            </div>

                            {/* Divider */}
                            <div style={{ borderTop: '2px solid #000', margin: '6px 0 4px' }}></div>

                            {/* Bill No & Date */}
                            <div style={{
                                display: 'flex', justifyContent: 'space-between',
                                padding: '6px 0 4px', fontSize: '14px'
                            }}>
                                <div>No. <strong>{billNo}</strong></div>
                                <div>Date : <strong>{date}</strong></div>
                            </div>

                            {/* Farmer Info */}
                            <div style={{ fontSize: '14px', marginBottom: '4px' }}>
                                <div style={{ marginBottom: '4px' }}>
                                    Name : <strong>{farmerName}</strong>
                                    <span style={{
                                        borderBottom: '1px dotted #999', display: 'inline-block',
                                        width: 'calc(100% - 200px)', marginLeft: '4px', verticalAlign: 'bottom'
                                    }}></span>
                                </div>
                                <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                                    <div>Mob. No. <strong>{mobileNo}</strong>
                                        <span style={{ borderBottom: '1px dotted #999', display: 'inline-block', width: '60px', marginLeft: '4px' }}></span>
                                    </div>
                                    <div>Veh. No. <strong>{vehNo || ''}</strong>
                                        <span style={{ borderBottom: '1px dotted #999', display: 'inline-block', width: '80px', marginLeft: '4px' }}></span>
                                    </div>
                                </div>
                            </div>

                            {/* Table */}
                            <table style={{
                                width: '100%', borderCollapse: 'collapse', marginTop: '8px',
                                border: '1.5px solid #333'
                            }}>
                                <thead>
                                    <tr style={{ background: '#f0f7f0' }}>
                                        <th style={{ border: '1px solid #333', textAlign: 'center', padding: '6px 4px', width: '7%', fontSize: '12px', fontWeight: 'bold' }}>Sr.<br />No.</th>
                                        <th style={{ border: '1px solid #333', textAlign: 'left', padding: '6px 8px', width: '33%', fontSize: '12px', fontWeight: 'bold' }}>Carat / Box</th>
                                        <th style={{ border: '1px solid #333', textAlign: 'center', padding: '6px 4px', width: '14%', fontSize: '12px', fontWeight: 'bold' }}>Rate</th>
                                        <th style={{ border: '1px solid #333', textAlign: 'center', padding: '6px 4px', width: '16%', fontSize: '12px', fontWeight: 'bold' }}>Weight</th>
                                        <th style={{ border: '1px solid #333', textAlign: 'right', padding: '6px 8px', width: '30%', fontSize: '12px', fontWeight: 'bold' }}>Amount</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {tableData.map((item, index) => (
                                        <tr key={index}>
                                            <td style={{ borderLeft: '1px solid #333', borderRight: '1px solid #333', textAlign: 'center', padding: '4px', fontSize: '13px' }}>{index + 1}</td>
                                            <td style={{ borderLeft: '1px solid #333', borderRight: '1px solid #333', padding: '4px 8px', fontSize: '13px' }}>{item.cratecount}</td>
                                            <td style={{ borderLeft: '1px solid #333', borderRight: '1px solid #333', textAlign: 'center', padding: '4px', fontSize: '13px' }}>{item.rate}</td>
                                            <td style={{ borderLeft: '1px solid #333', borderRight: '1px solid #333', textAlign: 'center', padding: '4px', fontSize: '13px' }}>{item.weight}</td>
                                            <td style={{ borderLeft: '1px solid #333', borderRight: '1px solid #333', textAlign: 'right', padding: '4px 8px', fontSize: '13px' }}>
                                                ₹ {(parseFloat(item.rate || 0) * parseFloat(item.weight || 0)).toFixed(2)}
                                            </td>
                                        </tr>
                                    ))}

                                    {Array.from({ length: emptyRowCount }).map((_, idx) => (
                                        <tr key={`empty-${idx}`}>
                                            <td style={{ borderLeft: '1px solid #333', borderRight: '1px solid #333', padding: '10px' }}>&nbsp;</td>
                                            <td style={{ borderLeft: '1px solid #333', borderRight: '1px solid #333' }}>&nbsp;</td>
                                            <td style={{ borderLeft: '1px solid #333', borderRight: '1px solid #333' }}>&nbsp;</td>
                                            <td style={{ borderLeft: '1px solid #333', borderRight: '1px solid #333' }}>&nbsp;</td>
                                            <td style={{ borderLeft: '1px solid #333', borderRight: '1px solid #333' }}>&nbsp;</td>
                                        </tr>
                                    ))}

                                    <tr style={{ borderTop: '1.5px solid #333' }}>
                                        <td colSpan="4" style={{ border: '1px solid #333', textAlign: 'right', fontWeight: 'bold', padding: '6px 8px', fontSize: '13px' }}>Total</td>
                                        <td style={{ border: '1px solid #333', textAlign: 'right', fontWeight: 'bold', padding: '6px 8px', fontSize: '14px' }}>₹ {totalAmount.toFixed(2)}</td>
                                    </tr>
                                </tbody>
                            </table>

                            {/* Amount in Words */}
                            <div style={{ padding: '8px 0 4px', fontSize: '12px', borderBottom: '1px solid #ccc', marginBottom: '6px' }}>
                                <em><strong>Amount in Words:</strong> {getAmountInWords(totalAmount)}</em>
                            </div>

                            {/* Footer - Signatures */}
                            <div style={{
                                display: 'flex', justifyContent: 'space-between',
                                alignItems: 'flex-end', padding: '28px 0 8px'
                            }}>
                                <div style={{ textAlign: 'center' }}>
                                    <div style={{ borderTop: '1px solid #333', width: '140px', paddingTop: '4px', fontSize: '12px', fontWeight: '500' }}>
                                        Customer Sign
                                    </div>
                                </div>
                                <div style={{ textAlign: 'center' }}>
                                    <div style={{ fontSize: '11px', color: '#1a7a1a', fontWeight: 'bold', marginBottom: '2px' }}>
                                        {companyName}
                                    </div>
                                    <div style={{ borderTop: '1px solid #333', width: '180px', paddingTop: '4px', fontSize: '12px', fontWeight: '500' }}>
                                        For {companyName.split(' ')[0]} {companyName.split(' ')[1]}...
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default BillReceipt;
