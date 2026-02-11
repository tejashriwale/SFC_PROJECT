using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdCashCounterNameRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_AddUpdCashCounterName input);
    }
}
