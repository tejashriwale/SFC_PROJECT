namespace Project.Model.Domain
{
    public class GET_ReceiptDetailsById
    {
        public string BDAID { get; set; }
        public string BAID { get; set; }
        public string CROPTYPE { get; set; }
        public string VYAPARINAME { get; set; }
        public int JALI { get; set; }
        public double WEIGHT { get; set; }
        public string WEIGHTMINMAX { get; set; }
        public double RATE { get; set; }
        public double AMOUNT { get; set; }
        public Boolean ISDELETED { get; set; }
      
    }
}
