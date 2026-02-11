namespace Project.Model.DTO
{
    public class GET_CountPrecoolingDto
    {
        public string PCID { get; set; }
        public string DATE { get; set; }
        public string DAYSFROM { get; set; }
        public string DAYSTO { get; set; }
        public double RATE { get; set; }
        public string STATUS { get; set; }
    }
}
