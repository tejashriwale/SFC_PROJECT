using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_StockLoadingVehicleMasterRepository
    {
        Task<List<GET_StockLoadingVehicleMaster>> getasync(GET_StockLoadingVehicleMasterDomain input);
        Task<List<GET_StockLoadingVehicleMaster>> getkeyword(GET_StockLoadingVehicleMasterDomain input);
    }
}
