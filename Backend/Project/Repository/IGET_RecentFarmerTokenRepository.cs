using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_RecentFarmerTokenRepository
    {
        Task<List<GET_RecentFarmerToken>> getasync(GET_RecentFarmerTokenDomain input);
    }
}
