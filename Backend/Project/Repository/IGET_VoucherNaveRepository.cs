using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_VoucherNaveRepository
    {
        Task<List<GET_VoucherNave>> getasync (GET_VoucherNaveDomain input);
    }
}
