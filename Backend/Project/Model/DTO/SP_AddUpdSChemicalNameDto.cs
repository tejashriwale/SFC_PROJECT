namespace Project.Model.DTO
{
    public class SP_AddUpdSChemicalNameDto
    {
        public string DSCID { get; set; }
        public string SRAID { get; set; }
        public string CNAME { get; set; }
        public string CTYPE { get; set; }
        public double QUANTITY { get; set; }
        public double COAST { get; set; }
        public string COMPANYID { get; set; }
        public string DEPTID { get; set; }
        public Boolean ISDELETED { get; set; }
    }
}
