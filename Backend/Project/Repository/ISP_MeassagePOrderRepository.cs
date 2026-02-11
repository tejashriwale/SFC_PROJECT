using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_MeassagePOrderRepository
    {
        Task<List<ApiResponseMessage>>getasync(SP_MeassagePOrder sP_MeassagePOrder);
    }
}
