using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Repository
{
    public interface IGetAdminAdvertisementRepository
    {
        Task<List<GetAdminAdvertisement>> getasync(GetAdminAdvertisementDomain input);
    }
}
