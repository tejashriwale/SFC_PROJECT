using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CounterNameRepository
    {
        Task<List<GET_CounterName>>getasync(GET_CounterNameDomain input);
    }
}
