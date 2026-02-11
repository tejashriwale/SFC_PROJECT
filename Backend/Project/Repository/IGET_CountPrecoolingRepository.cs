using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CountPrecoolingRepository
    {
        Task<List<GET_CountPrecooling>>getasync(GET_CountPrecoolingDomain input);
    }
}
