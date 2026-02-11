using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CategoryMasterDataRepository
    {
        Task<List<GET_CategoryMasterData>>getasync(GET_CategoryMasterDataDomain input);
        Task<List<GET_CategoryMasterData>>getkeyword(GET_CategoryMasterDataDomain input);
    }
}
