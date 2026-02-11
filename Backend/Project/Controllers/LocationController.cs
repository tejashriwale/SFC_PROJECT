using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Newtonsoft.Json.Linq;

namespace YourApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly string googleApiKey = "AIzaSyDGJJqkGy8Wp1FpnP24zrY1TMyUcJfYVYs";

        [HttpGet("get-address")]
        public async Task<IActionResult> GetAddressFromCoordinates([FromQuery] double lat, [FromQuery] double lng)
        {
            string url = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={lat},{lng}&key={googleApiKey}";

            var client = new RestClient(url);
            var request = new RestRequest();
            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
                return StatusCode(500, "Google API Error");

            var json = JObject.Parse(response.Content);
            var address = json["results"]?.FirstOrDefault()?["formatted_address"]?.ToString();

            if (string.IsNullOrEmpty(address))
                return NotFound("Address not found");

            return Ok(new { Latitude = lat, Longitude = lng, Address = address });
        }
    }
}
