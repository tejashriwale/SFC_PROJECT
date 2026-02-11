using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Project.Model.Domain
{
    public class _GET_UserProfile_
    {
        [Key]
        public string UserAutoID { get; set; }
        public string Username { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public string Registrationdate { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string AccessPolicy { get; set; }
    }
}
