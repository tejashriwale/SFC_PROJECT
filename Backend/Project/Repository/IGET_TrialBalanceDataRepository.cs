using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_TrialBalanceDataRepository
    {
        Task<List<GET_TrialBalanceData>> getasync(GET_TrialBalanceDataDomain input);
    }
}
