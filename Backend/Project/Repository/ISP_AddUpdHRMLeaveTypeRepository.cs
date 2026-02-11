using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdHRMLeaveTypeRepository
    {
        Task<List<ApiResponseMessage>>getasync(SP_AddUpdHRMLeaveType sP_AddUpdHRMLeaveType);
    }
}
