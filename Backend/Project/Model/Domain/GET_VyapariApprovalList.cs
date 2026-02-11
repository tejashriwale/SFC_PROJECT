namespace Project.Model.Domain
{
    public class GET_VyapariApprovalList
    {
        public string VYAPARI { get; set; }
        public string APKID { get; set; }
        public string MAID	 { get; set; }
        public int JAALI	 { get; set; }
        public string WEIGHT			 { get; set; }
        public double WEIGHTLESS		 { get; set; }
        public Boolean WEIGHTMAXMIN	 { get; set; }
        public string TOKEN			 { get; set; }
        public double RATE			 { get; set; }
        public string VILLAGE { get; set; }
        public string DPKID { get; set; }
        public string FNAME { get; set; }
        public string CROP_TYPE { get; set; }
        public string CROPLABEL { get; set; }
    
        public Boolean ISAPPORVED { get; set; }
        public Boolean ISVYAPARIVERIFIED { get; set; }
    }
}
