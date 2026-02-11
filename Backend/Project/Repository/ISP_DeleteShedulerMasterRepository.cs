using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteShedulerMasterRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeleteShedulerMaster input);
    }
}
