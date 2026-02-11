using Project.Model.Domain;

namespace Project.Repository
{
    public interface I_GET_HSNMasters_Repository
    {
        Task<List<_GET_HSNMasters_>> getasync(DomainMaster input);

        Task<List<_GET_HSNMasters_>> GetByGroupAsync(DomainMaster input);

        Task<List<_GET_HSNMasters_>> Serchasync(DomainMaster input);
    }
}
