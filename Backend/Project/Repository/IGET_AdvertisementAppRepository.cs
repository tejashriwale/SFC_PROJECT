using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_AdvertisementAppRepository
    {
        Task<List<GET_AdvertisementApp>> GetAllAdvertisementsAsync();
    }
}
