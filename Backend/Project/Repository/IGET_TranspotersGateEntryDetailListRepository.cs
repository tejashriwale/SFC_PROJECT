using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_TranspotersGateEntryDetailListRepository
    {
        Task<List<GET_TranspotersGateEntryDetailList>> getasync(GET_TranspotersGateEntryDetailListDomain input);
    }
}
