using Project.Controllers;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdPreCoolingRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_AddUpdPreCooling input);
    }
}
