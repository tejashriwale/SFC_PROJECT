using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_FarmerLedgerReportRepository
    {
        Task<List<GET_FarmerLedgerReport>> getasync(GET_FarmerLedgerReportDomain input);
    }
}
