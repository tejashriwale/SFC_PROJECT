using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_UpdateRECEIPTMasterRepository
    {
        Task<List<ApiResponseMessage>> getasync(List<SP_UpdateRECEIPTMaster> input);
    }
}
