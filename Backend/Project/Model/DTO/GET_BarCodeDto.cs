namespace Project.Model.DTO
{
    public class GET_BarCodeDto
    {
        public string BAID { get; set; }
        public string BCATEGORY { get; set; }
        public string BPRODUCT { get; set; }
        public string BUOM { get; set; }
        public string BBATCHNO { get; set; }
        public double BPRICE { get; set; }
        public int HSNCODE { get; set; }
        public string BQUANTITY { get; set; }
        public string BARCODE { get; set; }
        public Boolean BSHOWBATCHNO { get; set; }
        public Boolean BSHOWPRODUCTNAME { get; set; }
        public Boolean BSHOWPRICE { get; set; }
    }
}
