using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_GateBankDetailsRepository
    {
        Task<List<GET_GateBankDetails>>getasync(GET_GateBankDetailsDomain input);
        Task<List<GET_GateBankDetails>>getkeyword(GET_GateBankDetailsDomain input);
    }
}
