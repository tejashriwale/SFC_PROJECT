using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdVyapariLimitRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_AddUpdVyapariLimit input);
    }
}
