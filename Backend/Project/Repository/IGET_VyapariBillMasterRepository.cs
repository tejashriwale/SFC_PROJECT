using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_VyapariBillMasterRepository
    {
        Task<List<GET_VyapariBillMaster>> getasync(GET_VyapariBillMasterDomain input);
    }
}
