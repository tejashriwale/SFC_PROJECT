using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteBOMPackingRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeleteBOMPacking input);
    }
}
