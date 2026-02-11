using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_Report_AuctionRepository
    {
        Task<List<GET_Report_Auction>> getasync(GET_Report_AuctionDomain input);
    }
}
