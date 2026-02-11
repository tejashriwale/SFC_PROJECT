namespace Project.Model.DTO
{
    public class GET_SQuotationDetailByIdDto
    {
        public string SQAMAID { get; set; }
        public string SQAMDAID { get; set; }
        public string SQPRODUCT { get; set; }
        public string SQUOM { get; set; }
        public string SQQUANTITY { get; set; }
        public double SQRATE { get; set; }
        public double SQTAMT { get; set; }
        public double SQIGST { get; set; }
        public double SQSGST { get; set; }
        public double SQCGST { get; set; }
        public int HSNCODE { get; set; }
        public string UOMTITLE { get; set; }
        public string PRODUCTNAME { get; set; }
        public Boolean ISDELETED { get; set; }
        public double SQTAXBALEAMOUNT { get; set; }
    }
}
