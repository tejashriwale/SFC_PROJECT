using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdFarmerRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_AddUpdFarmer input);
    }
}
