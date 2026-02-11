using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_AuctionDataSearchRepository
    {
        Task<List<GET_AuctionDataSearch>>getasync(GET_AuctionDataSearchDomain input);
        Task<List<GET_AuctionDataSearch>>getkeyword(GET_AuctionDataSearchDomain input);
    }
}
