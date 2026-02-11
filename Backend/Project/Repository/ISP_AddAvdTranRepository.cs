using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddAvdTranRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_AddAvdTran input);
    }
}
