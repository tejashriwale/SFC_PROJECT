using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace Project.Model.DTO
{
    public class _GET_AccessRights_Dto
    {
        [Key]
        public string AccessRightsAutoID1 { get; set; }
        public string AccessRightID { get; set; }
        public string AccessRightTitle { get; set; }
        public string AccessRightDescription { get; set; }
        public string ApplicationGroup { get; set; }
        public string AccessRightGroup { get; set; }
        public Boolean IsAdminRight { get; set; }

    }
}
