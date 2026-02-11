using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.Model.Domain;
using System.Net.Http.Headers;
using System.Text;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OtpController : ControllerBase
    {
        [HttpPost("send")]
        public async Task<IActionResult> SendOtp([FromBody] OtpRequest request)
        {
            // 1. Generate OTP
            var otp = new Random().Next(1000, 9999).ToString();

            // 2. Prepare message
            var templateMessage = "We just sent your OTP.. Enter it to continue.. {{1}}";
            var messageToSend = templateMessage.Replace("{{1}}", otp);

            // 3. Prepare WhatsApp API payload
            var payload = new
            {
                template_id = "1638602300143643",
                phone = request.PhoneNumber,
                message = messageToSend
            };

            // 4. Send message (mocked here – replace with real API call)
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "qbGmMS2TByWc6jdIPFU3UcLUmkqpulp9NqtjwGBB");

            var response = await client.PostAsync("https://your-whatsapp-api/send",
                new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                return Ok(new { otp, status = "sent" });
            }

            return StatusCode((int)response.StatusCode, "Failed to send OTP");
        }
    }

}
