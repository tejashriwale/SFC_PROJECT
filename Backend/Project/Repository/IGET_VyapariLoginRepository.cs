using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_VyapariLoginRepository
    {
        Task<List<GET_VyapariLogin>> getasync(GET_VyapariLoginDomain input);
        Task<List<GET_VyapariLogin>> getkeyword(GET_VyapariLoginDomain input);
    }
}
