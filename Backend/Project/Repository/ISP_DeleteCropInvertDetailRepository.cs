using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteCropInvertDetailRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeleteCropInvertDetail input);
    }
}
