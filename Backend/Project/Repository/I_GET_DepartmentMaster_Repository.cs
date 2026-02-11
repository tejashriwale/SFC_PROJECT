using Project.Model.Domain;

namespace Project.Repository
{
    public interface I_GET_DepartmentMaster_Repository
    {
        Task<List<_GET_DepartmentMaster_>>getasync(DepartmentMasterDomain input );

        Task<List<_GET_DepartmentMaster_>> GetByGroupAsync(DepartmentMasterDomain input);

        Task<List<_GET_DepartmentMaster_>> searchasync(DepartmentMasterDomain input);
    }
}
