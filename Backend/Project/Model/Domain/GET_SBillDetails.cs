namespace Project.Model.Domain
{
    public class GET_SBillDetails
    {
        public string SBDAID		 { get; set; }
        public string SBAID		 { get; set; }
        public string SBPRODUCT	 { get; set; }
        public string SBDUOM		 { get; set; }
        public string SBDQUANTITY	 { get; set; }
        public double SBDRATE { get; set; }
        public double SBDTAMT { get; set; }
        public double SBDIGST { get; set; }
        public double SBDSGST { get; set; }
        public double SBDCGST { get; set; }
        public double SBDTOTAL { get; set; }
        public int HSNCODE { get; set; }
        public string BATCHNO { get; set; }
        public string EXPIRYDATE { get; set; }

    }
}
