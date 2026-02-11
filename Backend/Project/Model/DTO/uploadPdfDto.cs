using System.ComponentModel.DataAnnotations;

namespace Project.Model.DTO
{
    public class uploadPdfDto
    {
        [Required]
        public List<IFormFile> Files { get; set; }

        [Required]
        public List<string> FileNames { get; set; }

        public string? FileDescription { get; set; }
    }
}
