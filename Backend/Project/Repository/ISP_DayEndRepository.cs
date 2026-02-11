using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DayEndRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DayEnd input);
    }
}
