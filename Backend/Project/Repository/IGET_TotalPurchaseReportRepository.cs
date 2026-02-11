using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_TotalPurchaseReportRepository
    {
        Task<List<GET_TotalPurchaseReport>> getasync(GET_TotalPurchaseReportDomain input);
    }
}
