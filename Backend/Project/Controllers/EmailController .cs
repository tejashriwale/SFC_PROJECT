using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project.Service;
using Project.Model.Requests;

[ApiController]
[Route("api/email")]
public class EmailController : ControllerBase
{
    private readonly EmailService _emailService;

    public EmailController(EmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpPost("send-emails")]
    public async Task<IActionResult> SendEmails([FromBody] EmailRequest request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.RecipientEmail))
            return BadRequest(new { error = "❌ Invalid request! Recipient email is required." });

        var result = await _emailService.SendEmailsAsync(request.RecipientEmail);

        if (result.StartsWith("✅"))
            return Ok(new { message = result });

        return StatusCode(500, new { error = result });
    }
}
namespace Project.Model.Requests
{
    public class EmailRequest
    {
        public string RecipientEmail { get; set; }
    }
}
