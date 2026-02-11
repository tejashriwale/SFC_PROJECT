//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using System.IO;
//using iTextSharp.text;
//using iTextSharp.text.pdf;

//namespace YourNamespace.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class PdfDownloadController : ControllerBase
//    {
//        public class YourDataModel
//        {
//            public string VNAME { get; set; }
//            public string DATE { get; set; }
//            public string CARETCOUNT { get; set; }
//            public string TOTALWEIGHT { get; set; }
//            public string TOTALRATE { get; set; }
//            public string BANKNAME { get; set; }
//            public string ACCOUNTNAME { get; set; }
//            public string ACCOUNTNO { get; set; }
//            public string IFSCCODE { get; set; }
//        }

//        [HttpGet("download")]
//        public IActionResult DownloadPdf()
//        {
//            // Example data - replace this with your dynamic data source
//            var data = new List<YourDataModel>
//            {
//                new YourDataModel {
//                    VNAME = "Vendor A",
//                    DATE = "2025-05-30",
//                    CARETCOUNT = "10",
//                    TOTALWEIGHT = "20kg",
//                    TOTALRATE = "2000",
//                    BANKNAME = "Bank XYZ",
//                    ACCOUNTNAME = "Vendor A Account",
//                    ACCOUNTNO = "1234567890",
//                    IFSCCODE = "IFSC0001234"
//                },
//                new YourDataModel {
//                    VNAME = "Vendor B",
//                    DATE = "2025-05-29",
//                    CARETCOUNT = "15",
//                    TOTALWEIGHT = "30kg",
//                    TOTALRATE = "3000",
//                    BANKNAME = "Bank ABC",
//                    ACCOUNTNAME = "Vendor B Account",
//                    ACCOUNTNO = "0987654321",
//                    IFSCCODE = "IFSC0005678"
//                }
//            };

//            byte[] pdfBytes = GeneratePdf(data);

//            return File(pdfBytes, "application/pdf", "DynamicData.pdf");
//        }

//        private byte[] GeneratePdf(List<YourDataModel> data)
//        {
//            using (MemoryStream ms = new MemoryStream())
//            {
//                Document document = new Document(PageSize.A4, 10, 10, 10, 10);
//                PdfWriter.GetInstance(document, ms);
//                document.Open();

//                PdfPTable table = new PdfPTable(9)
//                {
//                    WidthPercentage = 100
//                };

//                // Add header row
//                AddCell(table, "VNAME", true);
//                AddCell(table, "DATE", true);
//                AddCell(table, "CARETCOUNT", true);
//                AddCell(table, "TOTALWEIGHT", true);
//                AddCell(table, "TOTALRATE", true);
//                AddCell(table, "BANKNAME", true);
//                AddCell(table, "ACCOUNTNAME", true);
//                AddCell(table, "ACCOUNTNO", true);
//                AddCell(table, "IFSCCODE", true);

//                // Add data rows
//                foreach (var item in data)
//                {
//                    AddCell(table, item.VNAME);
//                    AddCell(table, item.DATE);
//                    AddCell(table, item.CARETCOUNT);
//                    AddCell(table, item.TOTALWEIGHT);
//                    AddCell(table, item.TOTALRATE);
//                    AddCell(table, item.BANKNAME);
//                    AddCell(table, item.ACCOUNTNAME);
//                    AddCell(table, item.ACCOUNTNO);
//                    AddCell(table, item.IFSCCODE);
//                }

//                document.Add(table);
//                document.Close();

//                return ms.ToArray();
//            }
//        }

//        private void AddCell(PdfPTable table, string text, bool isHeader = false)
//        {
//            Font font = isHeader ? FontFactory.GetFont("Arial", 12, Font.BOLD) : FontFactory.GetFont("Arial", 10);
//            PdfPCell cell = new PdfPCell(new Phrase(text, font))
//            {
//                HorizontalAlignment = Element.ALIGN_CENTER,
//                Padding = 5,
//                BackgroundColor = isHeader ? BaseColor.LIGHT_GRAY : BaseColor.WHITE
//            };
//            table.AddCell(cell);
//        }
//    }
//}
