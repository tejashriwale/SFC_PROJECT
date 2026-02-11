using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_SOrderNoSearchRepository
    {
        Task<List<GET_SOrderNoSearch>>getasync(GET_SOrderNoSearchDomain input);
    }
}
