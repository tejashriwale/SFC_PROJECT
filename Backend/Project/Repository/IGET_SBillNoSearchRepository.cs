using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_SBillNoSearchRepository
    {
        Task<List<GET_SBillNoSearch>>getasync(GET_SBillNoSearchDomain input);
    }
}
