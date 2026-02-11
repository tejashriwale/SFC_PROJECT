using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdCashCounterRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_AddUpdCashCounter input);
    }
}
