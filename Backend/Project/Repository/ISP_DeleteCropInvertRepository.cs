using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteCropInvertRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeleteCropInvert input);

    }
}
