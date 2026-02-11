using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FetchPdfController : ControllerBase
    {
        // Path to the folder where PDFs are stored
        private readonly string _pdfFolderPath = @"E:\Project\Project\wwwroot\files";

        [HttpGet("get/{fileName}")]
        public IActionResult GetPdf(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                Console.WriteLine("❌ File name is empty or null.");
                return BadRequest("File name is required.");
            }

            // Combine the base path with the file name
            var fullPath = Path.Combine(_pdfFolderPath, fileName);

            Console.WriteLine($"📥 Requested File: {fileName}");
            Console.WriteLine($"📁 Full Path: {fullPath}");

            // Check if file exists
            if (!System.IO.File.Exists(fullPath))
            {
                Console.WriteLine("🚫 File not found at path.");
                return NotFound("PDF file not found.");
            }

            // Read the file and return as PDF response
            var fileBytes = System.IO.File.ReadAllBytes(fullPath);
            Console.WriteLine("✅ File found and being returned.");
            return File(fileBytes, "application/pdf", fileName);
        }
    }
}
