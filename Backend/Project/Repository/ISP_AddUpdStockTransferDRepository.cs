using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdStockTransferDRepository
    {
        Task<List<ApiResponseMessage>>getasync(List<SP_AddUpdStockTransferD> input);
    }
}
