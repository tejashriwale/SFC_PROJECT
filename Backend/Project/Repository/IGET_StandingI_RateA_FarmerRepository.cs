using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_StandingI_RateA_FarmerRepository
    {
        Task<List<GET_StandingI_RateA_Farmer>>getasync(GET_StandingI_RateA_FarmerDomain input);
        Task<List<GET_StandingI_RateA_Farmer>>getkeyword(GET_StandingI_RateA_FarmerDomain input);
    }
}
