using System.ComponentModel.DataAnnotations;

namespace Project.Model.Domain
{
    public class _GET_DepartmentMaster_
    {
        [Key]
        public string DEPTAID { get; set; }
        public string WAID { get; set; }
        public string STOREID { get; set; }

        public string DNAME { get; set; }
        public string DINTERCOMEXTENSION { get; set; }
        public string DCODE { get; set; }
        public string DADDRESS { get; set; }
        public string DCITY { get; set; }
        public string DAREA { get; set; }
        public string DSTATE { get; set; }
        public string DLANDMARK { get; set; }
        public string DPINCODE { get; set; }
        public string WNAME { get; set; }
        public string SSTATENAME { get; set; }
        public string SDISTRICT { get; set; }
       
    }
}
