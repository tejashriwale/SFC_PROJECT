using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_StockTransferRepository
    {
        Task<List<GET_StockTransfer>>getasync(GET_StockTransferDomain input);

        Task<List<GET_StockTransfer>> get(GET_StockTransferDomain input);
    }
}
