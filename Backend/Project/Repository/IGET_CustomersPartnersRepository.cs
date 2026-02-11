using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CustomersPartnersRepository
    {
        Task<List<_GET_CustomersPartners_>> getasync(CustomerDomain input);
        Task<List<_GET_CustomersPartners_>> GetByGroupAsync(CustomerDomain input);
        Task<List<_GET_CustomersPartners_>> searchasync(CustomerDomain input);
    }
}
