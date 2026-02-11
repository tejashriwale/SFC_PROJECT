using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_POrderNoCheckRepository
    {
        Task<List<GET_POrderNoCheck>> getasync(GET_POrderNoCheckDomain input);
    }
}
