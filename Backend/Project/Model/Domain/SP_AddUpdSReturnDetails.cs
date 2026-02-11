namespace Project.Model.Domain
{
    public class SP_AddUpdSReturnDetails
    {
        public string SRDAID		 { get; set; }
        public string SRAID		 { get; set; }
        public string SRDPRODUCT { get; set; }
        public string SRDUOM	 { get; set; }
        public string SRDQUANTITY { get; set; }
        public double SRTDAMT { get; set; }
        public double SRDIGST { get; set; }
        public double SRDSGST { get; set; }
        public double SRDCGST { get; set; }
        public string SRDQUANTITYRETURN	 { get; set; }
        public int HSNCODE { get; set; }
        public string COMPANYID			 { get; set; }
        public string DEPTID { get; set; }
        public string BATCHNO { get; set; }
        public string EXPIRYDATE { get; set; }
    }
}
