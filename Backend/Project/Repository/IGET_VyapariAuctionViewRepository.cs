using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_VyapariAuctionViewRepository
    {
        Task<List<GET_VyapariAuctionView>> getasync(GET_VyapariAuctionViewDomain input);
        Task<List<GET_VyapariAuctionView>> getkeyword(GET_VyapariAuctionViewDomain input);
    }
}
