using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdColdStorageOutwardRepository
    {
        Task<List<ApiResponseMessage>> getasync(List<SP_AddUpdColdStorageOutward> input);
    }
}
