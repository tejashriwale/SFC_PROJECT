using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_FarmerBillVoucherRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_FarmerBillVoucher input);
    }
}
