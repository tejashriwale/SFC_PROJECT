using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdRewardPointsRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_AddUpdRewardPoints input);
    }
}
