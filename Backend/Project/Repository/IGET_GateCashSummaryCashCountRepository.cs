using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_GateCashSummaryCashCountRepository
    {
        Task<List<GET_GateCashSummaryCashCount>> getasync(GET_GateCashSummaryCashCountDomain input);
    }
}
