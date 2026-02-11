using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_DepartmentNameRepository
    {
        Task<List<GET_DepartmentName>>getasync(GET_DepartmentNameDomain input);
    }
}
