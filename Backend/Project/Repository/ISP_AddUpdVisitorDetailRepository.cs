using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdVisitorDetailRepository
    {
        Task<List<ApiResponseMessage>> getasync(List<SP_AddUpdVisitorDetail> input);
    }
}
