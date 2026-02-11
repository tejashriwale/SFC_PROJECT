using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_CheckAadhaardetailRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_CheckAadhaardetail input);
    }
}
