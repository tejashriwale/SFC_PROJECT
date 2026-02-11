using Project.Model.Domain;

namespace Project.Repository
{
    public interface IAddUpdCategoryMasterRepository
    {
         Task<List<ApiResponseMessage>>getasync(AddUpdCategoryMaster categoryMaster);
    }
}
