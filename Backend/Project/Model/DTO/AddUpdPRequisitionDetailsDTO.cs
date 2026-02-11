namespace Project.Model.DTO
{
    public class AddUpdPRequisitionDetailsDTO
    {
        public string PRDAID { get; set; }
        public string PRAID { get; set; }
        public string PRDPRODUCT { get; set; }
        public string PRDDESCRIPTION { get; set; }
        public string PRDQUANTITY { get; set; }
        public string PRDUOM { get; set; }
        public Boolean ISDELETED { get; set; }
        public int HSNCODE { get; set; }
        public string COMPANYID { get; set; }
        public string DEPTID { get; set; }
        public int PRBASEQTY { get; set; }
    }
}
