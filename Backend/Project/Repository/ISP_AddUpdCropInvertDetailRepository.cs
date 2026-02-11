using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdCropInvertDetailRepository
    {
        Task<List<ApiResponseMessage>> getasync(List<SP_AddUpdCropInvertDetail> input);

    }
}
