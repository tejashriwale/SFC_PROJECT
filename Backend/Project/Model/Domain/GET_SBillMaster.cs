namespace Project.Model.Domain
{
    public class GET_SBillMaster
    {
        public string SBAID		 { get; set; }
        public string SBVENDORID	 { get; set; }
        public string SBCONSIGNER	 { get; set; }
        public string SBVEHICALNO	 { get; set; }
        public string SBPOSUPPLY	 { get; set; }
        public string SBBILLNO { get; set; }
        public string SBDATE		 { get; set; }
        public string SBBILLDATE	 { get; set; }
        public string SBDUEDATE { get; set; }
        public string SBNARRATION			 { get; set; }
        public string SBTRANSPORT			 { get; set; }
        public string SBTERMANDCONDITION	 { get; set; }
        public string SBCHALLANNO			 { get; set; }
        public double SBDISCOUNT			 { get; set; }
        public double SBNETAMOUNT			 { get; set; }
        public string VBILLNO				 { get; set; }
        public int QNODAYS				 { get; set; }
        public string QPAYMENTTERMS		 { get; set; }
        public string QPDUEDATE { get; set; }
        public string SCREFF { get; set; }
        public string PAYMENTMODE { get; set; }
        public double SBGROSSAMT	 { get; set; }
        public double SBILLTOTAL { get; set; }
        public string VENDORNAME { get; set; }
        public string TRANSPORTER { get; set; }
        public string STATUSID { get; set; }
    }
}
