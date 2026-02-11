using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using matrimonybackendApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Project.Model.Domain;
using Project.Model.DTO;
using Project.Repository;

namespace matrimonybackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoUploadController : ControllerBase
    {
        private readonly IVideoUploadRepository repository;

        public VideoUploadController(IVideoUploadRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] VideoUploadDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (requestDto.Files.Count != requestDto.FileNames.Count)
                return BadRequest("Number of files and filenames must match.");

            var uploadedFiles = new List<object>();

            for (int i = 0; i < requestDto.Files.Count; i++)
            {
                var file = requestDto.Files[i];
                var fileName = requestDto.FileNames[i];

                var validationResult = ValidateVideoUpload(file);
                if (!validationResult.Item1)
                    return BadRequest(validationResult.Item2);

                string sanitizedFileName = Path.GetFileNameWithoutExtension(fileName);

                var domain = new VideoUpload
                {
                    File = file,
                    FileName = sanitizedFileName,
                    IsActive = true
                };

                var uploadedFile = await repository.UploadAsync(domain);

                uploadedFiles.Add(new
                {
                    uploadedFile.FileName,
                    uploadedFile.FilePath
                });
            }

            return Ok(uploadedFiles);
        }

        private Tuple<bool, string> ValidateVideoUpload(IFormFile file)
        {
            var allowedExtensions = new[] { ".mp4", ".mov", ".avi", ".mkv" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(fileExtension))
                return Tuple.Create(false, "Unsupported File Type. Only video files (.mp4, .mov, etc.) are allowed.");

            if (file.Length > 200 * 1024 * 1024)
                return Tuple.Create(false, "File size exceeds 200MB. Please upload a smaller video.");

            return Tuple.Create(true, "Valid");
        }

    }
}
