namespace Project.Model.Domain
{
    public class AddGoodReceiptNoteAssurance
    {
        public string GAAID { get; set; }
        public  string QCID { get; set; }
        public  string GRNAID { get; set; }
        public  Boolean GACONDITION { get; set; }
        public Boolean GAAMT { get; set; }
        public Boolean GAEXPECTED { get; set; }
        public Boolean GAPACKAGE { get; set; }
        public  string GACONDITIONDESCRIPTION { get; set; }
        public  string GAAMTDESCRIPTION { get; set; }
        public  string GAEXPECTEDDESCRIPTION { get; set; }
        public  string GAPACKAGEDESCRIPTION { get; set; }
        public  string COMPANYID { get; set; }
        public string DEPTID { get; set; }
    }
}
