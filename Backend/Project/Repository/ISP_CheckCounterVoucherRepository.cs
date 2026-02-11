using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_CheckCounterVoucherRepository
    {
        Task<List<ApiResponseMessage>> getasync(List<SP_CheckCounterVoucher> input);
    }
}
