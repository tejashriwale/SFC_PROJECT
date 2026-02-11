using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteMarketStoreRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeleteMarketStore input);
    }
}
