using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_AnalysisRepository
    {
        Task<List<GET_Analysis>>getasync(CounterMasterDomain masterDomain);
    }
}
