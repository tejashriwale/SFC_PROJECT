using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_UpdateVyapariBillMasterRepository
    {
        Task<List<ApiResponseMessage>> getasync(List<SP_UpdateVyapariBillMaster> input);
    }
}
