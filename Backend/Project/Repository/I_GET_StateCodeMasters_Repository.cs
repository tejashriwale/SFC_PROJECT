using Project.Model.Domain;

namespace Project.Repository
{
    public interface I_GET_StateCodeMasters_Repository
    {
        Task<List<_GET_StateCodeMasters_>> getasync(DomainMaster input);

        Task<List<_GET_StateCodeMasters_>> GetByGroupAsync(DomainMaster input);

        Task<List<_GET_StateCodeMasters_>> serchasync(DomainMaster input);
    }
}
