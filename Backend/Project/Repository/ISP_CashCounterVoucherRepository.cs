using Project.Model.Domain;

using Project.Model.DTO;

namespace Project.Repository
{
    public interface ISP_CashCounterVoucherRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_CashCounterVoucher input);
    }
}