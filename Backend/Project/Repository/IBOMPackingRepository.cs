using Project.Model.Domain;

namespace Project.Repository
{
    public interface IBOMPackingRepository
    {
        Task<List<ApiResponseMessage>> getasync(List<BOMPacking> bOMPackings);
    }
}
