using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_PreCoolingRepository
    {
        Task<List<GET_PreCooling>> getasync(GET_PreCoolingDomain input);
        Task<List<GET_PreCooling>> getkeyword(GET_PreCoolingDomain input);
    }
}
