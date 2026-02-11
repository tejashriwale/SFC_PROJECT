using System.ComponentModel.DataAnnotations;

namespace Project.Model.Domain
{
    public class Implications
    {
        [Key]
        public string? IAutoID { get; set; }
        public string? ITitle { get; set; }
        public string? IValue { get; set; }
        public string? IGroup { get; set; }
    }
}
