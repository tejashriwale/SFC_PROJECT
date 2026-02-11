using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_ScheduleDetailsRepository
    {
        Task<List<GET_ScheduleDetails>> getasync(GET_ScheduleDetailsDomain input);
    }
}
