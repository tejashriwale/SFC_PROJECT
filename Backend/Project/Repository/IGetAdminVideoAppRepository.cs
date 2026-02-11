using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Repository
{
    public interface IGetAdminVideoAppRepository
    {
        Task<List<GetAdminVideoApp>> getasync(GetAdminVideoAppDomain input);
    }
}
