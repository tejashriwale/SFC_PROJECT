using matrimonybackendApi.Models.DTO;
using matrimonybackendApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;

namespace matrimonybackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfUploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public PdfUploadController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> Upload([FromForm] PdfUploadDto dto)
        {
            if (dto.Files.Count != dto.FileNames.Count)
                return BadRequest("Each file must have a name.");

            var uploadPath = Path.Combine(_env.WebRootPath, "Images");
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            for (int i = 0; i < dto.Files.Count; i++)
            {
                var file = dto.Files[i];
                var fileName = dto.FileNames[i];

                // ✅ Check if filename already ends with .pdf, if not, append
                if (!fileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                    fileName += ".pdf";

                var filePath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            return Ok(new { Message = "PDFs uploaded successfully." });
        }


        [HttpPost("Merge")]
        public async Task<IActionResult> Merge([FromForm] PdfMergeDto dto)
        {
            if (dto.Files.Count != 2)
                return BadRequest("Please upload exactly 2 PDF files.");

            var uploadPath = Path.Combine(_env.WebRootPath, "Images");  // Change to "Images" folder
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var outputFilePath = Path.Combine(uploadPath, $"{dto.OutputFileName}.pdf");

            using (var outputDoc = new PdfDocument())
            {
                foreach (var file in dto.Files)
                {
                    using var stream = file.OpenReadStream();
                    var inputDoc = PdfReader.Open(stream, PdfDocumentOpenMode.Import);

                    for (int i = 0; i < inputDoc.PageCount; i++)
                        outputDoc.AddPage(inputDoc.Pages[i]);
                }

                outputDoc.Save(outputFilePath);
            }

            return Ok(new
            {
                Message = "PDFs merged successfully.",
                MergedFileName = dto.OutputFileName,
                Url = $"/Images/{dto.OutputFileName}.pdf"  // Ensure the merged file path is also in the Images folder
            });
        }

    }
}
