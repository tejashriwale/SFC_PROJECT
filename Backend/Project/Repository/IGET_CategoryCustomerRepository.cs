using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CategoryCustomerRepository
    {
        Task<List<GET_CategoryCustomer>>getasync(GET_CategoryCustomerDomain customer);
    }
}
