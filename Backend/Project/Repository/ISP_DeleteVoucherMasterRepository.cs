using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteVoucherMasterRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeleteVoucherMaster input);
    }
}
