using Project.Model.Domain;

namespace Project.Repository
{
    public interface IWAREHOUSENAMERepository
    {
        Task<List<WAREHOUSENAME>> getasync(GET_WarehouseNameDomain input);
    }
}
