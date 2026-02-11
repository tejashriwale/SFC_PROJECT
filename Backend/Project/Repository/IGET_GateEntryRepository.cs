using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_GateEntryRepository
    {
        Task<List<GET_GateEntry>>getasync(GET_GateEntryDomain input);
        Task<List<GET_GateEntry>>getkeyword(GET_GateEntryDomain input);
    }
}
