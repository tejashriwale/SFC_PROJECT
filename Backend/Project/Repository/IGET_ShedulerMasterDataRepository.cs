using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_ShedulerMasterDataRepository
    {
        Task<List<GET_ShedulerMasterData>> getasync(GET_ShedulerMasterDataDomain input);
        Task<List<GET_ShedulerMasterData>> getkeyword(GET_ShedulerMasterDataDomain input);
    }
}
