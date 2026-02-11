using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_FarmerSearchRepository
    {
        Task<List<GET_FarmerSearch>>getasync(GET_FarmerSearchDomain input);
    }
}
