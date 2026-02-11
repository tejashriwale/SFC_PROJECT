using Project.Model;
using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CheckTranspoterTaidRepository
    {
        Task<List<GET_CheckTranspoterTaid>> getasync(GET_CheckTranspoterTaidDomain input);
    }
}
