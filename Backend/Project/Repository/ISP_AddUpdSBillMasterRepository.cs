using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdSBillMasterRepository
    {
        Task<List<ApiResponseMessage>>getasync(SP_AddUpdSBillMaster sP_AddUpdSBillMaster);
    }
}
