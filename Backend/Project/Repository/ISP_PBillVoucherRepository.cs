using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_PBillVoucherRepository
    {
        Task<List<ApiResponseMessage>> getasync(List<SP_PBillVoucher> input);
    }
}
