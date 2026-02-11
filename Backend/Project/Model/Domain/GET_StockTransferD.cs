namespace Project.Model.Domain
{
    public class GET_StockTransferD
    {
        public string TDID		   { get; set; }
        public string STMID		   { get; set; }
        public string STDPRODUCT	   { get; set; }
        public string STDHSNCODE	   { get; set; }
        public int STDREMS	   { get; set; }
        public string STDUOM		   { get; set; }
        public int STDQTY		   { get; set; }
        public double STDRATE	   { get; set; }
        public double STDTOTAL	   { get; set; }
        public string STDBATCHNO	   { get; set; }
        public string STDBATACHDATE { get; set; }
        public string UOMTITLE      { get; set; }
        public string PRODUCTNAME   { get; set; }
        public Boolean ISDELETED { get; set; }
    }
}
