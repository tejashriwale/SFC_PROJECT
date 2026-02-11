using System.ComponentModel.DataAnnotations;

namespace Project.Model.DTO
{
    public class ReturnPolicyDto
    {
        [Key]

        public string RPAID { get; set; }
        public string RPTERMANDCONDITION { get; set; }
        public string RPDURATION { get; set; }
        public string RPDESCRIPTION { get; set; }
        public string COMPANYID { get; set; }
        public string DEPTID { get; set; }
    }
}
