using System.ComponentModel.DataAnnotations;

namespace Project.Model.Domain
{
    public class PackingMasters
    {
        [Key]
        public string PCAID { get; set; }
        public string PAID { get; set; }
        public string PCTITLE { get; set; }
        public string PCSERIAL { get; set; }
        public string PCQUANTITYUOM { get; set; }
        public float PCQUANTITYAMOUNT { get; set; }
        public string COMPANYID { get; set; }
        public string DEPTID { get; set; }
    }
}


