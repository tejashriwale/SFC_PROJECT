using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteFarmerRepositorycs
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeleteFarmer input);
    }
}
