using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_RECEIPTMasterCashCounterRepositorycs
    {
        Task<List<GET_RECEIPTMasterCashCounter>> getasync(GET_RECEIPTMasterCashCounterDomain input);
        Task<List<GET_RECEIPTMasterCashCounter>> getkeyword(GET_RECEIPTMasterCashCounterDomain input);
    }
}
