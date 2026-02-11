using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;
using System.ComponentModel.DataAnnotations;

namespace Project.Model.Domain
{
    public class DepartmentMaster
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
        public string COMPANYID { get; set; }
     
    }
}
