using Project.Model;
using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Repository
{
    public interface IGET_StockCountRepository
    {
        Task<List<GET_StockCount>>getasync(GET_StockCountDomain input);
    }
}
