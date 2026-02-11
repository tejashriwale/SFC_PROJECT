using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_DashboardCountRepository
    {
        Task<List<GET_DashboardCount>> getasync(GET_DashboardCountDomain input);
    }
}
