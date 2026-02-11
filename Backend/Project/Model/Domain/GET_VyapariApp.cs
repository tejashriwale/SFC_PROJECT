using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Project.Model.Domain
{
    public class GET_VyapariApp
    {
        public string BAID { get; set; }
        public string MAID  { get; set; }
        public string BTOKANNO				 { get; set; }
        public string DATE		 { get; set; }
        public Boolean ISVYAPARIVERIFIED		 { get; set; }
        public double WEIGHT	  { get; set; }
        public double RATE	 { get; set; }
        public string WEIGHTMINMAX { get; set; }
        public string FNAME { get; set; }
    }
}
