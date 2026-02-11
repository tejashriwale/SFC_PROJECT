using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_ColdStorageInwardRepository
    {
        Task<List<GET_ColdStorageInward>> getasync(GET_ColdStorageInwardDomain input);
        Task<List<GET_ColdStorageInward>> getkeyword(GET_ColdStorageInwardDomain input);
    }
}
