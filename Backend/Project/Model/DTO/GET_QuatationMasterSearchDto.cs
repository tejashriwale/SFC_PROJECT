namespace Project.Model.DTO
{
    public class GET_QuatationMasterSearchDto
    {
        public string QAMAID { get; set; }
        public string QVAID { get; set; }
        public string QNO { get; set; }
        public string QDATE { get; set; }
        public string QAMDAID { get; set; }
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
        public string PNAME { get; set; }
        public string QTRANSPORTTERMS { get; set; }
        public string QPAYMENTTERMS { get; set; }
        public string QPDUEDATE { get; set; }
        public int QNODAYS { get; set; }
        public int HSNCODE { get; set; }
    }
}
