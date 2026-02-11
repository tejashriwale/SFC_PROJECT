using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteShopAlotRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeleteShopAlot input);
    }
}
