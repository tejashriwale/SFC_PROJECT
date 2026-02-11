namespace Project.Model.DTO
{
    public class GET_VoucherDto
    {
        public int VoucherAID { get; set; }
        public string OrganizationID { get; set; }
        public string DivisionID { get; set; }
        public string VoucherTID { get; set; }
        public string SerialNumber { get; set; }
        public string VoucherDate { get; set; }
        public string DRACC { get; set; }
        public string CRACC { get; set; }
        public double VoucherAmount { get; set; }
        public string ReferenceTID { get; set; }
        public string ReferenceKey { get; set; }
        public string Narration { get; set; }
        public string GRPKEY { get; set; }
        public string BATCHKEY { get; set; }
        public string OID { get; set; }
        public string VOUCHERTYPE { get; set; }
        public string JAMANAVE { get; set; }
        public string FNAME { get; set; }
        public string JAMANAVENAME { get; set; }
        public string DRACCENAME { get; set; }
        public string VoucherNumber { get; set; }
       
        public Boolean STATUS { get; set; }
    }
}
