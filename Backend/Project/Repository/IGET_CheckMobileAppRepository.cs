using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CheckMobileAppRepository
    {
        Task<List<GET_CheckMobileApp>>getasync(GET_CheckMobileAppDomain input);
    }
}
