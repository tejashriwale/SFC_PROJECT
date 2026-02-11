using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_Report_DayAuctionRepository
    {
        Task<List<GET_Report_DayAuction>> getasync(GET_Report_DayAuctionDomain input);
    }
}
