using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_POrderMasterRepository
    {
        Task<List<GET_POrderMaster>> getasync(GET_POrderMasterDomain input);

        Task<List<GET_POrderMaster>> getkeyword(GET_POrderMasterDomain input);
    }
}
