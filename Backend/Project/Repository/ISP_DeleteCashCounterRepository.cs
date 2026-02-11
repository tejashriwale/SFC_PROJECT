using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteCashCounterRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeleteCashCounter input);
    }
}
