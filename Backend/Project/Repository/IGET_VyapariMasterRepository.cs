using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Repository
{
    public interface IGET_VyapariMasterRepository
    {
        Task<List<GET_VyapariMaster>>getasync(GET_VyapariMasterDomain input);
        Task<List<GET_VyapariMaster>>getkeyword(GET_VyapariMasterDomain input);
    }
}
