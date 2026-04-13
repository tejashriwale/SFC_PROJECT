namespace Project.Model.Domain
{
    public class GET_CustomerLedger
    {
        public string TRAID    { get; set; }
        public string REFAID	 { get; set; }
        public string MAID	 { get; set; }
        public string CCOMPANYNAME { get; set; }
        public double CBAL	 { get; set; }
        public string TRNDT { get; set; }
        public double PAMT { get; set; }
        public double TOTSAMT { get; set; }
        public string VEHICLENO { get; set; }
        public double ADVAMT { get; set; }
        public string CROPNAME { get; set; }
        public double WTPERSKU { get; set; }
        public double QTYCRT { get; set; }
        public double QTYKG { get; set; }
        public double RATE { get; set; }
        public string RELEASEDATE { get; set; }
        public double PCHARGES { get; set; }
    }
}
