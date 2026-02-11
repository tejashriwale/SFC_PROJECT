namespace Project.Model.DTO
{
    public class AddQuatationMasterDTO
    {
        public string QAMAID { get; set; }
        public string QVAID { get; set; }
        public string QVCONTACT { get; set; }
        public string QVEMAIL { get; set; }
        public string QVADDRESS { get; set; }
        public string PRNO { get; set; }
        public double QMNAMT { get; set; }
        public double QGAMT { get; set; }
        public string QNO { get; set; }
        public string QDATE { get; set; }
        public string QDUEDATE { get; set; }
        public string COMPANYID { get; set; }
        public string DEPTID { get; set; }
        public string QTRANSPORTTERMS { get; set; }
        public string QPAYMENTTERMS { get; set; }
        public string QPDUEDATE { get; set; }
        public int QNODAYS { get; set; }
        public Boolean ISAPPROVED { get; set; }
        public string PRREFF { get; set; }
    }
}
