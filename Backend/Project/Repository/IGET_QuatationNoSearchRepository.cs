using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_QuatationNoSearchRepository
    {
        Task<List<GET_SQuatationNoSearch>>getasync(GET_SQuatationNoSearchDomain input);
    }
}
