using DocumentFormat.OpenXml.VariantTypes;

namespace Project.Model.DTO
{
    public class GET_VisitorMasterDto
    {
        public string VAID { get; set; }
        public string FARMERNAME { get; set; }
        public string VISITINGDATE { get; set; }
        public string PHONENUMBER { get; set; }
        public string LOCATION { get; set; }
        public string UAID { get; set; }
        public string COMPANYID { get; set; }
        public string DEPTID { get; set; }
        public string FNAME { get; set; }
        public string ISCOMPLITED { get; set; }
        public double ADVAMT { get; set; }
    }
}
