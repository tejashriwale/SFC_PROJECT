using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteColdStorageOutwardRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeleteColdStorageOutward input);
    }
}
