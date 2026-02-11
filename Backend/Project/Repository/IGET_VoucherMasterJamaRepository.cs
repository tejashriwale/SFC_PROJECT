using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_VoucherMasterJamaRepository
    {
        Task<List<GET_VoucherMasterJama>> getasync(GET_VoucherMasterJamaDomain input);
    }
}

