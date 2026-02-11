using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdCashSummaryUpdateRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_AddUpdCashSummaryUpdate input);
    }
}
