using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_UpdFarmerProfileRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_UpdFarmerProfile input);
    }
}
