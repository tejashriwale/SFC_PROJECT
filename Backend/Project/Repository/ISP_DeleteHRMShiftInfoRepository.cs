using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteHRMShiftInfoRepository
    {
        Task<List<ApiResponseMessage>>getasync(SP_DeleteHRMShiftInfo input);
    }
}
