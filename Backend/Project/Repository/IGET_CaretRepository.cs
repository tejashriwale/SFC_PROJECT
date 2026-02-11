using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CaretRepository
    {
        Task<List<GET_Caret>> getasync(GET_CaretDomain input);
        Task<List<GET_Caret>> getkeyword(GET_CaretDomain input);
    }
}
