using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGETNewGenrateTokenNoRepository
    {
        Task<List<GETNewGenrateTokenNo>>getasync(GETNewGenrateTokenNoDomain input);          
    }
}
