using Project.Model;
using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_TransportersForNotificationsRepository
    {
        Task<List<GET_TransportersForNotifications>> getasync(GET_TransportersForNotificationsDomain input);
    }
}
