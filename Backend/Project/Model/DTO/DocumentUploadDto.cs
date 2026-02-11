using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Project.Model.DTO
{
    public class DocumentUploadDto
    {
        [Required]
        public List<IFormFile> Files { get; set; }

        [Required]
        public List<string> FileNames { get; set; }

        public string? FileDescription { get; set; }
    }
}
