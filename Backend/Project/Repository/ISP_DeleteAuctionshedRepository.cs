using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteAuctionshedRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeleteAuctionshed input);
    }
}
