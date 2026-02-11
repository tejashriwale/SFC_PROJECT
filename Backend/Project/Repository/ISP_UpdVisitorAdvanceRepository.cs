using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_UpdVisitorAdvanceRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_UpdVisitorAdvance input);
    }
}
