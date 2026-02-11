using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_PBillMasterRepositroy
    {
        Task<List<GET_PBillMaster>> getasync(GET_PBillMasterDomain input);

        Task<List<GET_PBillMaster>> getkeyword(GET_PBillMasterDomain input);
    }
}
