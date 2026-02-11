using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_GateCashSummaryRepository
    {
        Task<List<GET_GateCashSummary>>getasync(GET_GateCashSummaryDomain input);
        
    }
}
