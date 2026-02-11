using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteLeaveTypeRepository
    {
        Task<List<ApiResponseMessage>>getasync(SP_DeleteLeaveType input);
    }
}
