using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_CheckVehicalNoRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_CheckVehicalNo input);
    }
}
