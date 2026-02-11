using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdSReturnDetailsRepository
    {
        Task<List<ApiResponseMessage>>getasync(List<SP_AddUpdSReturnDetails> sP_AddUpdSReturnDetails);
    }
}
