using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_AllTranDataCustomerRepository
    {
        Task<List<GET_AllTranDataCustomer>> getasync(GET_AllTranDataCustomerDomain input);
    }
}
