namespace Project.Model.DTO
{
    public class GET_FarmerForNotificationsDto
    {
        public string FAID { get; set; }
        public int LOGTYPE { get; set; }
        public string FNAME { get; set; }
        public string FCMTOKEN { get; set; }
        public string DATE { get; set; }
    }
}
