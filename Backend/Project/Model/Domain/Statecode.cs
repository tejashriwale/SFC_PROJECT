using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Project.Model.Domain
{
    public class Statecode
    {
        [Key]
        public string SAID { get; set; }
        public string SCIRCLENAME { get; set; }
        public string SREGIONNAME { get; set; }
        public string SDIVISIONNAME { get; set; }
        public string SOFFICENAME { get; set; }
        public string SPINCODE { get; set; }
        public string SOFFICETYPE { get; set; }
        public string SDELIVERY { get; set; }
        public string SDISTRICT { get; set; }
        public string SSTATENAME { get; set; }
        public string SSTATECODE { get; set; }
        public Double SLATITUDE { get; set; }
        public Double SLONTITUDE { get; set; }
        public string COMPANYID { get; set; }
        public string DEPTID { get; set; }
    }
}
