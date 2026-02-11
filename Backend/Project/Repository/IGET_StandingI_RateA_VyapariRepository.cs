using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_StandingI_RateA_VyapariRepository
    {
        Task<List<GET_StandingI_RateA_Vyapari>>getasync(GET_StandingI_RateA_VyapariDomain input);
        Task<List<GET_StandingI_RateA_Vyapari>>getkeyword(GET_StandingI_RateA_VyapariDomain input);
    }
}
