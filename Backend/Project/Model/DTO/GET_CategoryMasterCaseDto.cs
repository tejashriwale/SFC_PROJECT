namespace Project.Model.DTO
{
    public class GET_CategoryMasterCaseDto
    {
        public string CTID { get; set; }
        public string CTNAME { get; set; }
        public string CTTYPE { get; set; }
        public string CTSUBCATGORYID { get; set; }
        public string COMPANYID { get; set; }
        public string DEPTID { get; set; }
        public string CATEGORY { get; set; }
        public string SUBCATEGORY { get; set; }
        public Boolean CSTATUS { get; set; }
        public Boolean ISDELETED { get; set; }
    }
}
