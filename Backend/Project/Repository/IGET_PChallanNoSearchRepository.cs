using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_PChallanNoSearchRepository
    {
        Task<List<GET_PChallanNoSearch>>getasync(GET_PChallanNoSearchDomain input);
    }
}
