using Microsoft.AspNetCore.Mvc;
using Project.Model.Domain;
using System.Net.Http;
using System.Text.Json;

[ApiController]
[Route("api/[controller]")]
public class StateController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public StateController(IHttpClientFactory httpClientFactory)
    {
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
        };

        _httpClient = new HttpClient(handler)
        {
            Timeout = TimeSpan.FromSeconds(30)
        };
    }

    [HttpGet]
    public async Task<IActionResult> GetStates()
    {
        try
        {
            var apiUrl = "http://115.124.105.220/API/GetState";
            var response = await _httpClient.GetAsync(apiUrl);

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, "External API call failed");

            var jsonData = await response.Content.ReadAsStringAsync();
            var states = JsonSerializer.Deserialize<List<State>>(jsonData);

            return Ok(states);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error: {ex.Message}");
        }
    }
}
