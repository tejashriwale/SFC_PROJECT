using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteCategoryRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeleteCategory input);
    }
}
