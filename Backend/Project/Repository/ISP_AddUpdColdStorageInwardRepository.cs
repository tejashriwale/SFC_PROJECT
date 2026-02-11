using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdColdStorageInwardRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_AddUpdColdStorageInward input);
    }
}
