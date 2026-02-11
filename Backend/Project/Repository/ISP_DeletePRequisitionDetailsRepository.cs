using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeletePRequisitionDetailsRepository
    {
        Task<List<ApiResponseMessage>>getasync(SP_DeletePRequisitionDetailsDomain input);
    }
}
