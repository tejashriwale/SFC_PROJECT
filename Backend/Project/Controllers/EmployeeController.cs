using Microsoft.AspNetCore.Mvc;
using Project.Model.Domain;
using Project.PdfGenerator;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        [HttpPost("generate-pdf")]
        public IActionResult GeneratePdf([FromBody] EmployeeData data)
        {
            // 👇 Required line to remove the license error
            QuestPDF.Settings.License = LicenseType.Community;

            var document = new EmployeePdfDocument(data);
            using var stream = new MemoryStream();
            document.GeneratePdf(stream);

            var pdfBytes = stream.ToArray();
            return File(pdfBytes, "application/pdf", "employee-info.pdf");
        }
    }
}
