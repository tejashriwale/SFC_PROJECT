using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_PChallanSearchByIDRepository
    {
        Task<List<GET_PChallanSearchByID>>getasync(GET_PChallanSearchByIDDomain input);
    }
}
