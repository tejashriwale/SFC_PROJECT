using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdMarketStoreRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_AddUpdMarketStore input);
    }
}
