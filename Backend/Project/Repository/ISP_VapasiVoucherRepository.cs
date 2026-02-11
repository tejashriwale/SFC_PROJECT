using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_VapasiVoucherRepository
    {
        Task<List<ApiResponseMessage>> getasync(List<SP_VapasiVoucher> input);
    }
}
