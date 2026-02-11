using Project.Model.Domain;

namespace Project.Repository
{
    public interface IAddAnalysisRepository
    {
        Task<List<ApiResponseMessage>>getasync(List<AddAnalysis> addAnalysis);
    }
}
