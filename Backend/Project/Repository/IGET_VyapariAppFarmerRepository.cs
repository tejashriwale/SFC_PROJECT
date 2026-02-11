using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_VyapariAppFarmerRepository
    {
        Task<List<GET_VyapariAppFarmer>> getasync(GET_VyapariAppFarmerDomain input);
        Task<List<GET_VyapariAppFarmer>> getkeyword(GET_VyapariAppFarmerDomain input);
    }
}
