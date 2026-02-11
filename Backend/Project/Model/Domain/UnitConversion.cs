using System.ComponentModel.DataAnnotations;

namespace Project.Model.Domain
{
    public class UnitConversion
    {
        [Key]
        public string UAID { get; set; }
        public double FORMUNITQUANTITY { get; set; }
        public string FROMUNITUOM { get; set; }
        public double TOUMITQUANTITY { get; set; }
        public string TOUNITUOM { get; set; }
        public string utype { get; set; }
        public string COMPANYID { get; set; }
        public string DEPTID { get; set; }
    }
}
