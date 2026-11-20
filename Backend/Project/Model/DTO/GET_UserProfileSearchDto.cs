namespace Project.Model.DTO
{
    public class GET_UserProfileSearchDto
    {
        public string Username { get; set; }
        public string EMAIL { get; set; }
        public string Password { get; set; }
        public string AccessPolicy { get; set; }
        public string companyID { get; set; }
        public string departmentID { get; set; }
        public string UAID { get; set; }
        public string DEPARTMENTNAME { get; set; }
        public string COMPANYNAME { get; set; }
        public string COMPANYSTATE { get; set; }
        public string REGISTRATIONDATE { get; set; }
        public string Message { get; set; }
        public string UROLEID { get; set; }
        public string LANGUAGE { get; set; }
        public int Status { get; set; }
        public string? Token { get; set; }

    }
}
