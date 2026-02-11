using Project.Model.Domain;

namespace Project.Repository
{
    public interface IImplicationsRepository
    {
        Task<List<Implications>> GetAllAsync();

        Task<List<Implications>> GetByGroupAsync(string groupid);
    }
}
