using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdAuctionshedMasterRepository
    {
        Task<List<ApiResponseMessage2>> getasync(SP_AddUpdAuctionshedMaster input);
    }
}
