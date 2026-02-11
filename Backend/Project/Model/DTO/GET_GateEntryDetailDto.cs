namespace Project.Model.DTO
{
    public class GET_GateEntryDetailDto
    {
        public string DPKID { get; set; }
        public string MAID { get; set; }
        public string VEHNO { get; set; }
        public string TOKNNO { get; set; }
        public string DATE { get; set; }
        public string AADHARNO { get; set; }
        public string FULLNAME { get; set; }
        public string MOBILENO { get; set; }
        public string VILLAGE { get; set; }
        public string CROP_TYPE { get; set; }
        public string CARETS_COUNT { get; set; }
        public string UID { get; set; }
        public DateTime DATEO { get; set; }
      
        public Boolean ISCOMPLETED { get; set; }
        public Boolean ISVERIFIED { get; set; }
    }
}
