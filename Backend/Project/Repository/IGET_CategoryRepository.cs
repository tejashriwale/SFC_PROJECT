using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CategoryRepository
    {
        Task<List<GET_Category>>getasync(GET_CategoryDomain category);
    }
}
