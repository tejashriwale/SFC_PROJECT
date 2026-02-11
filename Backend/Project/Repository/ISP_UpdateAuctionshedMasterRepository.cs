using Project.Model;
using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_UpdateAuctionshedMasterRepository
    {
        Task<List<ApiResponseMessage>> getasync(List<SP_UpdateAuctionshedMaster> input);
    }
}
