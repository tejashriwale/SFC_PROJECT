using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddVoucherRepository
    {
        Task<List<ApiResponseMessage>> getasync(List<SP_AddVoucher> input);
    }
}
