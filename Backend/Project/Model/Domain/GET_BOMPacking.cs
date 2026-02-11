using System.ComponentModel.DataAnnotations;

namespace Project.Model.Domain
{
    public class GET_BOMPacking
    {
        [Key]
        public string BAID { get; set; }
        public string PAID { get; set; }
        public string PCAID { get; set; }
        public string BOMPACKING { get; set; }
        public string BRAWPRODUCTQUANTITY { get; set; }
        public string BRAWPRODUCTQUANTITYUOM { get; set; }
        public string PRODUCTNAME { get; set; }


    }
}
