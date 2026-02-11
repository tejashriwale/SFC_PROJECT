using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_LedgerDataRepository
    {
        Task<List<GET_LedgerData>> getasync(GET_LedgerDataDomain input);
    }
}
