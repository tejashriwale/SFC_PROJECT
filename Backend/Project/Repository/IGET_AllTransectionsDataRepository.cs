using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_AllTransectionsDataRepository
    {
        Task<List<GET_AllTransectionsData>> getasync(GET_AllTransectionsDataDomain input);
    }
}
