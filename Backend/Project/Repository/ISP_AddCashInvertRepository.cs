using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddCashInvertRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_AddCashInvert input);
    }
}
