import React from "react";
import "../Reports/Report.css"

const DayBook = () => {
    return (
        <div className="report-container">
            <style>
                {`
          @media print {
            @page {
              size: A4 landscape;
            }
            tbody:after {
              page-break-after: avoid !important;
            }
            div {
              page-break-after: avoid !important;
            }
          }
          table, th, td {
            border: 1px solid black;
            border-collapse: collapse;
          }
          th, td {
            padding: 8px;
            text-align: center;
          }
          .fullSize {
            width: 100%;
          }
          .aCenter {
            text-align: center;
          }
          .aLeft {
            text-align: left;
          }
          .aRight {
            text-align: right;
          }
          .tBold {
            font-weight: bold;
          }
          .tItalic {
            font-style: italic;
          }
        `}
            </style>

            <article className="landscape">
                <section id="idSocietyDetailsBlock" style={{ display: 'block' }}>
                    <div className="fullSize aCenter" style={{ fontSize: '170%' }}>
                        Society Title
                    </div>
                    <div className="fullSize aCenter" style={{ fontSize: '130%' }}>
                        नोंदणी क्रमांक व दिनांक: RegNo &nbsp; Date
                    </div>
                    <div className="fullSize aCenter" style={{ borderBottom: '2px solid gray' }}>
                    </div>
                </section>

                <section id="idReportTitleBlock">
                    <div className="fullSize aCenter" style={{ fontSize: '170%' }}>
                        Report Title
                    </div>
                    <div className="fullSize aCenter" style={{ fontSize: '130%' }}>
                        Report Sub Title
                    </div>
                </section>

                <section id="idReportDataBlock">
                    <table className="fullSize">
                        <thead>
                            <tr>
                                <th colSpan="5" style={{ width: '50%', borderRight: '2px solid black', fontSize: '150%' }}>जमा</th>
                                <th colSpan="5" style={{ width: '50%', fontSize: '150%' }}>नावे</th>
                            </tr>
                            <tr>
                                <th style={{ width: '5%', fontSize: '130%' }}>खाते क्र.</th>
                                <th style={{ width: '22.5%', fontSize: '140%' }}>तपशील</th>
                                <th style={{ width: '7.5%', fontSize: '140%' }}>रोख</th>
                                <th style={{ width: '7.5%', fontSize: '140%' }}>लेखांतरण</th>
                                <th style={{ width: '7.5%', fontSize: '140%', borderRight: '2px solid black' }}>एकूण</th>
                                <th style={{ width: '6%', fontSize: '130%' }}>खाते क्र.</th>
                                <th style={{ width: '21.5%', fontSize: '140%' }}>तपशील</th>
                                <th style={{ width: '7.5%', fontSize: '140%' }}>रोख</th>
                                <th style={{ width: '7.5%', fontSize: '140%' }}>लेखांतरण</th>
                                <th style={{ width: '7.5%', fontSize: '140%' }}>एकूण</th>
                            </tr>
                        </thead>

                        <tbody>
                            {/* Example static data row */}
                            <tr>
                                <td>001</td>
                                <td className="aLeft tBold">उदाहरण खाते</td>
                                <td>5000</td>
                                <td>2000</td>
                                <td style={{ borderRight: '2px solid black' }}>7000</td>
                                <td>002</td>
                                <td className="aLeft tItalic">नावे उदाहरण</td>
                                <td>3000</td>
                                <td>1000</td>
                                <td>4000</td>
                            </tr>
                        </tbody>

                        <tbody className="tBold aRight" style={{ fontSize: '140%' }}>
                            <tr>
                                <td colSpan="2">Total</td>
                                <td>5000</td>
                                <td>2000</td>
                                <td style={{ borderRight: '2px solid black' }}>7000</td>
                                <td colSpan="2">Total</td>
                                <td>3000</td>
                                <td>1000</td>
                                <td>4000</td>
                            </tr>
                        </tbody>
                    </table>
                </section>

                <section id="idSignatureBlock" style={{ display: 'block' }}>
                    <div className="tblDiv" style={{ marginTop: '50px' }}>
                        <div className="c40"></div>
                        <div className="c25" style={{ borderBottom: '1px dotted #D3D3D3' }}></div>
                        <div className="c10"></div>
                        <div className="c25" style={{ borderBottom: '1px dotted #D3D3D3' }}></div>
                    </div>

                    <div className="tblDiv aCenter" style={{ fontSize: '150%' }}>
                        <div className="c40"></div>
                        <div className="c25" style={{ fontWeight: 'bold' }}>सचिव</div>
                        <div className="c10"></div>
                        <div className="c25" style={{ fontWeight: 'bold' }}>चेअरमन</div>
                    </div>
                </section>
            </article>

            <footer>
                <div id="idPageCounter">AppName</div>
            </footer>
        </div>
    );
};

export default DayBook;
