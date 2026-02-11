using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project.Model.Domain
{
    public class ProfileUpload
    {
        [Key]
        public int Id { get; set; }

        [NotMapped]
        public IFormFile File { get; set; } // ✅ File Object (Not stored in DB)

        public string FileName { get; set; } // ✅ Only Name (Without Extension)

        public string? FileDescription { get; set; }

        public long FileSizeInBytes { get; set; }

        public string FilePath { get; set; } // ✅ Final File Path in Server
    }
}
