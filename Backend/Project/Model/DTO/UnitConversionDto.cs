using System.ComponentModel.DataAnnotations;

namespace Project.Model.DTO
{
    public class UnitConversionDto
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
