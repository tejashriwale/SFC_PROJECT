using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteNotificationRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeleteNotification input);
    }
}
