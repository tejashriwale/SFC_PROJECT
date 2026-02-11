using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_UpdFarmerBillStatusRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_UpdFarmerBillStatus input);

    }
}
