using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdCropBillDetailRepository
    {
        Task<List<ApiResponseMessage>> getasync(List<SP_AddUpdCropBillDetail> input);
    }
}
