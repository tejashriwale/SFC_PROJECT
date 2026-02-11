namespace Project.Model.DTO
{
    public class SP_AddUpdCashCounterDto
    {
        public string PKID { get; set; }
        public string TOKENNO { get; set; }
        public string BILLNO { get; set; }
        public string TARIKH { get; set; }
        public string FULLNAME { get; set; }
        public string VEHICLENO { get; set; }
        public double AMOUNT { get; set; }
        public double BILLAMOUNT { get; set; }
        public double CASHAMOUNT { get; set; }
        public double CHEQUEAMOUNT { get; set; }
        public double ONLINEAMOUNT { get; set; }
        public double REMAININGAMOUNT { get; set; }
        public double STATUS { get; set; }
        public double HAMALI { get; set; }
        public string COMPANYID { get; set; }
        public string DEPTID { get; set; }
        public string BAID { get; set; }
        public string FAID { get; set; }
        public Boolean ISCHECKED { get; set; }
        public Boolean ISCLOSE { get; set; }
    }
}
