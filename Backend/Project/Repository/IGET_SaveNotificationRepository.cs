using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_SaveNotificationRepository
    {
        Task<List<GET_SaveNotification>> getasync(GET_SaveNotificationDomain input);
    }
}
