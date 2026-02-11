//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//[Route("api/[controller]")]
//[ApiController]
//public class NotificationController : ControllerBase
//{
//    private readonly FirebaseService _firebaseService;

//    public NotificationController()
//    {
//        _firebaseService = new FirebaseService();
//    }

//    [HttpPost("send")]
//    public async Task<IActionResult> SendNotification([FromBody] NotificationRequest request)
//    {
//        if (request.DeviceTokens == null || request.DeviceTokens.Count == 0)
//            return BadRequest("At least one device token is required.");

//        var result = await _firebaseService.SendNotification(request.DeviceTokens, request.Title, request.Body);
//        return Ok(new { SuccessCount = result.Count, Responses = result });
//    }
//}

//public class NotificationRequest
//{
//    public List<string> DeviceTokens { get; set; }
//    public string Title { get; set; }
//    public string Body { get; set; }
//}
