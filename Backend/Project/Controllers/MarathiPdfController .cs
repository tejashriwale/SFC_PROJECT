//using Microsoft.AspNetCore.Mvc;
//using PdfSharpCore.Pdf;
//using TheArtOfDev.HtmlRenderer.PdfSharp;
//using System.IO;

//namespace MarathiPdfAPI.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class MarathiPdfController : ControllerBase
//    {
//        [HttpGet("download")]
//        public IActionResult DownloadMarathiPdf()
//        {
//            // Marathi message
//            var marathiText = "नमस्कार! ही मराठीत तयार केलेली पीडीएफ आहे.";

//            // Path to font
//            var fontPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/fonts/Mangal.ttf");

//            // HTML with inline font style
//            var html = $@"
//            <html>
//            <head>
//                <style>
//                    @font-face {{
//                        font-family: 'Mangal';
//                        src: url('file:///{fontPath.Replace("\\", "/")}');
//                    }}
//                    body {{
//                        font-family: 'Mangal';
//                        font-size: 20px;
//                    }}
//                </style>
//            </head>
//            <body>
//                <p>{marathiText}</p>
//            </body>
//            </html>";

//            // Generate PDF
//            using var pdf = PdfGenerator.GeneratePdf(html, PdfSharpCore.PageSize.A4);
//            using var stream = new MemoryStream();
//            pdf.Save(stream);
//            stream.Position = 0;

//            return File(stream.ToArray(), "application/pdf", "MarathiText.pdf");
//        }
//    }
//}
