using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_UpdateVyapariDueRepository
    {
        Task<List<ApiResponseMessage>> getasync(List<SP_UpdateVyapariDue> input);
    }
}
