using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteSChallanRepository
    {
        Task<List<ApiResponseMessage>>getasync(SP_DeleteSChallan input);
    }
}
