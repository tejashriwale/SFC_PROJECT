using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_VerifyGateEntryRepository
    {
        Task<List<ApiResponseMessage>> getasync(List<SP_VerifyGateEntry> input);
    }
}
