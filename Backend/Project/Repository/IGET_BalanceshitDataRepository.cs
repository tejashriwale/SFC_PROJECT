using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_BalanceshitDataRepository
    {
        Task<List<GET_BalanceshitData>> getasync(GET_BalanceshitDataDomain input);
    }
}
