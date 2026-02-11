namespace Project.Model.Domain
{
    public class AddGoodReceiptNoteDetails
    {
        public string GRNDAID { get; set; }
        public string GRNAID { get; set; }
        public string GRNDPRODUCT { get; set; }
        public string GRNDEPARTMENT { get; set; }
        public string GRNDUOM { get; set; }
        public double GRNDCHALLANQUANTITY { get; set; }
        public double GRNDSHORTQUANTITY { get; set; }
        public double GRNDEXCESSQUANTITY { get; set; }
        public double GRNDREJECTEDQUANTITY { get; set; }
        public double GRNDACCEPTEDQUANTITY { get; set; }
        public double GRNDRATE { get; set; }
        public double GRNDDISCOUNT { get; set; }
        public double GRNDTOTALVALUE { get; set; }
        public int HSNCODE { get; set; }
        public string COMPANYID { get; set; }
        public string DEPTID { get; set; }
        public string BATCHNO { get; set; }
        public string EXPIRYDATE { get; set; }
    }
}
