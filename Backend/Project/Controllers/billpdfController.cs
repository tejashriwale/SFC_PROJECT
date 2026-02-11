//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
//using System.Net.Http.Headers;
//using System.Text;

//namespace test.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class billpdfController : ControllerBase
//    {
//        // 🟢 1. Send WhatsApp Messages using existing uploaded PDF
//        [HttpPost("sendWhatsappMessages")]
//        public async Task<IActionResult> SendWhatsappMessages([FromBody] List<WhatsappRequest> requests)
//        {
//            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
//            var results = new List<object>();

//            foreach (var request in requests)
//            {
//                // 📎 PDF file already uploaded from frontend
//                var fileName = request.PdfFileName; // e.g. bill_31.pdf
//                var encryptedLink = EncryptionHelper.Encrypt(fileName);
//                var whatsappPdfLink = $"{baseUrl}/billpdf/{Uri.EscapeDataString(encryptedLink)}";



//                // 🌐 WhatsApp API call setup
//                var client = new HttpClient();
//                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "qbGmMS2TByWc6jdIPFU3UcLUmkqpulp9NqtjwGBB");

//                var payload = new
//                {
//                    template = "1667225473942121",
//                    to = request.MOBILE,
//                    header = new
//                    {
//                        uploaded = new
//                        {
//                            url = request.HeaderUrl ?? ""
//                        }
//                    },
//                    body = new List<string>
//                    {
//                        request.VNAME,
//                        request.DATE,
//                        request.CARETCOUNT,
//                        request.TOTALWEIGHT,
//                        request.TOTALRATE,
//                        whatsappPdfLink,
//                        request.BANKNAME,
//                        request.ACCOUNTNAME,
//                        request.ACCOUNTNO,
//                        request.IFSCCODE
//                    }
//                };

//                var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
//                var response = await client.PostAsync("https://app.aiwati.com/api/whatsapp-base/send_template?api_key=qbGmMS2TByWc6jdIPFU3UcLUmkqpulp9NqtjwGBB", content);
//                var responseContent = await response.Content.ReadAsStringAsync();

//                results.Add(new
//                {
//                    name = request.VNAME,
//                    mobile = request.MOBILE,
//                    pdfLink = whatsappPdfLink,
//                    generatedFileName = fileName,
//                    status = response.IsSuccessStatusCode ? "Sent ✅" : $"Failed ❌: {responseContent}"
//                });
//            }

//            return Ok(new
//            {
//                message = "All messages processed ✅",
//                result = results
//            });
//        }

//        // 🟡 2. Serve static PDF file to WhatsApp
//        [HttpGet("/billpdf/{encrypted}")]
//        public IActionResult GetGeneratedPdf(string encrypted)
//        {
//            try
//            {
//                var fileName = EncryptionHelper.Decrypt(Uri.UnescapeDataString(encrypted));
//                var filePath = Path.Combine(@"Z:\RELEASE\ReactPublish\Sayali\publish\wwwroot\files", fileName);

//                if (!System.IO.File.Exists(filePath))
//                {
//                    Console.WriteLine("❌ File Not Found at path: " + filePath);
//                    return NotFound("PDF not found");
//                }

//                var mimeType = "application/pdf";
//                var fileBytes = System.IO.File.ReadAllBytes(filePath);

//                return File(fileBytes, mimeType, fileName);
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("🚫 Decryption Failed: " + ex.Message);
//                return BadRequest("Invalid or tampered link ❌");
//            }
//        }


//        // 🔵 3. WhatsApp Template Request Format
//        public class WhatsappRequest
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
//            public string MOBILE { get; set; }
//            public string HeaderUrl { get; set; }
//            public string PdfFileName { get; set; } // 🆕 Frontend will send uploaded file name (e.g. "bill_31.pdf")
//        }
//    }
//}
