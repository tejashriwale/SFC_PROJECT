using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_VoucherRepository
    {
        Task<List<GET_Voucher>> getasync(GET_VoucherDomain input);
        Task<List<GET_Voucher>> getkeyword(GET_VoucherDomain input);
    }
}
