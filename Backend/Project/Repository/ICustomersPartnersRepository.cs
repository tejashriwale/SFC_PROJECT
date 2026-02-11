using Project.Model.Domain;

namespace Project.Repository
{
    public interface ICustomersPartnersRepository
    {
        Task<List<ApiResponseMessage>> GetAsync(CustomersPartners customersPartners);
    }
}
