using Project.Model;
using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_TransectionVoucherRepository
    {
        Task<List<ApiResponseMessage>> getasync(List<SP_TransectionVoucher> input);
    }
}
