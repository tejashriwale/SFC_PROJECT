namespace Project.Model.DTO
{
    public class SP_AddUpdVyapariBillMasterByBillDto
    {
        public string VBKID { get; set; }
        public int BILLNO { get; set; }
        public string DATE { get; set; }
        public int TOTALJAALI { get; set; }
        public double TOTALWEIGHT { get; set; }
        public double TOTALAMOUNT { get; set; }
        public string SEVA { get; set; }
        public double TOTALLOSS { get; set; }
        public double REMAININGAMOUNT { get; set; }
        public string COMPANYID { get; set; }
        public string DEPTID { get; set; }
        public string VYAPARINAME { get; set; }
        public string APKID { get; set; }
        public string UAID { get; set; }
        public Boolean ISDELETED { get; set; }

    }
}
