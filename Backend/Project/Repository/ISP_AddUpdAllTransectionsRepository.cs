using Project.Model.Domain;
using Project.Model.Model;

namespace Project.Repository
{
    public interface ISP_AddUpdAllTransectionsRepository
    {
        Task<List<ApiResponseMessage>> getasync(List<SP_AddUpdAllTransections> input);
    }
}
