namespace Project.Model.DTO
{
    public class GET_PChallanDetailsByIdDto
    {
        public string PCDAID { get; set; }
        public string PCAID { get; set; }
        public string PCDPRODUCT { get; set; }
        public string PCDUOM { get; set; }
        public string PCDQUANTITY { get; set; }
        public double PCDRATE { get; set; }
        public double PCDTAMT { get; set; }
        public string PCDTAXABLE { get; set; }
        public double PCDIGST { get; set; }
        public double PCDSGST { get; set; }
        public double PCDCGST { get; set; }
        public int HSNCODE { get; set; }
        public string BATCHNO { get; set; }
        public string EXPIRYDATE { get; set; }
        public string UOMTITLE { get; set; }
        public string PRODUCTNAME { get; set; }
        public Boolean ISDELETED { get; set; }
    }
}
