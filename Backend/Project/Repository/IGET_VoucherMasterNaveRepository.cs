using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_VoucherMasterNaveRepository
    {
        Task<List<GET_VoucherMasterNave>> getasync(GET_VoucherMasterNaveDomain input);
    }
}
