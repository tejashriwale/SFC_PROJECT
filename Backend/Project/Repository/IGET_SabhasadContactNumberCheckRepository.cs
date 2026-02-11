using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_SabhasadContactNumberCheckRepository
    {
        Task<List<GET_SabhasadContactNumberCheck>> getasync(GET_SabhasadContactNumberCheckDomain input);
    }
}
