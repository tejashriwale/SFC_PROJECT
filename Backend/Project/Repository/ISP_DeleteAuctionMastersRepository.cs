using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteAuctionMastersRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeleteAuctionMasters input);
    }
}
