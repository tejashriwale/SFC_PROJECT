using System.ComponentModel.DataAnnotations;

namespace Project.Model.DTO
{
    public class BOMPackingDto
    {
        [Key]
        public string BAID { get; set; }
        public string PAID { get; set; }
        public string PCAID { get; set; }
        public string BOMPACKING { get; set; }
        public string BRAWPRODUCTQUANTITY { get; set; }
        public string BRAWPRODUCTQUANTITYUOM { get; set; }
        public string COMPANYID { get; set; }
        public string DEPTID { get; set; }
    }
}
