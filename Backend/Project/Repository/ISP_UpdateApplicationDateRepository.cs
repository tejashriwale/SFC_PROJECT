using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_UpdateApplicationDateRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_UpdateApplicationDate input);
    }
}
