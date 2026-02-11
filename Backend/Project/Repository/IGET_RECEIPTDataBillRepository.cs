using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_RECEIPTDataBillRepository
    {
        Task<List<GET_RECEIPTDataBill>>getasync(GET_RECEIPTDataBillDomain input);
    }
}
