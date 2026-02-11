using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_StockLoadingVehicleDetailRepository
    {
        Task<List<GET_StockLoadingVehicleDetail>> getasync(GET_StockLoadingVehicleDetailDomain input);
    }
}
