namespace Project.Model.Domain
{
    public class GET_TopFarmerOnCrates
    {
        public string DPKID	 { get; set; }
        public string MAID { get; set; }
        public string FULLNAME { get; set; }
        public string CROP_TYPE { get; set; }
        public string CARETS_COUNT { get; set; }
        public string UID { get; set; }
        public Boolean ISCOMPLETED { get; set; }
    }
}
