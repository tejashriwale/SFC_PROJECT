using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_MasterDetailInvertDataRepository
    {
        Task<List<GET_MasterDetailInvertData>> getasync(GET_MasterDetailInvertDataDomain input);
    }
}
