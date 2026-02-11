using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.Model.DTO;
using System.Net.Http.Headers;
using System.Text;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WhatsApp3Controller : ControllerBase
    {
        [HttpPost("send")]
        public async Task<IActionResult> SendWhatsappMessage([FromBody] WhatsappRequest3 request)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "qbGmMS2TByWc6jdIPFU3UcLUmkqpulp9NqtjwGBB");

            var payload = new
            {
                template = "1007659848102492",
                to = request.MOBILE,
                header = new
                {
                    uploaded = new
                    {
                        url = request.HeaderUrl ?? ""
                    }
                },
                body = new List<string>
                {
                    request.CROP_TYPE,
                    request.DATE,
                    request.TOTALJALI,
                    request.TOTALWEIGHT,
                    request.TOPRATE,
                    request.BOTTOMRATE,
                    request.TOTALRATE,
                   
                  
                    
                }
            };

            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://app.aiwati.com/api/whatsapp-base/send_template?api_key=qbGmMS2TByWc6jdIPFU3UcLUmkqpulp9NqtjwGBB", content);

            var responseContent = await response.Content.ReadAsStringAsync();
            return Ok(responseContent);
        }
    }
}

