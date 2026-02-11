using Project.Model.Domain;

namespace Project.Repository
{
    public interface I_GET_ProductMaster_Repository
    {
        Task<List<_GET_ProductMaster_>>getasync(productmasterDomain input);

        Task<List<_GET_ProductMaster_>> GetByGroupAsync(productmasterDomain input);
        Task<List<_GET_ProductMaster_>> searchasync(productmasterDomain input);

    }
}
