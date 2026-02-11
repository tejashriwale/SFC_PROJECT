using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_MeassagePChallanRepository
    {
        Task<List<ApiResponseMessage>>getasync(SP_MeassagePChallan sP_MeassagePChallan);
    }
}
