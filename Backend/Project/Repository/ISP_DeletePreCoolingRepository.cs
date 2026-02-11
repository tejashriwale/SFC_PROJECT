using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeletePreCoolingRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeletePreCooling input);
    }
}
