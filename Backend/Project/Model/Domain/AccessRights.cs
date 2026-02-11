using System.ComponentModel.DataAnnotations;
using Project.Model.Domain;

namespace Project.Model.Domain
{
    public class AccessRights
    {
        [Key]
        public string AccessRightsAutoID1 { get; set; }
        public string? AccessRightID { get; set; }
        public string? AccessRightTitle { get; set; }
        public string? AccessRightDescription { get; set; }
        public string? ApplicationGroup { get; set; }
        public string? AccessRightGroup { get; set; }
        public string? COMPANYID { get; set; }
        public int IsAdminRight { get; set; }
    }
}

	
