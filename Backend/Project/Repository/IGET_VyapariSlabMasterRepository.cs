using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_VyapariSlabMasterRepository
    {
        Task<List<GET_VyapariSlabMaster>> getasync(GET_VyapariSlabMasterDomain input);
        Task<List<GET_VyapariSlabMaster>> getkeyword(GET_VyapariSlabMasterDomain input);
    }
}
