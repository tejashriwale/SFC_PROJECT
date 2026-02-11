using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_SchedulerMasterRepository
    {
        Task<List<GET_SchedulerMaster>> getasync(GET_SchedulerMasterDomain input);
    }
}
