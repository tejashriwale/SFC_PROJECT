using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Repository
{
    public interface ISP_AddUpdRTGSReposiytory
    {
        Task<List<ApiResponseMessage>> getasync(SP_AddUpdRTGS input);
    }
}
