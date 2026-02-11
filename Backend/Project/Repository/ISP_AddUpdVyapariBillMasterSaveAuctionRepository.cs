using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdVyapariBillMasterSaveAuctionRepository
    {
        Task<List<ApiResponseMessage>> getasync(List<SP_AddUpdVyapariBillMasterSaveAuction> input);
    }
}
