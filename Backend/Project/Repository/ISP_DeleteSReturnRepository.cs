using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteSReturnRepository
    {
        Task<List<ApiResponseMessage>>getasync(SP_DeleteSReturn input);
    }
}
