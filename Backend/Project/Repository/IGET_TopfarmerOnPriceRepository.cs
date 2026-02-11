using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_TopfarmerOnPriceRepository
    {
        Task<List<GET_TopfarmerOnPrice>> getasync(GET_TopfarmerOnPriceDomain input);
    }
}
