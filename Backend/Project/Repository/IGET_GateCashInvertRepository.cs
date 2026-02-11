using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_GateCashInvertRepository
    {
        Task<List<GET_GateCashInvert>>getasync(GET_GateCashInvertDomain input);
        Task<List<GET_GateCashInvert>>getkeyword(GET_GateCashInvertDomain input);
    }
}
