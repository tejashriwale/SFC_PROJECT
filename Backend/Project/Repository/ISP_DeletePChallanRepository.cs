using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeletePChallanRepository
    {
        Task<List<ApiResponseMessage>>getasync(SP_DeletePChallanDomain input);
    }
}
