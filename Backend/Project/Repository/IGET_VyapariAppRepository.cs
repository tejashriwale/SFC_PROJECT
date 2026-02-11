using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_VyapariAppRepository
    {
        Task<List<GET_VyapariApp>> getasync(GET_VyapariAppDomain input);
        Task<List<GET_VyapariApp>> getkeyword(GET_VyapariAppDomain input);
    }
}
