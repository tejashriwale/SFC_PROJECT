using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_InsertVyapariDueDataRepository
    {
        Task<List<ApiResponseMessage>> getasync(List<SP_InsertVyapariDueData> input);
    }
}
