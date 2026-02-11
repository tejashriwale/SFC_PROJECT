using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_StockTransferDRepository
    {
        Task<List<GET_StockTransferD>>getasync(GET_StockTransferDDomain input);
    }
}
