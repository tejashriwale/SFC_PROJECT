namespace Project.Model.Domain
{
    public class GET_FarmerLedgerReport
    {
        public string TRNDT { get; set; }
        public string BILLNO { get; set; }
        public string FNAME { get; set; }
        public string CROPNAME { get; set; }
        public string CVARIETY { get; set; }
        public string NARR { get; set; }
        public string TRANSACTIONTYPE { get; set; }
        public double DEBIT { get; set; }
        public double CREDIT { get; set; }
        public double BALANCE { get; set; }
        public string TRAID { get; set; }
        public int TRNSR { get; set; }
        public string REFAID { get; set; }
        public string FAID { get; set; }
        public string CROPID { get; set; }
    }
}
