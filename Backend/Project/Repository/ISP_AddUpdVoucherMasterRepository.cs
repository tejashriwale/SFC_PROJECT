using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdVoucherMasterRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_AddUpdVoucherMaster input);
    }
}
