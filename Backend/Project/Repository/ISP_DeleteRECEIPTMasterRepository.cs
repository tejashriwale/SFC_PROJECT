using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteRECEIPTMasterRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeleteRECEIPTMaster input);
    }
}
