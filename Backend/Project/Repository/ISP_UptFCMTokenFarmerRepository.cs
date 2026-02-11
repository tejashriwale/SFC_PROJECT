using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_UptFCMTokenFarmerRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_UptFCMTokenFarmer input);
    }
}
