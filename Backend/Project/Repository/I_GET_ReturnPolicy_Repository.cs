using Project.Model.Domain;

namespace Project.Repository
{
    public interface I_GET_ReturnPolicy_Repository
    {
        Task<List<_GET_ReturnPolicy_>> getasync(MasterInput input);

        Task<List<_GET_ReturnPolicy_>> GetByGroupAsync(MasterInput input);
    }
}
