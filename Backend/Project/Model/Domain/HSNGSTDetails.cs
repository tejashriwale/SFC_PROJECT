using System.ComponentModel.DataAnnotations;

namespace Project.Model.Domain
{
    public class HSNGSTDetails
    {
        [Key]
        public double HIGST { get; set; }
        public double HSGST { get; set; }
        public double HCGST { get; set; }
        public double HCESS { get; set; }
       
    }
}
