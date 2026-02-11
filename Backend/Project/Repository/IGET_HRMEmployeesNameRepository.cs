using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_HRMEmployeesNameRepository
    {
        Task<List<GET_HRMEmployeesName>>getasync(GET_HRMEmployeesNameDomain input);
    }
}
