using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteCropDirectInvertDetailRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeleteCropDirectInvertDetail input);
    }
}
