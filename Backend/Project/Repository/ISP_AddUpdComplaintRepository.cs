using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdComplaintRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_AddUpdComplaint input);
    }
}
