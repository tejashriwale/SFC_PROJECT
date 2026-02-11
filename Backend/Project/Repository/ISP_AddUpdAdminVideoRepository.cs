using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdAdminVideoRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_AddUpdAdminVideo input);
    }
}
