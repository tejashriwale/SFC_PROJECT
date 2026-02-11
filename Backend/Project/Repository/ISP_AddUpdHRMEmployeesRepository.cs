using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdHRMEmployeesRepository
    {
        Task<List<ApiResponseMessage>>getasync(SP_AddUpdHRMEmployees sP_AddUpdHRMEmployees);
    }
}
