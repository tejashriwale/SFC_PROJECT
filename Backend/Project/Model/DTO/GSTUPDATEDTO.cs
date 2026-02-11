using System.ComponentModel.DataAnnotations;

namespace Project.Model.DTO
{
    public class GSTUPDATEDTO
    {
        [Key]
        public string HCODE { get; set; }
        public double HIGST { get; set; }
        public double HSGST { get; set; }
        public double HCGST { get; set; }
        public double HCESS { get; set; }
    }
}
