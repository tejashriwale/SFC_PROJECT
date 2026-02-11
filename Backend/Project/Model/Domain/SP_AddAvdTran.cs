namespace Project.Model.Domain
{
    public class SP_AddAvdTran
    {
        public string AVDAID     { get; set; }
        public string ADATE      { get; set; }
        public string CDAID      { get; set; }
        public string SHAID      { get; set; }
        public string MAID       { get; set; }
        public double AVDAMT     { get; set; }
        public double PAMT       { get; set; }
        public string COMPANYID  { get; set; }
        public string DEPTID     { get; set; }
        public string ASID       { get; set; }
        public Boolean ISDELETED { get; set; }
        public double EXAVDAMT	 { get; set; }
        public double TOTAMT { get; set; }
    }
}
