using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Repository
{
    public interface IGET_FarmerAddedByTranspoterAllRepository
    {
        Task<List<GET_FarmerAddedByTranspoterAll>> getasync(GET_FarmerAddedByTranspoterAllDomain input);
    }
}
