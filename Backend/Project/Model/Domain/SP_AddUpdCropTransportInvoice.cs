namespace Project.Model.Domain
{
    public class SP_AddUpdCropTransportInvoice
    {
        
        public string CROP     { get; set; }
        public string TREADER  { get; set; }
        public string VEHCHNO  { get; set; }
        public int CRTCNT   { get; set; }
        public decimal TOTSAMT  { get; set; }
        public string CTAID    { get; set; }
        public decimal TOTEX    { get; set; }
        public decimal TOTIWDAMT{ get; set; }
        public string COMPANYID{ get; set; }
        public string DEPTID   { get; set; }
        public string UAID { get; set; }
    }
}
