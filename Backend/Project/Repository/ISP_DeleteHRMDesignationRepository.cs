using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteHRMDesignationRepository
    {
        Task<List<ApiResponseMessage>>getasync(SP_DeleteHRMDesignation input);
    }
}
