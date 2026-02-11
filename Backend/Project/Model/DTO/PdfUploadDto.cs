using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace matrimonybackendApi.Models.DTO
{
    public class PdfUploadDto
    {
        [Required]
        public List<IFormFile> Files { get; set; }

        [Required]
        public List<string> FileNames { get; set; }

        public string? FileDescription { get; set; }
    }
}
