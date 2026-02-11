using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdHRMPayrollRepository
    {
        Task<List<ApiResponseMessage>>getasync(SP_AddUpdHRMPayroll input);
    }
}
