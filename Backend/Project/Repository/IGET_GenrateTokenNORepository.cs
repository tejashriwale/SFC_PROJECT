using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_GenrateTokenNORepository
    {
        Task<List<GET_GenrateTokenNO>>getasync(GET_GenrateTokenNODomain input);
    }
}
