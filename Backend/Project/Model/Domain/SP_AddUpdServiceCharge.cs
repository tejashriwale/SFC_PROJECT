namespace Project.Model.Domain
{
    public class SP_AddUpdServiceCharge
    {
        public string SCID        { get; set; }
        public string RATEAPPLICABLE { get; set; }
        public string SERVICETYPE { get; set; }
        public string RATETYPE    { get; set; }
        public double RATE        { get; set; }
        public Boolean ISAPPROVAL { get; set; }
        public string COMPANYID   { get; set; }
        public string DEPTID { get; set; }
        public string LEJAR { get; set; }
        public string UAID { get; set; }
        public string DATE { get; set; }
    }
}
