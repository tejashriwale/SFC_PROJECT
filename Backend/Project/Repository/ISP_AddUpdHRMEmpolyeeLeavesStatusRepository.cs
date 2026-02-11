using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdHRMEmpolyeeLeavesStatusRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_AddUpdHRMEmpolyeeLeavesStatusDomain input);
    }
}
