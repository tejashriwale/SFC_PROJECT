using Project.Model;
using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_TrialBalanceDataRepository
    {
        Task<List<SP_TrialBalanceData>> getasync(SP_TrialBalanceDataDomain input);
    }
}
