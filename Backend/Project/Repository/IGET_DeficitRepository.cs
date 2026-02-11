using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_DeficitRepository
    {
        Task<List<GET_Deficit>> getasync(GET_DeficitDomain input);

        Task<List<GET_Deficit>> get(GET_DeficitDomain input);
    }
}
