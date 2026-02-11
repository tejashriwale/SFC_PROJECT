using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_AuctionDetailsByIdRepository
    {
        Task<List<GET_AuctionDetailsById>>getasync(GET_AuctionDetailsByIdDomain input);
    }
}
