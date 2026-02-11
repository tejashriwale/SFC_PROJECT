namespace Project.Model.DTO
{
    public class GET_AUCTIONSHED
    {
        public string APKID { get; set; }
        public string TOKEN			 { get; set; }
        public int BILLNO		 { get; set; }
        public string DATE			 { get; set; }
        public DateTime ODATE { get; set; }
        public string MAID { get; set; }
        public string FNAME { get; set; }
        public string FARMERNAME	 { get; set; }
        public string VILLAGE			 { get; set; }
        public string SEVA	 { get; set; }
        public string HAMALI	 { get; set; }
        public string TOLAI		 { get; set; }
        public int TOTALJALI		 { get; set; }
        public double TOTALAMOUNT	 { get; set; }
        public double REMAININGAMOUNT { get; set; }
        public Boolean ISCOMPLETED { get; set; }
        public string FMOBILE { get; set; }
    }
}
