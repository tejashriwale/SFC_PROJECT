using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace matrimonybackendApi.Models.DTO
{
    public class PdfMergeDto
    {
        [Required]
        public List<IFormFile> Files { get; set; }

        [Required]
        public string OutputFileName { get; set; }

        public string? FileDescription { get; set; }
    }

}
