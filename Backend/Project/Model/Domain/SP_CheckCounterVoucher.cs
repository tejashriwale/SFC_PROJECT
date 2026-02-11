namespace Project.Model.Domain
{
    public class SP_CheckCounterVoucher
    {
        public string UAID	 { get; set; }
        public string OrganizationID { get; set; }
        public string DivisionID { get; set; }
        public string VoucherDate { get; set; }
        public string FARMERACC	 { get; set; }
        public string CHECKACC { get; set; }
        public double VoucherAmount	 { get; set; }
        public string ReferenceKey { get; set; }
        public string VREFF { get; set; }
    }
}
