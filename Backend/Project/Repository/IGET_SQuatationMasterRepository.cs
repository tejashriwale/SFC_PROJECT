using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_SQuatationMasterRepository
    {
        Task<List<GET_SQuatationMaster>> getasync(GET_SQuatationMasterDomain input);

        Task<List<GET_SQuatationMaster>> getkeyword(GET_SQuatationMasterDomain input);
    }
}
