using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_ColdStorageOutWardByIdRepository
    {
        Task<List<GET_ColdStorageOutWardById>> getasync(GET_ColdStorageOutWardByIdDomain input);
    
    }
}
