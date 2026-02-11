namespace Project.Model.Domain
{
    public class GET_CashCounter
    {
        public string PKID	 { get; set; }
        public string TOKENNO { get; set; }
        public string TARIKH { get; set; }
        public string FULLNAME  		 { get; set; }
        public string VEHICLENO { get; set; }
        public string BILLNO { get; set; }
        public double AMOUNT { get; set; }
        public double BILLAMOUNT   { get; set; }
        public double CASHAMOUNT { get; set; }
        public double CHEQUEAMOUNT { get; set; }
        public double ONLINEAMOUNT { get; set; }
        public double REMAININGAMOUNT { get; set; }
        public Boolean STATUS { get; set; }
        public Boolean ISCLOSE { get; set; }
        public string BAID { get; set; }
        public string FAID { get; set; }
        public string CHEQUENO { get; set; }
        public DateTime DATEO { get; set; }

    }
}
