using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_DeficitByIdRepository
    {
        Task<List<GET_DeficitById>>getasync(GET_DeficitByIdDomain input);
    }
}
