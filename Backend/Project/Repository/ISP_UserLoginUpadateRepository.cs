using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_UserLoginUpadateRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_UserLoginUpadate input);
    }
}
