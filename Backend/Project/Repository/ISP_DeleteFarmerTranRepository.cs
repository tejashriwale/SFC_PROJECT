using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteFarmerTranRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeleteFarmerTran input);
    }
}
