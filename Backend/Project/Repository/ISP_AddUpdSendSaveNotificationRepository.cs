using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdSendSaveNotificationRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_AddUpdSendSaveNotification input);
    }
}
