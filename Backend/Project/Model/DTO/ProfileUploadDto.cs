using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Project.Model.DTO
{
    public class ProfileUploadDto
    {
        public List<IFormFile> Files { get; set; }

        public List<string> FileNames { get; set; }

        public string FileDescription { get; set; }
    }
}
