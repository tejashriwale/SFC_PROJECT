using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdChequeCounterUpdateRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_AddUpdChequeCounterUpdate input);
    }
}
