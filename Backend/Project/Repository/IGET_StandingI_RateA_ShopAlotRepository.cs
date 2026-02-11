using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_StandingI_RateA_ShopAlotRepository
    {
        Task<List<GET_StandingI_RateA_ShopAlot>>getasync(GET_StandingI_RateA_ShopAlotDomain input);
        Task<List<GET_StandingI_RateA_ShopAlot>>getkeyword(GET_StandingI_RateA_ShopAlotDomain input);
    }
}
