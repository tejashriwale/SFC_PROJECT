using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteVyapariSlabMasterRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeleteVyapariSlabMaster input);
    }
}
