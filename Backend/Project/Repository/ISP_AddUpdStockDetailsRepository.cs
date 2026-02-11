using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdStockDetailsRepository
    {
        Task<List<ApiResponseMessage>> getasync(List<SP_AddUpdStockDetails> input);
    }
}
