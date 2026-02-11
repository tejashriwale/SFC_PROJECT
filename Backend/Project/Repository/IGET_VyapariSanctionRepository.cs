using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_VyapariSanctionRepository 
    {
        Task<List<GET_VyapariSanction>>getasync(GET_VyapariSanctionDomain input);
        Task<List<GET_VyapariSanction>>getkeyword(GET_VyapariSanctionDomain input);
    }
}
