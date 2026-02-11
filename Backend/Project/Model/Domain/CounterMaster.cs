using System.ComponentModel.DataAnnotations;

namespace Project.Model.Domain
{
    public class CounterMaster
    {
        [Key]
        public string CAID { get; set; }
        public string CDATE { get; set; }

        public string CNAME { get; set; }
        public string CINCHARGE { get; set; }
        public string CINTERCOM { get; set; }
        public string CCODE { get; set; }
        public string DAID { get; set; }
        public string WAID { get; set; }
        public string COMPANYID { get; set; }
        public string DEPTID { get; set; }
    }
}
