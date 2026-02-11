using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Repository
{
    public interface ISP_AddUpdRECEIPTMasterAddAuctionRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_AddUpdRECEIPTMasterAddAuction input);
    }
}
