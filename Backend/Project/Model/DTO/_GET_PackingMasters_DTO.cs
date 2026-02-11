using System.ComponentModel.DataAnnotations;

namespace Project.Model.DTO
{
    public class _GET_PackingMasters_DTO
    {
        [Key]
        public string PCAID { get; set; }
        public string PAID { get; set; }
        public string PCTITLE { get; set; }
        public string PCSERIAL { get; set; }
        public string PCQUANTITYUOM { get; set; }
        public float PCQUANTITYAMOUNT { get; set; }
       
    }
}
