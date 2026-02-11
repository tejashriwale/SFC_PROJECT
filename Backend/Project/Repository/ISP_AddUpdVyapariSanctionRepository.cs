using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdVyapariSanctionRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_AddUpdVyapariSanction input);
    }
}
