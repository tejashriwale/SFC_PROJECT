namespace Project.Model.Domain
{
    public class SP_AddUpdHRMHoliday
    {
        public string HAID		 { get; set; }
        public string ADDHOLIDAY	 { get; set; }
        public string SDATE		 { get; set; }
        public string EDATE		 { get; set; }
        public int NODAYS		 { get; set; }
        public Boolean HASTATUS { get; set; }
        public string COMPANYID { get; set; }
        public string DEPTID { get; set; }

    }
}
