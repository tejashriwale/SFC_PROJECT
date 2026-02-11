using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Repository
{
    public interface IGET_VechicleProfitReportRepository
    {
        Task<List<GET_VechicleProfitReport>> getasync(GET_VechicleProfitReportDomain input);
    }
}
