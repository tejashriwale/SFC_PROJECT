using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class WhatsApp1Controller : ControllerBase
{
    [HttpPost("send")]
    public async Task<IActionResult> SendWhatsappMessages([FromBody] List<WhatsappRequest1> requests)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "qbGmMS2TByWc6jdIPFU3UcLUmkqpulp9NqtjwGBB");

        var results = new List<string>();

        foreach (var request in requests)
        {
            var payload = new
            {
                template = "1062437295813531",
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
                    request.MAID,
                    request.CROP_TYPE,
                    request.TOKEN_NO,
                    request.DATE,
                    request.CARETS_COUNT
                }
            };

            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://app.aiwati.com/api/whatsapp-base/send_template?api_key=qbGmMS2TByWc6jdIPFU3UcLUmkqpulp9NqtjwGBB", content);

            var responseContent = await response.Content.ReadAsStringAsync();
            results.Add($"To: {request.MOBILE}, Response: {responseContent}");
        }

        return Ok(results);
    }
}
