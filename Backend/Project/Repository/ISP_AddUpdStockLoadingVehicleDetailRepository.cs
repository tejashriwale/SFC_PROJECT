using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdStockLoadingVehicleDetailRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_AddUpdStockLoadingVehicleDetail input);
    }
}
