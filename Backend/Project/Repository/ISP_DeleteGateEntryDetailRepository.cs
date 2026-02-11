using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteGateEntryDetailRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeleteGateEntryDetail input);
    }
}
