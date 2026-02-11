using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteCashInvertRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeleteCashInvert input);
    }
}
