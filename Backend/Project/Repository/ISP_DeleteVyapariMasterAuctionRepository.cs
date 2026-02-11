using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteVyapariMasterAuctionRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeleteVyapariMasterAuction input);
    }
}
