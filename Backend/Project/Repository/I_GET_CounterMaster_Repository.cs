using Project.Model.Domain;

namespace Project.Repository
{
    public interface I_GET_CounterMaster_Repository
    {
        Task<List<_GET_CounterMaster_>>getasync(MasterInput input);

        Task<List<_GET_CounterMaster_>> GetByGroupAsync(MasterInput input);
    }
}
