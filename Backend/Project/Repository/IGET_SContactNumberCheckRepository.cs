using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_SContactNumberCheckRepository
    {
        Task<List<GET_SContactNumberCheck>> getasync(GET_SContactNumberCheckDomain input);
    }
}
