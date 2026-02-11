using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Repository
{
    public interface IGET_ShopAlotRepository
    {
        Task<List<GET_ShopAlot>>getasync(GET_ShopAlotDomain input);
        Task<List<GET_ShopAlot>>getkeyword(GET_ShopAlotDomain input);
    }
}
