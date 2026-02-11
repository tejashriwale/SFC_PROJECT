using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_PlotMasterRepository
    {
        Task<List<GET_PlotMaster>>getasync(GET_PlotMasterDomain input);
        Task<List<GET_PlotMaster>>getkeyword(GET_PlotMasterDomain input);
    }
}
