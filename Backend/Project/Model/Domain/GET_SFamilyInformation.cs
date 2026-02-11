namespace Project.Model.Domain
{
    public class GET_SFamilyInformation
    {
        public string DSFAID { get; set; }
        public string SRAID	 { get; set; }
        public string NAME	 { get; set; }
        public string RELATION	 { get; set; }
        public int AGE	 { get; set; }
        public string EDUCATION	 { get; set; }
        public string BUSINESS	 { get; set; }
        public string BUSINESSNAME	 { get; set; }
        public string RELATIONNAME { get; set; }
        public Boolean ISDELETED { get; set; }
    }
}
