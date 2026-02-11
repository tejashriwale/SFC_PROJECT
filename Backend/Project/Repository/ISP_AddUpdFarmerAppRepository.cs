using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdFarmerAppRepository
    {
        Task<List<SP_AddUpdFarmerApp>> getasync(SP_AddUpdFarmerAppDomain input);
    }
}
