using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_VoucherJamaRepository
    {
        Task<List<GET_VoucherJama>> getasync(GET_VoucherJamaDomain input);
    }
}
