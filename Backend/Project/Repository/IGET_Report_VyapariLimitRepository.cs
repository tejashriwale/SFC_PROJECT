using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_Report_VyapariLimitRepository
    {
        Task<List<GET_Report_VyapariLimit>> getasync(GET_Report_VyapariLimitDomain input);
    }
}
