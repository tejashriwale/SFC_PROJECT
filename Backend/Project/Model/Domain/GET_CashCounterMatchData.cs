namespace Project.Model.Domain
{
    public class GET_CashCounterMatchData
    {
        public string PKID { get; set; }
        public string FAID { get; set; }
        public string BAID { get; set; }
        public string TOKENNO { get; set; }
        public string BILLNO { get; set; }
        public string TARIKH { get; set; }
        public string FULLNAME         { get; set; }
        public string VEHICLENO { get; set; }
        public double AMOUNT { get; set; }
        public double BILLAMOUNT   { get; set; }
        public double CASHAMOUNT { get; set; }
        public double CHEQUEAMOUNT { get; set; }
        public double ONLINEAMOUNT { get; set; }
        public double REMAININGAMOUNT { get; set; }
        public Boolean STATUS { get; set; }
        public Boolean ISCHECKED { get; set; }
        public string FACCONAME { get; set; }
        public string FBANKNAME { get; set; }
        public string FACCOUNTNO { get; set; }
        public string FIFSCCODE { get; set; }
        public string RECEIPT_BAID { get; set; }
        public string CHEQUEAPPROVED { get; set; }
        public string CHEQUENO { get; set; }
        public string CHEQUEDATE { get; set; }
        public  DateTime SYSDT { get; set; }
        
    }
}
