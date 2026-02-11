using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Mvc;

namespace YourApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private static bool isFirebaseInitialized = false;

        public NotificationController()
        {
            if (!isFirebaseInitialized)
            {
                // 🔐 Use the path to your uploaded JSON file
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile("arthafarmer-firebase-adminsdk.json") // rename the uploaded file if needed
                });
                isFirebaseInitialized = true;
            }
        }

        public class NotificationRequest
        {
            public List<string> DeviceTokens { get; set; }
            public string Title { get; set; }
            public string Body { get; set; }
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendNotification([FromBody] NotificationRequest request)
        {
            if (request.DeviceTokens == null || !request.DeviceTokens.Any())
                return BadRequest("⚠️ Device tokens are required!");

            var messages = request.DeviceTokens.Select(token => new Message
            {
                Token = token,
                Notification = new Notification
                {
                    Title = request.Title,
                    Body = request.Body
                },
                Android = new AndroidConfig
                {
                    Priority = Priority.High,
                    Notification = new AndroidNotification
                    {
                        Sound = "default",
                        ChannelId = "default_channel" // This should match your Android app
                    }
                },
                Apns = new ApnsConfig
                {
                    Aps = new Aps
                    {
                        Sound = "default"
                    }
                }
            }).ToList();

            int successCount = 0;
            List<string> responses = new();

            foreach (var msg in messages)
            {
                try
                {
                    var res = await FirebaseMessaging.DefaultInstance.SendAsync(msg);
                    responses.Add($"✔️ Success: {res}");
                    successCount++;
                }
                catch (Exception ex)
                {
                    responses.Add($"❌ Failed: {ex.Message}");
                }
            }

            return Ok(new
            {
                successCount,
                total = request.DeviceTokens.Count,
                responses
            });
        }
    }
}
