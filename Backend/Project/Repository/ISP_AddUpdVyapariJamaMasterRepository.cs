using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdVyapariJamaMasterRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_AddUpdVyapariJamaMaster input);
    }
}
