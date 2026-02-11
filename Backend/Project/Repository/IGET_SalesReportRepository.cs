using Project.Model;
using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_SalesReportRepository
    {
        Task<List<GET_SalesReport>> getasync(GET_SalesReportDomain input);
    }
}
