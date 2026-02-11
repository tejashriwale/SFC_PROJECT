using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_FARMERAPPBillRepository
    {
        Task<List<GET_FARMERAPPBill>> getasync(GET_FARMERAPPBillDomain input);
    }
}
