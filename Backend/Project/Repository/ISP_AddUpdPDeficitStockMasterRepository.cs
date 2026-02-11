using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdPDeficitStockMasterRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_AddUpdPDeficitStockMaster input);
    }
}
