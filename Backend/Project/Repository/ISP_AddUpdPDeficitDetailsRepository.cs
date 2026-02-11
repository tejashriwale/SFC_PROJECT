using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdPDeficitDetailsRepository
    {
        Task<List<ApiResponseMessage>> getasync(List<SP_AddUpdPDeficitDetails> input);
    }
}
