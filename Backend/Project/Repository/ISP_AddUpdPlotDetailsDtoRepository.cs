using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdPlotDetailsDtoRepository
    {
        Task<List<ApiResponseMessage>> getasync(List<SP_AddUpdPlotDetails> input);
    }
}
