using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_BillNoSearchRepository
    {
        Task<List<GET_BillNoSearch>>getasync(GET_BillNoSearchDomain input);
        Task<List<GET_BillNoSearch>>getkeyword(GET_BillNoSearchDomain input);
    }
}
