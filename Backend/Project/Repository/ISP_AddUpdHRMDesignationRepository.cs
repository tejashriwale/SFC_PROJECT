using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdHRMDesignationRepository
    {
        Task<List<ApiResponseMessage>>getasync(SP_AddUpdHRMDesignation sP_AddUpdHRMDesignation);
    }
}
