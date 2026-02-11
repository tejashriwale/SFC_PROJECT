using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdAuctionShedDeatilByBillRepository
    {
        Task<List<ApiResponseMessage>> getasync(List<SP_AddUpdAuctionShedDeatilByBill> input);
    }
}
