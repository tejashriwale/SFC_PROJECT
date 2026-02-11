using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_AuctionDataByTokenRepository
    {
        Task<List<GET_AuctionDataByToken>> getasync(GET_AuctionDataByTokenDomain input);
    }
}
