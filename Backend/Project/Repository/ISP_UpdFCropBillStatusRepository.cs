using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_UpdFCropBillStatusRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_UpdFCropBillStatus input);
    }
}
