using Microsoft.AspNetCore.Mvc;
using Project.Model.Domain;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly PdfService _pdfService;

        public BillController(PdfService pdfService)
        {
            _pdfService = pdfService;
        }

        [HttpPost("generate")]
        public IActionResult GenerateBill([FromBody] BillRequest request)
        {
            var pdfLink = _pdfService.GenerateAndSaveBill(
                request.Name, request.Date, request.Crates, request.Weight, request.Amount
            );

            return Ok(new { PdfLink = pdfLink });
        }
    }
}
