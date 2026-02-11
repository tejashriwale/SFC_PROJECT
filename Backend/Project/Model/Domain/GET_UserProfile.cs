namespace Project.Model.Domain
{
    public class GET_UserProfile
    {
        internal string companyID;
        internal string departmentID;

        public string Username { get; set; }
        public string EMAIL { get; set; }
        public string Password { get; set; }
        public string AccessPolicy { get; set; }
    }
}
