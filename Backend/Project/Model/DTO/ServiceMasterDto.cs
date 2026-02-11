using System.ComponentModel.DataAnnotations;

namespace Project.Model.DTO
{
    public class ServiceMasterDto
    {
        [Key]
        public string? SAID { get; set; }
        public string? SCODE { get; set; }
        public string? SNAME { get; set; }
        public string? STYPE { get; set; }
        public string? SDESCRIPTION { get; set; }

        public string? SCATEGORY { get; set; }
        public string? SSUBCATEGORY { get; set; }
        public string? SHSN { get; set; }
        public string COMPANYID { get; set; }
        public string DEPTID { get; set; }



    }
}
