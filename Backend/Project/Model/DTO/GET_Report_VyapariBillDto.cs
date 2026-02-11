namespace Project.Model.DTO
{
    public class GET_Report_VyapariBillDto
    {
        public string VYAPARINAME { get; set; }
        public string VNAME { get; set; }
        public string PHONE { get; set; }
        public int BILLNO { get; set; }
        public string DATE { get; set; }
        public double TOTALAMOUNT { get; set; }
        public double TOTALWEIGHT { get; set; }
        public double REMAININGAMOUNT { get; set; }
        public double RATE { get; set; }
        public double WEIGHT { get; set; }
        public Boolean LIMITS { get; set; }
    }
}
