using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Project.Model.DTO
{
    public class VideoUploadDto
    {
        [Required]
        public List<IFormFile> Files { get; set; }

        [Required]
        public List<string> FileNames { get; set; }
    }
}
