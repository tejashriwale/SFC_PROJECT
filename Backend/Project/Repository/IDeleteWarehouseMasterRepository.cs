using Project.Model.Domain;

namespace Project.Repository
{
    public interface IDeleteWarehouseMasterRepository
    {
        Task<List<ApiResponseMessage>>getasync(DeleteWarehouseMaster input);
    }
}
