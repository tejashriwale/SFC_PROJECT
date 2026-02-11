using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddVehicalDetailsRepository
    {
        Task<List<ApiResponseMessage>> getasync(List<SP_AddVehicalDetails> input);
    }
}
