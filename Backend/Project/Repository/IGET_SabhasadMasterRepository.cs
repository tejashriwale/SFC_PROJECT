using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_SabhasadMasterRepository
    {
        Task<List<GET_SabhasadMaster>> getasync(GET_SabhasadMasterDomain input);
        Task<List<GET_SabhasadMaster>> getkeyword(GET_SabhasadMasterDomain input);
    }
}
