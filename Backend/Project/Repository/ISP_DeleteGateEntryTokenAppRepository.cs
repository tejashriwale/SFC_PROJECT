using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteGateEntryTokenAppRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeleteGateEntryTokenApp input);
    }
}
