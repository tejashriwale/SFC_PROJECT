namespace Project.Model.Domain
{
    public class GET_VyapariTransectionReport
    {
        public DateTime TRNDT           { get; set; }
        public string VBILLNO         { get; set; }
        public string PERTICULERS     { get; set; }
        public string TransactionType { get; set; }
        public Decimal BILLAMOUNT      { get; set; }
        public Decimal RECIVEAMOUNT    { get; set; }
        public string VDAID           { get; set; }
        public string VPAID { get; set; }
        public decimal total { get; set; }
    }
}
