using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddFamilyDetailsRepository
    {
        Task<List<ApiResponseMessage>> getasync(List<SP_AddFamilyDetails> input);
    }
}
