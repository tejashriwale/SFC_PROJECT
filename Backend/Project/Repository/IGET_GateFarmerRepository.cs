using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_GateFarmerRepository
    {
        Task<List<GET_GateFarmer>>getasync(GET_GateFarmerDomain input);
        Task<List<GET_GateFarmer>>getkeyword(GET_GateFarmerDomain input);
    }
}
