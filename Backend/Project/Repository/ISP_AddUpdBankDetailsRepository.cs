using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdBankDetailsRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_AddUpdBankDetails input);
    }
}
