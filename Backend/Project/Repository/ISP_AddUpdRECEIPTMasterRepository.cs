using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdRECEIPTMasterRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_AddUpdRECEIPTMaster input);
    }
}
