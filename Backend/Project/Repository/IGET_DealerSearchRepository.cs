using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_DealerSearchRepository
    {
        Task<List<GET_DealerSearch>> getasync(GET_DealerSearchDomain input);
    }
}
