using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_SRequisitionMasterRepository
    {
        Task<List<GET_SRequisitionMaster>>getasync(GET_SRequisitionMasterDomain input);

        Task<List<GET_SRequisitionMaster>> getkeyword(GET_SRequisitionMasterDomain input);
    }
}
