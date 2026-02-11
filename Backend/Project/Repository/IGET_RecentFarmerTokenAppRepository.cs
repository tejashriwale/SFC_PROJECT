using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_RecentFarmerTokenAppRepository
    {
        Task<List<GET_RecentFarmerTokenApp>> getasync(GET_RecentFarmerTokenAppDomain input);
    }
}
