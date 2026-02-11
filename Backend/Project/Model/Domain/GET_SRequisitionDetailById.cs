namespace Project.Model.Domain
{
    public class GET_SRequisitionDetailById
    {
        public string SRDAID { get; set; }
        public string SRAID { get; set; }
        public string SRDPRODUCT { get; set; }
        public string SRDDESCRIPTION { get; set; }
        public string SRDQUANTITY { get; set; }
        public string SRDUOM { get; set; }
        public int HSNCODE { get; set; }
        public Boolean ISDELETED { get; set; }
        public string UOMTITLE { get; set; }
        public string PRODUCTNAME { get; set; }
    }
}
