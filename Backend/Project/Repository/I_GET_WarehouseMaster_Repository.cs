using Project.Model;
using Project.Model.Domain;

namespace Project.Repository
{
    public interface I_GET_WarehouseMaster_Repository
    {
        Task<List<_GET_WarehouseMaster_>> getasync(getByIdInput input);

        Task<List<_GET_WarehouseMaster_>> GetByGroupAsync(getByIdInput input);

        Task<List<_GET_WarehouseMaster_>> searchasync(getByIdInput input);
    }
}
