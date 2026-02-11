using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_ProfitLossDataRepository
    {
        Task<List<GET_ProfitLossData>> getasync(GET_ProfitLossDataDomain input);
    }
}
