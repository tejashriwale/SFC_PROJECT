using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteDelarRateRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeleteDelarRate input);
    }
}
