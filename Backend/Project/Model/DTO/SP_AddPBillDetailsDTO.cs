namespace Project.Model.DTO
{
    public class SP_AddPBillDetailsDTO
    {
        public string PBDAID { get; set; }
        public string PBAID { get; set; }
        public string PBPRODUCT { get; set; }
        public string PBDUOM { get; set; }
        public string PBDQUANTITY { get; set; }
        public double PBDRATE { get; set; }
        public double PBDTAMT { get; set; }
        public double PBDIGST { get; set; }
        public double PBDSGST { get; set; }
        public double PBDCGST { get; set; }
        public int HSNCODE { get; set; }
        public string COMPANYID { get; set; }
        public string DEPTID { get; set; }
        public string BATCHNO { get; set; }
        public DateTime EXPIRYDATE { get; set; }
    }
}
