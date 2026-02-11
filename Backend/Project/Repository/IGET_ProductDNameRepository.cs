using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_ProductDNameRepository
    {
        Task<List<GET_ProductDName>> getasync(GET_ProductDNameDomain input);
    }
}
