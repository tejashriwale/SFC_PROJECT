using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdAccountsRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_AddUpdAccounts input);
    }
}
