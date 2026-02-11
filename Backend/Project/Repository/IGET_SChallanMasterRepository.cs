using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_SChallanMasterRepository
    {
        Task<List<GET_SChallanMaster>> getasync(GET_SChallanMasterDomain input);

        Task<List<GET_SChallanMaster>> getkeyword(GET_SChallanMasterDomain input);
    }
}
