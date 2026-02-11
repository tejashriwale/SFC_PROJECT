using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdVyapariApproveFarmerRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_AddUpdVyapariApproveFarmer input);
    }
}
