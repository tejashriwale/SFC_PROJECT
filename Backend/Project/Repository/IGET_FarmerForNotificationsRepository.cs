using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_FarmerForNotificationsRepository
    {
        Task<List<GET_FarmerForNotifications>> getasync(GET_FarmerForNotificationsDomain input);
    }
}
