using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_HRMEmployeesRepository
    {
        Task<List<GET_HRMEmployees>>getasync(GET_HRMEmployeesDomain input);
        
    }
}
