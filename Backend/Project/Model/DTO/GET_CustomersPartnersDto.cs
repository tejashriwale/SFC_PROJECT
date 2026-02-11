using System.ComponentModel.DataAnnotations;

namespace Project.Model.DTO
{
    public class GET_CustomersPartnersDto
    {
        [Key]
        public string CAID { get; set; }
        public string CCOMPANYNAME { get; set; }
        public string CTYPE { get; set; }
        public string CADDRESS { get; set; }
        public string CCITY { get; set; }
        public string CPINCODE { get; set; }
        public string CTELEPHONENUMBER { get; set; }
        public string CEMAIL { get; set; }
        public string CWEBSITEURL { get; set; }
        public string CCONTACTPERSONNAME { get; set; }
        public string CCONTACTPERSONMOBILE { get; set; }
        public string CDESIGNATION { get; set; }
        public string CGSTIN { get; set; }
        public string CTRADENAME { get; set; }
        public string CTRADEADDRESS { get; set; }
        public string CTRADECITY { get; set; }
        public string CTRADEPINCODE { get; set; }
        public string CSTATE { get; set; }
        public string CDISTANCE { get; set; }
        public string CPAN { get; set; }
        public string CBUSINESSTYPE { get; set; }
        public string CSTATUS { get; set; }
        public string CIFSC { get; set; }
        public string CACCOUNTNAME { get; set; }
        public string CACCOUNTNUMBER { get; set; }
        public string CBANKNAME { get; set; }
        public string CBRANCHNAME { get; set; }
        public string CAREA { get; set; }
        public string CLANDMARK { get; set; }
        public string CCATEGORY { get; set; }
        public string CTRADELANDMARK { get; set; }
        public string CTRADEAREA { get; set; }
        public string SCITY { get; set; }
        public string SSTATE { get; set; }
        public string BUSINESSTYPE { get; set; }
    }
}
