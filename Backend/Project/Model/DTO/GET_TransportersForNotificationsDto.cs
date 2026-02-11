namespace Project.Model.DTO
{
    public class GET_TransportersForNotificationsDto
    {
        public string NotiID { get; set; }
        public string TAID { get; set; }
        public string TNAME { get; set; }
        public string FCMTOKEN { get; set; }
        public int LOGTYPE { get; set; }
        public string TDATE { get; set; }
    }
}
