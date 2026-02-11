using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdSRequisitionMasterRepository
    {
        Task<List<ApiResponseMessage>>getasync(SP_AddUpdSRequisitionMaster sP_AddUpdSRequisitionMaster);
    }
}
