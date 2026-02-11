using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project.Model.Domain
{
    public class VideoUpload
    {
        [Key]
        public int Id { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }

        public string FileName { get; set; }
        public Boolean IsActive { get; set; }
        public string FilePath { get; set; }
    }
}
