using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_TyreProductMasterRepository
    {
        Task<List<GET_TyreProductMaster>> getasync(GET_TyreProductMasterDomain input);
        Task<List<GET_TyreProductMaster>> getkeyword(GET_TyreProductMasterDomain input);
    }
}
