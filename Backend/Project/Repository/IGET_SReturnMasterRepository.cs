using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_SReturnMasterRepository
    {
        Task<List<GET_SReturnMaster>>getasync(GET_SReturnMasterDomain input);

        Task<List<GET_SReturnMaster>> getkeyword(GET_SReturnMasterDomain input);
    }
}
