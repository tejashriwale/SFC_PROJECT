using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_PBillNoSearchRepository
    {
        Task<List<GET_PBillNoSearch>>getasync(GET_PBillNoSearchDomain input);
    }
}
