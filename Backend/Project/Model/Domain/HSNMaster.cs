using System.ComponentModel.DataAnnotations;

namespace Project.Model.Model
{
    public class HSNMaster
    {
        [Key]
        public string? HAID { get; set; }
        public string? HCODE { get; set; }
        public string? HTYPE { get; set; }
        public string? HDESCRIPTION { get; set; }
        public double? HIGST { get; set; }
        public double? HSGST { get; set; }
        public double? HCGST { get; set; }
        public double? HCESS { get; set; }
       

    }
}


