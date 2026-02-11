using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Repository
{
    public interface IGET_RokdaVyapariMasterRepository
    {
        Task<List<GET_RokdaVyapariMaster>>getasync(GET_RokdaVyapariMasterDomain input);
        Task<List<GET_RokdaVyapariMaster>>getkeyword(GET_RokdaVyapariMasterDomain input);
    }
}
