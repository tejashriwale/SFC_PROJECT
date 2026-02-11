using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_HRMPayrollRepository
    {
        Task<List<GET_HRMPayroll>>getasync(GET_HRMPayrollDomain input);
    }
}
