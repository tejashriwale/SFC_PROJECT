using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_HRM_Empoye_LeavesRepository
    {
        Task<List<GET_HRM_Empoye_Leaves>>getasync(GET_HRM_Empoye_LeavesDomain input);

        Task<List<GET_HRM_Empoye_Leaves>> getkeyword(GET_HRM_Empoye_LeavesDomain input);
    }
}
