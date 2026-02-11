namespace Project.Model.Domain
{
    public class GET_FarmerTranData
    {
        public string TRAID   { get; set; }
        public string TRNDT	 { get; set; }
        public int TRNSR	 { get; set; }
        public string TRNSID	 { get; set; }
        public string REFAID	 { get; set; }
        public string CROPID	 { get; set; }
        public string MAID	 { get; set; }
        public string NARR { get; set; }
        public double CBAL	 { get; set; }
        public double ADVAMT	 { get; set; }
        public double PAMT { get; set; }
        public Boolean STATUS { get; set; }
    }
}
