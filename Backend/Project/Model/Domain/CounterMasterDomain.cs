using System.ComponentModel.DataAnnotations;

namespace Project.Model.Domain
{
    public class CounterMasterDomain
    {
        [Key]
        public string? PKID { get; set; }

       
        public string? COMPANYID { get; set; }
        public string? DEPTID { get; set; }
    }
}
