using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddFamilyDetailsAppRepository
    {
        Task<List<ApiResponseMessage>> getasync(List<SP_AddFamilyDetailsApp> input);
    }
}
