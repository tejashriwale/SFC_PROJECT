using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeletePOrderRepository
    {
        Task<List<ApiResponseMessage>>getasync(SP_DeletePOrderDomain input);
    }
}
