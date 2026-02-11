using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_FarmerTokenNotificationRepository
    {
        Task<List<GET_FarmerTokenNotification>> getasync(GET_FarmerTokenNotificationDomain input);
    }
}
