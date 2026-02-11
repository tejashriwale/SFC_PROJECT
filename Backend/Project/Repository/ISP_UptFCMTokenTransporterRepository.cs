using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_UptFCMTokenTransporterRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_UptFCMTokenTransporter input);
    }
}
