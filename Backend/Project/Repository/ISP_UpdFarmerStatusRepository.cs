using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_UpdFarmerStatusRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_UpdFarmerStatus input);
    }
}
