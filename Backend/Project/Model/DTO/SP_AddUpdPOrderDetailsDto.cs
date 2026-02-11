namespace Project.Model.DTO
{
    public class SP_AddUpdPOrderDetailsDto
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
        public double PODTOTAL { get; set; }
        public string POAID { get; set; }
        public int HSNCODE { get; set; }
        public string COMPANYID { get; set; }
        public string DEPTID { get; set; }
    }
}
