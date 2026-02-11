using Project.Model.Domain;

namespace Project.Repository
{
    public interface IProductMasterRepository
    {
        Task<List<ApiResponseMessage>>getasync(ProductMaster productMaster);
    }
}
