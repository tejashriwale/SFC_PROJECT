using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteStockTransferRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeleteStockTransfer input);
    }
}
