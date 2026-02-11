namespace Project.Model.DTO
{
    public class GET_ColdStorageOutWardByIdDto
    {
        public string CSOWID { get; set; }
        public string LOTNO { get; set; }
        public string CNAME { get; set; }
        public string THIRDPARTYNAME { get; set; }
        public string ITEM { get; set; }
        public double WEIGHTINKG { get; set; }
        public string ENDDATE { get; set; }
        public int DAYINSLAB { get; set; }
        public double RATE { get; set; }
        public double OUTWARDWEIGHT { get; set; }
        public double REMAININGWEIGHT { get; set; }
        public double AMOUNT { get; set; }
        public double TOTALAMOUNT { get; set; }
        public string CUSTOMERNAME { get; set; }
        public string STARTDATE { get; set; }
        public string CSID { get; set; }
    }
}
