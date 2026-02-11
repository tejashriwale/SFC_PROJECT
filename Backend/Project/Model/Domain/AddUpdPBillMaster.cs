namespace Project.Model.Domain
{
    public class AddUpdPBillMaster
    {
        public string PBAID { get; set; }
        public string PBVENDORID { get; set; }
        public string PBCONSIGNER { get; set; }
        public string PBVEHICALNO { get; set; }
        public string PBPOSUPPLY { get; set; }
        public string PBBILLNO { get; set; }
        public string PBDATE { get; set; }
        public string PBDUEDATE { get; set; }
        public string PBBILLDATE { get; set; }
        public string PBNARRATION { get; set; }
        public string PBTRANSPORT { get; set; }
        public string PBTERMANDCONDITION { get; set; }
        public string COMPANYID { get; set; }
        public string DEPTID { get; set; }
        public string @PBCHALLANNO { get; set; }
        public double @PBDISCOUNT { get; set; }
        public double PBNETAMOUNT { get; set; }
        public string VBILLNO		{ get; set; }
        public int QNODAYS		 { get; set; }
        public string QPAYMENTTERMS	{ get; set; }
        public string QPDUEDATE     { get; set; }
        public string PCREFF { get; set; }
        public string PAYMENTMODE { get; set; }
    }
}
