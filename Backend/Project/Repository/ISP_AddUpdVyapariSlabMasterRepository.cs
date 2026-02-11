using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdVyapariSlabMasterRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_AddUpdVyapariSlabMaster input);
    }
}
