using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdSReturnMasterRepository
    {
        Task<List<ApiResponseMessage>>getasync(SP_AddUpdSReturnMaster sP_AddUpdSReturnMaster);
    }
}
