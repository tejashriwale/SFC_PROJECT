using Project.Model;
using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CustomerLedgerRepository
    {
        Task<List<GET_CustomerLedger>> getasync(GET_CustomerLedgerDomain input);
    }
}
