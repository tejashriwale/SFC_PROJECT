using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_ShopNameRepository
    {
        Task<List<GET_ShopName>>getasync(GET_ShopNameDomain input);
    }
}
