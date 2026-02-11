using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_StateCodeDistinctRepository
    {
        Task<List<GET_StateCodeDistinct>> getasync();
    }
}
