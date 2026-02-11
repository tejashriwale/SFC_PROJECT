using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_updateVoucherRepository
    {
        Task<List<ApiResponseMessage>> getasync(List<SP_updateVoucher> input);
    }
}
