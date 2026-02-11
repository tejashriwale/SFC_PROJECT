namespace Project.Model.DTO
{
    public class GET_SBillDetailsByIdDto
    {
        public string SBDAID { get; set; }
        public string SBAID { get; set; }
        public string SBPRODUCT { get; set; }
        public string SBDUOM { get; set; }
        public string SBDQUANTITY { get; set; }
        public double SBDRATE { get; set; }
        public double SBDTAMT { get; set; }
        public double SBDIGST { get; set; }
        public double SBDSGST { get; set; }
        public double SBDCGST { get; set; }
        public double SBDTOTAL { get; set; }
        public int HSNCODE { get; set; }
        public string BATCHNO { get; set; }
        public string EXPIRYDATE { get; set; }
        public string UOMTITLE { get; set; }
        public string PRODUCTNAME { get; set; }
        public Boolean ISDELETED { get; set; }
    }
}
