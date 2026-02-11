using Project.Model.Domain;

namespace Project.Repository
{
    public interface IPackingMasterRepository
    {
        Task<List<ApiResponseMessage>> getasync(PackingMasters packingMasters);
    }
}
