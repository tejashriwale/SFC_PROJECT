using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_LeaveTypeRepository
    {
        Task<List<GET_LeaveType>>getasync(GET_LeaveTypeDomain input);

        Task<List<GET_LeaveType>> getkeyword(GET_LeaveTypeDomain input);
    }
}
