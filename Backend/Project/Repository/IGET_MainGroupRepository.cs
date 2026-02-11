using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_MainGroupRepository
    {
        Task<List<GET_MainGroup>>getasync();
    }
}
