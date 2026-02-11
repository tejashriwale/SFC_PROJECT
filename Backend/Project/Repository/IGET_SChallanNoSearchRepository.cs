using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_SChallanNoSearchRepository
    {
        Task<List<GET_SChallanNoSearch>>getasync(GET_SChallanNoSearchDomain input);
    }
}
