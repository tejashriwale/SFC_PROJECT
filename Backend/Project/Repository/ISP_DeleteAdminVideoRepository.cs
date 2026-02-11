using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteAdminVideoRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeleteAdminVideo input);
    }
}
