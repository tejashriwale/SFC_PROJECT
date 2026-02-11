namespace Project.Model.DTO
{
    public class SP_AddUpdColdStorageOutwardDto
    {
        public string CSOWID { get; set; }
        public string CSID { get; set; }
        public string STARTDATE { get; set; }
        public string ENDDATE { get; set; }
        public int DAYFROM { get; set; }
        public int DAYTO { get; set; }
        public double RATE { get; set; }
        public double WEIGHTINKG { get; set; }
        public int DAYINSLAB { get; set; }
        public double OUTWARDWEIGHT { get; set; }
        public double REMAININGWEIGHT { get; set; }
        public double AMOUNT { get; set; }
        public double TOTALAMOUNT { get; set; }
        public Boolean STATUS { get; set; }
        public string COMPANYID { get; set; }
        public string DEPTID { get; set; }
        public string UAID { get; set; }
        public string TRANSACTIONMODE { get; set; }
        public string NAVEKHATE { get; set; }
        public string CHEQUEDATE { get; set; }
        public string CHEQUENO { get; set; }
        public string NARRATION { get; set; }
    }
}
