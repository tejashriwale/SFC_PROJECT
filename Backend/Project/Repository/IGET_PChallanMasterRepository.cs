using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_PChallanMasterRepository
    {
        Task<List<GET_PChallanMaster>>getasync(GET_PChallanMasterDomain masterDomain);

        Task<List<GET_PChallanMaster>> getkeyword(GET_PChallanMasterDomain masterDomain);
    }
}
