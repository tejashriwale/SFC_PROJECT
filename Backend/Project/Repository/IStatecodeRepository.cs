using Project.Model.Domain;

namespace Project.Repository
{
    public interface IStatecodeRepository
    {
        Task<List<ApiResponseMessage>> getAsync(Statecode statecode);
    }
}
