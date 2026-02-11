using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_OnlineAmountRepository
    {
        Task<List<GET_OnlineAmount>> getasync(GET_OnlineAmountDomain input);
        Task<List<GET_OnlineAmount>> getkeyword(GET_OnlineAmountDomain input);
    }
}
