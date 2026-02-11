namespace Project.Model.DTO
{
    public class SP_AddUpdAuctionshedMasterDto
    {
        public string APKID { get; set; }
        public string TOKEN { get; set; }
        public int BILLNO { get; set; }
        public string DATE { get; set; }
        public string MAID { get; set; }

        public string VILLAGE { get; set; }
        public string SEVA { get; set; }

        public int TOTALJALI { get; set; }
        public double TOTALAMOUNT { get; set; }
        public double REMAININGAMOUNT { get; set; }
        public double TOTALWEIGHT { get; set; }
        public string COMPANYID { get; set; }
        public string DEPTID { get; set; }
    }
}
