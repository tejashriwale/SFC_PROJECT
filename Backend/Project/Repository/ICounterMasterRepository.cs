using Project.Model.Domain;

namespace Project.Repository
{
    public interface ICounterMasterRepository
    {
        Task<List<ApiResponseMessage>> getasync(CounterMaster counterMaster);
    }
}
