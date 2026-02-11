namespace Project.Model.Domain
{
    public class GET_Analysis
    {
        public string AAID { get; set; }
        public string QAMAID { get; set; }
        public string VAID { get; set; }
        public string PRAID { get; set; }
        public double NETAMT { get; set; }
        public Boolean ISAPPROVED { get; set; }
        public string DEPTID { get; internal set; }
        public string COMPANYID { get; internal set; }
    }
}
