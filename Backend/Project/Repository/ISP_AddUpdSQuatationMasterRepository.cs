using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdSQuatationMasterRepository
    {
        Task<List<ApiResponseMessage>>getasync(SP_AddUpdSQuatationMaster sP_AddUpdSQuatationMaster);
    }
}
