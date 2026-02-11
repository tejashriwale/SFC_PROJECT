using Project.Model.Domain;

namespace Project.Repository
{
    public interface I_GET_PackingMasters_Repository
    {
        Task<List<_GET_PackingMasters_>> getasync(PackingDoomain input);

        Task<List<_GET_PackingMasters_>> GetByGroupAsync(PackingDoomain input);
    }
}
