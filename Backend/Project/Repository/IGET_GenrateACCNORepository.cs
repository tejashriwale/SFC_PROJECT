using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_GenrateACCNORepository
    {
        Task<List<GET_GenrateACCNO>> GetAll();
    }
}
