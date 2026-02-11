using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdSChallanMasterRepository
    {
        Task<List<ApiResponseMessage>>getasync(SP_AddUpdSChallanMaster sP_AddUpdSChallanMaster);
    }
}
