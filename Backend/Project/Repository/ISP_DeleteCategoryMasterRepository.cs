using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteCategoryMasterRepository
    {
        Task<List<ApiResponseMessage>>getasync(SP_DeleteCategoryMaster input);
    }
}
