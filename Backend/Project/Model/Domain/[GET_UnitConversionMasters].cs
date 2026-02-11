using System.ComponentModel.DataAnnotations;

namespace Project.Model.Domain
{
    public class _GET_UnitConversionMasters_
    {

        [Key]
        public string UAID { get; set; }
        public double FROMUNITQUANTITY { get; set; }
        public string FROMUNITUOM { get; set; }
        public double TOUNITQUANTITY { get; set; }
        public string TOUNITUOM { get; set; }
        public string utype { get; set; }
       
    }
}
