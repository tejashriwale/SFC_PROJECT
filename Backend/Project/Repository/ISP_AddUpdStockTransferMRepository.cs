using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdStockTransferMRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_AddUpdStockTransferM input);
    }
}
