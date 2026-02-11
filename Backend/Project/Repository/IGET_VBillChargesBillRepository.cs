using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_VBillChargesBillRepository
    {
        Task<List<GET_VBillChargesBill>> getasync(GET_VBillChargesBillDomain input);
    }
}
