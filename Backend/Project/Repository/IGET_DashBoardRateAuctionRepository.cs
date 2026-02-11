using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_DashBoardRateAuctionRepository
    {
        Task<List<GET_DashBoardRateAuction>> getall();
    }
}
