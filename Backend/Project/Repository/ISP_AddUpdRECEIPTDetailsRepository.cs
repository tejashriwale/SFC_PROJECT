using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdRECEIPTDetailsRepository
    {
        Task<List<ApiResponseMessage>> getasync(List<SP_AddUpdRECEIPTDetails> input);
    }
}
