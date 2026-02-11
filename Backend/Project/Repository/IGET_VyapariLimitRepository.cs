using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_VyapariLimitRepository
    {
        Task<List<GET_VyapariLimit>>getasync(GET_VyapariLimitDomain input);
        Task<List<GET_VyapariLimit>>getkeyword(GET_VyapariLimitDomain input);
    }
}
