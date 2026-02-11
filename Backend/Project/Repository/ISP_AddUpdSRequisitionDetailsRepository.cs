using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdSRequisitionDetailsRepository
    {
        Task<List<ApiResponseMessage>>getasync(List<SP_AddUpdSRequisitionDetails> sP_AddUpdSRequisitionDetails);
    }
}
