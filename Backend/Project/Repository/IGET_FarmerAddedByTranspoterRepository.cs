using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_FarmerAddedByTranspoterRepository
    {
        Task<List<GET_FarmerAddedByTranspoter>> getasync(GET_FarmerAddedByTranspoterDomain input);
    }
}
