namespace Project.Model.Domain
{
    public class GET_QuatationDetailData
    {
        public string QAMDAID { get; set; }
        public string QAMAID	 { get; set; }
        public string QPRODUCT { get; set; }
        public string QQUANTITY { get; set; }
        public string QUOM { get; set; }
        public double QRATE { get; set; }
        public double QTAMT { get; set; }
        public double QIGST { get; set; }
        public double QSGST { get; set; }
        public double QCGST { get; set; }
        public double QCESS { get; set; }
        public string UOMTITLE { get; set; }
        public string PRODUCTNAME { get; set; }
        public Boolean ISDELETED { get; set; }
        public int HSNCODE { get; set; }

    }
}
