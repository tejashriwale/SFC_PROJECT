using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteAccountsRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeleteAccounts input);
    }
}
