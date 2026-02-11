using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdSOrderDetailsRepository
    {
        Task<List<ApiResponseMessage>> getasync(List<SP_AddUpdSOrderDetails> sP_AddUpdSOrderDetails);
    }
}
