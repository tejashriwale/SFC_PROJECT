namespace Project.Model.Domain
{
    public class SP_AddUpdSQuatationMaster
    {
        public string SQAMAID	 { get; set; }
        public string SQCAID { get; set; }
        public string CAID { get; set; } 
        public string SPRNO { get; set; }
        public double SQMNAMT	 { get; set; }
        public double SQGAMT { get; set; }
        public string SQNO	 { get; set; }
        public string SQDATE { get; set; }
        public string SQDUEDATE { get; set; }
        public string SQTRANSPORTTERMS	 { get; set; }
        public string SQPAYMENTTERMS { get; set; }
        public Boolean @ISAPPROVED { get; set; }
        public string SQPDUEDATE { get; set; }
        public int SQNODAYS	 { get; set; }
        public string COMPANYID	 { get; set; }
        public string DEPTID { get; set; }
        public string SRREFF { get; set; }
        public string SQTRANSPORTID { get; set; }
        public string SATATUSID { get; set; }
    }
}
