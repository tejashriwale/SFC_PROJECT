namespace Project.Model.DTO
{
    public class GET_POrderDetailsDataDto
    {
        public string PODAID { get; set; }
        public string PODPRODUCT { get; set; }
        public string PODUOM { get; set; }
        public string PODQUANTITY { get; set; }
        public double PODRATE { get; set; }
        public double PODTAMT { get; set; }
        public double PODIGST { get; set; }
        public double PODSGST { get; set; }
        public double PODCGST { get; set; }
        public string POAID { get; set; }
        public double PODTOTAL { get; set; }
        public int HSNCODE { get; set; }
        public Boolean ISDELETED { get; set; }
        public string UOMTITLE { get; set; }
        public string PRODUCTNAME { get; set; }
    }
}
