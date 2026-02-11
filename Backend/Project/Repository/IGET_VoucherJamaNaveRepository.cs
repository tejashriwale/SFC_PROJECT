using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_VoucherJamaNaveRepository
    {
        Task<List<GET_VoucherJamaNave>> getasync(GET_VoucherJamaNaveDomain input);
    }
}
