using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_POrderNoSearchRepository
    {
        Task<List<GET_POrderNoSearch>>getasync(GET_POrderNoSearchDomain input);
    }
}
