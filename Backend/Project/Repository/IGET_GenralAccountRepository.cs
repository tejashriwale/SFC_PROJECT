using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_GenralAccountRepository
    {
        Task<List<GET_GenralAccount>> getasync(GET_GenralAccountDomain input);
    }
}
