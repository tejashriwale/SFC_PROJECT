using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdPlotMasterRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_AddUpdPlotMaster input);
    }
}
