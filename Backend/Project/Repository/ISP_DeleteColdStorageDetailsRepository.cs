using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteColdStorageDetailsRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeleteColdStorageDetails input);
    }
}
