using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteCropItemDetailsRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeleteCropItemDetails input);
    }
}
