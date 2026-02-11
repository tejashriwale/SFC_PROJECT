//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
//using System.Net.Http.Headers;
//using System.Text;

//namespace Project.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class FarmerBillController : ControllerBase
//    {
//        // ✅ 1. Send WhatsApp Messages with encrypted PDF link
//        [HttpPost("sendMessages")]
//        public async Task<IActionResult> SendWhatsappMessages([FromBody] List<FarmerWhatsappRequest> requests)
//        {
//            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
//            var results = new List<object>();

//            foreach (var request in requests)
//            {
//                var fileName = request.PdfFileName;
//                var encryptedLink = EncryptionHelper.Encrypt(fileName);
//                var whatsappPdfLink = $"{baseUrl}/farmerpdf/{Uri.EscapeDataString(encryptedLink)}";

//                var client = new HttpClient();
//                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "qbGmMS2TByWc6jdIPFU3UcLUmkqpulp9NqtjwGBB");

//                var payload = new
//                {
//                    template = "580623545093937", // eg: "1667225473942121"
//                    to = request.mobile,
//                    header = new
//                    {
//                        uploaded = new
//                        {
//                            url = request.headerUrl ?? ""
//                        }
//                    },
//                    body = new List<string>
//                    {
//                        request.croptype,
//                        request.date,
//                        request.totaljali,
//                        request.totalweight,
//                        request.toprate,
//                        request.bottomrate,
//                        request.totalrate,
//                        whatsappPdfLink
//                    }
//                };


//                var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
//                var response = await client.PostAsync("https://app.aiwati.com/api/whatsapp-base/send_template?api_key=qbGmMS2TByWc6jdIPFU3UcLUmkqpulp9NqtjwGBB", content);
//                var responseContent = await response.Content.ReadAsStringAsync();

//                results.Add(new
//                {
//                    croptype = request.croptype,
//                    mobile = request.mobile,
//                    pdfLink = whatsappPdfLink,
//                    generatedFileName = fileName,
//                    status = response.IsSuccessStatusCode ? "Sent ✅" : $"Failed ❌: {responseContent}"
//                });
//            }

//            return Ok(new
//            {
//                message = "All farmer messages processed ✅",
//                result = results
//            });
//        }

//        // ✅ 2. Serve encrypted static PDF file
//        [HttpGet("/farmerpdf/{encrypted}")]
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

//        // 📦 3. Model for request
//        public class FarmerWhatsappRequest
//        {
//            public string croptype { get; set; }
//            public string date { get; set; }
//            public string totaljali { get; set; }
//            public string totalweight { get; set; }
//            public string toprate { get; set; }
//            public string bottomrate { get; set; }
//            public string totalrate { get; set; }
//            public string mobile { get; set; }
//            public string headerUrl { get; set; }
//            public string PdfFileName { get; set; } // 📎 From frontend
//        }
//    }
//}
