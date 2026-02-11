using System.ComponentModel.DataAnnotations;

namespace Project.Model.Domain
{
    public class UserMasterDto
    {
        [Key]
        public string UAID { get; set; }
        public string UCOMPANYID { get; set; }
        public string UBRANCHID { get; set; }
        public string UPACSID { get; set; }
        //public string UDEPARTMENTID { get; set; }
        public string USTOREID { get; set; }
        public string UWAREHOUSEID { get; set; }
        public string UROLEID { get; set; }
        //public string UTAHASILID { get; set; }
        //public string UTYPEID { get; set; }
        public string UTITLE { get; set; }
        public string UFORENAME { get; set; }
        public string UDESCRIPTION { get; set; }
        public string UEMAILADDRESS { get; set; }
        public string UMOBILENUMBER { get; set; }
        public string UPASSWORD { get; set; }
        public string UGROUP { get; set; }
        public string UACCESSPOLICY { get; set; }
        public string UACCESSDAYS { get; set; }
        public string UACCESSTIMESTART { get; set; }
        public string UACCESSTIMEEND { get; set; }
        public string UREGISTRATIONDATE { get; set; }
        public string UEXPIRYDATE { get; set; }
        public string UREFERENCEKEY { get; set; }
        public string ULOGINKEY { get; set; }
        public string ULASTLOGINDATETIME { get; set; }
        public string ULOGINCOUNT { get; set; }
        public string USTATUSID { get; set; }
        public string UGOOGLECLIENTID { get; set; }
        public string UFACEBOOKCLIENTID { get; set; }
        public string UAPPLICATIONKEY { get; set; }
        public string UAPPLICATIONDATE { get; set; }
        public string UAPPLICATIONHOME { get; set; }
        public string ULANGUAGEID { get; set; }
    }
}
