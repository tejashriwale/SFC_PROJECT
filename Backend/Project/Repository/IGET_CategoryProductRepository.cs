using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CategoryProductRepository
    {
        Task<List<GET_CategoryProduct>>getasync(GET_CategoryProductDomain customer);
    }
}
