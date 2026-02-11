using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdGateEntryDetailAppRepository
    {
        Task<List<ApiResponseMessage>> getasync(List<SP_AddUpdGateEntryDetailApp> input);
    }
}
