using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CreateBillRepository
    {
        Task<List<GET_CreateBill>>getasync(GET_CreateBillDomain input);
       
    }
}
