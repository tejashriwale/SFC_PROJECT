using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdHRMEmpolyeeLeavesRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_AddUpdHRMEmpolyeeLeaves sP_AddUpdHRMEmpolyeeLeaves);
    }
}
