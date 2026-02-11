using Microsoft.AspNetCore.Mvc;
using Project.Model.Domain;
using Project.Model.DTO;
using Google.Cloud.Translation.V2;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TranslateController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public TranslateController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpPost]
        public async Task<IActionResult> TranslateText([FromBody] string marathiText)
        {
            var apiKey = "AIzaSyCbIE3WMt-tMVB8-s7DpgbIt_oYMmJCGlA"; // Keep this safe later
            var url = $"https://translation.googleapis.com/language/translate/v2?key={apiKey}";

            var requestData = new
            {
                q = marathiText,
                source = "mr",
                target = "en",
                format = "text"
            };

            var response = await _httpClient.PostAsJsonAsync(url, requestData);
            var result = await response.Content.ReadAsStringAsync();

            return Content(result, "application/json");
        }
    }
}
