using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_Report_SalesRepository
    {
        Task<List<GET_Report_Sales>> getasync(GET_Report_SalesDomain input);
    }
}
