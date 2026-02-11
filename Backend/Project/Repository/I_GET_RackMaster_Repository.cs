using Project.Model.Domain;

namespace Project.Repository
{
    public interface I_GET_RackMaster_Repository
    {
        Task<List<_GET_RackMaster_>>getasync(getByIdInput input);

        Task<List<_GET_RackMaster_>> GetByGroupAsync(getByIdInput input);

        Task<List<_GET_RackMaster_>> searchasync(getByIdInput input);
    }
}
