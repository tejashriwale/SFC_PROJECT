using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Model.Domain;
using Project.Model.DTO;
using Project.Repository;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileUploadController : ControllerBase
    {
        private readonly IProfileUploadRepository _repository;

        public ProfileUploadController(IProfileUploadRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> Upload([FromForm] ProfileUploadDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (requestDto.Files == null || requestDto.Files.Count == 0)
                return BadRequest("Please upload at least one file.");

            if (requestDto.FileNames.Count != requestDto.Files.Count)
                return BadRequest("Number of file names must match the number of files.");

            var validationResult = ValidateFileUpload(requestDto);
            if (!validationResult.Item1)
                return BadRequest(validationResult.Item2);

            var domainFiles = new List<ProfileUpload>();

            for (int i = 0; i < requestDto.Files.Count; i++)
            {
                string sanitizedFileName = Path.GetFileNameWithoutExtension(requestDto.FileNames[i]);

                domainFiles.Add(new ProfileUpload
                {
                    File = requestDto.Files[i],
                    FileSizeInBytes = requestDto.Files[i].Length,
                    FileName = sanitizedFileName,
                    FileDescription = requestDto.FileDescription
                });
            }

            var uploadedFiles = await _repository.UploadAsync(domainFiles);

            return Ok(uploadedFiles.Select(f => new
            {
                f.FileName,
                f.FileSizeInBytes,
                f.FilePath
            }));
        }

        private Tuple<bool, string> ValidateFileUpload(ProfileUploadDto request)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

            foreach (var file in request.Files)
            {
                string fileExtension = Path.GetExtension(file.FileName).ToLower();

                if (!allowedExtensions.Contains(fileExtension))
                    return Tuple.Create(false, $"Unsupported File Type: {fileExtension}. Only JPG, JPEG, and PNG are allowed.");

                if (file.Length > 10 * 1024 * 1024)
                    return Tuple.Create(false, "File size exceeds 10MB.");

                var header = new byte[4];
                using (var hs = file.OpenReadStream())
                {
                    if (hs.Read(header, 0, 4) < 4)
                        return Tuple.Create(false, "Invalid image file.");
                }
                bool isJpg = header[0] == 0xFF && header[1] == 0xD8;
                bool isPng = header[0] == 0x89 && header[1] == 0x50 && header[2] == 0x4E && header[3] == 0x47;
                if (!isJpg && !isPng)
                    return Tuple.Create(false, "File content is not a valid JPG or PNG image.");
            }

            return Tuple.Create(true, "Valid");
        }
    }
}
