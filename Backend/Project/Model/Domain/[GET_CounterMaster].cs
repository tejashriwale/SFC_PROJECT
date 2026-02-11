using System.ComponentModel.DataAnnotations;

namespace Project.Model.Domain
{
    public class _GET_CounterMaster_
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
        public string EMPNAME { get; set; }
        public string DNAME { get; set; }
        public string WNAME { get; set; }

    }
}
