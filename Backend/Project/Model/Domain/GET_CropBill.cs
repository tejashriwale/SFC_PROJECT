namespace Project.Model.Domain
{
    public class GET_CropBill
    {
        public string CROPID  	 { get; set; }
        public string CBKID { get; set; }
        public DateTime VISITINGDATE { get; set; }
        public string NARR { get; set; }
        public string PHONENUMBER { get; set; }
        public string CTYPE { get; set; }
        public string CVARIETY { get; set; }
        public string CAREA	 { get; set; }
        public string FARMERNAME { get; set; }
        public string CROPNAME { get; set; }
        public string FAID { get; set; }
        public string CDAID { get; set; }
        public string VDAID { get; set; }
        public double RATE { get; set; }
        public double WEIGHT	 { get; set; }
        public double CARET { get; set; }
        public double AMOUNT { get; set; }
        public double ADVANCE { get; set; }
        public string BILLSTATUS { get; set; }
    }
}
