namespace Project.Model.DTO
{
    public class GET_SQuatationForProceed
    {
        public string SQNO	 { get; set; }
        public string SQCAID { get; set; }
        public string SQTRANSPORTID { get; set; }
        public string SQAMAID { get; set; }
        public string SQAMDAID { get; set; }
        public string SQPRODUCT { get; set; }
        public string SQUOM { get; set; }
        public string QQUANTITY { get; set; }
        public double QRATE		 { get; set; }
        public double QTAMT	 { get; set; }
        public double QCGST { get; set; }
        public double QSGST		 { get; set; }
        public double QIGST	 { get; set; }
     
        public string UOMTITLE { get; set; }
        public string VENDORNAME { get; set; }
        public string PRODUCTNAME { get; set; }
        public int HSNCODE { get; set; }
    }
}
