using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGETNewGenrateBillNoRepository
    {
        Task<List<GETNewGenrateBillNo>>getasync(GETNewGenrateBillNoDomain input);
    }
}
