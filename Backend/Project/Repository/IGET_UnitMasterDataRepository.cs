using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_UnitMasterDataRepository
    {
        Task<List<GET_UnitMasterData>> GetAll();
    }
}
