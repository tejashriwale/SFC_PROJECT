using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_TOKENNoSearchRepository
    {
        Task<List<GET_TOKENNoSearch>>getasync(GET_TOKENNoSearchDomain input);
    }
}
