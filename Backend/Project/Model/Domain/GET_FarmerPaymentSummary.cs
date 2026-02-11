namespace Project.Model.Domain
{
    public class GET_FarmerPaymentSummary
    {
        public string TRAID { get; set; }
        public string TRANSACTIONDATE { get; set; }
        public string PAYMENTDATE { get; set; }
        public int TRNSR { get; set; }
        public string REFAID { get; set; }
        public string MAID { get; set; }
        public double PAYMENTAMOUNT { get; set; }
        public string NARRATION { get; set; }
        public string TRNSID { get; set; }
        public string TRNTID { get; set; }
    }

    public class GET_FarmerPaymentSummaryDomain
    {
        public string REFAID { get; set; }
        public string COMPANYID { get; set; }
        public string DEPTID { get; set; }
    }

    public class UpdatePaymentDateDomain
    {
        public string TRAID { get; set; }
        public string PAYMENTDATE { get; set; }
        public string COMPANYID { get; set; }
        public string DEPTID { get; set; }
    }
}
