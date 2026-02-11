using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeletePlotMasterRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeletePlotMaster input);
    }
}
