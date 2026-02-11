using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdFarmerFamilyRepository
    {
        Task<List<ApiResponseMessage>>getasync(List<SP_AddUpdFarmerFamily> input);
    }
}
