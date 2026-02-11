using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_PAIDReportRepository
    {
        Task<List<GET_PAIDReport>> GetAsync(GET_PAIDReportDomain input);
    }
}
