using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_VoucherMasterRepository
    {
        Task<List<GET_VoucherMaster>> getasync(GET_VoucherMasterDomain input);
    }
}
