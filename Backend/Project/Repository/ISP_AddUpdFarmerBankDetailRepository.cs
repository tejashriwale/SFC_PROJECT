using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdFarmerBankDetailRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_AddUpdFarmerBankDetail input);
    }
}
