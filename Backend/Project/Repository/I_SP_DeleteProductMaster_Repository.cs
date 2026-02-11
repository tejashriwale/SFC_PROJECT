using Project.Model.Domain;

namespace Project.Repository
{
    public interface I_SP_DeleteProductMaster_Repository
    {
        Task<List<ApiResponseMessage>>getasync(_SP_DeleteProductMaster_ input);
    }
}
