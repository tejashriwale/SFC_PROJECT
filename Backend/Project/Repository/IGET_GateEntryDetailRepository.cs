using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_GateEntryDetailRepository
    {
        Task<List<GET_GateEntryDetail>>getasync(GET_GateEntryDetailDomain input);
        Task<List<GET_GateEntryDetail>>getkeyword(GET_GateEntryDetailDomain input);
    }
}
