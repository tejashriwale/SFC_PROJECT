using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddBarCodeRepository
    {
        Task<List<ApiResponseMessage>>getasync(List<SP_AddBarCode> sP_AddBarCode);
    }
}
