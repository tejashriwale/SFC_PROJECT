using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Model.Domain
{
    public class uploadPdf
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
