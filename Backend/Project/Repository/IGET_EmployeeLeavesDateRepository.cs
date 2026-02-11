using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_EmployeeLeavesDateRepository
    {
        Task<List<GET_EmployeeLeavesDate>>getasync(GET_EmployeeLeavesDateDomain input);

        Task<List<GET_EmployeeLeavesDate>> getkeyword(GET_EmployeeLeavesDateDomain input);
    }
}
