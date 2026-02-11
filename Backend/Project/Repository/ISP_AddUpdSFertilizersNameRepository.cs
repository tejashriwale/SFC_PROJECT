using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdSFertilizersNameRepository
    {
        Task<List<ApiResponseMessage>> getasync(List<SP_AddUpdSFertilizersName> input);
    }
}
