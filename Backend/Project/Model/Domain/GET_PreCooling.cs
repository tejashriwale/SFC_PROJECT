namespace Project.Model.Domain
{
    public class GET_PreCooling
    {
        public string PCID	 { get; set; }
        public string DATE	 { get; set; }
       
        public double RATE { get; set; }
        public string STATUS { get; set; }
        public string DAYSFROM { get; set; }
        public string DAYSTO { get; set; }
    }
}
