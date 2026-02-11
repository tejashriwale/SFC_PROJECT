using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteRewardRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeleteReward input);
    }
}
