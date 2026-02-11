namespace Project.Model.DTO
{
    public class SP_AddUpdPDeficitDetailsDto
    {
        public string PKID { get; set; }
        public string DSID { get; set; }
        public string PRODUCTNAME { get; set; }
        public string UNIT { get; set; }
        public string REMAININGSTOCK { get; set; }
        public string BATCHNO { get; set; }
        public string BATCHDATE { get; set; }
        public int QUANTITY { get; set; }
        public double PRICE { get; set; }
        public double TOTAL { get; set; }
        public string COMPANYID { get; set; }
        public string DEPTID { get; set; }
        public Boolean ISDELETED { get; set; }
    }
}
