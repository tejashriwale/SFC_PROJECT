using matrimonybackendApi.Models.DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SinglePdfUploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public SinglePdfUploadController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpPost("SinglePdfUpload")]
        public async Task<IActionResult> UploadSinglePdf([FromForm] PdfUploadDto dto)
        {
            if (dto.Files.Count != dto.FileNames.Count)
                return BadRequest("Each file must have a name.");

            var uploadPath = Path.Combine(_env.WebRootPath, "files");
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            List<string> uploadedFilePaths = new List<string>();

            for (int i = 0; i < dto.Files.Count; i++)
            {
                var file = dto.Files[i];
                var fileName = dto.FileNames[i];

                if (!fileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                    fileName += ".pdf";

                var filePath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Relative path for frontend (you can also return full path if needed)
                var relativePath = Path.Combine("files", fileName);
                uploadedFilePaths.Add(relativePath);
            }

            return Ok(new
            {
                Message = "PDFs uploaded successfully ✅",
                FilePaths = uploadedFilePaths
            });
        }
    }
}
