using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_VyapariTransectionReportRepository
    {
        Task<List<GET_VyapariTransectionReport>> getasync(GET_VyapariTransectionReportDomain input);
    }
}
