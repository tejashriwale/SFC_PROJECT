namespace Project.Model.Domain
{
    public class GET_SOrderDetails
    {
        public string SOAID { get; set; }
        public string SODAID { get; set; }
        public string SODPRODUCT { get; set; }
        public string SODUOM	{ get; set; }
        public string SODQUANTITY	{ get; set; }
        public double SODRATE	{ get; set; }
        public double SODTAMT	{ get; set; }
        public double SODIGST	{ get; set; }
        public double SODSGST { get; set; }
        public double SODCGST { get; set; }
        public double SODTOTAL { get; set; }
        public double SODCESS { get; set; }
        public int HSNCODE { get; set; }

    }
}
