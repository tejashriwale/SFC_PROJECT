namespace Project.Model.Domain
{
    public class GET_GoodReceiptNoteDataById
    {
        public string GRNAID { get; set; }
        public string GRNTRANSACTIONID { get; set; }
        public string GRNDATE { get; set; }
        public string GRNNUMBER	 { get; set; }
        public string GRNCHALLANNO { get; set; }
        public string GRNCHALLANDATE { get; set; }
        public string GRNVECHILENO		 { get; set; }
        public string GRNVENDOR			 { get; set; }
        public string GRNCONSIGNER		 { get; set; }
        public string GRNINSPECTEDBY		 { get; set; }
        public string GRNNARRATION		 { get; set; }
        public string PCREFF	 { get; set; }
        public string GRNDAID				 { get; set; }
        public string GRNDPRODUCT			 { get; set; }
        public string GRNDEPARTMENT		 { get; set; }
        public string GRNDUOM				 { get; set; }
        public double GRNDCHALLANQUANTITY	 { get; set; }
        public double GRNDSHORTQUANTITY	 { get; set; }
        public double GRNDEXCESSQUANTITY	 { get; set; }
        public double GRNDREJECTEDQUANTITY { get; set; }
        public double GRNDACCEPTEDQUANTITY { get; set; }
        public double GRNDRATE			 { get; set; }
        public double GRNDDISCOUNT		 { get; set; }
        public double GRNDTOTALVALUE		 { get; set; }
        public int HSNCODE { get; set; }
        public string UOMTITLE { get; set; }
        public string VENDORNAME { get; set; }
        public string CONTACTNUMBER { get; set; }
        public string EMAIL { get; set; }
        public string PRODUCTNAME { get; set; }
        public string BATCHNO { get; set; }
        public string EXPIRYDATE { get; set; }

        public string GAAID { get; set; }
        public string QCID { get; set; }
        public Boolean GACONDITION { get; set; }
        public Boolean GAAMT { get; set; }
        public Boolean GAEXPECTED { get; set; }
        public Boolean GAPACKAGE { get; set; }
        public string GACONDITIONDESCRIPTION { get; set; }
        public string GAAMTDESCRIPTION { get; set; }
        public string GAEXPECTEDDESCRIPTION { get; set; }
        public string GAPACKAGEDESCRIPTION { get; set; }

    }
}
