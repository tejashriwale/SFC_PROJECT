using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdNotificationRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_AddUpdNotification input);
    }
}
