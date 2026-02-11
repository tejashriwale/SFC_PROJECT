using Project.Model.Domain;
using Project.Model.Model;

namespace Project.Repository
{
    public interface IWareHouseRepository
    {
        Task<List<ApiResponseMessage>> WareHouseAsync(WareHouseMaster wareHouse);
    }
}
