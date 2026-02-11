using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_SBillMasterRepository
    {
        Task<List<GET_SBillMaster>> getasync(GET_SBillMasterDomain input);

        Task<List<GET_SBillMaster>> getKeyword(GET_SBillMasterDomain input);
    }
}
