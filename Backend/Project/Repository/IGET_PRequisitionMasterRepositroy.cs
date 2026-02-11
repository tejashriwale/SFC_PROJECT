using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_PRequisitionMasterRepositroy
    {
        Task<List<GET_PRequisitionMaster>>getasync(GET_PRequisitionMasterDomain input);

        Task<List<GET_PRequisitionMaster>> getkeyword(GET_PRequisitionMasterDomain input);
    }
}
