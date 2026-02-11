using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_SOrderMasterRepository
    {
        Task<List<GET_SOrderMaster>> getasync(GET_SOrderMasterDomain input);

        Task<List<GET_SOrderMaster>> getkeyword(GET_SOrderMasterDomain input);
    }
}
