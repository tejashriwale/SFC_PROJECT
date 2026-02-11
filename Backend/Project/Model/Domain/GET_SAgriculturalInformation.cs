namespace Project.Model.Domain
{
    public class GET_SAgriculturalInformation
    {
        public string DSAID { get; set; }
        public string SRAID	 { get; set; }
        public string SURVEYNUMBER	 { get; set; }
        public string CLUSTERNUMBER	 { get; set; }
        public string LANDAREA	 { get; set; }
        public string LANDTYPE { get; set; }
        public string LANDNAME { get; set; }
        public Boolean ISDELETED { get; set; }
    }
}
