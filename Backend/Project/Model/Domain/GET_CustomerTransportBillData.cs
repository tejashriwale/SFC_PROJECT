namespace Project.Model.Domain
{
    public class GET_CustomerTransportBillData
    {
        public string TRAID { get; set; }
        public int TRNSR { get; set; }
        public string TRNDT { get; set; }
        public string CROPID { get; set; }
        public string REFAID { get; set; }
        public string MAID { get; set; }
        public double PENDINGRECIVE { get; set; }
        public double ADVANCEAMT { get; set; }
        public double RESIVEDAMT { get; set; }
        public string CROPNAME { get; set; }
        public string CUSTOMERNAME { get; set; }
        public string VEHCHNO { get; set; }
        public string NARR { get; set; }
        public double BILLAMOUNT { get; set; }
        public double CBAL { get; set; }
    }
}
