using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace matrimonybackendApi.Models.Domain
{
    public class PdfUpload
    {
        [Key]
        public int Id { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }

        public string FileName { get; set; }

        public string? FileDescription { get; set; }

    

        public string FilePath { get; set; }
    }
}
