using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteVyapariDueRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeleteVyapariDue input);
    }
}
