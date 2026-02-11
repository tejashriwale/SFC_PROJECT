using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdGateEntryDetailRepository
    {
        Task<List<ApiResponseMessage3>> getasync(SP_AddUpdGateEntryDetail input);
    }
}
