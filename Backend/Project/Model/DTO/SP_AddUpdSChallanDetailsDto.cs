namespace Project.Model.DTO
{
    public class SP_AddUpdSChallanDetailsDto
    {
        public string SCDAID { get; set; }
        public string SCAID { get; set; }
        public string SCDPRODUCT { get; set; }
        public string SCDUOM { get; set; }
        public string SCDQUANTITY { get; set; }
        public double SCDRATE { get; set; }
        public double SCDTAMT { get; set; }
        public string SCDTAXABLE { get; set; }
        public double SCDIGST { get; set; }
        public double SCDSGST { get; set; }
        public double SCDCGST { get; set; }
        public int HSNCODE { get; set; }
        public string COMPANYID { get; set; }
        public string DEPTID { get; set; }
        public string BATCHNO { get; set; }
        public string EXPIRYDATE { get; set; }
        public Boolean ISDELETED { get; set; }
    }
}
