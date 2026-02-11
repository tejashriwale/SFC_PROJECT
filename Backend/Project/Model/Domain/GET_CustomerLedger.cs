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
    }
}
