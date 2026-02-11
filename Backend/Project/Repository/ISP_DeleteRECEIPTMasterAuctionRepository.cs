using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteRECEIPTMasterAuctionRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeleteRECEIPTMasterAuction input);
    }
}
