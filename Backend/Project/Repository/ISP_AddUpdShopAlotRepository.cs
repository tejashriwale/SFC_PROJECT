using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdShopAlotRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_AddUpdShopAlot input);
    }
}
