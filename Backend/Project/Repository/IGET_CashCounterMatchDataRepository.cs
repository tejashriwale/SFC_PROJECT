using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CashCounterMatchDataRepository
    {
        Task<List<GET_CashCounterMatchData>> getasync(GET_CashCounterMatchDataDomain input);
        Task<List<GET_CashCounterMatchData>> getkeyword(GET_CashCounterMatchDataDomain input);
    }
}
