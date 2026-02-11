using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_FarmerAdvanceRepository
    {
        Task<List<GET_FarmerAdvance>> getasync(GET_FarmerAdvanceDomain input);
    }
}
