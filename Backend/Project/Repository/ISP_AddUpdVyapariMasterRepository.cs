using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdVyapariMasterRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_AddUpdVyapariMaster input);
    }
}
