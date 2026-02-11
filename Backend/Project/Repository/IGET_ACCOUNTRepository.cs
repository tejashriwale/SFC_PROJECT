using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_ACCOUNTRepository
    {
        Task<List<GET_ACCOUNT>>getasync(GET_ACCOUNTDomain input);
        Task<List<GET_ACCOUNT>>getkeyword(GET_ACCOUNTDomain input);
    }
}
