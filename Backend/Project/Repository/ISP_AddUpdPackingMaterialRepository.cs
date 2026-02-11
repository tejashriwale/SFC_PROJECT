using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdPackingMaterialRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_AddUpdPackingMaterial input);
    }
}
