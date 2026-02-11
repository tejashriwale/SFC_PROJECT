namespace Project.Model.Domain
{
    public class GET_Report_Farmerbill
    {
        public string TOKENNO { get; set; }
        public string BILLNO { get; set; }
        public string FULLNAME  	 { get; set; }
        public double BILLAMOUNT  { get; set; }
        public double CASHAMOUNT	 { get; set; }
        public double CHEQUEAMOUNT { get; set; }
        public double ONLINEAMOUNT { get; set; }
    }
}
