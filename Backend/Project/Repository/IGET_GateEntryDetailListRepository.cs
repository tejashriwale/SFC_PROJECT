using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_GateEntryDetailListRepository
    {
        Task<List<GET_GateEntryDetailList>>getasync(GET_GateEntryDetailListDomain input);
    }
}
