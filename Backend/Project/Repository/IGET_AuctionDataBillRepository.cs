using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_AuctionDataBillRepository
    {
        Task<List<GET_AuctionDataBill>>getasync(GET_AuctionDataBillDomain input);
    }
}
