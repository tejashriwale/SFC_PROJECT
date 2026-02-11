using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_ColdStorageOutWardRepository
    {
        Task<List<GET_ColdStorageOutWard>> getasync(GET_ColdStorageOutWardDomain input);
        Task<List<GET_ColdStorageOutWard>> keyword(GET_ColdStorageOutWardDomain input);
    }
}
