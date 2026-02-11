namespace Project.Model.Domain
{
    public class GET_PBillNoSearch
    {
        public string PBDAID		 { get; set; }
        public string PBPRODUCT	 { get; set; }
        public string PBDUOM		 { get; set; }
        public string PBDQUANTITY	 { get; set; }
        public double PBDRATE		 { get; set; }
        public double PBDTAMT		 { get; set; }
        public double PBDIGST		 { get; set; }
        public double PBDSGST		 { get; set; }
        public double PBDCGST	 { get; set; }
        public string PBAID { get; set; }
        public string PBVENDORID { get; set; }
        public string PBCONSIGNER { get; set; }
        public string PBVEHICALNO { get; set; }
        public string PBPOSUPPLY { get; set; }
        public string PBBILLNO { get; set; }
        public string PBDATE { get; set; }
        public string PBBILLDATE { get; set; }
        public string PBDUEDATE { get; set; }
        public string PBNARRATION { get; set; }
        public string PBTRANSPORT { get; set; }
        public string PBTERMANDCONDITION { get; set; }
        public string PBCHALLANNO { get; set; }
        public double PBDISCOUNT { get; set; }
        public double PBNETAMOUNT { get; set; }
        public string VBILLNO { get; set; }
        public int QNODAYS { get; set; }
        public string QPAYMENTTERMS { get; set; }
        public string QPDUEDATE { get; set; }
        public string UOMTITLE { get; set; }
        public string VENDORNAME { get; set; }
        public string CONTACTNUMBER { get; set; }
        public string PRODUCTNAME { get; set; }
        public string BATCHNO { get; set; }
        public string EXPIRYDATE { get; set; }
        public string PAYMENTMODE { get; set; }
        public string PAYMENTYPE { get; set; }
    }
}
