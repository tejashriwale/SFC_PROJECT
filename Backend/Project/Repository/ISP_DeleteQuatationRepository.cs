using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteQuatationRepository
    {
        Task<List<ApiResponseMessage>>getasync(SP_DeletePQuatationDomain input);
    }
}
