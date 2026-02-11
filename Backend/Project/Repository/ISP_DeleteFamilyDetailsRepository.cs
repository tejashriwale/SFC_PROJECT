using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteFamilyDetailsRepository
    {
        Task<List<ApiResponseMessage>>getasync(SP_DeleteFamilyDetails input);
    }
}
