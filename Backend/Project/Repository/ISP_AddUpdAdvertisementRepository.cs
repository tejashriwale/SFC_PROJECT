using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdAdvertisementRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_AddUpdAdvertisement input);
    }
}
