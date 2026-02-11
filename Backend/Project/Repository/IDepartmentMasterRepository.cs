using Project.Model.Domain;

namespace Project.Repository
{
    public interface IDepartmentMasterRepository
    {
        Task<List<ApiResponseMessage>> getasync (DepartmentMaster departmentMaster);
    }
}
