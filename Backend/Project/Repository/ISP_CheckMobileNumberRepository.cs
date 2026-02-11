using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_CheckMobileNumberRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_CheckMobileNumber input);
    }
}
