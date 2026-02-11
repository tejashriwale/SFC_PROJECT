using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdOTPRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_AddUpdOTP input);
    }
}
