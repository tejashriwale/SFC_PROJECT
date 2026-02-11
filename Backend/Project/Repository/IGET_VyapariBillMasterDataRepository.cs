using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_VyapariBillMasterDataRepository
    {
        Task<List<GET_VyapariBillMasterData>> getasync(GET_VyapariBillMasterDataDomain input);
        Task<List<GET_VyapariBillMasterData>> getkeyword(GET_VyapariBillMasterDataDomain input);
    }
}
