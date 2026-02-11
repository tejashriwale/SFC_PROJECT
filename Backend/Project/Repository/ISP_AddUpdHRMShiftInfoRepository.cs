using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdHRMShiftInfoRepository
    {
        Task<List<ApiResponseMessage>>getasync(SP_AddUpdHRMShiftInfo input);
    }
}
