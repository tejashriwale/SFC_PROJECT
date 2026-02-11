namespace Project.Model.DTO
{
    public class GET_PurchaseOrderDataByIdDto
    {
        public string POAID { get; set; }
        public string POTRANNO { get; set; }
        public string POPLACE { get; set; }
        public string PODAID { get; set; }
        public string PODPRODUCT { get; set; }
        public string PODUOM { get; set; }
        public double PODRATE { get; set; }
        public double PODQUANTITY { get; set; }
        public double PODTAMT { get; set; }
        public double PODIGST { get; set; }
        public double PODSGST { get; set; }
        public double PODCGST { get; set; }
        public double PODTOTAL { get; set; }
        public string UOMTITLE { get; set; }
        public string PNAME { get; set; }
        public int HSNCODE { get; set; }
    }
}
