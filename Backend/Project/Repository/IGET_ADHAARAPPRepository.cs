using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_ADHAARAPPRepository
    {
        Task<List<GET_ADHAARAPP>> getasync(GET_ADHAARAPPDomain input);
    }
}
