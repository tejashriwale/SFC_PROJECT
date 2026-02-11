namespace Project.Model.DTO
{
    public class GET_FarmerLedgerReportDto
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
    }
}
