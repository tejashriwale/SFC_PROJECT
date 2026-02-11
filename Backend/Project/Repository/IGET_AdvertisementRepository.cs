using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_AdvertisementRepository
    {
        Task<List<GET_Advertisement>>getasync(GET_AdvertisementDomain input);
        Task<List<GET_Advertisement>>getkeyword(GET_AdvertisementDomain input);
    }
}
