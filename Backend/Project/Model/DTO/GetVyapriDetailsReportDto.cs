namespace Project.Model.DTO
{
    public class GetVyapriDetailsReportDto
    {
        public int BILLNO { get; set; }
        public string VYAPARINAME { get; set; }
        public string DATE { get; set; }
        public string VBKID { get; set; }
        public double TOTALAMOUNT { get; set; }
        public double TOTALWEIGHT { get; set; }
        public double REMAININGAMOUNT { get; set; }
        public double TOTALLOSS { get; set; }
        public int TOTALJAALI { get; set; }
    }
}
