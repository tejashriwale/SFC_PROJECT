using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteAdvertisementRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeleteAdvertisement input);
    }
}
