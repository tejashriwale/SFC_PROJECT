using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteComplaintRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeleteComplaint input);
    }
}
