using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteGateEntryRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeleteGateEntry input);
    }
}
