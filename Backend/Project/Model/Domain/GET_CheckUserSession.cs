namespace Project.Model.Domain
{
    public class GET_CheckUserSession
    {
        public string UAID  { get; set; }
        public string UEMAILADDRESS { get; set; }
        public string UPASSWORD { get; set; }
        public Boolean ISLOGIN { get; set; }
    }
}
