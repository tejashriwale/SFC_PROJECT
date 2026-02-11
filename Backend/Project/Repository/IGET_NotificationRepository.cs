using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_NotificationRepository
    {
        Task<List<GET_Notification>> getasync(GET_NotificationDomain input);
        Task<List<GET_Notification>> getkeyword(GET_NotificationDomain input);
    }
}
