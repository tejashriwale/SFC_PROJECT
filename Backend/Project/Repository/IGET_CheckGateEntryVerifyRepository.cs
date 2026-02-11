using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CheckGateEntryVerifyRepository
    {
        Task<List<GET_CheckGateEntryVerify>> getasync(GET_CheckGateEntryVerifyDomain input);
    }
}
