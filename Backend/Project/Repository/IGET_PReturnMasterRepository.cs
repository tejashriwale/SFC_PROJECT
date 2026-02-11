using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_PReturnMasterRepository
    {
        Task<List<GET_PReturnMaster>>getasync(GET_PReturnMasterDomain input);

        Task<List<GET_PReturnMaster>> getkeyword(GET_PReturnMasterDomain input);
    }
}
