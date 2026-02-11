using System.ComponentModel.DataAnnotations;

namespace Project.Model.DTO
{
    public class _GET_ReturnPolicy_DTOs
    {
        [Key]

        public string RPAID { get; set; }
        public string RPTERMANDCONDITION { get; set; }
        public string RPDURATION { get; set; }
        public string RPDESCRIPTION { get; set; }
        
    }
}
