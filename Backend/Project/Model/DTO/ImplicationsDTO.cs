using System.ComponentModel.DataAnnotations;

namespace Project.Model.DTO
{
    public class ImplicationsDTO
    {
        [Key]
        public string? IAutoID { get; set; }
        public string? ITitle { get; set; }
        public string? IValue { get; set; }
        public string? IGroup { get; set; }
    }
}
