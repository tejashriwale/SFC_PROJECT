using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_AnalyzeDataRepository
    {
        Task<List<GET_AnalyzeData>> getasync(GET_AnalyzeDataDomain gET_Analyze);
    }
}
