using System.ComponentModel.DataAnnotations;

namespace Project.Model.Domain
{
    public class _GET_WarehouseMaster_
    {
        [Key]
        public string? WAID { get; set; }
        public string? WNAME { get; set; }
        public string? WAVAILABLECAPCITY { get; set; }
        public string? WINTERCOMEXTENSION { get; set; }
        public string WAREA {  get; set; }
        public string WLANDMARK { get; set; }
        public string WSTOREID { get; set; }

        public string? WADDRESS { get; set; }
        public string? WCITY { get; set; }
        //public string? WDISTRICT { get; set; }
        public string? WSTATE { get; set; }
        //public string? WCONTRY { get; set; }
        public string WPINCODE { get; set; }

        public string? WDESCRIPTION { get; set; }
        public string? SSTATENAME { get; set; }
        public string? SDISTRICT { get; set; }
      
    }
}
