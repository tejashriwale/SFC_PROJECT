using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_MarketStoreRepository
    {
        Task<List<GET_MarketStore>>getasync(GET_MarketStoreDomain input);
        Task<List<GET_MarketStore>>getkeyword(GET_MarketStoreDomain input);
    }
}
