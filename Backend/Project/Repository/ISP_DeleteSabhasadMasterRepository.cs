using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteSabhasadMasterRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeleteSabhasadMaster input);
    }
}
