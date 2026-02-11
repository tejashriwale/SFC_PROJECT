namespace Project.Model.Domain
{
    public class GET_SChemicalName
    {
        public string DSCID { get; set; }
        public string SRAID	 { get; set; }
        public string CNAME	 { get; set; }
        public string CTYPE	 { get; set; }
        public string CHEMICALNAME { get; set; }
        public double QUANTITY	 { get; set; }
        public double COAST	 { get; set; }
        public Boolean ISDELETED { get; set; }
     
    }
}
